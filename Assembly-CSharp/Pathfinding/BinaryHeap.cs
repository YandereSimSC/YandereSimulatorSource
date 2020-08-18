using System;

namespace Pathfinding
{
	// Token: 0x02000523 RID: 1315
	public class BinaryHeap
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x0018FCA9 File Offset: 0x0018DEA9
		public bool isEmpty
		{
			get
			{
				return this.numberOfItems <= 0;
			}
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x0018FCB7 File Offset: 0x0018DEB7
		private static int RoundUpToNextMultipleMod1(int v)
		{
			return v + (4 - (v - 1) % 4) % 4;
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x0018FCC4 File Offset: 0x0018DEC4
		public BinaryHeap(int capacity)
		{
			capacity = BinaryHeap.RoundUpToNextMultipleMod1(capacity);
			this.heap = new BinaryHeap.Tuple[capacity];
			this.numberOfItems = 0;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0018FCF4 File Offset: 0x0018DEF4
		public void Clear()
		{
			for (int i = 0; i < this.numberOfItems; i++)
			{
				this.heap[i].node.heapIndex = ushort.MaxValue;
			}
			this.numberOfItems = 0;
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0018FD34 File Offset: 0x0018DF34
		internal PathNode GetNode(int i)
		{
			return this.heap[i].node;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0018FD47 File Offset: 0x0018DF47
		internal void SetF(int i, uint f)
		{
			this.heap[i].F = f;
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0018FD5C File Offset: 0x0018DF5C
		private void Expand()
		{
			int num = BinaryHeap.RoundUpToNextMultipleMod1(Math.Max(this.heap.Length + 4, Math.Min(65533, (int)Math.Round((double)((float)this.heap.Length * this.growthFactor)))));
			if (num > 65534)
			{
				throw new Exception("Binary Heap Size really large (>65534). A heap size this large is probably the cause of pathfinding running in an infinite loop. ");
			}
			BinaryHeap.Tuple[] array = new BinaryHeap.Tuple[num];
			this.heap.CopyTo(array, 0);
			this.heap = array;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0018FDCC File Offset: 0x0018DFCC
		public void Add(PathNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.heapIndex != 65535)
			{
				this.DecreaseKey(this.heap[(int)node.heapIndex], node.heapIndex);
				return;
			}
			if (this.numberOfItems == this.heap.Length)
			{
				this.Expand();
			}
			this.DecreaseKey(new BinaryHeap.Tuple(0u, node), (ushort)this.numberOfItems);
			this.numberOfItems++;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0018FE4C File Offset: 0x0018E04C
		private void DecreaseKey(BinaryHeap.Tuple node, ushort index)
		{
			int num = (int)index;
			uint num2 = node.F = node.node.F;
			uint g = node.node.G;
			while (num != 0)
			{
				int num3 = (num - 1) / 4;
				if (num2 >= this.heap[num3].F && (num2 != this.heap[num3].F || g <= this.heap[num3].node.G))
				{
					break;
				}
				this.heap[num] = this.heap[num3];
				this.heap[num].node.heapIndex = (ushort)num;
				num = num3;
			}
			this.heap[num] = node;
			node.node.heapIndex = (ushort)num;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0018FF20 File Offset: 0x0018E120
		public PathNode Remove()
		{
			PathNode node = this.heap[0].node;
			node.heapIndex = ushort.MaxValue;
			this.numberOfItems--;
			if (this.numberOfItems == 0)
			{
				return node;
			}
			BinaryHeap.Tuple tuple = this.heap[this.numberOfItems];
			uint g = tuple.node.G;
			int num = 0;
			for (;;)
			{
				int num2 = num;
				uint num3 = tuple.F;
				int num4 = num2 * 4 + 1;
				if (num4 <= this.numberOfItems)
				{
					uint f = this.heap[num4].F;
					uint f2 = this.heap[num4 + 1].F;
					uint f3 = this.heap[num4 + 2].F;
					uint f4 = this.heap[num4 + 3].F;
					if (num4 < this.numberOfItems && (f < num3 || (f == num3 && this.heap[num4].node.G < g)))
					{
						num3 = f;
						num = num4;
					}
					if (num4 + 1 < this.numberOfItems && (f2 < num3 || (f2 == num3 && this.heap[num4 + 1].node.G < ((num == num2) ? g : this.heap[num].node.G))))
					{
						num3 = f2;
						num = num4 + 1;
					}
					if (num4 + 2 < this.numberOfItems && (f3 < num3 || (f3 == num3 && this.heap[num4 + 2].node.G < ((num == num2) ? g : this.heap[num].node.G))))
					{
						num3 = f3;
						num = num4 + 2;
					}
					if (num4 + 3 < this.numberOfItems && (f4 < num3 || (f4 == num3 && this.heap[num4 + 3].node.G < ((num == num2) ? g : this.heap[num].node.G))))
					{
						num = num4 + 3;
					}
				}
				if (num2 == num)
				{
					break;
				}
				this.heap[num2] = this.heap[num];
				this.heap[num2].node.heapIndex = (ushort)num2;
			}
			this.heap[num] = tuple;
			tuple.node.heapIndex = (ushort)num;
			return node;
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x00190194 File Offset: 0x0018E394
		private void Validate()
		{
			for (int i = 1; i < this.numberOfItems; i++)
			{
				int num = (i - 1) / 4;
				if (this.heap[num].F > this.heap[i].F)
				{
					throw new Exception(string.Concat(new object[]
					{
						"Invalid state at ",
						i,
						":",
						num,
						" ( ",
						this.heap[num].F,
						" > ",
						this.heap[i].F,
						" ) "
					}));
				}
				if ((int)this.heap[i].node.heapIndex != i)
				{
					throw new Exception("Invalid heap index");
				}
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00190284 File Offset: 0x0018E484
		public void Rebuild()
		{
			for (int i = 2; i < this.numberOfItems; i++)
			{
				int num = i;
				BinaryHeap.Tuple tuple = this.heap[i];
				uint f = tuple.F;
				while (num != 1)
				{
					int num2 = num / 4;
					if (f >= this.heap[num2].F)
					{
						break;
					}
					this.heap[num] = this.heap[num2];
					this.heap[num].node.heapIndex = (ushort)num;
					this.heap[num2] = tuple;
					this.heap[num2].node.heapIndex = (ushort)num2;
					num = num2;
				}
			}
		}

		// Token: 0x04003EF7 RID: 16119
		public int numberOfItems;

		// Token: 0x04003EF8 RID: 16120
		public float growthFactor = 2f;

		// Token: 0x04003EF9 RID: 16121
		private const int D = 4;

		// Token: 0x04003EFA RID: 16122
		private const bool SortGScores = true;

		// Token: 0x04003EFB RID: 16123
		public const ushort NotInHeap = 65535;

		// Token: 0x04003EFC RID: 16124
		private BinaryHeap.Tuple[] heap;

		// Token: 0x02000717 RID: 1815
		private struct Tuple
		{
			// Token: 0x06002C91 RID: 11409 RVA: 0x001CA04B File Offset: 0x001C824B
			public Tuple(uint f, PathNode node)
			{
				this.F = f;
				this.node = node;
			}

			// Token: 0x040048EC RID: 18668
			public PathNode node;

			// Token: 0x040048ED RID: 18669
			public uint F;
		}
	}
}
