using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000605 RID: 1541
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_example_mover.php")]
	public class ExampleMover : MonoBehaviour
	{
		// Token: 0x06002A32 RID: 10802 RVA: 0x001C74EF File Offset: 0x001C56EF
		private void Awake()
		{
			this.agent = base.GetComponent<RVOExampleAgent>();
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x001C74FD File Offset: 0x001C56FD
		private void Start()
		{
			this.agent.SetTarget(this.target.position);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x001C7515 File Offset: 0x001C5715
		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				this.agent.SetTarget(this.target.position);
			}
		}

		// Token: 0x0400447A RID: 17530
		private RVOExampleAgent agent;

		// Token: 0x0400447B RID: 17531
		public Transform target;
	}
}
