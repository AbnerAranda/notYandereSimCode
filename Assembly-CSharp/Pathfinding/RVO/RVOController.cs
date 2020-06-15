using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005DD RID: 1501
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Controller")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_controller.php")]
	public class RVOController : VersionedMonoBehaviour
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x001BE227 File Offset: 0x001BC427
		// (set) Token: 0x060028CC RID: 10444 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public LayerMask mask
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x0002D199 File Offset: 0x0002B399
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public bool enableRotation
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x0019A43D File Offset: 0x0019863D
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public float rotationSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x0019A43D File Offset: 0x0019863D
		// (set) Token: 0x060028D2 RID: 10450 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public float maxSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x001BE22F File Offset: 0x001BC42F
		public MovementPlane movementPlane
		{
			get
			{
				if (this.simulator != null)
				{
					return this.simulator.movementPlane;
				}
				if (RVOSimulator.active)
				{
					return RVOSimulator.active.movementPlane;
				}
				return MovementPlane.XZ;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x001BE25D File Offset: 0x001BC45D
		// (set) Token: 0x060028D5 RID: 10453 RVA: 0x001BE265 File Offset: 0x001BC465
		public IAgent rvoAgent { get; private set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x001BE26E File Offset: 0x001BC46E
		// (set) Token: 0x060028D7 RID: 10455 RVA: 0x001BE276 File Offset: 0x001BC476
		public Simulator simulator { get; private set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x001BE27F File Offset: 0x001BC47F
		public Vector3 position
		{
			get
			{
				return this.To3D(this.rvoAgent.Position, this.rvoAgent.ElevationCoordinate);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x001BE2A0 File Offset: 0x001BC4A0
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x001BE2D3 File Offset: 0x001BC4D3
		public Vector3 velocity
		{
			get
			{
				float num = (Time.deltaTime > 0.0001f) ? Time.deltaTime : 0.02f;
				return this.CalculateMovementDelta(num) / num;
			}
			set
			{
				this.rvoAgent.ForceSetVelocity(this.To2D(value));
			}
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x001BE2E8 File Offset: 0x001BC4E8
		public Vector3 CalculateMovementDelta(float deltaTime)
		{
			if (this.rvoAgent == null)
			{
				return Vector3.zero;
			}
			return this.To3D(Vector2.ClampMagnitude(this.rvoAgent.CalculatedTargetPoint - this.To2D((this.ai != null) ? this.ai.position : this.tr.position), this.rvoAgent.CalculatedSpeed * deltaTime), 0f);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x001BE356 File Offset: 0x001BC556
		public Vector3 CalculateMovementDelta(Vector3 position, float deltaTime)
		{
			return this.To3D(Vector2.ClampMagnitude(this.rvoAgent.CalculatedTargetPoint - this.To2D(position), this.rvoAgent.CalculatedSpeed * deltaTime), 0f);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x001BE38C File Offset: 0x001BC58C
		public void SetCollisionNormal(Vector3 normal)
		{
			this.rvoAgent.SetCollisionNormal(this.To2D(normal));
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x001BE3A0 File Offset: 0x001BC5A0
		[Obsolete("Set the 'velocity' property instead")]
		public void ForceSetVelocity(Vector3 velocity)
		{
			this.velocity = velocity;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x001BE3AC File Offset: 0x001BC5AC
		public Vector2 To2D(Vector3 p)
		{
			float num;
			return this.To2D(p, out num);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x001BE3C2 File Offset: 0x001BC5C2
		public Vector2 To2D(Vector3 p, out float elevation)
		{
			if (this.movementPlane == MovementPlane.XY)
			{
				elevation = -p.z;
				return new Vector2(p.x, p.y);
			}
			elevation = p.y;
			return new Vector2(p.x, p.z);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x001BE401 File Offset: 0x001BC601
		public Vector3 To3D(Vector2 p, float elevationCoordinate)
		{
			if (this.movementPlane == MovementPlane.XY)
			{
				return new Vector3(p.x, p.y, -elevationCoordinate);
			}
			return new Vector3(p.x, elevationCoordinate, p.y);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x001BE432 File Offset: 0x001BC632
		private void OnDisable()
		{
			if (this.simulator == null)
			{
				return;
			}
			this.simulator.RemoveAgent(this.rvoAgent);
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x001BE450 File Offset: 0x001BC650
		private void OnEnable()
		{
			this.tr = base.transform;
			this.ai = base.GetComponent<IAstarAI>();
			if (RVOSimulator.active == null)
			{
				Debug.LogError("No RVOSimulator component found in the scene. Please add one.");
				base.enabled = false;
				return;
			}
			this.simulator = RVOSimulator.active.GetSimulator();
			if (this.rvoAgent != null)
			{
				this.simulator.AddAgent(this.rvoAgent);
				return;
			}
			this.rvoAgent = this.simulator.AddAgent(Vector2.zero, 0f);
			this.rvoAgent.PreCalculationCallback = new Action(this.UpdateAgentProperties);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x001BE4F4 File Offset: 0x001BC6F4
		protected void UpdateAgentProperties()
		{
			this.rvoAgent.Radius = Mathf.Max(0.001f, this.radius);
			this.rvoAgent.AgentTimeHorizon = this.agentTimeHorizon;
			this.rvoAgent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
			this.rvoAgent.Locked = this.locked;
			this.rvoAgent.MaxNeighbours = this.maxNeighbours;
			this.rvoAgent.DebugDraw = this.debug;
			this.rvoAgent.Layer = this.layer;
			this.rvoAgent.CollidesWith = this.collidesWith;
			this.rvoAgent.Priority = this.priority;
			float num;
			this.rvoAgent.Position = this.To2D((this.ai != null) ? this.ai.position : this.tr.position, out num);
			if (this.movementPlane == MovementPlane.XZ)
			{
				this.rvoAgent.Height = this.height;
				this.rvoAgent.ElevationCoordinate = num + this.center - 0.5f * this.height;
				return;
			}
			this.rvoAgent.Height = 1f;
			this.rvoAgent.ElevationCoordinate = 0f;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x001BE631 File Offset: 0x001BC831
		public void SetTarget(Vector3 pos, float speed, float maxSpeed)
		{
			if (this.simulator == null)
			{
				return;
			}
			this.rvoAgent.SetTarget(this.To2D(pos), speed, maxSpeed);
			if (this.lockWhenNotMoving)
			{
				this.locked = (speed < 0.001f);
			}
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x001BE668 File Offset: 0x001BC868
		public void Move(Vector3 vel)
		{
			if (this.simulator == null)
			{
				return;
			}
			Vector2 b = this.To2D(vel);
			float magnitude = b.magnitude;
			this.rvoAgent.SetTarget(this.To2D((this.ai != null) ? this.ai.position : this.tr.position) + b, magnitude, magnitude);
			if (this.lockWhenNotMoving)
			{
				this.locked = (magnitude < 0.001f);
			}
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x001BE6DD File Offset: 0x001BC8DD
		[Obsolete("Use transform.position instead, the RVOController can now handle that without any issues.")]
		public void Teleport(Vector3 pos)
		{
			this.tr.position = pos;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x001BE6EC File Offset: 0x001BC8EC
		private void OnDrawGizmos()
		{
			Color color = RVOController.GizmoColor * (this.locked ? 0.5f : 1f);
			Vector3 vector = (this.ai != null) ? this.ai.position : base.transform.position;
			if (this.movementPlane == MovementPlane.XY)
			{
				Draw.Gizmos.Cylinder(vector, Vector3.forward, 0f, this.radius, color);
				return;
			}
			Draw.Gizmos.Cylinder(vector + this.To3D(Vector2.zero, this.center - this.height * 0.5f), this.To3D(Vector2.zero, 1f), this.height, this.radius, color);
		}

		// Token: 0x0400437B RID: 17275
		[Tooltip("Radius of the agent")]
		public float radius = 0.5f;

		// Token: 0x0400437C RID: 17276
		[Tooltip("Height of the agent. In world units")]
		public float height = 2f;

		// Token: 0x0400437D RID: 17277
		[Tooltip("A locked unit cannot move. Other units will still avoid it. But avoidance quality is not the best")]
		public bool locked;

		// Token: 0x0400437E RID: 17278
		[Tooltip("Automatically set #locked to true when desired velocity is approximately zero")]
		public bool lockWhenNotMoving;

		// Token: 0x0400437F RID: 17279
		[Tooltip("How far into the future to look for collisions with other agents (in seconds)")]
		public float agentTimeHorizon = 2f;

		// Token: 0x04004380 RID: 17280
		[Tooltip("How far into the future to look for collisions with obstacles (in seconds)")]
		public float obstacleTimeHorizon = 2f;

		// Token: 0x04004381 RID: 17281
		[Tooltip("Max number of other agents to take into account.\nA smaller value can reduce CPU load, a higher value can lead to better local avoidance quality.")]
		public int maxNeighbours = 10;

		// Token: 0x04004382 RID: 17282
		public RVOLayer layer = RVOLayer.DefaultAgent;

		// Token: 0x04004383 RID: 17283
		[EnumFlag]
		public RVOLayer collidesWith = (RVOLayer)(-1);

		// Token: 0x04004384 RID: 17284
		[HideInInspector]
		[Obsolete]
		public float wallAvoidForce = 1f;

		// Token: 0x04004385 RID: 17285
		[HideInInspector]
		[Obsolete]
		public float wallAvoidFalloff = 1f;

		// Token: 0x04004386 RID: 17286
		[Tooltip("How strongly other agents will avoid this agent")]
		[Range(0f, 1f)]
		public float priority = 0.5f;

		// Token: 0x04004387 RID: 17287
		[Tooltip("Center of the agent relative to the pivot point of this game object")]
		public float center = 1f;

		// Token: 0x0400438A RID: 17290
		protected Transform tr;

		// Token: 0x0400438B RID: 17291
		protected IAstarAI ai;

		// Token: 0x0400438C RID: 17292
		public bool debug;

		// Token: 0x0400438D RID: 17293
		private static readonly Color GizmoColor = new Color(0.9411765f, 0.8352941f, 0.117647059f);
	}
}
