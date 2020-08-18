using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020005D1 RID: 1489
	public static class ListPool<T>
	{
		// Token: 0x06002906 RID: 10502 RVA: 0x001BADD0 File Offset: 0x001B8FD0
		public static List<T> Claim()
		{
			List<List<T>> obj = ListPool<T>.pool;
			List<T> result;
			lock (obj)
			{
				if (ListPool<T>.pool.Count > 0)
				{
					List<T> list = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
					ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
					ListPool<T>.inPool.Remove(list);
					result = list;
				}
				else
				{
					result = new List<T>();
				}
			}
			return result;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x001BAE5C File Offset: 0x001B905C
		private static int FindCandidate(List<List<T>> pool, int capacity)
		{
			List<T> list = null;
			int result = -1;
			int num = 0;
			while (num < pool.Count && num < 8)
			{
				List<T> list2 = pool[pool.Count - 1 - num];
				if ((list == null || list2.Capacity > list.Capacity) && list2.Capacity < capacity * 16)
				{
					list = list2;
					result = pool.Count - 1 - num;
					if (list.Capacity >= capacity)
					{
						return result;
					}
				}
				num++;
			}
			return result;
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x001BAECC File Offset: 0x001B90CC
		public static List<T> Claim(int capacity)
		{
			List<List<T>> obj = ListPool<T>.pool;
			List<T> result;
			lock (obj)
			{
				List<List<T>> list = ListPool<T>.pool;
				int num = ListPool<T>.FindCandidate(ListPool<T>.pool, capacity);
				if (capacity > 5000)
				{
					int num2 = ListPool<T>.FindCandidate(ListPool<T>.largePool, capacity);
					if (num2 != -1)
					{
						list = ListPool<T>.largePool;
						num = num2;
					}
				}
				if (num == -1)
				{
					result = new List<T>(capacity);
				}
				else
				{
					List<T> list2 = list[num];
					ListPool<T>.inPool.Remove(list2);
					list[num] = list[list.Count - 1];
					list.RemoveAt(list.Count - 1);
					result = list2;
				}
			}
			return result;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x001BAF88 File Offset: 0x001B9188
		public static void Warmup(int count, int size)
		{
			List<List<T>> obj = ListPool<T>.pool;
			lock (obj)
			{
				List<T>[] array = new List<T>[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = ListPool<T>.Claim(size);
				}
				for (int j = 0; j < count; j++)
				{
					ListPool<T>.Release(array[j]);
				}
			}
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x001BAFF8 File Offset: 0x001B91F8
		public static void Release(ref List<T> list)
		{
			ListPool<T>.Release(list);
			list = null;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x001BB004 File Offset: 0x001B9204
		public static void Release(List<T> list)
		{
			list.ClearFast<T>();
			List<List<T>> obj = ListPool<T>.pool;
			lock (obj)
			{
				if (list.Capacity > 5000)
				{
					ListPool<T>.largePool.Add(list);
					if (ListPool<T>.largePool.Count > 8)
					{
						ListPool<T>.largePool.RemoveAt(0);
					}
				}
				else
				{
					ListPool<T>.pool.Add(list);
				}
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x001BB080 File Offset: 0x001B9280
		public static void Clear()
		{
			List<List<T>> obj = ListPool<T>.pool;
			lock (obj)
			{
				ListPool<T>.pool.Clear();
			}
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x001BB0C4 File Offset: 0x001B92C4
		public static int GetSize()
		{
			return ListPool<T>.pool.Count;
		}

		// Token: 0x040042FD RID: 17149
		private static readonly List<List<T>> pool = new List<List<T>>();

		// Token: 0x040042FE RID: 17150
		private static readonly List<List<T>> largePool = new List<List<T>>();

		// Token: 0x040042FF RID: 17151
		private static readonly HashSet<List<T>> inPool = new HashSet<List<T>>();

		// Token: 0x04004300 RID: 17152
		private const int MaxCapacitySearchLength = 8;

		// Token: 0x04004301 RID: 17153
		private const int LargeThreshold = 5000;

		// Token: 0x04004302 RID: 17154
		private const int MaxLargePoolSize = 8;
	}
}
