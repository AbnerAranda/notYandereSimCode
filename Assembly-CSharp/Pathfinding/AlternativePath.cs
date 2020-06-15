using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000595 RID: 1429
	[AddComponentMenu("Pathfinding/Modifiers/Alternative Path")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_alternative_path.php")]
	[Serializable]
	public class AlternativePath : MonoModifier
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x001AB7E3 File Offset: 0x001A99E3
		public override int Order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x001AB7E7 File Offset: 0x001A99E7
		public override void Apply(Path p)
		{
			if (this == null)
			{
				return;
			}
			this.ApplyNow(p.path);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x001AB7FF File Offset: 0x001A99FF
		protected void OnDestroy()
		{
			this.destroyed = true;
			this.ClearOnDestroy();
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x001AB80E File Offset: 0x001A9A0E
		private void ClearOnDestroy()
		{
			this.InversePrevious();
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x001AB818 File Offset: 0x001A9A18
		private void InversePrevious()
		{
			if (this.prevNodes != null)
			{
				bool flag = false;
				for (int i = 0; i < this.prevNodes.Count; i++)
				{
					if ((ulong)this.prevNodes[i].Penalty < (ulong)((long)this.prevPenalty))
					{
						flag = true;
						this.prevNodes[i].Penalty = 0U;
					}
					else
					{
						this.prevNodes[i].Penalty = (uint)((ulong)this.prevNodes[i].Penalty - (ulong)((long)this.prevPenalty));
					}
				}
				if (flag)
				{
					Debug.LogWarning("Penalty for some nodes has been reset while the AlternativePath modifier was active (possibly because of a graph update). Some penalties might be incorrect (they may be lower than expected for the affected nodes)");
				}
			}
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x001AB8B4 File Offset: 0x001A9AB4
		private void ApplyNow(List<GraphNode> nodes)
		{
			this.InversePrevious();
			this.prevNodes.Clear();
			if (this.destroyed)
			{
				return;
			}
			if (nodes != null)
			{
				for (int i = this.rnd.Next(this.randomStep); i < nodes.Count; i += this.rnd.Next(1, this.randomStep))
				{
					nodes[i].Penalty = (uint)((ulong)nodes[i].Penalty + (ulong)((long)this.penalty));
					this.prevNodes.Add(nodes[i]);
				}
			}
			this.prevPenalty = this.penalty;
		}

		// Token: 0x04004206 RID: 16902
		public int penalty = 1000;

		// Token: 0x04004207 RID: 16903
		public int randomStep = 10;

		// Token: 0x04004208 RID: 16904
		private List<GraphNode> prevNodes = new List<GraphNode>();

		// Token: 0x04004209 RID: 16905
		private int prevPenalty;

		// Token: 0x0400420A RID: 16906
		private readonly System.Random rnd = new System.Random();

		// Token: 0x0400420B RID: 16907
		private bool destroyed;
	}
}
