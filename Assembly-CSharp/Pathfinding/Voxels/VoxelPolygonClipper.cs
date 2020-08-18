using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005B5 RID: 1461
	internal struct VoxelPolygonClipper
	{
		// Token: 0x060027BD RID: 10173 RVA: 0x001B3F68 File Offset: 0x001B2168
		private void Init()
		{
			if (this.clipPolygonCache == null)
			{
				this.clipPolygonCache = new float[21];
				this.clipPolygonIntCache = new int[21];
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x001B3F8C File Offset: 0x001B218C
		public int ClipPolygon(float[] vIn, int n, float[] vOut, float multi, float offset, int axis)
		{
			this.Init();
			float[] array = this.clipPolygonCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i * 3 + axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					int num3 = num * 3;
					int num4 = j * 3;
					int num5 = num2 * 3;
					float num6 = array[num2] / (array[num2] - array[j]);
					vOut[num3] = vIn[num5] + (vIn[num4] - vIn[num5]) * num6;
					vOut[num3 + 1] = vIn[num5 + 1] + (vIn[num4 + 1] - vIn[num5 + 1]) * num6;
					vOut[num3 + 2] = vIn[num5 + 2] + (vIn[num4 + 2] - vIn[num5 + 2]) * num6;
					num++;
				}
				if (flag2)
				{
					int num7 = num * 3;
					int num8 = j * 3;
					vOut[num7] = vIn[num8];
					vOut[num7 + 1] = vIn[num8 + 1];
					vOut[num7 + 2] = vIn[num8 + 2];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x001B40A8 File Offset: 0x001B22A8
		public int ClipPolygonY(float[] vIn, int n, float[] vOut, float multi, float offset, int axis)
		{
			this.Init();
			float[] array = this.clipPolygonCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i * 3 + axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					vOut[num * 3 + 1] = vIn[num2 * 3 + 1] + (vIn[j * 3 + 1] - vIn[num2 * 3 + 1]) * (array[num2] / (array[num2] - array[j]));
					num++;
				}
				if (flag2)
				{
					vOut[num * 3 + 1] = vIn[j * 3 + 1];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x001B4160 File Offset: 0x001B2360
		public int ClipPolygon(Int3[] vIn, int n, Int3[] vOut, int multi, int offset, int axis)
		{
			this.Init();
			int[] array = this.clipPolygonIntCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i][axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0;
				bool flag2 = array[j] >= 0;
				if (flag != flag2)
				{
					double rhs = (double)array[num2] / (double)(array[num2] - array[j]);
					vOut[num] = vIn[num2] + (vIn[j] - vIn[num2]) * rhs;
					num++;
				}
				if (flag2)
				{
					vOut[num] = vIn[j];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x04004233 RID: 16947
		private float[] clipPolygonCache;

		// Token: 0x04004234 RID: 16948
		private int[] clipPolygonIntCache;
	}
}
