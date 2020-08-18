using System;

namespace Pathfinding
{
	// Token: 0x0200059C RID: 1436
	public class ABPathEndingCondition : PathEndingCondition
	{
		// Token: 0x0600271B RID: 10011 RVA: 0x001ABEEB File Offset: 0x001AA0EB
		public ABPathEndingCondition(ABPath p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.abPath = p;
			this.path = p;
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x001ABF0F File Offset: 0x001AA10F
		public override bool TargetFound(PathNode node)
		{
			return node.node == this.abPath.endNode;
		}

		// Token: 0x040041B8 RID: 16824
		protected ABPath abPath;
	}
}
