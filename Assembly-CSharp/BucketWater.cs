using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
[Serializable]
public class BucketWater : BucketContents
{
	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00056183 File Offset: 0x00054383
	// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0005618B File Offset: 0x0005438B
	public float Bloodiness
	{
		get
		{
			return this.bloodiness;
		}
		set
		{
			this.bloodiness = Mathf.Clamp01(value);
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00056199 File Offset: 0x00054399
	// (set) Token: 0x06000A7B RID: 2683 RVA: 0x000561A1 File Offset: 0x000543A1
	public bool HasBleach
	{
		get
		{
			return this.hasBleach;
		}
		set
		{
			this.hasBleach = value;
		}
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002D171 File Offset: 0x0002B371
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Water;
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00056199 File Offset: 0x00054399
	public override bool IsCleaningAgent
	{
		get
		{
			return this.hasBleach;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0002D171 File Offset: 0x0002B371
	public override bool IsFlammable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0002291C File Offset: 0x00020B1C
	public override bool CanBeLifted(int strength)
	{
		return true;
	}

	// Token: 0x04000B05 RID: 2821
	[SerializeField]
	private float bloodiness;

	// Token: 0x04000B06 RID: 2822
	[SerializeField]
	private bool hasBleach;
}
