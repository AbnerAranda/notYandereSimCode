using System;
using System.Collections.Generic;
using System.Threading;

namespace Pathfinding.Util
{
	// Token: 0x020005F5 RID: 1525
	public class ParallelWorkQueue<T>
	{
		// Token: 0x060029C7 RID: 10695 RVA: 0x001C42EF File Offset: 0x001C24EF
		public ParallelWorkQueue(Queue<T> queue)
		{
			this.queue = queue;
			this.initialCount = queue.Count;
			this.threadCount = Math.Min(this.initialCount, Math.Max(1, AstarPath.CalculateThreadCount(ThreadCount.AutomaticHighLoad)));
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x001C4328 File Offset: 0x001C2528
		public IEnumerable<int> Run(int progressTimeoutMillis)
		{
			if (this.initialCount != this.queue.Count)
			{
				throw new InvalidOperationException("Queue has been modified since the constructor");
			}
			if (this.initialCount == 0)
			{
				yield break;
			}
			this.waitEvents = new ManualResetEvent[this.threadCount];
			for (int i = 0; i < this.waitEvents.Length; i++)
			{
				this.waitEvents[i] = new ManualResetEvent(false);
				ThreadPool.QueueUserWorkItem(delegate(object threadIndex)
				{
					this.RunTask((int)threadIndex);
				}, i);
			}
			for (;;)
			{
				WaitHandle[] waitHandles = this.waitEvents;
				if (WaitHandle.WaitAll(waitHandles, progressTimeoutMillis))
				{
					break;
				}
				Queue<T> obj = this.queue;
				int count;
				lock (obj)
				{
					count = this.queue.Count;
				}
				yield return this.initialCount - count;
			}
			if (this.innerException != null)
			{
				throw this.innerException;
			}
			yield break;
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x001C4340 File Offset: 0x001C2540
		private void RunTask(int threadIndex)
		{
			try
			{
				for (;;)
				{
					Queue<T> obj = this.queue;
					T arg;
					lock (obj)
					{
						if (this.queue.Count == 0)
						{
							break;
						}
						arg = this.queue.Dequeue();
					}
					this.action(arg, threadIndex);
				}
			}
			catch (Exception ex)
			{
				this.innerException = ex;
				Queue<T> obj = this.queue;
				lock (obj)
				{
					this.queue.Clear();
				}
			}
			finally
			{
				this.waitEvents[threadIndex].Set();
			}
		}

		// Token: 0x04004408 RID: 17416
		public Action<T, int> action;

		// Token: 0x04004409 RID: 17417
		public readonly int threadCount;

		// Token: 0x0400440A RID: 17418
		private readonly Queue<T> queue;

		// Token: 0x0400440B RID: 17419
		private readonly int initialCount;

		// Token: 0x0400440C RID: 17420
		private ManualResetEvent[] waitEvents;

		// Token: 0x0400440D RID: 17421
		private Exception innerException;
	}
}
