using System;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class AppearanceWindowScript : MonoBehaviour
{
	// Token: 0x06000A07 RID: 2567 RVA: 0x0004FB14 File Offset: 0x0004DD14
	private void Start()
	{
		this.Window.localScale = Vector3.zero;
		for (int i = 1; i < 10; i++)
		{
			this.Checks[i].SetActive(DatingGlobals.GetSuitorCheck(i));
		}
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0004FB54 File Offset: 0x0004DD54
	private void Update()
	{
		if (!this.Show)
		{
			if (this.Window.gameObject.activeInHierarchy)
			{
				if (this.Window.localScale.x > 0.1f)
				{
					this.Window.localScale = Vector3.Lerp(this.Window.localScale, Vector3.zero, Time.deltaTime * 10f);
					return;
				}
				this.Window.localScale = Vector3.zero;
				this.Window.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			this.Window.localScale = Vector3.Lerp(this.Window.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (this.Ready)
			{
				if (this.InputManager.TappedUp)
				{
					this.Selected--;
					if (this.Selected == 10)
					{
						this.Selected = 9;
					}
					this.UpdateHighlight();
				}
				if (this.InputManager.TappedDown)
				{
					this.Selected++;
					if (this.Selected == 10)
					{
						this.Selected = 11;
					}
					this.UpdateHighlight();
				}
				if (Input.GetButtonDown("A"))
				{
					if (this.Selected == 1)
					{
						if (!this.Checks[1].activeInHierarchy)
						{
							StudentGlobals.CustomSuitorHair = 55;
							DatingGlobals.SetSuitorCheck(1, true);
							DatingGlobals.SetSuitorCheck(2, false);
							this.Checks[1].SetActive(true);
							this.Checks[2].SetActive(false);
						}
						else
						{
							StudentGlobals.CustomSuitorHair = 0;
							DatingGlobals.SetSuitorCheck(1, false);
							this.Checks[1].SetActive(false);
						}
					}
					else if (this.Selected == 2)
					{
						if (!this.Checks[2].activeInHierarchy)
						{
							StudentGlobals.CustomSuitorHair = 56;
							DatingGlobals.SetSuitorCheck(1, false);
							DatingGlobals.SetSuitorCheck(2, true);
							this.Checks[1].SetActive(false);
							this.Checks[2].SetActive(true);
						}
						else
						{
							StudentGlobals.CustomSuitorHair = 0;
							DatingGlobals.SetSuitorCheck(2, false);
							this.Checks[2].SetActive(false);
						}
					}
					else if (this.Selected == 3)
					{
						if (!this.Checks[3].activeInHierarchy)
						{
							StudentGlobals.CustomSuitorAccessory = 17;
							DatingGlobals.SetSuitorCheck(3, true);
							DatingGlobals.SetSuitorCheck(4, false);
							this.Checks[3].SetActive(true);
							this.Checks[4].SetActive(false);
						}
						else
						{
							StudentGlobals.CustomSuitorAccessory = 0;
							DatingGlobals.SetSuitorCheck(3, false);
							this.Checks[3].SetActive(false);
						}
					}
					else if (this.Selected == 4)
					{
						if (!this.Checks[4].activeInHierarchy)
						{
							StudentGlobals.CustomSuitorAccessory = 1;
							DatingGlobals.SetSuitorCheck(3, false);
							DatingGlobals.SetSuitorCheck(4, true);
							this.Checks[3].SetActive(false);
							this.Checks[4].SetActive(true);
						}
						else
						{
							StudentGlobals.CustomSuitorAccessory = 0;
							DatingGlobals.SetSuitorCheck(4, false);
							this.Checks[4].SetActive(false);
						}
					}
					else if (this.Selected == 5)
					{
						if (!this.Checks[5].activeInHierarchy)
						{
							StudentGlobals.CustomSuitorEyewear = 6;
							DatingGlobals.SetSuitorCheck(5, true);
							DatingGlobals.SetSuitorCheck(6, false);
							this.Checks[5].SetActive(true);
							this.Checks[6].SetActive(false);
						}
						else
						{
							StudentGlobals.CustomSuitorEyewear = 0;
							DatingGlobals.SetSuitorCheck(5, false);
							this.Checks[5].SetActive(false);
						}
					}
					else if (this.Selected == 6)
					{
						if (!this.Checks[6].activeInHierarchy)
						{
							StudentGlobals.CustomSuitorEyewear = 3;
							DatingGlobals.SetSuitorCheck(5, false);
							DatingGlobals.SetSuitorCheck(6, true);
							this.Checks[5].SetActive(false);
							this.Checks[6].SetActive(true);
						}
						else
						{
							StudentGlobals.CustomSuitorEyewear = 0;
							DatingGlobals.SetSuitorCheck(6, false);
							this.Checks[6].SetActive(false);
						}
					}
					else if (this.Selected == 7)
					{
						if (!this.Checks[7].activeInHierarchy)
						{
							StudentGlobals.CustomSuitorTan = true;
							DatingGlobals.SetSuitorCheck(7, true);
							this.Checks[7].SetActive(true);
						}
						else
						{
							StudentGlobals.CustomSuitorTan = false;
							DatingGlobals.SetSuitorCheck(7, false);
							this.Checks[7].SetActive(false);
						}
					}
					else if (this.Selected == 8)
					{
						if (!this.Checks[8].activeInHierarchy)
						{
							StudentGlobals.CustomSuitorBlack = true;
							DatingGlobals.SetSuitorCheck(8, true);
							this.Checks[8].SetActive(true);
						}
						else
						{
							StudentGlobals.CustomSuitorBlack = false;
							DatingGlobals.SetSuitorCheck(8, false);
							this.Checks[8].SetActive(false);
						}
					}
					else if (this.Selected == 9)
					{
						if (!this.Checks[9].activeInHierarchy)
						{
							StudentGlobals.CustomSuitorJewelry = 1;
							DatingGlobals.SetSuitorCheck(9, true);
							this.Checks[9].SetActive(true);
						}
						else
						{
							StudentGlobals.CustomSuitorJewelry = 0;
							DatingGlobals.SetSuitorCheck(9, false);
							this.Checks[9].SetActive(false);
						}
					}
					else if (this.Selected == 11)
					{
						StudentGlobals.CustomSuitor = true;
						this.PromptBar.ClearButtons();
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = false;
						this.Yandere.Interaction = YandereInteractionType.ChangingAppearance;
						this.Yandere.TalkTimer = 3f;
						this.Ready = false;
						this.Show = false;
					}
				}
			}
			if (Input.GetButtonUp("A"))
			{
				this.Ready = true;
			}
		}
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x000500A8 File Offset: 0x0004E2A8
	private void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 11;
		}
		else if (this.Selected > 11)
		{
			this.Selected = 1;
		}
		this.Highlight.transform.localPosition = new Vector3(this.Highlight.transform.localPosition.x, 300f - 50f * (float)this.Selected, this.Highlight.transform.localPosition.z);
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0005012B File Offset: 0x0004E32B
	private void Exit()
	{
		this.Selected = 1;
		this.UpdateHighlight();
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.Show = false;
	}

	// Token: 0x040009F4 RID: 2548
	public StudentManagerScript StudentManager;

	// Token: 0x040009F5 RID: 2549
	public InputManagerScript InputManager;

	// Token: 0x040009F6 RID: 2550
	public PromptBarScript PromptBar;

	// Token: 0x040009F7 RID: 2551
	public YandereScript Yandere;

	// Token: 0x040009F8 RID: 2552
	public Transform Highlight;

	// Token: 0x040009F9 RID: 2553
	public Transform Window;

	// Token: 0x040009FA RID: 2554
	public GameObject[] Checks;

	// Token: 0x040009FB RID: 2555
	public int Selected;

	// Token: 0x040009FC RID: 2556
	public bool Ready;

	// Token: 0x040009FD RID: 2557
	public bool Show;
}
