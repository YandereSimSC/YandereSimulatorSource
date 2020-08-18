using System;

// Token: 0x02000458 RID: 1112
[Serializable]
public class IntAndIntPair : SerializablePair<int, int>
{
	// Token: 0x06001CDE RID: 7390 RVA: 0x00155D39 File Offset: 0x00153F39
	public IntAndIntPair(int first, int second) : base(first, second)
	{
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x00155D43 File Offset: 0x00153F43
	public IntAndIntPair() : base(0, 0)
	{
	}
}
