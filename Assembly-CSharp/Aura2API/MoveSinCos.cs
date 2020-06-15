using System;
using UnityEngine;

namespace Aura2API
{
	// Token: 0x02000522 RID: 1314
	public class MoveSinCos : MonoBehaviour
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x0018BDF4 File Offset: 0x00189FF4
		private void Start()
		{
			this._initialPosition = base.transform.position;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0018BE08 File Offset: 0x0018A008
		private void Update()
		{
			Vector3 vector = this.sinDirection.normalized * Mathf.Sin(Time.time * this.sinSpeed + this.sinOffset) * this.sinAmplitude;
			Vector3 vector2 = this.cosDirection.normalized * Mathf.Cos(Time.time * this.cosSpeed + this.cosOffset) * this.cosAmplitude;
			vector = ((this.space == Space.World) ? vector : base.transform.localToWorldMatrix.MultiplyVector(vector));
			vector2 = ((this.space == Space.World) ? vector2 : base.transform.localToWorldMatrix.MultiplyVector(vector2));
			base.transform.position = this._initialPosition + vector + vector2;
		}

		// Token: 0x04003EDF RID: 16095
		private Vector3 _initialPosition;

		// Token: 0x04003EE0 RID: 16096
		public float cosAmplitude;

		// Token: 0x04003EE1 RID: 16097
		public Vector3 cosDirection = Vector3.right;

		// Token: 0x04003EE2 RID: 16098
		public float cosOffset;

		// Token: 0x04003EE3 RID: 16099
		public float cosSpeed;

		// Token: 0x04003EE4 RID: 16100
		public float sinAmplitude;

		// Token: 0x04003EE5 RID: 16101
		public Vector3 sinDirection = Vector3.up;

		// Token: 0x04003EE6 RID: 16102
		public float sinOffset;

		// Token: 0x04003EE7 RID: 16103
		public float sinSpeed;

		// Token: 0x04003EE8 RID: 16104
		public Space space = Space.Self;
	}
}
