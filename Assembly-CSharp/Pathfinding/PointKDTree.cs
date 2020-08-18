using System;
using System.Collections.Generic;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x0200057A RID: 1402
	public class PointKDTree
	{
		// Token: 0x06002616 RID: 9750 RVA: 0x001A4BD0 File Offset: 0x001A2DD0
		public PointKDTree()
		{
			this.tree[1] = new PointKDTree.Node
			{
				data = this.GetOrCreateList()
			};
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x001A4C28 File Offset: 0x001A2E28
		public void Add(GraphNode node)
		{
			this.numNodes++;
			this.Add(node, 1, 0);
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x001A4C44 File Offset: 0x001A2E44
		public void Rebuild(GraphNode[] nodes, int start, int end)
		{
			if (start < 0 || end < start || end > nodes.Length)
			{
				throw new ArgumentException();
			}
			for (int i = 0; i < this.tree.Length; i++)
			{
				GraphNode[] data = this.tree[i].data;
				if (data != null)
				{
					for (int j = 0; j < 21; j++)
					{
						data[j] = null;
					}
					this.arrayCache.Push(data);
					this.tree[i].data = null;
				}
			}
			this.numNodes = end - start;
			this.Build(1, new List<GraphNode>(nodes), start, end);
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x001A4CD4 File Offset: 0x001A2ED4
		private GraphNode[] GetOrCreateList()
		{
			if (this.arrayCache.Count <= 0)
			{
				return new GraphNode[21];
			}
			return this.arrayCache.Pop();
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x001A4CF7 File Offset: 0x001A2EF7
		private int Size(int index)
		{
			if (this.tree[index].data == null)
			{
				return this.Size(2 * index) + this.Size(2 * index + 1);
			}
			return (int)this.tree[index].count;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x001A4D34 File Offset: 0x001A2F34
		private void CollectAndClear(int index, List<GraphNode> buffer)
		{
			GraphNode[] data = this.tree[index].data;
			ushort count = this.tree[index].count;
			if (data != null)
			{
				this.tree[index] = default(PointKDTree.Node);
				for (int i = 0; i < (int)count; i++)
				{
					buffer.Add(data[i]);
					data[i] = null;
				}
				this.arrayCache.Push(data);
				return;
			}
			this.CollectAndClear(index * 2, buffer);
			this.CollectAndClear(index * 2 + 1, buffer);
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x001A4DB6 File Offset: 0x001A2FB6
		private static int MaxAllowedSize(int numNodes, int depth)
		{
			return Math.Min(5 * numNodes / 2 >> depth, 3 * numNodes / 4);
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x001A4DCC File Offset: 0x001A2FCC
		private void Rebalance(int index)
		{
			this.CollectAndClear(index, this.largeList);
			this.Build(index, this.largeList, 0, this.largeList.Count);
			this.largeList.ClearFast<GraphNode>();
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x001A4E00 File Offset: 0x001A3000
		private void EnsureSize(int index)
		{
			if (index >= this.tree.Length)
			{
				PointKDTree.Node[] array = new PointKDTree.Node[Math.Max(index + 1, this.tree.Length * 2)];
				this.tree.CopyTo(array, 0);
				this.tree = array;
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x001A4E44 File Offset: 0x001A3044
		private void Build(int index, List<GraphNode> nodes, int start, int end)
		{
			this.EnsureSize(index);
			if (end - start <= 10)
			{
				GraphNode[] array = this.tree[index].data = this.GetOrCreateList();
				this.tree[index].count = (ushort)(end - start);
				for (int i = start; i < end; i++)
				{
					array[i - start] = nodes[i];
				}
				return;
			}
			Int3 position;
			Int3 @int = position = nodes[start].position;
			for (int j = start; j < end; j++)
			{
				Int3 position2 = nodes[j].position;
				position = new Int3(Math.Min(position.x, position2.x), Math.Min(position.y, position2.y), Math.Min(position.z, position2.z));
				@int = new Int3(Math.Max(@int.x, position2.x), Math.Max(@int.y, position2.y), Math.Max(@int.z, position2.z));
			}
			Int3 int2 = @int - position;
			int num = (int2.x > int2.y) ? ((int2.x > int2.z) ? 0 : 2) : ((int2.y > int2.z) ? 1 : 2);
			nodes.Sort(start, end - start, PointKDTree.comparers[num]);
			int num2 = (start + end) / 2;
			this.tree[index].split = (nodes[num2 - 1].position[num] + nodes[num2].position[num] + 1) / 2;
			this.tree[index].splitAxis = (byte)num;
			this.Build(index * 2, nodes, start, num2);
			this.Build(index * 2 + 1, nodes, num2, end);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x001A5030 File Offset: 0x001A3230
		private void Add(GraphNode point, int index, int depth = 0)
		{
			while (this.tree[index].data == null)
			{
				index = 2 * index + ((point.position[(int)this.tree[index].splitAxis] < this.tree[index].split) ? 0 : 1);
				depth++;
			}
			GraphNode[] data = this.tree[index].data;
			PointKDTree.Node[] array = this.tree;
			int num = index;
			ushort count = array[num].count;
			array[num].count = count + 1;
			data[(int)count] = point;
			if (this.tree[index].count >= 21)
			{
				int num2 = 0;
				while (depth - num2 > 0 && this.Size(index >> num2) > PointKDTree.MaxAllowedSize(this.numNodes, depth - num2))
				{
					num2++;
				}
				this.Rebalance(index >> num2);
			}
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x001A5108 File Offset: 0x001A3308
		public GraphNode GetNearest(Int3 point, NNConstraint constraint)
		{
			GraphNode result = null;
			long maxValue = long.MaxValue;
			this.GetNearestInternal(1, point, constraint, ref result, ref maxValue);
			return result;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x001A5130 File Offset: 0x001A3330
		private void GetNearestInternal(int index, Int3 point, NNConstraint constraint, ref GraphNode best, ref long bestSqrDist)
		{
			GraphNode[] data = this.tree[index].data;
			if (data != null)
			{
				for (int i = (int)(this.tree[index].count - 1); i >= 0; i--)
				{
					long sqrMagnitudeLong = (data[i].position - point).sqrMagnitudeLong;
					if (sqrMagnitudeLong < bestSqrDist && (constraint == null || constraint.Suitable(data[i])))
					{
						bestSqrDist = sqrMagnitudeLong;
						best = data[i];
					}
				}
				return;
			}
			long num = (long)(point[(int)this.tree[index].splitAxis] - this.tree[index].split);
			int num2 = 2 * index + ((num < 0L) ? 0 : 1);
			this.GetNearestInternal(num2, point, constraint, ref best, ref bestSqrDist);
			if (num * num < bestSqrDist)
			{
				this.GetNearestInternal(num2 ^ 1, point, constraint, ref best, ref bestSqrDist);
			}
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x001A520B File Offset: 0x001A340B
		public void GetInRange(Int3 point, long sqrRadius, List<GraphNode> buffer)
		{
			this.GetInRangeInternal(1, point, sqrRadius, buffer);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x001A5218 File Offset: 0x001A3418
		private void GetInRangeInternal(int index, Int3 point, long sqrRadius, List<GraphNode> buffer)
		{
			GraphNode[] data = this.tree[index].data;
			if (data != null)
			{
				for (int i = (int)(this.tree[index].count - 1); i >= 0; i--)
				{
					if ((data[i].position - point).sqrMagnitudeLong < sqrRadius)
					{
						buffer.Add(data[i]);
					}
				}
				return;
			}
			long num = (long)(point[(int)this.tree[index].splitAxis] - this.tree[index].split);
			int num2 = 2 * index + ((num < 0L) ? 0 : 1);
			this.GetInRangeInternal(num2, point, sqrRadius, buffer);
			if (num * num < sqrRadius)
			{
				this.GetInRangeInternal(num2 ^ 1, point, sqrRadius, buffer);
			}
		}

		// Token: 0x04004112 RID: 16658
		public const int LeafSize = 10;

		// Token: 0x04004113 RID: 16659
		public const int LeafArraySize = 21;

		// Token: 0x04004114 RID: 16660
		private PointKDTree.Node[] tree = new PointKDTree.Node[16];

		// Token: 0x04004115 RID: 16661
		private int numNodes;

		// Token: 0x04004116 RID: 16662
		private readonly List<GraphNode> largeList = new List<GraphNode>();

		// Token: 0x04004117 RID: 16663
		private readonly Stack<GraphNode[]> arrayCache = new Stack<GraphNode[]>();

		// Token: 0x04004118 RID: 16664
		private static readonly IComparer<GraphNode>[] comparers = new IComparer<GraphNode>[]
		{
			new PointKDTree.CompareX(),
			new PointKDTree.CompareY(),
			new PointKDTree.CompareZ()
		};

		// Token: 0x0200074A RID: 1866
		private struct Node
		{
			// Token: 0x040049B9 RID: 18873
			public GraphNode[] data;

			// Token: 0x040049BA RID: 18874
			public int split;

			// Token: 0x040049BB RID: 18875
			public ushort count;

			// Token: 0x040049BC RID: 18876
			public byte splitAxis;
		}

		// Token: 0x0200074B RID: 1867
		private class CompareX : IComparer<GraphNode>
		{
			// Token: 0x06002D3A RID: 11578 RVA: 0x001CDB8F File Offset: 0x001CBD8F
			public int Compare(GraphNode lhs, GraphNode rhs)
			{
				return lhs.position.x.CompareTo(rhs.position.x);
			}
		}

		// Token: 0x0200074C RID: 1868
		private class CompareY : IComparer<GraphNode>
		{
			// Token: 0x06002D3C RID: 11580 RVA: 0x001CDBAC File Offset: 0x001CBDAC
			public int Compare(GraphNode lhs, GraphNode rhs)
			{
				return lhs.position.y.CompareTo(rhs.position.y);
			}
		}

		// Token: 0x0200074D RID: 1869
		private class CompareZ : IComparer<GraphNode>
		{
			// Token: 0x06002D3E RID: 11582 RVA: 0x001CDBC9 File Offset: 0x001CBDC9
			public int Compare(GraphNode lhs, GraphNode rhs)
			{
				return lhs.position.z.CompareTo(rhs.position.z);
			}
		}
	}
}
