using System;

namespace Pathfinding.Util
{
	// Token: 0x020005DE RID: 1502
	public static class Memory
	{
		// Token: 0x0600296D RID: 10605 RVA: 0x001BE5F0 File Offset: 0x001BC7F0
		public static void MemSet<T>(T[] array, T value, int byteSize) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = 32;
			int i = 0;
			int num2 = Math.Min(num, array.Length);
			while (i < num2)
			{
				array[i] = value;
				i++;
			}
			num2 = array.Length;
			while (i < num2)
			{
				Buffer.BlockCopy(array, 0, array, i * byteSize, Math.Min(num, num2 - i) * byteSize);
				i += num;
				num *= 2;
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x001BE654 File Offset: 0x001BC854
		public static void MemSet<T>(T[] array, T value, int totalSize, int byteSize) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = 32;
			int i = 0;
			int num2 = Math.Min(num, totalSize);
			while (i < num2)
			{
				array[i] = value;
				i++;
			}
			while (i < totalSize)
			{
				Buffer.BlockCopy(array, 0, array, i * byteSize, Math.Min(num, totalSize - i) * byteSize);
				i += num;
				num *= 2;
			}
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x001BE6B4 File Offset: 0x001BC8B4
		public static T[] ShrinkArray<T>(T[] arr, int newLength)
		{
			newLength = Math.Min(newLength, arr.Length);
			T[] array = new T[newLength];
			Array.Copy(arr, array, newLength);
			return array;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x001BE6DC File Offset: 0x001BC8DC
		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}
	}
}
