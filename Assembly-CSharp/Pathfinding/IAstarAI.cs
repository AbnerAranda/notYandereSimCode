using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000529 RID: 1321
	public interface IAstarAI
	{
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002147 RID: 8519
		Vector3 position { get; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002148 RID: 8520
		Quaternion rotation { get; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06002149 RID: 8521
		// (set) Token: 0x0600214A RID: 8522
		float maxSpeed { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600214B RID: 8523
		Vector3 velocity { get; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600214C RID: 8524
		Vector3 desiredVelocity { get; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600214D RID: 8525
		float remainingDistance { get; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600214E RID: 8526
		bool reachedEndOfPath { get; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600214F RID: 8527
		// (set) Token: 0x06002150 RID: 8528
		Vector3 destination { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002151 RID: 8529
		// (set) Token: 0x06002152 RID: 8530
		bool canSearch { get; set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06002153 RID: 8531
		// (set) Token: 0x06002154 RID: 8532
		bool canMove { get; set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002155 RID: 8533
		bool hasPath { get; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06002156 RID: 8534
		bool pathPending { get; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002157 RID: 8535
		// (set) Token: 0x06002158 RID: 8536
		bool isStopped { get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002159 RID: 8537
		Vector3 steeringTarget { get; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600215A RID: 8538
		// (set) Token: 0x0600215B RID: 8539
		Action onSearchPath { get; set; }

		// Token: 0x0600215C RID: 8540
		void SearchPath();

		// Token: 0x0600215D RID: 8541
		void SetPath(Path path);

		// Token: 0x0600215E RID: 8542
		void Teleport(Vector3 newPosition, bool clearPath = true);

		// Token: 0x0600215F RID: 8543
		void Move(Vector3 deltaPosition);

		// Token: 0x06002160 RID: 8544
		void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation);

		// Token: 0x06002161 RID: 8545
		void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation);
	}
}
