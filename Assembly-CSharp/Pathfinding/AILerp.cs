using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000527 RID: 1319
	[RequireComponent(typeof(Seeker))]
	[AddComponentMenu("Pathfinding/AI/AILerp (2D,3D)")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_lerp.php")]
	public class AILerp : VersionedMonoBehaviour, IAstarAI
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0018CE5B File Offset: 0x0018B05B
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x0018CE63 File Offset: 0x0018B063
		public bool reachedEndOfPath { get; private set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x0018CE6C File Offset: 0x0018B06C
		// (set) Token: 0x060020FC RID: 8444 RVA: 0x0018CE74 File Offset: 0x0018B074
		public Vector3 destination { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x0018CE80 File Offset: 0x0018B080
		// (set) Token: 0x060020FE RID: 8446 RVA: 0x0018CEA8 File Offset: 0x0018B0A8
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

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x0018CF0A File Offset: 0x0018B10A
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

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x0018CF26 File Offset: 0x0018B126
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

		// Token: 0x06002101 RID: 8449 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IAstarAI.Move(Vector3 deltaPosition)
		{
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x0018CF42 File Offset: 0x0018B142
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x0018CF4A File Offset: 0x0018B14A
		float IAstarAI.maxSpeed
		{
			get
			{
				return this.speed;
			}
			set
			{
				this.speed = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x0018CF53 File Offset: 0x0018B153
		// (set) Token: 0x06002105 RID: 8453 RVA: 0x0018CF5B File Offset: 0x0018B15B
		bool IAstarAI.canSearch
		{
			get
			{
				return this.canSearch;
			}
			set
			{
				this.canSearch = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x0018CF64 File Offset: 0x0018B164
		// (set) Token: 0x06002107 RID: 8455 RVA: 0x0018CF6C File Offset: 0x0018B16C
		bool IAstarAI.canMove
		{
			get
			{
				return this.canMove;
			}
			set
			{
				this.canMove = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x0018CF75 File Offset: 0x0018B175
		Vector3 IAstarAI.velocity
		{
			get
			{
				if (Time.deltaTime <= 1E-05f)
				{
					return Vector3.zero;
				}
				return (this.previousPosition1 - this.previousPosition2) / Time.deltaTime;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x0018CFA4 File Offset: 0x0018B1A4
		Vector3 IAstarAI.desiredVelocity
		{
			get
			{
				return ((IAstarAI)this).velocity;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x0018CFAC File Offset: 0x0018B1AC
		Vector3 IAstarAI.steeringTarget
		{
			get
			{
				if (!this.interpolator.valid)
				{
					return this.simulatedPosition;
				}
				return this.interpolator.position + this.interpolator.tangent;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x0018CFDD File Offset: 0x0018B1DD
		// (set) Token: 0x0600210C RID: 8460 RVA: 0x0018CFF4 File Offset: 0x0018B1F4
		public float remainingDistance
		{
			get
			{
				return Mathf.Max(this.interpolator.remainingDistance, 0f);
			}
			set
			{
				this.interpolator.remainingDistance = Mathf.Max(value, 0f);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x0018D00C File Offset: 0x0018B20C
		public bool hasPath
		{
			get
			{
				return this.interpolator.valid;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x0018D019 File Offset: 0x0018B219
		public bool pathPending
		{
			get
			{
				return !this.canSearchAgain;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x0018D024 File Offset: 0x0018B224
		// (set) Token: 0x06002110 RID: 8464 RVA: 0x0018D02C File Offset: 0x0018B22C
		public bool isStopped { get; set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x0018D035 File Offset: 0x0018B235
		// (set) Token: 0x06002112 RID: 8466 RVA: 0x0018D03D File Offset: 0x0018B23D
		public Action onSearchPath { get; set; }

		// Token: 0x06002113 RID: 8467 RVA: 0x0018D048 File Offset: 0x0018B248
		protected AILerp()
		{
			this.destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0018D0E8 File Offset: 0x0018B2E8
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
			this.seeker = base.GetComponent<Seeker>();
			this.seeker.startEndModifier.adjustStartPoint = (() => this.simulatedPosition);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0018D124 File Offset: 0x0018B324
		protected virtual void Start()
		{
			this.startHasRun = true;
			this.Init();
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0018D133 File Offset: 0x0018B333
		protected virtual void OnEnable()
		{
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
			this.Init();
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0018D163 File Offset: 0x0018B363
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

		// Token: 0x06002118 RID: 8472 RVA: 0x0018D194 File Offset: 0x0018B394
		public void OnDisable()
		{
			if (this.seeker != null)
			{
				this.seeker.CancelCurrentPathRequest(true);
			}
			this.canSearchAgain = true;
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = null;
			this.interpolator.SetPath(null);
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Remove(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0018D214 File Offset: 0x0018B414
		public void Teleport(Vector3 position, bool clearPath = true)
		{
			if (clearPath)
			{
				this.interpolator.SetPath(null);
			}
			this.previousPosition2 = position;
			this.previousPosition1 = position;
			this.simulatedPosition = position;
			if (this.updatePosition)
			{
				this.tr.position = position;
			}
			this.reachedEndOfPath = false;
			if (clearPath)
			{
				this.SearchPath();
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x0018D26D File Offset: 0x0018B46D
		protected virtual bool shouldRecalculatePath
		{
			get
			{
				return Time.time - this.lastRepath >= this.repathRate && this.canSearchAgain && this.canSearch && !float.IsPositiveInfinity(this.destination.x);
			}
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0018D2A8 File Offset: 0x0018B4A8
		[Obsolete("Use SearchPath instead")]
		public virtual void ForceSearchPath()
		{
			this.SearchPath();
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0018D2B0 File Offset: 0x0018B4B0
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
			Vector3 feetPosition = this.GetFeetPosition();
			this.canSearchAgain = false;
			this.seeker.StartPath(feetPosition, this.destination);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnTargetReached()
		{
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0018D310 File Offset: 0x0018B510
		protected virtual void OnPathComplete(Path _p)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null)
			{
				throw new Exception("This function only handles ABPaths, do not use special path types");
			}
			this.canSearchAgain = true;
			abpath.Claim(this);
			if (abpath.error)
			{
				abpath.Release(this, false);
				return;
			}
			if (this.interpolatePathSwitches)
			{
				this.ConfigurePathSwitchInterpolation();
			}
			ABPath abpath2 = this.path;
			this.path = abpath;
			this.reachedEndOfPath = false;
			if (this.path.vectorPath != null && this.path.vectorPath.Count == 1)
			{
				this.path.vectorPath.Insert(0, this.GetFeetPosition());
			}
			this.ConfigureNewPath();
			if (abpath2 != null)
			{
				abpath2.Release(this, false);
			}
			if (this.interpolator.remainingDistance < 0.0001f && !this.reachedEndOfPath)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0018D3E4 File Offset: 0x0018B5E4
		public void SetPath(Path path)
		{
			if (path.PipelineState == PathState.Created)
			{
				this.lastRepath = Time.time;
				this.canSearchAgain = false;
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

		// Token: 0x06002120 RID: 8480 RVA: 0x0018D468 File Offset: 0x0018B668
		protected virtual void ConfigurePathSwitchInterpolation()
		{
			bool flag = this.interpolator.valid && this.interpolator.remainingDistance < 0.0001f;
			if (this.interpolator.valid && !flag)
			{
				this.previousMovementOrigin = this.interpolator.position;
				this.previousMovementDirection = this.interpolator.tangent.normalized * this.interpolator.remainingDistance;
				this.pathSwitchInterpolationTime = 0f;
				return;
			}
			this.previousMovementOrigin = Vector3.zero;
			this.previousMovementDirection = Vector3.zero;
			this.pathSwitchInterpolationTime = float.PositiveInfinity;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0018D50F File Offset: 0x0018B70F
		public virtual Vector3 GetFeetPosition()
		{
			return this.position;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0018D518 File Offset: 0x0018B718
		protected virtual void ConfigureNewPath()
		{
			bool valid = this.interpolator.valid;
			Vector3 vector = valid ? this.interpolator.tangent : Vector3.zero;
			this.interpolator.SetPath(this.path.vectorPath);
			this.interpolator.MoveToClosestPoint(this.GetFeetPosition());
			if (this.interpolatePathSwitches && this.switchPathInterpolationSpeed > 0.01f && valid)
			{
				float num = Mathf.Max(-Vector3.Dot(vector.normalized, this.interpolator.tangent.normalized), 0f);
				this.interpolator.distance -= this.speed * num * (1f / this.switchPathInterpolationSpeed);
			}
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0018D5DC File Offset: 0x0018B7DC
		protected virtual void Update()
		{
			if (this.shouldRecalculatePath)
			{
				this.SearchPath();
			}
			if (this.canMove)
			{
				Vector3 nextPosition;
				Quaternion nextRotation;
				this.MovementUpdate(Time.deltaTime, out nextPosition, out nextRotation);
				this.FinalizeMovement(nextPosition, nextRotation);
			}
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0018D618 File Offset: 0x0018B818
		public void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			Vector3 direction;
			nextPosition = this.CalculateNextPosition(out direction, this.isStopped ? 0f : deltaTime);
			if (this.enableRotation)
			{
				nextRotation = this.SimulateRotationTowards(direction, deltaTime);
				return;
			}
			nextRotation = this.simulatedRotation;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0018D698 File Offset: 0x0018B898
		public void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation)
		{
			this.previousPosition2 = this.previousPosition1;
			this.simulatedPosition = nextPosition;
			this.previousPosition1 = nextPosition;
			this.simulatedRotation = nextRotation;
			if (this.updatePosition)
			{
				this.tr.position = nextPosition;
			}
			if (this.updateRotation)
			{
				this.tr.rotation = nextRotation;
			}
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0018D6F0 File Offset: 0x0018B8F0
		private Quaternion SimulateRotationTowards(Vector3 direction, float deltaTime)
		{
			if (direction != Vector3.zero)
			{
				Quaternion quaternion = Quaternion.LookRotation(direction, this.rotationIn2D ? Vector3.back : Vector3.up);
				if (this.rotationIn2D)
				{
					quaternion *= Quaternion.Euler(90f, 0f, 0f);
				}
				return Quaternion.Slerp(this.simulatedRotation, quaternion, deltaTime * this.rotationSpeed);
			}
			return this.simulatedRotation;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0018D764 File Offset: 0x0018B964
		protected virtual Vector3 CalculateNextPosition(out Vector3 direction, float deltaTime)
		{
			if (!this.interpolator.valid)
			{
				direction = Vector3.zero;
				return this.simulatedPosition;
			}
			this.interpolator.distance += deltaTime * this.speed;
			if (this.interpolator.remainingDistance < 0.0001f && !this.reachedEndOfPath)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
			direction = this.interpolator.tangent;
			this.pathSwitchInterpolationTime += deltaTime;
			float num = this.switchPathInterpolationSpeed * this.pathSwitchInterpolationTime;
			if (this.interpolatePathSwitches && num < 1f)
			{
				return Vector3.Lerp(this.previousMovementOrigin + Vector3.ClampMagnitude(this.previousMovementDirection, this.speed * this.pathSwitchInterpolationTime), this.interpolator.position, num);
			}
			return this.interpolator.position;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0018D84E File Offset: 0x0018BA4E
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (unityThread && this.targetCompatibility != null)
			{
				this.target = this.targetCompatibility;
			}
			return 2;
		}

		// Token: 0x04003F1A RID: 16154
		public float repathRate = 0.5f;

		// Token: 0x04003F1B RID: 16155
		public bool canSearch = true;

		// Token: 0x04003F1C RID: 16156
		public bool canMove = true;

		// Token: 0x04003F1D RID: 16157
		public float speed = 3f;

		// Token: 0x04003F1E RID: 16158
		public bool enableRotation = true;

		// Token: 0x04003F1F RID: 16159
		public bool rotationIn2D;

		// Token: 0x04003F20 RID: 16160
		public float rotationSpeed = 10f;

		// Token: 0x04003F21 RID: 16161
		public bool interpolatePathSwitches = true;

		// Token: 0x04003F22 RID: 16162
		public float switchPathInterpolationSpeed = 5f;

		// Token: 0x04003F25 RID: 16165
		[NonSerialized]
		public bool updatePosition = true;

		// Token: 0x04003F26 RID: 16166
		[NonSerialized]
		public bool updateRotation = true;

		// Token: 0x04003F29 RID: 16169
		protected Seeker seeker;

		// Token: 0x04003F2A RID: 16170
		protected Transform tr;

		// Token: 0x04003F2B RID: 16171
		protected float lastRepath = -9999f;

		// Token: 0x04003F2C RID: 16172
		protected ABPath path;

		// Token: 0x04003F2D RID: 16173
		protected bool canSearchAgain = true;

		// Token: 0x04003F2E RID: 16174
		protected Vector3 previousMovementOrigin;

		// Token: 0x04003F2F RID: 16175
		protected Vector3 previousMovementDirection;

		// Token: 0x04003F30 RID: 16176
		protected float pathSwitchInterpolationTime;

		// Token: 0x04003F31 RID: 16177
		protected PathInterpolator interpolator = new PathInterpolator();

		// Token: 0x04003F32 RID: 16178
		private bool startHasRun;

		// Token: 0x04003F33 RID: 16179
		private Vector3 previousPosition1;

		// Token: 0x04003F34 RID: 16180
		private Vector3 previousPosition2;

		// Token: 0x04003F35 RID: 16181
		private Vector3 simulatedPosition;

		// Token: 0x04003F36 RID: 16182
		private Quaternion simulatedRotation;

		// Token: 0x04003F37 RID: 16183
		[FormerlySerializedAs("target")]
		[SerializeField]
		[HideInInspector]
		private Transform targetCompatibility;
	}
}
