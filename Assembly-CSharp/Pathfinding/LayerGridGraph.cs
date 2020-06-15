using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200057D RID: 1405
	public class LayerGridGraph : GridGraph, IUpdatableGraph
	{
		// Token: 0x0600252F RID: 9519 RVA: 0x001A0DE3 File Offset: 0x0019EFE3
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.RemoveGridGraphFromStatic();
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x001A0DF1 File Offset: 0x0019EFF1
		private void RemoveGridGraphFromStatic()
		{
			LevelGridNode.SetGridGraph(this.active.data.GetGraphIndex(this), null);
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x0002D199 File Offset: 0x0002B399
		public override bool uniformWidthDepthGrid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x001A0E0A File Offset: 0x0019F00A
		public override int LayerCount
		{
			get
			{
				return this.layerCount;
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x001A0E14 File Offset: 0x0019F014
		public override int CountNodes()
		{
			if (this.nodes == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] != null)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x001A0E50 File Offset: 0x0019F050
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] != null)
				{
					action(this.nodes[i]);
				}
			}
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x001A0E94 File Offset: 0x0019F094
		protected override List<GraphNode> GetNodesInRegion(Bounds b, GraphUpdateShape shape)
		{
			IntRect rectFromBounds = base.GetRectFromBounds(b);
			if (this.nodes == null || !rectFromBounds.IsValid() || this.nodes.Length != this.width * this.depth * this.layerCount)
			{
				return ListPool<GraphNode>.Claim();
			}
			List<GraphNode> list = ListPool<GraphNode>.Claim(rectFromBounds.Width * rectFromBounds.Height * this.layerCount);
			for (int i = 0; i < this.layerCount; i++)
			{
				int num = i * this.width * this.depth;
				for (int j = rectFromBounds.xmin; j <= rectFromBounds.xmax; j++)
				{
					for (int k = rectFromBounds.ymin; k <= rectFromBounds.ymax; k++)
					{
						int num2 = num + k * this.width + j;
						GraphNode graphNode = this.nodes[num2];
						if (graphNode != null && b.Contains((Vector3)graphNode.position) && (shape == null || shape.Contains((Vector3)graphNode.position)))
						{
							list.Add(graphNode);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x001A0FAC File Offset: 0x0019F1AC
		public override List<GraphNode> GetNodesInRegion(IntRect rect)
		{
			List<GraphNode> list = ListPool<GraphNode>.Claim();
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			rect = IntRect.Intersection(rect, b);
			if (this.nodes == null || !rect.IsValid() || this.nodes.Length != this.width * this.depth * this.layerCount)
			{
				return list;
			}
			for (int i = 0; i < this.layerCount; i++)
			{
				int num = i * base.Width * base.Depth;
				for (int j = rect.ymin; j <= rect.ymax; j++)
				{
					int num2 = num + j * base.Width;
					for (int k = rect.xmin; k <= rect.xmax; k++)
					{
						LevelGridNode levelGridNode = this.nodes[num2 + k];
						if (levelGridNode != null)
						{
							list.Add(levelGridNode);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x001A1090 File Offset: 0x0019F290
		public override int GetNodesInRegion(IntRect rect, GridNodeBase[] buffer)
		{
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			rect = IntRect.Intersection(rect, b);
			if (this.nodes == null || !rect.IsValid() || this.nodes.Length != this.width * this.depth * this.layerCount)
			{
				return 0;
			}
			int num = 0;
			try
			{
				for (int i = 0; i < this.layerCount; i++)
				{
					int num2 = i * base.Width * base.Depth;
					for (int j = rect.ymin; j <= rect.ymax; j++)
					{
						int num3 = num2 + j * base.Width;
						for (int k = rect.xmin; k <= rect.xmax; k++)
						{
							LevelGridNode levelGridNode = this.nodes[num3 + k];
							if (levelGridNode != null)
							{
								buffer[num] = levelGridNode;
								num++;
							}
						}
					}
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new ArgumentException("Buffer is too small");
			}
			return num;
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x001A1190 File Offset: 0x0019F390
		public override GridNodeBase GetNode(int x, int z)
		{
			if (x < 0 || z < 0 || x >= this.width || z >= this.depth)
			{
				return null;
			}
			return this.nodes[x + z * this.width];
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x001A11C0 File Offset: 0x0019F3C0
		public GridNodeBase GetNode(int x, int z, int layer)
		{
			if (x < 0 || z < 0 || x >= this.width || z >= this.depth || layer < 0 || layer >= this.layerCount)
			{
				return null;
			}
			return this.nodes[x + z * this.width + layer * this.width * this.depth];
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x001A1218 File Offset: 0x0019F418
		void IUpdatableGraph.UpdateArea(GraphUpdateObject o)
		{
			if (this.nodes == null || this.nodes.Length != this.width * this.depth * this.layerCount)
			{
				Debug.LogWarning("The Grid Graph is not scanned, cannot update area ");
				return;
			}
			IntRect a;
			IntRect a2;
			IntRect intRect;
			bool flag;
			int num;
			base.CalculateAffectedRegions(o, out a, out a2, out intRect, out flag, out num);
			bool flag2 = o is LayerGridGraphUpdate && ((LayerGridGraphUpdate)o).recalculateNodes;
			bool flag3 = (o is LayerGridGraphUpdate) ? ((LayerGridGraphUpdate)o).preserveExistingNodes : (!o.resetPenaltyOnPhysics);
			if (o.trackChangedNodes && flag2)
			{
				Debug.LogError("Cannot track changed nodes when creating or deleting nodes.\nWill not update LayerGridGraph");
				return;
			}
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			IntRect intRect2 = IntRect.Intersection(a2, b);
			if (!flag2)
			{
				for (int i = intRect2.xmin; i <= intRect2.xmax; i++)
				{
					for (int j = intRect2.ymin; j <= intRect2.ymax; j++)
					{
						for (int k = 0; k < this.layerCount; k++)
						{
							o.WillUpdateNode(this.nodes[k * this.width * this.depth + j * this.width + i]);
						}
					}
				}
			}
			if (o.updatePhysics && !o.modifyWalkability)
			{
				this.collision.Initialize(base.transform, this.nodeSize);
				intRect2 = IntRect.Intersection(intRect, b);
				for (int l = intRect2.xmin; l <= intRect2.xmax; l++)
				{
					for (int m = intRect2.ymin; m <= intRect2.ymax; m++)
					{
						this.RecalculateCell(l, m, !flag3, false);
					}
				}
				for (int n = intRect2.xmin; n <= intRect2.xmax; n++)
				{
					for (int num2 = intRect2.ymin; num2 <= intRect2.ymax; num2++)
					{
						this.CalculateConnections(n, num2);
					}
				}
			}
			intRect2 = IntRect.Intersection(a, b);
			for (int num3 = intRect2.xmin; num3 <= intRect2.xmax; num3++)
			{
				for (int num4 = intRect2.ymin; num4 <= intRect2.ymax; num4++)
				{
					for (int num5 = 0; num5 < this.layerCount; num5++)
					{
						int num6 = num5 * this.width * this.depth + num4 * this.width + num3;
						LevelGridNode levelGridNode = this.nodes[num6];
						if (levelGridNode != null)
						{
							if (flag)
							{
								levelGridNode.Walkable = levelGridNode.WalkableErosion;
								if (o.bounds.Contains((Vector3)levelGridNode.position))
								{
									o.Apply(levelGridNode);
								}
								levelGridNode.WalkableErosion = levelGridNode.Walkable;
							}
							else if (o.bounds.Contains((Vector3)levelGridNode.position))
							{
								o.Apply(levelGridNode);
							}
						}
					}
				}
			}
			if (flag && num == 0)
			{
				intRect2 = IntRect.Intersection(a2, b);
				for (int num7 = intRect2.xmin; num7 <= intRect2.xmax; num7++)
				{
					for (int num8 = intRect2.ymin; num8 <= intRect2.ymax; num8++)
					{
						this.CalculateConnections(num7, num8);
					}
				}
				return;
			}
			if (flag && num > 0)
			{
				IntRect a3 = IntRect.Union(a, intRect).Expand(num);
				IntRect intRect3 = a3.Expand(num);
				a3 = IntRect.Intersection(a3, b);
				intRect3 = IntRect.Intersection(intRect3, b);
				for (int num9 = intRect3.xmin; num9 <= intRect3.xmax; num9++)
				{
					for (int num10 = intRect3.ymin; num10 <= intRect3.ymax; num10++)
					{
						for (int num11 = 0; num11 < this.layerCount; num11++)
						{
							int num12 = num11 * this.width * this.depth + num10 * this.width + num9;
							LevelGridNode levelGridNode2 = this.nodes[num12];
							if (levelGridNode2 != null)
							{
								bool walkable = levelGridNode2.Walkable;
								levelGridNode2.Walkable = levelGridNode2.WalkableErosion;
								if (!a3.Contains(num9, num10))
								{
									levelGridNode2.TmpWalkable = walkable;
								}
							}
						}
					}
				}
				for (int num13 = intRect3.xmin; num13 <= intRect3.xmax; num13++)
				{
					for (int num14 = intRect3.ymin; num14 <= intRect3.ymax; num14++)
					{
						this.CalculateConnections(num13, num14);
					}
				}
				base.ErodeWalkableArea(intRect3.xmin, intRect3.ymin, intRect3.xmax + 1, intRect3.ymax + 1);
				for (int num15 = intRect3.xmin; num15 <= intRect3.xmax; num15++)
				{
					for (int num16 = intRect3.ymin; num16 <= intRect3.ymax; num16++)
					{
						if (!a3.Contains(num15, num16))
						{
							for (int num17 = 0; num17 < this.layerCount; num17++)
							{
								int num18 = num17 * this.width * this.depth + num16 * this.width + num15;
								LevelGridNode levelGridNode3 = this.nodes[num18];
								if (levelGridNode3 != null)
								{
									levelGridNode3.Walkable = levelGridNode3.TmpWalkable;
								}
							}
						}
					}
				}
				for (int num19 = intRect3.xmin; num19 <= intRect3.xmax; num19++)
				{
					for (int num20 = intRect3.ymin; num20 <= intRect3.ymax; num20++)
					{
						this.CalculateConnections(num19, num20);
					}
				}
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x001A179C File Offset: 0x0019F99C
		protected override IEnumerable<Progress> ScanInternal()
		{
			if (this.nodeSize <= 0f)
			{
				yield break;
			}
			base.UpdateTransform();
			if (this.width > 1024 || this.depth > 1024)
			{
				Debug.LogError("One of the grid's sides is longer than 1024 nodes");
				yield break;
			}
			this.lastScannedWidth = this.width;
			this.lastScannedDepth = this.depth;
			this.SetUpOffsetsAndCosts();
			LevelGridNode.SetGridGraph((int)this.graphIndex, this);
			this.maxClimb = Mathf.Clamp(this.maxClimb, 0f, this.characterHeight);
			LinkedLevelNode[] linkedCells = new LinkedLevelNode[this.width * this.depth];
			this.collision = (this.collision ?? new GraphCollision());
			this.collision.Initialize(base.transform, this.nodeSize);
			int progressCounter = 0;
			int num;
			for (int z = 0; z < this.depth; z = num + 1)
			{
				if (progressCounter >= 1000)
				{
					progressCounter = 0;
					yield return new Progress(Mathf.Lerp(0.1f, 0.5f, (float)z / (float)this.depth), "Calculating positions");
				}
				progressCounter += this.width;
				for (int i = 0; i < this.width; i++)
				{
					linkedCells[z * this.width + i] = this.SampleCell(i, z);
				}
				num = z;
			}
			this.layerCount = 0;
			for (int j = 0; j < linkedCells.Length; j++)
			{
				int num2 = 0;
				for (LinkedLevelNode linkedLevelNode = linkedCells[j]; linkedLevelNode != null; linkedLevelNode = linkedLevelNode.next)
				{
					num2++;
				}
				this.layerCount = Math.Max(this.layerCount, num2);
			}
			if (this.layerCount > 255)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Too many layers, a maximum of ",
					255,
					" (LevelGridNode.MaxLayerCount) layers are allowed (found ",
					this.layerCount,
					")"
				}));
				yield break;
			}
			this.nodes = new LevelGridNode[this.width * this.depth * this.layerCount];
			for (int z = 0; z < this.depth; z = num + 1)
			{
				if (progressCounter >= 1000)
				{
					progressCounter = 0;
					yield return new Progress(Mathf.Lerp(0.5f, 0.8f, (float)z / (float)this.depth), "Creating nodes");
				}
				progressCounter += this.width;
				for (int k = 0; k < this.width; k++)
				{
					this.RecalculateCell(k, z, true, true);
				}
				num = z;
			}
			for (int z = 0; z < this.depth; z = num + 1)
			{
				if (progressCounter >= 1000)
				{
					progressCounter = 0;
					yield return new Progress(Mathf.Lerp(0.8f, 0.9f, (float)z / (float)this.depth), "Calculating connections");
				}
				progressCounter += this.width;
				for (int l = 0; l < this.width; l++)
				{
					this.CalculateConnections(l, z);
				}
				num = z;
			}
			yield return new Progress(0.95f, "Calculating Erosion");
			for (int m = 0; m < this.nodes.Length; m++)
			{
				LevelGridNode levelGridNode = this.nodes[m];
				if (levelGridNode != null && !levelGridNode.HasAnyGridConnections())
				{
					levelGridNode.Walkable = false;
					levelGridNode.WalkableErosion = levelGridNode.Walkable;
				}
			}
			this.ErodeWalkableArea();
			yield break;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x001A17AC File Offset: 0x0019F9AC
		private LinkedLevelNode SampleCell(int x, int z)
		{
			LinkedLevelNode linkedLevelNode = null;
			Vector3 vector = base.transform.Transform(new Vector3((float)x + 0.5f, 0f, (float)z + 0.5f));
			RaycastHit[] array = this.collision.CheckHeightAll(vector);
			Vector3 rhs = base.transform.WorldUpAtGraphPosition(vector);
			for (int i = 0; i < array.Length / 2; i++)
			{
				RaycastHit raycastHit = array[i];
				array[i] = array[array.Length - 1 - i];
				array[array.Length - 1 - i] = raycastHit;
			}
			if (array.Length != 0)
			{
				LinkedLevelNode linkedLevelNode2 = null;
				for (int j = 0; j < array.Length; j++)
				{
					LinkedLevelNode linkedLevelNode3 = new LinkedLevelNode();
					linkedLevelNode3.position = array[j].point;
					if (linkedLevelNode2 != null && Vector3.Dot(linkedLevelNode3.position - linkedLevelNode2.position, rhs) <= this.mergeSpanRange)
					{
						linkedLevelNode2.position = linkedLevelNode3.position;
						linkedLevelNode2.hit = array[j];
						linkedLevelNode2.walkable = this.collision.Check(linkedLevelNode3.position);
					}
					else
					{
						linkedLevelNode3.walkable = this.collision.Check(linkedLevelNode3.position);
						linkedLevelNode3.hit = array[j];
						linkedLevelNode3.height = float.PositiveInfinity;
						if (linkedLevelNode == null)
						{
							linkedLevelNode = linkedLevelNode3;
							linkedLevelNode2 = linkedLevelNode3;
						}
						else
						{
							linkedLevelNode2.next = linkedLevelNode3;
							linkedLevelNode2.height = Vector3.Dot(linkedLevelNode3.position - linkedLevelNode2.position, rhs);
							linkedLevelNode2 = linkedLevelNode2.next;
						}
					}
				}
			}
			else
			{
				linkedLevelNode = new LinkedLevelNode
				{
					position = vector,
					height = float.PositiveInfinity,
					walkable = (!this.collision.unwalkableWhenNoGround && this.collision.Check(vector))
				};
			}
			return linkedLevelNode;
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x001A1988 File Offset: 0x0019FB88
		public override void RecalculateCell(int x, int z, bool resetPenalties = true, bool resetTags = true)
		{
			float num = Mathf.Cos(this.maxSlope * 0.0174532924f);
			int i = 0;
			LinkedLevelNode linkedLevelNode = this.SampleCell(x, z);
			while (linkedLevelNode != null)
			{
				if (i >= this.layerCount)
				{
					if (i + 1 > 255)
					{
						Debug.LogError(string.Concat(new object[]
						{
							"Too many layers, a maximum of ",
							255,
							" are allowed (required ",
							i + 1,
							")"
						}));
						return;
					}
					this.AddLayers(1);
				}
				int num2 = z * this.width + x + this.width * this.depth * i;
				LevelGridNode levelGridNode = this.nodes[num2];
				bool flag = levelGridNode == null;
				if (flag)
				{
					if (this.nodes[num2] != null)
					{
						this.nodes[num2].Destroy();
					}
					levelGridNode = (this.nodes[num2] = new LevelGridNode(this.active));
					levelGridNode.NodeInGridIndex = z * this.width + x;
					levelGridNode.LayerCoordinateInGrid = i;
					levelGridNode.GraphIndex = this.graphIndex;
				}
				levelGridNode.position = (Int3)linkedLevelNode.position;
				levelGridNode.Walkable = linkedLevelNode.walkable;
				levelGridNode.WalkableErosion = levelGridNode.Walkable;
				if (flag || resetPenalties)
				{
					levelGridNode.Penalty = this.initialPenalty;
					if (this.penaltyPosition)
					{
						levelGridNode.Penalty += (uint)Mathf.RoundToInt(((float)levelGridNode.position.y - this.penaltyPositionOffset) * this.penaltyPositionFactor);
					}
				}
				if (flag || resetTags)
				{
					levelGridNode.Tag = 0U;
				}
				if (linkedLevelNode.hit.normal != Vector3.zero && (this.penaltyAngle || num > 0.0001f))
				{
					float num3 = Vector3.Dot(linkedLevelNode.hit.normal.normalized, this.collision.up);
					if (resetTags && this.penaltyAngle)
					{
						levelGridNode.Penalty += (uint)Mathf.RoundToInt((1f - num3) * this.penaltyAngleFactor);
					}
					if (num3 < num)
					{
						levelGridNode.Walkable = false;
					}
				}
				if (linkedLevelNode.height < this.characterHeight)
				{
					levelGridNode.Walkable = false;
				}
				levelGridNode.WalkableErosion = levelGridNode.Walkable;
				linkedLevelNode = linkedLevelNode.next;
				i++;
			}
			while (i < this.layerCount)
			{
				int num4 = z * this.width + x + this.width * this.depth * i;
				if (this.nodes[num4] != null)
				{
					this.nodes[num4].Destroy();
				}
				this.nodes[num4] = null;
				i++;
			}
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x001A1C24 File Offset: 0x0019FE24
		private void AddLayers(int count)
		{
			int num = this.layerCount + count;
			if (num > 255)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Too many layers, a maximum of ",
					255,
					" are allowed (required ",
					num,
					")"
				}));
				return;
			}
			LevelGridNode[] array = this.nodes;
			this.nodes = new LevelGridNode[this.width * this.depth * num];
			for (int i = 0; i < array.Length; i++)
			{
				this.nodes[i] = array[i];
			}
			this.layerCount = num;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x001A1CC4 File Offset: 0x0019FEC4
		protected override bool ErosionAnyFalseConnections(GraphNode baseNode)
		{
			LevelGridNode levelGridNode = baseNode as LevelGridNode;
			if (this.neighbours == NumNeighbours.Six)
			{
				for (int i = 0; i < 6; i++)
				{
					if (!levelGridNode.GetConnection(GridGraph.hexagonNeighbourIndices[i]))
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < 4; j++)
				{
					if (!levelGridNode.GetConnection(j))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x001A1D18 File Offset: 0x0019FF18
		[Obsolete("CalculateConnections no longer takes a node array, it just uses the one on the graph")]
		public void CalculateConnections(LevelGridNode[] nodes, LevelGridNode node, int x, int z, int layerIndex)
		{
			this.CalculateConnections(x, z, layerIndex);
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x001A1D28 File Offset: 0x0019FF28
		public override void CalculateConnections(GridNodeBase baseNode)
		{
			LevelGridNode levelGridNode = baseNode as LevelGridNode;
			this.CalculateConnections(levelGridNode.XCoordinateInGrid, levelGridNode.ZCoordinateInGrid, levelGridNode.LayerCoordinateInGrid);
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x001A1D54 File Offset: 0x0019FF54
		[Obsolete("Use CalculateConnections(x,z,layerIndex) or CalculateConnections(node) instead")]
		public void CalculateConnections(int x, int z, int layerIndex, LevelGridNode node)
		{
			this.CalculateConnections(x, z, layerIndex);
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x001A1D60 File Offset: 0x0019FF60
		public override void CalculateConnections(int x, int z)
		{
			for (int i = 0; i < this.layerCount; i++)
			{
				this.CalculateConnections(x, z, i);
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x001A1D88 File Offset: 0x0019FF88
		public void CalculateConnections(int x, int z, int layerIndex)
		{
			LevelGridNode levelGridNode = this.nodes[z * this.width + x + this.width * this.depth * layerIndex];
			if (levelGridNode == null)
			{
				return;
			}
			levelGridNode.ResetAllGridConnections();
			if (!levelGridNode.Walkable)
			{
				return;
			}
			Vector3 vector = (Vector3)levelGridNode.position;
			Vector3 rhs = base.transform.WorldUpAtGraphPosition(vector);
			float num = Vector3.Dot(vector, rhs);
			float num2;
			if (layerIndex == this.layerCount - 1 || this.nodes[levelGridNode.NodeInGridIndex + this.width * this.depth * (layerIndex + 1)] == null)
			{
				num2 = float.PositiveInfinity;
			}
			else
			{
				num2 = Math.Abs(num - Vector3.Dot((Vector3)this.nodes[levelGridNode.NodeInGridIndex + this.width * this.depth * (layerIndex + 1)].position, rhs));
			}
			for (int i = 0; i < 4; i++)
			{
				int num3 = x + this.neighbourXOffsets[i];
				int num4 = z + this.neighbourZOffsets[i];
				if (num3 >= 0 && num4 >= 0 && num3 < this.width && num4 < this.depth)
				{
					int num5 = num4 * this.width + num3;
					int value = 255;
					for (int j = 0; j < this.layerCount; j++)
					{
						GraphNode graphNode = this.nodes[num5 + this.width * this.depth * j];
						if (graphNode != null && graphNode.Walkable)
						{
							float num6 = Vector3.Dot((Vector3)graphNode.position, rhs);
							float num7;
							if (j == this.layerCount - 1 || this.nodes[num5 + this.width * this.depth * (j + 1)] == null)
							{
								num7 = float.PositiveInfinity;
							}
							else
							{
								num7 = Math.Abs(num6 - Vector3.Dot((Vector3)this.nodes[num5 + this.width * this.depth * (j + 1)].position, rhs));
							}
							float num8 = Mathf.Max(num6, num);
							if (Mathf.Min(num6 + num7, num + num2) - num8 >= this.characterHeight && Mathf.Abs(num6 - num) <= this.maxClimb)
							{
								value = j;
							}
						}
					}
					levelGridNode.SetConnectionValue(i, value);
				}
			}
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x001A1FD0 File Offset: 0x001A01D0
		public override NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			if (this.nodes == null || this.depth * this.width * this.layerCount != this.nodes.Length)
			{
				return default(NNInfoInternal);
			}
			Vector3 vector = base.transform.InverseTransform(position);
			float x = vector.x;
			float z = vector.z;
			int num = Mathf.Clamp((int)x, 0, this.width - 1);
			int num2 = Mathf.Clamp((int)z, 0, this.depth - 1);
			LevelGridNode nearestNode = this.GetNearestNode(position, num, num2, null);
			NNInfoInternal result = new NNInfoInternal(nearestNode);
			float y = base.transform.InverseTransform((Vector3)nearestNode.position).y;
			result.clampedPosition = base.transform.Transform(new Vector3(Mathf.Clamp(x, (float)num, (float)num + 1f), y, Mathf.Clamp(z, (float)num2, (float)num2 + 1f)));
			return result;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x001A20B8 File Offset: 0x001A02B8
		private LevelGridNode GetNearestNode(Vector3 position, int x, int z, NNConstraint constraint)
		{
			int num = this.width * z + x;
			float num2 = float.PositiveInfinity;
			LevelGridNode result = null;
			for (int i = 0; i < this.layerCount; i++)
			{
				LevelGridNode levelGridNode = this.nodes[num + this.width * this.depth * i];
				if (levelGridNode != null)
				{
					float sqrMagnitude = ((Vector3)levelGridNode.position - position).sqrMagnitude;
					if (sqrMagnitude < num2 && (constraint == null || constraint.Suitable(levelGridNode)))
					{
						num2 = sqrMagnitude;
						result = levelGridNode;
					}
				}
			}
			return result;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x001A2140 File Offset: 0x001A0340
		public override NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			if (this.nodes == null || this.depth * this.width * this.layerCount != this.nodes.Length || this.layerCount == 0)
			{
				return default(NNInfoInternal);
			}
			Vector3 vector = position;
			position = base.transform.InverseTransform(position);
			float x = position.x;
			float z = position.z;
			int num = Mathf.Clamp((int)x, 0, this.width - 1);
			int num2 = Mathf.Clamp((int)z, 0, this.depth - 1);
			float num3 = float.PositiveInfinity;
			int num4 = 2;
			LevelGridNode levelGridNode = this.GetNearestNode(vector, num, num2, constraint);
			if (levelGridNode != null)
			{
				num3 = ((Vector3)levelGridNode.position - vector).sqrMagnitude;
			}
			if (levelGridNode != null && num4 > 0)
			{
				num4--;
			}
			float num5 = constraint.constrainDistance ? AstarPath.active.maxNearestNodeDistance : float.PositiveInfinity;
			float num6 = num5 * num5;
			int num7 = 1;
			for (;;)
			{
				int i = num2 + num7;
				if (this.nodeSize * (float)num7 > num5)
				{
					break;
				}
				int j;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						LevelGridNode nearestNode = this.GetNearestNode(vector, j, i, constraint);
						if (nearestNode != null)
						{
							float sqrMagnitude = ((Vector3)nearestNode.position - vector).sqrMagnitude;
							if (sqrMagnitude < num3 && sqrMagnitude < num6)
							{
								num3 = sqrMagnitude;
								levelGridNode = nearestNode;
							}
						}
					}
				}
				i = num2 - num7;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						LevelGridNode nearestNode2 = this.GetNearestNode(vector, j, i, constraint);
						if (nearestNode2 != null)
						{
							float sqrMagnitude2 = ((Vector3)nearestNode2.position - vector).sqrMagnitude;
							if (sqrMagnitude2 < num3 && sqrMagnitude2 < num6)
							{
								num3 = sqrMagnitude2;
								levelGridNode = nearestNode2;
							}
						}
					}
				}
				j = num - num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						LevelGridNode nearestNode3 = this.GetNearestNode(vector, j, i, constraint);
						if (nearestNode3 != null)
						{
							float sqrMagnitude3 = ((Vector3)nearestNode3.position - vector).sqrMagnitude;
							if (sqrMagnitude3 < num3 && sqrMagnitude3 < num6)
							{
								num3 = sqrMagnitude3;
								levelGridNode = nearestNode3;
							}
						}
					}
				}
				j = num + num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						LevelGridNode nearestNode4 = this.GetNearestNode(vector, j, i, constraint);
						if (nearestNode4 != null)
						{
							float sqrMagnitude4 = ((Vector3)nearestNode4.position - vector).sqrMagnitude;
							if (sqrMagnitude4 < num3 && sqrMagnitude4 < num6)
							{
								num3 = sqrMagnitude4;
								levelGridNode = nearestNode4;
							}
						}
					}
				}
				if (levelGridNode != null)
				{
					if (num4 == 0)
					{
						break;
					}
					num4--;
				}
				num7++;
			}
			NNInfoInternal result = new NNInfoInternal(levelGridNode);
			if (levelGridNode != null)
			{
				int xcoordinateInGrid = levelGridNode.XCoordinateInGrid;
				int zcoordinateInGrid = levelGridNode.ZCoordinateInGrid;
				result.clampedPosition = base.transform.Transform(new Vector3(Mathf.Clamp(x, (float)xcoordinateInGrid, (float)xcoordinateInGrid + 1f), base.transform.InverseTransform((Vector3)levelGridNode.position).y, Mathf.Clamp(z, (float)zcoordinateInGrid, (float)zcoordinateInGrid + 1f)));
			}
			return result;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x001A24D7 File Offset: 0x001A06D7
		[Obsolete("Use node.GetConnection instead")]
		public static bool CheckConnection(LevelGridNode node, int dir)
		{
			return node.GetConnection(dir);
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x001A24E0 File Offset: 0x001A06E0
		protected override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			if (this.nodes == null)
			{
				ctx.writer.Write(-1);
				return;
			}
			ctx.writer.Write(this.nodes.Length);
			for (int i = 0; i < this.nodes.Length; i++)
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

		// Token: 0x0600254A RID: 9546 RVA: 0x001A255C File Offset: 0x001A075C
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.nodes = null;
				return;
			}
			this.nodes = new LevelGridNode[num];
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (ctx.reader.ReadInt32() != -1)
				{
					this.nodes[i] = new LevelGridNode(this.active);
					this.nodes[i].DeserializeNode(ctx);
				}
				else
				{
					this.nodes[i] = null;
				}
			}
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x001A25DC File Offset: 0x001A07DC
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			base.UpdateTransform();
			this.lastScannedWidth = this.width;
			this.lastScannedDepth = this.depth;
			this.SetUpOffsetsAndCosts();
			LevelGridNode.SetGridGraph((int)this.graphIndex, this);
			if (this.nodes == null || this.nodes.Length == 0)
			{
				return;
			}
			if (this.width * this.depth * this.layerCount != this.nodes.Length)
			{
				Debug.LogError("Node data did not match with bounds data. Probably a change to the bounds/width/depth data was made after scanning the graph just prior to saving it. Nodes will be discarded");
				this.nodes = new LevelGridNode[0];
				return;
			}
			for (int i = 0; i < this.layerCount; i++)
			{
				for (int j = 0; j < this.depth; j++)
				{
					for (int k = 0; k < this.width; k++)
					{
						LevelGridNode levelGridNode = this.nodes[j * this.width + k + this.width * this.depth * i];
						if (levelGridNode != null)
						{
							levelGridNode.NodeInGridIndex = j * this.width + k;
							levelGridNode.LayerCoordinateInGrid = i;
						}
					}
				}
			}
		}

		// Token: 0x04004162 RID: 16738
		[JsonMember]
		internal int layerCount;

		// Token: 0x04004163 RID: 16739
		[JsonMember]
		public float mergeSpanRange = 0.5f;

		// Token: 0x04004164 RID: 16740
		[JsonMember]
		public float characterHeight = 0.4f;

		// Token: 0x04004165 RID: 16741
		internal int lastScannedWidth;

		// Token: 0x04004166 RID: 16742
		internal int lastScannedDepth;

		// Token: 0x04004167 RID: 16743
		public new LevelGridNode[] nodes;
	}
}
