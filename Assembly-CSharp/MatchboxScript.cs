using System;
using UnityEngine;

// Token: 0x0200032E RID: 814
public class MatchboxScript : MonoBehaviour
{
	// Token: 0x06001830 RID: 6192 RVA: 0x000D88E1 File Offset: 0x000D6AE1
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x000D88F8 File Offset: 0x000D6AF8
	private void Update()
	{
		if (!this.Prompt.PauseScreen.Show)
		{
			if (this.Yandere.PickUp == this.PickUp)
			{
				if (this.Prompt.HideButton[0])
				{
					this.Yandere.Arc.SetActive(true);
					this.Prompt.HideButton[0] = false;
					this.Prompt.HideButton[3] = true;
				}
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.Prompt.Circle[0].fillAmount = 1f;
					if (!this.Yandere.Chased && this.Yandere.Chasers == 0 && this.Yandere.CanMove && !this.Yandere.Flicking)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Match, base.transform.position, Quaternion.identity);
						gameObject.transform.parent = this.Yandere.ItemParent;
						gameObject.transform.localPosition = new Vector3(0.0159f, 0.0043f, 0.0152f);
						gameObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
						gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
						this.Yandere.Match = gameObject;
						this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_flickingMatch_00");
						this.Yandere.YandereVision = false;
						this.Yandere.Arc.SetActive(false);
						this.Yandere.Flicking = true;
						this.Yandere.CanMove = false;
						this.Prompt.Hide();
						this.Prompt.enabled = false;
						return;
					}
				}
			}
			else if (!this.Prompt.HideButton[0])
			{
				this.Yandere.Arc.SetActive(false);
				this.Prompt.HideButton[0] = true;
				this.Prompt.HideButton[3] = false;
			}
		}
	}

	// Token: 0x04002320 RID: 8992
	public YandereScript Yandere;

	// Token: 0x04002321 RID: 8993
	public PromptScript Prompt;

	// Token: 0x04002322 RID: 8994
	public PickUpScript PickUp;

	// Token: 0x04002323 RID: 8995
	public GameObject Match;
}
