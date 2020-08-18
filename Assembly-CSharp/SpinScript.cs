using System;
using UnityEngine;

// Token: 0x020003EA RID: 1002
public class SpinScript : MonoBehaviour
{
	// Token: 0x06001AC2 RID: 6850 RVA: 0x0010C45C File Offset: 0x0010A65C
	private void Update()
	{
		this.RotationX += this.X * Time.deltaTime;
		this.RotationY += this.Y * Time.deltaTime;
		this.RotationZ += this.Z * Time.deltaTime;
		base.transform.localEulerAngles = new Vector3(this.RotationX, this.RotationY, this.RotationZ);
	}

	// Token: 0x04002B3F RID: 11071
	public float X;

	// Token: 0x04002B40 RID: 11072
	public float Y;

	// Token: 0x04002B41 RID: 11073
	public float Z;

	// Token: 0x04002B42 RID: 11074
	private float RotationX;

	// Token: 0x04002B43 RID: 11075
	private float RotationY;

	// Token: 0x04002B44 RID: 11076
	private float RotationZ;
}
