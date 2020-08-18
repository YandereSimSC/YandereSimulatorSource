using System;
using UnityEngine;

// Token: 0x02000359 RID: 857
[Serializable]
public class Phase
{
	// Token: 0x060018B1 RID: 6321 RVA: 0x000E3933 File Offset: 0x000E1B33
	public Phase(PhaseOfDay type)
	{
		this.type = type;
	}

	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x060018B2 RID: 6322 RVA: 0x000E3942 File Offset: 0x000E1B42
	public PhaseOfDay Type
	{
		get
		{
			return this.type;
		}
	}

	// Token: 0x040024AA RID: 9386
	[SerializeField]
	private PhaseOfDay type;
}
