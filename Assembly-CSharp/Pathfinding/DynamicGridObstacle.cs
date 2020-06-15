using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000571 RID: 1393
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_dynamic_grid_obstacle.php")]
	public class DynamicGridObstacle : GraphModifier
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x0019C4C8 File Offset: 0x0019A6C8
		private Bounds bounds
		{
			get
			{
				if (this.coll != null)
				{
					return this.coll.bounds;
				}
				Bounds bounds = this.coll2D.bounds;
				bounds.extents += new Vector3(0f, 0f, 10000f);
				return bounds;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x0019C522 File Offset: 0x0019A722
		private bool colliderEnabled
		{
			get
			{
				if (!(this.coll != null))
				{
					return this.coll2D.enabled;
				}
				return this.coll.enabled;
			}
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x0019C54C File Offset: 0x0019A74C
		protected override void Awake()
		{
			base.Awake();
			this.coll = base.GetComponent<Collider>();
			this.coll2D = base.GetComponent<Collider2D>();
			this.tr = base.transform;
			if (this.coll == null && this.coll2D == null)
			{
				throw new Exception("A collider or 2D collider must be attached to the GameObject(" + base.gameObject.name + ") for the DynamicGridObstacle to work");
			}
			this.prevBounds = this.bounds;
			this.prevRotation = this.tr.rotation;
			this.prevEnabled = false;
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x0019C5E3 File Offset: 0x0019A7E3
		public override void OnPostScan()
		{
			this.prevEnabled = this.colliderEnabled;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x0019C5F4 File Offset: 0x0019A7F4
		private void Update()
		{
			if (this.coll == null && this.coll2D == null)
			{
				Debug.LogError("Removed collider from DynamicGridObstacle", this);
				base.enabled = false;
				return;
			}
			if (AstarPath.active == null || AstarPath.active.isScanning || Time.realtimeSinceStartup - this.lastCheckTime < this.checkTime || !Application.isPlaying)
			{
				return;
			}
			this.lastCheckTime = Time.realtimeSinceStartup;
			if (this.colliderEnabled)
			{
				Bounds bounds = this.bounds;
				Quaternion rotation = this.tr.rotation;
				Vector3 vector = this.prevBounds.min - bounds.min;
				Vector3 vector2 = this.prevBounds.max - bounds.max;
				float num = bounds.extents.magnitude * Quaternion.Angle(this.prevRotation, rotation) * 0.0174532924f;
				if (vector.sqrMagnitude > this.updateError * this.updateError || vector2.sqrMagnitude > this.updateError * this.updateError || num > this.updateError || !this.prevEnabled)
				{
					this.DoUpdateGraphs();
					return;
				}
			}
			else if (this.prevEnabled)
			{
				this.DoUpdateGraphs();
			}
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0019C738 File Offset: 0x0019A938
		protected override void OnDisable()
		{
			base.OnDisable();
			if (AstarPath.active != null && Application.isPlaying)
			{
				GraphUpdateObject ob = new GraphUpdateObject(this.prevBounds);
				AstarPath.active.UpdateGraphs(ob);
				this.prevEnabled = false;
			}
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x0019C780 File Offset: 0x0019A980
		public void DoUpdateGraphs()
		{
			if (this.coll == null && this.coll2D == null)
			{
				return;
			}
			if (!this.colliderEnabled)
			{
				AstarPath.active.UpdateGraphs(this.prevBounds);
			}
			else
			{
				Bounds bounds = this.bounds;
				Bounds bounds2 = bounds;
				bounds2.Encapsulate(this.prevBounds);
				if (DynamicGridObstacle.BoundsVolume(bounds2) < DynamicGridObstacle.BoundsVolume(bounds) + DynamicGridObstacle.BoundsVolume(this.prevBounds))
				{
					AstarPath.active.UpdateGraphs(bounds2);
				}
				else
				{
					AstarPath.active.UpdateGraphs(this.prevBounds);
					AstarPath.active.UpdateGraphs(bounds);
				}
				this.prevBounds = bounds;
			}
			this.prevEnabled = this.colliderEnabled;
			this.prevRotation = this.tr.rotation;
			this.lastCheckTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x0019C84B File Offset: 0x0019AA4B
		private static float BoundsVolume(Bounds b)
		{
			return Math.Abs(b.size.x * b.size.y * b.size.z);
		}

		// Token: 0x040040FB RID: 16635
		private Collider coll;

		// Token: 0x040040FC RID: 16636
		private Collider2D coll2D;

		// Token: 0x040040FD RID: 16637
		private Transform tr;

		// Token: 0x040040FE RID: 16638
		public float updateError = 1f;

		// Token: 0x040040FF RID: 16639
		public float checkTime = 0.2f;

		// Token: 0x04004100 RID: 16640
		private Bounds prevBounds;

		// Token: 0x04004101 RID: 16641
		private Quaternion prevRotation;

		// Token: 0x04004102 RID: 16642
		private bool prevEnabled;

		// Token: 0x04004103 RID: 16643
		private float lastCheckTime = -9999f;
	}
}
