using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000525 RID: 1317
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_patrol.php")]
	public class Patrol : VersionedMonoBehaviour
	{
		// Token: 0x060020C7 RID: 8391 RVA: 0x0018C06C File Offset: 0x0018A26C
		protected override void Awake()
		{
			base.Awake();
			this.agent = base.GetComponent<IAstarAI>();
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x0018C080 File Offset: 0x0018A280
		private void Update()
		{
			if (this.targets.Length == 0)
			{
				return;
			}
			bool flag = false;
			if (this.agent.reachedEndOfPath && !this.agent.pathPending && float.IsPositiveInfinity(this.switchTime))
			{
				this.switchTime = Time.time + this.delay;
			}
			if (Time.time >= this.switchTime)
			{
				this.index++;
				flag = true;
				this.switchTime = float.PositiveInfinity;
			}
			this.index %= this.targets.Length;
			this.agent.destination = this.targets[this.index].position;
			if (flag)
			{
				this.agent.SearchPath();
			}
		}

		// Token: 0x04003EF1 RID: 16113
		public Transform[] targets;

		// Token: 0x04003EF2 RID: 16114
		public float delay;

		// Token: 0x04003EF3 RID: 16115
		private int index;

		// Token: 0x04003EF4 RID: 16116
		private IAstarAI agent;

		// Token: 0x04003EF5 RID: 16117
		private float switchTime = float.PositiveInfinity;
	}
}
