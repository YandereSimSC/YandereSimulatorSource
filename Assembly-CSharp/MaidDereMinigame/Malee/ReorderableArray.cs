using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame.Malee
{
	// Token: 0x0200050D RID: 1293
	[Serializable]
	public abstract class ReorderableArray<T> : ICloneable, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06002059 RID: 8281 RVA: 0x001862D4 File Offset: 0x001844D4
		public ReorderableArray() : this(0)
		{
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x001862DD File Offset: 0x001844DD
		public ReorderableArray(int length)
		{
			this.array = new List<T>(length);
		}

		// Token: 0x170004D2 RID: 1234
		public T this[int index]
		{
			get
			{
				return this.array[index];
			}
			set
			{
				this.array[index] = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00186319 File Offset: 0x00184519
		public int Length
		{
			get
			{
				return this.array.Count;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x0002D171 File Offset: 0x0002B371
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00186319 File Offset: 0x00184519
		public int Count
		{
			get
			{
				return this.array.Count;
			}
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00186326 File Offset: 0x00184526
		public object Clone()
		{
			return new List<T>(this.array);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00186333 File Offset: 0x00184533
		public void CopyFrom(IEnumerable<T> value)
		{
			this.array.Clear();
			this.array.AddRange(value);
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x0018634C File Offset: 0x0018454C
		public bool Contains(T value)
		{
			return this.array.Contains(value);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0018635A File Offset: 0x0018455A
		public int IndexOf(T value)
		{
			return this.array.IndexOf(value);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00186368 File Offset: 0x00184568
		public void Insert(int index, T item)
		{
			this.array.Insert(index, item);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00186377 File Offset: 0x00184577
		public void RemoveAt(int index)
		{
			this.array.RemoveAt(index);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00186385 File Offset: 0x00184585
		public void Add(T item)
		{
			this.array.Add(item);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00186393 File Offset: 0x00184593
		public void Clear()
		{
			this.array.Clear();
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x001863A0 File Offset: 0x001845A0
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.array.CopyTo(array, arrayIndex);
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x001863AF File Offset: 0x001845AF
		public bool Remove(T item)
		{
			return this.array.Remove(item);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x001863BD File Offset: 0x001845BD
		public T[] ToArray()
		{
			return this.array.ToArray();
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x001863CA File Offset: 0x001845CA
		public IEnumerator<T> GetEnumerator()
		{
			return this.array.GetEnumerator();
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x001863CA File Offset: 0x001845CA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.array.GetEnumerator();
		}

		// Token: 0x04003E10 RID: 15888
		[SerializeField]
		private List<T> array = new List<T>();
	}
}
