using System;

// Token: 0x020000EB RID: 235
public abstract class BucketContents
{
	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06000A73 RID: 2675
	public abstract BucketContentsType Type { get; }

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000A74 RID: 2676
	public abstract bool IsCleaningAgent { get; }

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000A75 RID: 2677
	public abstract bool IsFlammable { get; }

	// Token: 0x06000A76 RID: 2678
	public abstract bool CanBeLifted(int strength);
}
