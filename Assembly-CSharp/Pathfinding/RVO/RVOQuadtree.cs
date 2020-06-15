using System;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005DC RID: 1500
	public class RVOQuadtree
	{
		// Token: 0x060028C2 RID: 10434 RVA: 0x001BDBC9 File Offset: 0x001BBDC9
		public void Clear()
		{
			this.nodes[0] = default(RVOQuadtree.Node);
			this.filledNodes = 1;
			this.maxRadius = 0f;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x001BDBEF File Offset: 0x001BBDEF
		public void SetBounds(Rect r)
		{
			this.bounds = r;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x001BDBF8 File Offset: 0x001BBDF8
		private int GetNodeIndex()
		{
			if (this.filledNodes == this.nodes.Length)
			{
				RVOQuadtree.Node[] array = new RVOQuadtree.Node[this.nodes.Length * 2];
				for (int i = 0; i < this.nodes.Length; i++)
				{
					array[i] = this.nodes[i];
				}
				this.nodes = array;
			}
			this.nodes[this.filledNodes] = default(RVOQuadtree.Node);
			this.nodes[this.filledNodes].child00 = this.filledNodes;
			this.filledNodes++;
			return this.filledNodes - 1;
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x001BDC9C File Offset: 0x001BBE9C
		public void Insert(Agent agent)
		{
			int num = 0;
			Rect r = this.bounds;
			Vector2 vector = new Vector2(agent.position.x, agent.position.y);
			agent.next = null;
			this.maxRadius = Math.Max(agent.radius, this.maxRadius);
			int num2 = 0;
			for (;;)
			{
				num2++;
				if (this.nodes[num].child00 == num)
				{
					if (this.nodes[num].count < 15 || num2 > 10)
					{
						break;
					}
					RVOQuadtree.Node node = this.nodes[num];
					node.child00 = this.GetNodeIndex();
					node.child01 = this.GetNodeIndex();
					node.child10 = this.GetNodeIndex();
					node.child11 = this.GetNodeIndex();
					this.nodes[num] = node;
					this.nodes[num].Distribute(this.nodes, r);
				}
				if (this.nodes[num].child00 != num)
				{
					Vector2 center = r.center;
					if (vector.x > center.x)
					{
						if (vector.y > center.y)
						{
							num = this.nodes[num].child11;
							r = Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax);
						}
						else
						{
							num = this.nodes[num].child10;
							r = Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y);
						}
					}
					else if (vector.y > center.y)
					{
						num = this.nodes[num].child01;
						r = Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax);
					}
					else
					{
						num = this.nodes[num].child00;
						r = Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y);
					}
				}
			}
			this.nodes[num].Add(agent);
			RVOQuadtree.Node[] array = this.nodes;
			int num3 = num;
			array[num3].count = array[num3].count + 1;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x001BDEE9 File Offset: 0x001BC0E9
		public void CalculateSpeeds()
		{
			this.nodes[0].CalculateMaxSpeed(this.nodes, 0);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x001BDF04 File Offset: 0x001BC104
		public void Query(Vector2 p, float speed, float timeHorizon, float agentRadius, Agent agent)
		{
			RVOQuadtree.QuadtreeQuery quadtreeQuery = default(RVOQuadtree.QuadtreeQuery);
			quadtreeQuery.p = p;
			quadtreeQuery.speed = speed;
			quadtreeQuery.timeHorizon = timeHorizon;
			quadtreeQuery.maxRadius = float.PositiveInfinity;
			quadtreeQuery.agentRadius = agentRadius;
			quadtreeQuery.agent = agent;
			quadtreeQuery.nodes = this.nodes;
			quadtreeQuery.QueryRec(0, this.bounds);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x001BDF6A File Offset: 0x001BC16A
		public void DebugDraw()
		{
			this.DebugDrawRec(0, this.bounds);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x001BDF7C File Offset: 0x001BC17C
		private void DebugDrawRec(int i, Rect r)
		{
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMin), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMin), Color.white);
			if (this.nodes[i].child00 != i)
			{
				Vector2 center = r.center;
				this.DebugDrawRec(this.nodes[i].child11, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
				this.DebugDrawRec(this.nodes[i].child10, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
				this.DebugDrawRec(this.nodes[i].child01, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
				this.DebugDrawRec(this.nodes[i].child00, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
			}
			for (Agent agent = this.nodes[i].linkedList; agent != null; agent = agent.next)
			{
				Vector2 position = this.nodes[i].linkedList.position;
				Debug.DrawLine(new Vector3(position.x, 0f, position.y) + Vector3.up, new Vector3(agent.position.x, 0f, agent.position.y) + Vector3.up, new Color(1f, 1f, 0f, 0.5f));
			}
		}

		// Token: 0x04004376 RID: 17270
		private const int LeafSize = 15;

		// Token: 0x04004377 RID: 17271
		private float maxRadius;

		// Token: 0x04004378 RID: 17272
		private RVOQuadtree.Node[] nodes = new RVOQuadtree.Node[42];

		// Token: 0x04004379 RID: 17273
		private int filledNodes = 1;

		// Token: 0x0400437A RID: 17274
		private Rect bounds;

		// Token: 0x02000785 RID: 1925
		private struct Node
		{
			// Token: 0x06002DEB RID: 11755 RVA: 0x001D4FCC File Offset: 0x001D31CC
			public void Add(Agent agent)
			{
				agent.next = this.linkedList;
				this.linkedList = agent;
			}

			// Token: 0x06002DEC RID: 11756 RVA: 0x001D4FE4 File Offset: 0x001D31E4
			public void Distribute(RVOQuadtree.Node[] nodes, Rect r)
			{
				Vector2 center = r.center;
				while (this.linkedList != null)
				{
					Agent next = this.linkedList.next;
					if (this.linkedList.position.x > center.x)
					{
						if (this.linkedList.position.y > center.y)
						{
							nodes[this.child11].Add(this.linkedList);
						}
						else
						{
							nodes[this.child10].Add(this.linkedList);
						}
					}
					else if (this.linkedList.position.y > center.y)
					{
						nodes[this.child01].Add(this.linkedList);
					}
					else
					{
						nodes[this.child00].Add(this.linkedList);
					}
					this.linkedList = next;
				}
				this.count = 0;
			}

			// Token: 0x06002DED RID: 11757 RVA: 0x001D50D0 File Offset: 0x001D32D0
			public float CalculateMaxSpeed(RVOQuadtree.Node[] nodes, int index)
			{
				if (this.child00 == index)
				{
					for (Agent next = this.linkedList; next != null; next = next.next)
					{
						this.maxSpeed = Math.Max(this.maxSpeed, next.CalculatedSpeed);
					}
				}
				else
				{
					this.maxSpeed = Math.Max(nodes[this.child00].CalculateMaxSpeed(nodes, this.child00), nodes[this.child01].CalculateMaxSpeed(nodes, this.child01));
					this.maxSpeed = Math.Max(this.maxSpeed, nodes[this.child10].CalculateMaxSpeed(nodes, this.child10));
					this.maxSpeed = Math.Max(this.maxSpeed, nodes[this.child11].CalculateMaxSpeed(nodes, this.child11));
				}
				return this.maxSpeed;
			}

			// Token: 0x04004B35 RID: 19253
			public int child00;

			// Token: 0x04004B36 RID: 19254
			public int child01;

			// Token: 0x04004B37 RID: 19255
			public int child10;

			// Token: 0x04004B38 RID: 19256
			public int child11;

			// Token: 0x04004B39 RID: 19257
			public Agent linkedList;

			// Token: 0x04004B3A RID: 19258
			public byte count;

			// Token: 0x04004B3B RID: 19259
			public float maxSpeed;
		}

		// Token: 0x02000786 RID: 1926
		private struct QuadtreeQuery
		{
			// Token: 0x06002DEE RID: 11758 RVA: 0x001D51A8 File Offset: 0x001D33A8
			public void QueryRec(int i, Rect r)
			{
				float num = Math.Min(Math.Max((this.nodes[i].maxSpeed + this.speed) * this.timeHorizon, this.agentRadius) + this.agentRadius, this.maxRadius);
				if (this.nodes[i].child00 == i)
				{
					for (Agent agent = this.nodes[i].linkedList; agent != null; agent = agent.next)
					{
						float num2 = this.agent.InsertAgentNeighbour(agent, num * num);
						if (num2 < this.maxRadius * this.maxRadius)
						{
							this.maxRadius = Mathf.Sqrt(num2);
						}
					}
					return;
				}
				Vector2 center = r.center;
				if (this.p.x - num < center.x)
				{
					if (this.p.y - num < center.y)
					{
						this.QueryRec(this.nodes[i].child00, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
						num = Math.Min(num, this.maxRadius);
					}
					if (this.p.y + num > center.y)
					{
						this.QueryRec(this.nodes[i].child01, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
						num = Math.Min(num, this.maxRadius);
					}
				}
				if (this.p.x + num > center.x)
				{
					if (this.p.y - num < center.y)
					{
						this.QueryRec(this.nodes[i].child10, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
						num = Math.Min(num, this.maxRadius);
					}
					if (this.p.y + num > center.y)
					{
						this.QueryRec(this.nodes[i].child11, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
					}
				}
			}

			// Token: 0x04004B3C RID: 19260
			public Vector2 p;

			// Token: 0x04004B3D RID: 19261
			public float speed;

			// Token: 0x04004B3E RID: 19262
			public float timeHorizon;

			// Token: 0x04004B3F RID: 19263
			public float agentRadius;

			// Token: 0x04004B40 RID: 19264
			public float maxRadius;

			// Token: 0x04004B41 RID: 19265
			public Agent agent;

			// Token: 0x04004B42 RID: 19266
			public RVOQuadtree.Node[] nodes;
		}
	}
}
