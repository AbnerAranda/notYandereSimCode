using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200059E RID: 1438
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_navmesh_add.php")]
	public class NavmeshAdd : NavmeshClipper
	{
		// Token: 0x060026CB RID: 9931 RVA: 0x001AD5FC File Offset: 0x001AB7FC
		public override bool RequiresUpdate()
		{
			return (this.tr.position - this.lastPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotationAndScale && Quaternion.Angle(this.lastRotation, this.tr.rotation) > this.updateRotationDistance);
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x001AD660 File Offset: 0x001AB860
		public override void ForceUpdate()
		{
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x001AD67C File Offset: 0x001AB87C
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x001AD690 File Offset: 0x001AB890
		internal override void NotifyUpdated()
		{
			this.lastPosition = this.tr.position;
			if (this.useRotationAndScale)
			{
				this.lastRotation = this.tr.rotation;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x001AD6BC File Offset: 0x001AB8BC
		public Vector3 Center
		{
			get
			{
				return this.tr.position + (this.useRotationAndScale ? this.tr.TransformPoint(this.center) : this.center);
			}
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x001AD6F0 File Offset: 0x001AB8F0
		[ContextMenu("Rebuild Mesh")]
		public void RebuildMesh()
		{
			if (this.type != NavmeshAdd.MeshType.CustomMesh)
			{
				if (this.verts == null || this.verts.Length != 4 || this.tris == null || this.tris.Length != 6)
				{
					this.verts = new Vector3[4];
					this.tris = new int[6];
				}
				this.tris[0] = 0;
				this.tris[1] = 1;
				this.tris[2] = 2;
				this.tris[3] = 0;
				this.tris[4] = 2;
				this.tris[5] = 3;
				this.verts[0] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[1] = new Vector3(this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[2] = new Vector3(this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
				this.verts[3] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
				return;
			}
			if (this.mesh == null)
			{
				this.verts = null;
				this.tris = null;
				return;
			}
			this.verts = this.mesh.vertices;
			this.tris = this.mesh.triangles;
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x001AD8A0 File Offset: 0x001ABAA0
		internal override Rect GetBounds(GraphTransform inverseTransform)
		{
			if (this.verts == null)
			{
				this.RebuildMesh();
			}
			Int3[] array = ArrayPool<Int3>.Claim((this.verts != null) ? this.verts.Length : 0);
			int[] array2;
			this.GetMesh(ref array, out array2, inverseTransform);
			Rect result = default(Rect);
			for (int i = 0; i < array2.Length; i++)
			{
				Vector3 vector = (Vector3)array[array2[i]];
				if (i == 0)
				{
					result = new Rect(vector.x, vector.z, 0f, 0f);
				}
				else
				{
					result.xMax = Math.Max(result.xMax, vector.x);
					result.yMax = Math.Max(result.yMax, vector.z);
					result.xMin = Math.Min(result.xMin, vector.x);
					result.yMin = Math.Min(result.yMin, vector.z);
				}
			}
			ArrayPool<Int3>.Release(ref array, false);
			return result;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x001AD9A0 File Offset: 0x001ABBA0
		public void GetMesh(ref Int3[] vbuffer, out int[] tbuffer, GraphTransform inverseTransform = null)
		{
			if (this.verts == null)
			{
				this.RebuildMesh();
			}
			if (this.verts == null)
			{
				tbuffer = ArrayPool<int>.Claim(0);
				return;
			}
			if (vbuffer == null || vbuffer.Length < this.verts.Length)
			{
				if (vbuffer != null)
				{
					ArrayPool<Int3>.Release(ref vbuffer, false);
				}
				vbuffer = ArrayPool<Int3>.Claim(this.verts.Length);
			}
			tbuffer = this.tris;
			if (this.useRotationAndScale)
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(this.tr.position + this.center, this.tr.rotation, this.tr.localScale * this.meshScale);
				for (int i = 0; i < this.verts.Length; i++)
				{
					Vector3 vector = matrix4x.MultiplyPoint3x4(this.verts[i]);
					if (inverseTransform != null)
					{
						vector = inverseTransform.InverseTransform(vector);
					}
					vbuffer[i] = (Int3)vector;
				}
				return;
			}
			Vector3 a = this.tr.position + this.center;
			for (int j = 0; j < this.verts.Length; j++)
			{
				Vector3 vector2 = a + this.verts[j] * this.meshScale;
				if (inverseTransform != null)
				{
					vector2 = inverseTransform.InverseTransform(vector2);
				}
				vbuffer[j] = (Int3)vector2;
			}
		}

		// Token: 0x04004234 RID: 16948
		public NavmeshAdd.MeshType type;

		// Token: 0x04004235 RID: 16949
		public Mesh mesh;

		// Token: 0x04004236 RID: 16950
		private Vector3[] verts;

		// Token: 0x04004237 RID: 16951
		private int[] tris;

		// Token: 0x04004238 RID: 16952
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x04004239 RID: 16953
		public float meshScale = 1f;

		// Token: 0x0400423A RID: 16954
		public Vector3 center;

		// Token: 0x0400423B RID: 16955
		[FormerlySerializedAs("useRotation")]
		public bool useRotationAndScale;

		// Token: 0x0400423C RID: 16956
		[Tooltip("Distance between positions to require an update of the navmesh\nA smaller distance gives better accuracy, but requires more updates when moving the object over time, so it is often slower.")]
		public float updateDistance = 0.4f;

		// Token: 0x0400423D RID: 16957
		[Tooltip("How many degrees rotation that is required for an update to the navmesh. Should be between 0 and 180.")]
		public float updateRotationDistance = 10f;

		// Token: 0x0400423E RID: 16958
		protected Transform tr;

		// Token: 0x0400423F RID: 16959
		private Vector3 lastPosition;

		// Token: 0x04004240 RID: 16960
		private Quaternion lastRotation;

		// Token: 0x04004241 RID: 16961
		public static readonly Color GizmoColor = new Color(0.368627459f, 0.9372549f, 0.145098045f);

		// Token: 0x02000770 RID: 1904
		public enum MeshType
		{
			// Token: 0x04004AE7 RID: 19175
			Rectangle,
			// Token: 0x04004AE8 RID: 19176
			CustomMesh
		}
	}
}
