using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200054B RID: 1355
	internal class WorkItemProcessor : IWorkItemContext
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x00199F28 File Offset: 0x00198128
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x00199F30 File Offset: 0x00198130
		public bool workItemsInProgressRightNow { get; private set; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x00199F39 File Offset: 0x00198139
		// (set) Token: 0x06002394 RID: 9108 RVA: 0x00199F41 File Offset: 0x00198141
		public bool workItemsInProgress { get; private set; }

		// Token: 0x06002395 RID: 9109 RVA: 0x00199F4A File Offset: 0x0019814A
		void IWorkItemContext.QueueFloodFill()
		{
			this.queuedWorkItemFloodFill = true;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x00199F53 File Offset: 0x00198153
		public void EnsureValidFloodFill()
		{
			if (this.queuedWorkItemFloodFill)
			{
				this.astar.FloodFill();
			}
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x00199F68 File Offset: 0x00198168
		public WorkItemProcessor(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x00199F82 File Offset: 0x00198182
		public void OnFloodFill()
		{
			this.queuedWorkItemFloodFill = false;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00199F8B File Offset: 0x0019818B
		public void AddWorkItem(AstarWorkItem item)
		{
			this.workItems.Enqueue(item);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00199F9C File Offset: 0x0019819C
		public bool ProcessWorkItems(bool force)
		{
			if (this.workItemsInProgressRightNow)
			{
				throw new Exception("Processing work items recursively. Please do not wait for other work items to be completed inside work items. If you think this is not caused by any of your scripts, this might be a bug.");
			}
			this.workItemsInProgressRightNow = true;
			this.astar.data.LockGraphStructure(true);
			while (this.workItems.Count > 0)
			{
				if (!this.workItemsInProgress)
				{
					this.workItemsInProgress = true;
					this.queuedWorkItemFloodFill = false;
				}
				AstarWorkItem astarWorkItem = this.workItems[0];
				if (astarWorkItem.init != null)
				{
					astarWorkItem.init();
					astarWorkItem.init = null;
				}
				if (astarWorkItem.initWithContext != null)
				{
					astarWorkItem.initWithContext(this);
					astarWorkItem.initWithContext = null;
				}
				this.workItems[0] = astarWorkItem;
				bool flag;
				try
				{
					if (astarWorkItem.update != null)
					{
						flag = astarWorkItem.update(force);
					}
					else
					{
						flag = (astarWorkItem.updateWithContext == null || astarWorkItem.updateWithContext(this, force));
					}
				}
				catch
				{
					this.workItems.Dequeue();
					this.workItemsInProgressRightNow = false;
					this.astar.data.UnlockGraphStructure();
					throw;
				}
				if (!flag)
				{
					if (force)
					{
						Debug.LogError("Misbehaving WorkItem. 'force'=true but the work item did not complete.\nIf force=true is passed to a WorkItem it should always return true.");
					}
					this.workItemsInProgressRightNow = false;
					this.astar.data.UnlockGraphStructure();
					return false;
				}
				this.workItems.Dequeue();
			}
			this.EnsureValidFloodFill();
			this.workItemsInProgressRightNow = false;
			this.workItemsInProgress = false;
			this.astar.data.UnlockGraphStructure();
			return true;
		}

		// Token: 0x04004036 RID: 16438
		private readonly AstarPath astar;

		// Token: 0x04004037 RID: 16439
		private readonly WorkItemProcessor.IndexedQueue<AstarWorkItem> workItems = new WorkItemProcessor.IndexedQueue<AstarWorkItem>();

		// Token: 0x04004038 RID: 16440
		private bool queuedWorkItemFloodFill;

		// Token: 0x0200073D RID: 1853
		private class IndexedQueue<T>
		{
			// Token: 0x1700067C RID: 1660
			public T this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					return this.buffer[(this.start + index) % this.buffer.Length];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					this.buffer[(this.start + index) % this.buffer.Length] = value;
				}
			}

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x06002D0E RID: 11534 RVA: 0x001D041B File Offset: 0x001CE61B
			// (set) Token: 0x06002D0F RID: 11535 RVA: 0x001D0423 File Offset: 0x001CE623
			public int Count { get; private set; }

			// Token: 0x06002D10 RID: 11536 RVA: 0x001D042C File Offset: 0x001CE62C
			public void Enqueue(T item)
			{
				if (this.Count == this.buffer.Length)
				{
					T[] array = new T[this.buffer.Length * 2];
					for (int i = 0; i < this.Count; i++)
					{
						array[i] = this[i];
					}
					this.buffer = array;
					this.start = 0;
				}
				this.buffer[(this.start + this.Count) % this.buffer.Length] = item;
				int count = this.Count;
				this.Count = count + 1;
			}

			// Token: 0x06002D11 RID: 11537 RVA: 0x001D04B8 File Offset: 0x001CE6B8
			public T Dequeue()
			{
				if (this.Count == 0)
				{
					throw new InvalidOperationException();
				}
				T result = this.buffer[this.start];
				this.start = (this.start + 1) % this.buffer.Length;
				int count = this.Count;
				this.Count = count - 1;
				return result;
			}

			// Token: 0x04004A00 RID: 18944
			private T[] buffer = new T[4];

			// Token: 0x04004A01 RID: 18945
			private int start;
		}
	}
}
