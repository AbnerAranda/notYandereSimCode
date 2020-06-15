using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000608 RID: 1544
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_astar_smooth_follow2.php")]
	public class AstarSmoothFollow2 : MonoBehaviour
	{
		// Token: 0x06002A46 RID: 10822 RVA: 0x001C7F78 File Offset: 0x001C6178
		private void LateUpdate()
		{
			Vector3 b;
			if (this.staticOffset)
			{
				b = this.target.position + new Vector3(0f, this.height, this.distance);
			}
			else if (this.followBehind)
			{
				b = this.target.TransformPoint(0f, this.height, -this.distance);
			}
			else
			{
				b = this.target.TransformPoint(0f, this.height, this.distance);
			}
			base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * this.damping);
			if (this.smoothRotation)
			{
				Quaternion b2 = Quaternion.LookRotation(this.target.position - base.transform.position, this.target.up);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b2, Time.deltaTime * this.rotationDamping);
				return;
			}
			base.transform.LookAt(this.target, this.target.up);
		}

		// Token: 0x04004491 RID: 17553
		public Transform target;

		// Token: 0x04004492 RID: 17554
		public float distance = 3f;

		// Token: 0x04004493 RID: 17555
		public float height = 3f;

		// Token: 0x04004494 RID: 17556
		public float damping = 5f;

		// Token: 0x04004495 RID: 17557
		public bool smoothRotation = true;

		// Token: 0x04004496 RID: 17558
		public bool followBehind = true;

		// Token: 0x04004497 RID: 17559
		public float rotationDamping = 10f;

		// Token: 0x04004498 RID: 17560
		public bool staticOffset;
	}
}
