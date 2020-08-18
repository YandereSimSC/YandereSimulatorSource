using System;
using UnityEngine;

// Token: 0x0200022F RID: 559
public class CharacterScript : MonoBehaviour
{
	// Token: 0x0600122D RID: 4653 RVA: 0x00080A64 File Offset: 0x0007EC64
	private void SetAnimations()
	{
		Animation component = base.GetComponent<Animation>();
		component["f02_yanderePose_00"].layer = 1;
		component["f02_yanderePose_00"].weight = 0f;
		component.Play("f02_yanderePose_00");
		component["f02_shy_00"].layer = 2;
		component["f02_shy_00"].weight = 0f;
		component.Play("f02_shy_00");
		component["f02_fist_00"].layer = 3;
		component["f02_fist_00"].weight = 0f;
		component.Play("f02_fist_00");
		component["f02_mopping_00"].layer = 4;
		component["f02_mopping_00"].weight = 0f;
		component["f02_mopping_00"].speed = 2f;
		component.Play("f02_mopping_00");
		component["f02_carry_00"].layer = 5;
		component["f02_carry_00"].weight = 0f;
		component.Play("f02_carry_00");
		component["f02_mopCarry_00"].layer = 6;
		component["f02_mopCarry_00"].weight = 0f;
		component.Play("f02_mopCarry_00");
		component["f02_bucketCarry_00"].layer = 7;
		component["f02_bucketCarry_00"].weight = 0f;
		component.Play("f02_bucketCarry_00");
		component["f02_cameraPose_00"].layer = 8;
		component["f02_cameraPose_00"].weight = 0f;
		component.Play("f02_cameraPose_00");
		component["f02_dipping_00"].speed = 2f;
		component["f02_cameraPose_00"].weight = 0f;
		component["f02_shy_00"].weight = 0f;
	}

	// Token: 0x04001565 RID: 5477
	public Transform RightBreast;

	// Token: 0x04001566 RID: 5478
	public Transform LeftBreast;

	// Token: 0x04001567 RID: 5479
	public Transform ItemParent;

	// Token: 0x04001568 RID: 5480
	public Transform PelvisRoot;

	// Token: 0x04001569 RID: 5481
	public Transform RightEye;

	// Token: 0x0400156A RID: 5482
	public Transform LeftEye;

	// Token: 0x0400156B RID: 5483
	public Transform Head;

	// Token: 0x0400156C RID: 5484
	public Transform[] Spine;

	// Token: 0x0400156D RID: 5485
	public Transform[] Arm;

	// Token: 0x0400156E RID: 5486
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x0400156F RID: 5487
	public Renderer RightYandereEye;

	// Token: 0x04001570 RID: 5488
	public Renderer LeftYandereEye;
}
