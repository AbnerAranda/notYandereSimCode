using System;

// Token: 0x020003C2 RID: 962
[Serializable]
public class SenpaiSaveData
{
	// Token: 0x06001A37 RID: 6711 RVA: 0x00100D18 File Offset: 0x000FEF18
	public static SenpaiSaveData ReadFromGlobals()
	{
		return new SenpaiSaveData
		{
			customSenpai = SenpaiGlobals.CustomSenpai,
			senpaiEyeColor = SenpaiGlobals.SenpaiEyeColor,
			senpaiEyeWear = SenpaiGlobals.SenpaiEyeWear,
			senpaiFacialHair = SenpaiGlobals.SenpaiFacialHair,
			senpaiHairColor = SenpaiGlobals.SenpaiHairColor,
			senpaiHairStyle = SenpaiGlobals.SenpaiHairStyle,
			senpaiSkinColor = SenpaiGlobals.SenpaiSkinColor
		};
	}

	// Token: 0x06001A38 RID: 6712 RVA: 0x00100D78 File Offset: 0x000FEF78
	public static void WriteToGlobals(SenpaiSaveData data)
	{
		SenpaiGlobals.CustomSenpai = data.customSenpai;
		SenpaiGlobals.SenpaiEyeColor = data.senpaiEyeColor;
		SenpaiGlobals.SenpaiEyeWear = data.senpaiEyeWear;
		SenpaiGlobals.SenpaiFacialHair = data.senpaiFacialHair;
		SenpaiGlobals.SenpaiHairColor = data.senpaiHairColor;
		SenpaiGlobals.SenpaiHairStyle = data.senpaiHairStyle;
		SenpaiGlobals.SenpaiSkinColor = data.senpaiSkinColor;
	}

	// Token: 0x0400294B RID: 10571
	public bool customSenpai;

	// Token: 0x0400294C RID: 10572
	public string senpaiEyeColor = string.Empty;

	// Token: 0x0400294D RID: 10573
	public int senpaiEyeWear;

	// Token: 0x0400294E RID: 10574
	public int senpaiFacialHair;

	// Token: 0x0400294F RID: 10575
	public string senpaiHairColor = string.Empty;

	// Token: 0x04002950 RID: 10576
	public int senpaiHairStyle;

	// Token: 0x04002951 RID: 10577
	public int senpaiSkinColor;
}
