using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

// Token: 0x02000312 RID: 786
[Serializable]
public class StudentJson : JsonData
{
	// Token: 0x17000440 RID: 1088
	// (get) Token: 0x060017B1 RID: 6065 RVA: 0x000D1AB2 File Offset: 0x000CFCB2
	public static string FilePath
	{
		get
		{
			return Path.Combine(JsonData.FolderPath, "Students.json");
		}
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x000D1AC4 File Offset: 0x000CFCC4
	public static StudentJson[] LoadFromJson(string path)
	{
		StudentJson[] array = new StudentJson[101];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new StudentJson();
		}
		foreach (Dictionary<string, object> dictionary in JsonData.Deserialize(path))
		{
			int num = TFUtils.LoadInt(dictionary, "ID");
			if (num == 0)
			{
				break;
			}
			StudentJson studentJson = array[num];
			studentJson.name = TFUtils.LoadString(dictionary, "Name");
			studentJson.gender = TFUtils.LoadInt(dictionary, "Gender");
			studentJson.classID = TFUtils.LoadInt(dictionary, "Class");
			studentJson.seat = TFUtils.LoadInt(dictionary, "Seat");
			studentJson.club = (ClubType)TFUtils.LoadInt(dictionary, "Club");
			studentJson.persona = (PersonaType)TFUtils.LoadInt(dictionary, "Persona");
			studentJson.crush = TFUtils.LoadInt(dictionary, "Crush");
			studentJson.breastSize = TFUtils.LoadFloat(dictionary, "BreastSize");
			studentJson.strength = TFUtils.LoadInt(dictionary, "Strength");
			studentJson.hairstyle = TFUtils.LoadString(dictionary, "Hairstyle");
			studentJson.color = TFUtils.LoadString(dictionary, "Color");
			studentJson.eyes = TFUtils.LoadString(dictionary, "Eyes");
			studentJson.eyeType = TFUtils.LoadString(dictionary, "EyeType");
			studentJson.stockings = TFUtils.LoadString(dictionary, "Stockings");
			studentJson.accessory = TFUtils.LoadString(dictionary, "Accessory");
			studentJson.info = TFUtils.LoadString(dictionary, "Info");
			if (GameGlobals.LoveSick && studentJson.name == "Mai Waifu")
			{
				studentJson.name = "Mai Wakabayashi";
			}
			if (OptionGlobals.HighPopulation && studentJson.name == "Unknown")
			{
				studentJson.name = "Random";
			}
			float[] array3 = StudentJson.ConstructTempFloatArray(TFUtils.LoadString(dictionary, "ScheduleTime"));
			string[] array4 = StudentJson.ConstructTempStringArray(TFUtils.LoadString(dictionary, "ScheduleDestination"));
			string[] array5 = StudentJson.ConstructTempStringArray(TFUtils.LoadString(dictionary, "ScheduleAction"));
			studentJson.scheduleBlocks = new ScheduleBlock[array3.Length];
			for (int k = 0; k < studentJson.scheduleBlocks.Length; k++)
			{
				studentJson.scheduleBlocks[k] = new ScheduleBlock(array3[k], array4[k], array5[k]);
			}
			if (num == 10 || num == 11)
			{
				for (int l = 0; l < studentJson.scheduleBlocks.Length; l++)
				{
					studentJson.scheduleBlocks[l] = null;
				}
			}
			studentJson.success = true;
		}
		return array;
	}

	// Token: 0x17000441 RID: 1089
	// (get) Token: 0x060017B3 RID: 6067 RVA: 0x000D1D5D File Offset: 0x000CFF5D
	// (set) Token: 0x060017B4 RID: 6068 RVA: 0x000D1D65 File Offset: 0x000CFF65
	public string Name
	{
		get
		{
			return this.name;
		}
		set
		{
			this.name = value;
		}
	}

	// Token: 0x17000442 RID: 1090
	// (get) Token: 0x060017B5 RID: 6069 RVA: 0x000D1D6E File Offset: 0x000CFF6E
	public int Gender
	{
		get
		{
			return this.gender;
		}
	}

	// Token: 0x17000443 RID: 1091
	// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000D1D76 File Offset: 0x000CFF76
	// (set) Token: 0x060017B7 RID: 6071 RVA: 0x000D1D7E File Offset: 0x000CFF7E
	public int Class
	{
		get
		{
			return this.classID;
		}
		set
		{
			this.classID = value;
		}
	}

	// Token: 0x17000444 RID: 1092
	// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000D1D87 File Offset: 0x000CFF87
	// (set) Token: 0x060017B9 RID: 6073 RVA: 0x000D1D8F File Offset: 0x000CFF8F
	public int Seat
	{
		get
		{
			return this.seat;
		}
		set
		{
			this.seat = value;
		}
	}

	// Token: 0x17000445 RID: 1093
	// (get) Token: 0x060017BA RID: 6074 RVA: 0x000D1D98 File Offset: 0x000CFF98
	public ClubType Club
	{
		get
		{
			return this.club;
		}
	}

	// Token: 0x17000446 RID: 1094
	// (get) Token: 0x060017BB RID: 6075 RVA: 0x000D1DA0 File Offset: 0x000CFFA0
	// (set) Token: 0x060017BC RID: 6076 RVA: 0x000D1DA8 File Offset: 0x000CFFA8
	public PersonaType Persona
	{
		get
		{
			return this.persona;
		}
		set
		{
			this.persona = value;
		}
	}

	// Token: 0x17000447 RID: 1095
	// (get) Token: 0x060017BD RID: 6077 RVA: 0x000D1DB1 File Offset: 0x000CFFB1
	public int Crush
	{
		get
		{
			return this.crush;
		}
	}

	// Token: 0x17000448 RID: 1096
	// (get) Token: 0x060017BE RID: 6078 RVA: 0x000D1DB9 File Offset: 0x000CFFB9
	// (set) Token: 0x060017BF RID: 6079 RVA: 0x000D1DC1 File Offset: 0x000CFFC1
	public float BreastSize
	{
		get
		{
			return this.breastSize;
		}
		set
		{
			this.breastSize = value;
		}
	}

	// Token: 0x17000449 RID: 1097
	// (get) Token: 0x060017C0 RID: 6080 RVA: 0x000D1DCA File Offset: 0x000CFFCA
	// (set) Token: 0x060017C1 RID: 6081 RVA: 0x000D1DD2 File Offset: 0x000CFFD2
	public int Strength
	{
		get
		{
			return this.strength;
		}
		set
		{
			this.strength = value;
		}
	}

	// Token: 0x1700044A RID: 1098
	// (get) Token: 0x060017C2 RID: 6082 RVA: 0x000D1DDB File Offset: 0x000CFFDB
	// (set) Token: 0x060017C3 RID: 6083 RVA: 0x000D1DE3 File Offset: 0x000CFFE3
	public string Hairstyle
	{
		get
		{
			return this.hairstyle;
		}
		set
		{
			this.hairstyle = value;
		}
	}

	// Token: 0x1700044B RID: 1099
	// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000D1DEC File Offset: 0x000CFFEC
	public string Color
	{
		get
		{
			return this.color;
		}
	}

	// Token: 0x1700044C RID: 1100
	// (get) Token: 0x060017C5 RID: 6085 RVA: 0x000D1DF4 File Offset: 0x000CFFF4
	public string Eyes
	{
		get
		{
			return this.eyes;
		}
	}

	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x060017C6 RID: 6086 RVA: 0x000D1DFC File Offset: 0x000CFFFC
	public string EyeType
	{
		get
		{
			return this.eyeType;
		}
	}

	// Token: 0x1700044E RID: 1102
	// (get) Token: 0x060017C7 RID: 6087 RVA: 0x000D1E04 File Offset: 0x000D0004
	public string Stockings
	{
		get
		{
			return this.stockings;
		}
	}

	// Token: 0x1700044F RID: 1103
	// (get) Token: 0x060017C8 RID: 6088 RVA: 0x000D1E0C File Offset: 0x000D000C
	// (set) Token: 0x060017C9 RID: 6089 RVA: 0x000D1E14 File Offset: 0x000D0014
	public string Accessory
	{
		get
		{
			return this.accessory;
		}
		set
		{
			this.accessory = value;
		}
	}

	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x060017CA RID: 6090 RVA: 0x000D1E1D File Offset: 0x000D001D
	public string Info
	{
		get
		{
			return this.info;
		}
	}

	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x060017CB RID: 6091 RVA: 0x000D1E25 File Offset: 0x000D0025
	public ScheduleBlock[] ScheduleBlocks
	{
		get
		{
			return this.scheduleBlocks;
		}
	}

	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x060017CC RID: 6092 RVA: 0x000D1E2D File Offset: 0x000D002D
	public bool Success
	{
		get
		{
			return this.success;
		}
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x000D1E38 File Offset: 0x000D0038
	private static float[] ConstructTempFloatArray(string str)
	{
		string[] array = str.Split(new char[]
		{
			'_'
		});
		float[] array2 = new float[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			float num;
			if (float.TryParse(array[i], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num))
			{
				array2[i] = num;
			}
		}
		return array2;
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x000D1E89 File Offset: 0x000D0089
	private static string[] ConstructTempStringArray(string str)
	{
		return str.Split(new char[]
		{
			'_'
		});
	}

	// Token: 0x040021E6 RID: 8678
	[SerializeField]
	private string name;

	// Token: 0x040021E7 RID: 8679
	[SerializeField]
	private int gender;

	// Token: 0x040021E8 RID: 8680
	[SerializeField]
	private int classID;

	// Token: 0x040021E9 RID: 8681
	[SerializeField]
	private int seat;

	// Token: 0x040021EA RID: 8682
	[SerializeField]
	private ClubType club;

	// Token: 0x040021EB RID: 8683
	[SerializeField]
	private PersonaType persona;

	// Token: 0x040021EC RID: 8684
	[SerializeField]
	private int crush;

	// Token: 0x040021ED RID: 8685
	[SerializeField]
	private float breastSize;

	// Token: 0x040021EE RID: 8686
	[SerializeField]
	private int strength;

	// Token: 0x040021EF RID: 8687
	[SerializeField]
	private string hairstyle;

	// Token: 0x040021F0 RID: 8688
	[SerializeField]
	private string color;

	// Token: 0x040021F1 RID: 8689
	[SerializeField]
	private string eyes;

	// Token: 0x040021F2 RID: 8690
	[SerializeField]
	private string eyeType;

	// Token: 0x040021F3 RID: 8691
	[SerializeField]
	private string stockings;

	// Token: 0x040021F4 RID: 8692
	[SerializeField]
	private string accessory;

	// Token: 0x040021F5 RID: 8693
	[SerializeField]
	private string info;

	// Token: 0x040021F6 RID: 8694
	[SerializeField]
	private ScheduleBlock[] scheduleBlocks;

	// Token: 0x040021F7 RID: 8695
	[SerializeField]
	private bool success;
}
