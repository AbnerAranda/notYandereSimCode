using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO.Sampled
{
	// Token: 0x020005E2 RID: 1506
	public class Agent : IAgent
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x001BF310 File Offset: 0x001BD510
		// (set) Token: 0x06002916 RID: 10518 RVA: 0x001BF318 File Offset: 0x001BD518
		public Vector2 Position { get; set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x001BF321 File Offset: 0x001BD521
		// (set) Token: 0x06002918 RID: 10520 RVA: 0x001BF329 File Offset: 0x001BD529
		public float ElevationCoordinate { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x001BF332 File Offset: 0x001BD532
		// (set) Token: 0x0600291A RID: 10522 RVA: 0x001BF33A File Offset: 0x001BD53A
		public Vector2 CalculatedTargetPoint { get; private set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x001BF343 File Offset: 0x001BD543
		// (set) Token: 0x0600291C RID: 10524 RVA: 0x001BF34B File Offset: 0x001BD54B
		public float CalculatedSpeed { get; private set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x001BF354 File Offset: 0x001BD554
		// (set) Token: 0x0600291E RID: 10526 RVA: 0x001BF35C File Offset: 0x001BD55C
		public bool Locked { get; set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x001BF365 File Offset: 0x001BD565
		// (set) Token: 0x06002920 RID: 10528 RVA: 0x001BF36D File Offset: 0x001BD56D
		public float Radius { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x001BF376 File Offset: 0x001BD576
		// (set) Token: 0x06002922 RID: 10530 RVA: 0x001BF37E File Offset: 0x001BD57E
		public float Height { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x001BF387 File Offset: 0x001BD587
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x001BF38F File Offset: 0x001BD58F
		public float AgentTimeHorizon { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x001BF398 File Offset: 0x001BD598
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x001BF3A0 File Offset: 0x001BD5A0
		public float ObstacleTimeHorizon { get; set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x001BF3A9 File Offset: 0x001BD5A9
		// (set) Token: 0x06002928 RID: 10536 RVA: 0x001BF3B1 File Offset: 0x001BD5B1
		public int MaxNeighbours { get; set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x001BF3BA File Offset: 0x001BD5BA
		// (set) Token: 0x0600292A RID: 10538 RVA: 0x001BF3C2 File Offset: 0x001BD5C2
		public int NeighbourCount { get; private set; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x001BF3CB File Offset: 0x001BD5CB
		// (set) Token: 0x0600292C RID: 10540 RVA: 0x001BF3D3 File Offset: 0x001BD5D3
		public RVOLayer Layer { get; set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x001BF3DC File Offset: 0x001BD5DC
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x001BF3E4 File Offset: 0x001BD5E4
		public RVOLayer CollidesWith { get; set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x001BF3ED File Offset: 0x001BD5ED
		// (set) Token: 0x06002930 RID: 10544 RVA: 0x001BF3F5 File Offset: 0x001BD5F5
		public bool DebugDraw
		{
			get
			{
				return this.debugDraw;
			}
			set
			{
				this.debugDraw = (value && this.simulator != null && !this.simulator.Multithreading);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x001BF419 File Offset: 0x001BD619
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x001BF421 File Offset: 0x001BD621
		public float Priority { get; set; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x001BF42A File Offset: 0x001BD62A
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x001BF432 File Offset: 0x001BD632
		public Action PreCalculationCallback { private get; set; }

		// Token: 0x06002935 RID: 10549 RVA: 0x001BF43B File Offset: 0x001BD63B
		public void SetTarget(Vector2 targetPoint, float desiredSpeed, float maxSpeed)
		{
			maxSpeed = Math.Max(maxSpeed, 0f);
			desiredSpeed = Math.Min(Math.Max(desiredSpeed, 0f), maxSpeed);
			this.nextTargetPoint = targetPoint;
			this.nextDesiredSpeed = desiredSpeed;
			this.nextMaxSpeed = maxSpeed;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x001BF472 File Offset: 0x001BD672
		public void SetCollisionNormal(Vector2 normal)
		{
			this.collisionNormal = normal;
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x001BF47C File Offset: 0x001BD67C
		public void ForceSetVelocity(Vector2 velocity)
		{
			this.nextTargetPoint = (this.CalculatedTargetPoint = this.position + velocity * 1000f);
			this.nextDesiredSpeed = (this.CalculatedSpeed = velocity.magnitude);
			this.manuallyControlled = true;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x00158E73 File Offset: 0x00157073
		public List<ObstacleVertex> NeighbourObstacles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x001BF4CC File Offset: 0x001BD6CC
		public Agent(Vector2 pos, float elevationCoordinate)
		{
			this.AgentTimeHorizon = 2f;
			this.ObstacleTimeHorizon = 2f;
			this.Height = 5f;
			this.Radius = 5f;
			this.MaxNeighbours = 10;
			this.Locked = false;
			this.Position = pos;
			this.ElevationCoordinate = elevationCoordinate;
			this.Layer = RVOLayer.DefaultAgent;
			this.CollidesWith = (RVOLayer)(-1);
			this.Priority = 0.5f;
			this.CalculatedTargetPoint = pos;
			this.CalculatedSpeed = 0f;
			this.SetTarget(pos, 0f, 0f);
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x001BF590 File Offset: 0x001BD790
		public void BufferSwitch()
		{
			this.radius = this.Radius;
			this.height = this.Height;
			this.maxSpeed = this.nextMaxSpeed;
			this.desiredSpeed = this.nextDesiredSpeed;
			this.agentTimeHorizon = this.AgentTimeHorizon;
			this.obstacleTimeHorizon = this.ObstacleTimeHorizon;
			this.maxNeighbours = this.MaxNeighbours;
			this.locked = (this.Locked && !this.manuallyControlled);
			this.position = this.Position;
			this.elevationCoordinate = this.ElevationCoordinate;
			this.collidesWith = this.CollidesWith;
			this.layer = this.Layer;
			if (this.locked)
			{
				this.desiredTargetPointInVelocitySpace = this.position;
				this.desiredVelocity = (this.currentVelocity = Vector2.zero);
				return;
			}
			this.desiredTargetPointInVelocitySpace = this.nextTargetPoint - this.position;
			this.currentVelocity = (this.CalculatedTargetPoint - this.position).normalized * this.CalculatedSpeed;
			this.desiredVelocity = this.desiredTargetPointInVelocitySpace.normalized * this.desiredSpeed;
			if (this.collisionNormal != Vector2.zero)
			{
				this.collisionNormal.Normalize();
				float num = Vector2.Dot(this.currentVelocity, this.collisionNormal);
				if (num < 0f)
				{
					this.currentVelocity -= this.collisionNormal * num;
				}
				this.collisionNormal = Vector2.zero;
			}
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x001BF720 File Offset: 0x001BD920
		public void PreCalculation()
		{
			if (this.PreCalculationCallback != null)
			{
				this.PreCalculationCallback();
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x001BF738 File Offset: 0x001BD938
		public void PostCalculation()
		{
			if (!this.manuallyControlled)
			{
				this.CalculatedTargetPoint = this.calculatedTargetPoint;
				this.CalculatedSpeed = this.calculatedSpeed;
			}
			List<ObstacleVertex> list = this.obstaclesBuffered;
			this.obstaclesBuffered = this.obstacles;
			this.obstacles = list;
			this.manuallyControlled = false;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x001BF788 File Offset: 0x001BD988
		public void CalculateNeighbours()
		{
			this.neighbours.Clear();
			this.neighbourDists.Clear();
			if (this.MaxNeighbours > 0 && !this.locked)
			{
				this.simulator.Quadtree.Query(this.position, this.maxSpeed, this.agentTimeHorizon, this.radius, this);
			}
			this.NeighbourCount = this.neighbours.Count;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x001BF7F6 File Offset: 0x001BD9F6
		private static float Sqr(float x)
		{
			return x * x;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x001BF7FC File Offset: 0x001BD9FC
		internal float InsertAgentNeighbour(Agent agent, float rangeSq)
		{
			if (this == agent || (agent.layer & this.collidesWith) == (RVOLayer)0)
			{
				return rangeSq;
			}
			float sqrMagnitude = (agent.position - this.position).sqrMagnitude;
			if (sqrMagnitude < rangeSq)
			{
				if (this.neighbours.Count < this.maxNeighbours)
				{
					this.neighbours.Add(null);
					this.neighbourDists.Add(float.PositiveInfinity);
				}
				int num = this.neighbours.Count - 1;
				if (sqrMagnitude < this.neighbourDists[num])
				{
					while (num != 0 && sqrMagnitude < this.neighbourDists[num - 1])
					{
						this.neighbours[num] = this.neighbours[num - 1];
						this.neighbourDists[num] = this.neighbourDists[num - 1];
						num--;
					}
					this.neighbours[num] = agent;
					this.neighbourDists[num] = sqrMagnitude;
				}
				if (this.neighbours.Count == this.maxNeighbours)
				{
					rangeSq = this.neighbourDists[this.neighbourDists.Count - 1];
				}
			}
			return rangeSq;
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x001B2B10 File Offset: 0x001B0D10
		private static Vector3 FromXZ(Vector2 p)
		{
			return new Vector3(p.x, 0f, p.y);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x001B2AFD File Offset: 0x001B0CFD
		private static Vector2 ToXZ(Vector3 p)
		{
			return new Vector2(p.x, p.z);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x001BF924 File Offset: 0x001BDB24
		private Vector2 To2D(Vector3 p, out float elevation)
		{
			if (this.simulator.movementPlane == MovementPlane.XY)
			{
				elevation = -p.z;
				return new Vector2(p.x, p.y);
			}
			elevation = p.y;
			return new Vector2(p.x, p.z);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x001BF974 File Offset: 0x001BDB74
		private static void DrawVO(Vector2 circleCenter, float radius, Vector2 origin)
		{
			float num = Mathf.Atan2((origin - circleCenter).y, (origin - circleCenter).x);
			float num2 = radius / (origin - circleCenter).magnitude;
			float num3 = (num2 <= 1f) ? Mathf.Abs(Mathf.Acos(num2)) : 0f;
			Draw.Debug.CircleXZ(Agent.FromXZ(circleCenter), radius, Color.black, num - num3, num + num3);
			Vector2 vector = new Vector2(Mathf.Cos(num - num3), Mathf.Sin(num - num3)) * radius;
			Vector2 vector2 = new Vector2(Mathf.Cos(num + num3), Mathf.Sin(num + num3)) * radius;
			Vector2 p = -new Vector2(-vector.y, vector.x);
			Vector2 p2 = new Vector2(-vector2.y, vector2.x);
			vector += circleCenter;
			vector2 += circleCenter;
			Debug.DrawRay(Agent.FromXZ(vector), Agent.FromXZ(p).normalized * 100f, Color.black);
			Debug.DrawRay(Agent.FromXZ(vector2), Agent.FromXZ(p2).normalized * 100f, Color.black);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x001BFAB8 File Offset: 0x001BDCB8
		internal void CalculateVelocity(Simulator.WorkerContext context)
		{
			if (this.manuallyControlled)
			{
				return;
			}
			if (this.locked)
			{
				this.calculatedSpeed = 0f;
				this.calculatedTargetPoint = this.position;
				return;
			}
			Agent.VOBuffer vos = context.vos;
			vos.Clear();
			this.GenerateObstacleVOs(vos);
			this.GenerateNeighbourAgentVOs(vos);
			if (!Agent.BiasDesiredVelocity(vos, ref this.desiredVelocity, ref this.desiredTargetPointInVelocitySpace, this.simulator.symmetryBreakingBias))
			{
				this.calculatedTargetPoint = this.desiredTargetPointInVelocitySpace + this.position;
				this.calculatedSpeed = this.desiredSpeed;
				if (this.DebugDraw)
				{
					Draw.Debug.CrossXZ(Agent.FromXZ(this.calculatedTargetPoint), Color.white, 1f);
				}
				return;
			}
			Vector2 vector = Vector2.zero;
			vector = this.GradientDescent(vos, this.currentVelocity, this.desiredVelocity);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector + this.position), Color.white, 1f);
			}
			this.calculatedTargetPoint = this.position + vector;
			this.calculatedSpeed = Mathf.Min(vector.magnitude, this.maxSpeed);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x001BFBE4 File Offset: 0x001BDDE4
		private static Color Rainbow(float v)
		{
			Color color = new Color(v, 0f, 0f);
			if (color.r > 1f)
			{
				color.g = color.r - 1f;
				color.r = 1f;
			}
			if (color.g > 1f)
			{
				color.b = color.g - 1f;
				color.g = 1f;
			}
			return color;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x001BFC5C File Offset: 0x001BDE5C
		private void GenerateObstacleVOs(Agent.VOBuffer vos)
		{
			float num = this.maxSpeed * this.obstacleTimeHorizon;
			for (int i = 0; i < this.simulator.obstacles.Count; i++)
			{
				ObstacleVertex obstacleVertex = this.simulator.obstacles[i];
				ObstacleVertex obstacleVertex2 = obstacleVertex;
				do
				{
					if (obstacleVertex2.ignore || (obstacleVertex2.layer & this.collidesWith) == (RVOLayer)0)
					{
						obstacleVertex2 = obstacleVertex2.next;
					}
					else
					{
						float a;
						Vector2 vector = this.To2D(obstacleVertex2.position, out a);
						float b;
						Vector2 vector2 = this.To2D(obstacleVertex2.next.position, out b);
						Vector2 normalized = (vector2 - vector).normalized;
						float num2 = Agent.VO.SignedDistanceFromLine(vector, normalized, this.position);
						if (num2 >= -0.01f && num2 < num)
						{
							float t = Vector2.Dot(this.position - vector, vector2 - vector) / (vector2 - vector).sqrMagnitude;
							float num3 = Mathf.Lerp(a, b, t);
							if ((Vector2.Lerp(vector, vector2, t) - this.position).sqrMagnitude < num * num && (this.simulator.movementPlane == MovementPlane.XY || (this.elevationCoordinate <= num3 + obstacleVertex2.height && this.elevationCoordinate + this.height >= num3)))
							{
								vos.Add(Agent.VO.SegmentObstacle(vector2 - this.position, vector - this.position, Vector2.zero, this.radius * 0.01f, 1f / this.ObstacleTimeHorizon, 1f / this.simulator.DeltaTime));
							}
						}
						obstacleVertex2 = obstacleVertex2.next;
					}
				}
				while (obstacleVertex2 != obstacleVertex && obstacleVertex2 != null && obstacleVertex2.next != null);
			}
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x001BFE30 File Offset: 0x001BE030
		private void GenerateNeighbourAgentVOs(Agent.VOBuffer vos)
		{
			float num = 1f / this.agentTimeHorizon;
			Vector2 a = this.currentVelocity;
			for (int i = 0; i < this.neighbours.Count; i++)
			{
				Agent agent = this.neighbours[i];
				if (agent != this)
				{
					float num2 = Math.Min(this.elevationCoordinate + this.height, agent.elevationCoordinate + agent.height);
					float num3 = Math.Max(this.elevationCoordinate, agent.elevationCoordinate);
					if (num2 - num3 >= 0f)
					{
						float num4 = this.radius + agent.radius;
						Vector2 vector = agent.position - this.position;
						float num5;
						if (agent.locked || agent.manuallyControlled)
						{
							num5 = 1f;
						}
						else if (agent.Priority > 1E-05f || this.Priority > 1E-05f)
						{
							num5 = agent.Priority / (this.Priority + agent.Priority);
						}
						else
						{
							num5 = 0.5f;
						}
						Vector2 b = Vector2.Lerp(agent.currentVelocity, agent.desiredVelocity, 2f * num5 - 1f);
						Vector2 vector2 = Vector2.Lerp(a, b, num5);
						vos.Add(new Agent.VO(vector, vector2, num4, num, 1f / this.simulator.DeltaTime));
						if (this.DebugDraw)
						{
							Agent.DrawVO(this.position + vector * num + vector2, num4 * num, this.position + vector2);
						}
					}
				}
			}
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x001BFFBC File Offset: 0x001BE1BC
		private Vector2 GradientDescent(Agent.VOBuffer vos, Vector2 sampleAround1, Vector2 sampleAround2)
		{
			float num;
			Vector2 vector = this.Trace(vos, sampleAround1, out num);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector + this.position), Color.yellow, 0.5f);
			}
			float num2;
			Vector2 vector2 = this.Trace(vos, sampleAround2, out num2);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector2 + this.position), Color.magenta, 0.5f);
			}
			if (num >= num2)
			{
				return vector2;
			}
			return vector;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x001C0040 File Offset: 0x001BE240
		private static bool BiasDesiredVelocity(Agent.VOBuffer vos, ref Vector2 desiredVelocity, ref Vector2 targetPointInVelocitySpace, float maxBiasRadians)
		{
			float magnitude = desiredVelocity.magnitude;
			float num = 0f;
			for (int i = 0; i < vos.length; i++)
			{
				float b;
				vos.buffer[i].Gradient(desiredVelocity, out b);
				num = Mathf.Max(num, b);
			}
			bool result = num > 0f;
			if (magnitude < 0.001f)
			{
				return result;
			}
			float d = Mathf.Min(maxBiasRadians, num / magnitude);
			desiredVelocity += new Vector2(desiredVelocity.y, -desiredVelocity.x) * d;
			targetPointInVelocitySpace += new Vector2(targetPointInVelocitySpace.y, -targetPointInVelocitySpace.x) * d;
			return result;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x001C0104 File Offset: 0x001BE304
		private Vector2 EvaluateGradient(Agent.VOBuffer vos, Vector2 p, out float value)
		{
			Vector2 vector = Vector2.zero;
			value = 0f;
			for (int i = 0; i < vos.length; i++)
			{
				float num;
				Vector2 vector2 = vos.buffer[i].ScaledGradient(p, out num);
				if (num > value)
				{
					value = num;
					vector = vector2;
				}
			}
			Vector2 a = this.desiredVelocity - p;
			float magnitude = a.magnitude;
			if (magnitude > 0.0001f)
			{
				vector += a * (0.1f / magnitude);
				value += magnitude * 0.1f;
			}
			float sqrMagnitude = p.sqrMagnitude;
			if (sqrMagnitude > this.desiredSpeed * this.desiredSpeed)
			{
				float num2 = Mathf.Sqrt(sqrMagnitude);
				if (num2 > this.maxSpeed)
				{
					value += 3f * (num2 - this.maxSpeed);
					vector -= 3f * (p / num2);
				}
				float num3 = 0.2f;
				value += num3 * (num2 - this.desiredSpeed);
				vector -= num3 * (p / num2);
			}
			return vector;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x001C021C File Offset: 0x001BE41C
		private Vector2 Trace(Agent.VOBuffer vos, Vector2 p, out float score)
		{
			float num = Mathf.Max(this.radius, 0.2f * this.desiredSpeed);
			float num2 = float.PositiveInfinity;
			Vector2 result = p;
			for (int i = 0; i < 50; i++)
			{
				float num3 = 1f - (float)i / 50f;
				num3 = Agent.Sqr(num3) * num;
				float num4;
				Vector2 vector = this.EvaluateGradient(vos, p, out num4);
				if (num4 < num2)
				{
					num2 = num4;
					result = p;
				}
				vector.Normalize();
				vector *= num3;
				Vector2 a = p;
				p += vector;
				if (this.DebugDraw)
				{
					Debug.DrawLine(Agent.FromXZ(a + this.position), Agent.FromXZ(p + this.position), Agent.Rainbow((float)i * 0.1f) * new Color(1f, 1f, 1f, 1f));
				}
			}
			score = num2;
			return result;
		}

		// Token: 0x040043A5 RID: 17317
		internal float radius;

		// Token: 0x040043A6 RID: 17318
		internal float height;

		// Token: 0x040043A7 RID: 17319
		internal float desiredSpeed;

		// Token: 0x040043A8 RID: 17320
		internal float maxSpeed;

		// Token: 0x040043A9 RID: 17321
		internal float agentTimeHorizon;

		// Token: 0x040043AA RID: 17322
		internal float obstacleTimeHorizon;

		// Token: 0x040043AB RID: 17323
		internal bool locked;

		// Token: 0x040043AC RID: 17324
		private RVOLayer layer;

		// Token: 0x040043AD RID: 17325
		private RVOLayer collidesWith;

		// Token: 0x040043AE RID: 17326
		private int maxNeighbours;

		// Token: 0x040043AF RID: 17327
		internal Vector2 position;

		// Token: 0x040043B0 RID: 17328
		private float elevationCoordinate;

		// Token: 0x040043B1 RID: 17329
		private Vector2 currentVelocity;

		// Token: 0x040043B2 RID: 17330
		private Vector2 desiredTargetPointInVelocitySpace;

		// Token: 0x040043B3 RID: 17331
		private Vector2 desiredVelocity;

		// Token: 0x040043B4 RID: 17332
		private Vector2 nextTargetPoint;

		// Token: 0x040043B5 RID: 17333
		private float nextDesiredSpeed;

		// Token: 0x040043B6 RID: 17334
		private float nextMaxSpeed;

		// Token: 0x040043B7 RID: 17335
		private Vector2 collisionNormal;

		// Token: 0x040043B8 RID: 17336
		private bool manuallyControlled;

		// Token: 0x040043B9 RID: 17337
		private bool debugDraw;

		// Token: 0x040043C9 RID: 17353
		internal Agent next;

		// Token: 0x040043CA RID: 17354
		private float calculatedSpeed;

		// Token: 0x040043CB RID: 17355
		private Vector2 calculatedTargetPoint;

		// Token: 0x040043CC RID: 17356
		internal Simulator simulator;

		// Token: 0x040043CD RID: 17357
		private List<Agent> neighbours = new List<Agent>();

		// Token: 0x040043CE RID: 17358
		private List<float> neighbourDists = new List<float>();

		// Token: 0x040043CF RID: 17359
		private List<ObstacleVertex> obstaclesBuffered = new List<ObstacleVertex>();

		// Token: 0x040043D0 RID: 17360
		private List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x040043D1 RID: 17361
		private const float DesiredVelocityWeight = 0.1f;

		// Token: 0x040043D2 RID: 17362
		private const float WallWeight = 5f;

		// Token: 0x0200078A RID: 1930
		internal struct VO
		{
			// Token: 0x06002DF3 RID: 11763 RVA: 0x001D547C File Offset: 0x001D367C
			public VO(Vector2 center, Vector2 offset, float radius, float inverseDt, float inverseDeltaTime)
			{
				this.weightFactor = 1f;
				this.weightBonus = 0f;
				this.circleCenter = center * inverseDt + offset;
				this.weightFactor = 4f * Mathf.Exp(-Agent.Sqr(center.sqrMagnitude / (radius * radius))) + 1f;
				if (center.magnitude < radius)
				{
					this.colliding = true;
					this.line1 = center.normalized * (center.magnitude - radius - 0.001f) * 0.3f * inverseDeltaTime;
					this.dir1 = new Vector2(this.line1.y, -this.line1.x).normalized;
					this.line1 += offset;
					this.cutoffDir = Vector2.zero;
					this.cutoffLine = Vector2.zero;
					this.dir2 = Vector2.zero;
					this.line2 = Vector2.zero;
					this.radius = 0f;
				}
				else
				{
					this.colliding = false;
					center *= inverseDt;
					radius *= inverseDt;
					Vector2 b = center + offset;
					float d = center.magnitude - radius + 0.001f;
					this.cutoffLine = center.normalized * d;
					this.cutoffDir = new Vector2(-this.cutoffLine.y, this.cutoffLine.x).normalized;
					this.cutoffLine += offset;
					float num = Mathf.Atan2(-center.y, -center.x);
					float num2 = Mathf.Abs(Mathf.Acos(radius / center.magnitude));
					this.radius = radius;
					this.line1 = new Vector2(Mathf.Cos(num + num2), Mathf.Sin(num + num2));
					this.dir1 = new Vector2(this.line1.y, -this.line1.x);
					this.line2 = new Vector2(Mathf.Cos(num - num2), Mathf.Sin(num - num2));
					this.dir2 = new Vector2(this.line2.y, -this.line2.x);
					this.line1 = this.line1 * radius + b;
					this.line2 = this.line2 * radius + b;
				}
				this.segmentStart = Vector2.zero;
				this.segmentEnd = Vector2.zero;
				this.segment = false;
			}

			// Token: 0x06002DF4 RID: 11764 RVA: 0x001D5714 File Offset: 0x001D3914
			public static Agent.VO SegmentObstacle(Vector2 segmentStart, Vector2 segmentEnd, Vector2 offset, float radius, float inverseDt, float inverseDeltaTime)
			{
				Agent.VO vo = default(Agent.VO);
				vo.weightFactor = 1f;
				vo.weightBonus = Mathf.Max(radius, 1f) * 40f;
				Vector3 vector = VectorMath.ClosestPointOnSegment(segmentStart, segmentEnd, Vector2.zero);
				if (vector.magnitude <= radius)
				{
					vo.colliding = true;
					vo.line1 = vector.normalized * (vector.magnitude - radius) * 0.3f * inverseDeltaTime;
					vo.dir1 = new Vector2(vo.line1.y, -vo.line1.x).normalized;
					vo.line1 += offset;
					vo.cutoffDir = Vector2.zero;
					vo.cutoffLine = Vector2.zero;
					vo.dir2 = Vector2.zero;
					vo.line2 = Vector2.zero;
					vo.radius = 0f;
					vo.segmentStart = Vector2.zero;
					vo.segmentEnd = Vector2.zero;
					vo.segment = false;
				}
				else
				{
					vo.colliding = false;
					segmentStart *= inverseDt;
					segmentEnd *= inverseDt;
					radius *= inverseDt;
					Vector2 normalized = (segmentEnd - segmentStart).normalized;
					vo.cutoffDir = normalized;
					vo.cutoffLine = segmentStart + new Vector2(-normalized.y, normalized.x) * radius;
					vo.cutoffLine += offset;
					float sqrMagnitude = segmentStart.sqrMagnitude;
					Vector2 vector2 = -VectorMath.ComplexMultiply(segmentStart, new Vector2(radius, Mathf.Sqrt(Mathf.Max(0f, sqrMagnitude - radius * radius)))) / sqrMagnitude;
					float sqrMagnitude2 = segmentEnd.sqrMagnitude;
					Vector2 vector3 = -VectorMath.ComplexMultiply(segmentEnd, new Vector2(radius, -Mathf.Sqrt(Mathf.Max(0f, sqrMagnitude2 - radius * radius)))) / sqrMagnitude2;
					vo.line1 = segmentStart + vector2 * radius + offset;
					vo.line2 = segmentEnd + vector3 * radius + offset;
					vo.dir1 = new Vector2(vector2.y, -vector2.x);
					vo.dir2 = new Vector2(vector3.y, -vector3.x);
					vo.segmentStart = segmentStart;
					vo.segmentEnd = segmentEnd;
					vo.radius = radius;
					vo.segment = true;
				}
				return vo;
			}

			// Token: 0x06002DF5 RID: 11765 RVA: 0x001D59C9 File Offset: 0x001D3BC9
			public static float SignedDistanceFromLine(Vector2 a, Vector2 dir, Vector2 p)
			{
				return (p.x - a.x) * dir.y - dir.x * (p.y - a.y);
			}

			// Token: 0x06002DF6 RID: 11766 RVA: 0x001D59F4 File Offset: 0x001D3BF4
			public Vector2 ScaledGradient(Vector2 p, out float weight)
			{
				Vector2 vector = this.Gradient(p, out weight);
				if (weight > 0f)
				{
					vector *= 2f * this.weightFactor;
					weight *= 2f * this.weightFactor;
					weight += 1f + this.weightBonus;
				}
				return vector;
			}

			// Token: 0x06002DF7 RID: 11767 RVA: 0x001D5A4C File Offset: 0x001D3C4C
			public Vector2 Gradient(Vector2 p, out float weight)
			{
				if (this.colliding)
				{
					float num = Agent.VO.SignedDistanceFromLine(this.line1, this.dir1, p);
					if (num >= 0f)
					{
						weight = num;
						return new Vector2(-this.dir1.y, this.dir1.x);
					}
					weight = 0f;
					return new Vector2(0f, 0f);
				}
				else
				{
					float num2 = Agent.VO.SignedDistanceFromLine(this.cutoffLine, this.cutoffDir, p);
					if (num2 <= 0f)
					{
						weight = 0f;
						return Vector2.zero;
					}
					float num3 = Agent.VO.SignedDistanceFromLine(this.line1, this.dir1, p);
					float num4 = Agent.VO.SignedDistanceFromLine(this.line2, this.dir2, p);
					if (num3 < 0f || num4 < 0f)
					{
						weight = 0f;
						return Vector2.zero;
					}
					Vector2 result;
					if (Vector2.Dot(p - this.line1, this.dir1) > 0f && Vector2.Dot(p - this.line2, this.dir2) < 0f)
					{
						if (!this.segment)
						{
							float num5;
							result = VectorMath.Normalize(p - this.circleCenter, out num5);
							weight = this.radius - num5;
							return result;
						}
						if (num2 < this.radius)
						{
							Vector2 b = VectorMath.ClosestPointOnSegment(this.segmentStart, this.segmentEnd, p);
							float num6;
							result = VectorMath.Normalize(p - b, out num6);
							weight = this.radius - num6;
							return result;
						}
					}
					if (this.segment && num2 < num3 && num2 < num4)
					{
						weight = num2;
						result = new Vector2(-this.cutoffDir.y, this.cutoffDir.x);
						return result;
					}
					if (num3 < num4)
					{
						weight = num3;
						result = new Vector2(-this.dir1.y, this.dir1.x);
					}
					else
					{
						weight = num4;
						result = new Vector2(-this.dir2.y, this.dir2.x);
					}
					return result;
				}
			}

			// Token: 0x04004B4B RID: 19275
			private Vector2 line1;

			// Token: 0x04004B4C RID: 19276
			private Vector2 line2;

			// Token: 0x04004B4D RID: 19277
			private Vector2 dir1;

			// Token: 0x04004B4E RID: 19278
			private Vector2 dir2;

			// Token: 0x04004B4F RID: 19279
			private Vector2 cutoffLine;

			// Token: 0x04004B50 RID: 19280
			private Vector2 cutoffDir;

			// Token: 0x04004B51 RID: 19281
			private Vector2 circleCenter;

			// Token: 0x04004B52 RID: 19282
			private bool colliding;

			// Token: 0x04004B53 RID: 19283
			private float radius;

			// Token: 0x04004B54 RID: 19284
			private float weightFactor;

			// Token: 0x04004B55 RID: 19285
			private float weightBonus;

			// Token: 0x04004B56 RID: 19286
			private Vector2 segmentStart;

			// Token: 0x04004B57 RID: 19287
			private Vector2 segmentEnd;

			// Token: 0x04004B58 RID: 19288
			private bool segment;
		}

		// Token: 0x0200078B RID: 1931
		internal class VOBuffer
		{
			// Token: 0x06002DF8 RID: 11768 RVA: 0x001D5C5C File Offset: 0x001D3E5C
			public void Clear()
			{
				this.length = 0;
			}

			// Token: 0x06002DF9 RID: 11769 RVA: 0x001D5C65 File Offset: 0x001D3E65
			public VOBuffer(int n)
			{
				this.buffer = new Agent.VO[n];
				this.length = 0;
			}

			// Token: 0x06002DFA RID: 11770 RVA: 0x001D5C80 File Offset: 0x001D3E80
			public void Add(Agent.VO vo)
			{
				if (this.length >= this.buffer.Length)
				{
					Agent.VO[] array = new Agent.VO[this.buffer.Length * 2];
					this.buffer.CopyTo(array, 0);
					this.buffer = array;
				}
				Agent.VO[] array2 = this.buffer;
				int num = this.length;
				this.length = num + 1;
				array2[num] = vo;
			}

			// Token: 0x04004B59 RID: 19289
			public Agent.VO[] buffer;

			// Token: 0x04004B5A RID: 19290
			public int length;
		}
	}
}
