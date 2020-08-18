using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020005D5 RID: 1493
	public static class ObjectPoolSimple<T> where T : class, new()
	{
		// Token: 0x06002914 RID: 10516 RVA: 0x001BB4C0 File Offset: 0x001B96C0
		public static T Claim()
		{
			List<T> obj = ObjectPoolSimple<T>.pool;
			T result;
			lock (obj)
			{
				if (ObjectPoolSimple<T>.pool.Count > 0)
				{
					T t = ObjectPoolSimple<T>.pool[ObjectPoolSimple<T>.pool.Count - 1];
					ObjectPoolSimple<T>.pool.RemoveAt(ObjectPoolSimple<T>.pool.Count - 1);
					ObjectPoolSimple<T>.inPool.Remove(t);
					result = t;
				}
				else
				{
					result = Activator.CreateInstance<T>();
				}
			}
			return result;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x001BB54C File Offset: 0x001B974C
		public static void Release(ref T obj)
		{
			List<T> obj2 = ObjectPoolSimple<T>.pool;
			lock (obj2)
			{
				ObjectPoolSimple<T>.pool.Add(obj);
			}
			obj = default(T);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x001BB59C File Offset: 0x001B979C
		public static void Clear()
		{
			List<T> obj = ObjectPoolSimple<T>.pool;
			lock (obj)
			{
				ObjectPoolSimple<T>.pool.Clear();
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x001BB5E0 File Offset: 0x001B97E0
		public static int GetSize()
		{
			return ObjectPoolSimple<T>.pool.Count;
		}

		// Token: 0x04004303 RID: 17155
		private static List<T> pool = new List<T>();

		// Token: 0x04004304 RID: 17156
		private static readonly HashSet<T> inPool = new HashSet<T>();
	}
}
