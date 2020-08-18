using System;

namespace Pathfinding
{
	// Token: 0x02000542 RID: 1346
	public class PathNNConstraint : NNConstraint
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x00195E4D File Offset: 0x0019404D
		public new static PathNNConstraint Default
		{
			get
			{
				return new PathNNConstraint
				{
					constrainArea = true
				};
			}
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x00195E5B File Offset: 0x0019405B
		public virtual void SetStart(GraphNode node)
		{
			if (node != null)
			{
				this.area = (int)node.Area;
				return;
			}
			this.constrainArea = false;
		}
	}
}
