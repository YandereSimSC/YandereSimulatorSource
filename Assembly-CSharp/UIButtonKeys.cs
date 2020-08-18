using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
public class UIButtonKeys : UIKeyNavigation
{
	// Token: 0x06000181 RID: 385 RVA: 0x000149B7 File Offset: 0x00012BB7
	protected override void OnEnable()
	{
		this.Upgrade();
		base.OnEnable();
	}

	// Token: 0x06000182 RID: 386 RVA: 0x000149C8 File Offset: 0x00012BC8
	public void Upgrade()
	{
		if (this.onClick == null && this.selectOnClick != null)
		{
			this.onClick = this.selectOnClick.gameObject;
			this.selectOnClick = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.onLeft == null && this.selectOnLeft != null)
		{
			this.onLeft = this.selectOnLeft.gameObject;
			this.selectOnLeft = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.onRight == null && this.selectOnRight != null)
		{
			this.onRight = this.selectOnRight.gameObject;
			this.selectOnRight = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.onUp == null && this.selectOnUp != null)
		{
			this.onUp = this.selectOnUp.gameObject;
			this.selectOnUp = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.onDown == null && this.selectOnDown != null)
		{
			this.onDown = this.selectOnDown.gameObject;
			this.selectOnDown = null;
			NGUITools.SetDirty(this, "last change");
		}
	}

	// Token: 0x04000310 RID: 784
	public UIButtonKeys selectOnClick;

	// Token: 0x04000311 RID: 785
	public UIButtonKeys selectOnUp;

	// Token: 0x04000312 RID: 786
	public UIButtonKeys selectOnDown;

	// Token: 0x04000313 RID: 787
	public UIButtonKeys selectOnLeft;

	// Token: 0x04000314 RID: 788
	public UIButtonKeys selectOnRight;
}
