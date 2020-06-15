using System;
using UnityEngine;

namespace Aura2API
{
	// Token: 0x0200051F RID: 1311
	public class AutoMoveAndRotate : MonoBehaviour
	{
		// Token: 0x060020B2 RID: 8370 RVA: 0x0018BAB7 File Offset: 0x00189CB7
		private void Start()
		{
			this.m_LastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x0018BAC4 File Offset: 0x00189CC4
		private void Update()
		{
			float d = Time.deltaTime;
			if (this.ignoreTimescale)
			{
				d = Time.realtimeSinceStartup - this.m_LastRealTime;
				this.m_LastRealTime = Time.realtimeSinceStartup;
			}
			base.transform.Translate(this.moveUnitsPerSecond.value * d, this.moveUnitsPerSecond.space);
			base.transform.Rotate(this.rotateDegreesPerSecond.value * d, this.rotateDegreesPerSecond.space);
		}

		// Token: 0x04003EC8 RID: 16072
		public AutoMoveAndRotate.Vector3andSpace moveUnitsPerSecond;

		// Token: 0x04003EC9 RID: 16073
		public AutoMoveAndRotate.Vector3andSpace rotateDegreesPerSecond;

		// Token: 0x04003ECA RID: 16074
		public bool ignoreTimescale;

		// Token: 0x04003ECB RID: 16075
		private float m_LastRealTime;

		// Token: 0x02000722 RID: 1826
		[Serializable]
		public class Vector3andSpace
		{
			// Token: 0x0400499A RID: 18842
			public Vector3 value;

			// Token: 0x0400499B RID: 18843
			public Space space = Space.Self;
		}
	}
}
