using System;
using UnityEngine;

// Token: 0x02000404 RID: 1028
public class StruggleBarScript : MonoBehaviour
{
	// Token: 0x06001B2B RID: 6955 RVA: 0x001135B5 File Offset: 0x001117B5
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
		this.ChooseButton();
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x001135D0 File Offset: 0x001117D0
	private void Update()
	{
		if (this.Struggling)
		{
			this.Intensity = Mathf.MoveTowards(this.Intensity, 1f, Time.deltaTime);
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.Spikes.localEulerAngles = new Vector3(this.Spikes.localEulerAngles.x, this.Spikes.localEulerAngles.y, this.Spikes.localEulerAngles.z - Time.deltaTime * 360f);
			this.Victory -= Time.deltaTime * 10f * this.Strength * this.Intensity;
			if (this.Yandere.Club == ClubType.MartialArts)
			{
				this.Victory = 100f;
			}
			if (Input.GetButtonDown(this.CurrentButton))
			{
				if (this.Invincible)
				{
					this.Victory += 100f;
				}
				this.Victory += Time.deltaTime * (500f + (float)(ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus) * 150f) * this.Intensity;
			}
			if (this.Victory >= 100f)
			{
				this.Victory = 100f;
			}
			if (this.Victory <= -100f)
			{
				this.Victory = -100f;
			}
			UISprite uisprite = this.ButtonPrompts[this.ButtonID];
			uisprite.transform.localPosition = new Vector3(Mathf.Lerp(uisprite.transform.localPosition.x, this.Victory * 6.5f, Time.deltaTime * 10f), uisprite.transform.localPosition.y, uisprite.transform.localPosition.z);
			this.Spikes.localPosition = new Vector3(uisprite.transform.localPosition.x, this.Spikes.localPosition.y, this.Spikes.localPosition.z);
			if (this.Victory == 100f)
			{
				Debug.Log("Yandere-chan just won a struggle against " + this.Student.Name + ".");
				this.Yandere.Won = true;
				this.Student.Lost = true;
				this.Struggling = false;
				this.Victory = 0f;
				return;
			}
			if (this.Victory == -100f)
			{
				if (!this.Invincible)
				{
					this.HeroWins();
					return;
				}
			}
			else
			{
				this.ButtonTimer += Time.deltaTime;
				if (this.ButtonTimer >= 1f)
				{
					this.ChooseButton();
					this.ButtonTimer = 0f;
					this.Intensity = 0f;
					return;
				}
			}
		}
		else
		{
			if (base.transform.localScale.x > 0.1f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
				return;
			}
			base.transform.localScale = Vector3.zero;
			if (!this.Yandere.AttackManager.Censor)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (this.AttackTimer == 0f)
			{
				this.Yandere.Blur.enabled = true;
				this.Yandere.Blur.blurSize = 0f;
				this.Yandere.Blur.blurIterations = 0;
			}
			this.AttackTimer += Time.deltaTime;
			if (this.AttackTimer < 2.5f)
			{
				this.Yandere.Blur.blurSize = Mathf.MoveTowards(this.Yandere.Blur.blurSize, 10f, Time.deltaTime * 10f);
				if (this.Yandere.Blur.blurSize > (float)this.Yandere.Blur.blurIterations)
				{
					this.Yandere.Blur.blurIterations++;
					return;
				}
			}
			else
			{
				this.Yandere.Blur.blurSize = Mathf.Lerp(this.Yandere.Blur.blurSize, 0f, Time.deltaTime * 10f);
				if (this.Yandere.Blur.blurSize < (float)this.Yandere.Blur.blurIterations)
				{
					this.Yandere.Blur.blurIterations--;
				}
				if (this.AttackTimer >= 3f)
				{
					base.gameObject.SetActive(false);
					this.Yandere.Blur.enabled = false;
					this.Yandere.Blur.blurSize = 0f;
					this.Yandere.Blur.blurIterations = 0;
					this.AttackTimer = 0f;
				}
			}
		}
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x00113AC4 File Offset: 0x00111CC4
	public void HeroWins()
	{
		if (this.Yandere.Armed)
		{
			this.Yandere.EquippedWeapon.Drop();
		}
		this.Yandere.Lost = true;
		this.Student.Won = true;
		this.Struggling = false;
		this.Victory = 0f;
		this.Yandere.StudentManager.StopMoving();
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x00113B28 File Offset: 0x00111D28
	private void ChooseButton()
	{
		int buttonID = this.ButtonID;
		for (int i = 1; i < 5; i++)
		{
			this.ButtonPrompts[i].enabled = false;
			this.ButtonPrompts[i].transform.localPosition = this.ButtonPrompts[buttonID].transform.localPosition;
		}
		while (this.ButtonID == buttonID)
		{
			this.ButtonID = UnityEngine.Random.Range(1, 5);
		}
		if (this.ButtonID == 1)
		{
			this.CurrentButton = "A";
		}
		else if (this.ButtonID == 2)
		{
			this.CurrentButton = "B";
		}
		else if (this.ButtonID == 3)
		{
			this.CurrentButton = "X";
		}
		else if (this.ButtonID == 4)
		{
			this.CurrentButton = "Y";
		}
		this.ButtonPrompts[this.ButtonID].enabled = true;
	}

	// Token: 0x04002C82 RID: 11394
	public ShoulderCameraScript ShoulderCamera;

	// Token: 0x04002C83 RID: 11395
	public PromptSwapScript ButtonPrompt;

	// Token: 0x04002C84 RID: 11396
	public UISprite[] ButtonPrompts;

	// Token: 0x04002C85 RID: 11397
	public YandereScript Yandere;

	// Token: 0x04002C86 RID: 11398
	public StudentScript Student;

	// Token: 0x04002C87 RID: 11399
	public Transform Spikes;

	// Token: 0x04002C88 RID: 11400
	public string CurrentButton = string.Empty;

	// Token: 0x04002C89 RID: 11401
	public bool Struggling;

	// Token: 0x04002C8A RID: 11402
	public bool Invincible;

	// Token: 0x04002C8B RID: 11403
	public float AttackTimer;

	// Token: 0x04002C8C RID: 11404
	public float ButtonTimer;

	// Token: 0x04002C8D RID: 11405
	public float Intensity;

	// Token: 0x04002C8E RID: 11406
	public float Strength = 1f;

	// Token: 0x04002C8F RID: 11407
	public float Struggle;

	// Token: 0x04002C90 RID: 11408
	public float Victory;

	// Token: 0x04002C91 RID: 11409
	public int ButtonID;
}
