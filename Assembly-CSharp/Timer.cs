using System;
using UnityEngine;

// Token: 0x02000459 RID: 1113
[Serializable]
public class Timer
{
	// Token: 0x06001CE0 RID: 7392 RVA: 0x00155D4D File Offset: 0x00153F4D
	public Timer(float targetSeconds)
	{
		this.currentSeconds = 0f;
		this.targetSeconds = targetSeconds;
	}

	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00155D67 File Offset: 0x00153F67
	public float CurrentSeconds
	{
		get
		{
			return this.currentSeconds;
		}
	}

	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00155D6F File Offset: 0x00153F6F
	public float TargetSeconds
	{
		get
		{
			return this.targetSeconds;
		}
	}

	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x00155D77 File Offset: 0x00153F77
	public bool IsDone
	{
		get
		{
			return this.currentSeconds >= this.targetSeconds;
		}
	}

	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00155D8A File Offset: 0x00153F8A
	public float Progress
	{
		get
		{
			return Mathf.Clamp01(this.currentSeconds / this.targetSeconds);
		}
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x00155D9E File Offset: 0x00153F9E
	public void Reset()
	{
		this.currentSeconds = 0f;
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x00155DAB File Offset: 0x00153FAB
	public void SubtractTarget()
	{
		this.currentSeconds -= this.targetSeconds;
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x00155DC0 File Offset: 0x00153FC0
	public void Tick(float dt)
	{
		this.currentSeconds += dt;
	}

	// Token: 0x040035FC RID: 13820
	[SerializeField]
	private float currentSeconds;

	// Token: 0x040035FD RID: 13821
	[SerializeField]
	private float targetSeconds;
}
