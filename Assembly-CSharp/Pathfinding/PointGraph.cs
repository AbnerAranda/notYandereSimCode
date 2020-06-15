using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000589 RID: 1417
	[JsonOptIn]
	public class PointGraph : NavGraph, IUpdatableGraph
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x001A6FF1 File Offset: 0x001A51F1
		// (set) Token: 0x06002605 RID: 9733 RVA: 0x001A6FF9 File Offset: 0x001A51F9
		public int nodeCount { get; protected set; }

		// Token: 0x06002606 RID: 9734 RVA: 0x001A7002 File Offset: 0x001A5202
		public override int CountNodes()
		{
			return this.nodeCount;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x001A700C File Offset: 0x001A520C
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			int nodeCount = this.nodeCount;
			for (int i = 0; i < nodeCount; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x001A7043 File Offset: 0x001A5243
		public override NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			return this.GetNearestInternal(position, constraint, true);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x001A704E File Offset: 0x001A524E
		public override NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearestInternal(position, constraint, false);
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x001A705C File Offset: 0x001A525C
		private NNInfoInternal GetNearestInternal(Vector3 position, NNConstraint constraint, bool fastCheck)
		{
			if (this.nodes == null)
			{
				return default(NNInfoInternal);
			}
			if (this.optimizeForSparseGraph)
			{
				return new NNInfoInternal(this.lookupTree.GetNearest((Int3)position, fastCheck ? null : constraint));
			}
			float num = (constraint == null || constraint.constrainDistance) ? AstarPath.active.maxNearestNodeDistanceSqr : float.PositiveInfinity;
			NNInfoInternal nninfoInternal = new NNInfoInternal(null);
			float num2 = float.PositiveInfinity;
			float num3 = float.PositiveInfinity;
			for (int i = 0; i < this.nodeCount; i++)
			{
				PointNode pointNode = this.nodes[i];
				float sqrMagnitude = (position - (Vector3)pointNode.position).sqrMagnitude;
				if (sqrMagnitude < num2)
				{
					num2 = sqrMagnitude;
					nninfoInternal.node = pointNode;
				}
				if (sqrMagnitude < num3 && sqrMagnitude < num && (constraint == null || constraint.Suitable(pointNode)))
				{
					num3 = sqrMagnitude;
					nninfoInternal.constrainedNode = pointNode;
				}
			}
			if (!fastCheck)
			{
				nninfoInternal.node = nninfoInternal.constrainedNode;
			}
			nninfoInternal.UpdateInfo();
			return nninfoInternal;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x001A715C File Offset: 0x001A535C
		public PointNode AddNode(Int3 position)
		{
			return this.AddNode<PointNode>(new PointNode(this.active), position);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x001A7170 File Offset: 0x001A5370
		public T AddNode<T>(T node, Int3 position) where T : PointNode
		{
			if (this.nodes == null || this.nodeCount == this.nodes.Length)
			{
				PointNode[] array = new PointNode[(this.nodes != null) ? Math.Max(this.nodes.Length + 4, this.nodes.Length * 2) : 4];
				if (this.nodes != null)
				{
					this.nodes.CopyTo(array, 0);
				}
				this.nodes = array;
			}
			node.SetPosition(position);
			node.GraphIndex = this.graphIndex;
			node.Walkable = true;
			this.nodes[this.nodeCount] = node;
			int nodeCount = this.nodeCount;
			this.nodeCount = nodeCount + 1;
			if (this.optimizeForSparseGraph)
			{
				this.AddToLookup(node);
			}
			return node;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x001A723C File Offset: 0x001A543C
		protected static int CountChildren(Transform tr)
		{
			int num = 0;
			foreach (object obj in tr)
			{
				Transform tr2 = (Transform)obj;
				num++;
				num += PointGraph.CountChildren(tr2);
			}
			return num;
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x001A729C File Offset: 0x001A549C
		protected void AddChildren(ref int c, Transform tr)
		{
			foreach (object obj in tr)
			{
				Transform transform = (Transform)obj;
				this.nodes[c].position = (Int3)transform.position;
				this.nodes[c].Walkable = true;
				this.nodes[c].gameObject = transform.gameObject;
				c++;
				this.AddChildren(ref c, transform);
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x001A7334 File Offset: 0x001A5534
		public void RebuildNodeLookup()
		{
			if (!this.optimizeForSparseGraph || this.nodes == null)
			{
				this.lookupTree = new PointKDTree();
				return;
			}
			PointKDTree pointKDTree = this.lookupTree;
			GraphNode[] array = this.nodes;
			pointKDTree.Rebuild(array, 0, this.nodeCount);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x001A7377 File Offset: 0x001A5577
		private void AddToLookup(PointNode node)
		{
			this.lookupTree.Add(node);
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x001A7388 File Offset: 0x001A5588
		protected virtual PointNode[] CreateNodes(int count)
		{
			PointNode[] array = new PointNode[count];
			for (int i = 0; i < this.nodeCount; i++)
			{
				array[i] = new PointNode(this.active);
			}
			return array;
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x001A73BC File Offset: 0x001A55BC
		protected override IEnumerable<Progress> ScanInternal()
		{
			yield return new Progress(0f, "Searching for GameObjects");
			if (this.root == null)
			{
				GameObject[] gos = (this.searchTag != null) ? GameObject.FindGameObjectsWithTag(this.searchTag) : null;
				if (gos == null)
				{
					this.nodes = new PointNode[0];
					this.nodeCount = 0;
					yield break;
				}
				yield return new Progress(0.1f, "Creating nodes");
				this.nodeCount = gos.Length;
				this.nodes = this.CreateNodes(this.nodeCount);
				for (int i = 0; i < gos.Length; i++)
				{
					this.nodes[i].position = (Int3)gos[i].transform.position;
					this.nodes[i].Walkable = true;
					this.nodes[i].gameObject = gos[i].gameObject;
				}
				gos = null;
			}
			else
			{
				if (!this.recursive)
				{
					this.nodeCount = this.root.childCount;
					this.nodes = this.CreateNodes(this.nodeCount);
					int num = 0;
					using (IEnumerator enumerator = this.root.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							Transform transform = (Transform)obj;
							this.nodes[num].position = (Int3)transform.position;
							this.nodes[num].Walkable = true;
							this.nodes[num].gameObject = transform.gameObject;
							num++;
						}
						goto IL_24A;
					}
				}
				this.nodeCount = PointGraph.CountChildren(this.root);
				this.nodes = this.CreateNodes(this.nodeCount);
				int num2 = 0;
				this.AddChildren(ref num2, this.root);
			}
			IL_24A:
			if (this.optimizeForSparseGraph)
			{
				yield return new Progress(0.15f, "Building node lookup");
				this.RebuildNodeLookup();
			}
			foreach (Progress progress in this.ConnectNodesAsync())
			{
				yield return progress.MapTo(0.16f, 1f, null);
			}
			IEnumerator<Progress> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x001A73CC File Offset: 0x001A55CC
		public void ConnectNodes()
		{
			foreach (Progress progress in this.ConnectNodesAsync())
			{
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x001A7414 File Offset: 0x001A5614
		private IEnumerable<Progress> ConnectNodesAsync()
		{
			if (this.maxDistance >= 0f)
			{
				List<Connection> connections = new List<Connection>();
				List<GraphNode> candidateConnections = new List<GraphNode>();
				long maxSquaredRange;
				if (this.maxDistance == 0f && (this.limits.x == 0f || this.limits.y == 0f || this.limits.z == 0f))
				{
					maxSquaredRange = long.MaxValue;
				}
				else
				{
					maxSquaredRange = (long)(Mathf.Max(this.limits.x, Mathf.Max(this.limits.y, Mathf.Max(this.limits.z, this.maxDistance))) * 1000f) + 1L;
					maxSquaredRange *= maxSquaredRange;
				}
				int num3;
				for (int i = 0; i < this.nodeCount; i = num3 + 1)
				{
					if (i % 512 == 0)
					{
						yield return new Progress((float)i / (float)this.nodes.Length, "Connecting nodes");
					}
					connections.Clear();
					PointNode pointNode = this.nodes[i];
					if (this.optimizeForSparseGraph)
					{
						candidateConnections.Clear();
						this.lookupTree.GetInRange(pointNode.position, maxSquaredRange, candidateConnections);
						for (int j = 0; j < candidateConnections.Count; j++)
						{
							PointNode pointNode2 = candidateConnections[j] as PointNode;
							float num;
							if (pointNode2 != pointNode && this.IsValidConnection(pointNode, pointNode2, out num))
							{
								connections.Add(new Connection(pointNode2, (uint)Mathf.RoundToInt(num * 1000f), byte.MaxValue));
							}
						}
					}
					else
					{
						for (int k = 0; k < this.nodeCount; k++)
						{
							if (i != k)
							{
								PointNode pointNode3 = this.nodes[k];
								float num2;
								if (this.IsValidConnection(pointNode, pointNode3, out num2))
								{
									connections.Add(new Connection(pointNode3, (uint)Mathf.RoundToInt(num2 * 1000f), byte.MaxValue));
								}
							}
						}
					}
					pointNode.connections = connections.ToArray();
					num3 = i;
				}
				connections = null;
				candidateConnections = null;
			}
			yield break;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x001A7424 File Offset: 0x001A5624
		public virtual bool IsValidConnection(GraphNode a, GraphNode b, out float dist)
		{
			dist = 0f;
			if (!a.Walkable || !b.Walkable)
			{
				return false;
			}
			Vector3 vector = (Vector3)(b.position - a.position);
			if ((!Mathf.Approximately(this.limits.x, 0f) && Mathf.Abs(vector.x) > this.limits.x) || (!Mathf.Approximately(this.limits.y, 0f) && Mathf.Abs(vector.y) > this.limits.y) || (!Mathf.Approximately(this.limits.z, 0f) && Mathf.Abs(vector.z) > this.limits.z))
			{
				return false;
			}
			dist = vector.magnitude;
			if (this.maxDistance != 0f && dist >= this.maxDistance)
			{
				return false;
			}
			if (!this.raycast)
			{
				return true;
			}
			Ray ray = new Ray((Vector3)a.position, vector);
			Ray ray2 = new Ray((Vector3)b.position, -vector);
			if (this.use2DPhysics)
			{
				if (this.thickRaycast)
				{
					return !Physics2D.CircleCast(ray.origin, this.thickRaycastRadius, ray.direction, dist, this.mask) && !Physics2D.CircleCast(ray2.origin, this.thickRaycastRadius, ray2.direction, dist, this.mask);
				}
				return !Physics2D.Linecast((Vector3)a.position, (Vector3)b.position, this.mask) && !Physics2D.Linecast((Vector3)b.position, (Vector3)a.position, this.mask);
			}
			else
			{
				if (this.thickRaycast)
				{
					return !Physics.SphereCast(ray, this.thickRaycastRadius, dist, this.mask) && !Physics.SphereCast(ray2, this.thickRaycastRadius, dist, this.mask);
				}
				return !Physics.Linecast((Vector3)a.position, (Vector3)b.position, this.mask) && !Physics.Linecast((Vector3)b.position, (Vector3)a.position, this.mask);
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x0002D199 File Offset: 0x0002B399
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			return GraphUpdateThreading.UnityThread;
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject o)
		{
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x001A76DC File Offset: 0x001A58DC
		void IUpdatableGraph.UpdateArea(GraphUpdateObject guo)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodeCount; i++)
			{
				PointNode pointNode = this.nodes[i];
				if (guo.bounds.Contains((Vector3)pointNode.position))
				{
					guo.WillUpdateNode(pointNode);
					guo.Apply(pointNode);
				}
			}
			if (guo.updatePhysics)
			{
				Bounds bounds = guo.bounds;
				if (this.thickRaycast)
				{
					bounds.Expand(this.thickRaycastRadius * 2f);
				}
				List<Connection> list = ListPool<Connection>.Claim();
				for (int j = 0; j < this.nodeCount; j++)
				{
					PointNode pointNode2 = this.nodes[j];
					Vector3 a = (Vector3)pointNode2.position;
					List<Connection> list2 = null;
					for (int k = 0; k < this.nodeCount; k++)
					{
						if (k != j)
						{
							Vector3 b = (Vector3)this.nodes[k].position;
							if (VectorMath.SegmentIntersectsBounds(bounds, a, b))
							{
								PointNode pointNode3 = this.nodes[k];
								bool flag = pointNode2.ContainsConnection(pointNode3);
								float num;
								bool flag2 = this.IsValidConnection(pointNode2, pointNode3, out num);
								if (list2 == null && flag != flag2)
								{
									list.Clear();
									list2 = list;
									list2.AddRange(pointNode2.connections);
								}
								if (!flag && flag2)
								{
									uint cost = (uint)Mathf.RoundToInt(num * 1000f);
									list2.Add(new Connection(pointNode3, cost, byte.MaxValue));
								}
								else if (flag && !flag2)
								{
									for (int l = 0; l < list2.Count; l++)
									{
										if (list2[l].node == pointNode3)
										{
											list2.RemoveAt(l);
											break;
										}
									}
								}
							}
						}
					}
					if (list2 != null)
					{
						pointNode2.connections = list2.ToArray();
					}
				}
				ListPool<Connection>.Release(ref list);
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x001A78AD File Offset: 0x001A5AAD
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			this.RebuildNodeLookup();
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x001A78B5 File Offset: 0x001A5AB5
		public override void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			base.RelocateNodes(deltaMatrix);
			this.RebuildNodeLookup();
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x001A78C4 File Offset: 0x001A5AC4
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.root = (ctx.DeserializeUnityObject() as Transform);
			this.searchTag = ctx.reader.ReadString();
			this.maxDistance = ctx.reader.ReadSingle();
			this.limits = ctx.DeserializeVector3();
			this.raycast = ctx.reader.ReadBoolean();
			this.use2DPhysics = ctx.reader.ReadBoolean();
			this.thickRaycast = ctx.reader.ReadBoolean();
			this.thickRaycastRadius = ctx.reader.ReadSingle();
			this.recursive = ctx.reader.ReadBoolean();
			ctx.reader.ReadBoolean();
			this.mask = ctx.reader.ReadInt32();
			this.optimizeForSparseGraph = ctx.reader.ReadBoolean();
			ctx.reader.ReadBoolean();
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x001A79AC File Offset: 0x001A5BAC
		protected override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			if (this.nodes == null)
			{
				ctx.writer.Write(-1);
			}
			ctx.writer.Write(this.nodeCount);
			for (int i = 0; i < this.nodeCount; i++)
			{
				if (this.nodes[i] == null)
				{
					ctx.writer.Write(-1);
				}
				else
				{
					ctx.writer.Write(0);
					this.nodes[i].SerializeNode(ctx);
				}
			}
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x001A7A24 File Offset: 0x001A5C24
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.nodes = null;
				return;
			}
			this.nodes = new PointNode[num];
			this.nodeCount = num;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (ctx.reader.ReadInt32() != -1)
				{
					this.nodes[i] = new PointNode(this.active);
					this.nodes[i].DeserializeNode(ctx);
				}
			}
		}

		// Token: 0x040041A4 RID: 16804
		[JsonMember]
		public Transform root;

		// Token: 0x040041A5 RID: 16805
		[JsonMember]
		public string searchTag;

		// Token: 0x040041A6 RID: 16806
		[JsonMember]
		public float maxDistance;

		// Token: 0x040041A7 RID: 16807
		[JsonMember]
		public Vector3 limits;

		// Token: 0x040041A8 RID: 16808
		[JsonMember]
		public bool raycast = true;

		// Token: 0x040041A9 RID: 16809
		[JsonMember]
		public bool use2DPhysics;

		// Token: 0x040041AA RID: 16810
		[JsonMember]
		public bool thickRaycast;

		// Token: 0x040041AB RID: 16811
		[JsonMember]
		public float thickRaycastRadius = 1f;

		// Token: 0x040041AC RID: 16812
		[JsonMember]
		public bool recursive = true;

		// Token: 0x040041AD RID: 16813
		[JsonMember]
		public LayerMask mask;

		// Token: 0x040041AE RID: 16814
		[JsonMember]
		public bool optimizeForSparseGraph;

		// Token: 0x040041AF RID: 16815
		private PointKDTree lookupTree = new PointKDTree();

		// Token: 0x040041B0 RID: 16816
		public PointNode[] nodes;
	}
}
