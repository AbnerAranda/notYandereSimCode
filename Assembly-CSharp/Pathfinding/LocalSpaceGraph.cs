using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000570 RID: 1392
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_local_space_graph.php")]
	public class LocalSpaceGraph : VersionedMonoBehaviour
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x0019C456 File Offset: 0x0019A656
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x0019C45E File Offset: 0x0019A65E
		public GraphTransform transformation { get; private set; }

		// Token: 0x0600248A RID: 9354 RVA: 0x0019C467 File Offset: 0x0019A667
		private void Start()
		{
			this.originalMatrix = base.transform.worldToLocalMatrix;
			base.transform.hasChanged = true;
			this.Refresh();
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x0019C48C File Offset: 0x0019A68C
		public void Refresh()
		{
			if (base.transform.hasChanged)
			{
				this.transformation = new GraphTransform(base.transform.localToWorldMatrix * this.originalMatrix);
				base.transform.hasChanged = false;
			}
		}

		// Token: 0x040040F9 RID: 16633
		private Matrix4x4 originalMatrix;
	}
}
