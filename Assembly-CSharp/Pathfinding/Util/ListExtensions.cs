using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020005E5 RID: 1509
	public static class ListExtensions
	{
		// Token: 0x06002952 RID: 10578 RVA: 0x001C05C0 File Offset: 0x001BE7C0
		public static T[] ToArrayFromPool<T>(this List<T> list)
		{
			T[] array = ArrayPool<T>.ClaimWithExactLength(list.Count);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = list[i];
			}
			return array;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x001C05F6 File Offset: 0x001BE7F6
		public static void ClearFast<T>(this List<T> list)
		{
			if (list.Count * 2 < list.Capacity)
			{
				list.RemoveRange(0, list.Count);
				return;
			}
			list.Clear();
		}
	}
}
