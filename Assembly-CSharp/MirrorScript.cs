using System;
using UnityEngine;

// Token: 0x02000334 RID: 820
public class MirrorScript : MonoBehaviour
{
	// Token: 0x06001848 RID: 6216 RVA: 0x000DA268 File Offset: 0x000D8468
	private void Start()
	{
		this.Limit = this.Idles.Length - 1;
		if (this.Prompt.Yandere.Club == ClubType.Delinquent)
		{
			this.ID = 10;
			if (this.Prompt.Yandere.Persona != YanderePersonaType.Tough)
			{
				this.UpdatePersona();
			}
		}
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x000DA2BC File Offset: 0x000D84BC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Health > 0)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.ID++;
				if (this.ID == this.Limit)
				{
					this.ID = 0;
				}
				this.UpdatePersona();
				return;
			}
		}
		else if (this.Prompt.Circle[1].fillAmount == 0f && this.Prompt.Yandere.Health > 0)
		{
			this.Prompt.Circle[1].fillAmount = 1f;
			this.ID--;
			if (this.ID < 0)
			{
				this.ID = this.Limit - 1;
			}
			this.UpdatePersona();
		}
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x000DA3A8 File Offset: 0x000D85A8
	private void UpdatePersona()
	{
		if (!this.Prompt.Yandere.Carrying)
		{
			this.Prompt.Yandere.NotificationManager.PersonaName = this.Personas[this.ID];
			this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Persona);
			this.Prompt.Yandere.IdleAnim = this.Idles[this.ID];
			this.Prompt.Yandere.WalkAnim = this.Walks[this.ID];
			this.Prompt.Yandere.UpdatePersona(this.ID);
		}
		this.Prompt.Yandere.OriginalIdleAnim = this.Idles[this.ID];
		this.Prompt.Yandere.OriginalWalkAnim = this.Walks[this.ID];
		this.Prompt.Yandere.StudentManager.UpdatePerception();
	}

	// Token: 0x04002350 RID: 9040
	public PromptScript Prompt;

	// Token: 0x04002351 RID: 9041
	public string[] Personas;

	// Token: 0x04002352 RID: 9042
	public string[] Idles;

	// Token: 0x04002353 RID: 9043
	public string[] Walks;

	// Token: 0x04002354 RID: 9044
	public int ID;

	// Token: 0x04002355 RID: 9045
	public int Limit;
}
