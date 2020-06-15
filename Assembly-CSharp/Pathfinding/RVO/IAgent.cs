using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005D7 RID: 1495
	public interface IAgent
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002884 RID: 10372
		// (set) Token: 0x06002885 RID: 10373
		Vector2 Position { get; set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002886 RID: 10374
		// (set) Token: 0x06002887 RID: 10375
		float ElevationCoordinate { get; set; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002888 RID: 10376
		Vector2 CalculatedTargetPoint { get; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002889 RID: 10377
		float CalculatedSpeed { get; }

		// Token: 0x0600288A RID: 10378
		void SetTarget(Vector2 targetPoint, float desiredSpeed, float maxSpeed);

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600288B RID: 10379
		// (set) Token: 0x0600288C RID: 10380
		bool Locked { get; set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600288D RID: 10381
		// (set) Token: 0x0600288E RID: 10382
		float Radius { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600288F RID: 10383
		// (set) Token: 0x06002890 RID: 10384
		float Height { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002891 RID: 10385
		// (set) Token: 0x06002892 RID: 10386
		float AgentTimeHorizon { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002893 RID: 10387
		// (set) Token: 0x06002894 RID: 10388
		float ObstacleTimeHorizon { get; set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002895 RID: 10389
		// (set) Token: 0x06002896 RID: 10390
		int MaxNeighbours { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002897 RID: 10391
		int NeighbourCount { get; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002898 RID: 10392
		// (set) Token: 0x06002899 RID: 10393
		RVOLayer Layer { get; set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600289A RID: 10394
		// (set) Token: 0x0600289B RID: 10395
		RVOLayer CollidesWith { get; set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600289C RID: 10396
		// (set) Token: 0x0600289D RID: 10397
		bool DebugDraw { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600289E RID: 10398
		[Obsolete]
		List<ObstacleVertex> NeighbourObstacles { get; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600289F RID: 10399
		// (set) Token: 0x060028A0 RID: 10400
		float Priority { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (set) Token: 0x060028A1 RID: 10401
		Action PreCalculationCallback { set; }

		// Token: 0x060028A2 RID: 10402
		void SetCollisionNormal(Vector2 normal);

		// Token: 0x060028A3 RID: 10403
		void ForceSetVelocity(Vector2 velocity);
	}
}
