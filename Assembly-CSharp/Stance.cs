using System;
using UnityEngine;

// Token: 0x0200028C RID: 652
[Serializable]
public class Stance
{
	// Token: 0x060013C5 RID: 5061 RVA: 0x000AC52F File Offset: 0x000AA72F
	public Stance(StanceType initialStance)
	{
		this.current = initialStance;
		this.previous = initialStance;
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x060013C6 RID: 5062 RVA: 0x000AC545 File Offset: 0x000AA745
	// (set) Token: 0x060013C7 RID: 5063 RVA: 0x000AC54D File Offset: 0x000AA74D
	public StanceType Current
	{
		get
		{
			return this.current;
		}
		set
		{
			this.previous = this.current;
			this.current = value;
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x060013C8 RID: 5064 RVA: 0x000AC562 File Offset: 0x000AA762
	public StanceType Previous
	{
		get
		{
			return this.previous;
		}
	}

	// Token: 0x04001B87 RID: 7047
	[SerializeField]
	private StanceType current;

	// Token: 0x04001B88 RID: 7048
	[SerializeField]
	private StanceType previous;
}
