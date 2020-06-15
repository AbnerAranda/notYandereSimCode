using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200043C RID: 1084
public class ArrayWrapper<T> : IEnumerable
{
	// Token: 0x06001CA6 RID: 7334 RVA: 0x001584BC File Offset: 0x001566BC
	public ArrayWrapper(int size)
	{
		this.elements = new T[size];
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x001584D0 File Offset: 0x001566D0
	public ArrayWrapper(T[] elements)
	{
		this.elements = elements;
	}

	// Token: 0x17000474 RID: 1140
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

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x06001CAA RID: 7338 RVA: 0x001584FC File Offset: 0x001566FC
	public int Length
	{
		get
		{
			return this.elements.Length;
		}
	}

	// Token: 0x06001CAB RID: 7339 RVA: 0x00158506 File Offset: 0x00156706
	public T[] Get()
	{
		return this.elements;
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x0015850E File Offset: 0x0015670E
	public IEnumerator GetEnumerator()
	{
		return this.elements.GetEnumerator();
	}

	// Token: 0x0400364E RID: 13902
	[SerializeField]
	private T[] elements;
}
