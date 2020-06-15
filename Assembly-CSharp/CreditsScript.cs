using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000251 RID: 593
public class CreditsScript : MonoBehaviour
{
	// Token: 0x17000345 RID: 837
	// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00096BE1 File Offset: 0x00094DE1
	private bool ShouldStopCredits
	{
		get
		{
			return this.ID == this.JSON.Credits.Length;
		}
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x00096BF8 File Offset: 0x00094DF8
	private GameObject SpawnLabel(int size)
	{
		return UnityEngine.Object.Instantiate<GameObject>((size == 1) ? this.SmallCreditsLabel : this.BigCreditsLabel, this.SpawnPoint.position, Quaternion.identity);
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x00096C24 File Offset: 0x00094E24
	private void Start()
	{
		if (DateGlobals.Weekday == DayOfWeek.Sunday || GameGlobals.DarkEnding)
		{
			GameGlobals.DarkEnding = false;
			this.Jukebox.clip = this.DarkCreditsMusic;
			this.Darkness.color = new Color(0f, 0f, 0f, 0f);
			this.Speed = 1.1f;
		}
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x00096C88 File Offset: 0x00094E88
	private void Update()
	{
		if (!this.Begin)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Begin = true;
				this.Jukebox.Play();
				this.Timer = 0f;
			}
		}
		else
		{
			if (!this.ShouldStopCredits)
			{
				if (this.Timer == 0f)
				{
					CreditJson creditJson = this.JSON.Credits[this.ID];
					GameObject gameObject = this.SpawnLabel(creditJson.Size);
					this.TimerLimit = (float)creditJson.Size * this.SpeedUpFactor;
					gameObject.transform.parent = this.Panel;
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					gameObject.GetComponent<UILabel>().text = creditJson.Name;
					this.ID++;
				}
				this.Timer += Time.deltaTime * this.Speed;
				if (this.Timer >= this.TimerLimit)
				{
					this.Timer = 0f;
				}
			}
			if (Input.GetButtonDown("B") || !this.Jukebox.isPlaying)
			{
				this.FadeOut = true;
			}
		}
		if (this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			this.Jukebox.volume -= Time.deltaTime;
			if (this.Darkness.color.a == 1f)
			{
				if (this.Darkness.color.r == 1f)
				{
					SceneManager.LoadScene("TitleScene");
				}
				else
				{
					SceneManager.LoadScene("PostCreditsScene");
				}
			}
		}
		bool keyDown = Input.GetKeyDown(KeyCode.Minus);
		bool keyDown2 = Input.GetKeyDown(KeyCode.Equals);
		if (keyDown)
		{
			Time.timeScale -= 1f;
		}
		else if (keyDown2)
		{
			Time.timeScale += 1f;
		}
		if (keyDown || keyDown2)
		{
			this.Jukebox.pitch = Time.timeScale;
		}
	}

	// Token: 0x04001874 RID: 6260
	[SerializeField]
	private JsonScript JSON;

	// Token: 0x04001875 RID: 6261
	[SerializeField]
	private Transform SpawnPoint;

	// Token: 0x04001876 RID: 6262
	[SerializeField]
	private Transform Panel;

	// Token: 0x04001877 RID: 6263
	[SerializeField]
	private GameObject SmallCreditsLabel;

	// Token: 0x04001878 RID: 6264
	[SerializeField]
	private GameObject BigCreditsLabel;

	// Token: 0x04001879 RID: 6265
	[SerializeField]
	private UISprite Darkness;

	// Token: 0x0400187A RID: 6266
	[SerializeField]
	private int ID;

	// Token: 0x0400187B RID: 6267
	[SerializeField]
	private float SpeedUpFactor;

	// Token: 0x0400187C RID: 6268
	[SerializeField]
	private float TimerLimit;

	// Token: 0x0400187D RID: 6269
	[SerializeField]
	private float FadeTimer;

	// Token: 0x0400187E RID: 6270
	[SerializeField]
	private float Speed = 1f;

	// Token: 0x0400187F RID: 6271
	[SerializeField]
	private float Timer;

	// Token: 0x04001880 RID: 6272
	[SerializeField]
	private bool FadeOut;

	// Token: 0x04001881 RID: 6273
	[SerializeField]
	private bool Begin;

	// Token: 0x04001882 RID: 6274
	private const int SmallTextSize = 1;

	// Token: 0x04001883 RID: 6275
	private const int BigTextSize = 2;

	// Token: 0x04001884 RID: 6276
	public AudioClip DarkCreditsMusic;

	// Token: 0x04001885 RID: 6277
	public AudioSource Jukebox;
}
