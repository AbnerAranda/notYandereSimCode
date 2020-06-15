using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F9 RID: 1529
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_a_i.php")]
	public class TurnBasedAI : VersionedMonoBehaviour
	{
		// Token: 0x060029F2 RID: 10738 RVA: 0x001C4FCD File Offset: 0x001C31CD
		private void Start()
		{
			this.blocker.BlockAtCurrentPosition();
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x001C4FDA File Offset: 0x001C31DA
		protected override void Awake()
		{
			base.Awake();
			this.traversalProvider = new BlockManager.TraversalProvider(this.blockManager, BlockManager.BlockMode.AllExceptSelector, new List<SingleNodeBlocker>
			{
				this.blocker
			});
		}

		// Token: 0x04004427 RID: 17447
		public int movementPoints = 2;

		// Token: 0x04004428 RID: 17448
		public BlockManager blockManager;

		// Token: 0x04004429 RID: 17449
		public SingleNodeBlocker blocker;

		// Token: 0x0400442A RID: 17450
		public GraphNode targetNode;

		// Token: 0x0400442B RID: 17451
		public BlockManager.TraversalProvider traversalProvider;
	}
}
