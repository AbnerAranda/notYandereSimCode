using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000542 RID: 1346
	public class NodeLink3Node : PointNode
	{
		// Token: 0x06002344 RID: 9028 RVA: 0x0019825F File Offset: 0x0019645F
		public NodeLink3Node(AstarPath active) : base(active)
		{
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x00198268 File Offset: 0x00196468
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (this.connections.Length < 2)
			{
				return false;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length);
			}
			if (left != null)
			{
				left.Add(this.portalA);
				right.Add(this.portalB);
			}
			return true;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x001982C8 File Offset: 0x001964C8
		public GraphNode GetOther(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length);
			}
			if (a != this.connections[0].node)
			{
				return (this.connections[0].node as NodeLink3Node).GetOtherInternal(this);
			}
			return (this.connections[1].node as NodeLink3Node).GetOtherInternal(this);
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x00198358 File Offset: 0x00196558
		private GraphNode GetOtherInternal(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			if (a != this.connections[0].node)
			{
				return this.connections[0].node;
			}
			return this.connections[1].node;
		}

		// Token: 0x04004007 RID: 16391
		public NodeLink3 link;

		// Token: 0x04004008 RID: 16392
		public Vector3 portalA;

		// Token: 0x04004009 RID: 16393
		public Vector3 portalB;
	}
}
