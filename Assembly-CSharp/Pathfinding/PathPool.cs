using System;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x0200052E RID: 1326
	public static class PathPool
	{
		// Token: 0x06002305 RID: 8965 RVA: 0x00193250 File Offset: 0x00191450
		public static void Pool(Path path)
		{
			Dictionary<Type, Stack<Path>> obj = PathPool.pool;
			lock (obj)
			{
				if (((IPathInternals)path).Pooled)
				{
					throw new ArgumentException("The path is already pooled.");
				}
				Stack<Path> stack;
				if (!PathPool.pool.TryGetValue(path.GetType(), out stack))
				{
					stack = new Stack<Path>();
					PathPool.pool[path.GetType()] = stack;
				}
				((IPathInternals)path).Pooled = true;
				((IPathInternals)path).OnEnterPool();
				stack.Push(path);
			}
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x001932DC File Offset: 0x001914DC
		public static int GetTotalCreated(Type type)
		{
			int result;
			if (PathPool.totalCreated.TryGetValue(type, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x001932FC File Offset: 0x001914FC
		public static int GetSize(Type type)
		{
			Stack<Path> stack;
			if (PathPool.pool.TryGetValue(type, out stack))
			{
				return stack.Count;
			}
			return 0;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00193320 File Offset: 0x00191520
		public static T GetPath<T>() where T : Path, new()
		{
			Dictionary<Type, Stack<Path>> obj = PathPool.pool;
			T result;
			lock (obj)
			{
				Stack<Path> stack;
				T t;
				if (PathPool.pool.TryGetValue(typeof(T), out stack) && stack.Count > 0)
				{
					t = (stack.Pop() as T);
				}
				else
				{
					t = Activator.CreateInstance<T>();
					if (!PathPool.totalCreated.ContainsKey(typeof(T)))
					{
						PathPool.totalCreated[typeof(T)] = 0;
					}
					Dictionary<Type, int> dictionary = PathPool.totalCreated;
					Type typeFromHandle = typeof(T);
					int num = dictionary[typeFromHandle];
					dictionary[typeFromHandle] = num + 1;
				}
				t.Pooled = false;
				t.Reset();
				result = t;
			}
			return result;
		}

		// Token: 0x04003F39 RID: 16185
		private static readonly Dictionary<Type, Stack<Path>> pool = new Dictionary<Type, Stack<Path>>();

		// Token: 0x04003F3A RID: 16186
		private static readonly Dictionary<Type, int> totalCreated = new Dictionary<Type, int>();
	}
}
