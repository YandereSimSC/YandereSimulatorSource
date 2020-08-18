using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020005CF RID: 1487
	public static class ListExtensions
	{
		// Token: 0x060028FC RID: 10492 RVA: 0x001BA9D4 File Offset: 0x001B8BD4
		public static T[] ToArrayFromPool<T>(this List<T> list)
		{
			T[] array = ArrayPool<T>.ClaimWithExactLength(list.Count);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = list[i];
			}
			return array;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x001BAA0A File Offset: 0x001B8C0A
		public static void ClearFast<T>(this List<T> list)
		{
			if (list.Count * 2 < list.Capacity)
			{
				list.RemoveRange(0, list.Count);
				return;
			}
			list.Clear();
		}
	}
}
