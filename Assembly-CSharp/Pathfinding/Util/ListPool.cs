using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020005E7 RID: 1511
	public static class ListPool<T>
	{
		// Token: 0x0600295C RID: 10588 RVA: 0x001C09BC File Offset: 0x001BEBBC
		public static List<T> Claim()
		{
			List<List<T>> obj = ListPool<T>.pool;
			List<T> result;
			lock (obj)
			{
				if (ListPool<T>.pool.Count > 0)
				{
					List<T> list = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
					ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
					ListPool<T>.inPool.Remove(list);
					result = list;
				}
				else
				{
					result = new List<T>();
				}
			}
			return result;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x001C0A48 File Offset: 0x001BEC48
		private static int FindCandidate(List<List<T>> pool, int capacity)
		{
			List<T> list = null;
			int result = -1;
			int num = 0;
			while (num < pool.Count && num < 8)
			{
				List<T> list2 = pool[pool.Count - 1 - num];
				if ((list == null || list2.Capacity > list.Capacity) && list2.Capacity < capacity * 16)
				{
					list = list2;
					result = pool.Count - 1 - num;
					if (list.Capacity >= capacity)
					{
						return result;
					}
				}
				num++;
			}
			return result;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x001C0AB8 File Offset: 0x001BECB8
		public static List<T> Claim(int capacity)
		{
			List<List<T>> obj = ListPool<T>.pool;
			List<T> result;
			lock (obj)
			{
				List<List<T>> list = ListPool<T>.pool;
				int num = ListPool<T>.FindCandidate(ListPool<T>.pool, capacity);
				if (capacity > 5000)
				{
					int num2 = ListPool<T>.FindCandidate(ListPool<T>.largePool, capacity);
					if (num2 != -1)
					{
						list = ListPool<T>.largePool;
						num = num2;
					}
				}
				if (num == -1)
				{
					result = new List<T>(capacity);
				}
				else
				{
					List<T> list2 = list[num];
					ListPool<T>.inPool.Remove(list2);
					list[num] = list[list.Count - 1];
					list.RemoveAt(list.Count - 1);
					result = list2;
				}
			}
			return result;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x001C0B74 File Offset: 0x001BED74
		public static void Warmup(int count, int size)
		{
			List<List<T>> obj = ListPool<T>.pool;
			lock (obj)
			{
				List<T>[] array = new List<T>[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = ListPool<T>.Claim(size);
				}
				for (int j = 0; j < count; j++)
				{
					ListPool<T>.Release(array[j]);
				}
			}
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x001C0BE4 File Offset: 0x001BEDE4
		public static void Release(ref List<T> list)
		{
			ListPool<T>.Release(list);
			list = null;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x001C0BF0 File Offset: 0x001BEDF0
		public static void Release(List<T> list)
		{
			list.ClearFast<T>();
			List<List<T>> obj = ListPool<T>.pool;
			lock (obj)
			{
				if (list.Capacity > 5000)
				{
					ListPool<T>.largePool.Add(list);
					if (ListPool<T>.largePool.Count > 8)
					{
						ListPool<T>.largePool.RemoveAt(0);
					}
				}
				else
				{
					ListPool<T>.pool.Add(list);
				}
			}
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x001C0C6C File Offset: 0x001BEE6C
		public static void Clear()
		{
			List<List<T>> obj = ListPool<T>.pool;
			lock (obj)
			{
				ListPool<T>.pool.Clear();
			}
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x001C0CB0 File Offset: 0x001BEEB0
		public static int GetSize()
		{
			return ListPool<T>.pool.Count;
		}

		// Token: 0x040043DB RID: 17371
		private static readonly List<List<T>> pool = new List<List<T>>();

		// Token: 0x040043DC RID: 17372
		private static readonly List<List<T>> largePool = new List<List<T>>();

		// Token: 0x040043DD RID: 17373
		private static readonly HashSet<List<T>> inPool = new HashSet<List<T>>();

		// Token: 0x040043DE RID: 17374
		private const int MaxCapacitySearchLength = 8;

		// Token: 0x040043DF RID: 17375
		private const int LargeThreshold = 5000;

		// Token: 0x040043E0 RID: 17376
		private const int MaxLargePoolSize = 8;
	}
}
