using System;

// Token: 0x0200043A RID: 1082
[Serializable]
public class StringArrayWrapper : ArrayWrapper<string>
{
	// Token: 0x06001C8C RID: 7308 RVA: 0x0015503B File Offset: 0x0015323B
	public StringArrayWrapper(int size) : base(size)
	{
	}

	// Token: 0x06001C8D RID: 7309 RVA: 0x00155044 File Offset: 0x00153244
	public StringArrayWrapper(string[] elements) : base(elements)
	{
	}
}
