using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class NotificationScript : MonoBehaviour
{
	// Token: 0x06001896 RID: 6294 RVA: 0x000E1F14 File Offset: 0x000E0114
	private void Start()
	{
		if (MissionModeGlobals.MissionMode)
		{
			this.Icon[0].color = new Color(1f, 1f, 1f, 1f);
			this.Icon[1].color = new Color(1f, 1f, 1f, 1f);
			this.Label.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x000E1F98 File Offset: 0x000E0198
	private void Update()
	{
		if (!this.Display)
		{
			this.Panel.alpha -= Time.deltaTime * ((this.NotificationManager.NotificationsSpawned > this.ID + 2) ? 3f : 1f);
			if (this.Panel.alpha <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 4f)
			{
				this.Display = false;
			}
			if (this.NotificationManager.NotificationsSpawned > this.ID + 2)
			{
				this.Display = false;
			}
		}
	}

	// Token: 0x04002439 RID: 9273
	public NotificationManagerScript NotificationManager;

	// Token: 0x0400243A RID: 9274
	public UISprite[] Icon;

	// Token: 0x0400243B RID: 9275
	public UIPanel Panel;

	// Token: 0x0400243C RID: 9276
	public UILabel Label;

	// Token: 0x0400243D RID: 9277
	public bool Display;

	// Token: 0x0400243E RID: 9278
	public float Timer;

	// Token: 0x0400243F RID: 9279
	public int ID;
}
