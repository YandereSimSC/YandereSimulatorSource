using System;
using UnityEngine;

// Token: 0x02000281 RID: 641
[Serializable]
public class CharacterSkeleton
{
	// Token: 0x17000358 RID: 856
	// (get) Token: 0x0600139F RID: 5023 RVA: 0x000AC18F File Offset: 0x000AA38F
	public Transform Head
	{
		get
		{
			return this.head;
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x060013A0 RID: 5024 RVA: 0x000AC197 File Offset: 0x000AA397
	public Transform Neck
	{
		get
		{
			return this.neck;
		}
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x060013A1 RID: 5025 RVA: 0x000AC19F File Offset: 0x000AA39F
	public Transform Chest
	{
		get
		{
			return this.chest;
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x060013A2 RID: 5026 RVA: 0x000AC1A7 File Offset: 0x000AA3A7
	public Transform Stomach
	{
		get
		{
			return this.stomach;
		}
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x060013A3 RID: 5027 RVA: 0x000AC1AF File Offset: 0x000AA3AF
	public Transform Pelvis
	{
		get
		{
			return this.pelvis;
		}
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x060013A4 RID: 5028 RVA: 0x000AC1B7 File Offset: 0x000AA3B7
	public Transform RightShoulder
	{
		get
		{
			return this.rightShoulder;
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x060013A5 RID: 5029 RVA: 0x000AC1BF File Offset: 0x000AA3BF
	public Transform LeftShoulder
	{
		get
		{
			return this.leftShoulder;
		}
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000AC1C7 File Offset: 0x000AA3C7
	public Transform RightUpperArm
	{
		get
		{
			return this.rightUpperArm;
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x060013A7 RID: 5031 RVA: 0x000AC1CF File Offset: 0x000AA3CF
	public Transform LeftUpperArm
	{
		get
		{
			return this.leftUpperArm;
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000AC1D7 File Offset: 0x000AA3D7
	public Transform RightElbow
	{
		get
		{
			return this.rightElbow;
		}
	}

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x060013A9 RID: 5033 RVA: 0x000AC1DF File Offset: 0x000AA3DF
	public Transform LeftElbow
	{
		get
		{
			return this.leftElbow;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x060013AA RID: 5034 RVA: 0x000AC1E7 File Offset: 0x000AA3E7
	public Transform RightLowerArm
	{
		get
		{
			return this.rightLowerArm;
		}
	}

	// Token: 0x17000364 RID: 868
	// (get) Token: 0x060013AB RID: 5035 RVA: 0x000AC1EF File Offset: 0x000AA3EF
	public Transform LeftLowerArm
	{
		get
		{
			return this.leftLowerArm;
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x060013AC RID: 5036 RVA: 0x000AC1F7 File Offset: 0x000AA3F7
	public Transform RightPalm
	{
		get
		{
			return this.rightPalm;
		}
	}

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x060013AD RID: 5037 RVA: 0x000AC1FF File Offset: 0x000AA3FF
	public Transform LeftPalm
	{
		get
		{
			return this.leftPalm;
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x060013AE RID: 5038 RVA: 0x000AC207 File Offset: 0x000AA407
	public Transform RightUpperLeg
	{
		get
		{
			return this.rightUpperLeg;
		}
	}

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x060013AF RID: 5039 RVA: 0x000AC20F File Offset: 0x000AA40F
	public Transform LeftUpperLeg
	{
		get
		{
			return this.leftUpperLeg;
		}
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000AC217 File Offset: 0x000AA417
	public Transform RightKnee
	{
		get
		{
			return this.rightKnee;
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000AC21F File Offset: 0x000AA41F
	public Transform LeftKnee
	{
		get
		{
			return this.leftKnee;
		}
	}

	// Token: 0x1700036B RID: 875
	// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000AC227 File Offset: 0x000AA427
	public Transform RightLowerLeg
	{
		get
		{
			return this.rightLowerLeg;
		}
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x060013B3 RID: 5043 RVA: 0x000AC22F File Offset: 0x000AA42F
	public Transform LeftLowerLeg
	{
		get
		{
			return this.leftLowerLeg;
		}
	}

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000AC237 File Offset: 0x000AA437
	public Transform RightFoot
	{
		get
		{
			return this.rightFoot;
		}
	}

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x060013B5 RID: 5045 RVA: 0x000AC23F File Offset: 0x000AA43F
	public Transform LeftFoot
	{
		get
		{
			return this.leftFoot;
		}
	}

	// Token: 0x04001B28 RID: 6952
	[SerializeField]
	private Transform head;

	// Token: 0x04001B29 RID: 6953
	[SerializeField]
	private Transform neck;

	// Token: 0x04001B2A RID: 6954
	[SerializeField]
	private Transform chest;

	// Token: 0x04001B2B RID: 6955
	[SerializeField]
	private Transform stomach;

	// Token: 0x04001B2C RID: 6956
	[SerializeField]
	private Transform pelvis;

	// Token: 0x04001B2D RID: 6957
	[SerializeField]
	private Transform rightShoulder;

	// Token: 0x04001B2E RID: 6958
	[SerializeField]
	private Transform leftShoulder;

	// Token: 0x04001B2F RID: 6959
	[SerializeField]
	private Transform rightUpperArm;

	// Token: 0x04001B30 RID: 6960
	[SerializeField]
	private Transform leftUpperArm;

	// Token: 0x04001B31 RID: 6961
	[SerializeField]
	private Transform rightElbow;

	// Token: 0x04001B32 RID: 6962
	[SerializeField]
	private Transform leftElbow;

	// Token: 0x04001B33 RID: 6963
	[SerializeField]
	private Transform rightLowerArm;

	// Token: 0x04001B34 RID: 6964
	[SerializeField]
	private Transform leftLowerArm;

	// Token: 0x04001B35 RID: 6965
	[SerializeField]
	private Transform rightPalm;

	// Token: 0x04001B36 RID: 6966
	[SerializeField]
	private Transform leftPalm;

	// Token: 0x04001B37 RID: 6967
	[SerializeField]
	private Transform rightUpperLeg;

	// Token: 0x04001B38 RID: 6968
	[SerializeField]
	private Transform leftUpperLeg;

	// Token: 0x04001B39 RID: 6969
	[SerializeField]
	private Transform rightKnee;

	// Token: 0x04001B3A RID: 6970
	[SerializeField]
	private Transform leftKnee;

	// Token: 0x04001B3B RID: 6971
	[SerializeField]
	private Transform rightLowerLeg;

	// Token: 0x04001B3C RID: 6972
	[SerializeField]
	private Transform leftLowerLeg;

	// Token: 0x04001B3D RID: 6973
	[SerializeField]
	private Transform rightFoot;

	// Token: 0x04001B3E RID: 6974
	[SerializeField]
	private Transform leftFoot;
}
