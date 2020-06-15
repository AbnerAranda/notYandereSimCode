using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000540 RID: 1344
	[AddComponentMenu("Pathfinding/Link")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_node_link.php")]
	public class NodeLink : GraphModifier
	{
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x0019765D File Offset: 0x0019585D
		public Transform Start
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x00197665 File Offset: 0x00195865
		public Transform End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0019766D File Offset: 0x0019586D
		public override void OnPostScan()
		{
			if (AstarPath.active.isScanning)
			{
				this.InternalOnPostScan();
				return;
			}
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
			{
				this.InternalOnPostScan();
				return true;
			}));
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x0019769D File Offset: 0x0019589D
		public void InternalOnPostScan()
		{
			this.Apply();
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x001976A5 File Offset: 0x001958A5
		public override void OnGraphsPostUpdate()
		{
			if (!AstarPath.active.isScanning)
			{
				AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
				{
					this.InternalOnPostScan();
					return true;
				}));
			}
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x001976D0 File Offset: 0x001958D0
		public virtual void Apply()
		{
			if (this.Start == null || this.End == null || AstarPath.active == null)
			{
				return;
			}
			GraphNode node = AstarPath.active.GetNearest(this.Start.position).node;
			GraphNode node2 = AstarPath.active.GetNearest(this.End.position).node;
			if (node == null || node2 == null)
			{
				return;
			}
			if (this.deleteConnection)
			{
				node.RemoveConnection(node2);
				if (!this.oneWay)
				{
					node2.RemoveConnection(node);
					return;
				}
			}
			else
			{
				uint cost = (uint)Math.Round((double)((float)(node.position - node2.position).costMagnitude * this.costFactor));
				node.AddConnection(node2, cost);
				if (!this.oneWay)
				{
					node2.AddConnection(node, cost);
				}
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x001977A4 File Offset: 0x001959A4
		public void OnDrawGizmos()
		{
			if (this.Start == null || this.End == null)
			{
				return;
			}
			Draw.Gizmos.Bezier(this.Start.position, this.End.position, this.deleteConnection ? Color.red : Color.green);
		}

		// Token: 0x04003FF6 RID: 16374
		public Transform end;

		// Token: 0x04003FF7 RID: 16375
		public float costFactor = 1f;

		// Token: 0x04003FF8 RID: 16376
		public bool oneWay;

		// Token: 0x04003FF9 RID: 16377
		public bool deleteConnection;
	}
}
