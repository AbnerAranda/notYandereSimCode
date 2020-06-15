using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200052F RID: 1327
	[AddComponentMenu("Pathfinding/Seeker")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_seeker.php")]
	public class Seeker : VersionedMonoBehaviour
	{
		// Token: 0x060021B2 RID: 8626 RVA: 0x00190064 File Offset: 0x0018E264
		public Seeker()
		{
			this.onPathDelegate = new OnPathDelegate(this.OnPathComplete);
			this.onPartialPathDelegate = new OnPathDelegate(this.OnPartialPathComplete);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x001900D3 File Offset: 0x0018E2D3
		protected override void Awake()
		{
			base.Awake();
			this.startEndModifier.Awake(this);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x001900E7 File Offset: 0x0018E2E7
		public Path GetCurrentPath()
		{
			return this.path;
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x001900EF File Offset: 0x0018E2EF
		public void CancelCurrentPathRequest(bool pool = true)
		{
			if (!this.IsDone())
			{
				this.path.FailWithError("Canceled by script (Seeker.CancelCurrentPathRequest)");
				if (pool)
				{
					this.path.Claim(this.path);
					this.path.Release(this.path, false);
				}
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0019012F File Offset: 0x0018E32F
		public void OnDestroy()
		{
			this.ReleaseClaimedPath();
			this.startEndModifier.OnDestroy(this);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x00190143 File Offset: 0x0018E343
		public void ReleaseClaimedPath()
		{
			if (this.prevPath != null)
			{
				this.prevPath.Release(this, true);
				this.prevPath = null;
			}
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x00190161 File Offset: 0x0018E361
		public void RegisterModifier(IPathModifier modifier)
		{
			this.modifiers.Add(modifier);
			this.modifiers.Sort((IPathModifier a, IPathModifier b) => a.Order.CompareTo(b.Order));
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x00190199 File Offset: 0x0018E399
		public void DeregisterModifier(IPathModifier modifier)
		{
			this.modifiers.Remove(modifier);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x001901A8 File Offset: 0x0018E3A8
		public void PostProcess(Path path)
		{
			this.RunModifiers(Seeker.ModifierPass.PostProcess, path);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x001901B4 File Offset: 0x0018E3B4
		public void RunModifiers(Seeker.ModifierPass pass, Path path)
		{
			if (pass == Seeker.ModifierPass.PreProcess)
			{
				if (this.preProcessPath != null)
				{
					this.preProcessPath(path);
				}
				for (int i = 0; i < this.modifiers.Count; i++)
				{
					this.modifiers[i].PreProcess(path);
				}
				return;
			}
			if (pass == Seeker.ModifierPass.PostProcess)
			{
				if (this.postProcessPath != null)
				{
					this.postProcessPath(path);
				}
				for (int j = 0; j < this.modifiers.Count; j++)
				{
					this.modifiers[j].Apply(path);
				}
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x00190241 File Offset: 0x0018E441
		public bool IsDone()
		{
			return this.path == null || this.path.PipelineState >= PathState.Returned;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0019025E File Offset: 0x0018E45E
		private void OnPathComplete(Path path)
		{
			this.OnPathComplete(path, true, true);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0019026C File Offset: 0x0018E46C
		private void OnPathComplete(Path p, bool runModifiers, bool sendCallbacks)
		{
			if (p != null && p != this.path && sendCallbacks)
			{
				return;
			}
			if (this == null || p == null || p != this.path)
			{
				return;
			}
			if (!this.path.error && runModifiers)
			{
				this.RunModifiers(Seeker.ModifierPass.PostProcess, this.path);
			}
			if (sendCallbacks)
			{
				p.Claim(this);
				this.lastCompletedNodePath = p.path;
				this.lastCompletedVectorPath = p.vectorPath;
				if (this.tmpPathCallback != null)
				{
					this.tmpPathCallback(p);
				}
				if (this.pathCallback != null)
				{
					this.pathCallback(p);
				}
				if (this.prevPath != null)
				{
					this.prevPath.Release(this, true);
				}
				this.prevPath = p;
				if (!this.drawGizmos)
				{
					this.ReleaseClaimedPath();
				}
			}
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x00190339 File Offset: 0x0018E539
		private void OnPartialPathComplete(Path p)
		{
			this.OnPathComplete(p, true, false);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x00190344 File Offset: 0x0018E544
		private void OnMultiPathComplete(Path p)
		{
			this.OnPathComplete(p, false, true);
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0019034F File Offset: 0x0018E54F
		[Obsolete("Use ABPath.Construct(start, end, null) instead")]
		public ABPath GetNewPath(Vector3 start, Vector3 end)
		{
			return ABPath.Construct(start, end, null);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x00190359 File Offset: 0x0018E559
		public Path StartPath(Vector3 start, Vector3 end)
		{
			return this.StartPath(start, end, null);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x00190364 File Offset: 0x0018E564
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x00190375 File Offset: 0x0018E575
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback, int graphMask)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback, graphMask);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x00190388 File Offset: 0x0018E588
		public Path StartPath(Path p, OnPathDelegate callback = null)
		{
			if (p.nnConstraint.graphMask == -1)
			{
				p.nnConstraint.graphMask = this.graphMask;
			}
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x001903B2 File Offset: 0x0018E5B2
		public Path StartPath(Path p, OnPathDelegate callback, int graphMask)
		{
			p.nnConstraint.graphMask = graphMask;
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x001903CC File Offset: 0x0018E5CC
		private void StartPathInternal(Path p, OnPathDelegate callback)
		{
			MultiTargetPath multiTargetPath = p as MultiTargetPath;
			if (multiTargetPath != null)
			{
				OnPathDelegate[] array = new OnPathDelegate[multiTargetPath.targetPoints.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.onPartialPathDelegate;
				}
				multiTargetPath.callbacks = array;
				p.callback = (OnPathDelegate)Delegate.Combine(p.callback, new OnPathDelegate(this.OnMultiPathComplete));
			}
			else
			{
				p.callback = (OnPathDelegate)Delegate.Combine(p.callback, this.onPathDelegate);
			}
			p.enabledTags = this.traversableTags;
			p.tagPenalties = this.tagPenalties;
			if (this.path != null && this.path.PipelineState <= PathState.Processing && this.path.CompleteState != PathCompleteState.Error && this.lastPathID == (uint)this.path.pathID)
			{
				this.path.FailWithError("Canceled path because a new one was requested.\nThis happens when a new path is requested from the seeker when one was already being calculated.\nFor example if a unit got a new order, you might request a new path directly instead of waiting for the now invalid path to be calculated. Which is probably what you want.\nIf you are getting this a lot, you might want to consider how you are scheduling path requests.");
			}
			this.path = p;
			this.tmpPathCallback = callback;
			this.lastPathID = (uint)this.path.pathID;
			this.RunModifiers(Seeker.ModifierPass.PreProcess, this.path);
			AstarPath.StartPath(this.path, false);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x001904E8 File Offset: 0x0018E6E8
		public MultiTargetPath StartMultiTargetPath(Vector3 start, Vector3[] endPoints, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(start, endPoints, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x00190514 File Offset: 0x0018E714
		public MultiTargetPath StartMultiTargetPath(Vector3[] startPoints, Vector3 end, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(startPoints, end, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0019053F File Offset: 0x0018E73F
		[Obsolete("You can use StartPath instead of this method now. It will behave identically.")]
		public MultiTargetPath StartMultiTargetPath(MultiTargetPath p, OnPathDelegate callback = null, int graphMask = -1)
		{
			this.StartPath(p, callback, graphMask);
			return p;
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0019054C File Offset: 0x0018E74C
		public void OnDrawGizmos()
		{
			if (this.lastCompletedNodePath == null || !this.drawGizmos)
			{
				return;
			}
			if (this.detailedGizmos)
			{
				Gizmos.color = new Color(0.7f, 0.5f, 0.1f, 0.5f);
				if (this.lastCompletedNodePath != null)
				{
					for (int i = 0; i < this.lastCompletedNodePath.Count - 1; i++)
					{
						Gizmos.DrawLine((Vector3)this.lastCompletedNodePath[i].position, (Vector3)this.lastCompletedNodePath[i + 1].position);
					}
				}
			}
			Gizmos.color = new Color(0f, 1f, 0f, 1f);
			if (this.lastCompletedVectorPath != null)
			{
				for (int j = 0; j < this.lastCompletedVectorPath.Count - 1; j++)
				{
					Gizmos.DrawLine(this.lastCompletedVectorPath[j], this.lastCompletedVectorPath[j + 1]);
				}
			}
		}

		// Token: 0x04003F70 RID: 16240
		public bool drawGizmos = true;

		// Token: 0x04003F71 RID: 16241
		public bool detailedGizmos;

		// Token: 0x04003F72 RID: 16242
		[HideInInspector]
		public StartEndModifier startEndModifier = new StartEndModifier();

		// Token: 0x04003F73 RID: 16243
		[HideInInspector]
		public int traversableTags = -1;

		// Token: 0x04003F74 RID: 16244
		[HideInInspector]
		public int[] tagPenalties = new int[32];

		// Token: 0x04003F75 RID: 16245
		[HideInInspector]
		public int graphMask = -1;

		// Token: 0x04003F76 RID: 16246
		public OnPathDelegate pathCallback;

		// Token: 0x04003F77 RID: 16247
		public OnPathDelegate preProcessPath;

		// Token: 0x04003F78 RID: 16248
		public OnPathDelegate postProcessPath;

		// Token: 0x04003F79 RID: 16249
		[NonSerialized]
		private List<Vector3> lastCompletedVectorPath;

		// Token: 0x04003F7A RID: 16250
		[NonSerialized]
		private List<GraphNode> lastCompletedNodePath;

		// Token: 0x04003F7B RID: 16251
		[NonSerialized]
		protected Path path;

		// Token: 0x04003F7C RID: 16252
		[NonSerialized]
		private Path prevPath;

		// Token: 0x04003F7D RID: 16253
		private readonly OnPathDelegate onPathDelegate;

		// Token: 0x04003F7E RID: 16254
		private readonly OnPathDelegate onPartialPathDelegate;

		// Token: 0x04003F7F RID: 16255
		private OnPathDelegate tmpPathCallback;

		// Token: 0x04003F80 RID: 16256
		protected uint lastPathID;

		// Token: 0x04003F81 RID: 16257
		private readonly List<IPathModifier> modifiers = new List<IPathModifier>();

		// Token: 0x02000725 RID: 1829
		public enum ModifierPass
		{
			// Token: 0x040049A7 RID: 18855
			PreProcess,
			// Token: 0x040049A8 RID: 18856
			PostProcess = 2
		}
	}
}
