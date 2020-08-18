using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000221 RID: 545 RVA: 0x00018264 File Offset: 0x00016464
	// (set) Token: 0x06000222 RID: 546 RVA: 0x00018290 File Offset: 0x00016490
	public bool isEnabled
	{
		get
		{
			Collider component = base.gameObject.GetComponent<Collider>();
			return component && component.enabled;
		}
		set
		{
			Collider component = base.gameObject.GetComponent<Collider>();
			if (!component)
			{
				return;
			}
			if (component.enabled != value)
			{
				component.enabled = value;
				this.UpdateImage();
			}
		}
	}

	// Token: 0x06000223 RID: 547 RVA: 0x000182C8 File Offset: 0x000164C8
	private void OnEnable()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
		this.UpdateImage();
	}

	// Token: 0x06000224 RID: 548 RVA: 0x000182EC File Offset: 0x000164EC
	private void OnValidate()
	{
		if (this.target != null)
		{
			if (string.IsNullOrEmpty(this.normalSprite))
			{
				this.normalSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.hoverSprite))
			{
				this.hoverSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.pressedSprite))
			{
				this.pressedSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.disabledSprite))
			{
				this.disabledSprite = this.target.spriteName;
			}
		}
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00018380 File Offset: 0x00016580
	private void UpdateImage()
	{
		if (this.target != null)
		{
			if (this.isEnabled)
			{
				this.SetSprite(UICamera.IsHighlighted(base.gameObject) ? this.hoverSprite : this.normalSprite);
				return;
			}
			this.SetSprite(this.disabledSprite);
		}
	}

	// Token: 0x06000226 RID: 550 RVA: 0x000183D1 File Offset: 0x000165D1
	private void OnHover(bool isOver)
	{
		if (this.isEnabled && this.target != null)
		{
			this.SetSprite(isOver ? this.hoverSprite : this.normalSprite);
		}
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00018400 File Offset: 0x00016600
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.SetSprite(this.pressedSprite);
			return;
		}
		this.UpdateImage();
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00018418 File Offset: 0x00016618
	private void SetSprite(string sprite)
	{
		if (string.IsNullOrEmpty(sprite))
		{
			return;
		}
		INGUIAtlas atlas = this.target.atlas;
		if (atlas == null)
		{
			return;
		}
		INGUIAtlas inguiatlas = atlas;
		if (inguiatlas == null || inguiatlas.GetSprite(sprite) == null)
		{
			return;
		}
		this.target.spriteName = sprite;
		if (this.pixelSnap)
		{
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x040003A5 RID: 933
	public UISprite target;

	// Token: 0x040003A6 RID: 934
	public string normalSprite;

	// Token: 0x040003A7 RID: 935
	public string hoverSprite;

	// Token: 0x040003A8 RID: 936
	public string pressedSprite;

	// Token: 0x040003A9 RID: 937
	public string disabledSprite;

	// Token: 0x040003AA RID: 938
	public bool pixelSnap = true;
}
