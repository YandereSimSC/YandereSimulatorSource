using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200052F RID: 1327
	[Obsolete("Generic version is now obsolete to trade an extremely tiny performance decrease for a large decrease in boilerplate for Path classes")]
	public static class PathPool<T> where T : Path, new()
	{
		// Token: 0x0600230A RID: 8970 RVA: 0x00193416 File Offset: 0x00191616
		public static void Recycle(T path)
		{
			PathPool.Pool(path);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00193424 File Offset: 0x00191624
		public static void Warmup(int count, int length)
		{
			ListPool<GraphNode>.Warmup(count, length);
			ListPool<Vector3>.Warmup(count, length);
			Path[] array = new Path[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = PathPool<T>.GetPath();
				array[i].Claim(array);
			}
			for (int j = 0; j < count; j++)
			{
				array[j].Release(array, false);
			}
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0019347E File Offset: 0x0019167E
		public static int GetTotalCreated()
		{
			return PathPool.GetTotalCreated(typeof(T));
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0019348F File Offset: 0x0019168F
		public static int GetSize()
		{
			return PathPool.GetSize(typeof(T));
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x001934A0 File Offset: 0x001916A0
		[Obsolete("Use PathPool.GetPath<T> instead of PathPool<T>.GetPath")]
		public static T GetPath()
		{
			return PathPool.GetPath<T>();
		}
	}
}
