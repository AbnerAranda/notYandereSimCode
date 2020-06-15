using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200054E RID: 1358
	public abstract class MeshNode : GraphNode
	{
		// Token: 0x060023C5 RID: 9157 RVA: 0x0019A4CC File Offset: 0x001986CC
		protected MeshNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x060023C6 RID: 9158
		public abstract Int3 GetVertex(int i);

		// Token: 0x060023C7 RID: 9159
		public abstract int GetVertexCount();

		// Token: 0x060023C8 RID: 9160
		public abstract Vector3 ClosestPointOnNode(Vector3 p);

		// Token: 0x060023C9 RID: 9161
		public abstract Vector3 ClosestPointOnNodeXZ(Vector3 p);

		// Token: 0x060023CA RID: 9162 RVA: 0x0019A4D8 File Offset: 0x001986D8
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node != null)
					{
						this.connections[i].node.RemoveConnection(this);
					}
				}
			}
			ArrayPool<Connection>.Release(ref this.connections, true);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x0019A53C File Offset: 0x0019873C
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

		// Token: 0x060023CC RID: 9164 RVA: 0x0019A57C File Offset: 0x0019877C
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				if (node.Area != region)
				{
					node.Area = region;
					stack.Push(node);
				}
			}
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x0019A5D0 File Offset: 0x001987D0
		public override bool ContainsConnection(GraphNode node)
		{
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x0019A608 File Offset: 0x00198808
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

		// Token: 0x060023CF RID: 9167 RVA: 0x0019A675 File Offset: 0x00198875
		public override void AddConnection(GraphNode node, uint cost)
		{
			this.AddConnection(node, cost, -1);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x0019A680 File Offset: 0x00198880
		public void AddConnection(GraphNode node, uint cost, int shapeEdge)
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
						this.connections[i].shapeEdge = ((shapeEdge >= 0) ? ((byte)shapeEdge) : this.connections[i].shapeEdge);
						return;
					}
				}
			}
			int num = (this.connections != null) ? this.connections.Length : 0;
			Connection[] array = ArrayPool<Connection>.ClaimWithExactLength(num + 1);
			for (int j = 0; j < num; j++)
			{
				array[j] = this.connections[j];
			}
			array[num] = new Connection(node, cost, (byte)shapeEdge);
			if (this.connections != null)
			{
				ArrayPool<Connection>.Release(ref this.connections, true);
			}
			this.connections = array;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x0019A76C File Offset: 0x0019896C
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
					Connection[] array = ArrayPool<Connection>.ClaimWithExactLength(num - 1);
					for (int j = 0; j < i; j++)
					{
						array[j] = this.connections[j];
					}
					for (int k = i + 1; k < num; k++)
					{
						array[k - 1] = this.connections[k];
					}
					if (this.connections != null)
					{
						ArrayPool<Connection>.Release(ref this.connections, true);
					}
					this.connections = array;
					return;
				}
			}
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x0019A825 File Offset: 0x00198A25
		public virtual bool ContainsPoint(Int3 point)
		{
			return this.ContainsPoint((Vector3)point);
		}

		// Token: 0x060023D3 RID: 9171
		public abstract bool ContainsPoint(Vector3 point);

		// Token: 0x060023D4 RID: 9172
		public abstract bool ContainsPointInGraphSpace(Int3 point);

		// Token: 0x060023D5 RID: 9173 RVA: 0x0019A834 File Offset: 0x00198A34
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

		// Token: 0x060023D6 RID: 9174 RVA: 0x0019A884 File Offset: 0x00198A84
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
				ctx.writer.Write(this.connections[i].shapeEdge);
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x0019A91C File Offset: 0x00198B1C
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.connections = null;
				return;
			}
			this.connections = ArrayPool<Connection>.ClaimWithExactLength(num);
			for (int i = 0; i < num; i++)
			{
				this.connections[i] = new Connection(ctx.DeserializeNodeReference(), ctx.reader.ReadUInt32(), (ctx.meta.version < AstarSerializer.V4_1_0) ? byte.MaxValue : ctx.reader.ReadByte());
			}
		}

		// Token: 0x0400404F RID: 16463
		public Connection[] connections;
	}
}
