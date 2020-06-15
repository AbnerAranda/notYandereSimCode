using System;
using UnityEngine;

// Token: 0x020002AB RID: 683
public class FountainScript : MonoBehaviour
{
	// Token: 0x06001429 RID: 5161 RVA: 0x000B1B43 File Offset: 0x000AFD43
	private void Start()
	{
		this.SpraySFX.volume = 0.1f;
		this.DropsSFX.volume = 0.1f;
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x000B1B68 File Offset: 0x000AFD68
	private void Update()
	{
		if (this.StartTimer < 1f)
		{
			this.StartTimer += Time.deltaTime;
			if (this.StartTimer > 1f)
			{
				this.SpraySFX.gameObject.SetActive(true);
				this.DropsSFX.gameObject.SetActive(true);
			}
		}
		if (this.Drowning)
		{
			if (this.Timer == 0f && this.EventSubtitle.transform.localScale.x < 1f)
			{
				this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
				this.EventSubtitle.text = "Hey, what are you -";
				base.GetComponent<AudioSource>().Play();
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > 3f && this.EventSubtitle.transform.localScale.x > 0f)
			{
				this.EventSubtitle.transform.localScale = Vector3.zero;
				this.EventSubtitle.text = string.Empty;
				this.Splashes.Play();
			}
			if (this.Timer > 9f)
			{
				this.Drowning = false;
				this.Splashes.Stop();
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x04001C9C RID: 7324
	public ParticleSystem Splashes;

	// Token: 0x04001C9D RID: 7325
	public UILabel EventSubtitle;

	// Token: 0x04001C9E RID: 7326
	public Collider[] Colliders;

	// Token: 0x04001C9F RID: 7327
	public bool Drowning;

	// Token: 0x04001CA0 RID: 7328
	public AudioSource SpraySFX;

	// Token: 0x04001CA1 RID: 7329
	public AudioSource DropsSFX;

	// Token: 0x04001CA2 RID: 7330
	public float StartTimer;

	// Token: 0x04001CA3 RID: 7331
	public float Timer;
}
