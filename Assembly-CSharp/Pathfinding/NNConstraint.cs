using System;

namespace Pathfinding
{
	// Token: 0x02000541 RID: 1345
	public class NNConstraint
	{
		// Token: 0x060023E7 RID: 9191 RVA: 0x00195D67 File Offset: 0x00193F67
		public virtual bool SuitableGraph(int graphIndex, NavGraph graph)
		{
			return (this.graphMask >> graphIndex & 1) != 0;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00195D7C File Offset: 0x00193F7C
		public virtual bool Suitable(GraphNode node)
		{
			return (!this.constrainWalkability || node.Walkable == this.walkable) && (!this.constrainArea || this.area < 0 || (ulong)node.Area == (ulong)((long)this.area)) && (!this.constrainTags || (this.tags >> (int)node.Tag & 1) != 0);
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x00195DE3 File Offset: 0x00193FE3
		public static NNConstraint Default
		{
			get
			{
				return new NNConstraint();
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x00195DEA File Offset: 0x00193FEA
		public static NNConstraint None
		{
			get
			{
				return new NNConstraint
				{
					constrainWalkability = false,
					constrainArea = false,
					constrainTags = false,
					constrainDistance = false,
					graphMask = -1
				};
			}
		}

		// Token: 0x04003FB3 RID: 16307
		public int graphMask = -1;

		// Token: 0x04003FB4 RID: 16308
		public bool constrainArea;

		// Token: 0x04003FB5 RID: 16309
		public int area = -1;

		// Token: 0x04003FB6 RID: 16310
		public bool constrainWalkability = true;

		// Token: 0x04003FB7 RID: 16311
		public bool walkable = true;

		// Token: 0x04003FB8 RID: 16312
		public bool distanceXZ;

		// Token: 0x04003FB9 RID: 16313
		public bool constrainTags = true;

		// Token: 0x04003FBA RID: 16314
		public int tags = -1;

		// Token: 0x04003FBB RID: 16315
		public bool constrainDistance = true;
	}
}
