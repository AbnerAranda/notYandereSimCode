using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200027C RID: 636
public class StudentEditorScript : MonoBehaviour
{
	// Token: 0x06001392 RID: 5010 RVA: 0x000A8E38 File Offset: 0x000A7038
	private void Awake()
	{
		Dictionary<string, object>[] array = EditorManagerScript.DeserializeJson("Students.json");
		this.students = new StudentEditorScript.StudentData[array.Length];
		for (int i = 0; i < this.students.Length; i++)
		{
			this.students[i] = StudentEditorScript.StudentData.Deserialize(array[i]);
		}
		Array.Sort<StudentEditorScript.StudentData>(this.students, (StudentEditorScript.StudentData a, StudentEditorScript.StudentData b) => a.id - b.id);
		for (int j = 0; j < this.students.Length; j++)
		{
			StudentEditorScript.StudentData studentData = this.students[j];
			UILabel uilabel = UnityEngine.Object.Instantiate<UILabel>(this.studentLabelTemplate, this.listLabelsOrigin);
			uilabel.text = "(" + studentData.id.ToString() + ") " + studentData.name;
			Transform transform = uilabel.transform;
			transform.localPosition = new Vector3(transform.localPosition.x + (float)(uilabel.width / 2), transform.localPosition.y - (float)(j * uilabel.height), transform.localPosition.z);
			uilabel.gameObject.SetActive(true);
		}
		this.studentIndex = 0;
		this.bodyLabel.text = StudentEditorScript.GetStudentText(this.students[this.studentIndex]);
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x000A8F92 File Offset: 0x000A7192
	private void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.UpdateButtons();
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x000A8FD0 File Offset: 0x000A71D0
	private static ScheduleBlock[] DeserializeScheduleBlocks(Dictionary<string, object> dict)
	{
		string[] array = TFUtils.LoadString(dict, "ScheduleTime").Split(new char[]
		{
			'_'
		});
		string[] array2 = TFUtils.LoadString(dict, "ScheduleDestination").Split(new char[]
		{
			'_'
		});
		string[] array3 = TFUtils.LoadString(dict, "ScheduleAction").Split(new char[]
		{
			'_'
		});
		ScheduleBlock[] array4 = new ScheduleBlock[array.Length];
		for (int i = 0; i < array4.Length; i++)
		{
			array4[i] = new ScheduleBlock(float.Parse(array[i]), array2[i], array3[i]);
		}
		return array4;
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x000A9068 File Offset: 0x000A7268
	private static string GetStudentText(StudentEditorScript.StudentData data)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(string.Concat(new object[]
		{
			data.name,
			" (",
			data.id,
			"):\n"
		}));
		stringBuilder.Append("- Gender: " + (data.isMale ? "Male" : "Female") + "\n");
		stringBuilder.Append("- Class: " + data.attendanceInfo.classNumber + "\n");
		stringBuilder.Append("- Seat: " + data.attendanceInfo.seatNumber + "\n");
		stringBuilder.Append("- Club: " + data.attendanceInfo.club + "\n");
		stringBuilder.Append("- Persona: " + data.personality.persona + "\n");
		stringBuilder.Append("- Crush: " + data.personality.crush + "\n");
		stringBuilder.Append("- Breast size: " + data.cosmetics.breastSize + "\n");
		stringBuilder.Append("- Strength: " + data.stats.strength + "\n");
		stringBuilder.Append("- Hairstyle: " + data.cosmetics.hairstyle + "\n");
		stringBuilder.Append("- Color: " + data.cosmetics.color + "\n");
		stringBuilder.Append("- Eyes: " + data.cosmetics.eyes + "\n");
		stringBuilder.Append("- Stockings: " + data.cosmetics.stockings + "\n");
		stringBuilder.Append("- Accessory: " + data.cosmetics.accessory + "\n");
		stringBuilder.Append("- Schedule blocks: ");
		foreach (ScheduleBlock scheduleBlock in data.scheduleBlocks)
		{
			stringBuilder.Append(string.Concat(new object[]
			{
				"[",
				scheduleBlock.time,
				", ",
				scheduleBlock.destination,
				", ",
				scheduleBlock.action,
				"]"
			}));
		}
		stringBuilder.Append("\n");
		stringBuilder.Append("- Info: \"" + data.info + "\"\n");
		return stringBuilder.ToString();
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000A9334 File Offset: 0x000A7534
	private void HandleInput()
	{
		if (Input.GetButtonDown("B"))
		{
			this.mainPanel.gameObject.SetActive(true);
			this.studentPanel.gameObject.SetActive(false);
		}
		int num = 0;
		int num2 = this.students.Length - 1;
		bool tappedUp = this.inputManager.TappedUp;
		bool tappedDown = this.inputManager.TappedDown;
		if (tappedUp)
		{
			this.studentIndex = ((this.studentIndex > num) ? (this.studentIndex - 1) : num2);
		}
		else if (tappedDown)
		{
			this.studentIndex = ((this.studentIndex < num2) ? (this.studentIndex + 1) : num);
		}
		if (tappedUp || tappedDown)
		{
			this.bodyLabel.text = StudentEditorScript.GetStudentText(this.students[this.studentIndex]);
		}
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x000A93F0 File Offset: 0x000A75F0
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001AD9 RID: 6873
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001ADA RID: 6874
	[SerializeField]
	private UIPanel studentPanel;

	// Token: 0x04001ADB RID: 6875
	[SerializeField]
	private UILabel bodyLabel;

	// Token: 0x04001ADC RID: 6876
	[SerializeField]
	private Transform listLabelsOrigin;

	// Token: 0x04001ADD RID: 6877
	[SerializeField]
	private UILabel studentLabelTemplate;

	// Token: 0x04001ADE RID: 6878
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001ADF RID: 6879
	private StudentEditorScript.StudentData[] students;

	// Token: 0x04001AE0 RID: 6880
	private int studentIndex;

	// Token: 0x04001AE1 RID: 6881
	private InputManagerScript inputManager;

	// Token: 0x020006B4 RID: 1716
	private class StudentAttendanceInfo
	{
		// Token: 0x06002BD3 RID: 11219 RVA: 0x001CAEDE File Offset: 0x001C90DE
		public static StudentEditorScript.StudentAttendanceInfo Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentAttendanceInfo
			{
				classNumber = TFUtils.LoadInt(dict, "Class"),
				seatNumber = TFUtils.LoadInt(dict, "Seat"),
				club = TFUtils.LoadInt(dict, "Club")
			};
		}

		// Token: 0x04004750 RID: 18256
		public int classNumber;

		// Token: 0x04004751 RID: 18257
		public int seatNumber;

		// Token: 0x04004752 RID: 18258
		public int club;
	}

	// Token: 0x020006B5 RID: 1717
	private class StudentPersonality
	{
		// Token: 0x06002BD5 RID: 11221 RVA: 0x001CAF18 File Offset: 0x001C9118
		public static StudentEditorScript.StudentPersonality Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentPersonality
			{
				persona = (PersonaType)TFUtils.LoadInt(dict, "Persona"),
				crush = TFUtils.LoadInt(dict, "Crush")
			};
		}

		// Token: 0x04004753 RID: 18259
		public PersonaType persona;

		// Token: 0x04004754 RID: 18260
		public int crush;
	}

	// Token: 0x020006B6 RID: 1718
	private class StudentStats
	{
		// Token: 0x06002BD7 RID: 11223 RVA: 0x001CAF41 File Offset: 0x001C9141
		public static StudentEditorScript.StudentStats Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentStats
			{
				strength = TFUtils.LoadInt(dict, "Strength")
			};
		}

		// Token: 0x04004755 RID: 18261
		public int strength;
	}

	// Token: 0x020006B7 RID: 1719
	private class StudentCosmetics
	{
		// Token: 0x06002BD9 RID: 11225 RVA: 0x001CAF5C File Offset: 0x001C915C
		public static StudentEditorScript.StudentCosmetics Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentCosmetics
			{
				breastSize = TFUtils.LoadFloat(dict, "BreastSize"),
				hairstyle = TFUtils.LoadString(dict, "Hairstyle"),
				color = TFUtils.LoadString(dict, "Color"),
				eyes = TFUtils.LoadString(dict, "Eyes"),
				stockings = TFUtils.LoadString(dict, "Stockings"),
				accessory = TFUtils.LoadString(dict, "Accessory")
			};
		}

		// Token: 0x04004756 RID: 18262
		public float breastSize;

		// Token: 0x04004757 RID: 18263
		public string hairstyle;

		// Token: 0x04004758 RID: 18264
		public string color;

		// Token: 0x04004759 RID: 18265
		public string eyes;

		// Token: 0x0400475A RID: 18266
		public string stockings;

		// Token: 0x0400475B RID: 18267
		public string accessory;
	}

	// Token: 0x020006B8 RID: 1720
	private class StudentData
	{
		// Token: 0x06002BDB RID: 11227 RVA: 0x001CAFD4 File Offset: 0x001C91D4
		public static StudentEditorScript.StudentData Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentData
			{
				id = TFUtils.LoadInt(dict, "ID"),
				name = TFUtils.LoadString(dict, "Name"),
				isMale = (TFUtils.LoadInt(dict, "Gender") == 1),
				attendanceInfo = StudentEditorScript.StudentAttendanceInfo.Deserialize(dict),
				personality = StudentEditorScript.StudentPersonality.Deserialize(dict),
				stats = StudentEditorScript.StudentStats.Deserialize(dict),
				cosmetics = StudentEditorScript.StudentCosmetics.Deserialize(dict),
				scheduleBlocks = StudentEditorScript.DeserializeScheduleBlocks(dict),
				info = TFUtils.LoadString(dict, "Info")
			};
		}

		// Token: 0x0400475C RID: 18268
		public int id;

		// Token: 0x0400475D RID: 18269
		public string name;

		// Token: 0x0400475E RID: 18270
		public bool isMale;

		// Token: 0x0400475F RID: 18271
		public StudentEditorScript.StudentAttendanceInfo attendanceInfo;

		// Token: 0x04004760 RID: 18272
		public StudentEditorScript.StudentPersonality personality;

		// Token: 0x04004761 RID: 18273
		public StudentEditorScript.StudentStats stats;

		// Token: 0x04004762 RID: 18274
		public StudentEditorScript.StudentCosmetics cosmetics;

		// Token: 0x04004763 RID: 18275
		public ScheduleBlock[] scheduleBlocks;

		// Token: 0x04004764 RID: 18276
		public string info;
	}
}
