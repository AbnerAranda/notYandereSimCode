using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000528 RID: 1320
	[AddComponentMenu("Pathfinding/AI/AIPath (2D,3D)")]
	public class AIPath : AIBase, IAstarAI
	{
		// Token: 0x0600212A RID: 8490 RVA: 0x0018D876 File Offset: 0x0018BA76
		public override void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			if (clearPath)
			{
				this.interpolator.SetPath(null);
			}
			this.reachedEndOfPath = false;
			base.Teleport(newPosition, clearPath);
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x0018D898 File Offset: 0x0018BA98
		public float remainingDistance
		{
			get
			{
				if (!this.interpolator.valid)
				{
					return float.PositiveInfinity;
				}
				return this.interpolator.remainingDistance + this.movementPlane.ToPlane(this.interpolator.position - base.position).magnitude;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x0018D8ED File Offset: 0x0018BAED
		// (set) Token: 0x0600212D RID: 8493 RVA: 0x0018D8F5 File Offset: 0x0018BAF5
		public bool reachedEndOfPath { get; protected set; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x0018D8FE File Offset: 0x0018BAFE
		public bool hasPath
		{
			get
			{
				return this.interpolator.valid;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0018D90B File Offset: 0x0018BB0B
		public bool pathPending
		{
			get
			{
				return this.waitingForPathCalculation;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x0018D913 File Offset: 0x0018BB13
		public Vector3 steeringTarget
		{
			get
			{
				if (!this.interpolator.valid)
				{
					return base.position;
				}
				return this.interpolator.position;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x0018D934 File Offset: 0x0018BB34
		// (set) Token: 0x06002132 RID: 8498 RVA: 0x0018D93C File Offset: 0x0018BB3C
		float IAstarAI.maxSpeed
		{
			get
			{
				return this.maxSpeed;
			}
			set
			{
				this.maxSpeed = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x0018D945 File Offset: 0x0018BB45
		// (set) Token: 0x06002134 RID: 8500 RVA: 0x0018D94D File Offset: 0x0018BB4D
		bool IAstarAI.canSearch
		{
			get
			{
				return this.canSearch;
			}
			set
			{
				this.canSearch = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x0018D956 File Offset: 0x0018BB56
		// (set) Token: 0x06002136 RID: 8502 RVA: 0x0018D95E File Offset: 0x0018BB5E
		bool IAstarAI.canMove
		{
			get
			{
				return this.canMove;
			}
			set
			{
				this.canMove = value;
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0018D967 File Offset: 0x0018BB67
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = null;
			this.interpolator.SetPath(null);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnTargetReached()
		{
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0018D998 File Offset: 0x0018BB98
		protected override void OnPathComplete(Path newPath)
		{
			ABPath abpath = newPath as ABPath;
			if (abpath == null)
			{
				throw new Exception("This function only handles ABPaths, do not use special path types");
			}
			this.waitingForPathCalculation = false;
			abpath.Claim(this);
			if (abpath.error)
			{
				abpath.Release(this, false);
				return;
			}
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = abpath;
			if (this.path.vectorPath.Count == 1)
			{
				this.path.vectorPath.Add(this.path.vectorPath[0]);
			}
			this.interpolator.SetPath(this.path.vectorPath);
			ITransformedGraph transformedGraph = AstarData.GetGraph(this.path.path[0]) as ITransformedGraph;
			this.movementPlane = ((transformedGraph != null) ? transformedGraph.transform : (this.rotationIn2D ? new GraphTransform(Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-90f, 270f, 90f), Vector3.one)) : GraphTransform.identityTransform));
			this.reachedEndOfPath = false;
			this.interpolator.MoveToLocallyClosestPoint((this.GetFeetPosition() + abpath.originalStartPoint) * 0.5f, true, true);
			this.interpolator.MoveToLocallyClosestPoint(this.GetFeetPosition(), true, true);
			this.interpolator.MoveToCircleIntersection2D(base.position, this.pickNextWaypointDist, this.movementPlane);
			if (this.remainingDistance <= this.endReachedDistance)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0018DB20 File Offset: 0x0018BD20
		protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			float num = this.maxAcceleration;
			if (num < 0f)
			{
				num *= -this.maxSpeed;
			}
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			Vector3 simulatedPosition = this.simulatedPosition;
			this.interpolator.MoveToCircleIntersection2D(simulatedPosition, this.pickNextWaypointDist, this.movementPlane);
			Vector2 deltaPosition = this.movementPlane.ToPlane(this.steeringTarget - simulatedPosition);
			float num2 = deltaPosition.magnitude + Mathf.Max(0f, this.interpolator.remainingDistance);
			bool reachedEndOfPath = this.reachedEndOfPath;
			this.reachedEndOfPath = (num2 <= this.endReachedDistance && this.interpolator.valid);
			if (!reachedEndOfPath && this.reachedEndOfPath)
			{
				this.OnTargetReached();
			}
			Vector2 vector = this.movementPlane.ToPlane(this.simulatedRotation * (this.rotationIn2D ? Vector3.up : Vector3.forward));
			float num3;
			if (this.interpolator.valid && !base.isStopped)
			{
				num3 = ((num2 < this.slowdownDistance) ? Mathf.Sqrt(num2 / this.slowdownDistance) : 1f);
				if (this.reachedEndOfPath && this.whenCloseToDestination == CloseToDestinationMode.Stop)
				{
					this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, num * deltaTime);
				}
				else
				{
					this.velocity2D += MovementUtilities.CalculateAccelerationToReachPoint(deltaPosition, deltaPosition.normalized * this.maxSpeed, this.velocity2D, num, this.rotationSpeed, this.maxSpeed, vector) * deltaTime;
				}
			}
			else
			{
				num3 = 1f;
				this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, num * deltaTime);
			}
			this.velocity2D = MovementUtilities.ClampVelocity(this.velocity2D, this.maxSpeed, num3, this.slowWhenNotFacingTarget, vector);
			base.ApplyGravity(deltaTime);
			if (this.rvoController != null && this.rvoController.enabled)
			{
				Vector3 pos = simulatedPosition + this.movementPlane.ToWorld(Vector2.ClampMagnitude(this.velocity2D, num2), 0f);
				this.rvoController.SetTarget(pos, this.velocity2D.magnitude, this.maxSpeed);
			}
			Vector2 p = this.lastDeltaPosition = base.CalculateDeltaToMoveThisFrame(this.movementPlane.ToPlane(simulatedPosition), num2, deltaTime);
			nextPosition = simulatedPosition + this.movementPlane.ToWorld(p, this.verticalVelocity * this.lastDeltaTime);
			this.CalculateNextRotation(num3, out nextRotation);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0018DDD8 File Offset: 0x0018BFD8
		protected virtual void CalculateNextRotation(float slowdown, out Quaternion nextRotation)
		{
			if (this.lastDeltaTime > 1E-05f)
			{
				Vector2 direction;
				if (this.rvoController != null && this.rvoController.enabled)
				{
					Vector2 b = this.lastDeltaPosition / this.lastDeltaTime;
					direction = Vector2.Lerp(this.velocity2D, b, 4f * b.magnitude / (this.maxSpeed + 0.0001f));
				}
				else
				{
					direction = this.velocity2D;
				}
				float num = this.rotationSpeed * Mathf.Max(0f, (slowdown - 0.3f) / 0.7f);
				nextRotation = base.SimulateRotationTowards(direction, num * this.lastDeltaTime);
				return;
			}
			nextRotation = base.rotation;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0018DE94 File Offset: 0x0018C094
		protected override Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			if (this.constrainInsideGraph)
			{
				AIPath.cachedNNConstraint.tags = this.seeker.traversableTags;
				AIPath.cachedNNConstraint.graphMask = this.seeker.graphMask;
				AIPath.cachedNNConstraint.distanceXZ = true;
				Vector3 position2 = AstarPath.active.GetNearest(position, AIPath.cachedNNConstraint).position;
				Vector2 vector = this.movementPlane.ToPlane(position2 - position);
				float sqrMagnitude = vector.sqrMagnitude;
				if (sqrMagnitude > 1.00000011E-06f)
				{
					this.velocity2D -= vector * Vector2.Dot(vector, this.velocity2D) / sqrMagnitude;
					if (this.rvoController != null && this.rvoController.enabled)
					{
						this.rvoController.SetCollisionNormal(vector);
					}
					positionChanged = true;
					return position + this.movementPlane.ToWorld(vector, 0f);
				}
			}
			positionChanged = false;
			return position;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0018DF8E File Offset: 0x0018C18E
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			base.OnUpgradeSerializedData(version, unityThread);
			if (version < 1)
			{
				this.rotationSpeed *= 90f;
			}
			return 2;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0018DFB0 File Offset: 0x0018C1B0
		[Obsolete("When unifying the interfaces for different movement scripts, this property has been renamed to reachedEndOfPath.  [AstarUpgradable: 'TargetReached' -> 'reachedEndOfPath']")]
		public bool TargetReached
		{
			get
			{
				return this.reachedEndOfPath;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0018DFB8 File Offset: 0x0018C1B8
		// (set) Token: 0x06002140 RID: 8512 RVA: 0x0018DFC6 File Offset: 0x0018C1C6
		[Obsolete("This field has been renamed to #rotationSpeed and is now in degrees per second instead of a damping factor")]
		public float turningSpeed
		{
			get
			{
				return this.rotationSpeed / 90f;
			}
			set
			{
				this.rotationSpeed = value * 90f;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0018D934 File Offset: 0x0018BB34
		// (set) Token: 0x06002142 RID: 8514 RVA: 0x0018D93C File Offset: 0x0018BB3C
		[Obsolete("This member has been deprecated. Use 'maxSpeed' instead. [AstarUpgradable: 'speed' -> 'maxSpeed']")]
		public float speed
		{
			get
			{
				return this.maxSpeed;
			}
			set
			{
				this.maxSpeed = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0018DFD8 File Offset: 0x0018C1D8
		[Obsolete("Only exists for compatibility reasons. Use desiredVelocity or steeringTarget instead.")]
		public Vector3 targetDirection
		{
			get
			{
				return (this.steeringTarget - this.tr.position).normalized;
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0018E003 File Offset: 0x0018C203
		[Obsolete("This method no longer calculates the velocity. Use the desiredVelocity property instead")]
		public Vector3 CalculateVelocity(Vector3 position)
		{
			return base.desiredVelocity;
		}

		// Token: 0x04003F38 RID: 16184
		public float maxAcceleration = -2.5f;

		// Token: 0x04003F39 RID: 16185
		[FormerlySerializedAs("turningSpeed")]
		public float rotationSpeed = 360f;

		// Token: 0x04003F3A RID: 16186
		public float slowdownDistance = 0.6f;

		// Token: 0x04003F3B RID: 16187
		public float pickNextWaypointDist = 2f;

		// Token: 0x04003F3C RID: 16188
		public float endReachedDistance = 0.2f;

		// Token: 0x04003F3D RID: 16189
		public bool alwaysDrawGizmos;

		// Token: 0x04003F3E RID: 16190
		public bool slowWhenNotFacingTarget = true;

		// Token: 0x04003F3F RID: 16191
		public CloseToDestinationMode whenCloseToDestination;

		// Token: 0x04003F40 RID: 16192
		public bool constrainInsideGraph;

		// Token: 0x04003F41 RID: 16193
		protected Path path;

		// Token: 0x04003F42 RID: 16194
		public PathInterpolator interpolator = new PathInterpolator();

		// Token: 0x04003F44 RID: 16196
		private static NNConstraint cachedNNConstraint = NNConstraint.Default;
	}
}
