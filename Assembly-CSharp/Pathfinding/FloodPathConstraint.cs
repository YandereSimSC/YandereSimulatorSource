using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000596 RID: 1430
	public class FloodPathConstraint : NNConstraint
	{
		// Token: 0x060026E7 RID: 9959 RVA: 0x001AA733 File Offset: 0x001A8933
		public FloodPathConstraint(FloodPath path)
		{
			if (path == null)
			{
				Debug.LogWarning("FloodPathConstraint should not be used with a NULL path");
			}
			this.path = path;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x001AA74F File Offset: 0x001A894F
		public override bool Suitable(GraphNode node)
		{
			return base.Suitable(node) && this.path.HasPathTo(node);
		}

		// Token: 0x0400419E RID: 16798
		private readonly FloodPath path;
	}
}
