using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class MinMaxRangeAttribute : PropertyAttribute
{
	// Token: 0x060003C1 RID: 961 RVA: 0x00022ADC File Offset: 0x00020CDC
	public MinMaxRangeAttribute(float minLimit, float maxLimit)
	{
		this.minLimit = minLimit;
		this.maxLimit = maxLimit;
	}

	// Token: 0x040004CD RID: 1229
	public float minLimit;

	// Token: 0x040004CE RID: 1230
	public float maxLimit;
}
