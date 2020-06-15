using System;
using Pathfinding.RVO;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000526 RID: 1318
	[RequireComponent(typeof(Seeker))]
	public abstract class AIBase : VersionedMonoBehaviour
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x0018C14D File Offset: 0x0018A34D
		public Vector3 position
		{
			get
			{
				if (!this.updatePosition)
				{
					return this.simulatedPosition;
				}
				return this.tr.position;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0018C169 File Offset: 0x0018A369
		public Quaternion rotation
		{
			get
			{
				if (!this.updateRotation)
				{
					return this.simulatedRotation;
				}
				return this.tr.rotation;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x0018C185 File Offset: 0x0018A385
		// (set) Token: 0x060020CD RID: 8397 RVA: 0x0018C18D File Offset: 0x0018A38D
		private protected bool usingGravity { protected get; private set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x0018C198 File Offset: 0x0018A398
		// (set) Token: 0x060020CF RID: 8399 RVA: 0x0018C1C0 File Offset: 0x0018A3C0
		[Obsolete("Use the destination property or the AIDestinationSetter component instead")]
		public Transform target
		{
			get
			{
				AIDestinationSetter component = base.GetComponent<AIDestinationSetter>();
				if (!(component != null))
				{
					return null;
				}
				return component.target;
			}
			set
			{
				this.targetCompatibility = null;
				AIDestinationSetter aidestinationSetter = base.GetComponent<AIDestinationSetter>();
				if (aidestinationSetter == null)
				{
					aidestinationSetter = base.gameObject.AddComponent<AIDestinationSetter>();
				}
				aidestinationSetter.target = value;
				this.destination = ((value != null) ? value.position : new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity));
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x0018C222 File Offset: 0x0018A422
		// (set) Token: 0x060020D1 RID: 8401 RVA: 0x0018C22A File Offset: 0x0018A42A
		public Vector3 destination { get; set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0018C233 File Offset: 0x0018A433
		public Vector3 velocity
		{
			get
			{
				if (this.lastDeltaTime <= 1E-06f)
				{
					return Vector3.zero;
				}
				return (this.prevPosition1 - this.prevPosition2) / this.lastDeltaTime;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0018C264 File Offset: 0x0018A464
		public Vector3 desiredVelocity
		{
			get
			{
				if (this.lastDeltaTime <= 1E-05f)
				{
					return Vector3.zero;
				}
				return this.movementPlane.ToWorld(this.lastDeltaPosition / this.lastDeltaTime, this.verticalVelocity);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0018C29B File Offset: 0x0018A49B
		// (set) Token: 0x060020D5 RID: 8405 RVA: 0x0018C2A3 File Offset: 0x0018A4A3
		public bool isStopped { get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x0018C2AC File Offset: 0x0018A4AC
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x0018C2B4 File Offset: 0x0018A4B4
		public Action onSearchPath { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x0018C2BD File Offset: 0x0018A4BD
		protected virtual bool shouldRecalculatePath
		{
			get
			{
				return Time.time - this.lastRepath >= this.repathRate && !this.waitingForPathCalculation && this.canSearch && !float.IsPositiveInfinity(this.destination.x);
			}
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0018C2F8 File Offset: 0x0018A4F8
		protected AIBase()
		{
			this.destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0018C3AC File Offset: 0x0018A5AC
		public virtual void FindComponents()
		{
			this.tr = base.transform;
			this.seeker = base.GetComponent<Seeker>();
			this.rvoController = base.GetComponent<RVOController>();
			this.controller = base.GetComponent<CharacterController>();
			this.rigid = base.GetComponent<Rigidbody>();
			this.rigid2D = base.GetComponent<Rigidbody2D>();
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x0018C401 File Offset: 0x0018A601
		protected virtual void OnEnable()
		{
			this.FindComponents();
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
			this.Init();
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x0018C437 File Offset: 0x0018A637
		protected virtual void Start()
		{
			this.startHasRun = true;
			this.Init();
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0018C446 File Offset: 0x0018A646
		private void Init()
		{
			if (this.startHasRun)
			{
				this.Teleport(this.position, false);
				this.lastRepath = float.NegativeInfinity;
				if (this.shouldRecalculatePath)
				{
					this.SearchPath();
				}
			}
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x0018C478 File Offset: 0x0018A678
		public virtual void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			if (clearPath)
			{
				this.CancelCurrentPathRequest();
			}
			this.simulatedPosition = newPosition;
			this.prevPosition2 = newPosition;
			this.prevPosition1 = newPosition;
			if (this.updatePosition)
			{
				this.tr.position = newPosition;
			}
			if (this.rvoController != null)
			{
				this.rvoController.Move(Vector3.zero);
			}
			if (clearPath)
			{
				this.SearchPath();
			}
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x0018C4E2 File Offset: 0x0018A6E2
		protected void CancelCurrentPathRequest()
		{
			this.waitingForPathCalculation = false;
			if (this.seeker != null)
			{
				this.seeker.CancelCurrentPathRequest(true);
			}
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x0018C508 File Offset: 0x0018A708
		protected virtual void OnDisable()
		{
			this.CancelCurrentPathRequest();
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Remove(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
			this.velocity2D = Vector3.zero;
			this.accumulatedMovementDelta = Vector3.zero;
			this.verticalVelocity = 0f;
			this.lastDeltaTime = 0f;
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0018C574 File Offset: 0x0018A774
		protected virtual void Update()
		{
			if (this.shouldRecalculatePath)
			{
				this.SearchPath();
			}
			this.usingGravity = (!(this.gravity == Vector3.zero) && (!this.updatePosition || ((this.rigid == null || this.rigid.isKinematic) && (this.rigid2D == null || this.rigid2D.isKinematic))));
			if (this.rigid == null && this.rigid2D == null && this.canMove)
			{
				Vector3 nextPosition;
				Quaternion nextRotation;
				this.MovementUpdate(Time.deltaTime, out nextPosition, out nextRotation);
				this.FinalizeMovement(nextPosition, nextRotation);
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0018C62C File Offset: 0x0018A82C
		protected virtual void FixedUpdate()
		{
			if ((!(this.rigid == null) || !(this.rigid2D == null)) && this.canMove)
			{
				Vector3 nextPosition;
				Quaternion nextRotation;
				this.MovementUpdate(Time.fixedDeltaTime, out nextPosition, out nextRotation);
				this.FinalizeMovement(nextPosition, nextRotation);
			}
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0018C674 File Offset: 0x0018A874
		public void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			this.lastDeltaTime = deltaTime;
			this.MovementUpdateInternal(deltaTime, out nextPosition, out nextRotation);
		}

		// Token: 0x060020E4 RID: 8420
		protected abstract void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation);

		// Token: 0x060020E5 RID: 8421 RVA: 0x0018C686 File Offset: 0x0018A886
		protected virtual void CalculatePathRequestEndpoints(out Vector3 start, out Vector3 end)
		{
			start = this.GetFeetPosition();
			end = this.destination;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x0018C6A0 File Offset: 0x0018A8A0
		public virtual void SearchPath()
		{
			if (float.IsPositiveInfinity(this.destination.x))
			{
				return;
			}
			if (this.onSearchPath != null)
			{
				this.onSearchPath();
			}
			this.lastRepath = Time.time;
			this.waitingForPathCalculation = true;
			this.seeker.CancelCurrentPathRequest(true);
			Vector3 start;
			Vector3 end;
			this.CalculatePathRequestEndpoints(out start, out end);
			this.seeker.StartPath(start, end);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x0018C70C File Offset: 0x0018A90C
		public virtual Vector3 GetFeetPosition()
		{
			if (this.rvoController != null && this.rvoController.enabled && this.rvoController.movementPlane == MovementPlane.XZ)
			{
				return this.position + this.rotation * Vector3.up * (this.rvoController.center - this.rvoController.height * 0.5f);
			}
			if (this.controller != null && this.controller.enabled && this.updatePosition)
			{
				return this.tr.TransformPoint(this.controller.center) - Vector3.up * this.controller.height * 0.5f;
			}
			return this.position;
		}

		// Token: 0x060020E8 RID: 8424
		protected abstract void OnPathComplete(Path newPath);

		// Token: 0x060020E9 RID: 8425 RVA: 0x0018C7E4 File Offset: 0x0018A9E4
		public void SetPath(Path path)
		{
			if (path.PipelineState == PathState.Created)
			{
				this.lastRepath = Time.time;
				this.waitingForPathCalculation = true;
				this.seeker.CancelCurrentPathRequest(true);
				this.seeker.StartPath(path, null);
				return;
			}
			if (path.PipelineState != PathState.Returned)
			{
				throw new ArgumentException("You must call the SetPath method with a path that either has been completely calculated or one whose path calculation has not been started at all. It looks like the path calculation for the path you tried to use has been started, but is not yet finished.");
			}
			if (this.seeker.GetCurrentPath() != path)
			{
				this.seeker.CancelCurrentPathRequest(true);
				this.OnPathComplete(path);
				return;
			}
			throw new ArgumentException("If you calculate the path using seeker.StartPath then this script will pick up the calculated path anyway as it listens for all paths the Seeker finishes calculating. You should not call SetPath in that case.");
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x0018C868 File Offset: 0x0018AA68
		protected void ApplyGravity(float deltaTime)
		{
			if (this.usingGravity)
			{
				float num;
				this.velocity2D += this.movementPlane.ToPlane(deltaTime * (float.IsNaN(this.gravity.x) ? Physics.gravity : this.gravity), out num);
				this.verticalVelocity += num;
				return;
			}
			this.verticalVelocity = 0f;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x0018C8DC File Offset: 0x0018AADC
		protected Vector2 CalculateDeltaToMoveThisFrame(Vector2 position, float distanceToEndOfPath, float deltaTime)
		{
			if (this.rvoController != null && this.rvoController.enabled)
			{
				return this.movementPlane.ToPlane(this.rvoController.CalculateMovementDelta(this.movementPlane.ToWorld(position, 0f), deltaTime));
			}
			return Vector2.ClampMagnitude(this.velocity2D * deltaTime, distanceToEndOfPath);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x0018C93F File Offset: 0x0018AB3F
		public Quaternion SimulateRotationTowards(Vector3 direction, float maxDegrees)
		{
			return this.SimulateRotationTowards(this.movementPlane.ToPlane(direction), maxDegrees);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0018C954 File Offset: 0x0018AB54
		protected Quaternion SimulateRotationTowards(Vector2 direction, float maxDegrees)
		{
			if (direction != Vector2.zero)
			{
				Quaternion quaternion = Quaternion.LookRotation(this.movementPlane.ToWorld(direction, 0f), this.movementPlane.ToWorld(Vector2.zero, 1f));
				if (this.rotationIn2D)
				{
					quaternion *= Quaternion.Euler(90f, 0f, 0f);
				}
				return Quaternion.RotateTowards(this.simulatedRotation, quaternion, maxDegrees);
			}
			return this.simulatedRotation;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0018C9D1 File Offset: 0x0018ABD1
		public virtual void Move(Vector3 deltaPosition)
		{
			this.accumulatedMovementDelta += deltaPosition;
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0018C9E5 File Offset: 0x0018ABE5
		public virtual void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation)
		{
			this.FinalizeRotation(nextRotation);
			this.FinalizePosition(nextPosition);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0018C9F8 File Offset: 0x0018ABF8
		private void FinalizeRotation(Quaternion nextRotation)
		{
			this.simulatedRotation = nextRotation;
			if (this.updateRotation)
			{
				if (this.rigid != null)
				{
					this.rigid.MoveRotation(nextRotation);
					return;
				}
				if (this.rigid2D != null)
				{
					this.rigid2D.MoveRotation(nextRotation.eulerAngles.z);
					return;
				}
				this.tr.rotation = nextRotation;
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0018CA64 File Offset: 0x0018AC64
		private void FinalizePosition(Vector3 nextPosition)
		{
			Vector3 vector = this.simulatedPosition;
			bool flag = false;
			if (this.controller != null && this.controller.enabled && this.updatePosition)
			{
				this.tr.position = vector;
				this.controller.Move(nextPosition - vector + this.accumulatedMovementDelta);
				vector = this.tr.position;
				if (this.controller.isGrounded)
				{
					this.verticalVelocity = 0f;
				}
			}
			else
			{
				float lastElevation;
				this.movementPlane.ToPlane(vector, out lastElevation);
				vector = nextPosition + this.accumulatedMovementDelta;
				if (this.usingGravity)
				{
					vector = this.RaycastPosition(vector, lastElevation);
				}
				flag = true;
			}
			bool flag2 = false;
			vector = this.ClampToNavmesh(vector, out flag2);
			if ((flag || flag2) && this.updatePosition)
			{
				if (this.rigid != null)
				{
					this.rigid.MovePosition(vector);
				}
				else if (this.rigid2D != null)
				{
					this.rigid2D.MovePosition(vector);
				}
				else
				{
					this.tr.position = vector;
				}
			}
			this.accumulatedMovementDelta = Vector3.zero;
			this.simulatedPosition = vector;
			this.UpdateVelocity();
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0018CB98 File Offset: 0x0018AD98
		protected void UpdateVelocity()
		{
			int frameCount = Time.frameCount;
			if (frameCount != this.prevFrame)
			{
				this.prevPosition2 = this.prevPosition1;
			}
			this.prevPosition1 = this.position;
			this.prevFrame = frameCount;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0018CBD3 File Offset: 0x0018ADD3
		protected virtual Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			positionChanged = false;
			return position;
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0018CBDC File Offset: 0x0018ADDC
		protected Vector3 RaycastPosition(Vector3 position, float lastElevation)
		{
			float num;
			this.movementPlane.ToPlane(position, out num);
			float num2 = this.centerOffset + Mathf.Max(0f, lastElevation - num);
			Vector3 vector = this.movementPlane.ToWorld(Vector2.zero, num2);
			RaycastHit raycastHit;
			if (Physics.Raycast(position + vector, -vector, out raycastHit, num2, this.groundMask, QueryTriggerInteraction.Ignore))
			{
				this.verticalVelocity *= Math.Max(0f, 1f - 5f * this.lastDeltaTime);
				return raycastHit.point;
			}
			return position;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0018CC74 File Offset: 0x0018AE74
		protected virtual void OnDrawGizmosSelected()
		{
			if (Application.isPlaying)
			{
				this.FindComponents();
			}
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0018CC84 File Offset: 0x0018AE84
		protected virtual void OnDrawGizmos()
		{
			if (!Application.isPlaying || !base.enabled)
			{
				this.FindComponents();
			}
			if (!(this.gravity == Vector3.zero) && (!this.updatePosition || ((this.rigid == null || this.rigid.isKinematic) && (this.rigid2D == null || this.rigid2D.isKinematic))) && (this.controller == null || !this.controller.enabled))
			{
				Gizmos.color = AIBase.GizmoColorRaycast;
				Gizmos.DrawLine(this.position, this.position + base.transform.up * this.centerOffset);
				Gizmos.DrawLine(this.position - base.transform.right * 0.1f, this.position + base.transform.right * 0.1f);
				Gizmos.DrawLine(this.position - base.transform.forward * 0.1f, this.position + base.transform.forward * 0.1f);
			}
			if (!float.IsPositiveInfinity(this.destination.x) && Application.isPlaying)
			{
				Draw.Gizmos.CircleXZ(this.destination, 0.2f, Color.blue, 0f, 6.28318548f);
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0018CE20 File Offset: 0x0018B020
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (unityThread && this.targetCompatibility != null)
			{
				this.target = this.targetCompatibility;
			}
			return 1;
		}

		// Token: 0x04003EF6 RID: 16118
		public float repathRate = 0.5f;

		// Token: 0x04003EF7 RID: 16119
		[FormerlySerializedAs("repeatedlySearchPaths")]
		public bool canSearch = true;

		// Token: 0x04003EF8 RID: 16120
		public bool canMove = true;

		// Token: 0x04003EF9 RID: 16121
		[FormerlySerializedAs("speed")]
		public float maxSpeed = 1f;

		// Token: 0x04003EFA RID: 16122
		public Vector3 gravity = new Vector3(float.NaN, float.NaN, float.NaN);

		// Token: 0x04003EFB RID: 16123
		public LayerMask groundMask = -1;

		// Token: 0x04003EFC RID: 16124
		public float centerOffset = 1f;

		// Token: 0x04003EFD RID: 16125
		public bool rotationIn2D;

		// Token: 0x04003EFE RID: 16126
		protected Vector3 simulatedPosition;

		// Token: 0x04003EFF RID: 16127
		protected Quaternion simulatedRotation;

		// Token: 0x04003F00 RID: 16128
		private Vector3 accumulatedMovementDelta = Vector3.zero;

		// Token: 0x04003F01 RID: 16129
		protected Vector2 velocity2D;

		// Token: 0x04003F02 RID: 16130
		protected float verticalVelocity;

		// Token: 0x04003F03 RID: 16131
		public Seeker seeker;

		// Token: 0x04003F04 RID: 16132
		public Transform tr;

		// Token: 0x04003F05 RID: 16133
		protected Rigidbody rigid;

		// Token: 0x04003F06 RID: 16134
		protected Rigidbody2D rigid2D;

		// Token: 0x04003F07 RID: 16135
		protected CharacterController controller;

		// Token: 0x04003F08 RID: 16136
		protected RVOController rvoController;

		// Token: 0x04003F09 RID: 16137
		public IMovementPlane movementPlane = GraphTransform.identityTransform;

		// Token: 0x04003F0A RID: 16138
		[NonSerialized]
		public bool updatePosition = true;

		// Token: 0x04003F0B RID: 16139
		[NonSerialized]
		public bool updateRotation = true;

		// Token: 0x04003F0D RID: 16141
		protected float lastDeltaTime;

		// Token: 0x04003F0E RID: 16142
		protected int prevFrame;

		// Token: 0x04003F0F RID: 16143
		protected Vector3 prevPosition1;

		// Token: 0x04003F10 RID: 16144
		protected Vector3 prevPosition2;

		// Token: 0x04003F11 RID: 16145
		protected Vector2 lastDeltaPosition;

		// Token: 0x04003F12 RID: 16146
		protected bool waitingForPathCalculation;

		// Token: 0x04003F13 RID: 16147
		protected float lastRepath = float.NegativeInfinity;

		// Token: 0x04003F14 RID: 16148
		[FormerlySerializedAs("target")]
		[SerializeField]
		[HideInInspector]
		private Transform targetCompatibility;

		// Token: 0x04003F15 RID: 16149
		private bool startHasRun;

		// Token: 0x04003F19 RID: 16153
		protected static readonly Color GizmoColorRaycast = new Color(0.4627451f, 0.807843149f, 0.4392157f);
	}
}
