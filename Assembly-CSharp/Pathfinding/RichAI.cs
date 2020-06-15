using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Examples;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200052A RID: 1322
	[AddComponentMenu("Pathfinding/AI/RichAI (3D, for navmesh)")]
	public class RichAI : AIBase, IAstarAI
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0018E074 File Offset: 0x0018C274
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x0018E07C File Offset: 0x0018C27C
		public bool traversingOffMeshLink { get; protected set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0018E085 File Offset: 0x0018C285
		public float remainingDistance
		{
			get
			{
				return this.distanceToSteeringTarget + Vector3.Distance(this.steeringTarget, this.richPath.Endpoint);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x0018E0A4 File Offset: 0x0018C2A4
		public bool reachedEndOfPath
		{
			get
			{
				return this.approachingPathEndpoint && this.distanceToSteeringTarget < this.endReachedDistance;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x0018E0BE File Offset: 0x0018C2BE
		public bool hasPath
		{
			get
			{
				return this.richPath.GetCurrentPart() != null;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x0018E0CE File Offset: 0x0018C2CE
		public bool pathPending
		{
			get
			{
				return this.waitingForPathCalculation || this.delayUpdatePath;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x0018E0E0 File Offset: 0x0018C2E0
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x0018E0E8 File Offset: 0x0018C2E8
		public Vector3 steeringTarget { get; protected set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0018D934 File Offset: 0x0018BB34
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x0018D93C File Offset: 0x0018BB3C
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

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x0018D945 File Offset: 0x0018BB45
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x0018D94D File Offset: 0x0018BB4D
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

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x0018D956 File Offset: 0x0018BB56
		// (set) Token: 0x0600216F RID: 8559 RVA: 0x0018D95E File Offset: 0x0018BB5E
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

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x0018E0F1 File Offset: 0x0018C2F1
		Vector3 IAstarAI.position
		{
			get
			{
				return this.tr.position;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x0018E0FE File Offset: 0x0018C2FE
		public bool approachingPartEndpoint
		{
			get
			{
				return this.lastCorner && this.nextCorners.Count == 1;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x0018E118 File Offset: 0x0018C318
		public bool approachingPathEndpoint
		{
			get
			{
				return this.approachingPartEndpoint && this.richPath.IsLastPart;
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0018E130 File Offset: 0x0018C330
		public override void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			NNInfo nninfo = (AstarPath.active != null) ? AstarPath.active.GetNearest(newPosition) : default(NNInfo);
			float elevation;
			this.movementPlane.ToPlane(newPosition, out elevation);
			newPosition = this.movementPlane.ToWorld(this.movementPlane.ToPlane((nninfo.node != null) ? nninfo.position : newPosition), elevation);
			if (clearPath)
			{
				this.richPath.Clear();
			}
			base.Teleport(newPosition, clearPath);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0018E1B0 File Offset: 0x0018C3B0
		protected override void OnDisable()
		{
			base.OnDisable();
			this.lastCorner = false;
			this.distanceToSteeringTarget = float.PositiveInfinity;
			this.traversingOffMeshLink = false;
			this.delayUpdatePath = false;
			base.StopAllCoroutines();
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x0018E1DE File Offset: 0x0018C3DE
		protected override bool shouldRecalculatePath
		{
			get
			{
				return base.shouldRecalculatePath && !this.traversingOffMeshLink;
			}
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0018E1F3 File Offset: 0x0018C3F3
		public override void SearchPath()
		{
			if (this.traversingOffMeshLink)
			{
				this.delayUpdatePath = true;
				return;
			}
			base.SearchPath();
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0018E20C File Offset: 0x0018C40C
		protected override void OnPathComplete(Path p)
		{
			this.waitingForPathCalculation = false;
			p.Claim(this);
			if (p.error)
			{
				p.Release(this, false);
				return;
			}
			if (this.traversingOffMeshLink)
			{
				this.delayUpdatePath = true;
			}
			else
			{
				this.richPath.Initialize(this.seeker, p, true, this.funnelSimplification);
				RichFunnel richFunnel = this.richPath.GetCurrentPart() as RichFunnel;
				if (richFunnel != null)
				{
					if (this.updatePosition)
					{
						this.simulatedPosition = this.tr.position;
					}
					Vector2 b = this.movementPlane.ToPlane(this.UpdateTarget(richFunnel));
					if (this.lastCorner && this.nextCorners.Count == 1)
					{
						this.steeringTarget = this.nextCorners[0];
						Vector2 a = this.movementPlane.ToPlane(this.steeringTarget);
						this.distanceToSteeringTarget = (a - b).magnitude;
						if (this.distanceToSteeringTarget <= this.endReachedDistance)
						{
							this.NextPart();
						}
					}
				}
			}
			p.Release(this, false);
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0018E314 File Offset: 0x0018C514
		protected void NextPart()
		{
			if (!this.richPath.CompletedAllParts)
			{
				if (!this.richPath.IsLastPart)
				{
					this.lastCorner = false;
				}
				this.richPath.NextPart();
				if (this.richPath.CompletedAllParts)
				{
					this.OnTargetReached();
				}
			}
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void OnTargetReached()
		{
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0018E360 File Offset: 0x0018C560
		protected virtual Vector3 UpdateTarget(RichFunnel fn)
		{
			this.nextCorners.Clear();
			bool flag;
			Vector3 result = fn.Update(this.simulatedPosition, this.nextCorners, 2, out this.lastCorner, out flag);
			if (flag && !this.waitingForPathCalculation && this.canSearch)
			{
				this.SearchPath();
			}
			return result;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0018E3AC File Offset: 0x0018C5AC
		protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			RichPathPart currentPart = this.richPath.GetCurrentPart();
			if (currentPart is RichSpecial)
			{
				if (!this.traversingOffMeshLink)
				{
					base.StartCoroutine(this.TraverseSpecial(currentPart as RichSpecial));
				}
				nextPosition = (this.steeringTarget = this.simulatedPosition);
				nextRotation = base.rotation;
				return;
			}
			RichFunnel richFunnel = currentPart as RichFunnel;
			if (richFunnel != null && !base.isStopped)
			{
				this.TraverseFunnel(richFunnel, deltaTime, out nextPosition, out nextRotation);
				return;
			}
			this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, this.acceleration * deltaTime);
			this.FinalMovement(this.simulatedPosition, deltaTime, float.PositiveInfinity, 1f, out nextPosition, out nextRotation);
			this.steeringTarget = this.simulatedPosition;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0018E4A4 File Offset: 0x0018C6A4
		private void TraverseFunnel(RichFunnel fn, float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			Vector3 vector = this.UpdateTarget(fn);
			float elevation;
			Vector2 vector2 = this.movementPlane.ToPlane(vector, out elevation);
			if (Time.frameCount % 5 == 0 && this.wallForce > 0f && this.wallDist > 0f)
			{
				this.wallBuffer.Clear();
				fn.FindWalls(this.wallBuffer, this.wallDist);
			}
			this.steeringTarget = this.nextCorners[0];
			Vector2 vector3 = this.movementPlane.ToPlane(this.steeringTarget);
			Vector2 vector4 = vector3 - vector2;
			Vector2 vector5 = VectorMath.Normalize(vector4, out this.distanceToSteeringTarget);
			Vector2 a = this.CalculateWallForce(vector2, elevation, vector5);
			Vector2 targetVelocity;
			if (this.approachingPartEndpoint)
			{
				targetVelocity = ((this.slowdownTime > 0f) ? Vector2.zero : (vector5 * this.maxSpeed));
				a *= Math.Min(this.distanceToSteeringTarget / 0.5f, 1f);
				if (this.distanceToSteeringTarget <= this.endReachedDistance)
				{
					this.NextPart();
				}
			}
			else
			{
				targetVelocity = (((this.nextCorners.Count > 1) ? this.movementPlane.ToPlane(this.nextCorners[1]) : (vector2 + 2f * vector4)) - vector3).normalized * this.maxSpeed;
			}
			Vector2 forwardsVector = this.movementPlane.ToPlane(this.simulatedRotation * (this.rotationIn2D ? Vector3.up : Vector3.forward));
			Vector2 a2 = MovementUtilities.CalculateAccelerationToReachPoint(vector3 - vector2, targetVelocity, this.velocity2D, this.acceleration, this.rotationSpeed, this.maxSpeed, forwardsVector);
			this.velocity2D += (a2 + a * this.wallForce) * deltaTime;
			float num = this.distanceToSteeringTarget + Vector3.Distance(this.steeringTarget, fn.exactEnd);
			float slowdownFactor = (num < this.maxSpeed * this.slowdownTime) ? Mathf.Sqrt(num / (this.maxSpeed * this.slowdownTime)) : 1f;
			this.FinalMovement(vector, deltaTime, num, slowdownFactor, out nextPosition, out nextRotation);
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0018E6E0 File Offset: 0x0018C8E0
		private void FinalMovement(Vector3 position3D, float deltaTime, float distanceToEndOfPath, float slowdownFactor, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			Vector2 forward = this.movementPlane.ToPlane(this.simulatedRotation * (this.rotationIn2D ? Vector3.up : Vector3.forward));
			this.velocity2D = MovementUtilities.ClampVelocity(this.velocity2D, this.maxSpeed, slowdownFactor, this.slowWhenNotFacingTarget, forward);
			base.ApplyGravity(deltaTime);
			if (this.rvoController != null && this.rvoController.enabled)
			{
				Vector3 pos = position3D + this.movementPlane.ToWorld(Vector2.ClampMagnitude(this.velocity2D, distanceToEndOfPath), 0f);
				this.rvoController.SetTarget(pos, this.velocity2D.magnitude, this.maxSpeed);
			}
			Vector2 vector = this.lastDeltaPosition = base.CalculateDeltaToMoveThisFrame(this.movementPlane.ToPlane(position3D), distanceToEndOfPath, deltaTime);
			float num = this.approachingPartEndpoint ? Mathf.Clamp01(1.1f * slowdownFactor - 0.1f) : 1f;
			nextRotation = base.SimulateRotationTowards(vector, this.rotationSpeed * num * deltaTime);
			nextPosition = position3D + this.movementPlane.ToWorld(vector, this.verticalVelocity * deltaTime);
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0018E818 File Offset: 0x0018CA18
		protected override Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			if (this.richPath != null)
			{
				RichFunnel richFunnel = this.richPath.GetCurrentPart() as RichFunnel;
				if (richFunnel != null)
				{
					Vector3 a = richFunnel.ClampToNavmesh(position);
					Vector2 vector = this.movementPlane.ToPlane(a - position);
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
			}
			positionChanged = false;
			return position;
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0018E8E8 File Offset: 0x0018CAE8
		private Vector2 CalculateWallForce(Vector2 position, float elevation, Vector2 directionToTarget)
		{
			if (this.wallForce <= 0f || this.wallDist <= 0f)
			{
				return Vector2.zero;
			}
			float num = 0f;
			float num2 = 0f;
			Vector3 vector = this.movementPlane.ToWorld(position, elevation);
			for (int i = 0; i < this.wallBuffer.Count; i += 2)
			{
				float sqrMagnitude = (VectorMath.ClosestPointOnSegment(this.wallBuffer[i], this.wallBuffer[i + 1], vector) - vector).sqrMagnitude;
				if (sqrMagnitude <= this.wallDist * this.wallDist)
				{
					Vector2 normalized = this.movementPlane.ToPlane(this.wallBuffer[i + 1] - this.wallBuffer[i]).normalized;
					float num3 = Vector2.Dot(directionToTarget, normalized);
					float num4 = 1f - Math.Max(0f, 2f * (sqrMagnitude / (this.wallDist * this.wallDist)) - 1f);
					if (num3 > 0f)
					{
						num2 = Math.Max(num2, num3 * num4);
					}
					else
					{
						num = Math.Max(num, -num3 * num4);
					}
				}
			}
			return new Vector2(directionToTarget.y, -directionToTarget.x) * (num2 - num);
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0018EA3B File Offset: 0x0018CC3B
		protected virtual IEnumerator TraverseSpecial(RichSpecial link)
		{
			this.traversingOffMeshLink = true;
			this.velocity2D = Vector3.zero;
			IEnumerator routine = (this.onTraverseOffMeshLink != null) ? this.onTraverseOffMeshLink(link) : this.TraverseOffMeshLinkFallback(link);
			yield return base.StartCoroutine(routine);
			this.traversingOffMeshLink = false;
			this.NextPart();
			if (this.delayUpdatePath)
			{
				this.delayUpdatePath = false;
				if (this.canSearch)
				{
					this.SearchPath();
				}
			}
			yield break;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0018EA51 File Offset: 0x0018CC51
		protected IEnumerator TraverseOffMeshLinkFallback(RichSpecial link)
		{
			float duration = (this.maxSpeed > 0f) ? (Vector3.Distance(link.second.position, link.first.position) / this.maxSpeed) : 1f;
			float startTime = Time.time;
			for (;;)
			{
				Vector3 vector = Vector3.Lerp(link.first.position, link.second.position, Mathf.InverseLerp(startTime, startTime + duration, Time.time));
				if (this.updatePosition)
				{
					this.tr.position = vector;
				}
				else
				{
					this.simulatedPosition = vector;
				}
				if (Time.time >= startTime + duration)
				{
					break;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0018EA68 File Offset: 0x0018CC68
		protected override void OnDrawGizmos()
		{
			base.OnDrawGizmos();
			if (this.tr != null)
			{
				Gizmos.color = RichAI.GizmoColorPath;
				Vector3 from = base.position;
				for (int i = 0; i < this.nextCorners.Count; i++)
				{
					Gizmos.DrawLine(from, this.nextCorners[i]);
					from = this.nextCorners[i];
				}
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0018EACF File Offset: 0x0018CCCF
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (unityThread && this.animCompatibility != null)
			{
				this.anim = this.animCompatibility;
			}
			return base.OnUpgradeSerializedData(version, unityThread);
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0018EAF6 File Offset: 0x0018CCF6
		[Obsolete("Use SearchPath instead. [AstarUpgradable: 'UpdatePath' -> 'SearchPath']")]
		public void UpdatePath()
		{
			this.SearchPath();
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x0018EAFE File Offset: 0x0018CCFE
		[Obsolete("Use velocity instead (lowercase 'v'). [AstarUpgradable: 'Velocity' -> 'velocity']")]
		public Vector3 Velocity
		{
			get
			{
				return base.velocity;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0018EB06 File Offset: 0x0018CD06
		[Obsolete("Use steeringTarget instead. [AstarUpgradable: 'NextWaypoint' -> 'steeringTarget']")]
		public Vector3 NextWaypoint
		{
			get
			{
				return this.steeringTarget;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x0018EB0E File Offset: 0x0018CD0E
		[Obsolete("Use Vector3.Distance(transform.position, ai.steeringTarget) instead.")]
		public float DistanceToNextWaypoint
		{
			get
			{
				return this.distanceToSteeringTarget;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0018D945 File Offset: 0x0018BB45
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x0018D94D File Offset: 0x0018BB4D
		[Obsolete("Use canSearch instead. [AstarUpgradable: 'repeatedlySearchPaths' -> 'canSearch']")]
		public bool repeatedlySearchPaths
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

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0018EB16 File Offset: 0x0018CD16
		[Obsolete("When unifying the interfaces for different movement scripts, this property has been renamed to reachedEndOfPath (lowercase t).  [AstarUpgradable: 'TargetReached' -> 'reachedEndOfPath']")]
		public bool TargetReached
		{
			get
			{
				return this.reachedEndOfPath;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x0018EB1E File Offset: 0x0018CD1E
		[Obsolete("Use pathPending instead (lowercase 'p'). [AstarUpgradable: 'PathPending' -> 'pathPending']")]
		public bool PathPending
		{
			get
			{
				return this.pathPending;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0018EB26 File Offset: 0x0018CD26
		[Obsolete("Use approachingPartEndpoint (lowercase 'a') instead")]
		public bool ApproachingPartEndpoint
		{
			get
			{
				return this.approachingPartEndpoint;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x0018EB2E File Offset: 0x0018CD2E
		[Obsolete("Use approachingPathEndpoint (lowercase 'a') instead")]
		public bool ApproachingPathEndpoint
		{
			get
			{
				return this.approachingPathEndpoint;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0018EB36 File Offset: 0x0018CD36
		[Obsolete("This property has been renamed to 'traversingOffMeshLink'. [AstarUpgradable: 'TraversingSpecial' -> 'traversingOffMeshLink']")]
		public bool TraversingSpecial
		{
			get
			{
				return this.traversingOffMeshLink;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x0018EB06 File Offset: 0x0018CD06
		[Obsolete("This property has been renamed to steeringTarget")]
		public Vector3 TargetPoint
		{
			get
			{
				return this.steeringTarget;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x0018EB40 File Offset: 0x0018CD40
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x0018EB68 File Offset: 0x0018CD68
		[Obsolete("Use the onTraverseOffMeshLink event or the ... component instead. Setting this value will add a ... component")]
		public Animation anim
		{
			get
			{
				AnimationLinkTraverser component = base.GetComponent<AnimationLinkTraverser>();
				if (!(component != null))
				{
					return null;
				}
				return component.anim;
			}
			set
			{
				this.animCompatibility = null;
				AnimationLinkTraverser animationLinkTraverser = base.GetComponent<AnimationLinkTraverser>();
				if (animationLinkTraverser == null)
				{
					animationLinkTraverser = base.gameObject.AddComponent<AnimationLinkTraverser>();
				}
				animationLinkTraverser.anim = value;
			}
		}

		// Token: 0x04003F45 RID: 16197
		public float acceleration = 5f;

		// Token: 0x04003F46 RID: 16198
		public float rotationSpeed = 360f;

		// Token: 0x04003F47 RID: 16199
		public float slowdownTime = 0.5f;

		// Token: 0x04003F48 RID: 16200
		public float endReachedDistance = 0.01f;

		// Token: 0x04003F49 RID: 16201
		public float wallForce = 3f;

		// Token: 0x04003F4A RID: 16202
		public float wallDist = 1f;

		// Token: 0x04003F4B RID: 16203
		public bool funnelSimplification;

		// Token: 0x04003F4C RID: 16204
		public bool slowWhenNotFacingTarget = true;

		// Token: 0x04003F4D RID: 16205
		public Func<RichSpecial, IEnumerator> onTraverseOffMeshLink;

		// Token: 0x04003F4E RID: 16206
		protected readonly RichPath richPath = new RichPath();

		// Token: 0x04003F4F RID: 16207
		protected bool delayUpdatePath;

		// Token: 0x04003F50 RID: 16208
		protected bool lastCorner;

		// Token: 0x04003F51 RID: 16209
		protected float distanceToSteeringTarget = float.PositiveInfinity;

		// Token: 0x04003F52 RID: 16210
		protected readonly List<Vector3> nextCorners = new List<Vector3>();

		// Token: 0x04003F53 RID: 16211
		protected readonly List<Vector3> wallBuffer = new List<Vector3>();

		// Token: 0x04003F56 RID: 16214
		protected static readonly Color GizmoColorPath = new Color(0.03137255f, 0.305882365f, 0.7607843f);

		// Token: 0x04003F57 RID: 16215
		[FormerlySerializedAs("anim")]
		[SerializeField]
		[HideInInspector]
		private Animation animCompatibility;
	}
}
