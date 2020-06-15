using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005DF RID: 1503
	public abstract class RVOObstacle : VersionedMonoBehaviour
	{
		// Token: 0x060028F3 RID: 10483
		protected abstract void CreateObstacles();

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060028F4 RID: 10484
		protected abstract bool ExecuteInEditor { get; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060028F5 RID: 10485
		protected abstract bool LocalCoordinates { get; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060028F6 RID: 10486
		protected abstract bool StaticObstacle { get; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060028F7 RID: 10487
		protected abstract float Height { get; }

		// Token: 0x060028F8 RID: 10488
		protected abstract bool AreGizmosDirty();

		// Token: 0x060028F9 RID: 10489 RVA: 0x001BEA55 File Offset: 0x001BCC55
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x001BEA5E File Offset: 0x001BCC5E
		public void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x001BEA68 File Offset: 0x001BCC68
		public void OnDrawGizmos(bool selected)
		{
			this.gizmoDrawing = true;
			Gizmos.color = new Color(0.615f, 1f, 0.06f, selected ? 1f : 0.7f);
			MovementPlane movementPlane = (RVOSimulator.active != null) ? RVOSimulator.active.movementPlane : MovementPlane.XZ;
			Vector3 vector = (movementPlane == MovementPlane.XZ) ? Vector3.up : (-Vector3.forward);
			if (this.gizmoVerts == null || this.AreGizmosDirty() || this._obstacleMode != this.obstacleMode)
			{
				this._obstacleMode = this.obstacleMode;
				if (this.gizmoVerts == null)
				{
					this.gizmoVerts = new List<Vector3[]>();
				}
				else
				{
					this.gizmoVerts.Clear();
				}
				this.CreateObstacles();
			}
			Matrix4x4 matrix = this.GetMatrix();
			for (int i = 0; i < this.gizmoVerts.Count; i++)
			{
				Vector3[] array = this.gizmoVerts[i];
				int j = 0;
				int num = array.Length - 1;
				while (j < array.Length)
				{
					Gizmos.DrawLine(matrix.MultiplyPoint3x4(array[j]), matrix.MultiplyPoint3x4(array[num]));
					num = j++;
				}
				if (selected)
				{
					int k = 0;
					int num2 = array.Length - 1;
					while (k < array.Length)
					{
						Vector3 vector2 = matrix.MultiplyPoint3x4(array[num2]);
						Vector3 vector3 = matrix.MultiplyPoint3x4(array[k]);
						if (movementPlane != MovementPlane.XY)
						{
							Gizmos.DrawLine(vector2 + vector * this.Height, vector3 + vector * this.Height);
							Gizmos.DrawLine(vector2, vector2 + vector * this.Height);
						}
						Vector3 vector4 = (vector2 + vector3) * 0.5f;
						Vector3 normalized = (vector3 - vector2).normalized;
						if (!(normalized == Vector3.zero))
						{
							Vector3 vector5 = Vector3.Cross(vector, normalized);
							Gizmos.DrawLine(vector4, vector4 + vector5);
							Gizmos.DrawLine(vector4 + vector5, vector4 + vector5 * 0.5f + normalized * 0.5f);
							Gizmos.DrawLine(vector4 + vector5, vector4 + vector5 * 0.5f - normalized * 0.5f);
						}
						num2 = k++;
					}
				}
			}
			this.gizmoDrawing = false;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x001BECEB File Offset: 0x001BCEEB
		protected virtual Matrix4x4 GetMatrix()
		{
			if (!this.LocalCoordinates)
			{
				return Matrix4x4.identity;
			}
			return base.transform.localToWorldMatrix;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x001BED08 File Offset: 0x001BCF08
		public void OnDisable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnEnable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.RemoveObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x001BED60 File Offset: 0x001BCF60
		public void OnEnable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnDisable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					ObstacleVertex obstacleVertex = this.addedObstacles[i];
					ObstacleVertex obstacleVertex2 = obstacleVertex;
					do
					{
						obstacleVertex.layer = this.layer;
						obstacleVertex = obstacleVertex.next;
					}
					while (obstacleVertex != obstacleVertex2);
					this.sim.AddObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x001BEDDC File Offset: 0x001BCFDC
		public void Start()
		{
			this.addedObstacles = new List<ObstacleVertex>();
			this.sourceObstacles = new List<Vector3[]>();
			this.prevUpdateMatrix = this.GetMatrix();
			this.CreateObstacles();
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x001BEE08 File Offset: 0x001BD008
		public void Update()
		{
			Matrix4x4 matrix = this.GetMatrix();
			if (matrix != this.prevUpdateMatrix)
			{
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.UpdateObstacle(this.addedObstacles[i], this.sourceObstacles[i], matrix);
				}
				this.prevUpdateMatrix = matrix;
			}
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x001BEE6B File Offset: 0x001BD06B
		protected void FindSimulator()
		{
			if (RVOSimulator.active == null)
			{
				throw new InvalidOperationException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			this.sim = RVOSimulator.active.GetSimulator();
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x001BEE98 File Offset: 0x001BD098
		protected void AddObstacle(Vector3[] vertices, float height)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices Must Not Be Null");
			}
			if (height < 0f)
			{
				throw new ArgumentOutOfRangeException("Height must be non-negative");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("An obstacle must have at least two vertices");
			}
			if (this.sim == null)
			{
				this.FindSimulator();
			}
			if (this.gizmoDrawing)
			{
				Vector3[] array = new Vector3[vertices.Length];
				this.WindCorrectly(vertices);
				Array.Copy(vertices, array, vertices.Length);
				this.gizmoVerts.Add(array);
				return;
			}
			if (vertices.Length == 2)
			{
				this.AddObstacleInternal(vertices, height);
				return;
			}
			this.WindCorrectly(vertices);
			this.AddObstacleInternal(vertices, height);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x001BEF32 File Offset: 0x001BD132
		private void AddObstacleInternal(Vector3[] vertices, float height)
		{
			this.addedObstacles.Add(this.sim.AddObstacle(vertices, height, this.GetMatrix(), this.layer, true));
			this.sourceObstacles.Add(vertices);
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x001BEF68 File Offset: 0x001BD168
		private void WindCorrectly(Vector3[] vertices)
		{
			int num = 0;
			float num2 = float.PositiveInfinity;
			Matrix4x4 matrix = this.GetMatrix();
			for (int i = 0; i < vertices.Length; i++)
			{
				float x = matrix.MultiplyPoint3x4(vertices[i]).x;
				if (x < num2)
				{
					num = i;
					num2 = x;
				}
			}
			Vector3 vector = matrix.MultiplyPoint3x4(vertices[(num - 1 + vertices.Length) % vertices.Length]);
			Vector3 vector2 = matrix.MultiplyPoint3x4(vertices[num]);
			Vector3 vector3 = matrix.MultiplyPoint3x4(vertices[(num + 1) % vertices.Length]);
			MovementPlane movementPlane;
			if (this.sim != null)
			{
				movementPlane = this.sim.movementPlane;
			}
			else if (RVOSimulator.active)
			{
				movementPlane = RVOSimulator.active.movementPlane;
			}
			else
			{
				movementPlane = MovementPlane.XZ;
			}
			if (movementPlane == MovementPlane.XY)
			{
				vector.z = vector.y;
				vector2.z = vector2.y;
				vector3.z = vector3.y;
			}
			if (VectorMath.IsClockwiseXZ(vector, vector2, vector3) != (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.KeepIn))
			{
				Array.Reverse(vertices);
			}
		}

		// Token: 0x04004391 RID: 17297
		public RVOObstacle.ObstacleVertexWinding obstacleMode;

		// Token: 0x04004392 RID: 17298
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x04004393 RID: 17299
		protected Simulator sim;

		// Token: 0x04004394 RID: 17300
		private List<ObstacleVertex> addedObstacles;

		// Token: 0x04004395 RID: 17301
		private List<Vector3[]> sourceObstacles;

		// Token: 0x04004396 RID: 17302
		private bool gizmoDrawing;

		// Token: 0x04004397 RID: 17303
		private List<Vector3[]> gizmoVerts;

		// Token: 0x04004398 RID: 17304
		private RVOObstacle.ObstacleVertexWinding _obstacleMode;

		// Token: 0x04004399 RID: 17305
		private Matrix4x4 prevUpdateMatrix;

		// Token: 0x02000789 RID: 1929
		public enum ObstacleVertexWinding
		{
			// Token: 0x04004B49 RID: 19273
			KeepOut,
			// Token: 0x04004B4A RID: 19274
			KeepIn
		}
	}
}
