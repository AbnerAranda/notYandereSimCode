﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200048A RID: 1162
public class YanvaniaTitleScript : MonoBehaviour
{
	// Token: 0x06001DFB RID: 7675 RVA: 0x001771D8 File Offset: 0x001753D8
	private void Start()
	{
		this.Midori.transform.localPosition = new Vector3(1540f, 0f, 0f);
		this.Midori.transform.localEulerAngles = Vector3.zero;
		this.Midori.gameObject.SetActive(false);
		if (YanvaniaGlobals.DraculaDefeated)
		{
			TaskGlobals.SetTaskStatus(38, 2);
			this.SkipButton.SetActive(true);
			this.Logo.gameObject.SetActive(false);
		}
		else
		{
			this.SkipButton.SetActive(false);
		}
		this.Prologue.gameObject.SetActive(false);
		this.Prologue.localPosition = new Vector3(this.Prologue.localPosition.x, -2665f, this.Prologue.localPosition.z);
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		this.Buttons.alpha = 0f;
		this.Logo.color = new Color(this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 0f);
	}

	// Token: 0x06001DFC RID: 7676 RVA: 0x0017734C File Offset: 0x0017554C
	private void Update()
	{
		if (!this.Logo.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.M))
		{
			YanvaniaGlobals.DraculaDefeated = true;
			YanvaniaGlobals.MidoriEasterEgg = true;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.End))
		{
			YanvaniaGlobals.DraculaDefeated = true;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			YanvaniaGlobals.DraculaDefeated = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.FadeOut)
		{
			if (this.Darkness.color.a > 0f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Skip();
				}
				if (!this.ErrorWindow.activeInHierarchy)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a - Time.deltaTime);
					return;
				}
			}
			else if (this.Darkness.color.a <= 0f)
			{
				if (!YanvaniaGlobals.MidoriEasterEgg)
				{
					if (YanvaniaGlobals.DraculaDefeated)
					{
						if (!this.Prologue.gameObject.activeInHierarchy)
						{
							this.Prologue.gameObject.SetActive(true);
							component.volume = 0.5f;
							component.loop = true;
							component.clip = this.BGM;
							component.Play();
						}
						if (Input.GetButtonDown("B"))
						{
							this.Prologue.localPosition = new Vector3(this.Prologue.localPosition.x, 2501f, this.Prologue.localPosition.z);
							this.Prologue.GetComponent<AudioSource>().Stop();
						}
						if (this.Prologue.localPosition.y > 2500f)
						{
							if (component.isPlaying)
							{
								this.Midori.mainTexture = this.SadMidori;
								Time.timeScale = 1f;
								this.Midori.gameObject.GetComponent<AudioSource>().Stop();
								component.Stop();
							}
							if (this.ErrorLeave)
							{
								this.FadeOut = true;
								return;
							}
							this.ErrorWindow.SetActive(true);
							this.ErrorWindow.transform.localScale = Vector3.Lerp(this.ErrorWindow.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
							if (this.ErrorWindow.transform.localScale.x > 0.9f && Input.anyKeyDown)
							{
								AudioSource component2 = this.ErrorWindow.GetComponent<AudioSource>();
								component2.clip = this.ExitSound;
								component2.Play();
								this.ErrorLeave = true;
								return;
							}
						}
						else
						{
							this.Prologue.localPosition = new Vector3(this.Prologue.localPosition.x, this.Prologue.localPosition.y + Time.deltaTime * this.ScrollSpeed, this.Prologue.localPosition.z);
							if (Input.GetKeyDown(KeyCode.Equals))
							{
								Time.timeScale = 100f;
							}
							if (Input.GetKeyDown(KeyCode.Minus))
							{
								Time.timeScale = 1f;
								return;
							}
						}
					}
					else if (!component.isPlaying)
					{
						if (this.Logo.color.a == 0f)
						{
							component.Play();
							return;
						}
						component.loop = true;
						component.clip = this.BGM;
						component.Play();
						return;
					}
					else if (component.clip != this.BGM)
					{
						this.Logo.color = new Color(this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, this.Logo.color.a + Time.deltaTime);
						if (Input.GetButtonDown("A"))
						{
							this.Skip();
							return;
						}
					}
					else if (!this.FadeButtons)
					{
						this.Controls.alpha = Mathf.MoveTowards(this.Controls.alpha, 0f, Time.deltaTime);
						this.Credits.alpha = Mathf.MoveTowards(this.Credits.alpha, 0f, Time.deltaTime);
						if (this.Controls.alpha == 0f && this.Credits.alpha == 0f)
						{
							this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, -100f - 100f * (float)this.Selected, this.Highlight.localPosition.z);
							this.Buttons.alpha += Time.deltaTime;
							if (this.Buttons.alpha >= 1f)
							{
								if (Input.GetButtonDown("A"))
								{
									UnityEngine.Object.Instantiate<GameObject>(this.ButtonEffect, this.Highlight.position, Quaternion.identity);
									if (this.Selected == 1 || this.Selected == 4)
									{
										this.FadeOut = true;
									}
									if (this.Selected == 2 || this.Selected == 3)
									{
										this.FadeButtons = true;
									}
								}
								AudioSource component3 = this.Highlight.gameObject.GetComponent<AudioSource>();
								if (this.InputManager.TappedUp)
								{
									component3.Play();
									this.Selected--;
									if (this.Selected < 1)
									{
										this.Selected = 4;
									}
								}
								if (this.InputManager.TappedDown)
								{
									component3.Play();
									this.Selected++;
									if (this.Selected > 4)
									{
										this.Selected = 1;
										return;
									}
								}
							}
						}
					}
					else
					{
						this.Buttons.alpha -= Time.deltaTime;
						if (this.Buttons.alpha == 0f)
						{
							if (this.Selected == 2)
							{
								this.Controls.alpha = Mathf.MoveTowards(this.Controls.alpha, 1f, Time.deltaTime);
							}
							else
							{
								this.Credits.alpha = Mathf.MoveTowards(this.Credits.alpha, 1f, Time.deltaTime);
							}
						}
						if ((this.Controls.alpha == 1f || this.Credits.alpha == 1f) && Input.GetButtonDown("B"))
						{
							UnityEngine.Object.Instantiate<GameObject>(this.ButtonEffect, this.BackButtons[this.Selected].position, Quaternion.identity);
							this.FadeButtons = false;
							return;
						}
					}
				}
				else
				{
					this.Prologue.GetComponent<AudioSource>().enabled = false;
					this.Midori.gameObject.SetActive(true);
					this.ScrollSpeed = 60f;
					this.Midori.transform.localPosition = new Vector3(Mathf.Lerp(this.Midori.transform.localPosition.x, 875f, Time.deltaTime * 2f), this.Midori.transform.localPosition.y, this.Midori.transform.localPosition.z);
					this.Midori.transform.localEulerAngles = new Vector3(this.Midori.transform.localEulerAngles.x, this.Midori.transform.localEulerAngles.y, Mathf.Lerp(this.Midori.transform.localEulerAngles.z, 45f, Time.deltaTime * 2f));
					if (this.Midori.gameObject.GetComponent<AudioSource>().time > 3f)
					{
						YanvaniaGlobals.MidoriEasterEgg = false;
						return;
					}
				}
			}
		}
		else
		{
			this.ErrorWindow.transform.localScale = Vector3.Lerp(this.ErrorWindow.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			component.volume -= Time.deltaTime;
			if (this.Darkness.color.a >= 1f)
			{
				if (YanvaniaGlobals.DraculaDefeated)
				{
					SceneManager.LoadScene("HomeScene");
					return;
				}
				if (this.Selected == 1)
				{
					SceneManager.LoadScene("YanvaniaScene");
					return;
				}
				SceneManager.LoadScene("HomeScene");
			}
		}
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x00177C28 File Offset: 0x00175E28
	private void Skip()
	{
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
		this.Logo.color = new Color(this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 1f);
		AudioSource component = base.GetComponent<AudioSource>();
		component.loop = true;
		component.clip = this.BGM;
		component.Play();
	}

	// Token: 0x04003B8A RID: 15242
	public InputManagerScript InputManager;

	// Token: 0x04003B8B RID: 15243
	public GameObject ButtonEffect;

	// Token: 0x04003B8C RID: 15244
	public GameObject ErrorWindow;

	// Token: 0x04003B8D RID: 15245
	public GameObject SkipButton;

	// Token: 0x04003B8E RID: 15246
	public Transform Highlight;

	// Token: 0x04003B8F RID: 15247
	public Transform Prologue;

	// Token: 0x04003B90 RID: 15248
	public UIPanel Controls;

	// Token: 0x04003B91 RID: 15249
	public UIPanel Credits;

	// Token: 0x04003B92 RID: 15250
	public UIPanel Buttons;

	// Token: 0x04003B93 RID: 15251
	public UISprite Darkness;

	// Token: 0x04003B94 RID: 15252
	public UITexture Midori;

	// Token: 0x04003B95 RID: 15253
	public UITexture Logo;

	// Token: 0x04003B96 RID: 15254
	public AudioClip SelectSound;

	// Token: 0x04003B97 RID: 15255
	public AudioClip ExitSound;

	// Token: 0x04003B98 RID: 15256
	public AudioClip BGM;

	// Token: 0x04003B99 RID: 15257
	public Transform[] BackButtons;

	// Token: 0x04003B9A RID: 15258
	public Texture SadMidori;

	// Token: 0x04003B9B RID: 15259
	public bool FadeButtons;

	// Token: 0x04003B9C RID: 15260
	public bool ErrorLeave;

	// Token: 0x04003B9D RID: 15261
	public bool FadeOut;

	// Token: 0x04003B9E RID: 15262
	public float ScrollSpeed;

	// Token: 0x04003B9F RID: 15263
	public int Selected = 1;
}
