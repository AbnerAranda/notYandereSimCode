using System;
using UnityEngine;

// Token: 0x020002A0 RID: 672
public class FamilyVoiceScript : MonoBehaviour
{
	// Token: 0x06001409 RID: 5129 RVA: 0x000AFBF1 File Offset: 0x000ADDF1
	private void Start()
	{
		this.Subtitle.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x000AFC18 File Offset: 0x000ADE18
	private void Update()
	{
		if (!this.GameOver)
		{
			float num = Vector3.Distance(this.Yandere.transform.position, base.transform.position);
			if (num < 6f)
			{
				if (!this.Started)
				{
					this.MyAudio.Play();
					this.Started = true;
				}
				else
				{
					this.MyAudio.pitch = Time.timeScale;
					if (this.SpeechPhase < this.SpeechTime.Length && this.MyAudio.time > this.SpeechTime[this.SpeechPhase])
					{
						this.Subtitle.text = this.SpeechText[this.SpeechPhase];
						this.SpeechPhase++;
					}
					this.Scale = Mathf.Abs(1f - (num - 1f) / 5f);
					if (this.Scale < 0f)
					{
						this.Scale = 0f;
					}
					if (this.Scale > 1f)
					{
						this.Scale = 1f;
					}
					this.Jukebox.volume = 1f - 0.9f * this.Scale;
					this.Subtitle.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
					this.MyAudio.volume = this.Scale;
				}
				for (int i = 0; i < this.Boundary.Length; i++)
				{
					Transform transform = this.Boundary[i];
					if (transform != null)
					{
						float num2 = Vector3.Distance(this.Yandere.transform.position, transform.position);
						Debug.Log(base.gameObject.name + "'s BoundaryDistance is: " + num2);
						if (num2 < 0.33333f)
						{
							Debug.Log("Got a ''proximity'' game over from " + base.gameObject.name);
							AudioSource.PlayClipAtPoint(this.CrunchSound, Camera.main.transform.position);
							this.TransitionToGameOver();
						}
					}
				}
				if (this.YandereIsInFOV())
				{
					if (this.YandereIsInLOS())
					{
						this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * this.NoticeSpeed);
					}
					else
					{
						this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * this.NoticeSpeed);
					}
				}
				else
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * this.NoticeSpeed);
				}
				if (this.Alpha == 1f)
				{
					Debug.Log("Got a ''witnessed'' game over from " + base.gameObject.name);
					AudioSource.PlayClipAtPoint(this.GameOverSound, Camera.main.transform.position);
					this.TransitionToGameOver();
				}
			}
			else if (num < 7f)
			{
				this.Jukebox.volume = 1f;
				this.MyAudio.volume = 0f;
				this.Subtitle.transform.localScale = new Vector3(0f, 0f, 0f);
			}
			this.Marker.Tex.transform.localScale = new Vector3(1f, this.Alpha, 1f);
			this.Marker.Tex.color = new Color(1f, 0f, 0f, this.Alpha);
			return;
		}
		if (this.GameOverPhase == 0)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f && !this.MyAudio.isPlaying)
			{
				this.Subtitle.text = (this.GameOverText ?? "");
				this.MyAudio.clip = this.GameOverLine;
				this.MyAudio.Play();
				this.GameOverPhase++;
				return;
			}
		}
		else if (!this.MyAudio.isPlaying || Input.GetButton("A"))
		{
			this.Heartbroken.SetActive(true);
			this.Subtitle.text = "";
			base.enabled = false;
			this.MyAudio.Stop();
		}
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x000B0054 File Offset: 0x000AE254
	private bool YandereIsInFOV()
	{
		Vector3 to = this.Yandere.transform.position - this.Head.position;
		float num = 90f;
		return Vector3.Angle(this.Head.forward, to) <= num;
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x000B00A0 File Offset: 0x000AE2A0
	private bool YandereIsInLOS()
	{
		Debug.DrawLine(this.Head.position, new Vector3(this.Yandere.transform.position.x, this.YandereHead.position.y, this.Yandere.transform.position.z), Color.red);
		RaycastHit raycastHit;
		if (Physics.Linecast(this.Head.position, new Vector3(this.Yandere.transform.position.x, this.YandereHead.position.y, this.Yandere.transform.position.z), out raycastHit))
		{
			Debug.Log(base.gameObject.name + " shot out a raycast that hit ''" + raycastHit.collider.gameObject.name + "''");
			if (raycastHit.collider.gameObject.layer == 13)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x000B019C File Offset: 0x000AE39C
	private void TransitionToGameOver()
	{
		this.Marker.Tex.transform.localScale = new Vector3(1f, 0f, 1f);
		this.Marker.Tex.color = new Color(1f, 0f, 0f, 0f);
		this.Darkness.material.color = new Color(0f, 0f, 0f, 1f);
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.CanMove = false;
		this.Subtitle.text = "";
		this.GameOver = true;
		this.Jukebox.Stop();
		this.MyAudio.Stop();
		this.Alpha = 0f;
	}

	// Token: 0x04001C2D RID: 7213
	public StalkerYandereScript Yandere;

	// Token: 0x04001C2E RID: 7214
	public DetectionMarkerScript Marker;

	// Token: 0x04001C2F RID: 7215
	public AudioClip GameOverSound;

	// Token: 0x04001C30 RID: 7216
	public AudioClip GameOverLine;

	// Token: 0x04001C31 RID: 7217
	public AudioClip CrunchSound;

	// Token: 0x04001C32 RID: 7218
	public GameObject Heartbroken;

	// Token: 0x04001C33 RID: 7219
	public Transform YandereHead;

	// Token: 0x04001C34 RID: 7220
	public Transform Head;

	// Token: 0x04001C35 RID: 7221
	public AudioSource Jukebox;

	// Token: 0x04001C36 RID: 7222
	public AudioSource MyAudio;

	// Token: 0x04001C37 RID: 7223
	public Renderer Darkness;

	// Token: 0x04001C38 RID: 7224
	public UILabel Subtitle;

	// Token: 0x04001C39 RID: 7225
	public Transform[] Boundary;

	// Token: 0x04001C3A RID: 7226
	public string[] SpeechText;

	// Token: 0x04001C3B RID: 7227
	public float[] SpeechTime;

	// Token: 0x04001C3C RID: 7228
	public string GameOverText;

	// Token: 0x04001C3D RID: 7229
	public float NoticeSpeed;

	// Token: 0x04001C3E RID: 7230
	public float Alpha;

	// Token: 0x04001C3F RID: 7231
	public float Scale;

	// Token: 0x04001C40 RID: 7232
	public float Timer;

	// Token: 0x04001C41 RID: 7233
	public int GameOverPhase;

	// Token: 0x04001C42 RID: 7234
	public int SpeechPhase;

	// Token: 0x04001C43 RID: 7235
	public bool GameOver;

	// Token: 0x04001C44 RID: 7236
	public bool Started;
}
