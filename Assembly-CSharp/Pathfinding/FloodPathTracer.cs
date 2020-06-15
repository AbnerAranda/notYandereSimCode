using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005AD RID: 1453
	public class FloodPathTracer : ABPath
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x0002D199 File Offset: 0x0002B399
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x001B035C File Offset: 0x001AE55C
		public static FloodPathTracer Construct(Vector3 start, FloodPath flood, OnPathDelegate callback = null)
		{
			FloodPathTracer path = PathPool.GetPath<FloodPathTracer>();
			path.Setup(start, flood, callback);
			return path;
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x001B036C File Offset: 0x001AE56C
		protected void Setup(Vector3 start, FloodPath flood, OnPathDelegate callback)
		{
			this.flood = flood;
			if (flood == null || flood.PipelineState < PathState.Returned)
			{
				throw new ArgumentException("You must supply a calculated FloodPath to the 'flood' argument");
			}
			base.Setup(start, flood.originalStartPoint, callback);
			this.nnConstraint = new FloodPathConstraint(flood);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x001B03A6 File Offset: 0x001AE5A6
		protected override void Reset()
		{
			base.Reset();
			this.flood = null;
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x001B03B5 File Offset: 0x001AE5B5
		protected override void Initialize()
		{
			if (this.startNode != null && this.flood.HasPathTo(this.startNode))
			{
				this.Trace(this.startNode);
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			base.FailWithError("Could not find valid start node");
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x001B03F1 File Offset: 0x001AE5F1
		protected override void CalculateStep(long targetTick)
		{
			if (!base.IsDone())
			{
				throw new Exception("Something went wrong. At this point the path should be completed");
			}
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x001B0408 File Offset: 0x001AE608
		public void Trace(GraphNode from)
		{
			GraphNode graphNode = from;
			int num = 0;
			while (graphNode != null)
			{
				this.path.Add(graphNode);
				this.vectorPath.Add((Vector3)graphNode.position);
				graphNode = this.flood.GetParent(graphNode);
				num++;
				if (num > 1024)
				{
					Debug.LogWarning("Inifinity loop? >1024 node path. Remove this message if you really have that long paths (FloodPathTracer.cs, Trace function)");
					return;
				}
			}
		}

		// Token: 0x0400427D RID: 17021
		protected FloodPath flood;
	}
}
