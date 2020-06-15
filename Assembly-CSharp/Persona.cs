using System;
using UnityEngine;

// Token: 0x0200028A RID: 650
[Serializable]
public class Persona
{
	// Token: 0x060013C8 RID: 5064 RVA: 0x000AD60D File Offset: 0x000AB80D
	public Persona(PersonaType type)
	{
		this.type = type;
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000AD61C File Offset: 0x000AB81C
	public PersonaType Type
	{
		get
		{
			return this.type;
		}
	}

	// Token: 0x04001BA0 RID: 7072
	[SerializeField]
	private PersonaType type;

	// Token: 0x04001BA1 RID: 7073
	public static readonly PersonaTypeAndStringDictionary PersonaNames = new PersonaTypeAndStringDictionary
	{
		{
			PersonaType.None,
			"None"
		},
		{
			PersonaType.Loner,
			"Loner"
		},
		{
			PersonaType.TeachersPet,
			"Teacher's Pet"
		},
		{
			PersonaType.Heroic,
			"Heroic"
		},
		{
			PersonaType.Coward,
			"Coward"
		},
		{
			PersonaType.Evil,
			"Evil"
		},
		{
			PersonaType.SocialButterfly,
			"Social Butterfly"
		},
		{
			PersonaType.Lovestruck,
			"Lovestruck"
		},
		{
			PersonaType.Dangerous,
			"Dangerous"
		},
		{
			PersonaType.Strict,
			"Strict"
		},
		{
			PersonaType.PhoneAddict,
			"Phone Addict"
		},
		{
			PersonaType.Fragile,
			"Fragile"
		},
		{
			PersonaType.Spiteful,
			"Spiteful"
		},
		{
			PersonaType.Sleuth,
			"Sleuth"
		},
		{
			PersonaType.Vengeful,
			"Vengeful"
		},
		{
			PersonaType.Protective,
			"Protective"
		},
		{
			PersonaType.Violent,
			"Violent"
		},
		{
			PersonaType.Nemesis,
			"?????"
		}
	};
}
