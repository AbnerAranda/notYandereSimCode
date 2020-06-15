using System;
using UnityEngine;

// Token: 0x0200045F RID: 1119
public class VendingMachineScript : MonoBehaviour
{
	// Token: 0x06001D0D RID: 7437 RVA: 0x001592D4 File Offset: 0x001574D4
	private void Start()
	{
		if (this.SnackMachine)
		{
			this.Prompt.Text[0] = "Buy Snack for $" + this.Price + ".00";
		}
		else
		{
			this.Prompt.Text[0] = "Buy Drink for $" + this.Price + ".00";
		}
		this.Prompt.Label[0].text = "     " + this.Prompt.Text[0];
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x00159364 File Offset: 0x00157564
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Yandere.Inventory.Money >= (float)this.Price)
			{
				if (!this.Sabotaged)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.Cans[UnityEngine.Random.Range(0, this.Cans.Length)], this.CanSpawn.position, this.CanSpawn.rotation).GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f, 1.1f);
				}
				if (this.SnackMachine && SchemeGlobals.GetSchemeStage(4) == 3)
				{
					SchemeGlobals.SetSchemeStage(4, 4);
					this.Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
				}
				this.Prompt.Yandere.Inventory.Money -= (float)this.Price;
				this.Prompt.Yandere.Inventory.UpdateMoney();
				return;
			}
			this.Prompt.Yandere.NotificationManager.CustomText = "Not enough money!";
			this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
		}
	}

	// Token: 0x04003673 RID: 13939
	public PromptScript Prompt;

	// Token: 0x04003674 RID: 13940
	public Transform CanSpawn;

	// Token: 0x04003675 RID: 13941
	public GameObject[] Cans;

	// Token: 0x04003676 RID: 13942
	public bool SnackMachine;

	// Token: 0x04003677 RID: 13943
	public bool Sabotaged;

	// Token: 0x04003678 RID: 13944
	public int Price;
}
