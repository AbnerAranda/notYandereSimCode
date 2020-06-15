using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005B0 RID: 1456
	public class XPath : ABPath
	{
		// Token: 0x06002768 RID: 10088 RVA: 0x001B18EE File Offset: 0x001AFAEE
		public new static XPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			XPath path = PathPool.GetPath<XPath>();
			path.Setup(start, end, callback);
			path.endingCondition = new ABPathEndingCondition(path);
			return path;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x001B190A File Offset: 0x001AFB0A
		protected override void Reset()
		{
			base.Reset();
			this.endingCondition = null;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x0002D199 File Offset: 0x0002B399
		protected override bool EndPointGridGraphSpecialCase(GraphNode endNode)
		{
			return false;
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x001B191C File Offset: 0x001AFB1C
		protected override void CompletePathIfStartIsValidTarget()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			if (this.endingCondition.TargetFound(pathNode))
			{
				this.ChangeEndNode(this.startNode);
				this.Trace(pathNode);
				base.CompleteState = PathCompleteState.Complete;
			}
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x001B1964 File Offset: 0x001AFB64
		private void ChangeEndNode(GraphNode target)
		{
			if (this.endNode != null && this.endNode != this.startNode)
			{
				PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
				pathNode.flag1 = (pathNode.flag2 = false);
			}
			this.endNode = target;
			this.endPoint = (Vector3)target.position;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x001B19C0 File Offset: 0x001AFBC0
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.endingCondition.TargetFound(this.currentR))
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					base.FailWithError("Searched whole area but could not find target");
					return;
				}
				this.currentR = this.pathHandler.heap.Remove();
				if (num > 500)
				{
					if (DateTime.UtcNow.Ticks >= targetTick)
					{
						return;
					}
					num = 0;
					if (this.searchedNodes > 1000000)
					{
						throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
					}
				}
				num++;
			}
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.ChangeEndNode(this.currentR.node);
				this.Trace(this.currentR);
			}
		}

		// Token: 0x04004294 RID: 17044
		public PathEndingCondition endingCondition;
	}
}
