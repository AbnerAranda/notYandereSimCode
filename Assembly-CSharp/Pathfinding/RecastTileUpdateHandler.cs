using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000574 RID: 1396
	[AddComponentMenu("Pathfinding/Navmesh/RecastTileUpdateHandler")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_recast_tile_update_handler.php")]
	public class RecastTileUpdateHandler : MonoBehaviour
	{
		// Token: 0x0600249E RID: 9374 RVA: 0x0019CAFF File Offset: 0x0019ACFF
		public void SetGraph(RecastGraph graph)
		{
			this.graph = graph;
			if (graph == null)
			{
				return;
			}
			this.dirtyTiles = new bool[graph.tileXCount * graph.tileZCount];
			this.anyDirtyTiles = false;
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x0019CB2C File Offset: 0x0019AD2C
		public void ScheduleUpdate(Bounds bounds)
		{
			if (this.graph == null)
			{
				if (AstarPath.active != null)
				{
					this.SetGraph(AstarPath.active.data.recastGraph);
				}
				if (this.graph == null)
				{
					Debug.LogError("Received tile update request (from RecastTileUpdate), but no RecastGraph could be found to handle it");
					return;
				}
			}
			int num = Mathf.CeilToInt(this.graph.characterRadius / this.graph.cellSize) + 3;
			bounds.Expand(new Vector3((float)num, 0f, (float)num) * this.graph.cellSize * 2f);
			IntRect touchingTiles = this.graph.GetTouchingTiles(bounds);
			if (touchingTiles.Width * touchingTiles.Height > 0)
			{
				if (!this.anyDirtyTiles)
				{
					this.earliestDirty = Time.time;
					this.anyDirtyTiles = true;
				}
				for (int i = touchingTiles.ymin; i <= touchingTiles.ymax; i++)
				{
					for (int j = touchingTiles.xmin; j <= touchingTiles.xmax; j++)
					{
						this.dirtyTiles[i * this.graph.tileXCount + j] = true;
					}
				}
			}
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0019CC40 File Offset: 0x0019AE40
		private void OnEnable()
		{
			RecastTileUpdate.OnNeedUpdates += this.ScheduleUpdate;
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x0019CC53 File Offset: 0x0019AE53
		private void OnDisable()
		{
			RecastTileUpdate.OnNeedUpdates -= this.ScheduleUpdate;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x0019CC66 File Offset: 0x0019AE66
		private void Update()
		{
			if (this.anyDirtyTiles && Time.time - this.earliestDirty >= this.maxThrottlingDelay && this.graph != null)
			{
				this.UpdateDirtyTiles();
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0019CC94 File Offset: 0x0019AE94
		public void UpdateDirtyTiles()
		{
			if (this.graph == null)
			{
				new InvalidOperationException("No graph is set on this object");
			}
			if (this.graph.tileXCount * this.graph.tileZCount != this.dirtyTiles.Length)
			{
				Debug.LogError("Graph has changed dimensions. Clearing queued graph updates and resetting.");
				this.SetGraph(this.graph);
				return;
			}
			for (int i = 0; i < this.graph.tileZCount; i++)
			{
				for (int j = 0; j < this.graph.tileXCount; j++)
				{
					if (this.dirtyTiles[i * this.graph.tileXCount + j])
					{
						this.dirtyTiles[i * this.graph.tileXCount + j] = false;
						Bounds tileBounds = this.graph.GetTileBounds(j, i, 1, 1);
						tileBounds.extents *= 0.5f;
						GraphUpdateObject graphUpdateObject = new GraphUpdateObject(tileBounds);
						graphUpdateObject.nnConstraint.graphMask = 1 << (int)this.graph.graphIndex;
						AstarPath.active.UpdateGraphs(graphUpdateObject);
					}
				}
			}
			this.anyDirtyTiles = false;
		}

		// Token: 0x04004107 RID: 16647
		private RecastGraph graph;

		// Token: 0x04004108 RID: 16648
		private bool[] dirtyTiles;

		// Token: 0x04004109 RID: 16649
		private bool anyDirtyTiles;

		// Token: 0x0400410A RID: 16650
		private float earliestDirty = float.NegativeInfinity;

		// Token: 0x0400410B RID: 16651
		public float maxThrottlingDelay = 0.5f;
	}
}
