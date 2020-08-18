using System;

// Token: 0x020000ED RID: 237
[Serializable]
public class BucketGas : BucketContents
{
	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002291C File Offset: 0x00020B1C
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Gas;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0002D171 File Offset: 0x0002B371
	public override bool IsCleaningAgent
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002291C File Offset: 0x00020B1C
	public override bool IsFlammable
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0002291C File Offset: 0x00020B1C
	public override bool CanBeLifted(int strength)
	{
		return true;
	}
}
