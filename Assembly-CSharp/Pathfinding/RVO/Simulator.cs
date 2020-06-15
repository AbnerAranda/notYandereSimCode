using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005DA RID: 1498
	public class Simulator
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x001BD158 File Offset: 0x001BB358
		// (set) Token: 0x060028A5 RID: 10405 RVA: 0x001BD160 File Offset: 0x001BB360
		public RVOQuadtree Quadtree { get; private set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x001BD169 File Offset: 0x001BB369
		public float DeltaTime
		{
			get
			{
				return this.deltaTime;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x001BD171 File Offset: 0x001BB371
		public bool Multithreading
		{
			get
			{
				return this.workers != null && this.workers.Length != 0;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x001BD187 File Offset: 0x001BB387
		// (set) Token: 0x060028A9 RID: 10409 RVA: 0x001BD18F File Offset: 0x001BB38F
		public float DesiredDeltaTime
		{
			get
			{
				return this.desiredDeltaTime;
			}
			set
			{
				this.desiredDeltaTime = Math.Max(value, 0f);
			}
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x001BD1A2 File Offset: 0x001BB3A2
		public List<Agent> GetAgents()
		{
			return this.agents;
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x001BD1AA File Offset: 0x001BB3AA
		public List<ObstacleVertex> GetObstacles()
		{
			return this.obstacles;
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x001BD1B4 File Offset: 0x001BB3B4
		public Simulator(int workers, bool doubleBuffering, MovementPlane movementPlane)
		{
			this.workers = new Simulator.Worker[workers];
			this.doubleBuffering = doubleBuffering;
			this.DesiredDeltaTime = 1f;
			this.movementPlane = movementPlane;
			this.Quadtree = new RVOQuadtree();
			for (int i = 0; i < workers; i++)
			{
				this.workers[i] = new Simulator.Worker(this);
			}
			this.agents = new List<Agent>();
			this.obstacles = new List<ObstacleVertex>();
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x001BD25C File Offset: 0x001BB45C
		public void ClearAgents()
		{
			this.BlockUntilSimulationStepIsDone();
			for (int i = 0; i < this.agents.Count; i++)
			{
				this.agents[i].simulator = null;
			}
			this.agents.Clear();
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x001BD2A4 File Offset: 0x001BB4A4
		public void OnDestroy()
		{
			if (this.workers != null)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].Terminate();
				}
			}
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x001BD2DC File Offset: 0x001BB4DC
		~Simulator()
		{
			this.OnDestroy();
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x001BD308 File Offset: 0x001BB508
		public IAgent AddAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != null && agent2.simulator == this)
			{
				throw new ArgumentException("The agent is already in the simulation");
			}
			if (agent2.simulator != null)
			{
				throw new ArgumentException("The agent is already added to another simulation");
			}
			agent2.simulator = this;
			this.BlockUntilSimulationStepIsDone();
			this.agents.Add(agent2);
			return agent;
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x001BD38C File Offset: 0x001BB58C
		[Obsolete("Use AddAgent(Vector2,float) instead")]
		public IAgent AddAgent(Vector3 position)
		{
			return this.AddAgent(new Vector2(position.x, position.z), position.y);
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x001BD3AB File Offset: 0x001BB5AB
		public IAgent AddAgent(Vector2 position, float elevationCoordinate)
		{
			return this.AddAgent(new Agent(position, elevationCoordinate));
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x001BD3BC File Offset: 0x001BB5BC
		public void RemoveAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != this)
			{
				throw new ArgumentException("The agent is not added to this simulation");
			}
			this.BlockUntilSimulationStepIsDone();
			agent2.simulator = null;
			if (!this.agents.Remove(agent2))
			{
				throw new ArgumentException("Critical Bug! This should not happen. Please report this.");
			}
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x001BD431 File Offset: 0x001BB631
		public ObstacleVertex AddObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Add(v);
			this.UpdateObstacles();
			return v;
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x001BD45A File Offset: 0x001BB65A
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, bool cycle = true)
		{
			return this.AddObstacle(vertices, height, Matrix4x4.identity, RVOLayer.DefaultObstacle, cycle);
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x001BD46C File Offset: 0x001BB66C
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, Matrix4x4 matrix, RVOLayer layer = RVOLayer.DefaultObstacle, bool cycle = true)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			ObstacleVertex obstacleVertex = null;
			ObstacleVertex obstacleVertex2 = null;
			this.BlockUntilSimulationStepIsDone();
			for (int i = 0; i < vertices.Length; i++)
			{
				ObstacleVertex obstacleVertex3 = new ObstacleVertex
				{
					prev = obstacleVertex2,
					layer = layer,
					height = height
				};
				if (obstacleVertex == null)
				{
					obstacleVertex = obstacleVertex3;
				}
				else
				{
					obstacleVertex2.next = obstacleVertex3;
				}
				obstacleVertex2 = obstacleVertex3;
			}
			if (cycle)
			{
				obstacleVertex2.next = obstacleVertex;
				obstacleVertex.prev = obstacleVertex2;
			}
			this.UpdateObstacle(obstacleVertex, vertices, matrix);
			this.obstacles.Add(obstacleVertex);
			return obstacleVertex;
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x001BD504 File Offset: 0x001BB704
		public ObstacleVertex AddObstacle(Vector3 a, Vector3 b, float height)
		{
			ObstacleVertex obstacleVertex = new ObstacleVertex();
			ObstacleVertex obstacleVertex2 = new ObstacleVertex();
			obstacleVertex.layer = RVOLayer.DefaultObstacle;
			obstacleVertex2.layer = RVOLayer.DefaultObstacle;
			obstacleVertex.prev = obstacleVertex2;
			obstacleVertex2.prev = obstacleVertex;
			obstacleVertex.next = obstacleVertex2;
			obstacleVertex2.next = obstacleVertex;
			obstacleVertex.position = a;
			obstacleVertex2.position = b;
			obstacleVertex.height = height;
			obstacleVertex2.height = height;
			obstacleVertex2.ignore = true;
			obstacleVertex.dir = new Vector2(b.x - a.x, b.z - a.z).normalized;
			obstacleVertex2.dir = -obstacleVertex.dir;
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Add(obstacleVertex);
			this.UpdateObstacles();
			return obstacleVertex;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x001BD5C4 File Offset: 0x001BB7C4
		public void UpdateObstacle(ObstacleVertex obstacle, Vector3[] vertices, Matrix4x4 matrix)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (obstacle == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			bool flag = matrix == Matrix4x4.identity;
			this.BlockUntilSimulationStepIsDone();
			int i = 0;
			ObstacleVertex obstacleVertex = obstacle;
			while (i < vertices.Length)
			{
				obstacleVertex.position = (flag ? vertices[i] : matrix.MultiplyPoint3x4(vertices[i]));
				obstacleVertex = obstacleVertex.next;
				i++;
				if (obstacleVertex == obstacle || obstacleVertex == null)
				{
					obstacleVertex = obstacle;
					do
					{
						if (obstacleVertex.next == null)
						{
							obstacleVertex.dir = Vector2.zero;
						}
						else
						{
							Vector3 vector = obstacleVertex.next.position - obstacleVertex.position;
							obstacleVertex.dir = new Vector2(vector.x, vector.z).normalized;
						}
						obstacleVertex = obstacleVertex.next;
					}
					while (obstacleVertex != obstacle && obstacleVertex != null);
					this.ScheduleCleanObstacles();
					this.UpdateObstacles();
					return;
				}
			}
			Debug.DrawLine(obstacleVertex.prev.position, obstacleVertex.position, Color.red);
			throw new ArgumentException("Obstacle has more vertices than supplied for updating (" + vertices.Length + " supplied)");
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x001BD6EC File Offset: 0x001BB8EC
		private void ScheduleCleanObstacles()
		{
			this.doCleanObstacles = true;
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x00002ACE File Offset: 0x00000CCE
		private void CleanObstacles()
		{
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x001BD6F5 File Offset: 0x001BB8F5
		public void RemoveObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Vertex must not be null");
			}
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Remove(v);
			this.UpdateObstacles();
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x001BD71E File Offset: 0x001BB91E
		public void UpdateObstacles()
		{
			this.doUpdateObstacles = true;
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x001BD728 File Offset: 0x001BB928
		private void BuildQuadtree()
		{
			this.Quadtree.Clear();
			if (this.agents.Count > 0)
			{
				Rect bounds = Rect.MinMaxRect(this.agents[0].position.x, this.agents[0].position.y, this.agents[0].position.x, this.agents[0].position.y);
				for (int i = 1; i < this.agents.Count; i++)
				{
					Vector2 position = this.agents[i].position;
					bounds = Rect.MinMaxRect(Mathf.Min(bounds.xMin, position.x), Mathf.Min(bounds.yMin, position.y), Mathf.Max(bounds.xMax, position.x), Mathf.Max(bounds.yMax, position.y));
				}
				this.Quadtree.SetBounds(bounds);
				for (int j = 0; j < this.agents.Count; j++)
				{
					this.Quadtree.Insert(this.agents[j]);
				}
			}
			this.Quadtree.CalculateSpeeds();
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x001BD86C File Offset: 0x001BBA6C
		private void BlockUntilSimulationStepIsDone()
		{
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x001BD8AC File Offset: 0x001BBAAC
		private void PreCalculation()
		{
			for (int i = 0; i < this.agents.Count; i++)
			{
				this.agents[i].PreCalculation();
			}
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x001BD8E0 File Offset: 0x001BBAE0
		private void CleanAndUpdateObstaclesIfNecessary()
		{
			if (this.doCleanObstacles)
			{
				this.CleanObstacles();
				this.doCleanObstacles = false;
				this.doUpdateObstacles = true;
			}
			if (this.doUpdateObstacles)
			{
				this.doUpdateObstacles = false;
			}
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x001BD910 File Offset: 0x001BBB10
		public void Update()
		{
			if (this.lastStep < 0f)
			{
				this.lastStep = Time.time;
				this.deltaTime = this.DesiredDeltaTime;
			}
			if (Time.time - this.lastStep >= this.DesiredDeltaTime)
			{
				this.deltaTime = Time.time - this.lastStep;
				this.lastStep = Time.time;
				this.deltaTime = Math.Max(this.deltaTime, 0.0005f);
				if (this.Multithreading)
				{
					if (this.doubleBuffering)
					{
						for (int i = 0; i < this.workers.Length; i++)
						{
							this.workers[i].WaitOne();
						}
						for (int j = 0; j < this.agents.Count; j++)
						{
							this.agents[j].PostCalculation();
						}
					}
					this.PreCalculation();
					this.CleanAndUpdateObstaclesIfNecessary();
					this.BuildQuadtree();
					for (int k = 0; k < this.workers.Length; k++)
					{
						this.workers[k].start = k * this.agents.Count / this.workers.Length;
						this.workers[k].end = (k + 1) * this.agents.Count / this.workers.Length;
					}
					for (int l = 0; l < this.workers.Length; l++)
					{
						this.workers[l].Execute(1);
					}
					for (int m = 0; m < this.workers.Length; m++)
					{
						this.workers[m].WaitOne();
					}
					for (int n = 0; n < this.workers.Length; n++)
					{
						this.workers[n].Execute(0);
					}
					if (!this.doubleBuffering)
					{
						for (int num = 0; num < this.workers.Length; num++)
						{
							this.workers[num].WaitOne();
						}
						for (int num2 = 0; num2 < this.agents.Count; num2++)
						{
							this.agents[num2].PostCalculation();
						}
						return;
					}
				}
				else
				{
					this.PreCalculation();
					this.CleanAndUpdateObstaclesIfNecessary();
					this.BuildQuadtree();
					for (int num3 = 0; num3 < this.agents.Count; num3++)
					{
						this.agents[num3].BufferSwitch();
					}
					for (int num4 = 0; num4 < this.agents.Count; num4++)
					{
						this.agents[num4].CalculateNeighbours();
						this.agents[num4].CalculateVelocity(this.coroutineWorkerContext);
					}
					for (int num5 = 0; num5 < this.agents.Count; num5++)
					{
						this.agents[num5].PostCalculation();
					}
				}
			}
		}

		// Token: 0x04004367 RID: 17255
		private readonly bool doubleBuffering = true;

		// Token: 0x04004368 RID: 17256
		private float desiredDeltaTime = 0.05f;

		// Token: 0x04004369 RID: 17257
		private readonly Simulator.Worker[] workers;

		// Token: 0x0400436A RID: 17258
		private List<Agent> agents;

		// Token: 0x0400436B RID: 17259
		public List<ObstacleVertex> obstacles;

		// Token: 0x0400436D RID: 17261
		private float deltaTime;

		// Token: 0x0400436E RID: 17262
		private float lastStep = -99999f;

		// Token: 0x0400436F RID: 17263
		private bool doUpdateObstacles;

		// Token: 0x04004370 RID: 17264
		private bool doCleanObstacles;

		// Token: 0x04004371 RID: 17265
		public float symmetryBreakingBias = 0.1f;

		// Token: 0x04004372 RID: 17266
		public readonly MovementPlane movementPlane;

		// Token: 0x04004373 RID: 17267
		private Simulator.WorkerContext coroutineWorkerContext = new Simulator.WorkerContext();

		// Token: 0x02000783 RID: 1923
		internal class WorkerContext
		{
			// Token: 0x04004B26 RID: 19238
			public Agent.VOBuffer vos = new Agent.VOBuffer(16);

			// Token: 0x04004B27 RID: 19239
			public const int KeepCount = 3;

			// Token: 0x04004B28 RID: 19240
			public Vector2[] bestPos = new Vector2[3];

			// Token: 0x04004B29 RID: 19241
			public float[] bestSizes = new float[3];

			// Token: 0x04004B2A RID: 19242
			public float[] bestScores = new float[4];

			// Token: 0x04004B2B RID: 19243
			public Vector2[] samplePos = new Vector2[50];

			// Token: 0x04004B2C RID: 19244
			public float[] sampleSize = new float[50];
		}

		// Token: 0x02000784 RID: 1924
		private class Worker
		{
			// Token: 0x06002DE6 RID: 11750 RVA: 0x001D4DF8 File Offset: 0x001D2FF8
			public Worker(Simulator sim)
			{
				this.simulator = sim;
				new Thread(new ThreadStart(this.Run))
				{
					IsBackground = true,
					Name = "RVO Simulator Thread"
				}.Start();
			}

			// Token: 0x06002DE7 RID: 11751 RVA: 0x001D4E5D File Offset: 0x001D305D
			public void Execute(int task)
			{
				this.task = task;
				this.waitFlag.Reset();
				this.runFlag.Set();
			}

			// Token: 0x06002DE8 RID: 11752 RVA: 0x001D4E7E File Offset: 0x001D307E
			public void WaitOne()
			{
				if (!this.terminate)
				{
					this.waitFlag.WaitOne();
				}
			}

			// Token: 0x06002DE9 RID: 11753 RVA: 0x001D4E94 File Offset: 0x001D3094
			public void Terminate()
			{
				this.WaitOne();
				this.terminate = true;
				this.Execute(-1);
			}

			// Token: 0x06002DEA RID: 11754 RVA: 0x001D4EAC File Offset: 0x001D30AC
			public void Run()
			{
				this.runFlag.WaitOne();
				while (!this.terminate)
				{
					try
					{
						List<Agent> agents = this.simulator.GetAgents();
						if (this.task == 0)
						{
							for (int i = this.start; i < this.end; i++)
							{
								agents[i].CalculateNeighbours();
								agents[i].CalculateVelocity(this.context);
							}
						}
						else if (this.task == 1)
						{
							for (int j = this.start; j < this.end; j++)
							{
								agents[j].BufferSwitch();
							}
						}
						else
						{
							if (this.task != 2)
							{
								Debug.LogError("Invalid Task Number: " + this.task);
								throw new Exception("Invalid Task Number: " + this.task);
							}
							this.simulator.BuildQuadtree();
						}
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
					this.waitFlag.Set();
					this.runFlag.WaitOne();
				}
			}

			// Token: 0x04004B2D RID: 19245
			public int start;

			// Token: 0x04004B2E RID: 19246
			public int end;

			// Token: 0x04004B2F RID: 19247
			private readonly AutoResetEvent runFlag = new AutoResetEvent(false);

			// Token: 0x04004B30 RID: 19248
			private readonly ManualResetEvent waitFlag = new ManualResetEvent(true);

			// Token: 0x04004B31 RID: 19249
			private readonly Simulator simulator;

			// Token: 0x04004B32 RID: 19250
			private int task;

			// Token: 0x04004B33 RID: 19251
			private bool terminate;

			// Token: 0x04004B34 RID: 19252
			private Simulator.WorkerContext context = new Simulator.WorkerContext();
		}
	}
}
