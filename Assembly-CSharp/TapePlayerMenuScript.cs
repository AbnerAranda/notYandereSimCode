using System;
using UnityEngine;

// Token: 0x02000419 RID: 1049
public class TapePlayerMenuScript : MonoBehaviour
{
	// Token: 0x06001C24 RID: 7204 RVA: 0x001502F8 File Offset: 0x0014E4F8
	private void Start()
	{
		this.List.transform.localPosition = new Vector3(-955f, this.List.transform.localPosition.y, this.List.transform.localPosition.z);
		this.TimeBar.localPosition = new Vector3(this.TimeBar.localPosition.x, 100f, this.TimeBar.localPosition.z);
		this.Subtitle.text = string.Empty;
		this.TapePlayerCamera.position = new Vector3(-26.15f, this.TapePlayerCamera.position.y, 5.35f);
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x001503B8 File Offset: 0x0014E5B8
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		float t = Time.unscaledDeltaTime * 10f;
		if (this.Show)
		{
			if (this.Listening)
			{
				this.List.localPosition = new Vector3(Mathf.Lerp(this.List.localPosition.x, -955f, t), this.List.localPosition.y, this.List.localPosition.z);
				this.TimeBar.localPosition = new Vector3(this.TimeBar.localPosition.x, Mathf.Lerp(this.TimeBar.localPosition.y, 0f, t), this.TimeBar.localPosition.z);
				this.TapePlayerCamera.position = new Vector3(Mathf.Lerp(this.TapePlayerCamera.position.x, -26.15f, t), this.TapePlayerCamera.position.y, Mathf.Lerp(this.TapePlayerCamera.position.z, 5.35f, t));
				if (this.Phase == 1)
				{
					this.TapePlayer.GetComponent<Animation>()["InsertTape"].time += 0.0555555f;
					if (this.TapePlayer.GetComponent<Animation>()["InsertTape"].time >= this.TapePlayer.GetComponent<Animation>()["InsertTape"].length)
					{
						this.TapePlayer.GetComponent<Animation>().Play("PressPlay");
						component.Play();
						this.PromptBar.Label[0].text = "PAUSE";
						this.PromptBar.Label[1].text = "STOP";
						this.PromptBar.Label[5].text = "REWIND / FAST FORWARD";
						this.PromptBar.UpdateButtons();
						this.Phase++;
					}
				}
				else if (this.Phase == 2)
				{
					this.Timer += 0.0166666675f;
					if (component.isPlaying)
					{
						if ((double)this.Timer > 0.1)
						{
							this.TapePlayer.GetComponent<Animation>()["PressPlay"].time += 0.0166666675f;
							if (this.TapePlayer.GetComponent<Animation>()["PressPlay"].time > this.TapePlayer.GetComponent<Animation>()["PressPlay"].length)
							{
								this.TapePlayer.GetComponent<Animation>()["PressPlay"].time = this.TapePlayer.GetComponent<Animation>()["PressPlay"].length;
							}
						}
					}
					else
					{
						this.TapePlayer.GetComponent<Animation>()["PressPlay"].time -= 0.0166666675f;
						if (this.TapePlayer.GetComponent<Animation>()["PressPlay"].time < 0f)
						{
							this.TapePlayer.GetComponent<Animation>()["PressPlay"].time = 0f;
						}
						if (Input.GetButtonDown("A"))
						{
							this.PromptBar.Label[0].text = "PAUSE";
							this.TapePlayer.Spin = true;
							component.time = this.ResumeTime;
							component.Play();
						}
					}
					if (this.TapePlayer.GetComponent<Animation>()["PressPlay"].time >= this.TapePlayer.GetComponent<Animation>()["PressPlay"].length)
					{
						this.TapePlayer.Spin = true;
						if (component.time >= component.clip.length - 1f)
						{
							this.TapePlayer.GetComponent<Animation>().Play("PressEject");
							this.TapePlayer.Spin = false;
							if (!component.isPlaying)
							{
								component.clip = this.TapeStop;
								component.Play();
							}
							this.Subtitle.text = string.Empty;
							this.Phase++;
						}
						if (Input.GetButtonDown("A") && component.isPlaying)
						{
							this.PromptBar.Label[0].text = "PLAY";
							this.TapePlayer.Spin = false;
							this.ResumeTime = component.time;
							component.Stop();
						}
					}
					if (Input.GetButtonDown("B"))
					{
						this.TapePlayer.GetComponent<Animation>().Play("PressEject");
						component.clip = this.TapeStop;
						this.TapePlayer.Spin = false;
						component.Play();
						this.PromptBar.Label[0].text = string.Empty;
						this.PromptBar.Label[1].text = string.Empty;
						this.PromptBar.Label[5].text = string.Empty;
						this.PromptBar.UpdateButtons();
						this.Subtitle.text = string.Empty;
						this.Phase++;
					}
				}
				else if (this.Phase == 3)
				{
					this.TapePlayer.GetComponent<Animation>()["PressEject"].time += 0.0166666675f;
					if (this.TapePlayer.GetComponent<Animation>()["PressEject"].time >= this.TapePlayer.GetComponent<Animation>()["PressEject"].length)
					{
						this.TapePlayer.GetComponent<Animation>().Play("InsertTape");
						this.TapePlayer.GetComponent<Animation>()["InsertTape"].time = this.TapePlayer.GetComponent<Animation>()["InsertTape"].length;
						this.TapePlayer.FastForward = false;
						this.Phase++;
					}
				}
				else if (this.Phase == 4)
				{
					this.TapePlayer.GetComponent<Animation>()["InsertTape"].time -= 0.0555555f;
					if (this.TapePlayer.GetComponent<Animation>()["InsertTape"].time <= 0f)
					{
						this.TapePlayer.Tape.SetActive(false);
						this.Jukebox.SetActive(true);
						this.Listening = false;
						this.Timer = 0f;
						this.PromptBar.Label[0].text = "PLAY";
						this.PromptBar.Label[1].text = "BACK";
						this.PromptBar.Label[4].text = "CHOOSE";
						this.PromptBar.Label[5].text = "CATEGORY";
						this.PromptBar.UpdateButtons();
					}
				}
				if (this.Phase != 2)
				{
					this.Label.text = "00:00 / 00:00";
					this.Bar.fillAmount = 0f;
					return;
				}
				if (this.InputManager.DPadRight || Input.GetKey(KeyCode.RightArrow))
				{
					this.ResumeTime += 1.66666663f;
					component.time += 1.66666663f;
					this.TapePlayer.FastForward = true;
				}
				else
				{
					this.TapePlayer.FastForward = false;
				}
				if (this.InputManager.DPadLeft || Input.GetKey(KeyCode.LeftArrow))
				{
					this.ResumeTime -= 1.66666663f;
					component.time -= 1.66666663f;
					this.TapePlayer.Rewind = true;
				}
				else
				{
					this.TapePlayer.Rewind = false;
				}
				int num;
				int num2;
				if (component.isPlaying)
				{
					num = Mathf.FloorToInt(component.time / 60f);
					num2 = Mathf.FloorToInt(component.time - (float)num * 60f);
					this.Bar.fillAmount = component.time / component.clip.length;
				}
				else
				{
					num = Mathf.FloorToInt(this.ResumeTime / 60f);
					num2 = Mathf.FloorToInt(this.ResumeTime - (float)num * 60f);
					this.Bar.fillAmount = this.ResumeTime / component.clip.length;
				}
				this.CurrentTime = string.Format("{00:00}:{1:00}", num, num2);
				this.Label.text = this.CurrentTime + " / " + this.ClipLength;
				if (this.Category == 1)
				{
					if (this.Selected == 1)
					{
						for (int i = 0; i < this.Cues1.Length; i++)
						{
							if (component.time > this.Cues1[i])
							{
								this.Subtitle.text = this.Subs1[i];
							}
						}
						return;
					}
					if (this.Selected == 2)
					{
						for (int j = 0; j < this.Cues2.Length; j++)
						{
							if (component.time > this.Cues2[j])
							{
								this.Subtitle.text = this.Subs2[j];
							}
						}
						return;
					}
					if (this.Selected == 3)
					{
						for (int k = 0; k < this.Cues3.Length; k++)
						{
							if (component.time > this.Cues3[k])
							{
								this.Subtitle.text = this.Subs3[k];
							}
						}
						return;
					}
					if (this.Selected == 4)
					{
						for (int l = 0; l < this.Cues4.Length; l++)
						{
							if (component.time > this.Cues4[l])
							{
								this.Subtitle.text = this.Subs4[l];
							}
						}
						return;
					}
					if (this.Selected == 5)
					{
						for (int m = 0; m < this.Cues5.Length; m++)
						{
							if (component.time > this.Cues5[m])
							{
								this.Subtitle.text = this.Subs5[m];
							}
						}
						return;
					}
					if (this.Selected == 6)
					{
						for (int n = 0; n < this.Cues6.Length; n++)
						{
							if (component.time > this.Cues6[n])
							{
								this.Subtitle.text = this.Subs6[n];
							}
						}
						return;
					}
					if (this.Selected == 7)
					{
						for (int num3 = 0; num3 < this.Cues7.Length; num3++)
						{
							if (component.time > this.Cues7[num3])
							{
								this.Subtitle.text = this.Subs7[num3];
							}
						}
						return;
					}
					if (this.Selected == 8)
					{
						for (int num4 = 0; num4 < this.Cues8.Length; num4++)
						{
							if (component.time > this.Cues8[num4])
							{
								this.Subtitle.text = this.Subs8[num4];
							}
						}
						return;
					}
					if (this.Selected == 9)
					{
						for (int num5 = 0; num5 < this.Cues9.Length; num5++)
						{
							if (component.time > this.Cues9[num5])
							{
								this.Subtitle.text = this.Subs9[num5];
							}
						}
						return;
					}
					if (this.Selected == 10)
					{
						for (int num6 = 0; num6 < this.Cues10.Length; num6++)
						{
							if (component.time > this.Cues10[num6])
							{
								this.Subtitle.text = this.Subs10[num6];
							}
						}
						return;
					}
				}
				else if (this.Category == 2)
				{
					if (this.Selected == 1)
					{
						for (int num7 = 0; num7 < this.BasementCues1.Length; num7++)
						{
							if (component.time > this.BasementCues1[num7])
							{
								this.Subtitle.text = this.BasementSubs1[num7];
							}
						}
					}
					if (this.Selected == 10)
					{
						for (int num8 = 0; num8 < this.BasementCues10.Length; num8++)
						{
							if (component.time > this.BasementCues10[num8])
							{
								this.Subtitle.text = this.BasementSubs10[num8];
							}
						}
						return;
					}
				}
				else if (this.Category == 3)
				{
					if (this.Selected == 1)
					{
						for (int num9 = 0; num9 < this.HeadmasterCues1.Length; num9++)
						{
							if (component.time > this.HeadmasterCues1[num9])
							{
								this.Subtitle.text = this.HeadmasterSubs1[num9];
							}
						}
						return;
					}
					if (this.Selected == 2)
					{
						for (int num10 = 0; num10 < this.HeadmasterCues2.Length; num10++)
						{
							if (component.time > this.HeadmasterCues2[num10])
							{
								this.Subtitle.text = this.HeadmasterSubs2[num10];
							}
						}
						return;
					}
					if (this.Selected == 6)
					{
						for (int num11 = 0; num11 < this.HeadmasterCues6.Length; num11++)
						{
							if (component.time > this.HeadmasterCues6[num11])
							{
								this.Subtitle.text = this.HeadmasterSubs6[num11];
							}
						}
						return;
					}
					if (this.Selected == 10)
					{
						for (int num12 = 0; num12 < this.HeadmasterCues10.Length; num12++)
						{
							if (component.time > this.HeadmasterCues10[num12])
							{
								this.Subtitle.text = this.HeadmasterSubs10[num12];
							}
						}
						return;
					}
				}
			}
			else
			{
				this.TapePlayerCamera.position = new Vector3(Mathf.Lerp(this.TapePlayerCamera.position.x, -26.2125f, t), this.TapePlayerCamera.position.y, Mathf.Lerp(this.TapePlayerCamera.position.z, 5.4125f, t));
				this.List.transform.localPosition = new Vector3(Mathf.Lerp(this.List.transform.localPosition.x, 0f, t), this.List.transform.localPosition.y, this.List.transform.localPosition.z);
				this.TimeBar.localPosition = new Vector3(this.TimeBar.localPosition.x, Mathf.Lerp(this.TimeBar.localPosition.y, 100f, t), this.TimeBar.localPosition.z);
				if (this.InputManager.TappedRight)
				{
					this.Category++;
					if (this.Category > 3)
					{
						this.Category = 1;
					}
					this.UpdateLabels();
				}
				else if (this.InputManager.TappedLeft)
				{
					this.Category--;
					if (this.Category < 1)
					{
						this.Category = 3;
					}
					this.UpdateLabels();
				}
				if (this.InputManager.TappedUp)
				{
					this.Selected--;
					if (this.Selected < 1)
					{
						this.Selected = 10;
					}
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 440f - 80f * (float)this.Selected, this.Highlight.localPosition.z);
					this.CheckSelection();
					return;
				}
				if (this.InputManager.TappedDown)
				{
					this.Selected++;
					if (this.Selected > 10)
					{
						this.Selected = 1;
					}
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 440f - 80f * (float)this.Selected, this.Highlight.localPosition.z);
					this.CheckSelection();
					return;
				}
				if (Input.GetButtonDown("A"))
				{
					bool flag = false;
					if (this.Category == 1)
					{
						if (CollectibleGlobals.GetTapeCollected(this.Selected))
						{
							CollectibleGlobals.SetTapeListened(this.Selected, true);
							flag = true;
						}
					}
					else if (this.Category == 2)
					{
						if (CollectibleGlobals.GetBasementTapeCollected(this.Selected))
						{
							CollectibleGlobals.SetBasementTapeListened(this.Selected, true);
							flag = true;
						}
					}
					else if (this.Category == 3 && CollectibleGlobals.GetHeadmasterTapeCollected(this.Selected))
					{
						CollectibleGlobals.SetHeadmasterTapeListened(this.Selected, true);
						flag = true;
					}
					if (flag)
					{
						this.NewIcons[this.Selected].SetActive(false);
						this.Jukebox.SetActive(false);
						this.Listening = true;
						this.Phase = 1;
						this.PromptBar.Label[0].text = string.Empty;
						this.PromptBar.Label[1].text = string.Empty;
						this.PromptBar.Label[4].text = string.Empty;
						this.PromptBar.UpdateButtons();
						this.TapePlayer.GetComponent<Animation>().Play("InsertTape");
						this.TapePlayer.Tape.SetActive(true);
						if (this.Category == 1)
						{
							component.clip = this.Recordings[this.Selected];
						}
						else if (this.Category == 2)
						{
							component.clip = this.BasementRecordings[this.Selected];
						}
						else
						{
							component.clip = this.HeadmasterRecordings[this.Selected];
						}
						component.time = 0f;
						this.RoundedTime = (float)Mathf.CeilToInt(component.clip.length);
						int num13 = (int)(this.RoundedTime / 60f);
						int num14 = (int)(this.RoundedTime % 60f);
						this.ClipLength = string.Format("{0:00}:{1:00}", num13, num14);
						return;
					}
				}
				else if (Input.GetButtonDown("B"))
				{
					this.TapePlayer.Yandere.HeartCamera.enabled = true;
					this.TapePlayer.Yandere.RPGCamera.enabled = true;
					this.TapePlayer.TapePlayerCamera.enabled = false;
					this.TapePlayer.NoteWindow.SetActive(true);
					this.TapePlayer.PromptBar.ClearButtons();
					this.TapePlayer.Yandere.CanMove = true;
					this.TapePlayer.PromptBar.Show = false;
					this.TapePlayer.Prompt.enabled = true;
					this.TapePlayer.Yandere.HUD.alpha = 1f;
					Time.timeScale = 1f;
					this.Show = false;
				}
			}
			return;
		}
		if (this.List.localPosition.x > -955f)
		{
			this.List.localPosition = new Vector3(Mathf.Lerp(this.List.localPosition.x, -956f, t), this.List.localPosition.y, this.List.localPosition.z);
			this.TimeBar.localPosition = new Vector3(this.TimeBar.localPosition.x, Mathf.Lerp(this.TimeBar.localPosition.y, 100f, t), this.TimeBar.localPosition.z);
			return;
		}
		this.TimeBar.gameObject.SetActive(false);
		this.List.gameObject.SetActive(false);
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x001516C4 File Offset: 0x0014F8C4
	public void UpdateLabels()
	{
		int i = 0;
		while (i < this.TotalTapes)
		{
			i++;
			if (this.Category == 1)
			{
				this.HeaderLabel.text = "Mysterious Tapes";
				if (CollectibleGlobals.GetTapeCollected(i))
				{
					this.TapeLabels[i].text = "Mysterious Tape " + i.ToString();
					this.NewIcons[i].SetActive(!CollectibleGlobals.GetTapeListened(i));
				}
				else
				{
					this.TapeLabels[i].text = "?????";
					this.NewIcons[i].SetActive(false);
				}
			}
			else if (this.Category == 2)
			{
				this.HeaderLabel.text = "Basement Tapes";
				if (CollectibleGlobals.GetBasementTapeCollected(i))
				{
					this.TapeLabels[i].text = "Basement Tape " + i.ToString();
					this.NewIcons[i].SetActive(!CollectibleGlobals.GetBasementTapeListened(i));
				}
				else
				{
					this.TapeLabels[i].text = "?????";
					this.NewIcons[i].SetActive(false);
				}
			}
			else
			{
				this.HeaderLabel.text = "Headmaster Tapes";
				if (CollectibleGlobals.GetHeadmasterTapeCollected(i))
				{
					this.TapeLabels[i].text = "Headmaster Tape " + i.ToString();
					this.NewIcons[i].SetActive(!CollectibleGlobals.GetHeadmasterTapeListened(i));
				}
				else
				{
					this.TapeLabels[i].text = "?????";
					this.NewIcons[i].SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x00151854 File Offset: 0x0014FA54
	public void CheckSelection()
	{
		if (this.Category == 1)
		{
			this.TapePlayer.PromptBar.Label[0].text = (CollectibleGlobals.GetTapeCollected(this.Selected) ? "PLAY" : string.Empty);
			this.TapePlayer.PromptBar.UpdateButtons();
			return;
		}
		if (this.Category == 2)
		{
			this.TapePlayer.PromptBar.Label[0].text = (CollectibleGlobals.GetBasementTapeCollected(this.Selected) ? "PLAY" : string.Empty);
			this.TapePlayer.PromptBar.UpdateButtons();
			return;
		}
		this.TapePlayer.PromptBar.Label[0].text = (CollectibleGlobals.GetHeadmasterTapeCollected(this.Selected) ? "PLAY" : string.Empty);
		this.TapePlayer.PromptBar.UpdateButtons();
	}

	// Token: 0x04003480 RID: 13440
	public InputManagerScript InputManager;

	// Token: 0x04003481 RID: 13441
	public TapePlayerScript TapePlayer;

	// Token: 0x04003482 RID: 13442
	public PromptBarScript PromptBar;

	// Token: 0x04003483 RID: 13443
	public GameObject Jukebox;

	// Token: 0x04003484 RID: 13444
	public Transform TapePlayerCamera;

	// Token: 0x04003485 RID: 13445
	public Transform Highlight;

	// Token: 0x04003486 RID: 13446
	public Transform TimeBar;

	// Token: 0x04003487 RID: 13447
	public Transform List;

	// Token: 0x04003488 RID: 13448
	public AudioClip[] Recordings;

	// Token: 0x04003489 RID: 13449
	public AudioClip[] BasementRecordings;

	// Token: 0x0400348A RID: 13450
	public AudioClip[] HeadmasterRecordings;

	// Token: 0x0400348B RID: 13451
	public UILabel[] TapeLabels;

	// Token: 0x0400348C RID: 13452
	public GameObject[] NewIcons;

	// Token: 0x0400348D RID: 13453
	public AudioClip TapeStop;

	// Token: 0x0400348E RID: 13454
	public string CurrentTime;

	// Token: 0x0400348F RID: 13455
	public string ClipLength;

	// Token: 0x04003490 RID: 13456
	public bool Listening;

	// Token: 0x04003491 RID: 13457
	public bool Show;

	// Token: 0x04003492 RID: 13458
	public UILabel HeaderLabel;

	// Token: 0x04003493 RID: 13459
	public UILabel Subtitle;

	// Token: 0x04003494 RID: 13460
	public UILabel Label;

	// Token: 0x04003495 RID: 13461
	public UISprite Bar;

	// Token: 0x04003496 RID: 13462
	public int TotalTapes = 10;

	// Token: 0x04003497 RID: 13463
	public int Category = 1;

	// Token: 0x04003498 RID: 13464
	public int Selected = 1;

	// Token: 0x04003499 RID: 13465
	public int Phase = 1;

	// Token: 0x0400349A RID: 13466
	public float RoundedTime;

	// Token: 0x0400349B RID: 13467
	public float ResumeTime;

	// Token: 0x0400349C RID: 13468
	public float Timer;

	// Token: 0x0400349D RID: 13469
	public float[] Cues1;

	// Token: 0x0400349E RID: 13470
	public float[] Cues2;

	// Token: 0x0400349F RID: 13471
	public float[] Cues3;

	// Token: 0x040034A0 RID: 13472
	public float[] Cues4;

	// Token: 0x040034A1 RID: 13473
	public float[] Cues5;

	// Token: 0x040034A2 RID: 13474
	public float[] Cues6;

	// Token: 0x040034A3 RID: 13475
	public float[] Cues7;

	// Token: 0x040034A4 RID: 13476
	public float[] Cues8;

	// Token: 0x040034A5 RID: 13477
	public float[] Cues9;

	// Token: 0x040034A6 RID: 13478
	public float[] Cues10;

	// Token: 0x040034A7 RID: 13479
	public string[] Subs1;

	// Token: 0x040034A8 RID: 13480
	public string[] Subs2;

	// Token: 0x040034A9 RID: 13481
	public string[] Subs3;

	// Token: 0x040034AA RID: 13482
	public string[] Subs4;

	// Token: 0x040034AB RID: 13483
	public string[] Subs5;

	// Token: 0x040034AC RID: 13484
	public string[] Subs6;

	// Token: 0x040034AD RID: 13485
	public string[] Subs7;

	// Token: 0x040034AE RID: 13486
	public string[] Subs8;

	// Token: 0x040034AF RID: 13487
	public string[] Subs9;

	// Token: 0x040034B0 RID: 13488
	public string[] Subs10;

	// Token: 0x040034B1 RID: 13489
	public float[] BasementCues1;

	// Token: 0x040034B2 RID: 13490
	public float[] BasementCues10;

	// Token: 0x040034B3 RID: 13491
	public string[] BasementSubs1;

	// Token: 0x040034B4 RID: 13492
	public string[] BasementSubs10;

	// Token: 0x040034B5 RID: 13493
	public float[] HeadmasterCues1;

	// Token: 0x040034B6 RID: 13494
	public float[] HeadmasterCues2;

	// Token: 0x040034B7 RID: 13495
	public float[] HeadmasterCues6;

	// Token: 0x040034B8 RID: 13496
	public float[] HeadmasterCues10;

	// Token: 0x040034B9 RID: 13497
	public string[] HeadmasterSubs1;

	// Token: 0x040034BA RID: 13498
	public string[] HeadmasterSubs2;

	// Token: 0x040034BB RID: 13499
	public string[] HeadmasterSubs6;

	// Token: 0x040034BC RID: 13500
	public string[] HeadmasterSubs10;
}
