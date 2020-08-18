using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000437 RID: 1079
public class ArrayWrapper<T> : IEnumerable
{
	// Token: 0x06001C81 RID: 7297 RVA: 0x00154FB8 File Offset: 0x001531B8
	public ArrayWrapper(int size)
	{
		this.elements = new T[size];
	}

	// Token: 0x06001C82 RID: 7298 RVA: 0x00154FCC File Offset: 0x001531CC
	public ArrayWrapper(T[] elements)
	{
		this.elements = elements;
	}

	// Token: 0x1700046F RID: 1135
	public T this[int i]
	{
		get
		{
			return this.elements[i];
		}
		set
		{
			this.elements[i] = value;
		}
	}

	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00154FF8 File Offset: 0x001531F8
	public int Length
	{
		get
		{
			return this.elements.Length;
		}
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x00155002 File Offset: 0x00153202
	public T[] Get()
	{
		return this.elements;
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x0015500A File Offset: 0x0015320A
	public IEnumerator GetEnumerator()
	{
		return this.elements.GetEnumerator();
	}

	// Token: 0x040035D9 RID: 13785
	[SerializeField]
	private T[] elements;
}
