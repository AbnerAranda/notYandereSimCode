using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020005EB RID: 1515
	public static class ObjectPoolSimple<T> where T : class, new()
	{
		// Token: 0x0600296A RID: 10602 RVA: 0x001C10AC File Offset: 0x001BF2AC
		public static T Claim()
		{
			List<T> obj = ObjectPoolSimple<T>.pool;
			T result;
			lock (obj)
			{
				if (ObjectPoolSimple<T>.pool.Count > 0)
				{
					T t = ObjectPoolSimple<T>.pool[ObjectPoolSimple<T>.pool.Count - 1];
					ObjectPoolSimple<T>.pool.RemoveAt(ObjectPoolSimple<T>.pool.Count - 1);
					ObjectPoolSimple<T>.inPool.Remove(t);
					result = t;
				}
				else
				{
					result = Activator.CreateInstance<T>();
				}
			}
			return result;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x001C1138 File Offset: 0x001BF338
		public static void Release(ref T obj)
		{
			List<T> obj2 = ObjectPoolSimple<T>.pool;
			lock (obj2)
			{
				ObjectPoolSimple<T>.pool.Add(obj);
			}
			obj = default(T);
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x001C1188 File Offset: 0x001BF388
		public static void Clear()
		{
			List<T> obj = ObjectPoolSimple<T>.pool;
			lock (obj)
			{
				ObjectPoolSimple<T>.pool.Clear();
			}
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x001C11CC File Offset: 0x001BF3CC
		public static int GetSize()
		{
			return ObjectPoolSimple<T>.pool.Count;
		}

		// Token: 0x040043E1 RID: 17377
		private static List<T> pool = new List<T>();

		// Token: 0x040043E2 RID: 17378
		private static readonly HashSet<T> inPool = new HashSet<T>();
	}
}
