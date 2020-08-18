using System;
using UnityEngine;

// Token: 0x020003FE RID: 1022
public class StringScript : MonoBehaviour
{
	// Token: 0x06001B06 RID: 6918 RVA: 0x00110C1B File Offset: 0x0010EE1B
	private void Start()
	{
		if (this.ArrayID == 0)
		{
			this.Target.position = this.Origin.position;
		}
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x00110C3C File Offset: 0x0010EE3C
	private void Update()
	{
		this.String.position = this.Origin.position;
		this.String.LookAt(this.Target);
		this.String.localScale = new Vector3(this.String.localScale.x, this.String.localScale.y, Vector3.Distance(this.Origin.position, this.Target.position) * 0.5f);
	}

	// Token: 0x04002C1C RID: 11292
	public Transform Origin;

	// Token: 0x04002C1D RID: 11293
	public Transform Target;

	// Token: 0x04002C1E RID: 11294
	public Transform String;

	// Token: 0x04002C1F RID: 11295
	public int ArrayID;
}
