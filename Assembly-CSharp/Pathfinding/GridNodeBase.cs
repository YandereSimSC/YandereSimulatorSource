using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200056F RID: 1391
	public abstract class GridNodeBase : GraphNode
	{
		// Token: 0x06002570 RID: 9584 RVA: 0x001948E0 File Offset: 0x00192AE0
		protected GridNodeBase(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002571 RID: 9585 RVA: 0x001A035F File Offset: 0x0019E55F
		// (set) Token: 0x06002572 RID: 9586 RVA: 0x001A036D File Offset: 0x0019E56D
		public int NodeInGridIndex
		{
			get
			{
				return this.nodeInGridIndex & 16777215;
			}
			set
			{
				this.nodeInGridIndex = ((this.nodeInGridIndex & -16777216) | value);
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002573 RID: 9587 RVA: 0x001A0383 File Offset: 0x0019E583
		public int XCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex % GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x001A039C File Offset: 0x0019E59C
		public int ZCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex / GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002575 RID: 9589 RVA: 0x001A03B5 File Offset: 0x0019E5B5
		// (set) Token: 0x06002576 RID: 9590 RVA: 0x001A03C6 File Offset: 0x0019E5C6
		public bool WalkableErosion
		{
			get
			{
				return (this.gridFlags & 256) > 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -257) | (value ? 256 : 0));
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x001A03E7 File Offset: 0x0019E5E7
		// (set) Token: 0x06002578 RID: 9592 RVA: 0x001A03F8 File Offset: 0x0019E5F8
		public bool TmpWalkable
		{
			get
			{
				return (this.gridFlags & 512) > 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -513) | (value ? 512 : 0));
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002579 RID: 9593
		public abstract bool HasConnectionsToAllEightNeighbours { get; }

		// Token: 0x0600257A RID: 9594 RVA: 0x001A041C File Offset: 0x0019E61C
		public override float SurfaceArea()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			return gridGraph.nodeSize * gridGraph.nodeSize;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x001A0444 File Offset: 0x0019E644
		public override Vector3 RandomPointOnSurface()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			Vector3 a = gridGraph.transform.InverseTransform((Vector3)this.position);
			return gridGraph.transform.Transform(a + new Vector3(UnityEngine.Random.value - 0.5f, 0f, UnityEngine.Random.value - 0.5f));
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x001A04A3 File Offset: 0x0019E6A3
		public override int GetGizmoHashCode()
		{
			return base.GetGizmoHashCode() ^ (int)(109 * this.gridFlags);
		}

		// Token: 0x0600257D RID: 9597
		public abstract GridNodeBase GetNeighbourAlongDirection(int direction);

		// Token: 0x0600257E RID: 9598 RVA: 0x001A04B8 File Offset: 0x0019E6B8
		public override bool ContainsConnection(GraphNode node)
		{
			for (int i = 0; i < 8; i++)
			{
				if (node == this.GetNeighbourAlongDirection(i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x001A04DE File Offset: 0x0019E6DE
		public override void AddConnection(GraphNode node, uint cost)
		{
			throw new NotImplementedException("GridNodes do not have support for adding manual connections with your current settings.\nPlease disable ASTAR_GRID_NO_CUSTOM_CONNECTIONS in the Optimizations tab in the A* Inspector");
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x001A04EA File Offset: 0x0019E6EA
		public override void RemoveConnection(GraphNode node)
		{
			throw new NotImplementedException("GridNodes do not have support for adding manual connections with your current settings.\nPlease disable ASTAR_GRID_NO_CUSTOM_CONNECTIONS in the Optimizations tab in the A* Inspector");
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00002ACE File Offset: 0x00000CCE
		public void ClearCustomConnections(bool alsoReverse)
		{
		}

		// Token: 0x040040B7 RID: 16567
		private const int GridFlagsWalkableErosionOffset = 8;

		// Token: 0x040040B8 RID: 16568
		private const int GridFlagsWalkableErosionMask = 256;

		// Token: 0x040040B9 RID: 16569
		private const int GridFlagsWalkableTmpOffset = 9;

		// Token: 0x040040BA RID: 16570
		private const int GridFlagsWalkableTmpMask = 512;

		// Token: 0x040040BB RID: 16571
		protected const int NodeInGridIndexLayerOffset = 24;

		// Token: 0x040040BC RID: 16572
		protected const int NodeInGridIndexMask = 16777215;

		// Token: 0x040040BD RID: 16573
		protected int nodeInGridIndex;

		// Token: 0x040040BE RID: 16574
		protected ushort gridFlags;
	}
}
