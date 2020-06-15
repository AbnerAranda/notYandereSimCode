﻿using System;
using UnityEngine;

// Token: 0x02000262 RID: 610
public class DemonPortalScript : MonoBehaviour
{
	// Token: 0x06001334 RID: 4916 RVA: 0x000A0B6C File Offset: 0x0009ED6C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMove = false;
			UnityEngine.Object.Instantiate<GameObject>(this.DarkAura, this.Yandere.transform.position + Vector3.up * 0.81f, Quaternion.identity);
			this.Timer += Time.deltaTime;
		}
		this.DemonRealmAudio.volume = Mathf.MoveTowards(this.DemonRealmAudio.volume, (this.Yandere.transform.position.y > 1000f) ? 0.5f : 0f, Time.deltaTime * 0.1f);
		if (this.Timer > 0f)
		{
			if (this.Yandere.transform.position.y > 1000f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 4f)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
					if (this.Darkness.color.a == 1f)
					{
						this.Yandere.transform.position = new Vector3(12f, 0f, 28f);
						this.Yandere.Character.SetActive(true);
						this.Yandere.SetAnimationLayers();
						this.HeartbeatCamera.SetActive(true);
						this.FPS.SetActive(true);
						this.HUD.SetActive(true);
						return;
					}
				}
				else if (this.Timer > 1f)
				{
					this.Yandere.Character.SetActive(false);
					return;
				}
			}
			else
			{
				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0.5f, Time.deltaTime * 0.5f);
				if (this.Jukebox.Volume == 0.5f)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
					if (this.Darkness.color.a == 0f)
					{
						base.transform.parent.gameObject.SetActive(false);
						this.Darkness.enabled = false;
						this.Yandere.CanMove = true;
						this.Clock.StopTime = false;
						this.Timer = 0f;
					}
				}
			}
		}
	}

	// Token: 0x040019ED RID: 6637
	public YandereScript Yandere;

	// Token: 0x040019EE RID: 6638
	public JukeboxScript Jukebox;

	// Token: 0x040019EF RID: 6639
	public PromptScript Prompt;

	// Token: 0x040019F0 RID: 6640
	public ClockScript Clock;

	// Token: 0x040019F1 RID: 6641
	public AudioSource DemonRealmAudio;

	// Token: 0x040019F2 RID: 6642
	public GameObject HeartbeatCamera;

	// Token: 0x040019F3 RID: 6643
	public GameObject DarkAura;

	// Token: 0x040019F4 RID: 6644
	public GameObject FPS;

	// Token: 0x040019F5 RID: 6645
	public GameObject HUD;

	// Token: 0x040019F6 RID: 6646
	public UISprite Darkness;

	// Token: 0x040019F7 RID: 6647
	public float Timer;
}
