using System;
using UnityEngine;

// Token: 0x0200028E RID: 654
[Serializable]
public class WitnessMemory
{
	// Token: 0x060013C9 RID: 5065 RVA: 0x000AC56C File Offset: 0x000AA76C
	public WitnessMemory()
	{
		this.memories = new float[Enum.GetValues(typeof(WitnessMemoryType)).Length];
		for (int i = 0; i < this.memories.Length; i++)
		{
			this.memories[i] = float.PositiveInfinity;
		}
		this.memorySpan = 1800f;
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x000AC5C9 File Offset: 0x000AA7C9
	public bool Remembers(WitnessMemoryType type)
	{
		return this.memories[(int)type] < this.memorySpan;
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x000AC5DB File Offset: 0x000AA7DB
	public void Refresh(WitnessMemoryType type)
	{
		this.memories[(int)type] = 0f;
	}

	// Token: 0x060013CC RID: 5068 RVA: 0x000AC5EC File Offset: 0x000AA7EC
	public void Tick(float dt)
	{
		for (int i = 0; i < this.memories.Length; i++)
		{
			this.memories[i] += dt;
		}
	}

	// Token: 0x04001B8F RID: 7055
	[SerializeField]
	private float[] memories;

	// Token: 0x04001B90 RID: 7056
	[SerializeField]
	private float memorySpan;

	// Token: 0x04001B91 RID: 7057
	private const float LongMemorySpan = 28800f;

	// Token: 0x04001B92 RID: 7058
	private const float MediumMemorySpan = 7200f;

	// Token: 0x04001B93 RID: 7059
	private const float ShortMemorySpan = 1800f;

	// Token: 0x04001B94 RID: 7060
	private const float VeryShortMemorySpan = 120f;
}
