using System;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class PersonaSubtitleScript : MonoBehaviour
{
	// Token: 0x060018D0 RID: 6352 RVA: 0x000E5B54 File Offset: 0x000E3D54
	public void UpdateLabel(PersonaType Persona, float Reputation, float Duration)
	{
		switch (Persona)
		{
		case PersonaType.Loner:
			this.SubtitleArray = this.LonerReactions;
			break;
		case PersonaType.TeachersPet:
			this.SubtitleArray = this.TeachersPetReactions;
			break;
		case PersonaType.Heroic:
			this.SubtitleArray = this.HeroicReactions;
			break;
		case PersonaType.Coward:
			this.SubtitleArray = this.CowardReactions;
			break;
		case PersonaType.Evil:
			this.SubtitleArray = this.EvilReactions;
			break;
		case PersonaType.SocialButterfly:
			this.SubtitleArray = this.SocialButterflyReactions;
			break;
		case PersonaType.Lovestruck:
			this.SubtitleArray = this.LovestruckReactions;
			break;
		case PersonaType.Dangerous:
			this.SubtitleArray = this.DangerousReactions;
			break;
		case PersonaType.Strict:
			this.SubtitleArray = this.StrictReactions;
			break;
		case PersonaType.PhoneAddict:
			this.SubtitleArray = this.PhoneAddictReactions;
			break;
		case PersonaType.Fragile:
			this.SubtitleArray = this.FragileReactions;
			break;
		case PersonaType.Spiteful:
			this.SubtitleArray = this.SpitefulReactions;
			break;
		case PersonaType.Sleuth:
			this.SubtitleArray = this.SleuthReactions;
			break;
		case PersonaType.Vengeful:
			this.SubtitleArray = this.VengefulReactions;
			break;
		case PersonaType.Protective:
			this.SubtitleArray = this.ProtectiveReactions;
			break;
		case PersonaType.Violent:
			this.SubtitleArray = this.ViolentReactions;
			break;
		default:
			if (Persona == PersonaType.Nemesis)
			{
				this.SubtitleArray = this.NemesisReactions;
			}
			break;
		}
		if (Reputation < -0.333333343f)
		{
			this.Subtitle.Label.text = this.SubtitleArray[1];
		}
		else if (Reputation > 0.333333343f)
		{
			this.Subtitle.Label.text = this.SubtitleArray[3];
		}
		else
		{
			this.Subtitle.Label.text = this.SubtitleArray[2];
		}
		this.Subtitle.Timer = Duration;
	}

	// Token: 0x040024E4 RID: 9444
	public SubtitleScript Subtitle;

	// Token: 0x040024E5 RID: 9445
	public string[] LonerReactions;

	// Token: 0x040024E6 RID: 9446
	public string[] TeachersPetReactions;

	// Token: 0x040024E7 RID: 9447
	public string[] HeroicReactions;

	// Token: 0x040024E8 RID: 9448
	public string[] CowardReactions;

	// Token: 0x040024E9 RID: 9449
	public string[] EvilReactions;

	// Token: 0x040024EA RID: 9450
	public string[] SocialButterflyReactions;

	// Token: 0x040024EB RID: 9451
	public string[] LovestruckReactions;

	// Token: 0x040024EC RID: 9452
	public string[] DangerousReactions;

	// Token: 0x040024ED RID: 9453
	public string[] StrictReactions;

	// Token: 0x040024EE RID: 9454
	public string[] PhoneAddictReactions;

	// Token: 0x040024EF RID: 9455
	public string[] FragileReactions;

	// Token: 0x040024F0 RID: 9456
	public string[] SpitefulReactions;

	// Token: 0x040024F1 RID: 9457
	public string[] SleuthReactions;

	// Token: 0x040024F2 RID: 9458
	public string[] VengefulReactions;

	// Token: 0x040024F3 RID: 9459
	public string[] ProtectiveReactions;

	// Token: 0x040024F4 RID: 9460
	public string[] ViolentReactions;

	// Token: 0x040024F5 RID: 9461
	public string[] NemesisReactions;

	// Token: 0x040024F6 RID: 9462
	public string[] SubtitleArray;
}
