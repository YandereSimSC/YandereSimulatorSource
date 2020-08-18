using System;

// Token: 0x02000290 RID: 656
[Serializable]
public class ScheduleBlock
{
	// Token: 0x060013D2 RID: 5074 RVA: 0x000AD243 File Offset: 0x000AB443
	public ScheduleBlock(float time, string destination, string action)
	{
		this.time = time;
		this.destination = destination;
		this.action = action;
	}

	// Token: 0x04001BB1 RID: 7089
	public float time;

	// Token: 0x04001BB2 RID: 7090
	public string destination;

	// Token: 0x04001BB3 RID: 7091
	public string action;
}
