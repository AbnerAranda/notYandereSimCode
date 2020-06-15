using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005F7 RID: 1527
	public class GraphGizmoHelper : IAstarPooledObject, IDisposable
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x001C48C7 File Offset: 0x001C2AC7
		// (set) Token: 0x060029D8 RID: 10712 RVA: 0x001C48CF File Offset: 0x001C2ACF
		public RetainedGizmos.Hasher hasher { get; private set; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x001C48D8 File Offset: 0x001C2AD8
		// (set) Token: 0x060029DA RID: 10714 RVA: 0x001C48E0 File Offset: 0x001C2AE0
		public RetainedGizmos.Builder builder { get; private set; }

		// Token: 0x060029DB RID: 10715 RVA: 0x001C48E9 File Offset: 0x001C2AE9
		public GraphGizmoHelper()
		{
			this.drawConnection = new Action<GraphNode>(this.DrawConnection);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x001C4904 File Offset: 0x001C2B04
		public void Init(AstarPath active, RetainedGizmos.Hasher hasher, RetainedGizmos gizmos)
		{
			if (active != null)
			{
				this.debugData = active.debugPathData;
				this.debugPathID = active.debugPathID;
				this.debugMode = active.debugMode;
				this.debugFloor = active.debugFloor;
				this.debugRoof = active.debugRoof;
				this.showSearchTree = (active.showSearchTree && this.debugData != null);
			}
			this.gizmos = gizmos;
			this.hasher = hasher;
			this.builder = ObjectPool<RetainedGizmos.Builder>.Claim();
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x001C498C File Offset: 0x001C2B8C
		public void OnEnterPool()
		{
			RetainedGizmos.Builder builder = this.builder;
			ObjectPool<RetainedGizmos.Builder>.Release(ref builder);
			this.builder = null;
			this.debugData = null;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x001C49B8 File Offset: 0x001C2BB8
		public void DrawConnections(GraphNode node)
		{
			if (this.showSearchTree)
			{
				if (GraphGizmoHelper.InSearchTree(node, this.debugData, this.debugPathID) && this.debugData.GetPathNode(node).parent != null)
				{
					this.builder.DrawLine((Vector3)node.position, (Vector3)this.debugData.GetPathNode(node).parent.node.position, this.NodeColor(node));
					return;
				}
			}
			else
			{
				this.drawConnectionColor = this.NodeColor(node);
				this.drawConnectionStart = (Vector3)node.position;
				node.GetConnections(this.drawConnection);
			}
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x001C4A5C File Offset: 0x001C2C5C
		private void DrawConnection(GraphNode other)
		{
			this.builder.DrawLine(this.drawConnectionStart, Vector3.Lerp((Vector3)other.position, this.drawConnectionStart, 0.5f), this.drawConnectionColor);
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x001C4A90 File Offset: 0x001C2C90
		public Color NodeColor(GraphNode node)
		{
			if (this.showSearchTree && !GraphGizmoHelper.InSearchTree(node, this.debugData, this.debugPathID))
			{
				return Color.clear;
			}
			Color result;
			if (node.Walkable)
			{
				switch (this.debugMode)
				{
				case GraphDebugMode.Areas:
					return AstarColor.GetAreaColor(node.Area);
				case GraphDebugMode.Penalty:
					return Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, (node.Penalty - this.debugFloor) / (this.debugRoof - this.debugFloor));
				case GraphDebugMode.Connections:
					return AstarColor.NodeConnection;
				case GraphDebugMode.Tags:
					return AstarColor.GetAreaColor(node.Tag);
				}
				if (this.debugData == null)
				{
					result = AstarColor.NodeConnection;
				}
				else
				{
					PathNode pathNode = this.debugData.GetPathNode(node);
					float num;
					if (this.debugMode == GraphDebugMode.G)
					{
						num = pathNode.G;
					}
					else if (this.debugMode == GraphDebugMode.H)
					{
						num = pathNode.H;
					}
					else
					{
						num = pathNode.F;
					}
					result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, (num - this.debugFloor) / (this.debugRoof - this.debugFloor));
				}
			}
			else
			{
				result = AstarColor.UnwalkableNode;
			}
			return result;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x001C4BCE File Offset: 0x001C2DCE
		public static bool InSearchTree(GraphNode node, PathHandler handler, ushort pathID)
		{
			return handler.GetPathNode(node).pathID == pathID;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x001C4BDF File Offset: 0x001C2DDF
		public void DrawWireTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
		{
			this.builder.DrawLine(a, b, color);
			this.builder.DrawLine(b, c, color);
			this.builder.DrawLine(c, a, color);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x001C4C10 File Offset: 0x001C2E10
		public void DrawTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			List<int> list = ListPool<int>.Claim(numTriangles);
			for (int i = 0; i < numTriangles * 3; i++)
			{
				list.Add(i);
			}
			this.builder.DrawMesh(this.gizmos, vertices, list, colors);
			ListPool<int>.Release(ref list);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x001C4C54 File Offset: 0x001C2E54
		public void DrawWireTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			for (int i = 0; i < numTriangles; i++)
			{
				this.DrawWireTriangle(vertices[i * 3], vertices[i * 3 + 1], vertices[i * 3 + 2], colors[i * 3]);
			}
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x001C4C9B File Offset: 0x001C2E9B
		public void Submit()
		{
			this.builder.Submit(this.gizmos, this.hasher);
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x001C4CB4 File Offset: 0x001C2EB4
		void IDisposable.Dispose()
		{
			GraphGizmoHelper graphGizmoHelper = this;
			this.Submit();
			ObjectPool<GraphGizmoHelper>.Release(ref graphGizmoHelper);
		}

		// Token: 0x04004416 RID: 17430
		private RetainedGizmos gizmos;

		// Token: 0x04004417 RID: 17431
		private PathHandler debugData;

		// Token: 0x04004418 RID: 17432
		private ushort debugPathID;

		// Token: 0x04004419 RID: 17433
		private GraphDebugMode debugMode;

		// Token: 0x0400441A RID: 17434
		private bool showSearchTree;

		// Token: 0x0400441B RID: 17435
		private float debugFloor;

		// Token: 0x0400441C RID: 17436
		private float debugRoof;

		// Token: 0x0400441E RID: 17438
		private Vector3 drawConnectionStart;

		// Token: 0x0400441F RID: 17439
		private Color drawConnectionColor;

		// Token: 0x04004420 RID: 17440
		private readonly Action<GraphNode> drawConnection;
	}
}
