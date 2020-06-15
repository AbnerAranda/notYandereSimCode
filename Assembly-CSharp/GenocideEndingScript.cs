using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002B5 RID: 693
public class GenocideEndingScript : MonoBehaviour
{
	// Token: 0x06001450 RID: 5200 RVA: 0x000B4A96 File Offset: 0x000B2C96
	private void Start()
	{
		this.Senpai["kidnapTorture_01"].speed = 0.9f;
		GameGlobals.DarkEnding = true;
		Time.timeScale = 1f;
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x000B4AC4 File Offset: 0x000B2CC4
	private void Update()
	{
		if (Input.GetKeyDown("="))
		{
			Time.timeScale += 1f;
			this.MyAudio.pitch = Time.timeScale;
		}
		if (Input.GetKeyDown("-"))
		{
			Time.timeScale -= 1f;
			this.MyAudio.pitch = Time.timeScale;
		}
		if (this.SpeechPhase > 9)
		{
			base.transform.Translate(Vector3.forward * -0.1f * Time.deltaTime);
			if (this.MyAudio.isPlaying)
			{
				this.Senpai.Play();
				if (this.MyAudio.time < 7f)
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.25f);
				}
				else
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 0.25f);
				}
			}
			this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
		}
		if (!this.MyAudio.isPlaying || Input.GetButtonDown("A"))
		{
			if (Input.GetButtonDown("A"))
			{
				this.Timer = 1f;
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > this.Delay)
			{
				this.SpeechPhase++;
				this.Timer = 0f;
				if (this.SpeechPhase < this.SpeechClip.Length)
				{
					this.Subtitle.text = this.SpeechText[this.SpeechPhase];
					this.MyAudio.clip = this.SpeechClip[this.SpeechPhase];
					this.Delay = this.SpeechDelay[this.SpeechPhase];
					this.MyAudio.Play();
					return;
				}
				SceneManager.LoadScene("CreditsScene");
			}
		}
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x000B4CC8 File Offset: 0x000B2EC8
	private void LateUpdate()
	{
		this.Neck.transform.localEulerAngles = new Vector3(0f, this.Neck.transform.localEulerAngles.y, this.Neck.transform.localEulerAngles.z);
	}

	// Token: 0x04001D1A RID: 7450
	public AudioSource MyAudio;

	// Token: 0x04001D1B RID: 7451
	public UISprite Darkness;

	// Token: 0x04001D1C RID: 7452
	public UILabel Subtitle;

	// Token: 0x04001D1D RID: 7453
	public Animation Senpai;

	// Token: 0x04001D1E RID: 7454
	public Transform Neck;

	// Token: 0x04001D1F RID: 7455
	public AudioClip[] SpeechClip;

	// Token: 0x04001D20 RID: 7456
	public string[] SpeechText;

	// Token: 0x04001D21 RID: 7457
	public float[] SpeechDelay;

	// Token: 0x04001D22 RID: 7458
	public float[] SpeechTime;

	// Token: 0x04001D23 RID: 7459
	public int SpeechPhase;

	// Token: 0x04001D24 RID: 7460
	public float Alpha;

	// Token: 0x04001D25 RID: 7461
	public float Delay;

	// Token: 0x04001D26 RID: 7462
	public float Timer;
}
