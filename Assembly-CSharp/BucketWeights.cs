using System;
using UnityEngine;

// Token: 0x020000EE RID: 238
[Serializable]
public class BucketWeights : BucketContents
{
	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06000A86 RID: 2694 RVA: 0x000561B2 File Offset: 0x000543B2
	// (set) Token: 0x06000A87 RID: 2695 RVA: 0x000561BA File Offset: 0x000543BA
	public int Count
	{
		get
		{
			return this.count;
		}
		set
		{
			this.count = ((value < 0) ? 0 : value);
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00033EEA File Offset: 0x000320EA
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Weights;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002D171 File Offset: 0x0002B371
	public override bool IsCleaningAgent
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0002D171 File Offset: 0x0002B371
	public override bool IsFlammable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x000561CA File Offset: 0x000543CA
	public override bool CanBeLifted(int strength)
	{
		return strength > 0;
	}

	// Token: 0x04000B07 RID: 2823
	[SerializeField]
	private int count;
}
