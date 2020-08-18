using System;

namespace Pathfinding.Util
{
	// Token: 0x020005D4 RID: 1492
	public static class ObjectPool<T> where T : class, IAstarPooledObject, new()
	{
		// Token: 0x06002912 RID: 10514 RVA: 0x001BB4A0 File Offset: 0x001B96A0
		public static T Claim()
		{
			return ObjectPoolSimple<T>.Claim();
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x001BB4A7 File Offset: 0x001B96A7
		public static void Release(ref T obj)
		{
			T t = obj;
			ObjectPoolSimple<T>.Release(ref obj);
			t.OnEnterPool();
		}
	}
}
