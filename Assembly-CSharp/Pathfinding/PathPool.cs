using System;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x02000544 RID: 1348
	public static class PathPool
	{
		// Token: 0x0600235B RID: 9051 RVA: 0x00198E3C File Offset: 0x0019703C
		public static void Pool(Path path)
		{
			Dictionary<Type, Stack<Path>> obj = PathPool.pool;
			lock (obj)
			{
				if (((IPathInternals)path).Pooled)
				{
					throw new ArgumentException("The path is already pooled.");
				}
				Stack<Path> stack;
				if (!PathPool.pool.TryGetValue(path.GetType(), out stack))
				{
					stack = new Stack<Path>();
					PathPool.pool[path.GetType()] = stack;
				}
				((IPathInternals)path).Pooled = true;
				((IPathInternals)path).OnEnterPool();
				stack.Push(path);
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x00198EC8 File Offset: 0x001970C8
		public static int GetTotalCreated(Type type)
		{
			int result;
			if (PathPool.totalCreated.TryGetValue(type, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00198EE8 File Offset: 0x001970E8
		public static int GetSize(Type type)
		{
			Stack<Path> stack;
			if (PathPool.pool.TryGetValue(type, out stack))
			{
				return stack.Count;
			}
			return 0;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00198F0C File Offset: 0x0019710C
		public static T GetPath<T>() where T : Path, new()
		{
			Dictionary<Type, Stack<Path>> obj = PathPool.pool;
			T result;
			lock (obj)
			{
				Stack<Path> stack;
				T t;
				if (PathPool.pool.TryGetValue(typeof(T), out stack) && stack.Count > 0)
				{
					t = (stack.Pop() as T);
				}
				else
				{
					t = Activator.CreateInstance<T>();
					if (!PathPool.totalCreated.ContainsKey(typeof(T)))
					{
						PathPool.totalCreated[typeof(T)] = 0;
					}
					Dictionary<Type, int> dictionary = PathPool.totalCreated;
					Type typeFromHandle = typeof(T);
					int num = dictionary[typeFromHandle];
					dictionary[typeFromHandle] = num + 1;
				}
				t.Pooled = false;
				t.Reset();
				result = t;
			}
			return result;
		}

		// Token: 0x04004017 RID: 16407
		private static readonly Dictionary<Type, Stack<Path>> pool = new Dictionary<Type, Stack<Path>>();

		// Token: 0x04004018 RID: 16408
		private static readonly Dictionary<Type, int> totalCreated = new Dictionary<Type, int>();
	}
}
