using System;
using UnityEngine;

// Token: 0x02000442 RID: 1090
[Serializable]
public class RangeInt
{
	// Token: 0x06001CB6 RID: 7350 RVA: 0x001557FC File Offset: 0x001539FC
	public RangeInt(int value, int min, int max)
	{
		this.value = value;
		this.min = min;
		this.max = max;
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x00155819 File Offset: 0x00153A19
	public RangeInt(int min, int max) : this(min, min, max)
	{
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x00155824 File Offset: 0x00153A24
	// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x0015582C File Offset: 0x00153A2C
	public int Value
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x06001CBA RID: 7354 RVA: 0x00155835 File Offset: 0x00153A35
	public int Min
	{
		get
		{
			return this.min;
		}
	}

	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x06001CBB RID: 7355 RVA: 0x0015583D File Offset: 0x00153A3D
	public int Max
	{
		get
		{
			return this.max;
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x06001CBC RID: 7356 RVA: 0x00155845 File Offset: 0x00153A45
	public int Next
	{
		get
		{
			if (this.value != this.max)
			{
				return this.value + 1;
			}
			return this.min;
		}
	}

	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x06001CBD RID: 7357 RVA: 0x00155864 File Offset: 0x00153A64
	public int Previous
	{
		get
		{
			if (this.value != this.min)
			{
				return this.value - 1;
			}
			return this.max;
		}
	}

	// Token: 0x040035F0 RID: 13808
	[SerializeField]
	private int value;

	// Token: 0x040035F1 RID: 13809
	[SerializeField]
	private int min;

	// Token: 0x040035F2 RID: 13810
	[SerializeField]
	private int max;
}
