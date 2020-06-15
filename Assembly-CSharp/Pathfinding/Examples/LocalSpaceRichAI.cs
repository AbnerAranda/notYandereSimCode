using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000600 RID: 1536
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_local_space_rich_a_i.php")]
	public class LocalSpaceRichAI : RichAI
	{
		// Token: 0x06002A15 RID: 10773 RVA: 0x001C7042 File Offset: 0x001C5242
		private void RefreshTransform()
		{
			this.graph.Refresh();
			this.richPath.transform = this.graph.transformation;
			this.movementPlane = this.graph.transformation;
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x001C7076 File Offset: 0x001C5276
		protected override void Start()
		{
			this.RefreshTransform();
			base.Start();
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x001C7084 File Offset: 0x001C5284
		protected override void CalculatePathRequestEndpoints(out Vector3 start, out Vector3 end)
		{
			this.RefreshTransform();
			base.CalculatePathRequestEndpoints(out start, out end);
			start = this.graph.transformation.InverseTransform(start);
			end = this.graph.transformation.InverseTransform(end);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x001C70D7 File Offset: 0x001C52D7
		protected override void Update()
		{
			this.RefreshTransform();
			base.Update();
		}

		// Token: 0x0400446B RID: 17515
		public LocalSpaceGraph graph;
	}
}
