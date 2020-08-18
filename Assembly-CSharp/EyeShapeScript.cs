using System;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class EyeShapeScript : MonoBehaviour
{
	// Token: 0x060013F0 RID: 5104 RVA: 0x000AD840 File Offset: 0x000ABA40
	private void Start()
	{
		this.PosOffsetX = UnityEngine.Random.Range(-0.002f, 0.002f);
		this.PosOffsetY = UnityEngine.Random.Range(-0.002f, 0.002f);
		this.PosOffsetZ = UnityEngine.Random.Range(-0.002f, 0.002f);
		this.RotOffsetX = UnityEngine.Random.Range(-15f, 15f);
		this.RotOffsetY = UnityEngine.Random.Range(-15f, 15f);
		this.RotOffsetZ = UnityEngine.Random.Range(-15f, 15f);
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x000AD8CC File Offset: 0x000ABACC
	private void LateUpdate()
	{
		this.eyelid_und1_Left.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ);
		this.eyelid_und1_Right.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ * -1f);
		this.eyelid_und2_Left.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ);
		this.eyelid_und2_Right.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ * -1f);
		this.eyelid_und_Left.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ);
		this.eyelid_und_Right.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ * -1f);
		this.eyerid1_Left.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ);
		this.eyerid1_Right.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ * -1f);
		this.eyerid2_Left.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ);
		this.eyerid2_Right.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ * -1f);
		this.eyerid_Left.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ);
		this.eyerid_Right.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ * -1f);
		this.inner_corner_of_eye_Left.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ);
		this.inner_corner_of_eye_Reft.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ * -1f);
		this.tail_of_eye_Left.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ);
		this.tail_of_eye_Right.localPosition += new Vector3(this.PosOffsetX, this.PosOffsetY, this.PosOffsetZ * -1f);
		this.eyelid_und1_Left.localEulerAngles += new Vector3(this.RotOffsetX, this.RotOffsetY, this.RotOffsetZ);
		this.eyelid_und1_Right.localEulerAngles += new Vector3(this.RotOffsetX * -1f, this.RotOffsetY * -1f, this.RotOffsetZ);
		this.eyelid_und2_Left.localEulerAngles += new Vector3(this.RotOffsetX, this.RotOffsetY, this.RotOffsetZ);
		this.eyelid_und2_Right.localEulerAngles += new Vector3(this.RotOffsetX * -1f, this.RotOffsetY * -1f, this.RotOffsetZ);
		this.eyelid_und_Left.localEulerAngles += new Vector3(this.RotOffsetX, this.RotOffsetY, this.RotOffsetZ);
		this.eyelid_und_Right.localEulerAngles += new Vector3(this.RotOffsetX * -1f, this.RotOffsetY * -1f, this.RotOffsetZ);
		this.eyerid1_Left.localEulerAngles += new Vector3(this.RotOffsetX, this.RotOffsetY, this.RotOffsetZ);
		this.eyerid1_Right.localEulerAngles += new Vector3(this.RotOffsetX * -1f, this.RotOffsetY * -1f, this.RotOffsetZ);
		this.eyerid2_Left.localEulerAngles += new Vector3(this.RotOffsetX, this.RotOffsetY, this.RotOffsetZ);
		this.eyerid2_Right.localEulerAngles += new Vector3(this.RotOffsetX * -1f, this.RotOffsetY * -1f, this.RotOffsetZ);
		this.eyerid_Left.localEulerAngles += new Vector3(this.RotOffsetX, this.RotOffsetY, this.RotOffsetZ);
		this.eyerid_Right.localEulerAngles += new Vector3(this.RotOffsetX * -1f, this.RotOffsetY * -1f, this.RotOffsetZ);
		this.inner_corner_of_eye_Left.localEulerAngles += new Vector3(this.RotOffsetX, this.RotOffsetY, this.RotOffsetZ);
		this.inner_corner_of_eye_Reft.localEulerAngles += new Vector3(this.RotOffsetX * -1f, this.RotOffsetY * -1f, this.RotOffsetZ);
		this.tail_of_eye_Left.localEulerAngles += new Vector3(this.RotOffsetX, this.RotOffsetY, this.RotOffsetZ);
		this.tail_of_eye_Right.localEulerAngles += new Vector3(this.RotOffsetX * -1f, this.RotOffsetY * -1f, this.RotOffsetZ);
	}

	// Token: 0x04001BC9 RID: 7113
	public Transform eyelid_und1_Left;

	// Token: 0x04001BCA RID: 7114
	public Transform eyelid_und1_Right;

	// Token: 0x04001BCB RID: 7115
	public Transform eyelid_und2_Left;

	// Token: 0x04001BCC RID: 7116
	public Transform eyelid_und2_Right;

	// Token: 0x04001BCD RID: 7117
	public Transform eyelid_und_Left;

	// Token: 0x04001BCE RID: 7118
	public Transform eyelid_und_Right;

	// Token: 0x04001BCF RID: 7119
	public Transform eyerid1_Left;

	// Token: 0x04001BD0 RID: 7120
	public Transform eyerid1_Right;

	// Token: 0x04001BD1 RID: 7121
	public Transform eyerid2_Left;

	// Token: 0x04001BD2 RID: 7122
	public Transform eyerid2_Right;

	// Token: 0x04001BD3 RID: 7123
	public Transform eyerid_Left;

	// Token: 0x04001BD4 RID: 7124
	public Transform eyerid_Right;

	// Token: 0x04001BD5 RID: 7125
	public Transform inner_corner_of_eye_Left;

	// Token: 0x04001BD6 RID: 7126
	public Transform inner_corner_of_eye_Reft;

	// Token: 0x04001BD7 RID: 7127
	public Transform tail_of_eye_Left;

	// Token: 0x04001BD8 RID: 7128
	public Transform tail_of_eye_Right;

	// Token: 0x04001BD9 RID: 7129
	public float PosOffsetX;

	// Token: 0x04001BDA RID: 7130
	public float PosOffsetY;

	// Token: 0x04001BDB RID: 7131
	public float PosOffsetZ;

	// Token: 0x04001BDC RID: 7132
	public float RotOffsetX;

	// Token: 0x04001BDD RID: 7133
	public float RotOffsetY;

	// Token: 0x04001BDE RID: 7134
	public float RotOffsetZ;
}
