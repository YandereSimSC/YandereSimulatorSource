using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020005B6 RID: 1462
	public class Utility
	{
		// Token: 0x060027C1 RID: 10177 RVA: 0x001B4227 File Offset: 0x001B2427
		public static float Min(float a, float b, float c)
		{
			a = ((a < b) ? a : b);
			if (a >= c)
			{
				return c;
			}
			return a;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x001B423A File Offset: 0x001B243A
		public static float Max(float a, float b, float c)
		{
			a = ((a > b) ? a : b);
			if (a <= c)
			{
				return c;
			}
			return a;
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x001B424D File Offset: 0x001B244D
		public static int Max(int a, int b, int c, int d)
		{
			a = ((a > b) ? a : b);
			a = ((a > c) ? a : c);
			if (a <= d)
			{
				return d;
			}
			return a;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x001B426A File Offset: 0x001B246A
		public static int Min(int a, int b, int c, int d)
		{
			a = ((a < b) ? a : b);
			a = ((a < c) ? a : c);
			if (a >= d)
			{
				return d;
			}
			return a;
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x001B424D File Offset: 0x001B244D
		public static float Max(float a, float b, float c, float d)
		{
			a = ((a > b) ? a : b);
			a = ((a > c) ? a : c);
			if (a <= d)
			{
				return d;
			}
			return a;
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x001B426A File Offset: 0x001B246A
		public static float Min(float a, float b, float c, float d)
		{
			a = ((a < b) ? a : b);
			a = ((a < c) ? a : c);
			if (a >= d)
			{
				return d;
			}
			return a;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x001B4287 File Offset: 0x001B2487
		public static void CopyVector(float[] a, int i, Vector3 v)
		{
			a[i] = v.x;
			a[i + 1] = v.y;
			a[i + 2] = v.z;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x001B42A8 File Offset: 0x001B24A8
		public static Int3[] RemoveDuplicateVertices(Int3[] vertices, int[] triangles)
		{
			Dictionary<Int3, int> dictionary = ObjectPoolSimple<Dictionary<Int3, int>>.Claim();
			dictionary.Clear();
			int[] array = new int[vertices.Length];
			int num = 0;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (!dictionary.ContainsKey(vertices[i]))
				{
					dictionary.Add(vertices[i], num);
					array[i] = num;
					vertices[num] = vertices[i];
					num++;
				}
				else
				{
					array[i] = dictionary[vertices[i]];
				}
			}
			dictionary.Clear();
			ObjectPoolSimple<Dictionary<Int3, int>>.Release(ref dictionary);
			for (int j = 0; j < triangles.Length; j++)
			{
				triangles[j] = array[triangles[j]];
			}
			Int3[] array2 = new Int3[num];
			for (int k = 0; k < num; k++)
			{
				array2[k] = vertices[k];
			}
			return array2;
		}
	}
}
