using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200060C RID: 1548
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mine_bot_animation.php")]
	public class MineBotAnimation : VersionedMonoBehaviour
	{
		// Token: 0x06002A50 RID: 10832 RVA: 0x001C829F File Offset: 0x001C649F
		protected override void Awake()
		{
			base.Awake();
			this.ai = base.GetComponent<IAstarAI>();
			this.tr = base.GetComponent<Transform>();
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x001C82C0 File Offset: 0x001C64C0
		private void Start()
		{
			this.anim["forward"].layer = 10;
			this.anim.Play("awake");
			this.anim.Play("forward");
			this.anim["awake"].wrapMode = WrapMode.Once;
			this.anim["awake"].speed = 0f;
			this.anim["awake"].normalizedTime = 1f;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x001C8350 File Offset: 0x001C6550
		private void OnTargetReached()
		{
			if (this.endOfPathEffect != null && Vector3.Distance(this.tr.position, this.lastTarget) > 1f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.endOfPathEffect, this.tr.position, this.tr.rotation);
				this.lastTarget = this.tr.position;
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x001C83BC File Offset: 0x001C65BC
		protected void Update()
		{
			if (this.ai.reachedEndOfPath)
			{
				if (!this.isAtDestination)
				{
					this.OnTargetReached();
				}
				this.isAtDestination = true;
			}
			else
			{
				this.isAtDestination = false;
			}
			Vector3 vector = this.tr.InverseTransformDirection(this.ai.velocity);
			vector.y = 0f;
			if (vector.sqrMagnitude <= this.sleepVelocity * this.sleepVelocity)
			{
				this.anim.Blend("forward", 0f, 0.2f);
				return;
			}
			this.anim.Blend("forward", 1f, 0.2f);
			AnimationState animationState = this.anim["forward"];
			float z = vector.z;
			animationState.speed = z * this.animationSpeed;
		}

		// Token: 0x040044A5 RID: 17573
		public Animation anim;

		// Token: 0x040044A6 RID: 17574
		public float sleepVelocity = 0.4f;

		// Token: 0x040044A7 RID: 17575
		public float animationSpeed = 0.2f;

		// Token: 0x040044A8 RID: 17576
		public GameObject endOfPathEffect;

		// Token: 0x040044A9 RID: 17577
		private bool isAtDestination;

		// Token: 0x040044AA RID: 17578
		private IAstarAI ai;

		// Token: 0x040044AB RID: 17579
		private Transform tr;

		// Token: 0x040044AC RID: 17580
		protected Vector3 lastTarget;
	}
}
