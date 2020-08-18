using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class UIShowControlScheme : MonoBehaviour
{
	// Token: 0x060002FE RID: 766 RVA: 0x0001E203 File Offset: 0x0001C403
	private void OnEnable()
	{
		UICamera.onSchemeChange = (UICamera.OnSchemeChange)Delegate.Combine(UICamera.onSchemeChange, new UICamera.OnSchemeChange(this.OnScheme));
		this.OnScheme();
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0001E22B File Offset: 0x0001C42B
	private void OnDisable()
	{
		UICamera.onSchemeChange = (UICamera.OnSchemeChange)Delegate.Remove(UICamera.onSchemeChange, new UICamera.OnSchemeChange(this.OnScheme));
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0001E250 File Offset: 0x0001C450
	private void OnScheme()
	{
		if (this.target != null)
		{
			UICamera.ControlScheme currentScheme = UICamera.currentScheme;
			if (currentScheme == UICamera.ControlScheme.Mouse)
			{
				this.target.SetActive(this.mouse);
				return;
			}
			if (currentScheme == UICamera.ControlScheme.Touch)
			{
				this.target.SetActive(this.touch);
				return;
			}
			if (currentScheme == UICamera.ControlScheme.Controller)
			{
				this.target.SetActive(this.controller);
			}
		}
	}

	// Token: 0x04000450 RID: 1104
	public GameObject target;

	// Token: 0x04000451 RID: 1105
	public bool mouse;

	// Token: 0x04000452 RID: 1106
	public bool touch;

	// Token: 0x04000453 RID: 1107
	public bool controller = true;
}
