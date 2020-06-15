using System;
using UnityEngine;

// Token: 0x020002BE RID: 702
public static class ClassGlobals
{
	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06001487 RID: 5255 RVA: 0x000B5E23 File Offset: 0x000B4023
	// (set) Token: 0x06001488 RID: 5256 RVA: 0x000B5E43 File Offset: 0x000B4043
	public static int Biology
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Biology");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Biology", value);
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06001489 RID: 5257 RVA: 0x000B5E64 File Offset: 0x000B4064
	// (set) Token: 0x0600148A RID: 5258 RVA: 0x000B5E84 File Offset: 0x000B4084
	public static int BiologyBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BiologyBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BiologyBonus", value);
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x0600148B RID: 5259 RVA: 0x000B5EA5 File Offset: 0x000B40A5
	// (set) Token: 0x0600148C RID: 5260 RVA: 0x000B5EC5 File Offset: 0x000B40C5
	public static int BiologyGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BiologyGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BiologyGrade", value);
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x0600148D RID: 5261 RVA: 0x000B5EE6 File Offset: 0x000B40E6
	// (set) Token: 0x0600148E RID: 5262 RVA: 0x000B5F06 File Offset: 0x000B4106
	public static int Chemistry
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Chemistry");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Chemistry", value);
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x0600148F RID: 5263 RVA: 0x000B5F27 File Offset: 0x000B4127
	// (set) Token: 0x06001490 RID: 5264 RVA: 0x000B5F47 File Offset: 0x000B4147
	public static int ChemistryBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ChemistryBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ChemistryBonus", value);
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x06001491 RID: 5265 RVA: 0x000B5F68 File Offset: 0x000B4168
	// (set) Token: 0x06001492 RID: 5266 RVA: 0x000B5F88 File Offset: 0x000B4188
	public static int ChemistryGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ChemistryGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ChemistryGrade", value);
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x06001493 RID: 5267 RVA: 0x000B5FA9 File Offset: 0x000B41A9
	// (set) Token: 0x06001494 RID: 5268 RVA: 0x000B5FC9 File Offset: 0x000B41C9
	public static int Language
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Language");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Language", value);
		}
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x06001495 RID: 5269 RVA: 0x000B5FEA File Offset: 0x000B41EA
	// (set) Token: 0x06001496 RID: 5270 RVA: 0x000B600A File Offset: 0x000B420A
	public static int LanguageBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LanguageBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LanguageBonus", value);
		}
	}

	// Token: 0x17000385 RID: 901
	// (get) Token: 0x06001497 RID: 5271 RVA: 0x000B602B File Offset: 0x000B422B
	// (set) Token: 0x06001498 RID: 5272 RVA: 0x000B604B File Offset: 0x000B424B
	public static int LanguageGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LanguageGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LanguageGrade", value);
		}
	}

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x06001499 RID: 5273 RVA: 0x000B606C File Offset: 0x000B426C
	// (set) Token: 0x0600149A RID: 5274 RVA: 0x000B608C File Offset: 0x000B428C
	public static int Physical
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Physical");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Physical", value);
		}
	}

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x0600149B RID: 5275 RVA: 0x000B60AD File Offset: 0x000B42AD
	// (set) Token: 0x0600149C RID: 5276 RVA: 0x000B60CD File Offset: 0x000B42CD
	public static int PhysicalBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PhysicalBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PhysicalBonus", value);
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x0600149D RID: 5277 RVA: 0x000B60EE File Offset: 0x000B42EE
	// (set) Token: 0x0600149E RID: 5278 RVA: 0x000B610E File Offset: 0x000B430E
	public static int PhysicalGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PhysicalGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PhysicalGrade", value);
		}
	}

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x0600149F RID: 5279 RVA: 0x000B612F File Offset: 0x000B432F
	// (set) Token: 0x060014A0 RID: 5280 RVA: 0x000B614F File Offset: 0x000B434F
	public static int Psychology
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Psychology");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Psychology", value);
		}
	}

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x060014A1 RID: 5281 RVA: 0x000B6170 File Offset: 0x000B4370
	// (set) Token: 0x060014A2 RID: 5282 RVA: 0x000B6190 File Offset: 0x000B4390
	public static int PsychologyBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PsychologyBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PsychologyBonus", value);
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x060014A3 RID: 5283 RVA: 0x000B61B1 File Offset: 0x000B43B1
	// (set) Token: 0x060014A4 RID: 5284 RVA: 0x000B61D1 File Offset: 0x000B43D1
	public static int PsychologyGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PsychologyGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PsychologyGrade", value);
		}
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x000B61F4 File Offset: 0x000B43F4
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Biology");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BiologyBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BiologyGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Chemistry");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ChemistryBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ChemistryGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Language");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LanguageBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LanguageGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Physical");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PhysicalBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PhysicalGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Psychology");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PsychologyBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PsychologyGrade");
	}

	// Token: 0x04001D3B RID: 7483
	private const string Str_Biology = "Biology";

	// Token: 0x04001D3C RID: 7484
	private const string Str_BiologyBonus = "BiologyBonus";

	// Token: 0x04001D3D RID: 7485
	private const string Str_BiologyGrade = "BiologyGrade";

	// Token: 0x04001D3E RID: 7486
	private const string Str_Chemistry = "Chemistry";

	// Token: 0x04001D3F RID: 7487
	private const string Str_ChemistryBonus = "ChemistryBonus";

	// Token: 0x04001D40 RID: 7488
	private const string Str_ChemistryGrade = "ChemistryGrade";

	// Token: 0x04001D41 RID: 7489
	private const string Str_Language = "Language";

	// Token: 0x04001D42 RID: 7490
	private const string Str_LanguageBonus = "LanguageBonus";

	// Token: 0x04001D43 RID: 7491
	private const string Str_LanguageGrade = "LanguageGrade";

	// Token: 0x04001D44 RID: 7492
	private const string Str_Physical = "Physical";

	// Token: 0x04001D45 RID: 7493
	private const string Str_PhysicalBonus = "PhysicalBonus";

	// Token: 0x04001D46 RID: 7494
	private const string Str_PhysicalGrade = "PhysicalGrade";

	// Token: 0x04001D47 RID: 7495
	private const string Str_Psychology = "Psychology";

	// Token: 0x04001D48 RID: 7496
	private const string Str_PsychologyBonus = "PsychologyBonus";

	// Token: 0x04001D49 RID: 7497
	private const string Str_PsychologyGrade = "PsychologyGrade";
}
