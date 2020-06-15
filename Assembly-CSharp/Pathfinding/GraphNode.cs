using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200054D RID: 1357
	public abstract class GraphNode
	{
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x0019A185 File Offset: 0x00198385
		public NavGraph Graph
		{
			get
			{
				if (!this.Destroyed)
				{
					return AstarData.GetGraph(this);
				}
				return null;
			}
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0019A197 File Offset: 0x00198397
		protected GraphNode(AstarPath astar)
		{
			if (astar != null)
			{
				this.nodeIndex = astar.GetNewNodeIndex();
				astar.InitializeNode(this);
				return;
			}
			throw new Exception("No active AstarPath object to bind to");
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0019A1C0 File Offset: 0x001983C0
		internal void Destroy()
		{
			if (this.Destroyed)
			{
				return;
			}
			this.ClearConnections(true);
			if (AstarPath.active != null)
			{
				AstarPath.active.DestroyNode(this);
			}
			this.NodeIndex = 268435454;
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x0019A1F5 File Offset: 0x001983F5
		public bool Destroyed
		{
			get
			{
				return this.NodeIndex == 268435454;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x0019A204 File Offset: 0x00198404
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x0019A212 File Offset: 0x00198412
		public int NodeIndex
		{
			get
			{
				return this.nodeIndex & 268435455;
			}
			private set
			{
				this.nodeIndex = ((this.nodeIndex & -268435456) | value);
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x0019A228 File Offset: 0x00198428
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x0019A239 File Offset: 0x00198439
		internal bool TemporaryFlag1
		{
			get
			{
				return (this.nodeIndex & 268435456) != 0;
			}
			set
			{
				this.nodeIndex = ((this.nodeIndex & -268435457) | (value ? 268435456 : 0));
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0019A259 File Offset: 0x00198459
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x0019A26A File Offset: 0x0019846A
		internal bool TemporaryFlag2
		{
			get
			{
				return (this.nodeIndex & 536870912) != 0;
			}
			set
			{
				this.nodeIndex = ((this.nodeIndex & -536870913) | (value ? 536870912 : 0));
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x0019A28A File Offset: 0x0019848A
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x0019A292 File Offset: 0x00198492
		public uint Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0019A29B File Offset: 0x0019849B
		// (set) Token: 0x060023AB RID: 9131 RVA: 0x0019A2A3 File Offset: 0x001984A3
		public uint Penalty
		{
			get
			{
				return this.penalty;
			}
			set
			{
				if (value > 16777215U)
				{
					Debug.LogWarning("Very high penalty applied. Are you sure negative values haven't underflowed?\nPenalty values this high could with long paths cause overflows and in some cases infinity loops because of that.\nPenalty value applied: " + value);
				}
				this.penalty = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0019A2C9 File Offset: 0x001984C9
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x0019A2D6 File Offset: 0x001984D6
		public bool Walkable
		{
			get
			{
				return (this.flags & 1U) > 0U;
			}
			set
			{
				this.flags = ((this.flags & 4294967294U) | (value ? 1U : 0U));
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x0019A2EF File Offset: 0x001984EF
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x0019A2FF File Offset: 0x001984FF
		public uint Area
		{
			get
			{
				return (this.flags & 262142U) >> 1;
			}
			set
			{
				this.flags = ((this.flags & 4294705153U) | value << 1);
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0019A317 File Offset: 0x00198517
		// (set) Token: 0x060023B1 RID: 9137 RVA: 0x0019A328 File Offset: 0x00198528
		public uint GraphIndex
		{
			get
			{
				return (this.flags & 4278190080U) >> 24;
			}
			set
			{
				this.flags = ((this.flags & 16777215U) | value << 24);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x0019A341 File Offset: 0x00198541
		// (set) Token: 0x060023B3 RID: 9139 RVA: 0x0019A352 File Offset: 0x00198552
		public uint Tag
		{
			get
			{
				return (this.flags & 16252928U) >> 19;
			}
			set
			{
				this.flags = ((this.flags & 4278714367U) | value << 19);
			}
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0019A36C File Offset: 0x0019856C
		public virtual void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			this.GetConnections(delegate(GraphNode other)
			{
				PathNode pathNode2 = handler.GetPathNode(other);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					other.UpdateRecursiveG(path, pathNode2, handler);
				}
			});
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x0019A3D0 File Offset: 0x001985D0
		public virtual void FloodFill(Stack<GraphNode> stack, uint region)
		{
			this.GetConnections(delegate(GraphNode other)
			{
				if (other.Area != region)
				{
					other.Area = region;
					stack.Push(other);
				}
			});
		}

		// Token: 0x060023B6 RID: 9142
		public abstract void GetConnections(Action<GraphNode> action);

		// Token: 0x060023B7 RID: 9143
		public abstract void AddConnection(GraphNode node, uint cost);

		// Token: 0x060023B8 RID: 9144
		public abstract void RemoveConnection(GraphNode node);

		// Token: 0x060023B9 RID: 9145
		public abstract void ClearConnections(bool alsoReverse);

		// Token: 0x060023BA RID: 9146 RVA: 0x0019A404 File Offset: 0x00198604
		public virtual bool ContainsConnection(GraphNode node)
		{
			bool contains = false;
			this.GetConnections(delegate(GraphNode neighbour)
			{
				contains |= (neighbour == node);
			});
			return contains;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void RecalculateConnectionCosts()
		{
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0002D199 File Offset: 0x0002B399
		public virtual bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			return false;
		}

		// Token: 0x060023BD RID: 9149
		public abstract void Open(Path path, PathNode pathNode, PathHandler handler);

		// Token: 0x060023BE RID: 9150 RVA: 0x0019A43D File Offset: 0x0019863D
		public virtual float SurfaceArea()
		{
			return 0f;
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x0019A444 File Offset: 0x00198644
		public virtual Vector3 RandomPointOnSurface()
		{
			return (Vector3)this.position;
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x0019A451 File Offset: 0x00198651
		public virtual int GetGizmoHashCode()
		{
			return this.position.GetHashCode() ^ (int)(19U * this.Penalty) ^ (int)(41U * this.flags);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x0019A478 File Offset: 0x00198678
		public virtual void SerializeNode(GraphSerializationContext ctx)
		{
			ctx.writer.Write(this.Penalty);
			ctx.writer.Write(this.Flags);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x0019A49C File Offset: 0x0019869C
		public virtual void DeserializeNode(GraphSerializationContext ctx)
		{
			this.Penalty = ctx.reader.ReadUInt32();
			this.Flags = ctx.reader.ReadUInt32();
			this.GraphIndex = ctx.graphIndex;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void SerializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void DeserializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0400403D RID: 16445
		private int nodeIndex;

		// Token: 0x0400403E RID: 16446
		protected uint flags;

		// Token: 0x0400403F RID: 16447
		private uint penalty;

		// Token: 0x04004040 RID: 16448
		private const int NodeIndexMask = 268435455;

		// Token: 0x04004041 RID: 16449
		private const int DestroyedNodeIndex = 268435454;

		// Token: 0x04004042 RID: 16450
		private const int TemporaryFlag1Mask = 268435456;

		// Token: 0x04004043 RID: 16451
		private const int TemporaryFlag2Mask = 536870912;

		// Token: 0x04004044 RID: 16452
		public Int3 position;

		// Token: 0x04004045 RID: 16453
		private const int FlagsWalkableOffset = 0;

		// Token: 0x04004046 RID: 16454
		private const uint FlagsWalkableMask = 1U;

		// Token: 0x04004047 RID: 16455
		private const int FlagsAreaOffset = 1;

		// Token: 0x04004048 RID: 16456
		private const uint FlagsAreaMask = 262142U;

		// Token: 0x04004049 RID: 16457
		private const int FlagsGraphOffset = 24;

		// Token: 0x0400404A RID: 16458
		private const uint FlagsGraphMask = 4278190080U;

		// Token: 0x0400404B RID: 16459
		public const uint MaxAreaIndex = 131071U;

		// Token: 0x0400404C RID: 16460
		public const uint MaxGraphIndex = 255U;

		// Token: 0x0400404D RID: 16461
		private const int FlagsTagOffset = 19;

		// Token: 0x0400404E RID: 16462
		private const uint FlagsTagMask = 16252928U;
	}
}
