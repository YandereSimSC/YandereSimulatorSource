using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class BetterList<T>
{
	// Token: 0x06000367 RID: 871 RVA: 0x00020980 File Offset: 0x0001EB80
	public IEnumerator<T> GetEnumerator()
	{
		if (this.buffer != null)
		{
			int num;
			for (int i = 0; i < this.size; i = num)
			{
				yield return this.buffer[i];
				num = i + 1;
			}
		}
		yield break;
	}

	// Token: 0x17000066 RID: 102
	[DebuggerHidden]
	[Obsolete("Access the list.buffer[index] instead -- direct array access avoids a copy, so it can be much faster")]
	public T this[int i]
	{
		get
		{
			return this.buffer[i];
		}
		set
		{
			this.buffer[i] = value;
		}
	}

	// Token: 0x0600036A RID: 874 RVA: 0x000209AC File Offset: 0x0001EBAC
	private void AllocateMore()
	{
		T[] array = (this.buffer != null) ? new T[Mathf.Max(this.buffer.Length << 1, 32)] : new T[32];
		if (this.buffer != null && this.size > 0)
		{
			this.buffer.CopyTo(array, 0);
		}
		this.buffer = array;
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00020A08 File Offset: 0x0001EC08
	private void Trim()
	{
		if (this.size > 0)
		{
			if (this.size < this.buffer.Length)
			{
				T[] array = new T[this.size];
				for (int i = 0; i < this.size; i++)
				{
					array[i] = this.buffer[i];
				}
				this.buffer = array;
				return;
			}
		}
		else
		{
			this.buffer = null;
		}
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00020A6D File Offset: 0x0001EC6D
	public void Clear()
	{
		this.size = 0;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x00020A76 File Offset: 0x0001EC76
	public void Release()
	{
		this.size = 0;
		this.buffer = null;
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00020A88 File Offset: 0x0001EC88
	public void Add(T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		T[] array = this.buffer;
		int num = this.size;
		this.size = num + 1;
		array[num] = item;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00020AD0 File Offset: 0x0001ECD0
	public void Insert(int index, T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		if (index > -1 && index < this.size)
		{
			for (int i = this.size; i > index; i--)
			{
				this.buffer[i] = this.buffer[i - 1];
			}
			this.buffer[index] = item;
			this.size++;
			return;
		}
		this.Add(item);
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00020B58 File Offset: 0x0001ED58
	public bool Contains(T item)
	{
		if (this.buffer == null)
		{
			return false;
		}
		for (int i = 0; i < this.size; i++)
		{
			if (this.buffer[i].Equals(item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00020BA4 File Offset: 0x0001EDA4
	public int IndexOf(T item)
	{
		if (this.buffer == null)
		{
			return -1;
		}
		for (int i = 0; i < this.size; i++)
		{
			if (this.buffer[i].Equals(item))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00020BF0 File Offset: 0x0001EDF0
	public bool Remove(T item)
	{
		if (this.buffer != null)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < this.size; i++)
			{
				if (@default.Equals(this.buffer[i], item))
				{
					this.size--;
					this.buffer[i] = default(T);
					for (int j = i; j < this.size; j++)
					{
						this.buffer[j] = this.buffer[j + 1];
					}
					this.buffer[this.size] = default(T);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00020CA8 File Offset: 0x0001EEA8
	public void RemoveAt(int index)
	{
		if (this.buffer != null && index > -1 && index < this.size)
		{
			this.size--;
			this.buffer[index] = default(T);
			for (int i = index; i < this.size; i++)
			{
				this.buffer[i] = this.buffer[i + 1];
			}
			this.buffer[this.size] = default(T);
		}
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00020D34 File Offset: 0x0001EF34
	public T Pop()
	{
		if (this.buffer != null && this.size != 0)
		{
			T[] array = this.buffer;
			int num = this.size - 1;
			this.size = num;
			T result = array[num];
			this.buffer[this.size] = default(T);
			return result;
		}
		return default(T);
	}

	// Token: 0x06000375 RID: 885 RVA: 0x00020D91 File Offset: 0x0001EF91
	public T[] ToArray()
	{
		this.Trim();
		return this.buffer;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00020DA0 File Offset: 0x0001EFA0
	[DebuggerHidden]
	[DebuggerStepThrough]
	public void Sort(BetterList<T>.CompareFunc comparer)
	{
		int num = 0;
		int num2 = this.size - 1;
		bool flag = true;
		while (flag)
		{
			flag = false;
			for (int i = num; i < num2; i++)
			{
				if (comparer(this.buffer[i], this.buffer[i + 1]) > 0)
				{
					T t = this.buffer[i];
					this.buffer[i] = this.buffer[i + 1];
					this.buffer[i + 1] = t;
					flag = true;
				}
				else if (!flag)
				{
					num = ((i == 0) ? 0 : (i - 1));
				}
			}
		}
	}

	// Token: 0x040004B3 RID: 1203
	public T[] buffer;

	// Token: 0x040004B4 RID: 1204
	public int size;

	// Token: 0x0200062F RID: 1583
	// (Invoke) Token: 0x06002A83 RID: 10883
	public delegate int CompareFunc(T left, T right);
}
