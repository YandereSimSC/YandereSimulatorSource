using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000043 RID: 67
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600015D RID: 349 RVA: 0x00013F54 File Offset: 0x00012154
	// (set) Token: 0x0600015E RID: 350 RVA: 0x00013FA0 File Offset: 0x000121A0
	public override bool isEnabled
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			Collider component = base.gameObject.GetComponent<Collider>();
			if (component && component.enabled)
			{
				return true;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 && component2.enabled;
		}
		set
		{
			if (this.isEnabled != value)
			{
				Collider component = base.gameObject.GetComponent<Collider>();
				if (component != null)
				{
					component.enabled = value;
					UIButton[] components = base.GetComponents<UIButton>();
					for (int i = 0; i < components.Length; i++)
					{
						components[i].SetState(value ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled, false);
					}
					return;
				}
				Collider2D component2 = base.GetComponent<Collider2D>();
				if (component2 != null)
				{
					component2.enabled = value;
					UIButton[] components = base.GetComponents<UIButton>();
					for (int i = 0; i < components.Length; i++)
					{
						components[i].SetState(value ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled, false);
					}
					return;
				}
				base.enabled = value;
			}
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600015F RID: 351 RVA: 0x0001403F File Offset: 0x0001223F
	// (set) Token: 0x06000160 RID: 352 RVA: 0x00014058 File Offset: 0x00012258
	public string normalSprite
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mNormalSprite;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.mSprite != null && !string.IsNullOrEmpty(this.mNormalSprite) && this.mNormalSprite == this.mSprite.spriteName)
			{
				this.mNormalSprite = value;
				this.SetSprite(value);
				NGUITools.SetDirty(this.mSprite, "last change");
				return;
			}
			this.mNormalSprite = value;
			if (this.mState == UIButtonColor.State.Normal)
			{
				this.SetSprite(value);
			}
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000161 RID: 353 RVA: 0x000140DB File Offset: 0x000122DB
	// (set) Token: 0x06000162 RID: 354 RVA: 0x000140F4 File Offset: 0x000122F4
	public Sprite normalSprite2D
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mNormalSprite2D;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.mSprite2D != null && this.mNormalSprite2D == this.mSprite2D.sprite2D)
			{
				this.mNormalSprite2D = value;
				this.SetSprite(value);
				NGUITools.SetDirty(this.mSprite, "last change");
				return;
			}
			this.mNormalSprite2D = value;
			if (this.mState == UIButtonColor.State.Normal)
			{
				this.SetSprite(value);
			}
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0001416C File Offset: 0x0001236C
	protected override void OnInit()
	{
		base.OnInit();
		this.mSprite = (this.mWidget as UISprite);
		this.mSprite2D = (this.mWidget as UI2DSprite);
		if (this.mSprite != null)
		{
			this.mNormalSprite = this.mSprite.spriteName;
		}
		if (this.mSprite2D != null)
		{
			this.mNormalSprite2D = this.mSprite2D.sprite2D;
		}
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000141DF File Offset: 0x000123DF
	protected override void OnEnable()
	{
		if (this.isEnabled)
		{
			if (this.mInitDone)
			{
				this.OnHover(UICamera.hoveredObject == base.gameObject);
				return;
			}
		}
		else
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00014210 File Offset: 0x00012410
	protected override void OnDragOver()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOver();
		}
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0001423F File Offset: 0x0001243F
	protected override void OnDragOut()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOut();
		}
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0001426E File Offset: 0x0001246E
	protected virtual void OnClick()
	{
		if (UIButton.current == null && this.isEnabled && UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3)
		{
			UIButton.current = this;
			EventDelegate.Execute(this.onClick);
			UIButton.current = null;
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000142B0 File Offset: 0x000124B0
	public override void SetState(UIButtonColor.State state, bool immediate)
	{
		base.SetState(state, immediate);
		if (!(this.mSprite != null))
		{
			if (this.mSprite2D != null)
			{
				switch (state)
				{
				case UIButtonColor.State.Normal:
					this.SetSprite(this.mNormalSprite2D);
					return;
				case UIButtonColor.State.Hover:
					this.SetSprite((this.hoverSprite2D == null) ? this.mNormalSprite2D : this.hoverSprite2D);
					return;
				case UIButtonColor.State.Pressed:
					this.SetSprite(this.pressedSprite2D);
					return;
				case UIButtonColor.State.Disabled:
					this.SetSprite(this.disabledSprite2D);
					break;
				default:
					return;
				}
			}
			return;
		}
		switch (state)
		{
		case UIButtonColor.State.Normal:
			this.SetSprite(this.mNormalSprite);
			return;
		case UIButtonColor.State.Hover:
			this.SetSprite(string.IsNullOrEmpty(this.hoverSprite) ? this.mNormalSprite : this.hoverSprite);
			return;
		case UIButtonColor.State.Pressed:
			this.SetSprite(this.pressedSprite);
			return;
		case UIButtonColor.State.Disabled:
			this.SetSprite(this.disabledSprite);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000143A4 File Offset: 0x000125A4
	protected void SetSprite(string sp)
	{
		if (this.mSprite != null && !string.IsNullOrEmpty(sp) && this.mSprite.spriteName != sp)
		{
			this.mSprite.spriteName = sp;
			if (this.pixelSnap)
			{
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000143FC File Offset: 0x000125FC
	protected void SetSprite(Sprite sp)
	{
		if (sp != null && this.mSprite2D != null && this.mSprite2D.sprite2D != sp)
		{
			this.mSprite2D.sprite2D = sp;
			if (this.pixelSnap)
			{
				this.mSprite2D.MakePixelPerfect();
			}
		}
	}

	// Token: 0x040002F6 RID: 758
	public static UIButton current;

	// Token: 0x040002F7 RID: 759
	public bool dragHighlight;

	// Token: 0x040002F8 RID: 760
	public string hoverSprite;

	// Token: 0x040002F9 RID: 761
	public string pressedSprite;

	// Token: 0x040002FA RID: 762
	public string disabledSprite;

	// Token: 0x040002FB RID: 763
	public Sprite hoverSprite2D;

	// Token: 0x040002FC RID: 764
	public Sprite pressedSprite2D;

	// Token: 0x040002FD RID: 765
	public Sprite disabledSprite2D;

	// Token: 0x040002FE RID: 766
	public bool pixelSnap;

	// Token: 0x040002FF RID: 767
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x04000300 RID: 768
	[NonSerialized]
	private UISprite mSprite;

	// Token: 0x04000301 RID: 769
	[NonSerialized]
	private UI2DSprite mSprite2D;

	// Token: 0x04000302 RID: 770
	[NonSerialized]
	private string mNormalSprite;

	// Token: 0x04000303 RID: 771
	[NonSerialized]
	private Sprite mNormalSprite2D;
}
