using System;
using UnityEngine;

// Token: 0x0200028A RID: 650
[Serializable]
public class RivalData
{
	// Token: 0x060013C3 RID: 5059 RVA: 0x000AC518 File Offset: 0x000AA718
	public RivalData(int week)
	{
		this.week = week;
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x060013C4 RID: 5060 RVA: 0x000AC527 File Offset: 0x000AA727
	public int Week
	{
		get
		{
			return this.week;
		}
	}

	// Token: 0x04001B82 RID: 7042
	[SerializeField]
	private int week;
}
