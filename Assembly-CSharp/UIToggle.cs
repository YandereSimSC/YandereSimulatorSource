using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000067 RID: 103
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggle")]
public class UIToggle : UIWidgetContainer
{
	// Token: 0x17000052 RID: 82
	// (get) Token: 0x0600031C RID: 796 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
	// (set) Token: 0x0600031D RID: 797 RVA: 0x0001ECDB File Offset: 0x0001CEDB
	public bool value
	{
		get
		{
			if (!this.mStarted)
			{
				return this.startsActive;
			}
			return this.mIsActive;
		}
		set
		{
			if (!this.mStarted)
			{
				this.startsActive = value;
				return;
			}
			if (this.group == 0 || value || this.optionCanBeNone || !this.mStarted)
			{
				this.Set(value, true);
			}
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x0600031E RID: 798 RVA: 0x0001ED14 File Offset: 0x0001CF14
	public bool isColliderEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x0600031F RID: 799 RVA: 0x0001ED50 File Offset: 0x0001CF50
	// (set) Token: 0x06000320 RID: 800 RVA: 0x0001ED58 File Offset: 0x0001CF58
	[Obsolete("Use 'value' instead")]
	public bool isChecked
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0001ED64 File Offset: 0x0001CF64
	public static UIToggle GetActiveToggle(int group)
	{
		for (int i = 0; i < UIToggle.list.size; i++)
		{
			UIToggle uitoggle = UIToggle.list.buffer[i];
			if (uitoggle != null && uitoggle.group == group && uitoggle.mIsActive)
			{
				return uitoggle;
			}
		}
		return null;
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
	private void OnEnable()
	{
		UIToggle.list.Add(this);
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0001EDBD File Offset: 0x0001CFBD
	private void OnDisable()
	{
		UIToggle.list.Remove(this);
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0001EDCC File Offset: 0x0001CFCC
	public void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		if (this.startsChecked)
		{
			this.startsChecked = false;
			this.startsActive = true;
		}
		if (!Application.isPlaying)
		{
			if (this.checkSprite != null && this.activeSprite == null)
			{
				this.activeSprite = this.checkSprite;
				this.checkSprite = null;
			}
			if (this.checkAnimation != null && this.activeAnimation == null)
			{
				this.activeAnimation = this.checkAnimation;
				this.checkAnimation = null;
			}
			if (Application.isPlaying && this.activeSprite != null)
			{
				this.activeSprite.alpha = (this.invertSpriteState ? (this.startsActive ? 0f : 1f) : (this.startsActive ? 1f : 0f));
			}
			if (EventDelegate.IsValid(this.onChange))
			{
				this.eventReceiver = null;
				this.functionName = null;
				return;
			}
		}
		else
		{
			this.mIsActive = !this.startsActive;
			this.mStarted = true;
			bool flag = this.instantTween;
			this.instantTween = true;
			this.Set(this.startsActive, true);
			this.instantTween = flag;
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0001EF06 File Offset: 0x0001D106
	private void OnClick()
	{
		if (base.enabled && this.isColliderEnabled && UICamera.currentTouchID != -2)
		{
			this.value = !this.value;
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0001EF30 File Offset: 0x0001D130
	public void Set(bool state, bool notify = true)
	{
		if (this.validator != null && !this.validator(state))
		{
			return;
		}
		if (!this.mStarted)
		{
			this.mIsActive = state;
			this.startsActive = state;
			if (this.activeSprite != null)
			{
				this.activeSprite.alpha = (this.invertSpriteState ? (state ? 0f : 1f) : (state ? 1f : 0f));
				return;
			}
		}
		else if (this.mIsActive != state)
		{
			if (this.group != 0 && state)
			{
				int i = 0;
				int size = UIToggle.list.size;
				while (i < size)
				{
					UIToggle uitoggle = UIToggle.list.buffer[i];
					if (uitoggle != this && uitoggle.group == this.group)
					{
						uitoggle.Set(false, true);
					}
					if (UIToggle.list.size != size)
					{
						size = UIToggle.list.size;
						i = 0;
					}
					else
					{
						i++;
					}
				}
			}
			this.mIsActive = state;
			if (this.activeSprite != null)
			{
				if (this.instantTween || !NGUITools.GetActive(this))
				{
					this.activeSprite.alpha = (this.invertSpriteState ? (this.mIsActive ? 0f : 1f) : (this.mIsActive ? 1f : 0f));
				}
				else
				{
					TweenAlpha.Begin(this.activeSprite.gameObject, 0.15f, this.invertSpriteState ? (this.mIsActive ? 0f : 1f) : (this.mIsActive ? 1f : 0f), 0f);
				}
			}
			if (notify && UIToggle.current == null)
			{
				UIToggle uitoggle2 = UIToggle.current;
				UIToggle.current = this;
				if (EventDelegate.IsValid(this.onChange))
				{
					EventDelegate.Execute(this.onChange);
				}
				else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
				{
					this.eventReceiver.SendMessage(this.functionName, this.mIsActive, SendMessageOptions.DontRequireReceiver);
				}
				UIToggle.current = uitoggle2;
			}
			if (this.animator != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.animator, null, state ? AnimationOrTween.Direction.Forward : AnimationOrTween.Direction.Reverse, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
				if (activeAnimation != null && (this.instantTween || !NGUITools.GetActive(this)))
				{
					activeAnimation.Finish();
					return;
				}
			}
			else if (this.activeAnimation != null)
			{
				ActiveAnimation activeAnimation2 = ActiveAnimation.Play(this.activeAnimation, null, state ? AnimationOrTween.Direction.Forward : AnimationOrTween.Direction.Reverse, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
				if (activeAnimation2 != null && (this.instantTween || !NGUITools.GetActive(this)))
				{
					activeAnimation2.Finish();
					return;
				}
			}
			else if (this.tween != null)
			{
				bool active = NGUITools.GetActive(this);
				if (this.tween.tweenGroup != 0)
				{
					UITweener[] componentsInChildren = this.tween.GetComponentsInChildren<UITweener>(true);
					int j = 0;
					int num = componentsInChildren.Length;
					while (j < num)
					{
						UITweener uitweener = componentsInChildren[j];
						if (uitweener.tweenGroup == this.tween.tweenGroup)
						{
							uitweener.Play(state);
							if (this.instantTween || !active)
							{
								uitweener.tweenFactor = (state ? 1f : 0f);
							}
						}
						j++;
					}
					return;
				}
				this.tween.Play(state);
				if (this.instantTween || !active)
				{
					this.tween.tweenFactor = (state ? 1f : 0f);
				}
			}
		}
	}

	// Token: 0x04000465 RID: 1125
	public static BetterList<UIToggle> list = new BetterList<UIToggle>();

	// Token: 0x04000466 RID: 1126
	public static UIToggle current;

	// Token: 0x04000467 RID: 1127
	public int group;

	// Token: 0x04000468 RID: 1128
	public UIWidget activeSprite;

	// Token: 0x04000469 RID: 1129
	public bool invertSpriteState;

	// Token: 0x0400046A RID: 1130
	public Animation activeAnimation;

	// Token: 0x0400046B RID: 1131
	public Animator animator;

	// Token: 0x0400046C RID: 1132
	public UITweener tween;

	// Token: 0x0400046D RID: 1133
	public bool startsActive;

	// Token: 0x0400046E RID: 1134
	public bool instantTween;

	// Token: 0x0400046F RID: 1135
	public bool optionCanBeNone;

	// Token: 0x04000470 RID: 1136
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x04000471 RID: 1137
	public UIToggle.Validate validator;

	// Token: 0x04000472 RID: 1138
	[HideInInspector]
	[SerializeField]
	private UISprite checkSprite;

	// Token: 0x04000473 RID: 1139
	[HideInInspector]
	[SerializeField]
	private Animation checkAnimation;

	// Token: 0x04000474 RID: 1140
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000475 RID: 1141
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnActivate";

	// Token: 0x04000476 RID: 1142
	[HideInInspector]
	[SerializeField]
	private bool startsChecked;

	// Token: 0x04000477 RID: 1143
	private bool mIsActive = true;

	// Token: 0x04000478 RID: 1144
	private bool mStarted;

	// Token: 0x0200062D RID: 1581
	// (Invoke) Token: 0x06002A7B RID: 10875
	public delegate bool Validate(bool choice);
}
