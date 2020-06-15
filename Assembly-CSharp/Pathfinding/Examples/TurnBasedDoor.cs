using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000603 RID: 1539
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(SingleNodeBlocker))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_door.php")]
	public class TurnBasedDoor : MonoBehaviour
	{
		// Token: 0x06002A21 RID: 10785 RVA: 0x001C71AA File Offset: 0x001C53AA
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.blocker = base.GetComponent<SingleNodeBlocker>();
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x001C71C4 File Offset: 0x001C53C4
		private void Start()
		{
			this.blocker.BlockAtCurrentPosition();
			this.animator.CrossFade("close", 0.2f);
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x001C71E6 File Offset: 0x001C53E6
		public void Close()
		{
			base.StartCoroutine(this.WaitAndClose());
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x001C71F5 File Offset: 0x001C53F5
		private IEnumerator WaitAndClose()
		{
			List<SingleNodeBlocker> selector = new List<SingleNodeBlocker>
			{
				this.blocker
			};
			GraphNode node = AstarPath.active.GetNearest(base.transform.position).node;
			if (this.blocker.manager.NodeContainsAnyExcept(node, selector))
			{
				this.animator.CrossFade("blocked", 0.2f);
			}
			while (this.blocker.manager.NodeContainsAnyExcept(node, selector))
			{
				yield return null;
			}
			this.open = false;
			this.animator.CrossFade("close", 0.2f);
			this.blocker.BlockAtCurrentPosition();
			yield break;
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x001C7204 File Offset: 0x001C5404
		public void Open()
		{
			base.StopAllCoroutines();
			this.animator.CrossFade("open", 0.2f);
			this.open = true;
			this.blocker.Unblock();
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x001C7233 File Offset: 0x001C5433
		public void Toggle()
		{
			if (this.open)
			{
				this.Close();
				return;
			}
			this.Open();
		}

		// Token: 0x04004470 RID: 17520
		private Animator animator;

		// Token: 0x04004471 RID: 17521
		private SingleNodeBlocker blocker;

		// Token: 0x04004472 RID: 17522
		private bool open;
	}
}
