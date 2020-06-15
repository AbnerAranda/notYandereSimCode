using System;

namespace Pathfinding
{
	// Token: 0x02000549 RID: 1353
	public struct AstarWorkItem
	{
		// Token: 0x0600238B RID: 9099 RVA: 0x00199EB0 File Offset: 0x001980B0
		public AstarWorkItem(Func<bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = null;
			this.update = update;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x00199ECE File Offset: 0x001980CE
		public AstarWorkItem(Func<IWorkItemContext, bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = update;
			this.update = null;
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00199EEC File Offset: 0x001980EC
		public AstarWorkItem(Action init, Func<bool, bool> update = null)
		{
			this.init = init;
			this.initWithContext = null;
			this.update = update;
			this.updateWithContext = null;
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00199F0A File Offset: 0x0019810A
		public AstarWorkItem(Action<IWorkItemContext> init, Func<IWorkItemContext, bool, bool> update = null)
		{
			this.init = null;
			this.initWithContext = init;
			this.update = null;
			this.updateWithContext = update;
		}

		// Token: 0x04004031 RID: 16433
		public Action init;

		// Token: 0x04004032 RID: 16434
		public Action<IWorkItemContext> initWithContext;

		// Token: 0x04004033 RID: 16435
		public Func<bool, bool> update;

		// Token: 0x04004034 RID: 16436
		public Func<IWorkItemContext, bool, bool> updateWithContext;
	}
}
