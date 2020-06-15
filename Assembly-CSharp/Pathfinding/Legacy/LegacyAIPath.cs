using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Legacy
{
	// Token: 0x020005BC RID: 1468
	[RequireComponent(typeof(Seeker))]
	[AddComponentMenu("Pathfinding/Legacy/AI/Legacy AIPath (3D)")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_legacy_1_1_legacy_a_i_path.php")]
	public class LegacyAIPath : AIPath
	{
		// Token: 0x060027B5 RID: 10165 RVA: 0x001B3A43 File Offset: 0x001B1C43
		protected override void Awake()
		{
			base.Awake();
			if (this.rvoController != null)
			{
				if (this.rvoController is LegacyRVOController)
				{
					(this.rvoController as LegacyRVOController).enableRotation = false;
					return;
				}
				Debug.LogError("The LegacyAIPath component only works with the legacy RVOController, not the latest one. Please upgrade this component", this);
			}
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x001B3A84 File Offset: 0x001B1C84
		protected override void OnPathComplete(Path _p)
		{
			ABPath abpath = _p as ABPath;
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
			this.currentWaypointIndex = 0;
			base.reachedEndOfPath = false;
			if (this.closestOnPathCheck)
			{
				Vector3 vector = (Time.time - this.lastFoundWaypointTime < 0.3f) ? this.lastFoundWaypointPosition : abpath.originalStartPoint;
				Vector3 vector2 = this.GetFeetPosition() - vector;
				float magnitude = vector2.magnitude;
				vector2 /= magnitude;
				int num = (int)(magnitude / this.pickNextWaypointDist);
				for (int i = 0; i <= num; i++)
				{
					this.CalculateVelocity(vector);
					vector += vector2;
				}
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x001B3B64 File Offset: 0x001B1D64
		protected override void Update()
		{
			if (!this.canMove)
			{
				return;
			}
			Vector3 vector = this.CalculateVelocity(this.GetFeetPosition());
			this.RotateTowards(this.targetDirection);
			if (this.rvoController != null)
			{
				this.rvoController.Move(vector);
				return;
			}
			if (this.controller != null)
			{
				this.controller.SimpleMove(vector);
				return;
			}
			if (this.rigid != null)
			{
				this.rigid.AddForce(vector);
				return;
			}
			this.tr.Translate(vector * Time.deltaTime, Space.World);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x001B3BFC File Offset: 0x001B1DFC
		protected float XZSqrMagnitude(Vector3 a, Vector3 b)
		{
			float num = b.x - a.x;
			float num2 = b.z - a.z;
			return num * num + num2 * num2;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x001B3C2C File Offset: 0x001B1E2C
		protected new Vector3 CalculateVelocity(Vector3 currentPosition)
		{
			if (this.path == null || this.path.vectorPath == null || this.path.vectorPath.Count == 0)
			{
				return Vector3.zero;
			}
			List<Vector3> vectorPath = this.path.vectorPath;
			if (vectorPath.Count == 1)
			{
				vectorPath.Insert(0, currentPosition);
			}
			if (this.currentWaypointIndex >= vectorPath.Count)
			{
				this.currentWaypointIndex = vectorPath.Count - 1;
			}
			if (this.currentWaypointIndex <= 1)
			{
				this.currentWaypointIndex = 1;
			}
			while (this.currentWaypointIndex < vectorPath.Count - 1 && this.XZSqrMagnitude(vectorPath[this.currentWaypointIndex], currentPosition) < this.pickNextWaypointDist * this.pickNextWaypointDist)
			{
				this.lastFoundWaypointPosition = currentPosition;
				this.lastFoundWaypointTime = Time.time;
				this.currentWaypointIndex++;
			}
			Vector3 vector = vectorPath[this.currentWaypointIndex] - vectorPath[this.currentWaypointIndex - 1];
			vector = this.CalculateTargetPoint(currentPosition, vectorPath[this.currentWaypointIndex - 1], vectorPath[this.currentWaypointIndex]) - currentPosition;
			vector.y = 0f;
			float magnitude = vector.magnitude;
			float num = Mathf.Clamp01(magnitude / this.slowdownDistance);
			this.targetDirection = vector;
			if (this.currentWaypointIndex == vectorPath.Count - 1 && magnitude <= this.endReachedDistance)
			{
				if (!base.reachedEndOfPath)
				{
					base.reachedEndOfPath = true;
					this.OnTargetReached();
				}
				return Vector3.zero;
			}
			Vector3 forward = this.tr.forward;
			float a = Vector3.Dot(vector.normalized, forward);
			float num2 = this.maxSpeed * Mathf.Max(a, this.minMoveScale) * num;
			if (Time.deltaTime > 0f)
			{
				num2 = Mathf.Clamp(num2, 0f, magnitude / (Time.deltaTime * 2f));
			}
			return forward * num2;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x001B3E10 File Offset: 0x001B2010
		protected void RotateTowards(Vector3 dir)
		{
			if (dir == Vector3.zero)
			{
				return;
			}
			Quaternion quaternion = this.tr.rotation;
			Quaternion b = Quaternion.LookRotation(dir);
			Vector3 eulerAngles = Quaternion.Slerp(quaternion, b, base.turningSpeed * Time.deltaTime).eulerAngles;
			eulerAngles.z = 0f;
			eulerAngles.x = 0f;
			quaternion = Quaternion.Euler(eulerAngles);
			this.tr.rotation = quaternion;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x001B3E88 File Offset: 0x001B2088
		protected Vector3 CalculateTargetPoint(Vector3 p, Vector3 a, Vector3 b)
		{
			a.y = p.y;
			b.y = p.y;
			float magnitude = (a - b).magnitude;
			if (magnitude == 0f)
			{
				return a;
			}
			float num = Mathf.Clamp01(VectorMath.ClosestPointOnLineFactor(a, b, p));
			float magnitude2 = ((b - a) * num + a - p).magnitude;
			float num2 = Mathf.Clamp(this.forwardLook - magnitude2, 0f, this.forwardLook) / magnitude;
			num2 = Mathf.Clamp(num2 + num, 0f, 1f);
			return (b - a) * num2 + a;
		}

		// Token: 0x040042AA RID: 17066
		public float forwardLook = 1f;

		// Token: 0x040042AB RID: 17067
		public bool closestOnPathCheck = true;

		// Token: 0x040042AC RID: 17068
		protected float minMoveScale = 0.05f;

		// Token: 0x040042AD RID: 17069
		protected int currentWaypointIndex;

		// Token: 0x040042AE RID: 17070
		protected Vector3 lastFoundWaypointPosition;

		// Token: 0x040042AF RID: 17071
		protected float lastFoundWaypointTime = -9999f;

		// Token: 0x040042B0 RID: 17072
		protected new Vector3 targetDirection;
	}
}
