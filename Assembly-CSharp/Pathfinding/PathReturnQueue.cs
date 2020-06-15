using System;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x02000547 RID: 1351
	internal class PathReturnQueue
	{
		// Token: 0x06002379 RID: 9081 RVA: 0x001997D2 File Offset: 0x001979D2
		public PathReturnQueue(object pathsClaimedSilentlyBy)
		{
			this.pathsClaimedSilentlyBy = pathsClaimedSilentlyBy;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x001997EC File Offset: 0x001979EC
		public void Enqueue(Path path)
		{
			Queue<Path> obj = this.pathReturnQueue;
			lock (obj)
			{
				this.pathReturnQueue.Enqueue(path);
			}
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00199834 File Offset: 0x00197A34
		public void ReturnPaths(bool timeSlice)
		{
			long num = timeSlice ? (DateTime.UtcNow.Ticks + 10000L) : 0L;
			int num2 = 0;
			for (;;)
			{
				Queue<Path> obj = this.pathReturnQueue;
				Path path;
				lock (obj)
				{
					if (this.pathReturnQueue.Count == 0)
					{
						return;
					}
					path = this.pathReturnQueue.Dequeue();
				}
				((IPathInternals)path).ReturnPath();
				((IPathInternals)path).AdvanceState(PathState.Returned);
				path.Release(this.pathsClaimedSilentlyBy, true);
				num2++;
				if (num2 > 5 && timeSlice)
				{
					num2 = 0;
					if (DateTime.UtcNow.Ticks >= num)
					{
						break;
					}
				}
			}
		}

		// Token: 0x04004026 RID: 16422
		private Queue<Path> pathReturnQueue = new Queue<Path>();

		// Token: 0x04004027 RID: 16423
		private object pathsClaimedSilentlyBy;
	}
}
