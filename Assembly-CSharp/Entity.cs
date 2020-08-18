using System;
using UnityEngine;

// Token: 0x02000287 RID: 647
[Serializable]
public abstract class Entity
{
	// Token: 0x060013BB RID: 5051 RVA: 0x000AC3DA File Offset: 0x000AA5DA
	public Entity(GenderType gender)
	{
		this.gender = gender;
		this.deathType = DeathType.None;
	}

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x060013BC RID: 5052 RVA: 0x000AC3F0 File Offset: 0x000AA5F0
	public GenderType Gender
	{
		get
		{
			return this.gender;
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x060013BD RID: 5053 RVA: 0x000AC3F8 File Offset: 0x000AA5F8
	// (set) Token: 0x060013BE RID: 5054 RVA: 0x000AC400 File Offset: 0x000AA600
	public DeathType DeathType
	{
		get
		{
			return this.deathType;
		}
		set
		{
			this.deathType = value;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x060013BF RID: 5055
	public abstract EntityType EntityType { get; }

	// Token: 0x04001B6B RID: 7019
	[SerializeField]
	private GenderType gender;

	// Token: 0x04001B6C RID: 7020
	[SerializeField]
	private DeathType deathType;
}
