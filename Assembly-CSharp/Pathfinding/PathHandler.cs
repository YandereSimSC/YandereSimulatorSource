using System;
using System.Text;

namespace Pathfinding
{
	// Token: 0x0200053E RID: 1342
	public class PathHandler
	{
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x0019599C File Offset: 0x00193B9C
		public ushort PathID
		{
			get
			{
				return this.pathID;
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x001959A4 File Offset: 0x00193BA4
		public PathHandler(int threadID, int totalThreadCount)
		{
			this.threadID = threadID;
			this.totalThreadCount = totalThreadCount;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x001959E1 File Offset: 0x00193BE1
		public void InitializeForPath(Path p)
		{
			this.pathID = p.pathID;
			this.heap.Clear();
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x001959FA File Offset: 0x00193BFA
		public void DestroyNode(GraphNode node)
		{
			PathNode pathNode = this.GetPathNode(node);
			pathNode.node = null;
			pathNode.parent = null;
			pathNode.pathID = 0;
			pathNode.G = 0u;
			pathNode.H = 0u;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00195A28 File Offset: 0x00193C28
		public void InitializeNode(GraphNode node)
		{
			int nodeIndex = node.NodeIndex;
			if (nodeIndex >= this.nodes.Length)
			{
				PathNode[] array = new PathNode[Math.Max(128, this.nodes.Length * 2)];
				this.nodes.CopyTo(array, 0);
				for (int i = this.nodes.Length; i < array.Length; i++)
				{
					array[i] = new PathNode();
				}
				this.nodes = array;
			}
			this.nodes[nodeIndex].node = node;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00195AA0 File Offset: 0x00193CA0
		public PathNode GetPathNode(int nodeIndex)
		{
			return this.nodes[nodeIndex];
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00195AAA File Offset: 0x00193CAA
		public PathNode GetPathNode(GraphNode node)
		{
			return this.nodes[node.NodeIndex];
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00195ABC File Offset: 0x00193CBC
		public void ClearPathIDs()
		{
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] != null)
				{
					this.nodes[i].pathID = 0;
				}
			}
		}

		// Token: 0x04003F9A RID: 16282
		private ushort pathID;

		// Token: 0x04003F9B RID: 16283
		public readonly int threadID;

		// Token: 0x04003F9C RID: 16284
		public readonly int totalThreadCount;

		// Token: 0x04003F9D RID: 16285
		public readonly BinaryHeap heap = new BinaryHeap(128);

		// Token: 0x04003F9E RID: 16286
		public PathNode[] nodes = new PathNode[0];

		// Token: 0x04003F9F RID: 16287
		public readonly StringBuilder DebugStringBuilder = new StringBuilder();
	}
}
