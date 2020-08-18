using System;

// Token: 0x02000457 RID: 1111
public class SerializablePair<T, U>
{
	// Token: 0x06001CDC RID: 7388 RVA: 0x00155CFC File Offset: 0x00153EFC
	public SerializablePair(T first, U second)
	{
		this.first = first;
		this.second = second;
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x00155D14 File Offset: 0x00153F14
	public SerializablePair() : this(default(T), default(U))
	{
	}

	// Token: 0x040035FA RID: 13818
	public T first;

	// Token: 0x040035FB RID: 13819
	public U second;
}
