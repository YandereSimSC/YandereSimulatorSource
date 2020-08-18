using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200050F RID: 1295
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_patrol.php")]
	public class Patrol : VersionedMonoBehaviour
	{
		// Token: 0x06002071 RID: 8305 RVA: 0x00186480 File Offset: 0x00184680
		protected override void Awake()
		{
			base.Awake();
			this.agent = base.GetComponent<IAstarAI>();
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00186494 File Offset: 0x00184694
		private void Update()
		{
			if (this.targets.Length == 0)
			{
				return;
			}
			bool flag = false;
			if (this.agent.reachedEndOfPath && !this.agent.pathPending && float.IsPositiveInfinity(this.switchTime))
			{
				this.switchTime = Time.time + this.delay;
			}
			if (Time.time >= this.switchTime)
			{
				this.index++;
				flag = true;
				this.switchTime = float.PositiveInfinity;
			}
			this.index %= this.targets.Length;
			this.agent.destination = this.targets[this.index].position;
			if (flag)
			{
				this.agent.SearchPath();
			}
		}

		// Token: 0x04003E13 RID: 15891
		public Transform[] targets;

		// Token: 0x04003E14 RID: 15892
		public float delay;

		// Token: 0x04003E15 RID: 15893
		private int index;

		// Token: 0x04003E16 RID: 15894
		private IAstarAI agent;

		// Token: 0x04003E17 RID: 15895
		private float switchTime = float.PositiveInfinity;
	}
}
