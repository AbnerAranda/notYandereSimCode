using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200057B RID: 1403
	[JsonOptIn]
	public class GridGraph : NavGraph, IUpdatableGraph, ITransformedGraph, IRaycastableGraph
	{
		// Token: 0x060024DB RID: 9435 RVA: 0x0019D9F6 File Offset: 0x0019BBF6
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.RemoveGridGraphFromStatic();
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x0019DA04 File Offset: 0x0019BC04
		protected override void DestroyAllNodes()
		{
			this.GetNodes(delegate(GraphNode node)
			{
				(node as GridNodeBase).ClearCustomConnections(true);
				node.ClearConnections(false);
				node.Destroy();
			});
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x0019DA2B File Offset: 0x0019BC2B
		private void RemoveGridGraphFromStatic()
		{
			GridNode.SetGridGraph(AstarPath.active.data.GetGraphIndex(this), null);
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x00022944 File Offset: 0x00020B44
		public virtual bool uniformWidthDepthGrid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x00022944 File Offset: 0x00020B44
		public virtual int LayerCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x0019DA43 File Offset: 0x0019BC43
		public override int CountNodes()
		{
			return this.nodes.Length;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0019DA50 File Offset: 0x0019BC50
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x0019DA87 File Offset: 0x0019BC87
		protected bool useRaycastNormal
		{
			get
			{
				return Math.Abs(90f - this.maxSlope) > float.Epsilon;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x0019DAA1 File Offset: 0x0019BCA1
		// (set) Token: 0x060024E4 RID: 9444 RVA: 0x0019DAA9 File Offset: 0x0019BCA9
		public Vector2 size { get; protected set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x0019DAB2 File Offset: 0x0019BCB2
		// (set) Token: 0x060024E6 RID: 9446 RVA: 0x0019DABA File Offset: 0x0019BCBA
		public GraphTransform transform { get; private set; }

		// Token: 0x060024E7 RID: 9447 RVA: 0x0019DAC4 File Offset: 0x0019BCC4
		public GridGraph()
		{
			this.unclampedSize = new Vector2(10f, 10f);
			this.nodeSize = 1f;
			this.collision = new GraphCollision();
			this.transform = new GraphTransform(Matrix4x4.identity);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0019DBBD File Offset: 0x0019BDBD
		public override void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			throw new Exception("This method cannot be used for Grid Graphs. Please use the other overload of RelocateNodes instead");
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0019DBCC File Offset: 0x0019BDCC
		public void RelocateNodes(Vector3 center, Quaternion rotation, float nodeSize, float aspectRatio = 1f, float isometricAngle = 0f)
		{
			GraphTransform previousTransform = this.transform;
			this.center = center;
			this.rotation = rotation.eulerAngles;
			this.aspectRatio = aspectRatio;
			this.isometricAngle = isometricAngle;
			this.SetDimensions(this.width, this.depth, nodeSize);
			this.GetNodes(delegate(GraphNode node)
			{
				GridNodeBase gridNodeBase = node as GridNodeBase;
				float y = previousTransform.InverseTransform((Vector3)node.position).y;
				node.position = this.GraphPointToWorld(gridNodeBase.XCoordinateInGrid, gridNodeBase.ZCoordinateInGrid, y);
			});
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0019DC3B File Offset: 0x0019BE3B
		public Int3 GraphPointToWorld(int x, int z, float height)
		{
			return (Int3)this.transform.Transform(new Vector3((float)x + 0.5f, height, (float)z + 0.5f));
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x0019DC63 File Offset: 0x0019BE63
		// (set) Token: 0x060024EC RID: 9452 RVA: 0x0019DC6B File Offset: 0x0019BE6B
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x0019DC74 File Offset: 0x0019BE74
		// (set) Token: 0x060024EE RID: 9454 RVA: 0x0019DC7C File Offset: 0x0019BE7C
		public int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0019DC85 File Offset: 0x0019BE85
		public uint GetConnectionCost(int dir)
		{
			return this.neighbourCosts[dir];
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x0019DC90 File Offset: 0x0019BE90
		public GridNode GetNodeConnection(GridNode node, int dir)
		{
			if (!node.HasConnectionInDirection(dir))
			{
				return null;
			}
			if (!node.EdgeNode)
			{
				return this.nodes[node.NodeInGridIndex + this.neighbourOffsets[dir]];
			}
			int nodeInGridIndex = node.NodeInGridIndex;
			int num = nodeInGridIndex / this.Width;
			int x = nodeInGridIndex - num * this.Width;
			return this.GetNodeConnection(nodeInGridIndex, x, num, dir);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x0019DCEC File Offset: 0x0019BEEC
		public bool HasNodeConnection(GridNode node, int dir)
		{
			if (!node.HasConnectionInDirection(dir))
			{
				return false;
			}
			if (!node.EdgeNode)
			{
				return true;
			}
			int nodeInGridIndex = node.NodeInGridIndex;
			int num = nodeInGridIndex / this.Width;
			int x = nodeInGridIndex - num * this.Width;
			return this.HasNodeConnection(nodeInGridIndex, x, num, dir);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0019DD34 File Offset: 0x0019BF34
		public void SetNodeConnection(GridNode node, int dir, bool value)
		{
			int nodeInGridIndex = node.NodeInGridIndex;
			int num = nodeInGridIndex / this.Width;
			int x = nodeInGridIndex - num * this.Width;
			this.SetNodeConnection(nodeInGridIndex, x, num, dir, value);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0019DD68 File Offset: 0x0019BF68
		private GridNode GetNodeConnection(int index, int x, int z, int dir)
		{
			if (!this.nodes[index].HasConnectionInDirection(dir))
			{
				return null;
			}
			int num = x + this.neighbourXOffsets[dir];
			if (num < 0 || num >= this.Width)
			{
				return null;
			}
			int num2 = z + this.neighbourZOffsets[dir];
			if (num2 < 0 || num2 >= this.Depth)
			{
				return null;
			}
			int num3 = index + this.neighbourOffsets[dir];
			return this.nodes[num3];
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x0019DDD2 File Offset: 0x0019BFD2
		public void SetNodeConnection(int index, int x, int z, int dir, bool value)
		{
			this.nodes[index].SetConnectionInternal(dir, value);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x0019DDE8 File Offset: 0x0019BFE8
		public bool HasNodeConnection(int index, int x, int z, int dir)
		{
			if (!this.nodes[index].HasConnectionInDirection(dir))
			{
				return false;
			}
			int num = x + this.neighbourXOffsets[dir];
			if (num < 0 || num >= this.Width)
			{
				return false;
			}
			int num2 = z + this.neighbourZOffsets[dir];
			return num2 >= 0 && num2 < this.Depth;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x0019DE3F File Offset: 0x0019C03F
		public void SetDimensions(int width, int depth, float nodeSize)
		{
			this.unclampedSize = new Vector2((float)width, (float)depth) * nodeSize;
			this.nodeSize = nodeSize;
			this.UpdateTransform();
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0019DE63 File Offset: 0x0019C063
		[Obsolete("Use SetDimensions instead")]
		public void UpdateSizeFromWidthDepth()
		{
			this.SetDimensions(this.width, this.depth, this.nodeSize);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x0019DE7D File Offset: 0x0019C07D
		[Obsolete("This method has been renamed to UpdateTransform")]
		public void GenerateMatrix()
		{
			this.UpdateTransform();
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x0019DE85 File Offset: 0x0019C085
		public void UpdateTransform()
		{
			this.CalculateDimensions(out this.width, out this.depth, out this.nodeSize);
			this.transform = this.CalculateTransform();
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0019DEAC File Offset: 0x0019C0AC
		public GraphTransform CalculateTransform()
		{
			int num;
			int num2;
			float num3;
			this.CalculateDimensions(out num, out num2, out num3);
			Matrix4x4 rhs = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 45f, 0f), Vector3.one);
			rhs = Matrix4x4.Scale(new Vector3(Mathf.Cos(0.0174532924f * this.isometricAngle), 1f, 1f)) * rhs;
			rhs = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, -45f, 0f), Vector3.one) * rhs;
			return new GraphTransform(Matrix4x4.TRS((Matrix4x4.TRS(this.center, Quaternion.Euler(this.rotation), new Vector3(this.aspectRatio, 1f, 1f)) * rhs).MultiplyPoint3x4(-new Vector3((float)num * num3, 0f, (float)num2 * num3) * 0.5f), Quaternion.Euler(this.rotation), new Vector3(num3 * this.aspectRatio, 1f, num3)) * rhs);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x0019DFC8 File Offset: 0x0019C1C8
		private void CalculateDimensions(out int width, out int depth, out float nodeSize)
		{
			Vector2 vector = this.unclampedSize;
			vector.x *= Mathf.Sign(vector.x);
			vector.y *= Mathf.Sign(vector.y);
			nodeSize = Mathf.Max(this.nodeSize, vector.x / 1024f);
			nodeSize = Mathf.Max(this.nodeSize, vector.y / 1024f);
			vector.x = ((vector.x < nodeSize) ? nodeSize : vector.x);
			vector.y = ((vector.y < nodeSize) ? nodeSize : vector.y);
			this.size = vector;
			width = Mathf.FloorToInt(this.size.x / nodeSize);
			depth = Mathf.FloorToInt(this.size.y / nodeSize);
			if (Mathf.Approximately(this.size.x / nodeSize, (float)Mathf.CeilToInt(this.size.x / nodeSize)))
			{
				width = Mathf.CeilToInt(this.size.x / nodeSize);
			}
			if (Mathf.Approximately(this.size.y / nodeSize, (float)Mathf.CeilToInt(this.size.y / nodeSize)))
			{
				depth = Mathf.CeilToInt(this.size.y / nodeSize);
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0019E120 File Offset: 0x0019C320
		public override NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			if (this.nodes == null || this.depth * this.width != this.nodes.Length)
			{
				return default(NNInfoInternal);
			}
			position = this.transform.InverseTransform(position);
			float x = position.x;
			float z = position.z;
			int num = Mathf.Clamp((int)x, 0, this.width - 1);
			int num2 = Mathf.Clamp((int)z, 0, this.depth - 1);
			NNInfoInternal result = new NNInfoInternal(this.nodes[num2 * this.width + num]);
			float y = this.transform.InverseTransform((Vector3)this.nodes[num2 * this.width + num].position).y;
			result.clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)num, (float)num + 1f), y, Mathf.Clamp(z, (float)num2, (float)num2 + 1f)));
			return result;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x0019E218 File Offset: 0x0019C418
		public override NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			if (this.nodes == null || this.depth * this.width != this.nodes.Length)
			{
				return default(NNInfoInternal);
			}
			Vector3 b = position;
			position = this.transform.InverseTransform(position);
			float x = position.x;
			float z = position.z;
			int num = Mathf.Clamp((int)x, 0, this.width - 1);
			int num2 = Mathf.Clamp((int)z, 0, this.depth - 1);
			GridNode gridNode = this.nodes[num + num2 * this.width];
			GridNode gridNode2 = null;
			float num3 = float.PositiveInfinity;
			int num4 = 2;
			Vector3 clampedPosition = Vector3.zero;
			NNInfoInternal result = new NNInfoInternal(null);
			if (constraint == null || constraint.Suitable(gridNode))
			{
				gridNode2 = gridNode;
				num3 = ((Vector3)gridNode2.position - b).sqrMagnitude;
				float y = this.transform.InverseTransform((Vector3)gridNode.position).y;
				clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)num, (float)num + 1f), y, Mathf.Clamp(z, (float)num2, (float)num2 + 1f)));
			}
			if (gridNode2 != null)
			{
				result.node = gridNode2;
				result.clampedPosition = clampedPosition;
				if (num4 == 0)
				{
					return result;
				}
				num4--;
			}
			float num5 = (constraint == null || constraint.constrainDistance) ? AstarPath.active.maxNearestNodeDistance : float.PositiveInfinity;
			float num6 = num5 * num5;
			int num7 = 1;
			while (this.nodeSize * (float)num7 <= num5)
			{
				bool flag = false;
				int i = num2 + num7;
				int num8 = i * this.width;
				int j;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + num8]))
						{
							float sqrMagnitude = ((Vector3)this.nodes[j + num8].position - b).sqrMagnitude;
							if (sqrMagnitude < num3 && sqrMagnitude < num6)
							{
								num3 = sqrMagnitude;
								gridNode2 = this.nodes[j + num8];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				i = num2 - num7;
				num8 = i * this.width;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + num8]))
						{
							float sqrMagnitude2 = ((Vector3)this.nodes[j + num8].position - b).sqrMagnitude;
							if (sqrMagnitude2 < num3 && sqrMagnitude2 < num6)
							{
								num3 = sqrMagnitude2;
								gridNode2 = this.nodes[j + num8];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				j = num - num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + i * this.width]))
						{
							float sqrMagnitude3 = ((Vector3)this.nodes[j + i * this.width].position - b).sqrMagnitude;
							if (sqrMagnitude3 < num3 && sqrMagnitude3 < num6)
							{
								num3 = sqrMagnitude3;
								gridNode2 = this.nodes[j + i * this.width];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				j = num + num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + i * this.width]))
						{
							float sqrMagnitude4 = ((Vector3)this.nodes[j + i * this.width].position - b).sqrMagnitude;
							if (sqrMagnitude4 < num3 && sqrMagnitude4 < num6)
							{
								num3 = sqrMagnitude4;
								gridNode2 = this.nodes[j + i * this.width];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				if (gridNode2 != null)
				{
					if (num4 == 0)
					{
						break;
					}
					num4--;
				}
				if (!flag)
				{
					break;
				}
				num7++;
			}
			result.node = gridNode2;
			result.clampedPosition = clampedPosition;
			return result;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x0019E820 File Offset: 0x0019CA20
		public virtual void SetUpOffsetsAndCosts()
		{
			this.neighbourOffsets[0] = -this.width;
			this.neighbourOffsets[1] = 1;
			this.neighbourOffsets[2] = this.width;
			this.neighbourOffsets[3] = -1;
			this.neighbourOffsets[4] = -this.width + 1;
			this.neighbourOffsets[5] = this.width + 1;
			this.neighbourOffsets[6] = this.width - 1;
			this.neighbourOffsets[7] = -this.width - 1;
			uint num = (uint)Mathf.RoundToInt(this.nodeSize * 1000f);
			uint num2 = this.uniformEdgeCosts ? num : ((uint)Mathf.RoundToInt(this.nodeSize * Mathf.Sqrt(2f) * 1000f));
			this.neighbourCosts[0] = num;
			this.neighbourCosts[1] = num;
			this.neighbourCosts[2] = num;
			this.neighbourCosts[3] = num;
			this.neighbourCosts[4] = num2;
			this.neighbourCosts[5] = num2;
			this.neighbourCosts[6] = num2;
			this.neighbourCosts[7] = num2;
			this.neighbourXOffsets[0] = 0;
			this.neighbourXOffsets[1] = 1;
			this.neighbourXOffsets[2] = 0;
			this.neighbourXOffsets[3] = -1;
			this.neighbourXOffsets[4] = 1;
			this.neighbourXOffsets[5] = 1;
			this.neighbourXOffsets[6] = -1;
			this.neighbourXOffsets[7] = -1;
			this.neighbourZOffsets[0] = -1;
			this.neighbourZOffsets[1] = 0;
			this.neighbourZOffsets[2] = 1;
			this.neighbourZOffsets[3] = 0;
			this.neighbourZOffsets[4] = -1;
			this.neighbourZOffsets[5] = 1;
			this.neighbourZOffsets[6] = 1;
			this.neighbourZOffsets[7] = -1;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0019E9B0 File Offset: 0x0019CBB0
		protected override IEnumerable<Progress> ScanInternal()
		{
			if (this.nodeSize <= 0f)
			{
				yield break;
			}
			this.UpdateTransform();
			if (this.width > 1024 || this.depth > 1024)
			{
				Debug.LogError("One of the grid's sides is longer than 1024 nodes");
				yield break;
			}
			if (this.useJumpPointSearch)
			{
				Debug.LogError("Trying to use Jump Point Search, but support for it is not enabled. Please enable it in the inspector (Grid Graph settings).");
			}
			this.SetUpOffsetsAndCosts();
			GridNode.SetGridGraph((int)this.graphIndex, this);
			yield return new Progress(0.05f, "Creating nodes");
			this.nodes = new GridNode[this.width * this.depth];
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					int num = i * this.width + j;
					GridNode gridNode = this.nodes[num] = new GridNode(this.active);
					gridNode.GraphIndex = this.graphIndex;
					gridNode.NodeInGridIndex = num;
				}
			}
			if (this.collision == null)
			{
				this.collision = new GraphCollision();
			}
			this.collision.Initialize(this.transform, this.nodeSize);
			this.textureData.Initialize();
			int progressCounter = 0;
			int num2;
			for (int z = 0; z < this.depth; z = num2 + 1)
			{
				if (progressCounter >= 1000)
				{
					progressCounter = 0;
					yield return new Progress(Mathf.Lerp(0.1f, 0.7f, (float)z / (float)this.depth), "Calculating positions");
				}
				progressCounter += this.width;
				for (int k = 0; k < this.width; k++)
				{
					this.RecalculateCell(k, z, true, true);
					this.textureData.Apply(this.nodes[z * this.width + k], k, z);
				}
				num2 = z;
			}
			progressCounter = 0;
			for (int z = 0; z < this.depth; z = num2 + 1)
			{
				if (progressCounter >= 1000)
				{
					progressCounter = 0;
					yield return new Progress(Mathf.Lerp(0.7f, 0.9f, (float)z / (float)this.depth), "Calculating connections");
				}
				progressCounter += this.width;
				for (int l = 0; l < this.width; l++)
				{
					this.CalculateConnections(l, z);
				}
				num2 = z;
			}
			yield return new Progress(0.95f, "Calculating erosion");
			this.ErodeWalkableArea();
			yield break;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0019E9C0 File Offset: 0x0019CBC0
		[Obsolete("Use RecalculateCell instead which works both for grid graphs and layered grid graphs")]
		public virtual void UpdateNodePositionCollision(GridNode node, int x, int z, bool resetPenalty = true)
		{
			this.RecalculateCell(x, z, resetPenalty, false);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x0019E9D0 File Offset: 0x0019CBD0
		public virtual void RecalculateCell(int x, int z, bool resetPenalties = true, bool resetTags = true)
		{
			GridNode gridNode = this.nodes[z * this.width + x];
			gridNode.position = this.GraphPointToWorld(x, z, 0f);
			RaycastHit raycastHit;
			bool flag;
			Vector3 ob = this.collision.CheckHeight((Vector3)gridNode.position, out raycastHit, out flag);
			gridNode.position = (Int3)ob;
			if (resetPenalties)
			{
				gridNode.Penalty = this.initialPenalty;
				if (this.penaltyPosition)
				{
					gridNode.Penalty += (uint)Mathf.RoundToInt(((float)gridNode.position.y - this.penaltyPositionOffset) * this.penaltyPositionFactor);
				}
			}
			if (resetTags)
			{
				gridNode.Tag = 0U;
			}
			if (flag && this.useRaycastNormal && this.collision.heightCheck && raycastHit.normal != Vector3.zero)
			{
				float num = Vector3.Dot(raycastHit.normal.normalized, this.collision.up);
				if (this.penaltyAngle && resetPenalties)
				{
					gridNode.Penalty += (uint)Mathf.RoundToInt((1f - Mathf.Pow(num, this.penaltyAnglePower)) * this.penaltyAngleFactor);
				}
				float num2 = Mathf.Cos(this.maxSlope * 0.0174532924f);
				if (num < num2)
				{
					flag = false;
				}
			}
			gridNode.Walkable = (flag && this.collision.Check((Vector3)gridNode.position));
			gridNode.WalkableErosion = gridNode.Walkable;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x0019EB4C File Offset: 0x0019CD4C
		protected virtual bool ErosionAnyFalseConnections(GraphNode baseNode)
		{
			GridNode node = baseNode as GridNode;
			if (this.neighbours == NumNeighbours.Six)
			{
				for (int i = 0; i < 6; i++)
				{
					if (!this.HasNodeConnection(node, GridGraph.hexagonNeighbourIndices[i]))
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < 4; j++)
				{
					if (!this.HasNodeConnection(node, j))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x0019EBA2 File Offset: 0x0019CDA2
		private void ErodeNode(GraphNode node)
		{
			if (node.Walkable && this.ErosionAnyFalseConnections(node))
			{
				node.Walkable = false;
			}
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x0019EBBC File Offset: 0x0019CDBC
		private void ErodeNodeWithTagsInit(GraphNode node)
		{
			if (node.Walkable && this.ErosionAnyFalseConnections(node))
			{
				node.Tag = (uint)this.erosionFirstTag;
				return;
			}
			node.Tag = 0U;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x0019EBE4 File Offset: 0x0019CDE4
		private void ErodeNodeWithTags(GraphNode node, int iteration)
		{
			GridNodeBase gridNodeBase = node as GridNodeBase;
			if (gridNodeBase.Walkable && (ulong)gridNodeBase.Tag >= (ulong)((long)this.erosionFirstTag) && (ulong)gridNodeBase.Tag < (ulong)((long)(this.erosionFirstTag + iteration)))
			{
				if (this.neighbours == NumNeighbours.Six)
				{
					for (int i = 0; i < 6; i++)
					{
						GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(GridGraph.hexagonNeighbourIndices[i]);
						if (neighbourAlongDirection != null)
						{
							uint tag = neighbourAlongDirection.Tag;
							if ((ulong)tag > (ulong)((long)(this.erosionFirstTag + iteration)) || (ulong)tag < (ulong)((long)this.erosionFirstTag))
							{
								neighbourAlongDirection.Tag = (uint)(this.erosionFirstTag + iteration);
							}
						}
					}
					return;
				}
				for (int j = 0; j < 4; j++)
				{
					GridNodeBase neighbourAlongDirection2 = gridNodeBase.GetNeighbourAlongDirection(j);
					if (neighbourAlongDirection2 != null)
					{
						uint tag2 = neighbourAlongDirection2.Tag;
						if ((ulong)tag2 > (ulong)((long)(this.erosionFirstTag + iteration)) || (ulong)tag2 < (ulong)((long)this.erosionFirstTag))
						{
							neighbourAlongDirection2.Tag = (uint)(this.erosionFirstTag + iteration);
						}
					}
				}
			}
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x0019ECCF File Offset: 0x0019CECF
		public virtual void ErodeWalkableArea()
		{
			this.ErodeWalkableArea(0, 0, this.Width, this.Depth);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x0019ECE8 File Offset: 0x0019CEE8
		public void ErodeWalkableArea(int xmin, int zmin, int xmax, int zmax)
		{
			if (this.erosionUseTags)
			{
				if (this.erodeIterations + this.erosionFirstTag > 31)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Too few tags available for ",
						this.erodeIterations,
						" erode iterations and starting with tag ",
						this.erosionFirstTag,
						" (erodeIterations+erosionFirstTag > 31)"
					}), this.active);
					return;
				}
				if (this.erosionFirstTag <= 0)
				{
					Debug.LogError("First erosion tag must be greater or equal to 1", this.active);
					return;
				}
			}
			if (this.erodeIterations == 0)
			{
				return;
			}
			IntRect rect = new IntRect(xmin, zmin, xmax - 1, zmax - 1);
			List<GraphNode> nodesInRegion = this.GetNodesInRegion(rect);
			int count = nodesInRegion.Count;
			for (int i = 0; i < this.erodeIterations; i++)
			{
				if (this.erosionUseTags)
				{
					if (i == 0)
					{
						for (int j = 0; j < count; j++)
						{
							this.ErodeNodeWithTagsInit(nodesInRegion[j]);
						}
					}
					else
					{
						for (int k = 0; k < count; k++)
						{
							this.ErodeNodeWithTags(nodesInRegion[k], i);
						}
					}
				}
				else
				{
					for (int l = 0; l < count; l++)
					{
						this.ErodeNode(nodesInRegion[l]);
					}
					for (int m = 0; m < count; m++)
					{
						this.CalculateConnections(nodesInRegion[m] as GridNodeBase);
					}
				}
			}
			ListPool<GraphNode>.Release(ref nodesInRegion);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x0019EE48 File Offset: 0x0019D048
		public virtual bool IsValidConnection(GridNodeBase node1, GridNodeBase node2)
		{
			if (!node1.Walkable || !node2.Walkable)
			{
				return false;
			}
			if (this.maxClimb <= 0f || this.collision.use2D)
			{
				return true;
			}
			if (this.transform.onlyTranslational)
			{
				return (float)Math.Abs(node1.position.y - node2.position.y) <= this.maxClimb * 1000f;
			}
			Vector3 vector = (Vector3)node1.position;
			Vector3 rhs = (Vector3)node2.position;
			Vector3 lhs = this.transform.WorldUpAtGraphPosition(vector);
			return Math.Abs(Vector3.Dot(lhs, vector) - Vector3.Dot(lhs, rhs)) <= this.maxClimb;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x0019EF04 File Offset: 0x0019D104
		public void CalculateConnectionsForCellAndNeighbours(int x, int z)
		{
			this.CalculateConnections(x, z);
			for (int i = 0; i < 8; i++)
			{
				int x2 = x + this.neighbourXOffsets[i];
				int z2 = z + this.neighbourZOffsets[i];
				this.CalculateConnections(x2, z2);
			}
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0019EF43 File Offset: 0x0019D143
		[Obsolete("Use the instance function instead")]
		public static void CalculateConnections(GridNode node)
		{
			(AstarData.GetGraph(node) as GridGraph).CalculateConnections(node);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0019EF58 File Offset: 0x0019D158
		public virtual void CalculateConnections(GridNodeBase node)
		{
			int nodeInGridIndex = node.NodeInGridIndex;
			int x = nodeInGridIndex % this.width;
			int z = nodeInGridIndex / this.width;
			this.CalculateConnections(x, z);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0019EF84 File Offset: 0x0019D184
		[Obsolete("CalculateConnections no longer takes a node array, it just uses the one on the graph")]
		public virtual void CalculateConnections(GridNode[] nodes, int x, int z, GridNode node)
		{
			this.CalculateConnections(x, z);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0019EF8E File Offset: 0x0019D18E
		[Obsolete("Use CalculateConnections(x,z) or CalculateConnections(node) instead")]
		public virtual void CalculateConnections(int x, int z, GridNode node)
		{
			this.CalculateConnections(x, z);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0019EF98 File Offset: 0x0019D198
		public virtual void CalculateConnections(int x, int z)
		{
			GridNode gridNode = this.nodes[z * this.width + x];
			if (!gridNode.Walkable)
			{
				gridNode.ResetConnectionsInternal();
				return;
			}
			int nodeInGridIndex = gridNode.NodeInGridIndex;
			if (this.neighbours == NumNeighbours.Four || this.neighbours == NumNeighbours.Eight)
			{
				int num = 0;
				for (int i = 0; i < 4; i++)
				{
					int num2 = x + this.neighbourXOffsets[i];
					int num3 = z + this.neighbourZOffsets[i];
					if (num2 >= 0 & num3 >= 0 & num2 < this.width & num3 < this.depth)
					{
						GridNode node = this.nodes[nodeInGridIndex + this.neighbourOffsets[i]];
						if (this.IsValidConnection(gridNode, node))
						{
							num |= 1 << i;
						}
					}
				}
				int num4 = 0;
				if (this.neighbours == NumNeighbours.Eight)
				{
					if (this.cutCorners)
					{
						for (int j = 0; j < 4; j++)
						{
							if (((num >> j | num >> j + 1 | num >> j + 1 - 4) & 1) != 0)
							{
								int num5 = j + 4;
								int num6 = x + this.neighbourXOffsets[num5];
								int num7 = z + this.neighbourZOffsets[num5];
								if (num6 >= 0 & num7 >= 0 & num6 < this.width & num7 < this.depth)
								{
									GridNode node2 = this.nodes[nodeInGridIndex + this.neighbourOffsets[num5]];
									if (this.IsValidConnection(gridNode, node2))
									{
										num4 |= 1 << num5;
									}
								}
							}
						}
					}
					else
					{
						for (int k = 0; k < 4; k++)
						{
							if ((num >> k & 1) != 0 && ((num >> k + 1 | num >> k + 1 - 4) & 1) != 0)
							{
								GridNode node3 = this.nodes[nodeInGridIndex + this.neighbourOffsets[k + 4]];
								if (this.IsValidConnection(gridNode, node3))
								{
									num4 |= 1 << k + 4;
								}
							}
						}
					}
				}
				gridNode.SetAllConnectionInternal(num | num4);
				return;
			}
			gridNode.ResetConnectionsInternal();
			for (int l = 0; l < GridGraph.hexagonNeighbourIndices.Length; l++)
			{
				int num8 = GridGraph.hexagonNeighbourIndices[l];
				int num9 = x + this.neighbourXOffsets[num8];
				int num10 = z + this.neighbourZOffsets[num8];
				if (num9 >= 0 & num10 >= 0 & num9 < this.width & num10 < this.depth)
				{
					GridNode node4 = this.nodes[nodeInGridIndex + this.neighbourOffsets[num8]];
					gridNode.SetConnectionInternal(num8, this.IsValidConnection(gridNode, node4));
				}
			}
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x0019F220 File Offset: 0x0019D420
		public override void OnDrawGizmos(RetainedGizmos gizmos, bool drawNodes)
		{
			using (GraphGizmoHelper singleFrameGizmoHelper = gizmos.GetSingleFrameGizmoHelper(this.active))
			{
				int num;
				int num2;
				float num3;
				this.CalculateDimensions(out num, out num2, out num3);
				Bounds bounds = default(Bounds);
				bounds.SetMinMax(Vector3.zero, new Vector3((float)num, 0f, (float)num2));
				GraphTransform graphTransform = this.CalculateTransform();
				singleFrameGizmoHelper.builder.DrawWireCube(graphTransform, bounds, Color.white);
				int num4 = (this.nodes != null) ? this.nodes.Length : -1;
				if (this is LayerGridGraph)
				{
					num4 = (((this as LayerGridGraph).nodes != null) ? (this as LayerGridGraph).nodes.Length : -1);
				}
				if (drawNodes && this.width * this.depth * this.LayerCount != num4)
				{
					Color color = new Color(1f, 1f, 1f, 0.2f);
					for (int i = 0; i < num2; i++)
					{
						singleFrameGizmoHelper.builder.DrawLine(graphTransform.Transform(new Vector3(0f, 0f, (float)i)), graphTransform.Transform(new Vector3((float)num, 0f, (float)i)), color);
					}
					for (int j = 0; j < num; j++)
					{
						singleFrameGizmoHelper.builder.DrawLine(graphTransform.Transform(new Vector3((float)j, 0f, 0f)), graphTransform.Transform(new Vector3((float)j, 0f, (float)num2)), color);
					}
				}
			}
			if (!drawNodes)
			{
				return;
			}
			GridNodeBase[] array = ArrayPool<GridNodeBase>.Claim(1024 * this.LayerCount);
			for (int k = this.width / 32; k >= 0; k--)
			{
				for (int l = this.depth / 32; l >= 0; l--)
				{
					int nodesInRegion = this.GetNodesInRegion(new IntRect(k * 32, l * 32, (k + 1) * 32 - 1, (l + 1) * 32 - 1), array);
					RetainedGizmos.Hasher hasher = new RetainedGizmos.Hasher(this.active);
					hasher.AddHash(this.showMeshOutline ? 1 : 0);
					hasher.AddHash(this.showMeshSurface ? 1 : 0);
					hasher.AddHash(this.showNodeConnections ? 1 : 0);
					for (int m = 0; m < nodesInRegion; m++)
					{
						hasher.HashNode(array[m]);
					}
					if (!gizmos.Draw(hasher))
					{
						using (GraphGizmoHelper gizmoHelper = gizmos.GetGizmoHelper(this.active, hasher))
						{
							if (this.showNodeConnections)
							{
								for (int n = 0; n < nodesInRegion; n++)
								{
									if (array[n].Walkable)
									{
										gizmoHelper.DrawConnections(array[n]);
									}
								}
							}
							if (this.showMeshSurface || this.showMeshOutline)
							{
								this.CreateNavmeshSurfaceVisualization(array, nodesInRegion, gizmoHelper);
							}
						}
					}
				}
			}
			ArrayPool<GridNodeBase>.Release(ref array, false);
			if (this.active.showUnwalkableNodes)
			{
				base.DrawUnwalkableNodes(this.nodeSize * 0.3f);
			}
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x0019F54C File Offset: 0x0019D74C
		private void CreateNavmeshSurfaceVisualization(GridNodeBase[] nodes, int nodeCount, GraphGizmoHelper helper)
		{
			int num = 0;
			for (int i = 0; i < nodeCount; i++)
			{
				if (nodes[i].Walkable)
				{
					num++;
				}
			}
			int[] array;
			if (this.neighbours != NumNeighbours.Six)
			{
				RuntimeHelpers.InitializeArray(array = new int[4], fieldof(<PrivateImplementationDetails>.02E4414E7DFA0F3AA2387EE8EA7AB31431CB406A).FieldHandle);
			}
			else
			{
				array = GridGraph.hexagonNeighbourIndices;
			}
			int[] array2 = array;
			float num2 = (this.neighbours == NumNeighbours.Six) ? 0.333333f : 0.5f;
			int num3 = array2.Length - 2;
			int num4 = 3 * num3;
			Vector3[] array3 = ArrayPool<Vector3>.Claim(num * num4);
			Color[] array4 = ArrayPool<Color>.Claim(num * num4);
			int num5 = 0;
			for (int j = 0; j < nodeCount; j++)
			{
				GridNodeBase gridNodeBase = nodes[j];
				if (gridNodeBase.Walkable)
				{
					Color color = helper.NodeColor(gridNodeBase);
					if (color.a > 0.001f)
					{
						for (int k = 0; k < array2.Length; k++)
						{
							int num6 = array2[k];
							int num7 = array2[(k + 1) % array2.Length];
							GridNodeBase gridNodeBase2 = null;
							GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(num6);
							if (neighbourAlongDirection != null && this.neighbours != NumNeighbours.Six)
							{
								gridNodeBase2 = neighbourAlongDirection.GetNeighbourAlongDirection(num7);
							}
							GridNodeBase neighbourAlongDirection2 = gridNodeBase.GetNeighbourAlongDirection(num7);
							if (neighbourAlongDirection2 != null && gridNodeBase2 == null && this.neighbours != NumNeighbours.Six)
							{
								gridNodeBase2 = neighbourAlongDirection2.GetNeighbourAlongDirection(num6);
							}
							Vector3 vector = new Vector3((float)gridNodeBase.XCoordinateInGrid + 0.5f, 0f, (float)gridNodeBase.ZCoordinateInGrid + 0.5f);
							vector.x += (float)(this.neighbourXOffsets[num6] + this.neighbourXOffsets[num7]) * num2;
							vector.z += (float)(this.neighbourZOffsets[num6] + this.neighbourZOffsets[num7]) * num2;
							vector.y += this.transform.InverseTransform((Vector3)gridNodeBase.position).y;
							if (neighbourAlongDirection != null)
							{
								vector.y += this.transform.InverseTransform((Vector3)neighbourAlongDirection.position).y;
							}
							if (neighbourAlongDirection2 != null)
							{
								vector.y += this.transform.InverseTransform((Vector3)neighbourAlongDirection2.position).y;
							}
							if (gridNodeBase2 != null)
							{
								vector.y += this.transform.InverseTransform((Vector3)gridNodeBase2.position).y;
							}
							vector.y /= 1f + ((neighbourAlongDirection != null) ? 1f : 0f) + ((neighbourAlongDirection2 != null) ? 1f : 0f) + ((gridNodeBase2 != null) ? 1f : 0f);
							vector = this.transform.Transform(vector);
							array3[num5 + k] = vector;
						}
						if (this.neighbours == NumNeighbours.Six)
						{
							array3[num5 + 6] = array3[num5];
							array3[num5 + 7] = array3[num5 + 2];
							array3[num5 + 8] = array3[num5 + 3];
							array3[num5 + 9] = array3[num5];
							array3[num5 + 10] = array3[num5 + 3];
							array3[num5 + 11] = array3[num5 + 5];
						}
						else
						{
							array3[num5 + 4] = array3[num5];
							array3[num5 + 5] = array3[num5 + 2];
						}
						for (int l = 0; l < num4; l++)
						{
							array4[num5 + l] = color;
						}
						for (int m = 0; m < array2.Length; m++)
						{
							GridNodeBase neighbourAlongDirection3 = gridNodeBase.GetNeighbourAlongDirection(array2[(m + 1) % array2.Length]);
							if (neighbourAlongDirection3 == null || (this.showMeshOutline && gridNodeBase.NodeInGridIndex < neighbourAlongDirection3.NodeInGridIndex))
							{
								helper.builder.DrawLine(array3[num5 + m], array3[num5 + (m + 1) % array2.Length], (neighbourAlongDirection3 == null) ? Color.black : color);
							}
						}
						num5 += num4;
					}
				}
			}
			if (this.showMeshSurface)
			{
				helper.DrawTriangles(array3, array4, num5 * num3 / num4);
			}
			ArrayPool<Vector3>.Release(ref array3, false);
			ArrayPool<Color>.Release(ref array4, false);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x0019F984 File Offset: 0x0019DB84
		protected IntRect GetRectFromBounds(Bounds bounds)
		{
			bounds = this.transform.InverseTransform(bounds);
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			int xmin = Mathf.RoundToInt(min.x - 0.5f);
			int xmax = Mathf.RoundToInt(max.x - 0.5f);
			int ymin = Mathf.RoundToInt(min.z - 0.5f);
			int ymax = Mathf.RoundToInt(max.z - 0.5f);
			IntRect a = new IntRect(xmin, ymin, xmax, ymax);
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			return IntRect.Intersection(a, b);
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x0019FA20 File Offset: 0x0019DC20
		[Obsolete("This method has been renamed to GetNodesInRegion", true)]
		public List<GraphNode> GetNodesInArea(Bounds bounds)
		{
			return this.GetNodesInRegion(bounds);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0019FA29 File Offset: 0x0019DC29
		[Obsolete("This method has been renamed to GetNodesInRegion", true)]
		public List<GraphNode> GetNodesInArea(GraphUpdateShape shape)
		{
			return this.GetNodesInRegion(shape);
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x0019FA32 File Offset: 0x0019DC32
		[Obsolete("This method has been renamed to GetNodesInRegion", true)]
		public List<GraphNode> GetNodesInArea(Bounds bounds, GraphUpdateShape shape)
		{
			return this.GetNodesInRegion(bounds, shape);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0019FA3C File Offset: 0x0019DC3C
		public List<GraphNode> GetNodesInRegion(Bounds bounds)
		{
			return this.GetNodesInRegion(bounds, null);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0019FA46 File Offset: 0x0019DC46
		public List<GraphNode> GetNodesInRegion(GraphUpdateShape shape)
		{
			return this.GetNodesInRegion(shape.GetBounds(), shape);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x0019FA58 File Offset: 0x0019DC58
		protected virtual List<GraphNode> GetNodesInRegion(Bounds bounds, GraphUpdateShape shape)
		{
			IntRect rectFromBounds = this.GetRectFromBounds(bounds);
			if (this.nodes == null || !rectFromBounds.IsValid() || this.nodes.Length != this.width * this.depth)
			{
				return ListPool<GraphNode>.Claim();
			}
			List<GraphNode> list = ListPool<GraphNode>.Claim(rectFromBounds.Width * rectFromBounds.Height);
			for (int i = rectFromBounds.xmin; i <= rectFromBounds.xmax; i++)
			{
				for (int j = rectFromBounds.ymin; j <= rectFromBounds.ymax; j++)
				{
					int num = j * this.width + i;
					GraphNode graphNode = this.nodes[num];
					if (bounds.Contains((Vector3)graphNode.position) && (shape == null || shape.Contains((Vector3)graphNode.position)))
					{
						list.Add(graphNode);
					}
				}
			}
			return list;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x0019FB28 File Offset: 0x0019DD28
		public virtual List<GraphNode> GetNodesInRegion(IntRect rect)
		{
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			rect = IntRect.Intersection(rect, b);
			if (this.nodes == null || !rect.IsValid() || this.nodes.Length != this.width * this.depth)
			{
				return ListPool<GraphNode>.Claim(0);
			}
			List<GraphNode> list = ListPool<GraphNode>.Claim(rect.Width * rect.Height);
			for (int i = rect.ymin; i <= rect.ymax; i++)
			{
				int num = i * this.Width;
				for (int j = rect.xmin; j <= rect.xmax; j++)
				{
					list.Add(this.nodes[num + j]);
				}
			}
			return list;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0019FBE8 File Offset: 0x0019DDE8
		public virtual int GetNodesInRegion(IntRect rect, GridNodeBase[] buffer)
		{
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			rect = IntRect.Intersection(rect, b);
			if (this.nodes == null || !rect.IsValid() || this.nodes.Length != this.width * this.depth)
			{
				return 0;
			}
			if (buffer.Length < rect.Width * rect.Height)
			{
				throw new ArgumentException("Buffer is too small");
			}
			int num = 0;
			int i = rect.ymin;
			while (i <= rect.ymax)
			{
				Array.Copy(this.nodes, i * this.Width + rect.xmin, buffer, num, rect.Width);
				i++;
				num += rect.Width;
			}
			return num;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x0019FCA6 File Offset: 0x0019DEA6
		public virtual GridNodeBase GetNode(int x, int z)
		{
			if (x < 0 || z < 0 || x >= this.width || z >= this.depth)
			{
				return null;
			}
			return this.nodes[x + z * this.width];
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0002D199 File Offset: 0x0002B399
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			return GraphUpdateThreading.UnityThread;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject o)
		{
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x0019FCD8 File Offset: 0x0019DED8
		protected void CalculateAffectedRegions(GraphUpdateObject o, out IntRect originalRect, out IntRect affectRect, out IntRect physicsRect, out bool willChangeWalkability, out int erosion)
		{
			Bounds bounds = this.transform.InverseTransform(o.bounds);
			Vector3 vector = bounds.min;
			Vector3 vector2 = bounds.max;
			int xmin = Mathf.RoundToInt(vector.x - 0.5f);
			int xmax = Mathf.RoundToInt(vector2.x - 0.5f);
			int ymin = Mathf.RoundToInt(vector.z - 0.5f);
			int ymax = Mathf.RoundToInt(vector2.z - 0.5f);
			originalRect = new IntRect(xmin, ymin, xmax, ymax);
			affectRect = originalRect;
			physicsRect = originalRect;
			erosion = (o.updateErosion ? this.erodeIterations : 0);
			willChangeWalkability = (o.updatePhysics || o.modifyWalkability);
			if (o.updatePhysics && !o.modifyWalkability && this.collision.collisionCheck)
			{
				Vector3 a = new Vector3(this.collision.diameter, 0f, this.collision.diameter) * 0.5f;
				vector -= a * 1.02f;
				vector2 += a * 1.02f;
				physicsRect = new IntRect(Mathf.RoundToInt(vector.x - 0.5f), Mathf.RoundToInt(vector.z - 0.5f), Mathf.RoundToInt(vector2.x - 0.5f), Mathf.RoundToInt(vector2.z - 0.5f));
				affectRect = IntRect.Union(physicsRect, affectRect);
			}
			if (willChangeWalkability || erosion > 0)
			{
				affectRect = affectRect.Expand(erosion + 1);
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0019FEA4 File Offset: 0x0019E0A4
		void IUpdatableGraph.UpdateArea(GraphUpdateObject o)
		{
			if (this.nodes == null || this.nodes.Length != this.width * this.depth)
			{
				Debug.LogWarning("The Grid Graph is not scanned, cannot update area");
				return;
			}
			IntRect a;
			IntRect a2;
			IntRect intRect;
			bool flag;
			int num;
			this.CalculateAffectedRegions(o, out a, out a2, out intRect, out flag, out num);
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			IntRect intRect2 = IntRect.Intersection(a2, b);
			for (int i = intRect2.xmin; i <= intRect2.xmax; i++)
			{
				for (int j = intRect2.ymin; j <= intRect2.ymax; j++)
				{
					o.WillUpdateNode(this.nodes[j * this.width + i]);
				}
			}
			if (o.updatePhysics && !o.modifyWalkability)
			{
				this.collision.Initialize(this.transform, this.nodeSize);
				intRect2 = IntRect.Intersection(intRect, b);
				for (int k = intRect2.xmin; k <= intRect2.xmax; k++)
				{
					for (int l = intRect2.ymin; l <= intRect2.ymax; l++)
					{
						this.RecalculateCell(k, l, o.resetPenaltyOnPhysics, false);
					}
				}
			}
			intRect2 = IntRect.Intersection(a, b);
			for (int m = intRect2.xmin; m <= intRect2.xmax; m++)
			{
				for (int n = intRect2.ymin; n <= intRect2.ymax; n++)
				{
					int num2 = n * this.width + m;
					GridNode gridNode = this.nodes[num2];
					if (flag)
					{
						gridNode.Walkable = gridNode.WalkableErosion;
						if (o.bounds.Contains((Vector3)gridNode.position))
						{
							o.Apply(gridNode);
						}
						gridNode.WalkableErosion = gridNode.Walkable;
					}
					else if (o.bounds.Contains((Vector3)gridNode.position))
					{
						o.Apply(gridNode);
					}
				}
			}
			if (flag && num == 0)
			{
				intRect2 = IntRect.Intersection(a2, b);
				for (int num3 = intRect2.xmin; num3 <= intRect2.xmax; num3++)
				{
					for (int num4 = intRect2.ymin; num4 <= intRect2.ymax; num4++)
					{
						this.CalculateConnections(num3, num4);
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
				for (int num5 = intRect3.xmin; num5 <= intRect3.xmax; num5++)
				{
					for (int num6 = intRect3.ymin; num6 <= intRect3.ymax; num6++)
					{
						int num7 = num6 * this.width + num5;
						GridNode gridNode2 = this.nodes[num7];
						bool walkable = gridNode2.Walkable;
						gridNode2.Walkable = gridNode2.WalkableErosion;
						if (!a3.Contains(num5, num6))
						{
							gridNode2.TmpWalkable = walkable;
						}
					}
				}
				for (int num8 = intRect3.xmin; num8 <= intRect3.xmax; num8++)
				{
					for (int num9 = intRect3.ymin; num9 <= intRect3.ymax; num9++)
					{
						this.CalculateConnections(num8, num9);
					}
				}
				this.ErodeWalkableArea(intRect3.xmin, intRect3.ymin, intRect3.xmax + 1, intRect3.ymax + 1);
				for (int num10 = intRect3.xmin; num10 <= intRect3.xmax; num10++)
				{
					for (int num11 = intRect3.ymin; num11 <= intRect3.ymax; num11++)
					{
						if (!a3.Contains(num10, num11))
						{
							int num12 = num11 * this.width + num10;
							GridNode gridNode3 = this.nodes[num12];
							gridNode3.Walkable = gridNode3.TmpWalkable;
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
			}
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x001A02CC File Offset: 0x0019E4CC
		public bool Linecast(Vector3 from, Vector3 to)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(from, to, null, out graphHitInfo);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x001A02E4 File Offset: 0x0019E4E4
		public bool Linecast(Vector3 from, Vector3 to, GraphNode hint)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(from, to, hint, out graphHitInfo);
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x001A02FC File Offset: 0x0019E4FC
		public bool Linecast(Vector3 from, Vector3 to, GraphNode hint, out GraphHitInfo hit)
		{
			return this.Linecast(from, to, hint, out hit, null);
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x001A030A File Offset: 0x0019E50A
		protected static float CrossMagnitude(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y;
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x001A0327 File Offset: 0x0019E527
		protected static long CrossMagnitude(Int2 a, Int2 b)
		{
			return (long)a.x * (long)b.y - (long)b.x * (long)a.y;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x001A0348 File Offset: 0x0019E548
		protected bool ClipLineSegmentToBounds(Vector3 a, Vector3 b, out Vector3 outA, out Vector3 outB)
		{
			if (a.x < 0f || a.z < 0f || a.x > (float)this.width || a.z > (float)this.depth || b.x < 0f || b.z < 0f || b.x > (float)this.width || b.z > (float)this.depth)
			{
				Vector3 vector = new Vector3(0f, 0f, 0f);
				Vector3 vector2 = new Vector3(0f, 0f, (float)this.depth);
				Vector3 vector3 = new Vector3((float)this.width, 0f, (float)this.depth);
				Vector3 vector4 = new Vector3((float)this.width, 0f, 0f);
				int num = 0;
				bool flag;
				Vector3 vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector, vector2, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector, vector2, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector2, vector3, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector2, vector3, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector3, vector4, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector3, vector4, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector4, vector, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector4, vector, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				if (num == 0)
				{
					outA = Vector3.zero;
					outB = Vector3.zero;
					return false;
				}
			}
			outA = a;
			outB = b;
			return true;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x001A0504 File Offset: 0x0019E704
		public bool Linecast(Vector3 from, Vector3 to, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace)
		{
			hit = default(GraphHitInfo);
			hit.origin = from;
			Vector3 vector = this.transform.InverseTransform(from);
			Vector3 vector2 = this.transform.InverseTransform(to);
			if (!this.ClipLineSegmentToBounds(vector, vector2, out vector, out vector2))
			{
				hit.point = to;
				return false;
			}
			GridNodeBase gridNodeBase = base.GetNearest(this.transform.Transform(vector), NNConstraint.None).node as GridNodeBase;
			GridNodeBase gridNodeBase2 = base.GetNearest(this.transform.Transform(vector2), NNConstraint.None).node as GridNodeBase;
			if (!gridNodeBase.Walkable)
			{
				hit.node = gridNodeBase;
				hit.point = this.transform.Transform(vector);
				hit.tangentOrigin = hit.point;
				return true;
			}
			Vector2 vector3 = new Vector2(vector.x - 0.5f, vector.z - 0.5f);
			Vector2 vector4 = new Vector2(vector2.x - 0.5f, vector2.z - 0.5f);
			if (gridNodeBase == null || gridNodeBase2 == null)
			{
				hit.node = null;
				hit.point = from;
				return true;
			}
			Vector2 vector5 = vector4 - vector3;
			Vector2 b = new Vector2(Mathf.Sign(vector5.x), Mathf.Sign(vector5.y));
			float num = GridGraph.CrossMagnitude(vector5, b) * 0.5f;
			int num2 = ((vector5.y >= 0f) ? 0 : 3) ^ ((vector5.x >= 0f) ? 0 : 1);
			int num3 = num2 + 1 & 3;
			int num4 = num2 + 2 & 3;
			GridNodeBase gridNodeBase3 = gridNodeBase;
			while (gridNodeBase3.NodeInGridIndex != gridNodeBase2.NodeInGridIndex)
			{
				if (trace != null)
				{
					trace.Add(gridNodeBase3);
				}
				Vector2 a = new Vector2((float)gridNodeBase3.XCoordinateInGrid, (float)gridNodeBase3.ZCoordinateInGrid);
				int num5 = (GridGraph.CrossMagnitude(vector5, a - vector3) + num < 0f) ? num4 : num3;
				GridNodeBase neighbourAlongDirection = gridNodeBase3.GetNeighbourAlongDirection(num5);
				if (neighbourAlongDirection == null)
				{
					Vector2 a2 = new Vector2((float)this.neighbourXOffsets[num5], (float)this.neighbourZOffsets[num5]);
					Vector2 b2 = new Vector2((float)this.neighbourXOffsets[num5 - 1 + 4 & 3], (float)this.neighbourZOffsets[num5 - 1 + 4 & 3]);
					Vector2 vector6 = new Vector2((float)this.neighbourXOffsets[num5 + 1 & 3], (float)this.neighbourZOffsets[num5 + 1 & 3]);
					Vector2 vector7 = a + (a2 + b2) * 0.5f;
					Vector2 vector8 = VectorMath.LineIntersectionPoint(vector7, vector7 + vector6, vector3, vector4);
					Vector3 vector9 = this.transform.InverseTransform((Vector3)gridNodeBase3.position);
					Vector3 point = new Vector3(vector8.x + 0.5f, vector9.y, vector8.y + 0.5f);
					Vector3 point2 = new Vector3(vector7.x + 0.5f, vector9.y, vector7.y + 0.5f);
					hit.point = this.transform.Transform(point);
					hit.tangentOrigin = this.transform.Transform(point2);
					hit.tangent = this.transform.TransformVector(new Vector3(vector6.x, 0f, vector6.y));
					hit.node = gridNodeBase3;
					return true;
				}
				gridNodeBase3 = neighbourAlongDirection;
			}
			if (trace != null)
			{
				trace.Add(gridNodeBase3);
			}
			if (gridNodeBase3 == gridNodeBase2)
			{
				hit.point = to;
				hit.node = gridNodeBase3;
				return false;
			}
			hit.point = (Vector3)gridNodeBase3.position;
			hit.tangentOrigin = hit.point;
			return true;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x001A08AC File Offset: 0x0019EAAC
		public bool SnappedLinecast(Vector3 from, Vector3 to, GraphNode hint, out GraphHitInfo hit)
		{
			return this.Linecast((Vector3)base.GetNearest(from, NNConstraint.None).node.position, (Vector3)base.GetNearest(to, NNConstraint.None).node.position, hint, out hit);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x001A08F8 File Offset: 0x0019EAF8
		public bool Linecast(GridNodeBase fromNode, GridNodeBase toNode)
		{
			Int2 @int = new Int2(toNode.XCoordinateInGrid - fromNode.XCoordinateInGrid, toNode.ZCoordinateInGrid - fromNode.ZCoordinateInGrid);
			long num = GridGraph.CrossMagnitude(@int, new Int2(Math.Sign(@int.x), Math.Sign(@int.y)));
			int num2 = 0;
			if (@int.x <= 0 && @int.y > 0)
			{
				num2 = 1;
			}
			else if (@int.x < 0 && @int.y <= 0)
			{
				num2 = 2;
			}
			else if (@int.x >= 0 && @int.y < 0)
			{
				num2 = 3;
			}
			int num3 = num2 + 1 & 3;
			int num4 = num2 + 2 & 3;
			int num5 = (@int.x != 0 && @int.y != 0) ? (4 + (num2 + 1 & 3)) : -1;
			Int2 int2 = new Int2(0, 0);
			while (fromNode != null && fromNode.NodeInGridIndex != toNode.NodeInGridIndex)
			{
				long num6 = GridGraph.CrossMagnitude(@int, int2) * 2L + num;
				int num7 = (num6 < 0L) ? num4 : num3;
				if (num6 == 0L && num5 != -1)
				{
					num7 = num5;
				}
				fromNode = fromNode.GetNeighbourAlongDirection(num7);
				int2 += new Int2(this.neighbourXOffsets[num7], this.neighbourZOffsets[num7]);
			}
			return fromNode != toNode;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x001A0A24 File Offset: 0x0019EC24
		public bool CheckConnection(GridNode node, int dir)
		{
			if (this.neighbours == NumNeighbours.Eight || this.neighbours == NumNeighbours.Six || dir < 4)
			{
				return this.HasNodeConnection(node, dir);
			}
			int num = dir - 4 - 1 & 3;
			int num2 = dir - 4 + 1 & 3;
			if (!this.HasNodeConnection(node, num) || !this.HasNodeConnection(node, num2))
			{
				return false;
			}
			GridNode gridNode = this.nodes[node.NodeInGridIndex + this.neighbourOffsets[num]];
			GridNode gridNode2 = this.nodes[node.NodeInGridIndex + this.neighbourOffsets[num2]];
			return gridNode.Walkable && gridNode2.Walkable && this.HasNodeConnection(gridNode2, num) && this.HasNodeConnection(gridNode, num2);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x001A0AD0 File Offset: 0x0019ECD0
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
				this.nodes[i].SerializeNode(ctx);
			}
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x001A0B28 File Offset: 0x0019ED28
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.nodes = null;
				return;
			}
			this.nodes = new GridNode[num];
			for (int i = 0; i < this.nodes.Length; i++)
			{
				this.nodes[i] = new GridNode(this.active);
				this.nodes[i].DeserializeNode(ctx);
			}
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x001A0B90 File Offset: 0x0019ED90
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.aspectRatio = ctx.reader.ReadSingle();
			this.rotation = ctx.DeserializeVector3();
			this.center = ctx.DeserializeVector3();
			this.unclampedSize = ctx.DeserializeVector3();
			this.nodeSize = ctx.reader.ReadSingle();
			this.collision.DeserializeSettingsCompatibility(ctx);
			this.maxClimb = ctx.reader.ReadSingle();
			ctx.reader.ReadInt32();
			this.maxSlope = ctx.reader.ReadSingle();
			this.erodeIterations = ctx.reader.ReadInt32();
			this.erosionUseTags = ctx.reader.ReadBoolean();
			this.erosionFirstTag = ctx.reader.ReadInt32();
			ctx.reader.ReadBoolean();
			this.neighbours = (NumNeighbours)ctx.reader.ReadInt32();
			this.cutCorners = ctx.reader.ReadBoolean();
			this.penaltyPosition = ctx.reader.ReadBoolean();
			this.penaltyPositionFactor = ctx.reader.ReadSingle();
			this.penaltyAngle = ctx.reader.ReadBoolean();
			this.penaltyAngleFactor = ctx.reader.ReadSingle();
			this.penaltyAnglePower = ctx.reader.ReadSingle();
			this.isometricAngle = ctx.reader.ReadSingle();
			this.uniformEdgeCosts = ctx.reader.ReadBoolean();
			this.useJumpPointSearch = ctx.reader.ReadBoolean();
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x001A0D14 File Offset: 0x0019EF14
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			this.UpdateTransform();
			this.SetUpOffsetsAndCosts();
			GridNode.SetGridGraph((int)this.graphIndex, this);
			if (this.nodes == null || this.nodes.Length == 0)
			{
				return;
			}
			if (this.width * this.depth != this.nodes.Length)
			{
				Debug.LogError("Node data did not match with bounds data. Probably a change to the bounds/width/depth data was made after scanning the graph just prior to saving it. Nodes will be discarded");
				this.nodes = new GridNode[0];
				return;
			}
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					GridNode gridNode = this.nodes[i * this.width + j];
					if (gridNode == null)
					{
						Debug.LogError("Deserialization Error : Couldn't cast the node to the appropriate type - GridGenerator");
						return;
					}
					gridNode.NodeInGridIndex = i * this.width + j;
				}
			}
		}

		// Token: 0x04004138 RID: 16696
		[JsonMember]
		public InspectorGridMode inspectorGridMode;

		// Token: 0x04004139 RID: 16697
		public int width;

		// Token: 0x0400413A RID: 16698
		public int depth;

		// Token: 0x0400413B RID: 16699
		[JsonMember]
		public float aspectRatio = 1f;

		// Token: 0x0400413C RID: 16700
		[JsonMember]
		public float isometricAngle;

		// Token: 0x0400413D RID: 16701
		[JsonMember]
		public bool uniformEdgeCosts;

		// Token: 0x0400413E RID: 16702
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x0400413F RID: 16703
		[JsonMember]
		public Vector3 center;

		// Token: 0x04004140 RID: 16704
		[JsonMember]
		public Vector2 unclampedSize;

		// Token: 0x04004141 RID: 16705
		[JsonMember]
		public float nodeSize = 1f;

		// Token: 0x04004142 RID: 16706
		[JsonMember]
		public GraphCollision collision;

		// Token: 0x04004143 RID: 16707
		[JsonMember]
		public float maxClimb = 0.4f;

		// Token: 0x04004144 RID: 16708
		[JsonMember]
		public float maxSlope = 90f;

		// Token: 0x04004145 RID: 16709
		[JsonMember]
		public int erodeIterations;

		// Token: 0x04004146 RID: 16710
		[JsonMember]
		public bool erosionUseTags;

		// Token: 0x04004147 RID: 16711
		[JsonMember]
		public int erosionFirstTag = 1;

		// Token: 0x04004148 RID: 16712
		[JsonMember]
		public NumNeighbours neighbours = NumNeighbours.Eight;

		// Token: 0x04004149 RID: 16713
		[JsonMember]
		public bool cutCorners = true;

		// Token: 0x0400414A RID: 16714
		[JsonMember]
		public float penaltyPositionOffset;

		// Token: 0x0400414B RID: 16715
		[JsonMember]
		public bool penaltyPosition;

		// Token: 0x0400414C RID: 16716
		[JsonMember]
		public float penaltyPositionFactor = 1f;

		// Token: 0x0400414D RID: 16717
		[JsonMember]
		public bool penaltyAngle;

		// Token: 0x0400414E RID: 16718
		[JsonMember]
		public float penaltyAngleFactor = 100f;

		// Token: 0x0400414F RID: 16719
		[JsonMember]
		public float penaltyAnglePower = 1f;

		// Token: 0x04004150 RID: 16720
		[JsonMember]
		public bool useJumpPointSearch;

		// Token: 0x04004151 RID: 16721
		[JsonMember]
		public bool showMeshOutline = true;

		// Token: 0x04004152 RID: 16722
		[JsonMember]
		public bool showNodeConnections;

		// Token: 0x04004153 RID: 16723
		[JsonMember]
		public bool showMeshSurface = true;

		// Token: 0x04004154 RID: 16724
		[JsonMember]
		public GridGraph.TextureData textureData = new GridGraph.TextureData();

		// Token: 0x04004156 RID: 16726
		[NonSerialized]
		public readonly int[] neighbourOffsets = new int[8];

		// Token: 0x04004157 RID: 16727
		[NonSerialized]
		public readonly uint[] neighbourCosts = new uint[8];

		// Token: 0x04004158 RID: 16728
		[NonSerialized]
		public readonly int[] neighbourXOffsets = new int[8];

		// Token: 0x04004159 RID: 16729
		[NonSerialized]
		public readonly int[] neighbourZOffsets = new int[8];

		// Token: 0x0400415A RID: 16730
		internal static readonly int[] hexagonNeighbourIndices = new int[]
		{
			0,
			1,
			5,
			2,
			3,
			7
		};

		// Token: 0x0400415B RID: 16731
		public const int getNearestForceOverlap = 2;

		// Token: 0x0400415C RID: 16732
		public GridNode[] nodes;

		// Token: 0x0200074C RID: 1868
		public class TextureData
		{
			// Token: 0x06002D39 RID: 11577 RVA: 0x001D10E0 File Offset: 0x001CF2E0
			public void Initialize()
			{
				if (this.enabled && this.source != null)
				{
					for (int i = 0; i < this.channels.Length; i++)
					{
						if (this.channels[i] != GridGraph.TextureData.ChannelUse.None)
						{
							try
							{
								this.data = this.source.GetPixels32();
								break;
							}
							catch (UnityException ex)
							{
								Debug.LogWarning(ex.ToString());
								this.data = null;
								break;
							}
						}
					}
				}
			}

			// Token: 0x06002D3A RID: 11578 RVA: 0x001D1158 File Offset: 0x001CF358
			public void Apply(GridNode node, int x, int z)
			{
				if (this.enabled && this.data != null && x < this.source.width && z < this.source.height)
				{
					Color32 color = this.data[z * this.source.width + x];
					if (this.channels[0] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.r, this.channels[0], this.factors[0]);
					}
					if (this.channels[1] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.g, this.channels[1], this.factors[1]);
					}
					if (this.channels[2] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.b, this.channels[2], this.factors[2]);
					}
					node.WalkableErosion = node.Walkable;
				}
			}

			// Token: 0x06002D3B RID: 11579 RVA: 0x001D1240 File Offset: 0x001CF440
			private void ApplyChannel(GridNode node, int x, int z, int value, GridGraph.TextureData.ChannelUse channelUse, float factor)
			{
				switch (channelUse)
				{
				case GridGraph.TextureData.ChannelUse.Penalty:
					node.Penalty += (uint)Mathf.RoundToInt((float)value * factor);
					return;
				case GridGraph.TextureData.ChannelUse.Position:
					node.position = GridNode.GetGridGraph(node.GraphIndex).GraphPointToWorld(x, z, (float)value);
					return;
				case GridGraph.TextureData.ChannelUse.WalkablePenalty:
					if (value == 0)
					{
						node.Walkable = false;
						return;
					}
					node.Penalty += (uint)Mathf.RoundToInt((float)(value - 1) * factor);
					return;
				default:
					return;
				}
			}

			// Token: 0x04004A2E RID: 18990
			public bool enabled;

			// Token: 0x04004A2F RID: 18991
			public Texture2D source;

			// Token: 0x04004A30 RID: 18992
			public float[] factors = new float[3];

			// Token: 0x04004A31 RID: 18993
			public GridGraph.TextureData.ChannelUse[] channels = new GridGraph.TextureData.ChannelUse[3];

			// Token: 0x04004A32 RID: 18994
			private Color32[] data;

			// Token: 0x020007B0 RID: 1968
			public enum ChannelUse
			{
				// Token: 0x04004BE0 RID: 19424
				None,
				// Token: 0x04004BE1 RID: 19425
				Penalty,
				// Token: 0x04004BE2 RID: 19426
				Position,
				// Token: 0x04004BE3 RID: 19427
				WalkablePenalty
			}
		}
	}
}
