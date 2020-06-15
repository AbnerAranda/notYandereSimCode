using System;
using System.Collections.Generic;

// Token: 0x020002C1 RID: 705
public static class ConversationGlobals
{
	// Token: 0x060014D3 RID: 5331 RVA: 0x000B7119 File Offset: 0x000B5319
	public static bool GetTopicDiscovered(int topicID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscovered_",
			topicID.ToString()
		}));
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x000B7154 File Offset: 0x000B5354
	public static void SetTopicDiscovered(int topicID, bool value)
	{
		string text = topicID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TopicDiscovered_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscovered_",
			text
		}), value);
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x000B71BA File Offset: 0x000B53BA
	public static int[] KeysOfTopicDiscovered()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TopicDiscovered_");
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x000B71DC File Offset: 0x000B53DC
	public static bool GetTopicLearnedByStudent(int topicID, int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicLearnedByStudent_",
			topicID.ToString(),
			"_",
			studentID.ToString()
		}));
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x000B7234 File Offset: 0x000B5434
	public static void SetTopicLearnedByStudent(int topicID, int studentID, bool value)
	{
		string text = topicID.ToString();
		string text2 = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TopicLearnedByStudent_", text + "^" + text2);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicLearnedByStudent_",
			text,
			"_",
			text2
		}), value);
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x000B72BC File Offset: 0x000B54BC
	public static IntAndIntPair[] KeysOfTopicLearnedByStudent()
	{
		KeyValuePair<int, int>[] keys = KeysHelper.GetKeys<int, int>("Profile_" + GameGlobals.Profile + "_TopicLearnedByStudent_");
		IntAndIntPair[] array = new IntAndIntPair[keys.Length];
		for (int i = 0; i < keys.Length; i++)
		{
			KeyValuePair<int, int> keyValuePair = keys[i];
			array[i] = new IntAndIntPair(keyValuePair.Key, keyValuePair.Value);
		}
		return array;
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000B7320 File Offset: 0x000B5520
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TopicDiscovered_", ConversationGlobals.KeysOfTopicDiscovered());
		foreach (IntAndIntPair intAndIntPair in ConversationGlobals.KeysOfTopicLearnedByStudent())
		{
			Globals.Delete(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_TopicLearnedByStudent_",
				intAndIntPair.first.ToString(),
				"_",
				intAndIntPair.second.ToString()
			}));
		}
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_TopicLearnedByStudent_");
	}

	// Token: 0x04001D5A RID: 7514
	private const string Str_TopicDiscovered = "TopicDiscovered_";

	// Token: 0x04001D5B RID: 7515
	private const string Str_TopicLearnedByStudent = "TopicLearnedByStudent_";
}
