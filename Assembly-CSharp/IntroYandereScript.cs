using System;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class IntroYandereScript : MonoBehaviour
{
	// Token: 0x06001783 RID: 6019 RVA: 0x000CE884 File Offset: 0x000CCA84
	private void LateUpdate()
	{
		this.Hips.localEulerAngles = new Vector3(this.Hips.localEulerAngles.x + this.X, this.Hips.localEulerAngles.y, this.Hips.localEulerAngles.z);
		this.Spine.localEulerAngles = new Vector3(this.Spine.localEulerAngles.x + this.X, this.Spine.localEulerAngles.y, this.Spine.localEulerAngles.z);
		this.Spine1.localEulerAngles = new Vector3(this.Spine1.localEulerAngles.x + this.X, this.Spine1.localEulerAngles.y, this.Spine1.localEulerAngles.z);
		this.Spine2.localEulerAngles = new Vector3(this.Spine2.localEulerAngles.x + this.X, this.Spine2.localEulerAngles.y, this.Spine2.localEulerAngles.z);
		this.Spine3.localEulerAngles = new Vector3(this.Spine3.localEulerAngles.x + this.X, this.Spine3.localEulerAngles.y, this.Spine3.localEulerAngles.z);
		this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x + this.X, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
		this.Head.localEulerAngles = new Vector3(this.Head.localEulerAngles.x + this.X, this.Head.localEulerAngles.y, this.Head.localEulerAngles.z);
		this.RightUpLeg.localEulerAngles = new Vector3(this.RightUpLeg.localEulerAngles.x - this.X, this.RightUpLeg.localEulerAngles.y, this.RightUpLeg.localEulerAngles.z);
		this.RightLeg.localEulerAngles = new Vector3(this.RightLeg.localEulerAngles.x - this.X, this.RightLeg.localEulerAngles.y, this.RightLeg.localEulerAngles.z);
		this.RightFoot.localEulerAngles = new Vector3(this.RightFoot.localEulerAngles.x - this.X, this.RightFoot.localEulerAngles.y, this.RightFoot.localEulerAngles.z);
		this.LeftUpLeg.localEulerAngles = new Vector3(this.LeftUpLeg.localEulerAngles.x - this.X, this.LeftUpLeg.localEulerAngles.y, this.LeftUpLeg.localEulerAngles.z);
		this.LeftLeg.localEulerAngles = new Vector3(this.LeftLeg.localEulerAngles.x - this.X, this.LeftLeg.localEulerAngles.y, this.LeftLeg.localEulerAngles.z);
		this.LeftFoot.localEulerAngles = new Vector3(this.LeftFoot.localEulerAngles.x - this.X, this.LeftFoot.localEulerAngles.y, this.LeftFoot.localEulerAngles.z);
	}

	// Token: 0x04002135 RID: 8501
	public Transform Hips;

	// Token: 0x04002136 RID: 8502
	public Transform Spine;

	// Token: 0x04002137 RID: 8503
	public Transform Spine1;

	// Token: 0x04002138 RID: 8504
	public Transform Spine2;

	// Token: 0x04002139 RID: 8505
	public Transform Spine3;

	// Token: 0x0400213A RID: 8506
	public Transform Neck;

	// Token: 0x0400213B RID: 8507
	public Transform Head;

	// Token: 0x0400213C RID: 8508
	public Transform RightUpLeg;

	// Token: 0x0400213D RID: 8509
	public Transform RightLeg;

	// Token: 0x0400213E RID: 8510
	public Transform RightFoot;

	// Token: 0x0400213F RID: 8511
	public Transform LeftUpLeg;

	// Token: 0x04002140 RID: 8512
	public Transform LeftLeg;

	// Token: 0x04002141 RID: 8513
	public Transform LeftFoot;

	// Token: 0x04002142 RID: 8514
	public float X;
}
