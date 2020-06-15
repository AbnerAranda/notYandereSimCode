using System;

// Token: 0x020003B2 RID: 946
[Serializable]
public class ClassSaveData
{
	// Token: 0x06001A07 RID: 6663 RVA: 0x000FF544 File Offset: 0x000FD744
	public static ClassSaveData ReadFromGlobals()
	{
		return new ClassSaveData
		{
			biology = ClassGlobals.Biology,
			biologyBonus = ClassGlobals.BiologyBonus,
			biologyGrade = ClassGlobals.BiologyGrade,
			chemistry = ClassGlobals.Chemistry,
			chemistryBonus = ClassGlobals.ChemistryBonus,
			chemistryGrade = ClassGlobals.ChemistryGrade,
			language = ClassGlobals.Language,
			languageBonus = ClassGlobals.LanguageBonus,
			languageGrade = ClassGlobals.LanguageGrade,
			physical = ClassGlobals.Physical,
			physicalBonus = ClassGlobals.PhysicalBonus,
			physicalGrade = ClassGlobals.PhysicalGrade,
			psychology = ClassGlobals.Psychology,
			psychologyBonus = ClassGlobals.PsychologyBonus,
			psychologyGrade = ClassGlobals.PsychologyGrade
		};
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x000FF5FC File Offset: 0x000FD7FC
	public static void WriteToGlobals(ClassSaveData data)
	{
		ClassGlobals.Biology = data.biology;
		ClassGlobals.BiologyBonus = data.biologyBonus;
		ClassGlobals.BiologyGrade = data.biologyGrade;
		ClassGlobals.Chemistry = data.chemistry;
		ClassGlobals.ChemistryBonus = data.chemistryBonus;
		ClassGlobals.ChemistryGrade = data.chemistryGrade;
		ClassGlobals.Language = data.language;
		ClassGlobals.LanguageBonus = data.languageBonus;
		ClassGlobals.LanguageGrade = data.languageGrade;
		ClassGlobals.Physical = data.physical;
		ClassGlobals.PhysicalBonus = data.physicalBonus;
		ClassGlobals.PhysicalGrade = data.physicalGrade;
		ClassGlobals.Psychology = data.psychology;
		ClassGlobals.PsychologyBonus = data.psychologyBonus;
		ClassGlobals.PsychologyGrade = data.psychologyGrade;
	}

	// Token: 0x040028DD RID: 10461
	public int biology;

	// Token: 0x040028DE RID: 10462
	public int biologyBonus;

	// Token: 0x040028DF RID: 10463
	public int biologyGrade;

	// Token: 0x040028E0 RID: 10464
	public int chemistry;

	// Token: 0x040028E1 RID: 10465
	public int chemistryBonus;

	// Token: 0x040028E2 RID: 10466
	public int chemistryGrade;

	// Token: 0x040028E3 RID: 10467
	public int language;

	// Token: 0x040028E4 RID: 10468
	public int languageBonus;

	// Token: 0x040028E5 RID: 10469
	public int languageGrade;

	// Token: 0x040028E6 RID: 10470
	public int physical;

	// Token: 0x040028E7 RID: 10471
	public int physicalBonus;

	// Token: 0x040028E8 RID: 10472
	public int physicalGrade;

	// Token: 0x040028E9 RID: 10473
	public int psychology;

	// Token: 0x040028EA RID: 10474
	public int psychologyBonus;

	// Token: 0x040028EB RID: 10475
	public int psychologyGrade;
}
