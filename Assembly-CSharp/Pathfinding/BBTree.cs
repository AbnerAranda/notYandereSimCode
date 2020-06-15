using System;
using System.Diagnostics;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200058B RID: 1419
	public class BBTree : IAstarPooledObject
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x001A88DC File Offset: 0x001A6ADC
		public Rect Size
		{
			get
			{
				if (this.count == 0)
				{
					return new Rect(0f, 0f, 0f, 0f);
				}
				IntRect rect = this.tree[0].rect;
				return Rect.MinMaxRect((float)rect.xmin * 0.001f, (float)rect.ymin * 0.001f, (float)rect.xmax * 0.001f, (float)rect.ymax * 0.001f);
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x001A8958 File Offset: 0x001A6B58
		public void Clear()
		{
			this.count = 0;
			this.leafNodes = 0;
			if (this.tree != null)
			{
				ArrayPool<BBTree.BBTreeBox>.Release(ref this.tree, false);
			}
			if (this.nodeLookup != null)
			{
				for (int i = 0; i < this.nodeLookup.Length; i++)
				{
					this.nodeLookup[i] = null;
				}
				ArrayPool<TriangleMeshNode>.Release(ref this.nodeLookup, false);
			}
			this.tree = ArrayPool<BBTree.BBTreeBox>.Claim(0);
			this.nodeLookup = ArrayPool<TriangleMeshNode>.Claim(0);
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x001A89CF File Offset: 0x001A6BCF
		void IAstarPooledObject.OnEnterPool()
		{
			this.Clear();
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x001A89D8 File Offset: 0x001A6BD8
		private void EnsureCapacity(int c)
		{
			if (c > this.tree.Length)
			{
				BBTree.BBTreeBox[] array = ArrayPool<BBTree.BBTreeBox>.Claim(c);
				this.tree.CopyTo(array, 0);
				ArrayPool<BBTree.BBTreeBox>.Release(ref this.tree, false);
				this.tree = array;
			}
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x001A8A18 File Offset: 0x001A6C18
		private void EnsureNodeCapacity(int c)
		{
			if (c > this.nodeLookup.Length)
			{
				TriangleMeshNode[] array = ArrayPool<TriangleMeshNode>.Claim(c);
				this.nodeLookup.CopyTo(array, 0);
				ArrayPool<TriangleMeshNode>.Release(ref this.nodeLookup, false);
				this.nodeLookup = array;
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x001A8A58 File Offset: 0x001A6C58
		private int GetBox(IntRect rect)
		{
			if (this.count >= this.tree.Length)
			{
				this.EnsureCapacity(this.count + 1);
			}
			this.tree[this.count] = new BBTree.BBTreeBox(rect);
			this.count++;
			return this.count - 1;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x001A8AB0 File Offset: 0x001A6CB0
		public void RebuildFrom(TriangleMeshNode[] nodes)
		{
			this.Clear();
			if (nodes.Length == 0)
			{
				return;
			}
			this.EnsureCapacity(Mathf.CeilToInt((float)nodes.Length * 2.1f));
			this.EnsureNodeCapacity(Mathf.CeilToInt((float)nodes.Length * 1.1f));
			int[] array = ArrayPool<int>.Claim(nodes.Length);
			for (int i = 0; i < nodes.Length; i++)
			{
				array[i] = i;
			}
			IntRect[] array2 = ArrayPool<IntRect>.Claim(nodes.Length);
			for (int j = 0; j < nodes.Length; j++)
			{
				Int3 @int;
				Int3 int2;
				Int3 int3;
				nodes[j].GetVertices(out @int, out int2, out int3);
				IntRect intRect = new IntRect(@int.x, @int.z, @int.x, @int.z);
				intRect = intRect.ExpandToContain(int2.x, int2.z);
				intRect = intRect.ExpandToContain(int3.x, int3.z);
				array2[j] = intRect;
			}
			this.RebuildFromInternal(nodes, array, array2, 0, nodes.Length, false);
			ArrayPool<int>.Release(ref array, false);
			ArrayPool<IntRect>.Release(ref array2, false);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x001A8BAC File Offset: 0x001A6DAC
		private static int SplitByX(TriangleMeshNode[] nodes, int[] permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				if (nodes[permutation[i]].position.x > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x001A8BF4 File Offset: 0x001A6DF4
		private static int SplitByZ(TriangleMeshNode[] nodes, int[] permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				if (nodes[permutation[i]].position.z > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x001A8C3C File Offset: 0x001A6E3C
		private int RebuildFromInternal(TriangleMeshNode[] nodes, int[] permutation, IntRect[] nodeBounds, int from, int to, bool odd)
		{
			IntRect intRect = BBTree.NodeBounds(permutation, nodeBounds, from, to);
			int box = this.GetBox(intRect);
			if (to - from <= 4)
			{
				int num = this.tree[box].nodeOffset = this.leafNodes * 4;
				this.EnsureNodeCapacity(num + 4);
				this.leafNodes++;
				for (int i = 0; i < 4; i++)
				{
					this.nodeLookup[num + i] = ((i < to - from) ? nodes[permutation[from + i]] : null);
				}
				return box;
			}
			int num2;
			if (odd)
			{
				int divider = (intRect.xmin + intRect.xmax) / 2;
				num2 = BBTree.SplitByX(nodes, permutation, from, to, divider);
			}
			else
			{
				int divider2 = (intRect.ymin + intRect.ymax) / 2;
				num2 = BBTree.SplitByZ(nodes, permutation, from, to, divider2);
			}
			if (num2 == from || num2 == to)
			{
				if (!odd)
				{
					int divider3 = (intRect.xmin + intRect.xmax) / 2;
					num2 = BBTree.SplitByX(nodes, permutation, from, to, divider3);
				}
				else
				{
					int divider4 = (intRect.ymin + intRect.ymax) / 2;
					num2 = BBTree.SplitByZ(nodes, permutation, from, to, divider4);
				}
				if (num2 == from || num2 == to)
				{
					num2 = (from + to) / 2;
				}
			}
			this.tree[box].left = this.RebuildFromInternal(nodes, permutation, nodeBounds, from, num2, !odd);
			this.tree[box].right = this.RebuildFromInternal(nodes, permutation, nodeBounds, num2, to, !odd);
			return box;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x001A8DB8 File Offset: 0x001A6FB8
		private static IntRect NodeBounds(int[] permutation, IntRect[] nodeBounds, int from, int to)
		{
			IntRect intRect = nodeBounds[permutation[from]];
			for (int i = from + 1; i < to; i++)
			{
				IntRect intRect2 = nodeBounds[permutation[i]];
				intRect.xmin = Math.Min(intRect.xmin, intRect2.xmin);
				intRect.ymin = Math.Min(intRect.ymin, intRect2.ymin);
				intRect.xmax = Math.Max(intRect.xmax, intRect2.xmax);
				intRect.ymax = Math.Max(intRect.ymax, intRect2.ymax);
			}
			return intRect;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x001A8E48 File Offset: 0x001A7048
		[Conditional("ASTARDEBUG")]
		private static void DrawDebugRect(IntRect rect)
		{
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymin), new Vector3((float)rect.xmax, 0f, (float)rect.ymin), Color.white);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymax), new Vector3((float)rect.xmax, 0f, (float)rect.ymax), Color.white);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymin), new Vector3((float)rect.xmin, 0f, (float)rect.ymax), Color.white);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmax, 0f, (float)rect.ymin), new Vector3((float)rect.xmax, 0f, (float)rect.ymax), Color.white);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x001A8F40 File Offset: 0x001A7140
		[Conditional("ASTARDEBUG")]
		private static void DrawDebugNode(TriangleMeshNode node, float yoffset, Color color)
		{
			UnityEngine.Debug.DrawLine((Vector3)node.GetVertex(1) + Vector3.up * yoffset, (Vector3)node.GetVertex(2) + Vector3.up * yoffset, color);
			UnityEngine.Debug.DrawLine((Vector3)node.GetVertex(0) + Vector3.up * yoffset, (Vector3)node.GetVertex(1) + Vector3.up * yoffset, color);
			UnityEngine.Debug.DrawLine((Vector3)node.GetVertex(2) + Vector3.up * yoffset, (Vector3)node.GetVertex(0) + Vector3.up * yoffset, color);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x001A9007 File Offset: 0x001A7207
		public NNInfoInternal QueryClosest(Vector3 p, NNConstraint constraint, out float distance)
		{
			distance = float.PositiveInfinity;
			return this.QueryClosest(p, constraint, ref distance, new NNInfoInternal(null));
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x001A9020 File Offset: 0x001A7220
		public NNInfoInternal QueryClosestXZ(Vector3 p, NNConstraint constraint, ref float distance, NNInfoInternal previous)
		{
			float num = distance * distance;
			float num2 = num;
			if (this.count > 0 && BBTree.SquaredRectPointDistance(this.tree[0].rect, p) < num)
			{
				this.SearchBoxClosestXZ(0, p, ref num, constraint, ref previous);
				if (num < num2)
				{
					distance = Mathf.Sqrt(num);
				}
			}
			return previous;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x001A9074 File Offset: 0x001A7274
		private void SearchBoxClosestXZ(int boxi, Vector3 p, ref float closestSqrDist, NNConstraint constraint, ref NNInfoInternal nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				for (int i = 0; i < 4; i++)
				{
					if (array[bbtreeBox.nodeOffset + i] == null)
					{
						return;
					}
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + i];
					if (constraint == null || constraint.Suitable(triangleMeshNode))
					{
						Vector3 vector = triangleMeshNode.ClosestPointOnNodeXZ(p);
						float num = (vector.x - p.x) * (vector.x - p.x) + (vector.z - p.z) * (vector.z - p.z);
						if (nnInfo.constrainedNode == null || num < closestSqrDist - 1E-06f || (num <= closestSqrDist + 1E-06f && Mathf.Abs(vector.y - p.y) < Mathf.Abs(nnInfo.constClampedPosition.y - p.y)))
						{
							nnInfo.constrainedNode = triangleMeshNode;
							nnInfo.constClampedPosition = vector;
							closestSqrDist = num;
						}
					}
				}
			}
			else
			{
				int left = bbtreeBox.left;
				int right = bbtreeBox.right;
				float num2;
				float num3;
				this.GetOrderedChildren(ref left, ref right, out num2, out num3, p);
				if (num2 <= closestSqrDist)
				{
					this.SearchBoxClosestXZ(left, p, ref closestSqrDist, constraint, ref nnInfo);
				}
				if (num3 <= closestSqrDist)
				{
					this.SearchBoxClosestXZ(right, p, ref closestSqrDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x001A91CC File Offset: 0x001A73CC
		public NNInfoInternal QueryClosest(Vector3 p, NNConstraint constraint, ref float distance, NNInfoInternal previous)
		{
			float num = distance * distance;
			float num2 = num;
			if (this.count > 0 && BBTree.SquaredRectPointDistance(this.tree[0].rect, p) < num)
			{
				this.SearchBoxClosest(0, p, ref num, constraint, ref previous);
				if (num < num2)
				{
					distance = Mathf.Sqrt(num);
				}
			}
			return previous;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x001A9220 File Offset: 0x001A7420
		private void SearchBoxClosest(int boxi, Vector3 p, ref float closestSqrDist, NNConstraint constraint, ref NNInfoInternal nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				for (int i = 0; i < 4; i++)
				{
					if (array[bbtreeBox.nodeOffset + i] == null)
					{
						return;
					}
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + i];
					Vector3 vector = triangleMeshNode.ClosestPointOnNode(p);
					float sqrMagnitude = (vector - p).sqrMagnitude;
					if (sqrMagnitude < closestSqrDist && (constraint == null || constraint.Suitable(triangleMeshNode)))
					{
						nnInfo.constrainedNode = triangleMeshNode;
						nnInfo.constClampedPosition = vector;
						closestSqrDist = sqrMagnitude;
					}
				}
			}
			else
			{
				int left = bbtreeBox.left;
				int right = bbtreeBox.right;
				float num;
				float num2;
				this.GetOrderedChildren(ref left, ref right, out num, out num2, p);
				if (num < closestSqrDist)
				{
					this.SearchBoxClosest(left, p, ref closestSqrDist, constraint, ref nnInfo);
				}
				if (num2 < closestSqrDist)
				{
					this.SearchBoxClosest(right, p, ref closestSqrDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x001A92FC File Offset: 0x001A74FC
		private void GetOrderedChildren(ref int first, ref int second, out float firstDist, out float secondDist, Vector3 p)
		{
			firstDist = BBTree.SquaredRectPointDistance(this.tree[first].rect, p);
			secondDist = BBTree.SquaredRectPointDistance(this.tree[second].rect, p);
			if (secondDist < firstDist)
			{
				int num = first;
				first = second;
				second = num;
				float num2 = firstDist;
				firstDist = secondDist;
				secondDist = num2;
			}
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x001A935D File Offset: 0x001A755D
		public TriangleMeshNode QueryInside(Vector3 p, NNConstraint constraint)
		{
			if (this.count == 0 || !this.tree[0].Contains(p))
			{
				return null;
			}
			return this.SearchBoxInside(0, p, constraint);
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x001A9388 File Offset: 0x001A7588
		private TriangleMeshNode SearchBoxInside(int boxi, Vector3 p, NNConstraint constraint)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				for (int i = 0; i < 4; i++)
				{
					if (array[bbtreeBox.nodeOffset + i] == null)
					{
						break;
					}
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + i];
					if (triangleMeshNode.ContainsPoint((Int3)p) && (constraint == null || constraint.Suitable(triangleMeshNode)))
					{
						return triangleMeshNode;
					}
				}
			}
			else
			{
				if (this.tree[bbtreeBox.left].Contains(p))
				{
					TriangleMeshNode triangleMeshNode2 = this.SearchBoxInside(bbtreeBox.left, p, constraint);
					if (triangleMeshNode2 != null)
					{
						return triangleMeshNode2;
					}
				}
				if (this.tree[bbtreeBox.right].Contains(p))
				{
					TriangleMeshNode triangleMeshNode3 = this.SearchBoxInside(bbtreeBox.right, p, constraint);
					if (triangleMeshNode3 != null)
					{
						return triangleMeshNode3;
					}
				}
			}
			return null;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x001A9454 File Offset: 0x001A7654
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
			if (this.count == 0)
			{
				return;
			}
			this.OnDrawGizmos(0, 0);
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x001A9488 File Offset: 0x001A7688
		private void OnDrawGizmos(int boxi, int depth)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			Vector3 a = (Vector3)new Int3(bbtreeBox.rect.xmin, 0, bbtreeBox.rect.ymin);
			Vector3 vector = (Vector3)new Int3(bbtreeBox.rect.xmax, 0, bbtreeBox.rect.ymax);
			Vector3 vector2 = (a + vector) * 0.5f;
			Vector3 vector3 = (vector - vector2) * 2f;
			vector3 = new Vector3(vector3.x, 1f, vector3.z);
			vector2.y += (float)(depth * 2);
			Gizmos.color = AstarMath.IntToColor(depth, 1f);
			Gizmos.DrawCube(vector2, vector3);
			if (!bbtreeBox.IsLeaf)
			{
				this.OnDrawGizmos(bbtreeBox.left, depth + 1);
				this.OnDrawGizmos(bbtreeBox.right, depth + 1);
			}
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x001A9570 File Offset: 0x001A7770
		private static bool NodeIntersectsCircle(TriangleMeshNode node, Vector3 p, float radius)
		{
			return float.IsPositiveInfinity(radius) || (p - node.ClosestPointOnNode(p)).sqrMagnitude < radius * radius;
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x001A95A4 File Offset: 0x001A77A4
		private static bool RectIntersectsCircle(IntRect r, Vector3 p, float radius)
		{
			if (float.IsPositiveInfinity(radius))
			{
				return true;
			}
			Vector3 vector = p;
			p.x = Math.Max(p.x, (float)r.xmin * 0.001f);
			p.x = Math.Min(p.x, (float)r.xmax * 0.001f);
			p.z = Math.Max(p.z, (float)r.ymin * 0.001f);
			p.z = Math.Min(p.z, (float)r.ymax * 0.001f);
			return (p.x - vector.x) * (p.x - vector.x) + (p.z - vector.z) * (p.z - vector.z) < radius * radius;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x001A9678 File Offset: 0x001A7878
		private static float SquaredRectPointDistance(IntRect r, Vector3 p)
		{
			Vector3 vector = p;
			p.x = Math.Max(p.x, (float)r.xmin * 0.001f);
			p.x = Math.Min(p.x, (float)r.xmax * 0.001f);
			p.z = Math.Max(p.z, (float)r.ymin * 0.001f);
			p.z = Math.Min(p.z, (float)r.ymax * 0.001f);
			return (p.x - vector.x) * (p.x - vector.x) + (p.z - vector.z) * (p.z - vector.z);
		}

		// Token: 0x040041CE RID: 16846
		private BBTree.BBTreeBox[] tree;

		// Token: 0x040041CF RID: 16847
		private TriangleMeshNode[] nodeLookup;

		// Token: 0x040041D0 RID: 16848
		private int count;

		// Token: 0x040041D1 RID: 16849
		private int leafNodes;

		// Token: 0x040041D2 RID: 16850
		private const int MaximumLeafSize = 4;

		// Token: 0x0200075D RID: 1885
		private struct BBTreeBox
		{
			// Token: 0x17000690 RID: 1680
			// (get) Token: 0x06002D8E RID: 11662 RVA: 0x001D32F3 File Offset: 0x001D14F3
			public bool IsLeaf
			{
				get
				{
					return this.nodeOffset >= 0;
				}
			}

			// Token: 0x06002D8F RID: 11663 RVA: 0x001D3304 File Offset: 0x001D1504
			public BBTreeBox(IntRect rect)
			{
				this.nodeOffset = -1;
				this.rect = rect;
				this.left = (this.right = -1);
			}

			// Token: 0x06002D90 RID: 11664 RVA: 0x001D3330 File Offset: 0x001D1530
			public BBTreeBox(int nodeOffset, IntRect rect)
			{
				this.nodeOffset = nodeOffset;
				this.rect = rect;
				this.left = (this.right = -1);
			}

			// Token: 0x06002D91 RID: 11665 RVA: 0x001D335C File Offset: 0x001D155C
			public bool Contains(Vector3 point)
			{
				Int3 @int = (Int3)point;
				return this.rect.Contains(@int.x, @int.z);
			}

			// Token: 0x04004A83 RID: 19075
			public IntRect rect;

			// Token: 0x04004A84 RID: 19076
			public int nodeOffset;

			// Token: 0x04004A85 RID: 19077
			public int left;

			// Token: 0x04004A86 RID: 19078
			public int right;
		}
	}
}
