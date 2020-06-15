using System;
using UnityEngine;

namespace Aura2API
{
	// Token: 0x0200051E RID: 1310
	public class AuraFreeCamera : MonoBehaviour
	{
		// Token: 0x060020AF RID: 8367 RVA: 0x0018B8F8 File Offset: 0x00189AF8
		private void Start()
		{
			this.m_yaw = base.transform.rotation.eulerAngles.y;
			this.m_pitch = base.transform.rotation.eulerAngles.x;
			Cursor.visible = this.showCursor;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0018B94C File Offset: 0x00189B4C
		private void Update()
		{
			if (!this.freeLookEnabled)
			{
				return;
			}
			this.m_yaw = (this.m_yaw + this.lookSpeed * Input.GetAxis("Mouse X")) % 360f;
			this.m_pitch = (this.m_pitch - this.lookSpeed * Input.GetAxis("Mouse Y")) % 360f;
			base.transform.rotation = Quaternion.AngleAxis(this.m_yaw, Vector3.up) * Quaternion.AngleAxis(this.m_pitch, Vector3.right);
			float num = Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? this.sprintSpeed : this.moveSpeed);
			float d = num * Input.GetAxis("Vertical");
			float d2 = num * Input.GetAxis("Horizontal");
			float d3 = num * ((Input.GetKey(KeyCode.Q) ? 1f : 0f) - (Input.GetKey(KeyCode.E) ? 1f : 0f));
			base.transform.position += base.transform.forward * d + base.transform.right * d2 + Vector3.up * d3;
		}

		// Token: 0x04003EC1 RID: 16065
		public bool freeLookEnabled;

		// Token: 0x04003EC2 RID: 16066
		public bool showCursor;

		// Token: 0x04003EC3 RID: 16067
		public float lookSpeed = 5f;

		// Token: 0x04003EC4 RID: 16068
		public float moveSpeed = 5f;

		// Token: 0x04003EC5 RID: 16069
		public float sprintSpeed = 50f;

		// Token: 0x04003EC6 RID: 16070
		private float m_yaw;

		// Token: 0x04003EC7 RID: 16071
		private float m_pitch;
	}
}
