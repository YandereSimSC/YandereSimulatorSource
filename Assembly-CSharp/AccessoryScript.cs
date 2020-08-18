﻿using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class AccessoryScript : MonoBehaviour
{
	// Token: 0x060009E3 RID: 2531 RVA: 0x0004D058 File Offset: 0x0004B258
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Prompt.MyCollider.enabled = false;
			base.transform.parent = this.Target;
			base.transform.localPosition = new Vector3(this.X, this.Y, this.Z);
			base.transform.localEulerAngles = Vector3.zero;
			base.enabled = false;
		}
	}

	// Token: 0x0400085D RID: 2141
	public PromptScript Prompt;

	// Token: 0x0400085E RID: 2142
	public Transform Target;

	// Token: 0x0400085F RID: 2143
	public float X;

	// Token: 0x04000860 RID: 2144
	public float Y;

	// Token: 0x04000861 RID: 2145
	public float Z;
}
