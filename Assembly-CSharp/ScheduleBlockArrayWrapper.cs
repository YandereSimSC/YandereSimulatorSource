using System;

// Token: 0x02000439 RID: 1081
[Serializable]
public class ScheduleBlockArrayWrapper : ArrayWrapper<ScheduleBlock>
{
	// Token: 0x06001C8A RID: 7306 RVA: 0x00155029 File Offset: 0x00153229
	public ScheduleBlockArrayWrapper(int size) : base(size)
	{
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x00155032 File Offset: 0x00153232
	public ScheduleBlockArrayWrapper(ScheduleBlock[] elements) : base(elements)
	{
	}
}
