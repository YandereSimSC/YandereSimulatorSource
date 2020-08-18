using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200050E RID: 1294
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour
	{
		// Token: 0x0600206D RID: 8301 RVA: 0x001863DC File Offset: 0x001845DC
		private void OnEnable()
		{
			this.ai = base.GetComponent<IAstarAI>();
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Combine(astarAI.onSearchPath, new Action(this.Update));
			}
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00186419 File Offset: 0x00184619
		private void OnDisable()
		{
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Remove(astarAI.onSearchPath, new Action(this.Update));
			}
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0018644A File Offset: 0x0018464A
		private void Update()
		{
			if (this.target != null && this.ai != null)
			{
				this.ai.destination = this.target.position;
			}
		}

		// Token: 0x04003E11 RID: 15889
		public Transform target;

		// Token: 0x04003E12 RID: 15890
		public IAstarAI ai;
	}
}
