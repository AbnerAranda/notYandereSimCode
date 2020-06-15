using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005F8 RID: 1528
	public class RetainedGizmos
	{
		// Token: 0x060029E7 RID: 10727 RVA: 0x001C4CD0 File Offset: 0x001C2ED0
		public GraphGizmoHelper GetSingleFrameGizmoHelper(AstarPath active)
		{
			RetainedGizmos.Hasher hasher = default(RetainedGizmos.Hasher);
			hasher.AddHash(Time.realtimeSinceStartup.GetHashCode());
			this.Draw(hasher);
			return this.GetGizmoHelper(active, hasher);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x001C4D09 File Offset: 0x001C2F09
		public GraphGizmoHelper GetGizmoHelper(AstarPath active, RetainedGizmos.Hasher hasher)
		{
			GraphGizmoHelper graphGizmoHelper = ObjectPool<GraphGizmoHelper>.Claim();
			graphGizmoHelper.Init(active, hasher, this);
			return graphGizmoHelper;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x001C4D19 File Offset: 0x001C2F19
		private void PoolMesh(Mesh mesh)
		{
			mesh.Clear();
			this.cachedMeshes.Push(mesh);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x001C4D2D File Offset: 0x001C2F2D
		private Mesh GetMesh()
		{
			if (this.cachedMeshes.Count > 0)
			{
				return this.cachedMeshes.Pop();
			}
			return new Mesh
			{
				hideFlags = HideFlags.DontSave
			};
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x001C4D56 File Offset: 0x001C2F56
		public bool HasCachedMesh(RetainedGizmos.Hasher hasher)
		{
			return this.existingHashes.Contains(hasher.Hash);
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x001C4D6A File Offset: 0x001C2F6A
		public bool Draw(RetainedGizmos.Hasher hasher)
		{
			this.usedHashes.Add(hasher.Hash);
			return this.HasCachedMesh(hasher);
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x001C4D88 File Offset: 0x001C2F88
		public void DrawExisting()
		{
			for (int i = 0; i < this.meshes.Count; i++)
			{
				this.usedHashes.Add(this.meshes[i].hash);
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x001C4DC8 File Offset: 0x001C2FC8
		public void FinalizeDraw()
		{
			this.RemoveUnusedMeshes(this.meshes);
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.current);
			if (this.surfaceMaterial == null || this.lineMaterial == null)
			{
				return;
			}
			for (int i = 0; i <= 1; i++)
			{
				Material material = (i == 0) ? this.surfaceMaterial : this.lineMaterial;
				for (int j = 0; j < material.passCount; j++)
				{
					material.SetPass(j);
					for (int k = 0; k < this.meshes.Count; k++)
					{
						if (this.meshes[k].lines == (material == this.lineMaterial) && GeometryUtility.TestPlanesAABB(planes, this.meshes[k].mesh.bounds))
						{
							Graphics.DrawMeshNow(this.meshes[k].mesh, Matrix4x4.identity);
						}
					}
				}
			}
			this.usedHashes.Clear();
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x001C4ECC File Offset: 0x001C30CC
		public void ClearCache()
		{
			this.usedHashes.Clear();
			this.RemoveUnusedMeshes(this.meshes);
			while (this.cachedMeshes.Count > 0)
			{
				UnityEngine.Object.DestroyImmediate(this.cachedMeshes.Pop());
			}
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x001C4F08 File Offset: 0x001C3108
		private void RemoveUnusedMeshes(List<RetainedGizmos.MeshWithHash> meshList)
		{
			int i = 0;
			int num = 0;
			while (i < meshList.Count)
			{
				if (num == meshList.Count)
				{
					num--;
					meshList.RemoveAt(num);
				}
				else if (this.usedHashes.Contains(meshList[num].hash))
				{
					meshList[i] = meshList[num];
					i++;
					num++;
				}
				else
				{
					this.PoolMesh(meshList[num].mesh);
					this.existingHashes.Remove(meshList[num].hash);
					num++;
				}
			}
		}

		// Token: 0x04004421 RID: 17441
		private List<RetainedGizmos.MeshWithHash> meshes = new List<RetainedGizmos.MeshWithHash>();

		// Token: 0x04004422 RID: 17442
		private HashSet<ulong> usedHashes = new HashSet<ulong>();

		// Token: 0x04004423 RID: 17443
		private HashSet<ulong> existingHashes = new HashSet<ulong>();

		// Token: 0x04004424 RID: 17444
		private Stack<Mesh> cachedMeshes = new Stack<Mesh>();

		// Token: 0x04004425 RID: 17445
		public Material surfaceMaterial;

		// Token: 0x04004426 RID: 17446
		public Material lineMaterial;

		// Token: 0x02000795 RID: 1941
		public struct Hasher
		{
			// Token: 0x06002E10 RID: 11792 RVA: 0x001D642C File Offset: 0x001D462C
			public Hasher(AstarPath active)
			{
				this.hash = 0UL;
				this.debugData = active.debugPathData;
				this.includePathSearchInfo = (this.debugData != null && (active.debugMode == GraphDebugMode.F || active.debugMode == GraphDebugMode.G || active.debugMode == GraphDebugMode.H || active.showSearchTree));
				this.AddHash((int)active.debugMode);
				this.AddHash(active.debugFloor.GetHashCode());
				this.AddHash(active.debugRoof.GetHashCode());
			}

			// Token: 0x06002E11 RID: 11793 RVA: 0x001D64B0 File Offset: 0x001D46B0
			public void AddHash(int hash)
			{
				this.hash = (1572869UL * this.hash ^ (ulong)((long)hash));
			}

			// Token: 0x06002E12 RID: 11794 RVA: 0x001D64C8 File Offset: 0x001D46C8
			public void HashNode(GraphNode node)
			{
				this.AddHash(node.GetGizmoHashCode());
				if (this.includePathSearchInfo)
				{
					PathNode pathNode = this.debugData.GetPathNode(node.NodeIndex);
					this.AddHash((int)pathNode.pathID);
					this.AddHash((pathNode.pathID == this.debugData.PathID) ? 1 : 0);
					this.AddHash((int)pathNode.F);
				}
			}

			// Token: 0x17000697 RID: 1687
			// (get) Token: 0x06002E13 RID: 11795 RVA: 0x001D6530 File Offset: 0x001D4730
			public ulong Hash
			{
				get
				{
					return this.hash;
				}
			}

			// Token: 0x04004B87 RID: 19335
			private ulong hash;

			// Token: 0x04004B88 RID: 19336
			private bool includePathSearchInfo;

			// Token: 0x04004B89 RID: 19337
			private PathHandler debugData;
		}

		// Token: 0x02000796 RID: 1942
		public class Builder : IAstarPooledObject
		{
			// Token: 0x06002E14 RID: 11796 RVA: 0x001D6538 File Offset: 0x001D4738
			public void DrawMesh(RetainedGizmos gizmos, Vector3[] vertices, List<int> triangles, Color[] colors)
			{
				Mesh mesh = gizmos.GetMesh();
				mesh.vertices = vertices;
				mesh.SetTriangles(triangles, 0);
				mesh.colors = colors;
				mesh.UploadMeshData(true);
				this.meshes.Add(mesh);
			}

			// Token: 0x06002E15 RID: 11797 RVA: 0x001D6578 File Offset: 0x001D4778
			public void DrawWireCube(GraphTransform tr, Bounds bounds, Color color)
			{
				Vector3 min = bounds.min;
				Vector3 max = bounds.max;
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, min.z)), tr.Transform(new Vector3(max.x, min.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, min.z)), tr.Transform(new Vector3(max.x, min.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, max.z)), tr.Transform(new Vector3(min.x, min.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, max.z)), tr.Transform(new Vector3(min.x, min.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, max.y, min.z)), tr.Transform(new Vector3(max.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, max.y, min.z)), tr.Transform(new Vector3(max.x, max.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, max.y, max.z)), tr.Transform(new Vector3(min.x, max.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, max.y, max.z)), tr.Transform(new Vector3(min.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, min.z)), tr.Transform(new Vector3(min.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, min.z)), tr.Transform(new Vector3(max.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, max.z)), tr.Transform(new Vector3(max.x, max.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, max.z)), tr.Transform(new Vector3(min.x, max.y, max.z)), color);
			}

			// Token: 0x06002E16 RID: 11798 RVA: 0x001D68A4 File Offset: 0x001D4AA4
			public void DrawLine(Vector3 start, Vector3 end, Color color)
			{
				this.lines.Add(start);
				this.lines.Add(end);
				Color32 item = color;
				this.lineColors.Add(item);
				this.lineColors.Add(item);
			}

			// Token: 0x06002E17 RID: 11799 RVA: 0x001D68E8 File Offset: 0x001D4AE8
			public void Submit(RetainedGizmos gizmos, RetainedGizmos.Hasher hasher)
			{
				this.SubmitLines(gizmos, hasher.Hash);
				this.SubmitMeshes(gizmos, hasher.Hash);
			}

			// Token: 0x06002E18 RID: 11800 RVA: 0x001D6908 File Offset: 0x001D4B08
			private void SubmitMeshes(RetainedGizmos gizmos, ulong hash)
			{
				for (int i = 0; i < this.meshes.Count; i++)
				{
					gizmos.meshes.Add(new RetainedGizmos.MeshWithHash
					{
						hash = hash,
						mesh = this.meshes[i],
						lines = false
					});
					gizmos.existingHashes.Add(hash);
				}
			}

			// Token: 0x06002E19 RID: 11801 RVA: 0x001D6970 File Offset: 0x001D4B70
			private void SubmitLines(RetainedGizmos gizmos, ulong hash)
			{
				int num = (this.lines.Count + 32766 - 1) / 32766;
				for (int i = 0; i < num; i++)
				{
					int num2 = 32766 * i;
					int num3 = Mathf.Min(num2 + 32766, this.lines.Count);
					int num4 = num3 - num2;
					List<Vector3> list = ListPool<Vector3>.Claim(num4 * 2);
					List<Color32> list2 = ListPool<Color32>.Claim(num4 * 2);
					List<Vector3> list3 = ListPool<Vector3>.Claim(num4 * 2);
					List<Vector2> list4 = ListPool<Vector2>.Claim(num4 * 2);
					List<int> list5 = ListPool<int>.Claim(num4 * 3);
					for (int j = num2; j < num3; j++)
					{
						Vector3 item = this.lines[j];
						list.Add(item);
						list.Add(item);
						Color32 item2 = this.lineColors[j];
						list2.Add(item2);
						list2.Add(item2);
						list4.Add(new Vector2(0f, 0f));
						list4.Add(new Vector2(1f, 0f));
					}
					for (int k = num2; k < num3; k += 2)
					{
						Vector3 item3 = this.lines[k + 1] - this.lines[k];
						list3.Add(item3);
						list3.Add(item3);
						list3.Add(item3);
						list3.Add(item3);
					}
					int l = 0;
					int num5 = 0;
					while (l < num4 * 3)
					{
						list5.Add(num5);
						list5.Add(num5 + 1);
						list5.Add(num5 + 2);
						list5.Add(num5 + 1);
						list5.Add(num5 + 3);
						list5.Add(num5 + 2);
						l += 6;
						num5 += 4;
					}
					Mesh mesh = gizmos.GetMesh();
					if (mesh.isReadable)
					{
						mesh.SetVertices(list);
						mesh.SetTriangles(list5, 0);
						mesh.SetColors(list2);
						mesh.SetNormals(list3);
						mesh.SetUVs(0, list4);
						mesh.UploadMeshData(true);
						ListPool<Vector3>.Release(ref list);
						ListPool<Color32>.Release(ref list2);
						ListPool<Vector3>.Release(ref list3);
						ListPool<Vector2>.Release(ref list4);
						ListPool<int>.Release(ref list5);
						gizmos.meshes.Add(new RetainedGizmos.MeshWithHash
						{
							hash = hash,
							mesh = mesh,
							lines = true
						});
						gizmos.existingHashes.Add(hash);
					}
				}
			}

			// Token: 0x06002E1A RID: 11802 RVA: 0x001D6BDF File Offset: 0x001D4DDF
			void IAstarPooledObject.OnEnterPool()
			{
				this.lines.Clear();
				this.lineColors.Clear();
				this.meshes.Clear();
			}

			// Token: 0x04004B8A RID: 19338
			private List<Vector3> lines = new List<Vector3>();

			// Token: 0x04004B8B RID: 19339
			private List<Color32> lineColors = new List<Color32>();

			// Token: 0x04004B8C RID: 19340
			private List<Mesh> meshes = new List<Mesh>();
		}

		// Token: 0x02000797 RID: 1943
		private struct MeshWithHash
		{
			// Token: 0x04004B8D RID: 19341
			public ulong hash;

			// Token: 0x04004B8E RID: 19342
			public Mesh mesh;

			// Token: 0x04004B8F RID: 19343
			public bool lines;
		}
	}
}
