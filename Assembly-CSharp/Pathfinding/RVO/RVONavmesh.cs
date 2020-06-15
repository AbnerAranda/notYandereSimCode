using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005DE RID: 1502
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Navmesh")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_navmesh.php")]
	public class RVONavmesh : GraphModifier
	{
		// Token: 0x060028EB RID: 10475 RVA: 0x001BE848 File Offset: 0x001BCA48
		public override void OnPostCacheLoad()
		{
			this.OnLatePostScan();
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x001BE848 File Offset: 0x001BCA48
		public override void OnGraphsPostUpdate()
		{
			this.OnLatePostScan();
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x001BE850 File Offset: 0x001BCA50
		public override void OnLatePostScan()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.RemoveObstacles();
			NavGraph[] graphs = AstarPath.active.graphs;
			RVOSimulator active = RVOSimulator.active;
			if (active == null)
			{
				throw new NullReferenceException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			this.lastSim = active.GetSimulator();
			for (int i = 0; i < graphs.Length; i++)
			{
				RecastGraph recastGraph = graphs[i] as RecastGraph;
				INavmesh navmesh = graphs[i] as INavmesh;
				GridGraph gridGraph = graphs[i] as GridGraph;
				if (recastGraph != null)
				{
					foreach (NavmeshTile navmesh2 in recastGraph.GetTiles())
					{
						this.AddGraphObstacles(this.lastSim, navmesh2);
					}
				}
				else if (navmesh != null)
				{
					this.AddGraphObstacles(this.lastSim, navmesh);
				}
				else if (gridGraph != null)
				{
					this.AddGraphObstacles(this.lastSim, gridGraph);
				}
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x001BE925 File Offset: 0x001BCB25
		protected override void OnDisable()
		{
			base.OnDisable();
			this.RemoveObstacles();
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x001BE934 File Offset: 0x001BCB34
		public void RemoveObstacles()
		{
			if (this.lastSim != null)
			{
				for (int i = 0; i < this.obstacles.Count; i++)
				{
					this.lastSim.RemoveObstacle(this.obstacles[i]);
				}
				this.lastSim = null;
			}
			this.obstacles.Clear();
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x001BE988 File Offset: 0x001BCB88
		private void AddGraphObstacles(Simulator sim, GridGraph grid)
		{
			bool reverse = Vector3.Dot(grid.transform.TransformVector(Vector3.up), (sim.movementPlane == MovementPlane.XY) ? Vector3.back : Vector3.up) > 0f;
			GraphUtilities.GetContours(grid, delegate(Vector3[] vertices)
			{
				if (reverse)
				{
					Array.Reverse(vertices);
				}
				this.obstacles.Add(sim.AddObstacle(vertices, this.wallHeight, true));
			}, this.wallHeight * 0.4f, null);
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x001BEA04 File Offset: 0x001BCC04
		private void AddGraphObstacles(Simulator simulator, INavmesh navmesh)
		{
			GraphUtilities.GetContours(navmesh, delegate(List<Int3> vertices, bool cycle)
			{
				Vector3[] array = new Vector3[vertices.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (Vector3)vertices[i];
				}
				ListPool<Int3>.Release(vertices);
				this.obstacles.Add(simulator.AddObstacle(array, this.wallHeight, cycle));
			});
		}

		// Token: 0x0400438E RID: 17294
		public float wallHeight = 5f;

		// Token: 0x0400438F RID: 17295
		private readonly List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x04004390 RID: 17296
		private Simulator lastSim;
	}
}
