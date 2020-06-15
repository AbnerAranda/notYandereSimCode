using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000586 RID: 1414
	public class PointNode : GraphNode
	{
		// Token: 0x060025D8 RID: 9688 RVA: 0x001A278E File Offset: 0x001A098E
		public void SetPosition(Int3 value)
		{
			this.position = value;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0019A4CC File Offset: 0x001986CC
		public PointNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x001A60E4 File Offset: 0x001A42E4
		public override void GetConnections(Action<GraphNode> action)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				action(this.connections[i].node);
			}
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x001A6124 File Offset: 0x001A4324
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].node.RemoveConnection(this);
				}
			}
			this.connections = null;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x001A6170 File Offset: 0x001A4370
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				PathNode pathNode2 = handler.GetPathNode(node);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					node.UpdateRecursiveG(path, pathNode2, handler);
				}
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x001A61E0 File Offset: 0x001A43E0
		public override bool ContainsConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return false;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x001A6224 File Offset: 0x001A4424
		public override void AddConnection(GraphNode node, uint cost)
		{
			if (node == null)
			{
				throw new ArgumentNullException();
			}
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node == node)
					{
						this.connections[i].cost = cost;
						return;
					}
				}
			}
			int num = (this.connections != null) ? this.connections.Length : 0;
			Connection[] array = new Connection[num + 1];
			for (int j = 0; j < num; j++)
			{
				array[j] = this.connections[j];
			}
			array[num] = new Connection(node, cost, byte.MaxValue);
			this.connections = array;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x001A62D4 File Offset: 0x001A44D4
		public override void RemoveConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					int num = this.connections.Length;
					Connection[] array = new Connection[num - 1];
					for (int j = 0; j < i; j++)
					{
						array[j] = this.connections[j];
					}
					for (int k = i + 1; k < num; k++)
					{
						array[k - 1] = this.connections[k];
					}
					this.connections = array;
					return;
				}
			}
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x001A6378 File Offset: 0x001A4578
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				if (path.CanTraverse(node))
				{
					PathNode pathNode2 = handler.GetPathNode(node);
					if (pathNode2.pathID != handler.PathID)
					{
						pathNode2.parent = pathNode;
						pathNode2.pathID = handler.PathID;
						pathNode2.cost = this.connections[i].cost;
						pathNode2.H = path.CalculateHScore(node);
						pathNode2.UpdateG(path);
						handler.heap.Add(pathNode2);
					}
					else
					{
						uint cost = this.connections[i].cost;
						if (pathNode.G + cost + path.GetTraversalCost(node) < pathNode2.G)
						{
							pathNode2.cost = cost;
							pathNode2.parent = pathNode;
							node.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x001A6468 File Offset: 0x001A4668
		public override int GetGizmoHashCode()
		{
			int num = base.GetGizmoHashCode();
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					num ^= 17 * this.connections[i].GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x001A64B5 File Offset: 0x001A46B5
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x001A64CA File Offset: 0x001A46CA
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x001A64E0 File Offset: 0x001A46E0
		public override void SerializeReferences(GraphSerializationContext ctx)
		{
			if (this.connections == null)
			{
				ctx.writer.Write(-1);
				return;
			}
			ctx.writer.Write(this.connections.Length);
			for (int i = 0; i < this.connections.Length; i++)
			{
				ctx.SerializeNodeReference(this.connections[i].node);
				ctx.writer.Write(this.connections[i].cost);
			}
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x001A655C File Offset: 0x001A475C
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.connections = null;
				return;
			}
			this.connections = new Connection[num];
			for (int i = 0; i < num; i++)
			{
				this.connections[i] = new Connection(ctx.DeserializeNodeReference(), ctx.reader.ReadUInt32(), byte.MaxValue);
			}
		}

		// Token: 0x0400419D RID: 16797
		public Connection[] connections;

		// Token: 0x0400419E RID: 16798
		public GameObject gameObject;
	}
}
