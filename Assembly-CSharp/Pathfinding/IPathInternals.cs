using System;

namespace Pathfinding
{
	// Token: 0x02000552 RID: 1362
	internal interface IPathInternals
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06002416 RID: 9238
		PathHandler PathHandler { get; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002417 RID: 9239
		// (set) Token: 0x06002418 RID: 9240
		bool Pooled { get; set; }

		// Token: 0x06002419 RID: 9241
		void AdvanceState(PathState s);

		// Token: 0x0600241A RID: 9242
		void OnEnterPool();

		// Token: 0x0600241B RID: 9243
		void Reset();

		// Token: 0x0600241C RID: 9244
		void ReturnPath();

		// Token: 0x0600241D RID: 9245
		void PrepareBase(PathHandler handler);

		// Token: 0x0600241E RID: 9246
		void Prepare();

		// Token: 0x0600241F RID: 9247
		void Initialize();

		// Token: 0x06002420 RID: 9248
		void Cleanup();

		// Token: 0x06002421 RID: 9249
		void CalculateStep(long targetTick);
	}
}
