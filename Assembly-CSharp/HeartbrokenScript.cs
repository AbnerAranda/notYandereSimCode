using System;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x020002E3 RID: 739
public class HeartbrokenScript : MonoBehaviour
{
	// Token: 0x0600170D RID: 5901 RVA: 0x000C1580 File Offset: 0x000BF780
	private void Start()
	{
		if (GameGlobals.MostRecentSlot == 0)
		{
			this.Options[2].color = new Color(this.Options[2].color.r * 0.5f, this.Options[2].color.g * 0.5f, this.Options[2].color.b * 0.5f, 1f);
		}
		if (!this.Caught && !this.Noticed && this.Yandere.Bloodiness > 0f && !this.Yandere.RedPaint && !this.Yandere.Unmasked)
		{
			this.Arrested = true;
		}
		if (this.Caught)
		{
			this.Letters[0].text = "";
			this.Letters[1].text = "";
			this.Letters[2].text = "C";
			this.Letters[3].text = "A";
			this.Letters[4].text = "U";
			this.Letters[5].text = "G";
			this.Letters[6].text = "H";
			this.Letters[7].text = "T";
			this.Letters[8].text = "";
			this.Letters[9].text = "";
			this.Letters[10].text = "";
			foreach (UILabel uilabel in this.Letters)
			{
				uilabel.transform.localPosition = new Vector3(uilabel.transform.localPosition.x + 125f, uilabel.transform.localPosition.y, uilabel.transform.localPosition.z);
			}
			this.LetterID = 1;
			this.StopID = 8;
			this.NoSnap = true;
			this.SNAP.SetActive(false);
			this.Cursor.Options = 4;
		}
		else if (this.Confessed)
		{
			this.Letters[0].text = "S";
			this.Letters[1].text = "E";
			this.Letters[2].text = "N";
			this.Letters[3].text = "P";
			this.Letters[4].text = "A";
			this.Letters[5].text = "I";
			this.Letters[6].text = string.Empty;
			this.Letters[7].text = "L";
			this.Letters[8].text = "O";
			this.Letters[9].text = "S";
			this.Letters[10].text = "T";
			this.LetterID = 0;
			this.StopID = 11;
			this.NoSnap = true;
		}
		else if (this.Yandere.Attacked)
		{
			if (!this.Headmaster)
			{
				this.Letters[0].text = string.Empty;
				this.Letters[1].text = "C";
				this.Letters[2].text = "O";
				this.Letters[3].text = "M";
				this.Letters[4].text = "A";
				this.Letters[5].text = "T";
				this.Letters[6].text = "O";
				this.Letters[7].text = "S";
				this.Letters[8].text = "E";
				this.Letters[9].text = string.Empty;
				this.Letters[10].text = string.Empty;
				this.Letters[3].fontSize = 250;
				this.LetterID = 1;
				this.StopID = 9;
			}
			else
			{
				this.Letters[0].text = "?";
				this.Letters[1].text = "?";
				this.Letters[2].text = "?";
				this.Letters[3].text = "?";
				this.Letters[4].text = "?";
				this.Letters[5].text = "?";
				this.Letters[6].text = "?";
				this.Letters[7].text = "?";
				this.Letters[8].text = "?";
				this.Letters[9].text = "?";
				this.Letters[10].text = string.Empty;
				this.LetterID = 0;
				this.StopID = 10;
			}
			foreach (UILabel uilabel2 in this.Letters)
			{
				uilabel2.transform.localPosition = new Vector3(uilabel2.transform.localPosition.x + 100f, uilabel2.transform.localPosition.y, uilabel2.transform.localPosition.z);
			}
			this.SNAP.SetActive(false);
			this.Cursor.Options = 4;
			this.NoSnap = true;
		}
		else if (this.Yandere.Lost || this.ShoulderCamera.LookDown || this.ShoulderCamera.Counter || this.ShoulderCamera.ObstacleCounter)
		{
			this.Letters[0].text = "A";
			this.Letters[1].text = "P";
			this.Letters[2].text = "P";
			this.Letters[3].text = "R";
			this.Letters[4].text = "E";
			this.Letters[5].text = "H";
			this.Letters[6].text = "E";
			this.Letters[7].text = "N";
			this.Letters[8].text = "D";
			this.Letters[9].text = "E";
			this.Letters[10].text = "D";
			this.LetterID = 0;
			this.StopID = 11;
			this.NoSnap = true;
		}
		else if (this.Exposed)
		{
			this.Letters[0].text = string.Empty;
			this.Letters[1].text = string.Empty;
			this.Letters[2].text = "E";
			this.Letters[3].text = "X";
			this.Letters[4].text = "P";
			this.Letters[5].text = "O";
			this.Letters[6].text = "S";
			this.Letters[7].text = "E";
			this.Letters[8].text = "D";
			this.Letters[9].text = string.Empty;
			this.Letters[10].text = string.Empty;
			foreach (UILabel uilabel3 in this.Letters)
			{
				uilabel3.transform.localPosition = new Vector3(uilabel3.transform.localPosition.x + 100f, uilabel3.transform.localPosition.y, uilabel3.transform.localPosition.z);
			}
			this.LetterID = 1;
			this.StopID = 9;
			this.NoSnap = true;
		}
		else if (this.Arrested)
		{
			this.Letters[0].text = string.Empty;
			this.Letters[1].text = "A";
			this.Letters[2].text = "R";
			this.Letters[3].text = "R";
			this.Letters[4].text = "E";
			this.Letters[5].text = "S";
			this.Letters[6].text = "T";
			this.Letters[7].text = "E";
			this.Letters[8].text = "D";
			this.Letters[9].text = string.Empty;
			this.Letters[10].text = string.Empty;
			foreach (UILabel uilabel4 in this.Letters)
			{
				uilabel4.transform.localPosition = new Vector3(uilabel4.transform.localPosition.x + 100f, uilabel4.transform.localPosition.y, uilabel4.transform.localPosition.z);
			}
			this.LetterID = 1;
			this.StopID = 9;
			this.NoSnap = true;
		}
		else if (this.Counselor.Expelled || this.Yandere.Sprayed)
		{
			this.Letters[0].text = string.Empty;
			this.Letters[1].text = "E";
			this.Letters[2].text = "X";
			this.Letters[3].text = "P";
			this.Letters[4].text = "E";
			this.Letters[5].text = "L";
			this.Letters[6].text = "L";
			this.Letters[7].text = "E";
			this.Letters[8].text = "D";
			this.Letters[9].text = string.Empty;
			this.Letters[10].text = string.Empty;
			foreach (UILabel uilabel5 in this.Letters)
			{
				uilabel5.transform.localPosition = new Vector3(uilabel5.transform.localPosition.x + 100f, uilabel5.transform.localPosition.y, uilabel5.transform.localPosition.z);
			}
			this.LetterID = 1;
			this.StopID = 9;
			this.NoSnap = true;
		}
		else
		{
			this.LetterID = 0;
			this.StopID = 11;
		}
		this.ID = 0;
		while (this.ID < this.Letters.Length)
		{
			UILabel uilabel6 = this.Letters[this.ID];
			uilabel6.transform.localScale = new Vector3(10f, 10f, 1f);
			uilabel6.color = new Color(uilabel6.color.r, uilabel6.color.g, uilabel6.color.b, 0f);
			this.Origins[this.ID] = uilabel6.transform.localPosition;
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.Options.Length)
		{
			UILabel uilabel7 = this.Options[this.ID];
			uilabel7.color = new Color(uilabel7.color.r, uilabel7.color.g, uilabel7.color.b, 0f);
			this.ID++;
		}
		this.ID = 0;
		this.Subtitle.color = new Color(this.Subtitle.color.r, this.Subtitle.color.g, this.Subtitle.color.b, 0f);
		if (this.Noticed)
		{
			this.Background.color = new Color(this.Background.color.r, this.Background.color.g, this.Background.color.b, 0f);
			this.Ground.color = new Color(this.Ground.color.r, this.Ground.color.g, this.Ground.color.b, 0f);
		}
		else
		{
			base.transform.parent.transform.position = new Vector3(base.transform.parent.transform.position.x, 100f, base.transform.parent.transform.position.z);
		}
		if (this.Cursor.SnappedYandere != null)
		{
			int num = 0;
			WeaponScript[] weapons = this.Cursor.SnappedYandere.Weapons;
			for (int i = 0; i < weapons.Length; i++)
			{
				if (weapons[i] != null)
				{
					num++;
				}
			}
			if (num == 0 || this.NoSnap || this.Yandere.Police.GameOver || this.Yandere.StudentManager.Clock.HourTime >= 18f || this.Yandere.transform.position.y < -1f)
			{
				this.SNAP.SetActive(false);
				this.Cursor.Options = 4;
			}
			this.Clock.StopTime = true;
		}
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x000C2330 File Offset: 0x000C0530
	private void Update()
	{
		if (Input.GetKeyDown("m"))
		{
			base.gameObject.GetComponent<AudioSource>().Stop();
		}
		this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0f, Time.deltaTime);
		if (this.VibrationTimer == 0f)
		{
			GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
		}
		if (this.Noticed)
		{
			this.Ground.transform.eulerAngles = new Vector3(90f, 0f, 0f);
			this.Ground.transform.position = new Vector3(this.Ground.transform.position.x, this.Yandere.transform.position.y, this.Ground.transform.position.z);
		}
		this.Timer += Time.deltaTime;
		if (this.Timer > 3f)
		{
			if (this.Phase == 1)
			{
				if (this.Noticed)
				{
					this.UpdateSubtitle();
				}
				this.Phase += ((this.Subtitle.color.a > 0f) ? 1 : 2);
			}
			else if (this.Phase == 2)
			{
				if (Input.GetButtonDown("A"))
				{
					this.AudioTimer = 100f;
				}
				this.AudioTimer += Time.deltaTime;
				if (this.AudioTimer > this.Subtitle.GetComponent<AudioSource>().clip.length)
				{
					this.Phase++;
				}
			}
		}
		else if (this.Yandere != null && this.Yandere.Unmasked)
		{
			this.Yandere.ShoulderCamera.transform.position = Vector3.Lerp(this.Yandere.ShoulderCamera.transform.position, this.Yandere.ShoulderCamera.NoticedPOV.position, Time.deltaTime * 1f);
			this.Yandere.ShoulderCamera.transform.LookAt(this.Yandere.ShoulderCamera.NoticedFocus);
			if (Vector3.Distance(this.Yandere.transform.position, this.Yandere.Senpai.position) < 1.25f)
			{
				this.Yandere.MyController.Move(this.Yandere.transform.forward * (Time.deltaTime * -1f));
			}
			if (this.Yandere.CharacterAnimation["f02_down_22"].time >= this.Yandere.CharacterAnimation["f02_down_22"].length)
			{
				this.Yandere.CharacterAnimation.CrossFade("f02_down_23");
			}
		}
		if (this.Background.color.a < 1f)
		{
			this.Background.color = new Color(this.Background.color.r, this.Background.color.g, this.Background.color.b, this.Background.color.a + Time.deltaTime);
			this.Ground.color = new Color(this.Ground.color.r, this.Ground.color.g, this.Ground.color.b, this.Ground.color.a + Time.deltaTime);
			if (this.Background.color.a >= 1f)
			{
				this.MainCamera.enabled = false;
			}
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.LetterID < this.StopID)
		{
			UILabel uilabel = this.Letters[this.LetterID];
			uilabel.transform.localScale = Vector3.MoveTowards(uilabel.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 100f);
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, uilabel.color.a + Time.deltaTime * 10f);
			if (uilabel.transform.localScale == new Vector3(1f, 1f, 1f))
			{
				component.PlayOneShot(this.Slam);
				GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
				this.VibrationTimer = 0.1f;
				this.LetterID++;
				if (this.LetterID == this.StopID)
				{
					this.ID = 0;
				}
			}
		}
		else if (this.Phase == 3)
		{
			if (this.Options[0].color.a == 0f)
			{
				this.Subtitle.color = new Color(this.Subtitle.color.r, this.Subtitle.color.g, this.Subtitle.color.b, 0f);
				component.Play();
			}
			if (this.ID < this.Options.Length)
			{
				UILabel uilabel2 = this.Options[this.ID];
				uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, uilabel2.color.a + Time.deltaTime * 5f);
				if (uilabel2.color.a >= 1f)
				{
					this.ID++;
				}
			}
		}
		if (!this.Freeze)
		{
			this.ShakeID = 0;
			while (this.ShakeID < this.Letters.Length)
			{
				UILabel uilabel3 = this.Letters[this.ShakeID];
				Vector3 vector = this.Origins[this.ShakeID];
				uilabel3.transform.localPosition = new Vector3(vector.x + UnityEngine.Random.Range(-5f, 5f), vector.y + UnityEngine.Random.Range(-5f, 5f), uilabel3.transform.localPosition.z);
				this.ShakeID++;
			}
		}
		this.GrowID = 0;
		while (this.GrowID < 4)
		{
			UILabel uilabel4 = this.Options[this.GrowID];
			uilabel4.transform.localScale = Vector3.Lerp(uilabel4.transform.localScale, (this.Cursor.Selected - 1 != this.GrowID) ? new Vector3(0.5f, 0.5f, 0.5f) : new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.GrowID++;
		}
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x000C2A54 File Offset: 0x000C0C54
	private void UpdateSubtitle()
	{
		StudentScript component = this.Yandere.Senpai.GetComponent<StudentScript>();
		if (!component.Teacher && this.Yandere.Noticed)
		{
			this.Subtitle.color = new Color(this.Subtitle.color.r, this.Subtitle.color.g, this.Subtitle.color.b, 1f);
			GameOverType gameOverCause = component.GameOverCause;
			int num = 0;
			if (gameOverCause == GameOverType.Stalking)
			{
				num = 4;
			}
			else if (gameOverCause == GameOverType.Insanity)
			{
				num = 3;
			}
			else if (gameOverCause == GameOverType.Weapon)
			{
				num = 1;
			}
			else if (gameOverCause == GameOverType.Murder)
			{
				num = 5;
			}
			else if (gameOverCause == GameOverType.Blood)
			{
				num = 2;
			}
			else if (gameOverCause == GameOverType.Lewd)
			{
				num = 6;
			}
			this.Subtitle.text = this.NoticedLines[num];
			this.Subtitle.GetComponent<AudioSource>().clip = this.NoticedClips[num];
			this.Subtitle.GetComponent<AudioSource>().Play();
			return;
		}
		if (this.Headmaster)
		{
			this.Subtitle.color = new Color(this.Subtitle.color.r, this.Subtitle.color.g, this.Subtitle.color.b, 1f);
			this.Subtitle.text = this.NoticedLines[8];
			this.Subtitle.GetComponent<AudioSource>().clip = this.NoticedClips[8];
			this.Subtitle.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x000C2BD0 File Offset: 0x000C0DD0
	public void Darken()
	{
		for (int i = 0; i < this.Letters.Length; i++)
		{
			if (this.Letters[i].color.a > 1f)
			{
				this.Letters[i].color = new Color(1f, 0f, 0f, 1f);
			}
			this.Letters[i].color = new Color(1f, 0f, 0f, this.Letters[i].color.a - 0.05882353f);
		}
		for (int i = 0; i < 4; i++)
		{
			if (this.Options[i].color.a > 1f)
			{
				this.Options[i].color = new Color(this.Options[i].color.r, this.Options[i].color.g, this.Options[i].color.b, 1f);
			}
			this.Options[i].color = new Color(this.Options[i].color.r, this.Options[i].color.g, this.Options[i].color.b, this.Options[i].color.a - 0.05882353f);
		}
	}

	// Token: 0x04001EF4 RID: 7924
	public ShoulderCameraScript ShoulderCamera;

	// Token: 0x04001EF5 RID: 7925
	public HeartbrokenCursorScript Cursor;

	// Token: 0x04001EF6 RID: 7926
	public CounselorScript Counselor;

	// Token: 0x04001EF7 RID: 7927
	public YandereScript Yandere;

	// Token: 0x04001EF8 RID: 7928
	public ClockScript Clock;

	// Token: 0x04001EF9 RID: 7929
	public AudioListener Listener;

	// Token: 0x04001EFA RID: 7930
	public AudioClip[] NoticedClips;

	// Token: 0x04001EFB RID: 7931
	public string[] NoticedLines;

	// Token: 0x04001EFC RID: 7932
	public UILabel[] Letters;

	// Token: 0x04001EFD RID: 7933
	public UILabel[] Options;

	// Token: 0x04001EFE RID: 7934
	public Vector3[] Origins;

	// Token: 0x04001EFF RID: 7935
	public UISprite Background;

	// Token: 0x04001F00 RID: 7936
	public UISprite Ground;

	// Token: 0x04001F01 RID: 7937
	public Camera MainCamera;

	// Token: 0x04001F02 RID: 7938
	public UILabel Subtitle;

	// Token: 0x04001F03 RID: 7939
	public GameObject SNAP;

	// Token: 0x04001F04 RID: 7940
	public AudioClip Slam;

	// Token: 0x04001F05 RID: 7941
	public bool Headmaster;

	// Token: 0x04001F06 RID: 7942
	public bool Confessed;

	// Token: 0x04001F07 RID: 7943
	public bool Arrested;

	// Token: 0x04001F08 RID: 7944
	public bool Exposed;

	// Token: 0x04001F09 RID: 7945
	public bool Noticed = true;

	// Token: 0x04001F0A RID: 7946
	public bool Freeze;

	// Token: 0x04001F0B RID: 7947
	public bool NoSnap;

	// Token: 0x04001F0C RID: 7948
	public bool Caught;

	// Token: 0x04001F0D RID: 7949
	public float VibrationTimer;

	// Token: 0x04001F0E RID: 7950
	public float AudioTimer;

	// Token: 0x04001F0F RID: 7951
	public float Timer;

	// Token: 0x04001F10 RID: 7952
	public int Phase = 1;

	// Token: 0x04001F11 RID: 7953
	public int LetterID;

	// Token: 0x04001F12 RID: 7954
	public int ShakeID;

	// Token: 0x04001F13 RID: 7955
	public int GrowID;

	// Token: 0x04001F14 RID: 7956
	public int StopID;

	// Token: 0x04001F15 RID: 7957
	public int ID;
}
