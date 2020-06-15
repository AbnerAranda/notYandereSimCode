using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200057F RID: 1407
	public class LevelGridNode : GridNodeBase
	{
		// Token: 0x0600254E RID: 9550 RVA: 0x001A26ED File Offset: 0x001A08ED
		public LevelGridNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x001A26F6 File Offset: 0x001A08F6
		public static LayerGridGraph GetGridGraph(uint graphIndex)
		{
			return LevelGridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x001A2700 File Offset: 0x001A0900
		public static void SetGridGraph(int graphIndex, LayerGridGraph graph)
		{
			GridNode.SetGridGraph(graphIndex, graph);
			if (LevelGridNode._gridGraphs.Length <= graphIndex)
			{
				LayerGridGraph[] array = new LayerGridGraph[graphIndex + 1];
				for (int i = 0; i < LevelGridNode._gridGraphs.Length; i++)
				{
					array[i] = LevelGridNode._gridGraphs[i];
				}
				LevelGridNode._gridGraphs = array;
			}
			LevelGridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x001A2751 File Offset: 0x001A0951
		public void ResetAllGridConnections()
		{
			this.gridConnections = ulong.MaxValue;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x001A275B File Offset: 0x001A095B
		public bool HasAnyGridConnections()
		{
			return this.gridConnections != ulong.MaxValue;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x0002D199 File Offset: 0x0002B399
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x001A276A File Offset: 0x001A096A
		// (set) Token: 0x06002555 RID: 9557 RVA: 0x001A2775 File Offset: 0x001A0975
		public int LayerCoordinateInGrid
		{
			get
			{
				return this.nodeInGridIndex >> 24;
			}
			set
			{
				this.nodeInGridIndex = ((this.nodeInGridIndex & 16777215) | value << 24);
			}
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x001A278E File Offset: 0x001A098E
		public void SetPosition(Int3 position)
		{
			this.position = position;
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x001A2797 File Offset: 0x001A0997
		public override int GetGizmoHashCode()
		{
			return base.GetGizmoHashCode() ^ (int)(805306457UL * this.gridConnections);
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x001A27B0 File Offset: 0x001A09B0
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			if (this.GetConnection(direction))
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * this.GetConnectionValue(direction)];
			}
			return null;
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x001A2800 File Offset: 0x001A0A00
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				int[] neighbourOffsets = gridGraph.neighbourOffsets;
				LevelGridNode[] nodes = gridGraph.nodes;
				for (int i = 0; i < 4; i++)
				{
					int connectionValue = this.GetConnectionValue(i);
					if (connectionValue != 255)
					{
						LevelGridNode levelGridNode = nodes[base.NodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
						if (levelGridNode != null)
						{
							levelGridNode.SetConnectionValue((i + 2) % 4, 255);
						}
					}
				}
			}
			this.ResetAllGridConnections();
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x001A2884 File Offset: 0x001A0A84
		public override void GetConnections(Action<GraphNode> action)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (levelGridNode != null)
					{
						action(levelGridNode);
					}
				}
			}
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x001A28FC File Offset: 0x001A0AFC
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			int nodeInGridIndex = base.NodeInGridIndex;
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (levelGridNode != null && levelGridNode.Area != region)
					{
						levelGridNode.Area = region;
						stack.Push(levelGridNode);
					}
				}
			}
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x001A2986 File Offset: 0x001A0B86
		public bool GetConnection(int i)
		{
			return (this.gridConnections >> i * 8 & 255UL) != 255UL;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x001A29A7 File Offset: 0x001A0BA7
		public void SetConnectionValue(int dir, int value)
		{
			this.gridConnections = ((this.gridConnections & ~(255UL << dir * 8)) | (ulong)((ulong)((long)value) << dir * 8));
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x001A29CE File Offset: 0x001A0BCE
		public int GetConnectionValue(int dir)
		{
			return (int)(this.gridConnections >> dir * 8 & 255UL);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x001A29E8 File Offset: 0x001A0BE8
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255 && other == nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue])
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
			return false;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x001A2AF0 File Offset: 0x001A0CF0
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			handler.heap.Add(pathNode);
			pathNode.UpdateG(path);
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					PathNode pathNode2 = handler.GetPathNode(levelGridNode);
					if (pathNode2 != null && pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
					{
						levelGridNode.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x001A2BA4 File Offset: 0x001A0DA4
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					GraphNode graphNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (path.CanTraverse(graphNode))
					{
						PathNode pathNode2 = handler.GetPathNode(graphNode);
						if (pathNode2.pathID != handler.PathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = handler.PathID;
							pathNode2.cost = neighbourCosts[i];
							pathNode2.H = path.CalculateHScore(graphNode);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else
						{
							uint num = neighbourCosts[i];
							if (pathNode.G + num + path.GetTraversalCost(graphNode) < pathNode2.G)
							{
								pathNode2.cost = num;
								pathNode2.parent = pathNode;
								graphNode.UpdateRecursiveG(path, pathNode2, handler);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x001A2CCB File Offset: 0x001A0ECB
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
			ctx.writer.Write(this.gridConnections);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x001A2D04 File Offset: 0x001A0F04
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
			if (ctx.meta.version < AstarSerializer.V3_9_0)
			{
				this.gridConnections = ((ulong)ctx.reader.ReadUInt32() | 18446744069414584320UL);
				return;
			}
			this.gridConnections = ctx.reader.ReadUInt64();
		}

		// Token: 0x0400416D RID: 16749
		private static LayerGridGraph[] _gridGraphs = new LayerGridGraph[0];

		// Token: 0x0400416E RID: 16750
		public ulong gridConnections;

		// Token: 0x0400416F RID: 16751
		protected static LayerGridGraph[] gridGraphs;

		// Token: 0x04004170 RID: 16752
		public const int NoConnection = 255;

		// Token: 0x04004171 RID: 16753
		public const int ConnectionMask = 255;

		// Token: 0x04004172 RID: 16754
		private const int ConnectionStride = 8;

		// Token: 0x04004173 RID: 16755
		public const int MaxLayerCount = 255;
	}
}
