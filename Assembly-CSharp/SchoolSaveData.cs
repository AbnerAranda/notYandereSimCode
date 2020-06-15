using System;

// Token: 0x020003C1 RID: 961
[Serializable]
public class SchoolSaveData
{
	// Token: 0x06001A34 RID: 6708 RVA: 0x00100B70 File Offset: 0x000FED70
	public static SchoolSaveData ReadFromGlobals()
	{
		SchoolSaveData schoolSaveData = new SchoolSaveData();
		foreach (int num in SchoolGlobals.KeysOfDemonActive())
		{
			if (SchoolGlobals.GetDemonActive(num))
			{
				schoolSaveData.demonActive.Add(num);
			}
		}
		foreach (int num2 in SchoolGlobals.KeysOfGardenGraveOccupied())
		{
			if (SchoolGlobals.GetGardenGraveOccupied(num2))
			{
				schoolSaveData.gardenGraveOccupied.Add(num2);
			}
		}
		schoolSaveData.kidnapVictim = SchoolGlobals.KidnapVictim;
		schoolSaveData.population = SchoolGlobals.Population;
		schoolSaveData.roofFence = SchoolGlobals.RoofFence;
		schoolSaveData.schoolAtmosphere = SchoolGlobals.SchoolAtmosphere;
		schoolSaveData.schoolAtmosphereSet = SchoolGlobals.SchoolAtmosphereSet;
		schoolSaveData.scp = SchoolGlobals.SCP;
		return schoolSaveData;
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x00100C24 File Offset: 0x000FEE24
	public static void WriteToGlobals(SchoolSaveData data)
	{
		foreach (int demonID in data.demonActive)
		{
			SchoolGlobals.SetDemonActive(demonID, true);
		}
		foreach (int graveID in data.gardenGraveOccupied)
		{
			SchoolGlobals.SetGardenGraveOccupied(graveID, true);
		}
		SchoolGlobals.KidnapVictim = data.kidnapVictim;
		SchoolGlobals.Population = data.population;
		SchoolGlobals.RoofFence = data.roofFence;
		SchoolGlobals.SchoolAtmosphere = data.schoolAtmosphere;
		SchoolGlobals.SchoolAtmosphereSet = data.schoolAtmosphereSet;
		SchoolGlobals.SCP = data.scp;
	}

	// Token: 0x04002943 RID: 10563
	public IntHashSet demonActive = new IntHashSet();

	// Token: 0x04002944 RID: 10564
	public IntHashSet gardenGraveOccupied = new IntHashSet();

	// Token: 0x04002945 RID: 10565
	public int kidnapVictim;

	// Token: 0x04002946 RID: 10566
	public int population;

	// Token: 0x04002947 RID: 10567
	public bool roofFence;

	// Token: 0x04002948 RID: 10568
	public float schoolAtmosphere;

	// Token: 0x04002949 RID: 10569
	public bool schoolAtmosphereSet;

	// Token: 0x0400294A RID: 10570
	public bool scp;
}
