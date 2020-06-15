using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x020005A0 RID: 1440
	[AddComponentMenu("Pathfinding/Navmesh/Navmesh Cut")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_navmesh_cut.php")]
	public class NavmeshCut : NavmeshClipper
	{
		// Token: 0x060026E0 RID: 9952 RVA: 0x001ADC74 File Offset: 0x001ABE74
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x001ADC88 File Offset: 0x001ABE88
		protected override void OnEnable()
		{
			base.OnEnable();
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			this.lastRotation = this.tr.rotation;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x001ADCBB File Offset: 0x001ABEBB
		public override void ForceUpdate()
		{
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x001ADCD8 File Offset: 0x001ABED8
		public override bool RequiresUpdate()
		{
			return (this.tr.position - this.lastPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotationAndScale && Quaternion.Angle(this.lastRotation, this.tr.rotation) > this.updateRotationDistance);
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void UsedForCut()
		{
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x001ADD3C File Offset: 0x001ABF3C
		internal override void NotifyUpdated()
		{
			this.lastPosition = this.tr.position;
			if (this.useRotationAndScale)
			{
				this.lastRotation = this.tr.rotation;
			}
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x001ADD68 File Offset: 0x001ABF68
		private void CalculateMeshContour()
		{
			if (this.mesh == null)
			{
				return;
			}
			NavmeshCut.edges.Clear();
			NavmeshCut.pointers.Clear();
			Vector3[] vertices = this.mesh.vertices;
			int[] triangles = this.mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				if (VectorMath.IsClockwiseXZ(vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]]))
				{
					int num = triangles[i];
					triangles[i] = triangles[i + 2];
					triangles[i + 2] = num;
				}
				NavmeshCut.edges[new Int2(triangles[i], triangles[i + 1])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 1], triangles[i + 2])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 2], triangles[i])] = i;
			}
			for (int j = 0; j < triangles.Length; j += 3)
			{
				for (int k = 0; k < 3; k++)
				{
					if (!NavmeshCut.edges.ContainsKey(new Int2(triangles[j + (k + 1) % 3], triangles[j + k % 3])))
					{
						NavmeshCut.pointers[triangles[j + k % 3]] = triangles[j + (k + 1) % 3];
					}
				}
			}
			List<Vector3[]> list = new List<Vector3[]>();
			List<Vector3> list2 = ListPool<Vector3>.Claim();
			for (int l = 0; l < vertices.Length; l++)
			{
				if (NavmeshCut.pointers.ContainsKey(l))
				{
					list2.Clear();
					int num2 = l;
					do
					{
						int num3 = NavmeshCut.pointers[num2];
						if (num3 == -1)
						{
							break;
						}
						NavmeshCut.pointers[num2] = -1;
						list2.Add(vertices[num2]);
						num2 = num3;
						if (num2 == -1)
						{
							goto Block_9;
						}
					}
					while (num2 != l);
					IL_1E4:
					if (list2.Count > 0)
					{
						list.Add(list2.ToArray());
						goto IL_1F9;
					}
					goto IL_1F9;
					Block_9:
					Debug.LogError("Invalid Mesh '" + this.mesh.name + " in " + base.gameObject.name);
					goto IL_1E4;
				}
				IL_1F9:;
			}
			ListPool<Vector3>.Release(ref list2);
			this.contours = list.ToArray();
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x001ADF94 File Offset: 0x001AC194
		internal override Rect GetBounds(GraphTransform inverseTranform)
		{
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Rect result = default(Rect);
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = inverseTranform.InverseTransform(list2[j]);
					if (j == 0)
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
			}
			ListPool<List<Vector3>>.Release(ref list);
			return result;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x001AE098 File Offset: 0x001AC298
		public void GetContour(List<List<Vector3>> buffer)
		{
			if (this.circleResolution < 3)
			{
				this.circleResolution = 3;
			}
			switch (this.type)
			{
			case NavmeshCut.MeshType.Rectangle:
			{
				List<Vector3> list = ListPool<Vector3>.Claim();
				list.Add(new Vector3(-this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(-this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f);
				bool reverse = this.rectangleSize.x < 0f ^ this.rectangleSize.y < 0f;
				this.TransformBuffer(list, reverse);
				buffer.Add(list);
				return;
			}
			case NavmeshCut.MeshType.Circle:
			{
				List<Vector3> list = ListPool<Vector3>.Claim(this.circleResolution);
				for (int i = 0; i < this.circleResolution; i++)
				{
					list.Add(new Vector3(Mathf.Cos((float)(i * 2) * 3.14159274f / (float)this.circleResolution), 0f, Mathf.Sin((float)(i * 2) * 3.14159274f / (float)this.circleResolution)) * this.circleRadius);
				}
				bool reverse = this.circleRadius < 0f;
				this.TransformBuffer(list, reverse);
				buffer.Add(list);
				return;
			}
			case NavmeshCut.MeshType.CustomMesh:
				if (this.mesh != this.lastMesh || this.contours == null)
				{
					this.CalculateMeshContour();
					this.lastMesh = this.mesh;
				}
				if (this.contours != null)
				{
					bool reverse = this.meshScale < 0f;
					for (int j = 0; j < this.contours.Length; j++)
					{
						Vector3[] array = this.contours[j];
						List<Vector3> list = ListPool<Vector3>.Claim(array.Length);
						for (int k = 0; k < array.Length; k++)
						{
							list.Add(array[k] * this.meshScale);
						}
						this.TransformBuffer(list, reverse);
						buffer.Add(list);
					}
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x001AE300 File Offset: 0x001AC500
		private void TransformBuffer(List<Vector3> buffer, bool reverse)
		{
			Vector3 vector = this.center;
			if (this.useRotationAndScale)
			{
				Matrix4x4 localToWorldMatrix = this.tr.localToWorldMatrix;
				for (int i = 0; i < buffer.Count; i++)
				{
					buffer[i] = localToWorldMatrix.MultiplyPoint3x4(buffer[i] + vector);
				}
				reverse ^= VectorMath.ReversesFaceOrientationsXZ(localToWorldMatrix);
			}
			else
			{
				vector += this.tr.position;
				for (int j = 0; j < buffer.Count; j++)
				{
					int index = j;
					buffer[index] += vector;
				}
			}
			if (reverse)
			{
				buffer.Reverse();
			}
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x001AE3AC File Offset: 0x001AC5AC
		public void OnDrawGizmos()
		{
			if (this.tr == null)
			{
				this.tr = base.transform;
			}
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Gizmos.color = NavmeshCut.GizmoColor;
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 from = list2[j];
					Vector3 to = list2[(j + 1) % list2.Count];
					Gizmos.DrawLine(from, to);
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x001AE43B File Offset: 0x001AC63B
		internal float GetY(GraphTransform transform)
		{
			return transform.InverseTransform(this.useRotationAndScale ? this.tr.TransformPoint(this.center) : (this.tr.position + this.center)).y;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x001AE47C File Offset: 0x001AC67C
		public void OnDrawGizmosSelected()
		{
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Color color = Color.Lerp(NavmeshCut.GizmoColor, Color.white, 0.5f);
			color.a *= 0.5f;
			Gizmos.color = color;
			NavmeshBase navmeshBase = (AstarPath.active != null) ? (AstarPath.active.data.recastGraph ?? AstarPath.active.data.navmesh) : null;
			GraphTransform graphTransform = (navmeshBase != null) ? navmeshBase.transform : GraphTransform.identityTransform;
			float y = this.GetY(graphTransform);
			float y2 = y - this.height * 0.5f;
			float y3 = y + this.height * 0.5f;
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = graphTransform.InverseTransform(list2[j]);
					Vector3 vector2 = graphTransform.InverseTransform(list2[(j + 1) % list2.Count]);
					Vector3 point = vector;
					Vector3 point2 = vector2;
					Vector3 point3 = vector;
					Vector3 point4 = vector2;
					point.y = (point2.y = y2);
					point3.y = (point4.y = y3);
					Gizmos.DrawLine(graphTransform.Transform(point), graphTransform.Transform(point2));
					Gizmos.DrawLine(graphTransform.Transform(point3), graphTransform.Transform(point4));
					Gizmos.DrawLine(graphTransform.Transform(point), graphTransform.Transform(point3));
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
		}

		// Token: 0x04004246 RID: 16966
		[Tooltip("Shape of the cut")]
		public NavmeshCut.MeshType type;

		// Token: 0x04004247 RID: 16967
		[Tooltip("The contour(s) of the mesh will be extracted. This mesh should only be a 2D surface, not a volume (see documentation).")]
		public Mesh mesh;

		// Token: 0x04004248 RID: 16968
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x04004249 RID: 16969
		public float circleRadius = 1f;

		// Token: 0x0400424A RID: 16970
		public int circleResolution = 6;

		// Token: 0x0400424B RID: 16971
		public float height = 1f;

		// Token: 0x0400424C RID: 16972
		[Tooltip("Scale of the custom mesh")]
		public float meshScale = 1f;

		// Token: 0x0400424D RID: 16973
		public Vector3 center;

		// Token: 0x0400424E RID: 16974
		[Tooltip("Distance between positions to require an update of the navmesh\nA smaller distance gives better accuracy, but requires more updates when moving the object over time, so it is often slower.")]
		public float updateDistance = 0.4f;

		// Token: 0x0400424F RID: 16975
		[Tooltip("Only makes a split in the navmesh, but does not remove the geometry to make a hole")]
		public bool isDual;

		// Token: 0x04004250 RID: 16976
		public bool cutsAddedGeom = true;

		// Token: 0x04004251 RID: 16977
		[Tooltip("How many degrees rotation that is required for an update to the navmesh. Should be between 0 and 180.")]
		public float updateRotationDistance = 10f;

		// Token: 0x04004252 RID: 16978
		[Tooltip("Includes rotation in calculations. This is slower since a lot more matrix multiplications are needed but gives more flexibility.")]
		[FormerlySerializedAs("useRotation")]
		public bool useRotationAndScale;

		// Token: 0x04004253 RID: 16979
		private Vector3[][] contours;

		// Token: 0x04004254 RID: 16980
		protected Transform tr;

		// Token: 0x04004255 RID: 16981
		private Mesh lastMesh;

		// Token: 0x04004256 RID: 16982
		private Vector3 lastPosition;

		// Token: 0x04004257 RID: 16983
		private Quaternion lastRotation;

		// Token: 0x04004258 RID: 16984
		private static readonly Dictionary<Int2, int> edges = new Dictionary<Int2, int>();

		// Token: 0x04004259 RID: 16985
		private static readonly Dictionary<int, int> pointers = new Dictionary<int, int>();

		// Token: 0x0400425A RID: 16986
		public static readonly Color GizmoColor = new Color(0.145098045f, 0.721568644f, 0.9372549f);

		// Token: 0x02000771 RID: 1905
		public enum MeshType
		{
			// Token: 0x04004AEA RID: 19178
			Rectangle,
			// Token: 0x04004AEB RID: 19179
			Circle,
			// Token: 0x04004AEC RID: 19180
			CustomMesh
		}
	}
}
