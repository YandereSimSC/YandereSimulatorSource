using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class BoneSetsScript : MonoBehaviour
{
	// Token: 0x06000A6B RID: 2667 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Start()
	{
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00055B84 File Offset: 0x00053D84
	private void Update()
	{
		if (this.Head != null)
		{
			this.RightArm.localPosition = this.RightArmPosition;
			this.RightArm.localEulerAngles = this.RightArmRotation;
			this.LeftArm.localPosition = this.LeftArmPosition;
			this.LeftArm.localEulerAngles = this.LeftArmRotation;
			this.RightLeg.localPosition = this.RightLegPosition;
			this.RightLeg.localEulerAngles = this.RightLegRotation;
			this.LeftLeg.localPosition = this.LeftLegPosition;
			this.LeftLeg.localEulerAngles = this.LeftLegRotation;
			this.Head.localPosition = this.HeadPosition;
		}
		base.enabled = false;
	}

	// Token: 0x04000AC6 RID: 2758
	public Transform[] BoneSet1;

	// Token: 0x04000AC7 RID: 2759
	public Transform[] BoneSet2;

	// Token: 0x04000AC8 RID: 2760
	public Transform[] BoneSet3;

	// Token: 0x04000AC9 RID: 2761
	public Transform[] BoneSet4;

	// Token: 0x04000ACA RID: 2762
	public Transform[] BoneSet5;

	// Token: 0x04000ACB RID: 2763
	public Transform[] BoneSet6;

	// Token: 0x04000ACC RID: 2764
	public Transform[] BoneSet7;

	// Token: 0x04000ACD RID: 2765
	public Transform[] BoneSet8;

	// Token: 0x04000ACE RID: 2766
	public Transform[] BoneSet9;

	// Token: 0x04000ACF RID: 2767
	public Vector3[] BoneSet1Pos;

	// Token: 0x04000AD0 RID: 2768
	public Vector3[] BoneSet2Pos;

	// Token: 0x04000AD1 RID: 2769
	public Vector3[] BoneSet3Pos;

	// Token: 0x04000AD2 RID: 2770
	public Vector3[] BoneSet4Pos;

	// Token: 0x04000AD3 RID: 2771
	public Vector3[] BoneSet5Pos;

	// Token: 0x04000AD4 RID: 2772
	public Vector3[] BoneSet6Pos;

	// Token: 0x04000AD5 RID: 2773
	public Vector3[] BoneSet7Pos;

	// Token: 0x04000AD6 RID: 2774
	public Vector3[] BoneSet8Pos;

	// Token: 0x04000AD7 RID: 2775
	public Vector3[] BoneSet9Pos;

	// Token: 0x04000AD8 RID: 2776
	public float Timer;

	// Token: 0x04000AD9 RID: 2777
	public Transform RightArm;

	// Token: 0x04000ADA RID: 2778
	public Transform LeftArm;

	// Token: 0x04000ADB RID: 2779
	public Transform RightLeg;

	// Token: 0x04000ADC RID: 2780
	public Transform LeftLeg;

	// Token: 0x04000ADD RID: 2781
	public Transform Head;

	// Token: 0x04000ADE RID: 2782
	public Vector3 RightArmPosition;

	// Token: 0x04000ADF RID: 2783
	public Vector3 RightArmRotation;

	// Token: 0x04000AE0 RID: 2784
	public Vector3 LeftArmPosition;

	// Token: 0x04000AE1 RID: 2785
	public Vector3 LeftArmRotation;

	// Token: 0x04000AE2 RID: 2786
	public Vector3 RightLegPosition;

	// Token: 0x04000AE3 RID: 2787
	public Vector3 RightLegRotation;

	// Token: 0x04000AE4 RID: 2788
	public Vector3 LeftLegPosition;

	// Token: 0x04000AE5 RID: 2789
	public Vector3 LeftLegRotation;

	// Token: 0x04000AE6 RID: 2790
	public Vector3 HeadPosition;
}
