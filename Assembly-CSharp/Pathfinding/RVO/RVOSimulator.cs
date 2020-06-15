using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005E0 RID: 1504
	[ExecuteInEditMode]
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Simulator")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_simulator.php")]
	public class RVOSimulator : VersionedMonoBehaviour
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x001BF082 File Offset: 0x001BD282
		// (set) Token: 0x06002907 RID: 10503 RVA: 0x001BF089 File Offset: 0x001BD289
		public static RVOSimulator active { get; private set; }

		// Token: 0x06002908 RID: 10504 RVA: 0x001BF091 File Offset: 0x001BD291
		public Simulator GetSimulator()
		{
			if (this.simulator == null)
			{
				this.Awake();
			}
			return this.simulator;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x001BF0A7 File Offset: 0x001BD2A7
		private void OnEnable()
		{
			RVOSimulator.active = this;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x001BF0B0 File Offset: 0x001BD2B0
		protected override void Awake()
		{
			base.Awake();
			if (this.simulator == null && Application.isPlaying)
			{
				int workers = AstarPath.CalculateThreadCount(this.workerThreads);
				this.simulator = new Simulator(workers, this.doubleBuffering, this.movementPlane);
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x001BF0F8 File Offset: 0x001BD2F8
		private void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.desiredSimulationFPS < 1)
			{
				this.desiredSimulationFPS = 1;
			}
			Simulator simulator = this.GetSimulator();
			simulator.DesiredDeltaTime = 1f / (float)this.desiredSimulationFPS;
			simulator.symmetryBreakingBias = this.symmetryBreakingBias;
			simulator.Update();
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x001BF147 File Offset: 0x001BD347
		private void OnDestroy()
		{
			RVOSimulator.active = null;
			if (this.simulator != null)
			{
				this.simulator.OnDestroy();
			}
		}

		// Token: 0x0400439B RID: 17307
		[Tooltip("Desired FPS for rvo simulation. It is usually not necessary to run a crowd simulation at a very high fps.\nUsually 10-30 fps is enough, but can be increased for better quality.\nThe rvo simulation will never run at a higher fps than the game")]
		public int desiredSimulationFPS = 20;

		// Token: 0x0400439C RID: 17308
		[Tooltip("Number of RVO worker threads. If set to None, no multithreading will be used.")]
		public ThreadCount workerThreads = ThreadCount.Two;

		// Token: 0x0400439D RID: 17309
		[Tooltip("Calculate local avoidance in between frames.\nThis can increase jitter in the agents' movement so use it only if you really need the performance boost. It will also reduce the responsiveness of the agents to the commands you send to them.")]
		public bool doubleBuffering;

		// Token: 0x0400439E RID: 17310
		[Tooltip("Bias agents to pass each other on the right side.\nIf the desired velocity of an agent puts it on a collision course with another agent or an obstacle its desired velocity will be rotated this number of radians (1 radian is approximately 57°) to the right. This helps to break up symmetries and makes it possible to resolve some situations much faster.\n\nWhen many agents have the same goal this can however have the side effect that the group clustered around the target point may as a whole start to spin around the target point.")]
		[Range(0f, 0.2f)]
		public float symmetryBreakingBias = 0.1f;

		// Token: 0x0400439F RID: 17311
		[Tooltip("Determines if the XY (2D) or XZ (3D) plane is used for movement")]
		public MovementPlane movementPlane;

		// Token: 0x040043A0 RID: 17312
		public bool drawObstacles;

		// Token: 0x040043A1 RID: 17313
		private Simulator simulator;
	}
}
