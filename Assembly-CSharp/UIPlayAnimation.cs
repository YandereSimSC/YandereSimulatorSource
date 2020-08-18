using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200005B RID: 91
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Animation")]
public class UIPlayAnimation : MonoBehaviour
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x0600024D RID: 589 RVA: 0x00018F25 File Offset: 0x00017125
	private bool dualState
	{
		get
		{
			return this.trigger == Trigger.OnPress || this.trigger == Trigger.OnHover;
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00018F3C File Offset: 0x0001713C
	private void Awake()
	{
		UIButton component = base.GetComponent<UIButton>();
		if (component != null)
		{
			this.dragHighlight = component.dragHighlight;
		}
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00018F90 File Offset: 0x00017190
	private void Start()
	{
		this.mStarted = true;
		if (this.target == null && this.animator == null)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		if (this.animator != null)
		{
			if (this.animator.enabled)
			{
				this.animator.enabled = false;
			}
			return;
		}
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
		if (this.target != null && this.target.enabled)
		{
			this.target.enabled = false;
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00019038 File Offset: 0x00017238
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

	// Token: 0x06000251 RID: 593 RVA: 0x000190E8 File Offset: 0x000172E8
	private void OnDisable()
	{
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0001911D File Offset: 0x0001731D
	private void OnHover(bool isOver)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver))
		{
			this.Play(isOver, this.dualState);
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00019158 File Offset: 0x00017358
	private void OnPress(bool isPressed)
	{
		if (!base.enabled)
		{
			return;
		}
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed))
		{
			this.Play(isPressed, this.dualState);
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x000191B0 File Offset: 0x000173B0
	private void OnClick()
	{
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x06000255 RID: 597 RVA: 0x000191DD File Offset: 0x000173DD
	private void OnDoubleClick()
	{
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0001920C File Offset: 0x0001740C
	private void OnSelect(bool isSelected)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected))
		{
			this.Play(isSelected, this.dualState);
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0001924C File Offset: 0x0001744C
	private void OnToggle()
	{
		if (!base.enabled || UIToggle.current == null)
		{
			return;
		}
		if (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (this.trigger == Trigger.OnActivateFalse && !UIToggle.current.value))
		{
			this.Play(UIToggle.current.value, this.dualState);
		}
	}

	// Token: 0x06000258 RID: 600 RVA: 0x000192BC File Offset: 0x000174BC
	private void OnDragOver()
	{
		if (base.enabled && this.dualState)
		{
			if (UICamera.currentTouch.dragged == base.gameObject)
			{
				this.Play(true, true);
				return;
			}
			if (this.dragHighlight && this.trigger == Trigger.OnPress)
			{
				this.Play(true, true);
			}
		}
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00019312 File Offset: 0x00017512
	private void OnDragOut()
	{
		if (base.enabled && this.dualState && UICamera.hoveredObject != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0001933E File Offset: 0x0001753E
	private void OnDrop(GameObject go)
	{
		if (base.enabled && this.trigger == Trigger.OnPress && UICamera.currentTouch.dragged != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00019370 File Offset: 0x00017570
	public void Play(bool forward)
	{
		this.Play(forward, true);
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0001937C File Offset: 0x0001757C
	public void Play(bool forward, bool onlyIfDifferent)
	{
		if (this.target || this.animator)
		{
			if (onlyIfDifferent)
			{
				if (this.mActivated == forward)
				{
					return;
				}
				this.mActivated = forward;
			}
			if (this.clearSelection && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			int num = (int)(-(int)this.playDirection);
			AnimationOrTween.Direction direction = forward ? this.playDirection : ((AnimationOrTween.Direction)num);
			ActiveAnimation activeAnimation = this.target ? ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished) : ActiveAnimation.Play(this.animator, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
			if (activeAnimation != null)
			{
				if (this.resetOnPlay)
				{
					activeAnimation.Reset();
				}
				for (int i = 0; i < this.onFinished.Count; i++)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
				}
			}
		}
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00019481 File Offset: 0x00017681
	public void PlayForward()
	{
		this.Play(true);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0001948A File Offset: 0x0001768A
	public void PlayReverse()
	{
		this.Play(false);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00019494 File Offset: 0x00017694
	private void OnFinished()
	{
		if (UIPlayAnimation.current == null)
		{
			UIPlayAnimation.current = this;
			EventDelegate.Execute(this.onFinished);
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, SendMessageOptions.DontRequireReceiver);
			}
			this.eventReceiver = null;
			UIPlayAnimation.current = null;
		}
	}

	// Token: 0x040003BD RID: 957
	public static UIPlayAnimation current;

	// Token: 0x040003BE RID: 958
	public Animation target;

	// Token: 0x040003BF RID: 959
	public Animator animator;

	// Token: 0x040003C0 RID: 960
	public string clipName;

	// Token: 0x040003C1 RID: 961
	public Trigger trigger;

	// Token: 0x040003C2 RID: 962
	public AnimationOrTween.Direction playDirection = AnimationOrTween.Direction.Forward;

	// Token: 0x040003C3 RID: 963
	public bool resetOnPlay;

	// Token: 0x040003C4 RID: 964
	public bool clearSelection;

	// Token: 0x040003C5 RID: 965
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x040003C6 RID: 966
	public DisableCondition disableWhenFinished;

	// Token: 0x040003C7 RID: 967
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x040003C8 RID: 968
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x040003C9 RID: 969
	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	// Token: 0x040003CA RID: 970
	private bool mStarted;

	// Token: 0x040003CB RID: 971
	private bool mActivated;

	// Token: 0x040003CC RID: 972
	private bool dragHighlight;
}
