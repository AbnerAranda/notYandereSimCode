using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000551 RID: 1361
	public abstract class Path : IPathInternals
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x0019A9DA File Offset: 0x00198BDA
		// (set) Token: 0x060023DD RID: 9181 RVA: 0x0019A9E2 File Offset: 0x00198BE2
		internal PathState PipelineState { get; private set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x0019A9EB File Offset: 0x00198BEB
		// (set) Token: 0x060023DF RID: 9183 RVA: 0x0019A9F4 File Offset: 0x00198BF4
		public PathCompleteState CompleteState
		{
			get
			{
				return this.completeState;
			}
			protected set
			{
				object obj = this.stateLock;
				lock (obj)
				{
					if (this.completeState != PathCompleteState.Error)
					{
						this.completeState = value;
					}
				}
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x0019AA40 File Offset: 0x00198C40
		public bool error
		{
			get
			{
				return this.CompleteState == PathCompleteState.Error;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x0019AA4B File Offset: 0x00198C4B
		// (set) Token: 0x060023E2 RID: 9186 RVA: 0x0019AA53 File Offset: 0x00198C53
		public string errorLog { get; private set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x0019AA5C File Offset: 0x00198C5C
		// (set) Token: 0x060023E4 RID: 9188 RVA: 0x0019AA64 File Offset: 0x00198C64
		bool IPathInternals.Pooled { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0002D199 File Offset: 0x0002B399
		[Obsolete("Has been renamed to 'Pooled' to use more widely underestood terminology", true)]
		internal bool recycled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x0019AA6D File Offset: 0x00198C6D
		// (set) Token: 0x060023E7 RID: 9191 RVA: 0x0019AA75 File Offset: 0x00198C75
		internal ushort pathID { get; private set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x0019AA7E File Offset: 0x00198C7E
		// (set) Token: 0x060023E9 RID: 9193 RVA: 0x0019AA86 File Offset: 0x00198C86
		public int[] tagPenalties
		{
			get
			{
				return this.manualTagPenalties;
			}
			set
			{
				if (value == null || value.Length != 32)
				{
					this.manualTagPenalties = null;
					this.internalTagPenalties = Path.ZeroTagPenalties;
					return;
				}
				this.manualTagPenalties = value;
				this.internalTagPenalties = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x0002D199 File Offset: 0x0002B399
		internal virtual bool FloodingPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x0019AAB4 File Offset: 0x00198CB4
		public float GetTotalLength()
		{
			if (this.vectorPath == null)
			{
				return float.PositiveInfinity;
			}
			float num = 0f;
			for (int i = 0; i < this.vectorPath.Count - 1; i++)
			{
				num += Vector3.Distance(this.vectorPath[i], this.vectorPath[i + 1]);
			}
			return num;
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0019AB10 File Offset: 0x00198D10
		public IEnumerator WaitForPath()
		{
			if (this.PipelineState == PathState.Created)
			{
				throw new InvalidOperationException("This path has not been started yet");
			}
			while (this.PipelineState != PathState.Returned)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public void BlockUntilCalculated()
		{
			AstarPath.BlockUntilCalculated(this);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x0019AB20 File Offset: 0x00198D20
		internal uint CalculateHScore(GraphNode node)
		{
			switch (this.heuristic)
			{
			case Heuristic.Manhattan:
			{
				Int3 position = node.position;
				uint num = (uint)((float)(Math.Abs(this.hTarget.x - position.x) + Math.Abs(this.hTarget.y - position.y) + Math.Abs(this.hTarget.z - position.z)) * this.heuristicScale);
				if (this.hTargetNode != null)
				{
					num = Math.Max(num, AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				}
				return num;
			}
			case Heuristic.DiagonalManhattan:
			{
				Int3 @int = this.GetHTarget() - node.position;
				@int.x = Math.Abs(@int.x);
				@int.y = Math.Abs(@int.y);
				@int.z = Math.Abs(@int.z);
				int num2 = Math.Min(@int.x, @int.z);
				int num3 = Math.Max(@int.x, @int.z);
				uint num = (uint)((float)(14 * num2 / 10 + (num3 - num2) + @int.y) * this.heuristicScale);
				if (this.hTargetNode != null)
				{
					num = Math.Max(num, AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				}
				return num;
			}
			case Heuristic.Euclidean:
			{
				uint num = (uint)((float)(this.GetHTarget() - node.position).costMagnitude * this.heuristicScale);
				if (this.hTargetNode != null)
				{
					num = Math.Max(num, AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				}
				return num;
			}
			default:
				return 0U;
			}
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x0019ACE8 File Offset: 0x00198EE8
		internal uint GetTagPenalty(int tag)
		{
			return (uint)this.internalTagPenalties[tag];
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x0019ACF2 File Offset: 0x00198EF2
		internal Int3 GetHTarget()
		{
			return this.hTarget;
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x0019ACFA File Offset: 0x00198EFA
		internal bool CanTraverse(GraphNode node)
		{
			if (this.traversalProvider != null)
			{
				return this.traversalProvider.CanTraverse(this, node);
			}
			return node.Walkable && (this.enabledTags >> (int)node.Tag & 1) != 0;
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0019AD31 File Offset: 0x00198F31
		internal uint GetTraversalCost(GraphNode node)
		{
			if (this.traversalProvider != null)
			{
				return this.traversalProvider.GetTraversalCost(this, node);
			}
			return this.GetTagPenalty((int)node.Tag) + node.Penalty;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x0019AD5C File Offset: 0x00198F5C
		internal virtual uint GetConnectionSpecialCost(GraphNode a, GraphNode b, uint currentCost)
		{
			return currentCost;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0019AD5F File Offset: 0x00198F5F
		public bool IsDone()
		{
			return this.CompleteState > PathCompleteState.NotCalculated;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0019AD6C File Offset: 0x00198F6C
		void IPathInternals.AdvanceState(PathState s)
		{
			object obj = this.stateLock;
			lock (obj)
			{
				this.PipelineState = (PathState)Math.Max((int)this.PipelineState, (int)s);
			}
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x0019ADB8 File Offset: 0x00198FB8
		[Obsolete("Use the 'PipelineState' property instead")]
		public PathState GetState()
		{
			return this.PipelineState;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x0019ADC0 File Offset: 0x00198FC0
		internal void FailWithError(string msg)
		{
			this.Error();
			if (this.errorLog != "")
			{
				this.errorLog = this.errorLog + "\n" + msg;
				return;
			}
			this.errorLog = msg;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x0019ADF9 File Offset: 0x00198FF9
		[Obsolete("Use FailWithError instead")]
		internal void LogError(string msg)
		{
			this.Log(msg);
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x0019AE02 File Offset: 0x00199002
		[Obsolete("Use FailWithError instead")]
		internal void Log(string msg)
		{
			this.errorLog += msg;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x0019AE16 File Offset: 0x00199016
		public void Error()
		{
			this.CompleteState = PathCompleteState.Error;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x0019AE20 File Offset: 0x00199020
		private void ErrorCheck()
		{
			if (!this.hasBeenReset)
			{
				this.FailWithError("Please use the static Construct function for creating paths, do not use the normal constructors.");
			}
			if (((IPathInternals)this).Pooled)
			{
				this.FailWithError("The path is currently in a path pool. Are you sending the path for calculation twice?");
			}
			if (this.pathHandler == null)
			{
				this.FailWithError("Field pathHandler is not set. Please report this bug.");
			}
			if (this.PipelineState > PathState.Processing)
			{
				this.FailWithError("This path has already been processed. Do not request a path with the same path object twice.");
			}
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0019AE7A File Offset: 0x0019907A
		protected virtual void OnEnterPool()
		{
			if (this.vectorPath != null)
			{
				ListPool<Vector3>.Release(ref this.vectorPath);
			}
			if (this.path != null)
			{
				ListPool<GraphNode>.Release(ref this.path);
			}
			this.callback = null;
			this.immediateCallback = null;
			this.traversalProvider = null;
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x0019AEB8 File Offset: 0x001990B8
		protected virtual void Reset()
		{
			if (AstarPath.active == null)
			{
				throw new NullReferenceException("No AstarPath object found in the scene. Make sure there is one or do not create paths in Awake");
			}
			this.hasBeenReset = true;
			this.PipelineState = PathState.Created;
			this.releasedNotSilent = false;
			this.pathHandler = null;
			this.callback = null;
			this.immediateCallback = null;
			this.errorLog = "";
			this.completeState = PathCompleteState.NotCalculated;
			this.path = ListPool<GraphNode>.Claim();
			this.vectorPath = ListPool<Vector3>.Claim();
			this.currentR = null;
			this.duration = 0f;
			this.searchedNodes = 0;
			this.nnConstraint = PathNNConstraint.Default;
			this.next = null;
			this.heuristic = AstarPath.active.heuristic;
			this.heuristicScale = AstarPath.active.heuristicScale;
			this.enabledTags = -1;
			this.tagPenalties = null;
			this.pathID = AstarPath.active.GetNextPathID();
			this.hTarget = Int3.zero;
			this.hTargetNode = null;
			this.traversalProvider = null;
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x0019AFAC File Offset: 0x001991AC
		public void Claim(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (this.claimed[i] == o)
				{
					throw new ArgumentException("You have already claimed the path with that object (" + o + "). Are you claiming the path with the same object twice?");
				}
			}
			this.claimed.Add(o);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x0019B00E File Offset: 0x0019920E
		[Obsolete("Use Release(o, true) instead")]
		internal void ReleaseSilent(object o)
		{
			this.Release(o, true);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0019B018 File Offset: 0x00199218
		public void Release(object o, bool silent = false)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (this.claimed[i] == o)
				{
					this.claimed.RemoveAt(i);
					if (!silent)
					{
						this.releasedNotSilent = true;
					}
					if (this.claimed.Count == 0 && this.releasedNotSilent)
					{
						PathPool.Pool(this);
					}
					return;
				}
			}
			if (this.claimed.Count == 0)
			{
				throw new ArgumentException("You are releasing a path which is not claimed at all (most likely it has been pooled already). Are you releasing the path with the same object (" + o + ") twice?\nCheck out the documentation on path pooling for help.");
			}
			throw new ArgumentException("You are releasing a path which has not been claimed with this object (" + o + "). Are you releasing the path with the same object twice?\nCheck out the documentation on path pooling for help.");
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x0019B0C4 File Offset: 0x001992C4
		protected virtual void Trace(PathNode from)
		{
			PathNode pathNode = from;
			int num = 0;
			while (pathNode != null)
			{
				pathNode = pathNode.parent;
				num++;
				if (num > 2048)
				{
					Debug.LogWarning("Infinite loop? >2048 node path. Remove this message if you really have that long paths (Path.cs, Trace method)");
					break;
				}
			}
			if (this.path.Capacity < num)
			{
				this.path.Capacity = num;
			}
			if (this.vectorPath.Capacity < num)
			{
				this.vectorPath.Capacity = num;
			}
			pathNode = from;
			for (int i = 0; i < num; i++)
			{
				this.path.Add(pathNode.node);
				pathNode = pathNode.parent;
			}
			int num2 = num / 2;
			for (int j = 0; j < num2; j++)
			{
				GraphNode value = this.path[j];
				this.path[j] = this.path[num - j - 1];
				this.path[num - j - 1] = value;
			}
			for (int k = 0; k < num; k++)
			{
				this.vectorPath.Add((Vector3)this.path[k].position);
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x0019B1DC File Offset: 0x001993DC
		protected void DebugStringPrefix(PathLog logMode, StringBuilder text)
		{
			text.Append(this.error ? "Path Failed : " : "Path Completed : ");
			text.Append("Computation Time ");
			text.Append(this.duration.ToString((logMode == PathLog.Heavy) ? "0.000 ms " : "0.00 ms "));
			text.Append("Searched Nodes ").Append(this.searchedNodes);
			if (!this.error)
			{
				text.Append(" Path Length ");
				text.Append((this.path == null) ? "Null" : this.path.Count.ToString());
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0019B288 File Offset: 0x00199488
		protected void DebugStringSuffix(PathLog logMode, StringBuilder text)
		{
			if (this.error)
			{
				text.Append("\nError: ").Append(this.errorLog);
			}
			if (logMode == PathLog.Heavy && !AstarPath.active.IsUsingMultithreading)
			{
				text.Append("\nCallback references ");
				if (this.callback != null)
				{
					text.Append(this.callback.Target.GetType().FullName).AppendLine();
				}
				else
				{
					text.AppendLine("NULL");
				}
			}
			text.Append("\nPath Number ").Append(this.pathID).Append(" (unique id)");
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x0019B328 File Offset: 0x00199528
		internal virtual string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!this.error && logMode == PathLog.OnlyErrors))
			{
				return "";
			}
			StringBuilder debugStringBuilder = this.pathHandler.DebugStringBuilder;
			debugStringBuilder.Length = 0;
			this.DebugStringPrefix(logMode, debugStringBuilder);
			this.DebugStringSuffix(logMode, debugStringBuilder);
			return debugStringBuilder.ToString();
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x0019B373 File Offset: 0x00199573
		protected virtual void ReturnPath()
		{
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0019B38C File Offset: 0x0019958C
		protected void PrepareBase(PathHandler pathHandler)
		{
			if (pathHandler.PathID > this.pathID)
			{
				pathHandler.ClearPathIDs();
			}
			this.pathHandler = pathHandler;
			pathHandler.InitializeForPath(this);
			if (this.internalTagPenalties == null || this.internalTagPenalties.Length != 32)
			{
				this.internalTagPenalties = Path.ZeroTagPenalties;
			}
			try
			{
				this.ErrorCheck();
			}
			catch (Exception ex)
			{
				this.FailWithError(ex.Message);
			}
		}

		// Token: 0x06002407 RID: 9223
		protected abstract void Prepare();

		// Token: 0x06002408 RID: 9224 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void Cleanup()
		{
		}

		// Token: 0x06002409 RID: 9225
		protected abstract void Initialize();

		// Token: 0x0600240A RID: 9226
		protected abstract void CalculateStep(long targetTick);

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x0019B404 File Offset: 0x00199604
		PathHandler IPathInternals.PathHandler
		{
			get
			{
				return this.pathHandler;
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0019B40C File Offset: 0x0019960C
		void IPathInternals.OnEnterPool()
		{
			this.OnEnterPool();
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0019B414 File Offset: 0x00199614
		void IPathInternals.Reset()
		{
			this.Reset();
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x0019B41C File Offset: 0x0019961C
		void IPathInternals.ReturnPath()
		{
			this.ReturnPath();
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x0019B424 File Offset: 0x00199624
		void IPathInternals.PrepareBase(PathHandler handler)
		{
			this.PrepareBase(handler);
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x0019B42D File Offset: 0x0019962D
		void IPathInternals.Prepare()
		{
			this.Prepare();
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x0019B435 File Offset: 0x00199635
		void IPathInternals.Cleanup()
		{
			this.Cleanup();
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x0019B43D File Offset: 0x0019963D
		void IPathInternals.Initialize()
		{
			this.Initialize();
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0019B445 File Offset: 0x00199645
		void IPathInternals.CalculateStep(long targetTick)
		{
			this.CalculateStep(targetTick);
		}

		// Token: 0x04004050 RID: 16464
		protected PathHandler pathHandler;

		// Token: 0x04004051 RID: 16465
		public OnPathDelegate callback;

		// Token: 0x04004052 RID: 16466
		public OnPathDelegate immediateCallback;

		// Token: 0x04004054 RID: 16468
		private object stateLock = new object();

		// Token: 0x04004055 RID: 16469
		public ITraversalProvider traversalProvider;

		// Token: 0x04004056 RID: 16470
		protected PathCompleteState completeState;

		// Token: 0x04004058 RID: 16472
		public List<GraphNode> path;

		// Token: 0x04004059 RID: 16473
		public List<Vector3> vectorPath;

		// Token: 0x0400405A RID: 16474
		protected PathNode currentR;

		// Token: 0x0400405B RID: 16475
		internal float duration;

		// Token: 0x0400405C RID: 16476
		internal int searchedNodes;

		// Token: 0x0400405E RID: 16478
		protected bool hasBeenReset;

		// Token: 0x0400405F RID: 16479
		public NNConstraint nnConstraint = PathNNConstraint.Default;

		// Token: 0x04004060 RID: 16480
		internal Path next;

		// Token: 0x04004061 RID: 16481
		public Heuristic heuristic;

		// Token: 0x04004062 RID: 16482
		public float heuristicScale = 1f;

		// Token: 0x04004064 RID: 16484
		protected GraphNode hTargetNode;

		// Token: 0x04004065 RID: 16485
		protected Int3 hTarget;

		// Token: 0x04004066 RID: 16486
		public int enabledTags = -1;

		// Token: 0x04004067 RID: 16487
		private static readonly int[] ZeroTagPenalties = new int[32];

		// Token: 0x04004068 RID: 16488
		protected int[] internalTagPenalties;

		// Token: 0x04004069 RID: 16489
		protected int[] manualTagPenalties;

		// Token: 0x0400406A RID: 16490
		private List<object> claimed = new List<object>();

		// Token: 0x0400406B RID: 16491
		private bool releasedNotSilent;
	}
}
