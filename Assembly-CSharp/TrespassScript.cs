using System;
using UnityEngine;

// Token: 0x02000436 RID: 1078
public class TrespassScript : MonoBehaviour
{
	// Token: 0x06001C91 RID: 7313 RVA: 0x00157148 File Offset: 0x00155348
	private void OnTriggerEnter(Collider other)
	{
		if (base.enabled && other.gameObject.layer == 13)
		{
			this.YandereObject = other.gameObject;
			this.Yandere = other.gameObject.GetComponent<YandereScript>();
			if (this.Yandere != null)
			{
				if (!this.Yandere.Trespassing)
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Intrude);
				}
				this.Yandere.Trespassing = true;
			}
		}
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x001571C1 File Offset: 0x001553C1
	private void OnTriggerExit(Collider other)
	{
		if (this.Yandere != null && other.gameObject == this.YandereObject)
		{
			this.Yandere.Trespassing = false;
		}
	}

	// Token: 0x040035D1 RID: 13777
	public GameObject YandereObject;

	// Token: 0x040035D2 RID: 13778
	public YandereScript Yandere;

	// Token: 0x040035D3 RID: 13779
	public bool HideNotification;

	// Token: 0x040035D4 RID: 13780
	public bool OffLimits;
}
