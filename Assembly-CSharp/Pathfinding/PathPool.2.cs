using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000545 RID: 1349
	[Obsolete("Generic version is now obsolete to trade an extremely tiny performance decrease for a large decrease in boilerplate for Path classes")]
	public static class PathPool<T> where T : Path, new()
	{
		// Token: 0x06002360 RID: 9056 RVA: 0x00199002 File Offset: 0x00197202
		public static void Recycle(T path)
		{
			PathPool.Pool(path);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x00199010 File Offset: 0x00197210
		public static void Warmup(int count, int length)
		{
			ListPool<GraphNode>.Warmup(count, length);
			ListPool<Vector3>.Warmup(count, length);
			Path[] array = new Path[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = PathPool<T>.GetPath();
				array[i].Claim(array);
			}
			for (int j = 0; j < count; j++)
			{
				array[j].Release(array, false);
			}
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0019906A File Offset: 0x0019726A
		public static int GetTotalCreated()
		{
			return PathPool.GetTotalCreated(typeof(T));
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0019907B File Offset: 0x0019727B
		public static int GetSize()
		{
			return PathPool.GetSize(typeof(T));
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0019908C File Offset: 0x0019728C
		[Obsolete("Use PathPool.GetPath<T> instead of PathPool<T>.GetPath")]
		public static T GetPath()
		{
			return PathPool.GetPath<T>();
		}
	}
}
