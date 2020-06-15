using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000584 RID: 1412
	public class GridNode : GridNodeBase
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x001A26ED File Offset: 0x001A08ED
		public GridNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x001A5825 File Offset: 0x001A3A25
		public static GridGraph GetGridGraph(uint graphIndex)
		{
			return GridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x001A5830 File Offset: 0x001A3A30
		public static void SetGridGraph(int graphIndex, GridGraph graph)
		{
			if (GridNode._gridGraphs.Length <= graphIndex)
			{
				GridGraph[] array = new GridGraph[graphIndex + 1];
				for (int i = 0; i < GridNode._gridGraphs.Length; i++)
				{
					array[i] = GridNode._gridGraphs[i];
				}
				GridNode._gridGraphs = array;
			}
			GridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x001A587A File Offset: 0x001A3A7A
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x001A5882 File Offset: 0x001A3A82
		internal ushort InternalGridFlags
		{
			get
			{
				return this.gridFlags;
			}
			set
			{
				this.gridFlags = value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x001A588B File Offset: 0x001A3A8B
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				return (this.InternalGridFlags & 255) == 255;
			}
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x001A58A0 File Offset: 0x001A3AA0
		public bool HasConnectionInDirection(int dir)
		{
			return (this.gridFlags >> dir & 1) != 0;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x001A58B2 File Offset: 0x001A3AB2
		[Obsolete("Use HasConnectionInDirection")]
		public bool GetConnectionInternal(int dir)
		{
			return this.HasConnectionInDirection(dir);
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x001A58BB File Offset: 0x001A3ABB
		public void SetConnectionInternal(int dir, bool value)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & ~(1 << dir)) | (value ? 1 : 0) << (dir & 31));
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x001A58DF File Offset: 0x001A3ADF
		public void SetAllConnectionInternal(int connections)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & -256) | connections);
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x001A58F6 File Offset: 0x001A3AF6
		public void ResetConnectionsInternal()
		{
			this.gridFlags = (ushort)((int)this.gridFlags & -256);
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x001A590B File Offset: 0x001A3B0B
		// (set) Token: 0x060025BA RID: 9658 RVA: 0x001A591C File Offset: 0x001A3B1C
		public bool EdgeNode
		{
			get
			{
				return (this.gridFlags & 1024) > 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -1025) | (value ? 1024 : 0));
			}
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x001A5940 File Offset: 0x001A3B40
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			if (this.HasConnectionInDirection(direction))
			{
				GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction]];
			}
			return null;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x001A597C File Offset: 0x001A3B7C
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				for (int i = 0; i < 8; i++)
				{
					GridNode gridNode = this.GetNeighbourAlongDirection(i) as GridNode;
					if (gridNode != null)
					{
						gridNode.SetConnectionInternal((i < 4) ? ((i + 2) % 4) : ((i - 2) % 4 + 4), false);
					}
				}
			}
			this.ResetConnectionsInternal();
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x001A59C8 File Offset: 0x001A3BC8
		public override void GetConnections(Action<GraphNode> action)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[base.NodeInGridIndex + neighbourOffsets[i]];
					if (gridNode != null)
					{
						action(gridNode);
					}
				}
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x001A5A1C File Offset: 0x001A3C1C
		public Vector3 ClosestPointOnNode(Vector3 p)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			p = gridGraph.transform.InverseTransform(p);
			int num = base.NodeInGridIndex % gridGraph.width;
			int num2 = base.NodeInGridIndex / gridGraph.width;
			float y = gridGraph.transform.InverseTransform((Vector3)this.position).y;
			Vector3 point = new Vector3(Mathf.Clamp(p.x, (float)num, (float)num + 1f), y, Mathf.Clamp(p.z, (float)num2, (float)num2 + 1f));
			return gridGraph.transform.Transform(point);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x001A5ABC File Offset: 0x001A3CBC
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				if (this.HasConnectionInDirection(i) && other == nodes[base.NodeInGridIndex + neighbourOffsets[i]])
				{
					Vector3 a = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector.Normalize();
					vector *= gridGraph.nodeSize * 0.5f;
					left.Add(a - vector);
					right.Add(a + vector);
					return true;
				}
			}
			for (int j = 4; j < 8; j++)
			{
				if (this.HasConnectionInDirection(j) && other == nodes[base.NodeInGridIndex + neighbourOffsets[j]])
				{
					bool flag = false;
					bool flag2 = false;
					if (this.HasConnectionInDirection(j - 4))
					{
						GridNode gridNode = nodes[base.NodeInGridIndex + neighbourOffsets[j - 4]];
						if (gridNode.Walkable && gridNode.HasConnectionInDirection((j - 4 + 1) % 4))
						{
							flag = true;
						}
					}
					if (this.HasConnectionInDirection((j - 4 + 1) % 4))
					{
						GridNode gridNode2 = nodes[base.NodeInGridIndex + neighbourOffsets[(j - 4 + 1) % 4]];
						if (gridNode2.Walkable && gridNode2.HasConnectionInDirection(j - 4))
						{
							flag2 = true;
						}
					}
					Vector3 a2 = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector2 = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector2.Normalize();
					vector2 *= gridGraph.nodeSize * 1.4142f;
					left.Add(a2 - (flag2 ? vector2 : Vector3.zero));
					right.Add(a2 + (flag ? vector2 : Vector3.zero));
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x001A5CF0 File Offset: 0x001A3EF0
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[nodeInGridIndex + neighbourOffsets[i]];
					if (gridNode != null && gridNode.Area != region)
					{
						gridNode.Area = region;
						stack.Push(gridNode);
					}
				}
			}
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x001A5D58 File Offset: 0x001A3F58
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			ushort pathID = handler.PathID;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[nodeInGridIndex + neighbourOffsets[i]];
					PathNode pathNode2 = handler.GetPathNode(gridNode);
					if (pathNode2.parent == pathNode && pathNode2.pathID == pathID)
					{
						gridNode.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x001A5DEC File Offset: 0x001A3FEC
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			ushort pathID = handler.PathID;
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			GridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[nodeInGridIndex + neighbourOffsets[i]];
					if (path.CanTraverse(gridNode))
					{
						PathNode pathNode2 = handler.GetPathNode(gridNode);
						uint num = neighbourCosts[i];
						if (pathNode2.pathID != pathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = pathID;
							pathNode2.cost = num;
							pathNode2.H = path.CalculateHScore(gridNode);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else if (pathNode.G + num + path.GetTraversalCost(gridNode) < pathNode2.G)
						{
							pathNode2.cost = num;
							pathNode2.parent = pathNode;
							gridNode.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x001A5EF2 File Offset: 0x001A40F2
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x001A5F18 File Offset: 0x001A4118
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
		}

		// Token: 0x0400418F RID: 16783
		private static GridGraph[] _gridGraphs = new GridGraph[0];

		// Token: 0x04004190 RID: 16784
		private const int GridFlagsConnectionOffset = 0;

		// Token: 0x04004191 RID: 16785
		private const int GridFlagsConnectionBit0 = 1;

		// Token: 0x04004192 RID: 16786
		private const int GridFlagsConnectionMask = 255;

		// Token: 0x04004193 RID: 16787
		private const int GridFlagsEdgeNodeOffset = 10;

		// Token: 0x04004194 RID: 16788
		private const int GridFlagsEdgeNodeMask = 1024;
	}
}
