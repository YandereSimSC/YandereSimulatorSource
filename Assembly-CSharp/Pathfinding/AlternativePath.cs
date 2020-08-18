using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200057F RID: 1407
	[AddComponentMenu("Pathfinding/Modifiers/Alternative Path")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_alternative_path.php")]
	[Serializable]
	public class AlternativePath : MonoModifier
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x001A5BF7 File Offset: 0x001A3DF7
		public override int Order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x001A5BFB File Offset: 0x001A3DFB
		public override void Apply(Path p)
		{
			if (this == null)
			{
				return;
			}
			this.ApplyNow(p.path);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x001A5C13 File Offset: 0x001A3E13
		protected void OnDestroy()
		{
			this.destroyed = true;
			this.ClearOnDestroy();
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x001A5C22 File Offset: 0x001A3E22
		private void ClearOnDestroy()
		{
			this.InversePrevious();
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x001A5C2C File Offset: 0x001A3E2C
		private void InversePrevious()
		{
			if (this.prevNodes != null)
			{
				bool flag = false;
				for (int i = 0; i < this.prevNodes.Count; i++)
				{
					if ((ulong)this.prevNodes[i].Penalty < (ulong)((long)this.prevPenalty))
					{
						flag = true;
						this.prevNodes[i].Penalty = 0u;
					}
					else
					{
						this.prevNodes[i].Penalty = (uint)((ulong)this.prevNodes[i].Penalty - (ulong)((long)this.prevPenalty));
					}
				}
				if (flag)
				{
					Debug.LogWarning("Penalty for some nodes has been reset while the AlternativePath modifier was active (possibly because of a graph update). Some penalties might be incorrect (they may be lower than expected for the affected nodes)");
				}
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x001A5CC8 File Offset: 0x001A3EC8
		private void ApplyNow(List<GraphNode> nodes)
		{
			this.InversePrevious();
			this.prevNodes.Clear();
			if (this.destroyed)
			{
				return;
			}
			if (nodes != null)
			{
				for (int i = this.rnd.Next(this.randomStep); i < nodes.Count; i += this.rnd.Next(1, this.randomStep))
				{
					nodes[i].Penalty = (uint)((ulong)nodes[i].Penalty + (ulong)((long)this.penalty));
					this.prevNodes.Add(nodes[i]);
				}
			}
			this.prevPenalty = this.penalty;
		}

		// Token: 0x04004128 RID: 16680
		public int penalty = 1000;

		// Token: 0x04004129 RID: 16681
		public int randomStep = 10;

		// Token: 0x0400412A RID: 16682
		private List<GraphNode> prevNodes = new List<GraphNode>();

		// Token: 0x0400412B RID: 16683
		private int prevPenalty;

		// Token: 0x0400412C RID: 16684
		private readonly System.Random rnd = new System.Random();

		// Token: 0x0400412D RID: 16685
		private bool destroyed;
	}
}
