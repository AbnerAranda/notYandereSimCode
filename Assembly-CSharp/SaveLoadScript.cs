using System;
using System.IO;
using UnityEngine;

// Token: 0x020003C9 RID: 969
public class SaveLoadScript : MonoBehaviour
{
	// Token: 0x06001A57 RID: 6743 RVA: 0x00102BF4 File Offset: 0x00100DF4
	private void DetermineFilePath()
	{
		this.SaveProfile = GameGlobals.Profile;
		this.SaveSlot = PlayerPrefs.GetInt("SaveSlot");
		this.SaveFilePath = string.Concat(new object[]
		{
			Application.streamingAssetsPath,
			"/SaveData/Profile_",
			this.SaveProfile,
			"/Slot_",
			this.SaveSlot,
			"/Student_",
			this.Student.StudentID,
			"_Data.txt"
		});
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x00102C84 File Offset: 0x00100E84
	public void SaveData()
	{
		this.DetermineFilePath();
		this.SerializedData = JsonUtility.ToJson(this.Student);
		File.WriteAllText(this.SaveFilePath, this.SerializedData);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_posX"
		}), base.transform.position.x);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_posY"
		}), base.transform.position.y);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_posZ"
		}), base.transform.position.z);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_rotX"
		}), base.transform.eulerAngles.x);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_rotY"
		}), base.transform.eulerAngles.y);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_rotZ"
		}), base.transform.eulerAngles.z);
	}

	// Token: 0x06001A59 RID: 6745 RVA: 0x00102F54 File Offset: 0x00101154
	public void LoadData()
	{
		this.DetermineFilePath();
		if (File.Exists(this.SaveFilePath))
		{
			base.transform.position = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"_Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_posX"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"_Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_posY"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"_Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_posZ"
			})));
			base.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_rotX"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_rotY"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_rotZ"
			})));
			JsonUtility.FromJsonOverwrite(File.ReadAllText(this.SaveFilePath), this.Student);
		}
	}

	// Token: 0x040029A3 RID: 10659
	public StudentScript Student;

	// Token: 0x040029A4 RID: 10660
	public string SerializedData;

	// Token: 0x040029A5 RID: 10661
	public string SaveFilePath;

	// Token: 0x040029A6 RID: 10662
	public int SaveProfile;

	// Token: 0x040029A7 RID: 10663
	public int SaveSlot;
}
