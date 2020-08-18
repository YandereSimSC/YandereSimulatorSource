using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200005D RID: 93
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Tween")]
public class UIPlayTween : MonoBehaviour
{
	// Token: 0x0600026B RID: 619 RVA: 0x000196C9 File Offset: 0x000178C9
	private void Awake()
	{
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x0600026C RID: 620 RVA: 0x000196F4 File Offset: 0x000178F4
	private void Start()
	{
		this.mStarted = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00019718 File Offset: 0x00017918
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (this.trigger == Trigger.OnPress || this.trigger == Trigger.OnPressTrue)
			{
				this.mActivated = (UICamera.currentTouch.pressed == base.gameObject);
			}
			if (this.trigger == Trigger.OnHover || this.trigger == Trigger.OnHoverTrue)
			{
				this.mActivated = (UICamera.currentTouch.current == base.gameObject);
			}
		}
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x000197C8 File Offset: 0x000179C8
	private void OnDisable()
	{
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x0600026F RID: 623 RVA: 0x000197FD File Offset: 0x000179FD
	private void OnDragOver()
	{
		if (this.trigger == Trigger.OnHover)
		{
			this.OnHover(true);
		}
	}

	// Token: 0x06000270 RID: 624 RVA: 0x00019810 File Offset: 0x00017A10
	private void OnHover(bool isOver)
	{
		if (base.enabled && (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver)))
		{
			if (isOver == this.mActivated)
			{
				return;
			}
			if (!isOver && UICamera.hoveredObject != null && UICamera.hoveredObject.transform.IsChildOf(base.transform))
			{
				UICamera.onHover = (UICamera.BoolDelegate)Delegate.Combine(UICamera.onHover, new UICamera.BoolDelegate(this.CustomHoverListener));
				isOver = true;
				if (this.mActivated)
				{
					return;
				}
			}
			this.mActivated = (isOver && this.trigger == Trigger.OnHover);
			this.Play(isOver);
		}
	}

	// Token: 0x06000271 RID: 625 RVA: 0x000198C4 File Offset: 0x00017AC4
	private void CustomHoverListener(GameObject go, bool isOver)
	{
		if (!this)
		{
			return;
		}
		GameObject gameObject = base.gameObject;
		if (!gameObject || !go || (!(go == gameObject) && !go.transform.IsChildOf(base.transform)))
		{
			this.OnHover(false);
			UICamera.onHover = (UICamera.BoolDelegate)Delegate.Remove(UICamera.onHover, new UICamera.BoolDelegate(this.CustomHoverListener));
		}
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0001993A File Offset: 0x00017B3A
	private void OnDragOut()
	{
		if (base.enabled && this.mActivated)
		{
			this.mActivated = false;
			this.Play(false);
		}
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0001995C File Offset: 0x00017B5C
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.mActivated = (isPressed && this.trigger == Trigger.OnPress);
			this.Play(isPressed);
		}
	}

	// Token: 0x06000274 RID: 628 RVA: 0x000199AF File Offset: 0x00017BAF
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x06000275 RID: 629 RVA: 0x000199C8 File Offset: 0x00017BC8
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x06000276 RID: 630 RVA: 0x000199E4 File Offset: 0x00017BE4
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			this.mActivated = (isSelected && this.trigger == Trigger.OnSelect);
			this.Play(isSelected);
		}
	}

	// Token: 0x06000277 RID: 631 RVA: 0x00019A3C File Offset: 0x00017C3C
	private void OnToggle()
	{
		if (!base.enabled || UIToggle.current == null)
		{
			return;
		}
		if (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (this.trigger == Trigger.OnActivateFalse && !UIToggle.current.value))
		{
			this.Play(UIToggle.current.value);
		}
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00019AA4 File Offset: 0x00017CA4
	private void Update()
	{
		if (this.disableWhenFinished != DisableCondition.DoNotDisable && this.mTweens != null)
		{
			bool flag = true;
			bool flag2 = true;
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (uitweener.enabled)
					{
						flag = false;
						break;
					}
					if (uitweener.direction != (AnimationOrTween.Direction)this.disableWhenFinished)
					{
						flag2 = false;
					}
				}
				i++;
			}
			if (flag)
			{
				if (flag2)
				{
					NGUITools.SetActive(this.tweenTarget, false);
				}
				this.mTweens = null;
			}
		}
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00019B2A File Offset: 0x00017D2A
	public void Play()
	{
		this.Play(true);
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00019B34 File Offset: 0x00017D34
	public void Play(bool forward)
	{
		this.mActive = 0;
		GameObject gameObject = (this.tweenTarget == null) ? base.gameObject : this.tweenTarget;
		if (!NGUITools.GetActive(gameObject))
		{
			if (this.ifDisabledOnPlay != EnableCondition.EnableThenPlay)
			{
				return;
			}
			NGUITools.SetActive(gameObject, true);
		}
		this.mTweens = (this.includeChildren ? gameObject.GetComponentsInChildren<UITweener>() : gameObject.GetComponents<UITweener>());
		if (this.mTweens.Length == 0)
		{
			if (this.disableWhenFinished != DisableCondition.DoNotDisable)
			{
				NGUITools.SetActive(this.tweenTarget, false);
				return;
			}
		}
		else
		{
			bool flag = false;
			if (this.playDirection == AnimationOrTween.Direction.Reverse)
			{
				forward = !forward;
			}
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (!flag && !NGUITools.GetActive(gameObject))
					{
						flag = true;
						NGUITools.SetActive(gameObject, true);
					}
					this.mActive++;
					if (this.playDirection == AnimationOrTween.Direction.Toggle)
					{
						EventDelegate.Add(uitweener.onFinished, new EventDelegate.Callback(this.OnFinished), true);
						uitweener.Toggle();
					}
					else
					{
						if (this.resetOnPlay || (this.resetIfDisabled && !uitweener.enabled))
						{
							uitweener.Play(forward);
							uitweener.ResetToBeginning();
						}
						EventDelegate.Add(uitweener.onFinished, new EventDelegate.Callback(this.OnFinished), true);
						uitweener.Play(forward);
					}
				}
				i++;
			}
		}
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00019C9C File Offset: 0x00017E9C
	private void OnFinished()
	{
		int num = this.mActive - 1;
		this.mActive = num;
		if (num == 0 && UIPlayTween.current == null)
		{
			UIPlayTween.current = this;
			EventDelegate.Execute(this.onFinished);
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, SendMessageOptions.DontRequireReceiver);
			}
			this.eventReceiver = null;
			UIPlayTween.current = null;
		}
	}

	// Token: 0x040003D2 RID: 978
	public static UIPlayTween current;

	// Token: 0x040003D3 RID: 979
	public GameObject tweenTarget;

	// Token: 0x040003D4 RID: 980
	public int tweenGroup;

	// Token: 0x040003D5 RID: 981
	public Trigger trigger;

	// Token: 0x040003D6 RID: 982
	public AnimationOrTween.Direction playDirection = AnimationOrTween.Direction.Forward;

	// Token: 0x040003D7 RID: 983
	public bool resetOnPlay;

	// Token: 0x040003D8 RID: 984
	public bool resetIfDisabled;

	// Token: 0x040003D9 RID: 985
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x040003DA RID: 986
	public DisableCondition disableWhenFinished;

	// Token: 0x040003DB RID: 987
	public bool includeChildren;

	// Token: 0x040003DC RID: 988
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x040003DD RID: 989
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x040003DE RID: 990
	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	// Token: 0x040003DF RID: 991
	private UITweener[] mTweens;

	// Token: 0x040003E0 RID: 992
	private bool mStarted;

	// Token: 0x040003E1 RID: 993
	private int mActive;

	// Token: 0x040003E2 RID: 994
	private bool mActivated;
}
