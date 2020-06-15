using System;

namespace Pathfinding
{
	// Token: 0x0200055C RID: 1372
	public interface IUpdatableGraph
	{
		// Token: 0x0600244E RID: 9294
		void UpdateArea(GraphUpdateObject o);

		// Token: 0x0600244F RID: 9295
		void UpdateAreaInit(GraphUpdateObject o);

		// Token: 0x06002450 RID: 9296
		void UpdateAreaPost(GraphUpdateObject o);

		// Token: 0x06002451 RID: 9297
		GraphUpdateThreading CanUpdateAsync(GraphUpdateObject o);
	}
}
