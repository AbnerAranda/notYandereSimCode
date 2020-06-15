using System;
using UnityEngine;

namespace Aura2API
{
	// Token: 0x02000523 RID: 1315
	public class SinRotationOffset : MonoBehaviour
	{
		// Token: 0x060020C0 RID: 8384 RVA: 0x0018BEFF File Offset: 0x0018A0FF
		private void Start()
		{
			this._initialRotation = ((this.space == Space.Self) ? base.transform.localRotation : base.transform.rotation);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0018BF28 File Offset: 0x0018A128
		private void Update()
		{
			Quaternion rhs = Quaternion.AngleAxis(this.sinAmplitude * Mathf.Sin(Time.time * this.sinSpeed + this.sinOffset), this.sinDirection);
			if (this.space == Space.Self)
			{
				base.transform.localRotation = this._initialRotation * rhs;
				return;
			}
			base.transform.rotation = this._initialRotation * rhs;
		}

		// Token: 0x04003EE9 RID: 16105
		private Quaternion _initialRotation;

		// Token: 0x04003EEA RID: 16106
		public float sinAmplitude = 15f;

		// Token: 0x04003EEB RID: 16107
		public Vector3 sinDirection = Vector3.up;

		// Token: 0x04003EEC RID: 16108
		public float sinOffset;

		// Token: 0x04003EED RID: 16109
		public float sinSpeed = 1f;

		// Token: 0x04003EEE RID: 16110
		public Space space = Space.Self;
	}
}
