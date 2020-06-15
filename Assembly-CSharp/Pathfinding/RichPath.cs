using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200052B RID: 1323
	public class RichPath
	{
		// Token: 0x06002194 RID: 8596 RVA: 0x0018EC43 File Offset: 0x0018CE43
		public RichPath()
		{
			this.Clear();
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0018EC5C File Offset: 0x0018CE5C
		public void Clear()
		{
			this.parts.Clear();
			this.currentPart = 0;
			this.Endpoint = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0018EC8C File Offset: 0x0018CE8C
		public void Initialize(Seeker seeker, Path path, bool mergePartEndpoints, bool simplificationMode)
		{
			if (path.error)
			{
				throw new ArgumentException("Path has an error");
			}
			List<GraphNode> path2 = path.path;
			if (path2.Count == 0)
			{
				throw new ArgumentException("Path traverses no nodes");
			}
			this.seeker = seeker;
			for (int i = 0; i < this.parts.Count; i++)
			{
				RichFunnel richFunnel = this.parts[i] as RichFunnel;
				RichSpecial richSpecial = this.parts[i] as RichSpecial;
				if (richFunnel != null)
				{
					ObjectPool<RichFunnel>.Release(ref richFunnel);
				}
				else if (richSpecial != null)
				{
					ObjectPool<RichSpecial>.Release(ref richSpecial);
				}
			}
			this.Clear();
			this.Endpoint = path.vectorPath[path.vectorPath.Count - 1];
			for (int j = 0; j < path2.Count; j++)
			{
				if (path2[j] is TriangleMeshNode)
				{
					NavmeshBase navmeshBase = AstarData.GetGraph(path2[j]) as NavmeshBase;
					if (navmeshBase == null)
					{
						throw new Exception("Found a TriangleMeshNode that was not in a NavmeshBase graph");
					}
					RichFunnel richFunnel2 = ObjectPool<RichFunnel>.Claim().Initialize(this, navmeshBase);
					richFunnel2.funnelSimplification = simplificationMode;
					int num = j;
					uint graphIndex = path2[num].GraphIndex;
					while (j < path2.Count && (path2[j].GraphIndex == graphIndex || path2[j] is NodeLink3Node))
					{
						j++;
					}
					j--;
					if (num == 0)
					{
						richFunnel2.exactStart = path.vectorPath[0];
					}
					else
					{
						richFunnel2.exactStart = (Vector3)path2[mergePartEndpoints ? (num - 1) : num].position;
					}
					if (j == path2.Count - 1)
					{
						richFunnel2.exactEnd = path.vectorPath[path.vectorPath.Count - 1];
					}
					else
					{
						richFunnel2.exactEnd = (Vector3)path2[mergePartEndpoints ? (j + 1) : j].position;
					}
					richFunnel2.BuildFunnelCorridor(path2, num, j);
					this.parts.Add(richFunnel2);
				}
				else if (NodeLink2.GetNodeLink(path2[j]) != null)
				{
					NodeLink2 nodeLink = NodeLink2.GetNodeLink(path2[j]);
					int num2 = j;
					uint graphIndex2 = path2[num2].GraphIndex;
					j++;
					while (j < path2.Count && path2[j].GraphIndex == graphIndex2)
					{
						j++;
					}
					j--;
					if (j - num2 > 1)
					{
						throw new Exception("NodeLink2 path length greater than two (2) nodes. " + (j - num2));
					}
					if (j - num2 != 0)
					{
						RichSpecial item = ObjectPool<RichSpecial>.Claim().Initialize(nodeLink, path2[num2]);
						this.parts.Add(item);
					}
				}
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x0018EF51 File Offset: 0x0018D151
		// (set) Token: 0x06002198 RID: 8600 RVA: 0x0018EF59 File Offset: 0x0018D159
		public Vector3 Endpoint { get; private set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0018EF62 File Offset: 0x0018D162
		public bool CompletedAllParts
		{
			get
			{
				return this.currentPart >= this.parts.Count;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x0018EF7A File Offset: 0x0018D17A
		public bool IsLastPart
		{
			get
			{
				return this.currentPart >= this.parts.Count - 1;
			}
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0018EF94 File Offset: 0x0018D194
		public void NextPart()
		{
			this.currentPart = Mathf.Min(this.currentPart + 1, this.parts.Count);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0018EFB4 File Offset: 0x0018D1B4
		public RichPathPart GetCurrentPart()
		{
			if (this.parts.Count == 0)
			{
				return null;
			}
			if (this.currentPart >= this.parts.Count)
			{
				return this.parts[this.parts.Count - 1];
			}
			return this.parts[this.currentPart];
		}

		// Token: 0x04003F58 RID: 16216
		private int currentPart;

		// Token: 0x04003F59 RID: 16217
		private readonly List<RichPathPart> parts = new List<RichPathPart>();

		// Token: 0x04003F5A RID: 16218
		public Seeker seeker;

		// Token: 0x04003F5B RID: 16219
		public ITransform transform;
	}
}
