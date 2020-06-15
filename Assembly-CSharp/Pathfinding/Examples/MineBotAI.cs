using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200060B RID: 1547
	[RequireComponent(typeof(Seeker))]
	[Obsolete("This script has been replaced by Pathfinding.Examples.MineBotAnimation. Any uses of this script in the Unity editor will be automatically replaced by one AIPath component and one MineBotAnimation component.")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mine_bot_a_i.php")]
	public class MineBotAI : AIPath
	{
		// Token: 0x040044A1 RID: 17569
		public Animation anim;

		// Token: 0x040044A2 RID: 17570
		public float sleepVelocity = 0.4f;

		// Token: 0x040044A3 RID: 17571
		public float animationSpeed = 0.2f;

		// Token: 0x040044A4 RID: 17572
		public GameObject endOfPathEffect;
	}
}
