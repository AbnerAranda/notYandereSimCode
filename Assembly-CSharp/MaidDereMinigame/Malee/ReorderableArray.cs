using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame.Malee
{
	// Token: 0x0200051D RID: 1309
	[Serializable]
	public abstract class ReorderableArray<T> : ICloneable, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600209B RID: 8347 RVA: 0x0018B7F0 File Offset: 0x001899F0
		public ReorderableArray() : this(0)
		{
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0018B7F9 File Offset: 0x001899F9
		public ReorderableArray(int length)
		{
			this.array = new List<T>(length);
		}

		// Token: 0x170004D8 RID: 1240
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

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x0018B835 File Offset: 0x00189A35
		public int Length
		{
			get
			{
				return this.array.Count;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x0002D199 File Offset: 0x0002B399
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x0018B835 File Offset: 0x00189A35
		public int Count
		{
			get
			{
				return this.array.Count;
			}
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x0018B842 File Offset: 0x00189A42
		public object Clone()
		{
			return new List<T>(this.array);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0018B84F File Offset: 0x00189A4F
		public void CopyFrom(IEnumerable<T> value)
		{
			this.array.Clear();
			this.array.AddRange(value);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x0018B868 File Offset: 0x00189A68
		public bool Contains(T value)
		{
			return this.array.Contains(value);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0018B876 File Offset: 0x00189A76
		public int IndexOf(T value)
		{
			return this.array.IndexOf(value);
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x0018B884 File Offset: 0x00189A84
		public void Insert(int index, T item)
		{
			this.array.Insert(index, item);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x0018B893 File Offset: 0x00189A93
		public void RemoveAt(int index)
		{
			this.array.RemoveAt(index);
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0018B8A1 File Offset: 0x00189AA1
		public void Add(T item)
		{
			this.array.Add(item);
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0018B8AF File Offset: 0x00189AAF
		public void Clear()
		{
			this.array.Clear();
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0018B8BC File Offset: 0x00189ABC
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.array.CopyTo(array, arrayIndex);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x0018B8CB File Offset: 0x00189ACB
		public bool Remove(T item)
		{
			return this.array.Remove(item);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x0018B8D9 File Offset: 0x00189AD9
		public T[] ToArray()
		{
			return this.array.ToArray();
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0018B8E6 File Offset: 0x00189AE6
		public IEnumerator<T> GetEnumerator()
		{
			return this.array.GetEnumerator();
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0018B8E6 File Offset: 0x00189AE6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.array.GetEnumerator();
		}

		// Token: 0x04003EC0 RID: 16064
		[SerializeField]
		private List<T> array = new List<T>();
	}
}
