using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000524 RID: 1316
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour
	{
		// Token: 0x060020C3 RID: 8387 RVA: 0x0018BFC8 File Offset: 0x0018A1C8
		private void OnEnable()
		{
			this.ai = base.GetComponent<IAstarAI>();
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Combine(astarAI.onSearchPath, new Action(this.Update));
			}
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0018C005 File Offset: 0x0018A205
		private void OnDisable()
		{
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Remove(astarAI.onSearchPath, new Action(this.Update));
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0018C036 File Offset: 0x0018A236
		private void Update()
		{
			if (this.target != null && this.ai != null)
			{
				this.ai.destination = this.target.position;
			}
		}

		// Token: 0x04003EEF RID: 16111
		public Transform target;

		// Token: 0x04003EF0 RID: 16112
		public IAstarAI ai;
	}
}
