using System;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x02000578 RID: 1400
	public class NavmeshTile : INavmeshHolder, ITransformedGraph, INavmesh
	{
		// Token: 0x0600260B RID: 9739 RVA: 0x001A4301 File Offset: 0x001A2501
		public void GetTileCoordinates(int tileIndex, out int x, out int z)
		{
			x = this.x;
			z = this.z;
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x0019D4FE File Offset: 0x0019B6FE
		public int GetVertexArrayIndex(int index)
		{
			return index & 4095;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x001A4314 File Offset: 0x001A2514
		public Int3 GetVertex(int index)
		{
			int num = index & 4095;
			return this.verts[num];
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x001A4335 File Offset: 0x001A2535
		public Int3 GetVertexInGraphSpace(int index)
		{
			return this.vertsInGraphSpace[index & 4095];
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x001A4349 File Offset: 0x001A2549
		public GraphTransform transform
		{
			get
			{
				return this.graph.transform;
			}
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x001A4358 File Offset: 0x001A2558
		public void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x04004107 RID: 16647
		public int[] tris;

		// Token: 0x04004108 RID: 16648
		public Int3[] verts;

		// Token: 0x04004109 RID: 16649
		public Int3[] vertsInGraphSpace;

		// Token: 0x0400410A RID: 16650
		public int x;

		// Token: 0x0400410B RID: 16651
		public int z;

		// Token: 0x0400410C RID: 16652
		public int w;

		// Token: 0x0400410D RID: 16653
		public int d;

		// Token: 0x0400410E RID: 16654
		public TriangleMeshNode[] nodes;

		// Token: 0x0400410F RID: 16655
		public BBTree bbTree;

		// Token: 0x04004110 RID: 16656
		public bool flag;

		// Token: 0x04004111 RID: 16657
		public NavmeshBase graph;
	}
}
