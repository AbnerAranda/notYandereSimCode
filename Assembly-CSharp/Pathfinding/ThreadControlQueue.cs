using System;
using System.Threading;

namespace Pathfinding
{
	// Token: 0x02000548 RID: 1352
	internal class ThreadControlQueue
	{
		// Token: 0x0600237C RID: 9084 RVA: 0x001998E8 File Offset: 0x00197AE8
		public ThreadControlQueue(int numReceivers)
		{
			this.numReceivers = numReceivers;
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x0019990E File Offset: 0x00197B0E
		public bool IsEmpty
		{
			get
			{
				return this.head == null;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x00199919 File Offset: 0x00197B19
		public bool IsTerminating
		{
			get
			{
				return this.terminate;
			}
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x00199924 File Offset: 0x00197B24
		public void Block()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = true;
				this.block.Reset();
			}
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x00199974 File Offset: 0x00197B74
		public void Unblock()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = false;
				this.block.Set();
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x001999C4 File Offset: 0x00197BC4
		public void Lock()
		{
			Monitor.Enter(this.lockObj);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x001999D1 File Offset: 0x00197BD1
		public void Unlock()
		{
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x001999E0 File Offset: 0x00197BE0
		public bool AllReceiversBlocked
		{
			get
			{
				object obj = this.lockObj;
				bool result;
				lock (obj)
				{
					result = (this.blocked && this.blockedReceivers == this.numReceivers);
				}
				return result;
			}
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x00199A38 File Offset: 0x00197C38
		public void PushFront(Path path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (!this.terminate)
				{
					if (this.tail == null)
					{
						this.head = path;
						this.tail = path;
						if (this.starving && !this.blocked)
						{
							this.starving = false;
							this.block.Set();
						}
						else
						{
							this.starving = false;
						}
					}
					else
					{
						path.next = this.head;
						this.head = path;
					}
				}
			}
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00199AD4 File Offset: 0x00197CD4
		public void Push(Path path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (!this.terminate)
				{
					if (this.tail == null)
					{
						this.head = path;
						this.tail = path;
						if (this.starving && !this.blocked)
						{
							this.starving = false;
							this.block.Set();
						}
						else
						{
							this.starving = false;
						}
					}
					else
					{
						this.tail.next = path;
						this.tail = path;
					}
				}
			}
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x00199B70 File Offset: 0x00197D70
		private void Starving()
		{
			this.starving = true;
			this.block.Reset();
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00199B88 File Offset: 0x00197D88
		public void TerminateReceivers()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.terminate = true;
				this.block.Set();
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00199BD8 File Offset: 0x00197DD8
		public Path Pop()
		{
			Path result;
			lock (this.lockObj)
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				while (this.blocked || this.starving)
				{
					this.blockedReceivers++;
					if (this.blockedReceivers > this.numReceivers)
					{
						throw new InvalidOperationException(string.Concat(new object[]
						{
							"More receivers are blocked than specified in constructor (",
							this.blockedReceivers,
							" > ",
							this.numReceivers,
							")"
						}));
					}
					Monitor.Exit(this.lockObj);
					this.block.WaitOne();
					Monitor.Enter(this.lockObj);
					if (this.terminate)
					{
						throw new ThreadControlQueue.QueueTerminationException();
					}
					this.blockedReceivers--;
					if (this.head == null)
					{
						this.Starving();
					}
				}
				Path path = this.head;
				Path next = this.head.next;
				if (next == null)
				{
					this.tail = null;
				}
				this.head.next = null;
				this.head = next;
				result = path;
			}
			return result;
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x00199D40 File Offset: 0x00197F40
		public void ReceiverTerminated()
		{
			Monitor.Enter(this.lockObj);
			this.blockedReceivers++;
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00199D68 File Offset: 0x00197F68
		public Path PopNoBlock(bool blockedBefore)
		{
			Path result;
			lock (this.lockObj)
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				if (this.blocked || this.starving)
				{
					if (!blockedBefore)
					{
						this.blockedReceivers++;
						if (this.terminate)
						{
							throw new ThreadControlQueue.QueueTerminationException();
						}
						if (this.blockedReceivers != this.numReceivers && this.blockedReceivers > this.numReceivers)
						{
							throw new InvalidOperationException(string.Concat(new object[]
							{
								"More receivers are blocked than specified in constructor (",
								this.blockedReceivers,
								" > ",
								this.numReceivers,
								")"
							}));
						}
					}
					result = null;
				}
				else
				{
					if (blockedBefore)
					{
						this.blockedReceivers--;
					}
					Path path = this.head;
					Path next = this.head.next;
					if (next == null)
					{
						this.tail = null;
					}
					this.head.next = null;
					this.head = next;
					result = path;
				}
			}
			return result;
		}

		// Token: 0x04004028 RID: 16424
		private Path head;

		// Token: 0x04004029 RID: 16425
		private Path tail;

		// Token: 0x0400402A RID: 16426
		private readonly object lockObj = new object();

		// Token: 0x0400402B RID: 16427
		private readonly int numReceivers;

		// Token: 0x0400402C RID: 16428
		private bool blocked;

		// Token: 0x0400402D RID: 16429
		private int blockedReceivers;

		// Token: 0x0400402E RID: 16430
		private bool starving;

		// Token: 0x0400402F RID: 16431
		private bool terminate;

		// Token: 0x04004030 RID: 16432
		private ManualResetEvent block = new ManualResetEvent(true);

		// Token: 0x0200073C RID: 1852
		public class QueueTerminationException : Exception
		{
		}
	}
}
