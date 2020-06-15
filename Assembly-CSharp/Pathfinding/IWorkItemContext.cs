using System;

namespace Pathfinding
{
	// Token: 0x0200054A RID: 1354
	public interface IWorkItemContext
	{
		// Token: 0x0600238F RID: 9103
		void QueueFloodFill();

		// Token: 0x06002390 RID: 9104
		void EnsureValidFloodFill();
	}
}
