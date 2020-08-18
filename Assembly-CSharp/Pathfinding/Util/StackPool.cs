using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005D7 RID: 1495
	public static class StackPool<T>
	{
		// Token: 0x0600292B RID: 10539 RVA: 0x001BBD1C File Offset: 0x001B9F1C
		public static Stack<T> Claim()
		{
			if (StackPool<T>.pool.Count > 0)
			{
				Stack<T> result = StackPool<T>.pool[StackPool<T>.pool.Count - 1];
				StackPool<T>.pool.RemoveAt(StackPool<T>.pool.Count - 1);
				return result;
			}
			return new Stack<T>();
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x001BBD68 File Offset: 0x001B9F68
		public static void Warmup(int count)
		{
			Stack<T>[] array = new Stack<T>[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = StackPool<T>.Claim();
			}
			for (int j = 0; j < count; j++)
			{
				StackPool<T>.Release(array[j]);
			}
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x001BBDA4 File Offset: 0x001B9FA4
		public static void Release(Stack<T> stack)
		{
			for (int i = 0; i < StackPool<T>.pool.Count; i++)
			{
				if (StackPool<T>.pool[i] == stack)
				{
					Debug.LogError("The Stack is released even though it is inside the pool");
				}
			}
			stack.Clear();
			StackPool<T>.pool.Add(stack);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x001BBDEF File Offset: 0x001B9FEF
		public static void Clear()
		{
			StackPool<T>.pool.Clear();
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x001BBDFB File Offset: 0x001B9FFB
		public static int GetSize()
		{
			return StackPool<T>.pool.Count;
		}

		// Token: 0x0400430B RID: 17163
		private static readonly List<Stack<T>> pool = new List<Stack<T>>();
	}
}
