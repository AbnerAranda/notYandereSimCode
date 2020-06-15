using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200059D RID: 1437
	[Serializable]
	public class StartEndModifier : PathModifier
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x0002D199 File Offset: 0x0002B399
		public override int Order
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x001AD290 File Offset: 0x001AB490
		public override void Apply(Path _p)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null || abpath.vectorPath.Count == 0)
			{
				return;
			}
			if (abpath.vectorPath.Count == 1 && !this.addPoints)
			{
				abpath.vectorPath.Add(abpath.vectorPath[0]);
			}
			bool flag;
			Vector3 vector = this.Snap(abpath, this.exactStartPoint, true, out flag);
			bool flag2;
			Vector3 vector2 = this.Snap(abpath, this.exactEndPoint, false, out flag2);
			if ((flag || this.addPoints) && this.exactStartPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Insert(0, vector);
			}
			else
			{
				abpath.vectorPath[0] = vector;
			}
			if ((flag2 || this.addPoints) && this.exactEndPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Add(vector2);
				return;
			}
			abpath.vectorPath[abpath.vectorPath.Count - 1] = vector2;
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x001AD370 File Offset: 0x001AB570
		private Vector3 Snap(ABPath path, StartEndModifier.Exactness mode, bool start, out bool forceAddPoint)
		{
			int num = start ? 0 : (path.path.Count - 1);
			GraphNode graphNode = path.path[num];
			Vector3 vector = (Vector3)graphNode.position;
			forceAddPoint = false;
			switch (mode)
			{
			case StartEndModifier.Exactness.SnapToNode:
				return vector;
			case StartEndModifier.Exactness.Original:
			case StartEndModifier.Exactness.Interpolate:
			case StartEndModifier.Exactness.NodeConnection:
			{
				Vector3 vector2;
				if (start)
				{
					vector2 = ((this.adjustStartPoint != null) ? this.adjustStartPoint() : path.originalStartPoint);
				}
				else
				{
					vector2 = path.originalEndPoint;
				}
				switch (mode)
				{
				case StartEndModifier.Exactness.Original:
					return this.GetClampedPoint(vector, vector2, graphNode);
				case StartEndModifier.Exactness.Interpolate:
				{
					GraphNode graphNode2 = path.path[Mathf.Clamp(num + (start ? 1 : -1), 0, path.path.Count - 1)];
					return VectorMath.ClosestPointOnSegment(vector, (Vector3)graphNode2.position, vector2);
				}
				case StartEndModifier.Exactness.NodeConnection:
				{
					this.connectionBuffer = (this.connectionBuffer ?? new List<GraphNode>());
					Action<GraphNode> action;
					if ((action = this.connectionBufferAddDelegate) == null)
					{
						action = new Action<GraphNode>(this.connectionBuffer.Add);
					}
					this.connectionBufferAddDelegate = action;
					GraphNode graphNode2 = path.path[Mathf.Clamp(num + (start ? 1 : -1), 0, path.path.Count - 1)];
					graphNode.GetConnections(this.connectionBufferAddDelegate);
					Vector3 result = vector;
					float num2 = float.PositiveInfinity;
					for (int i = this.connectionBuffer.Count - 1; i >= 0; i--)
					{
						GraphNode graphNode3 = this.connectionBuffer[i];
						Vector3 vector3 = VectorMath.ClosestPointOnSegment(vector, (Vector3)graphNode3.position, vector2);
						float sqrMagnitude = (vector3 - vector2).sqrMagnitude;
						if (sqrMagnitude < num2)
						{
							result = vector3;
							num2 = sqrMagnitude;
							forceAddPoint = (graphNode3 != graphNode2);
						}
					}
					this.connectionBuffer.Clear();
					return result;
				}
				}
				throw new ArgumentException("Cannot reach this point, but the compiler is not smart enough to realize that.");
			}
			case StartEndModifier.Exactness.ClosestOnNode:
				if (!start)
				{
					return path.endPoint;
				}
				return path.startPoint;
			default:
				throw new ArgumentException("Invalid mode");
			}
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x001AD574 File Offset: 0x001AB774
		protected Vector3 GetClampedPoint(Vector3 from, Vector3 to, GraphNode hint)
		{
			Vector3 vector = to;
			RaycastHit raycastHit;
			if (this.useRaycasting && Physics.Linecast(from, to, out raycastHit, this.mask))
			{
				vector = raycastHit.point;
			}
			if (this.useGraphRaycasting && hint != null)
			{
				IRaycastableGraph raycastableGraph = AstarData.GetGraph(hint) as IRaycastableGraph;
				GraphHitInfo graphHitInfo;
				if (raycastableGraph != null && raycastableGraph.Linecast(from, vector, hint, out graphHitInfo))
				{
					vector = graphHitInfo.point;
				}
			}
			return vector;
		}

		// Token: 0x0400422B RID: 16939
		public bool addPoints;

		// Token: 0x0400422C RID: 16940
		public StartEndModifier.Exactness exactStartPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x0400422D RID: 16941
		public StartEndModifier.Exactness exactEndPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x0400422E RID: 16942
		public Func<Vector3> adjustStartPoint;

		// Token: 0x0400422F RID: 16943
		public bool useRaycasting;

		// Token: 0x04004230 RID: 16944
		public LayerMask mask = -1;

		// Token: 0x04004231 RID: 16945
		public bool useGraphRaycasting;

		// Token: 0x04004232 RID: 16946
		private List<GraphNode> connectionBuffer;

		// Token: 0x04004233 RID: 16947
		private Action<GraphNode> connectionBufferAddDelegate;

		// Token: 0x0200076F RID: 1903
		public enum Exactness
		{
			// Token: 0x04004AE1 RID: 19169
			SnapToNode,
			// Token: 0x04004AE2 RID: 19170
			Original,
			// Token: 0x04004AE3 RID: 19171
			Interpolate,
			// Token: 0x04004AE4 RID: 19172
			ClosestOnNode,
			// Token: 0x04004AE5 RID: 19173
			NodeConnection
		}
	}
}
