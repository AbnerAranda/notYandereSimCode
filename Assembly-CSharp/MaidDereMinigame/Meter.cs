using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000517 RID: 1303
	public class Meter : MonoBehaviour
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x0018B2C8 File Offset: 0x001894C8
		private void Awake()
		{
			this.startPos = this.fillBar.transform.localPosition.x;
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x0018B2E8 File Offset: 0x001894E8
		public void SetFill(float interpolater)
		{
			float num = Mathf.Lerp(this.emptyPos, this.startPos, interpolater);
			num = Mathf.Round(num * 50f) / 50f;
			this.fillBar.transform.localPosition = new Vector3(num, 0f, 0f);
		}

		// Token: 0x04003EAA RID: 16042
		public SpriteRenderer fillBar;

		// Token: 0x04003EAB RID: 16043
		public float emptyPos;

		// Token: 0x04003EAC RID: 16044
		private float startPos;
	}
}
