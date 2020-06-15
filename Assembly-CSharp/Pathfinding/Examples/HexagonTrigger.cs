using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pathfinding.Examples
{
	// Token: 0x02000602 RID: 1538
	[RequireComponent(typeof(Animator))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_hexagon_trigger.php")]
	public class HexagonTrigger : MonoBehaviour
	{
		// Token: 0x06002A1D RID: 10781 RVA: 0x001C70ED File Offset: 0x001C52ED
		private void Awake()
		{
			this.anim = base.GetComponent<Animator>();
			this.button.interactable = false;
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x001C7108 File Offset: 0x001C5308
		private void OnTriggerEnter(Collider coll)
		{
			TurnBasedAI componentInParent = coll.GetComponentInParent<TurnBasedAI>();
			GraphNode node = AstarPath.active.GetNearest(base.transform.position).node;
			if (componentInParent != null && componentInParent.targetNode == node)
			{
				this.button.interactable = true;
				this.visible = true;
				this.anim.CrossFade("show", 0.1f);
			}
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x001C7171 File Offset: 0x001C5371
		private void OnTriggerExit(Collider coll)
		{
			if (coll.GetComponentInParent<TurnBasedAI>() != null && this.visible)
			{
				this.button.interactable = false;
				this.anim.CrossFade("hide", 0.1f);
			}
		}

		// Token: 0x0400446D RID: 17517
		public Button button;

		// Token: 0x0400446E RID: 17518
		private Animator anim;

		// Token: 0x0400446F RID: 17519
		private bool visible;
	}
}
