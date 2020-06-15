using System;
using UnityEngine;

namespace Aura2API
{
	// Token: 0x02000520 RID: 1312
	public class DebugFps : MonoBehaviour
	{
		// Token: 0x060020B5 RID: 8373 RVA: 0x0018BB48 File Offset: 0x00189D48
		private void Update()
		{
			if (Time.time - this._timestamp > this.interval)
			{
				this._meanFps = this._accumulationValue / (float)this._framesCount;
				this._timestamp = Time.time;
				this._framesCount = 0;
				this._accumulationValue = 0f;
			}
			this._framesCount++;
			this._rawFps = 1f / Time.deltaTime;
			this._accumulationValue += this._rawFps;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0018BBCC File Offset: 0x00189DCC
		private void OnGUI()
		{
			GUI.color = Color.white;
			GUI.Label(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), string.Concat(new object[]
			{
				"Mean FPS over ",
				this.interval,
				" second(s) = ",
				this._meanFps,
				"\nRaw FPS = ",
				this._rawFps
			}));
		}

		// Token: 0x04003ECC RID: 16076
		public float interval = 1f;

		// Token: 0x04003ECD RID: 16077
		private float _accumulationValue;

		// Token: 0x04003ECE RID: 16078
		private int _framesCount;

		// Token: 0x04003ECF RID: 16079
		private float _timestamp;

		// Token: 0x04003ED0 RID: 16080
		private float _rawFps;

		// Token: 0x04003ED1 RID: 16081
		private float _meanFps;
	}
}
