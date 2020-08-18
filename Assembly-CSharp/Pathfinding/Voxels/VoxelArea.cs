using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020005A9 RID: 1449
	public class VoxelArea
	{
		// Token: 0x0600276E RID: 10094 RVA: 0x001AEDA4 File Offset: 0x001ACFA4
		public void Reset()
		{
			this.ResetLinkedVoxelSpans();
			for (int i = 0; i < this.compactCells.Length; i++)
			{
				this.compactCells[i].count = 0u;
				this.compactCells[i].index = 0u;
			}
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x001AEDF0 File Offset: 0x001ACFF0
		private void ResetLinkedVoxelSpans()
		{
			int num = this.linkedSpans.Length;
			this.linkedSpanCount = this.width * this.depth;
			LinkedVoxelSpan linkedVoxelSpan = new LinkedVoxelSpan(uint.MaxValue, uint.MaxValue, -1, -1);
			for (int i = 0; i < num; i++)
			{
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
			}
			this.removedStackCount = 0;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x001AEF4C File Offset: 0x001AD14C
		public VoxelArea(int width, int depth)
		{
			this.width = width;
			this.depth = depth;
			int num = width * depth;
			this.compactCells = new CompactVoxelCell[num];
			this.linkedSpans = new LinkedVoxelSpan[(int)((float)num * 8f) + 15 & -16];
			this.ResetLinkedVoxelSpans();
			int[] array = new int[4];
			array[0] = -1;
			array[2] = 1;
			this.DirectionX = array;
			this.DirectionZ = new int[]
			{
				0,
				width,
				0,
				-width
			};
			this.VectorDirection = new Vector3[]
			{
				Vector3.left,
				Vector3.forward,
				Vector3.right,
				Vector3.back
			};
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x001AF014 File Offset: 0x001AD214
		public int GetSpanCountAll()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295u)
				{
					num++;
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x001AF070 File Offset: 0x001AD270
		public int GetSpanCount()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295u)
				{
					if (this.linkedSpans[num3].area != 0)
					{
						num++;
					}
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x001AF0E0 File Offset: 0x001AD2E0
		private void PushToSpanRemovedStack(int index)
		{
			if (this.removedStackCount == this.removedStack.Length)
			{
				int[] dst = new int[this.removedStackCount * 4];
				Buffer.BlockCopy(this.removedStack, 0, dst, 0, this.removedStackCount * 4);
				this.removedStack = dst;
			}
			this.removedStack[this.removedStackCount] = index;
			this.removedStackCount++;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x001AF144 File Offset: 0x001AD344
		public void AddLinkedSpan(int index, uint bottom, uint top, int area, int voxelWalkableClimb)
		{
			if (this.linkedSpans[index].bottom == 4294967295u)
			{
				this.linkedSpans[index] = new LinkedVoxelSpan(bottom, top, area);
				return;
			}
			int num = -1;
			int num2 = index;
			while (index != -1 && this.linkedSpans[index].bottom <= top)
			{
				if (this.linkedSpans[index].top < bottom)
				{
					num = index;
					index = this.linkedSpans[index].next;
				}
				else
				{
					bottom = Math.Min(this.linkedSpans[index].bottom, bottom);
					top = Math.Max(this.linkedSpans[index].top, top);
					if (Math.Abs((int)(top - this.linkedSpans[index].top)) <= voxelWalkableClimb)
					{
						area = Math.Max(area, this.linkedSpans[index].area);
					}
					int next = this.linkedSpans[index].next;
					if (num != -1)
					{
						this.linkedSpans[num].next = next;
						this.PushToSpanRemovedStack(index);
						index = next;
					}
					else
					{
						if (next == -1)
						{
							this.linkedSpans[num2] = new LinkedVoxelSpan(bottom, top, area);
							return;
						}
						this.linkedSpans[num2] = this.linkedSpans[next];
						this.PushToSpanRemovedStack(next);
					}
				}
			}
			if (this.linkedSpanCount >= this.linkedSpans.Length)
			{
				LinkedVoxelSpan[] array = this.linkedSpans;
				int num3 = this.linkedSpanCount;
				int num4 = this.removedStackCount;
				this.linkedSpans = new LinkedVoxelSpan[this.linkedSpans.Length * 2];
				this.ResetLinkedVoxelSpans();
				this.linkedSpanCount = num3;
				this.removedStackCount = num4;
				for (int i = 0; i < this.linkedSpanCount; i++)
				{
					this.linkedSpans[i] = array[i];
				}
				Debug.Log("Layer estimate too low, doubling size of buffer.\nThis message is harmless.");
			}
			int num5;
			if (this.removedStackCount > 0)
			{
				this.removedStackCount--;
				num5 = this.removedStack[this.removedStackCount];
			}
			else
			{
				num5 = this.linkedSpanCount;
				this.linkedSpanCount++;
			}
			if (num != -1)
			{
				this.linkedSpans[num5] = new LinkedVoxelSpan(bottom, top, area, this.linkedSpans[num].next);
				this.linkedSpans[num].next = num5;
				return;
			}
			this.linkedSpans[num5] = this.linkedSpans[num2];
			this.linkedSpans[num2] = new LinkedVoxelSpan(bottom, top, area, num5);
		}

		// Token: 0x040041DC RID: 16860
		public const uint MaxHeight = 65536u;

		// Token: 0x040041DD RID: 16861
		public const int MaxHeightInt = 65536;

		// Token: 0x040041DE RID: 16862
		public const uint InvalidSpanValue = 4294967295u;

		// Token: 0x040041DF RID: 16863
		public const float AvgSpanLayerCountEstimate = 8f;

		// Token: 0x040041E0 RID: 16864
		public readonly int width;

		// Token: 0x040041E1 RID: 16865
		public readonly int depth;

		// Token: 0x040041E2 RID: 16866
		public CompactVoxelSpan[] compactSpans;

		// Token: 0x040041E3 RID: 16867
		public CompactVoxelCell[] compactCells;

		// Token: 0x040041E4 RID: 16868
		public int compactSpanCount;

		// Token: 0x040041E5 RID: 16869
		public ushort[] tmpUShortArr;

		// Token: 0x040041E6 RID: 16870
		public int[] areaTypes;

		// Token: 0x040041E7 RID: 16871
		public ushort[] dist;

		// Token: 0x040041E8 RID: 16872
		public ushort maxDistance;

		// Token: 0x040041E9 RID: 16873
		public int maxRegions;

		// Token: 0x040041EA RID: 16874
		public int[] DirectionX;

		// Token: 0x040041EB RID: 16875
		public int[] DirectionZ;

		// Token: 0x040041EC RID: 16876
		public Vector3[] VectorDirection;

		// Token: 0x040041ED RID: 16877
		private int linkedSpanCount;

		// Token: 0x040041EE RID: 16878
		public LinkedVoxelSpan[] linkedSpans;

		// Token: 0x040041EF RID: 16879
		private int[] removedStack = new int[128];

		// Token: 0x040041F0 RID: 16880
		private int removedStackCount;
	}
}
