using System;

namespace Pathfinding.Util
{
	// Token: 0x020005EA RID: 1514
	public static class ObjectPool<T> where T : class, IAstarPooledObject, new()
	{
		// Token: 0x06002968 RID: 10600 RVA: 0x001C108C File Offset: 0x001BF28C
		public static T Claim()
		{
			return ObjectPoolSimple<T>.Claim();
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x001C1093 File Offset: 0x001BF293
		public static void Release(ref T obj)
		{
			T t = obj;
			ObjectPoolSimple<T>.Release(ref obj);
			t.OnEnterPool();
		}
	}
}
