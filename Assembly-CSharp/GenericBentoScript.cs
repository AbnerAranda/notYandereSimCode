using System;
using UnityEngine;

// Token: 0x020002B4 RID: 692
public class GenericBentoScript : MonoBehaviour
{
	// Token: 0x0600144C RID: 5196 RVA: 0x000B4620 File Offset: 0x000B2820
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Inventory.EmeticPoison)
			{
				this.Prompt.Yandere.Inventory.EmeticPoison = false;
				this.Prompt.Yandere.PoisonType = 1;
			}
			else
			{
				this.Prompt.Yandere.Inventory.RatPoison = false;
				this.Prompt.Yandere.PoisonType = 3;
			}
			this.Emetic = true;
			this.ShutOff();
			return;
		}
		if (this.Prompt.Circle[1].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Inventory.Sedative)
			{
				this.Prompt.Yandere.Inventory.Sedative = false;
			}
			else
			{
				this.Prompt.Yandere.Inventory.Tranquilizer = false;
			}
			this.Prompt.Yandere.PoisonType = 4;
			this.Tranquil = true;
			this.ShutOff();
			return;
		}
		if (this.Prompt.Circle[2].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Inventory.LethalPoison)
			{
				this.Prompt.Yandere.Inventory.LethalPoison = false;
				this.Prompt.Yandere.PoisonType = 2;
			}
			else
			{
				this.Prompt.Yandere.Inventory.ChemicalPoison = false;
				this.Prompt.Yandere.PoisonType = 2;
			}
			this.Lethal = true;
			this.ShutOff();
			return;
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.HeadachePoison = false;
			this.Prompt.Yandere.PoisonType = 5;
			this.Headache = true;
			this.ShutOff();
		}
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x000B4814 File Offset: 0x000B2A14
	private void ShutOff()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, base.transform.position, Quaternion.identity);
		this.PoisonSpot = gameObject.transform;
		this.PoisonSpot.position = new Vector3(this.PoisonSpot.position.x, this.Prompt.Yandere.transform.position.y, this.PoisonSpot.position.z);
		this.PoisonSpot.LookAt(this.Prompt.Yandere.transform.position);
		this.PoisonSpot.Translate(Vector3.forward * 0.25f);
		this.Prompt.Yandere.CharacterAnimation["f02_poisoning_00"].speed = 2f;
		this.Prompt.Yandere.CharacterAnimation.CrossFade("f02_poisoning_00");
		this.Prompt.Yandere.StudentManager.UpdateAllBentos();
		this.Prompt.Yandere.TargetBento = this;
		this.Prompt.Yandere.Poisoning = true;
		this.Prompt.Yandere.CanMove = false;
		this.Tampered = true;
		base.enabled = false;
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x000B4978 File Offset: 0x000B2B78
	public void UpdatePrompts()
	{
		this.Prompt.HideButton[0] = true;
		this.Prompt.HideButton[1] = true;
		this.Prompt.HideButton[2] = true;
		this.Prompt.HideButton[3] = true;
		if (this.Prompt.Yandere.Inventory.EmeticPoison || this.Prompt.Yandere.Inventory.RatPoison)
		{
			this.Prompt.HideButton[0] = false;
		}
		if (this.Prompt.Yandere.Inventory.Tranquilizer || this.Prompt.Yandere.Inventory.Sedative)
		{
			this.Prompt.HideButton[1] = false;
		}
		if (this.Prompt.Yandere.Inventory.LethalPoison || this.Prompt.Yandere.Inventory.ChemicalPoison)
		{
			this.Prompt.HideButton[2] = false;
		}
		if (this.Prompt.Yandere.Inventory.HeadachePoison)
		{
			this.Prompt.HideButton[3] = false;
		}
	}

	// Token: 0x04001D11 RID: 7441
	public GameObject EmptyGameObject;

	// Token: 0x04001D12 RID: 7442
	public Transform PoisonSpot;

	// Token: 0x04001D13 RID: 7443
	public PromptScript Prompt;

	// Token: 0x04001D14 RID: 7444
	public bool Emetic;

	// Token: 0x04001D15 RID: 7445
	public bool Tranquil;

	// Token: 0x04001D16 RID: 7446
	public bool Headache;

	// Token: 0x04001D17 RID: 7447
	public bool Lethal;

	// Token: 0x04001D18 RID: 7448
	public bool Tampered;

	// Token: 0x04001D19 RID: 7449
	public int StudentID;
}
