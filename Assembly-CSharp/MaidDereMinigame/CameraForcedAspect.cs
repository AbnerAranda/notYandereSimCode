using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000501 RID: 1281
	[RequireComponent(typeof(Camera))]
	public class CameraForcedAspect : MonoBehaviour
	{
		// Token: 0x06002027 RID: 8231 RVA: 0x0018A17F File Offset: 0x0018837F
		private void Awake()
		{
			this.cam = base.GetComponent<Camera>();
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x0018A190 File Offset: 0x00188390
		private void Start()
		{
			float num = this.targetAspect.x / this.targetAspect.y;
			float num2 = (float)Screen.width / (float)Screen.height / num;
			if (num2 < 1f)
			{
				Rect rect = this.cam.rect;
				rect.width = 1f;
				rect.height = num2;
				rect.x = 0f;
				rect.y = (1f - num2) / 2f;
				this.cam.rect = rect;
				return;
			}
			Rect rect2 = this.cam.rect;
			float num3 = 1f / num2;
			rect2.width = num3;
			rect2.height = 1f;
			rect2.x = (1f - num3) / 2f;
			rect2.y = 0f;
			this.cam.rect = rect2;
		}

		// Token: 0x04003E51 RID: 15953
		public Vector2 targetAspect = new Vector2(16f, 9f);

		// Token: 0x04003E52 RID: 15954
		private Camera cam;
	}
}
