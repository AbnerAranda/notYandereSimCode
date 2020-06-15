using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005EC RID: 1516
	public class PathInterpolator
	{
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x001C11F0 File Offset: 0x001BF3F0
		public virtual Vector3 position
		{
			get
			{
				float t = (this.currentSegmentLength > 0.0001f) ? ((this.currentDistance - this.distanceToSegmentStart) / this.currentSegmentLength) : 0f;
				return Vector3.Lerp(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], t);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x001C1250 File Offset: 0x001BF450
		public Vector3 tangent
		{
			get
			{
				return this.path[this.segmentIndex + 1] - this.path[this.segmentIndex];
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x001C127B File Offset: 0x001BF47B
		// (set) Token: 0x06002972 RID: 10610 RVA: 0x001C128A File Offset: 0x001BF48A
		public float remainingDistance
		{
			get
			{
				return this.totalDistance - this.distance;
			}
			set
			{
				this.distance = this.totalDistance - value;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x001C129A File Offset: 0x001BF49A
		// (set) Token: 0x06002974 RID: 10612 RVA: 0x001C12A4 File Offset: 0x001BF4A4
		public float distance
		{
			get
			{
				return this.currentDistance;
			}
			set
			{
				this.currentDistance = value;
				while (this.currentDistance < this.distanceToSegmentStart)
				{
					if (this.segmentIndex <= 0)
					{
						break;
					}
					this.PrevSegment();
				}
				while (this.currentDistance > this.distanceToSegmentStart + this.currentSegmentLength && this.segmentIndex < this.path.Count - 2)
				{
					this.NextSegment();
				}
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x001C1309 File Offset: 0x001BF509
		// (set) Token: 0x06002976 RID: 10614 RVA: 0x001C1311 File Offset: 0x001BF511
		public int segmentIndex { get; private set; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x001C131A File Offset: 0x001BF51A
		public bool valid
		{
			get
			{
				return this.path != null;
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x001C1328 File Offset: 0x001BF528
		public void SetPath(List<Vector3> path)
		{
			this.path = path;
			this.currentDistance = 0f;
			this.segmentIndex = 0;
			this.distanceToSegmentStart = 0f;
			if (path == null)
			{
				this.totalDistance = float.PositiveInfinity;
				this.currentSegmentLength = float.PositiveInfinity;
				return;
			}
			if (path.Count < 2)
			{
				throw new ArgumentException("Path must have a length of at least 2");
			}
			this.currentSegmentLength = (path[1] - path[0]).magnitude;
			this.totalDistance = 0f;
			Vector3 b = path[0];
			for (int i = 1; i < path.Count; i++)
			{
				Vector3 vector = path[i];
				this.totalDistance += (vector - b).magnitude;
				b = vector;
			}
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x001C13F4 File Offset: 0x001BF5F4
		public void MoveToSegment(int index, float fractionAlongSegment)
		{
			if (this.path == null)
			{
				return;
			}
			if (index < 0 || index >= this.path.Count - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			while (this.segmentIndex > index)
			{
				this.PrevSegment();
			}
			while (this.segmentIndex < index)
			{
				this.NextSegment();
			}
			this.distance = this.distanceToSegmentStart + Mathf.Clamp01(fractionAlongSegment) * this.currentSegmentLength;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x001C1464 File Offset: 0x001BF664
		public void MoveToClosestPoint(Vector3 point)
		{
			if (this.path == null)
			{
				return;
			}
			float num = float.PositiveInfinity;
			float fractionAlongSegment = 0f;
			int index = 0;
			for (int i = 0; i < this.path.Count - 1; i++)
			{
				float num2 = VectorMath.ClosestPointOnLineFactor(this.path[i], this.path[i + 1], point);
				Vector3 b = Vector3.Lerp(this.path[i], this.path[i + 1], num2);
				float sqrMagnitude = (point - b).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					fractionAlongSegment = num2;
					index = i;
				}
			}
			this.MoveToSegment(index, fractionAlongSegment);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x001C1510 File Offset: 0x001BF710
		public void MoveToLocallyClosestPoint(Vector3 point, bool allowForwards = true, bool allowBackwards = true)
		{
			if (this.path == null)
			{
				return;
			}
			while (allowForwards && this.segmentIndex < this.path.Count - 2)
			{
				if ((this.path[this.segmentIndex + 1] - point).sqrMagnitude > (this.path[this.segmentIndex] - point).sqrMagnitude)
				{
					break;
				}
				this.NextSegment();
			}
			while (allowBackwards && this.segmentIndex > 0 && (this.path[this.segmentIndex - 1] - point).sqrMagnitude <= (this.path[this.segmentIndex] - point).sqrMagnitude)
			{
				this.PrevSegment();
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = float.PositiveInfinity;
			float num4 = float.PositiveInfinity;
			if (this.segmentIndex > 0)
			{
				num = VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex - 1], this.path[this.segmentIndex], point);
				num3 = (Vector3.Lerp(this.path[this.segmentIndex - 1], this.path[this.segmentIndex], num) - point).sqrMagnitude;
			}
			if (this.segmentIndex < this.path.Count - 1)
			{
				num2 = VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], point);
				num4 = (Vector3.Lerp(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], num2) - point).sqrMagnitude;
			}
			if (num3 < num4)
			{
				this.MoveToSegment(this.segmentIndex - 1, num);
				return;
			}
			this.MoveToSegment(this.segmentIndex, num2);
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x001C1704 File Offset: 0x001BF904
		public void MoveToCircleIntersection2D(Vector3 circleCenter3D, float radius, IMovementPlane transform)
		{
			if (this.path == null)
			{
				return;
			}
			while (this.segmentIndex < this.path.Count - 2 && VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], circleCenter3D) > 1f)
			{
				this.NextSegment();
			}
			Vector2 vector = transform.ToPlane(circleCenter3D);
			while (this.segmentIndex < this.path.Count - 2 && (transform.ToPlane(this.path[this.segmentIndex + 1]) - vector).sqrMagnitude <= radius * radius)
			{
				this.NextSegment();
			}
			float fractionAlongSegment = VectorMath.LineCircleIntersectionFactor(vector, transform.ToPlane(this.path[this.segmentIndex]), transform.ToPlane(this.path[this.segmentIndex + 1]), radius);
			this.MoveToSegment(this.segmentIndex, fractionAlongSegment);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x001C180C File Offset: 0x001BFA0C
		protected virtual void PrevSegment()
		{
			int segmentIndex = this.segmentIndex;
			this.segmentIndex = segmentIndex - 1;
			this.currentSegmentLength = (this.path[this.segmentIndex + 1] - this.path[this.segmentIndex]).magnitude;
			this.distanceToSegmentStart -= this.currentSegmentLength;
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x001C1874 File Offset: 0x001BFA74
		protected virtual void NextSegment()
		{
			int segmentIndex = this.segmentIndex;
			this.segmentIndex = segmentIndex + 1;
			this.distanceToSegmentStart += this.currentSegmentLength;
			this.currentSegmentLength = (this.path[this.segmentIndex + 1] - this.path[this.segmentIndex]).magnitude;
		}

		// Token: 0x040043E3 RID: 17379
		private List<Vector3> path;

		// Token: 0x040043E4 RID: 17380
		private float distanceToSegmentStart;

		// Token: 0x040043E5 RID: 17381
		private float currentDistance;

		// Token: 0x040043E6 RID: 17382
		private float currentSegmentLength = float.PositiveInfinity;

		// Token: 0x040043E7 RID: 17383
		private float totalDistance = float.PositiveInfinity;
	}
}
