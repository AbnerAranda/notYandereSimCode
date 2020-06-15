using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005ED RID: 1517
	public static class StackPool<T>
	{
		// Token: 0x06002981 RID: 10625 RVA: 0x001C1908 File Offset: 0x001BFB08
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

		// Token: 0x06002982 RID: 10626 RVA: 0x001C1954 File Offset: 0x001BFB54
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

		// Token: 0x06002983 RID: 10627 RVA: 0x001C1990 File Offset: 0x001BFB90
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

		// Token: 0x06002984 RID: 10628 RVA: 0x001C19DB File Offset: 0x001BFBDB
		public static void Clear()
		{
			StackPool<T>.pool.Clear();
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x001C19E7 File Offset: 0x001BFBE7
		public static int GetSize()
		{
			return StackPool<T>.pool.Count;
		}

		// Token: 0x040043E9 RID: 17385
		private static readonly List<Stack<T>> pool = new List<Stack<T>>();
	}
}
