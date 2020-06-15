using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002BB RID: 699
public static class KeysHelper
{
	// Token: 0x06001472 RID: 5234 RVA: 0x000B5AF1 File Offset: 0x000B3CF1
	public static int[] GetIntegerKeys(string key)
	{
		return Array.ConvertAll<string, int>(KeysHelper.SplitList(KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key))), (string str) => int.Parse(str));
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x000B5B27 File Offset: 0x000B3D27
	public static string[] GetStringKeys(string key)
	{
		return KeysHelper.SplitList(KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key)));
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x000B5B39 File Offset: 0x000B3D39
	public static T[] GetEnumKeys<T>(string key) where T : struct, IConvertible
	{
		return Array.ConvertAll<string, T>(KeysHelper.SplitList(KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key))), (string str) => (T)((object)Enum.Parse(typeof(T), str)));
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x000B5B70 File Offset: 0x000B3D70
	public static KeyValuePair<T, U>[] GetKeys<T, U>(string key) where T : struct where U : struct
	{
		string[] array = KeysHelper.SplitList(KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key)));
		KeyValuePair<T, U>[] array2 = new KeyValuePair<T, U>[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array3 = array[i].Split(new char[]
			{
				'^'
			});
			array2[i] = new KeyValuePair<T, U>((T)((object)int.Parse(array3[0])), (U)((object)int.Parse(array3[1])));
		}
		return array2;
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x000B5BEC File Offset: 0x000B3DEC
	public static void AddIfMissing(string key, string id)
	{
		string keyListKey = KeysHelper.GetKeyListKey(key);
		string keyList = KeysHelper.GetKeyList(keyListKey);
		if (!KeysHelper.HasKey(KeysHelper.SplitList(keyList), id))
		{
			KeysHelper.AppendKey(keyListKey, keyList, id);
		}
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x000B5C1D File Offset: 0x000B3E1D
	public static void Delete(string key)
	{
		Globals.Delete(KeysHelper.GetKeyListKey(key));
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x000B5C2A File Offset: 0x000B3E2A
	private static string GetKeyListKey(string key)
	{
		return key + "Keys";
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x000B5C37 File Offset: 0x000B3E37
	private static string GetKeyList(string keyListKey)
	{
		return PlayerPrefs.GetString(keyListKey);
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000B5C3F File Offset: 0x000B3E3F
	private static string[] SplitList(string keyList)
	{
		if (keyList.Length <= 0)
		{
			return new string[0];
		}
		return keyList.Split(new char[]
		{
			'|'
		});
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x000B5C62 File Offset: 0x000B3E62
	private static int FindKey(string[] keyListStrings, string key)
	{
		return Array.IndexOf<string>(keyListStrings, key);
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x000B5C6B File Offset: 0x000B3E6B
	private static bool HasKey(string[] keyListStrings, string key)
	{
		return KeysHelper.FindKey(keyListStrings, key) > -1;
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x000B5C78 File Offset: 0x000B3E78
	private static void AppendKey(string keyListKey, string keyList, string key)
	{
		string value = (keyList.Length == 0) ? (keyList + key) : (keyList + "|" + key);
		PlayerPrefs.SetString(keyListKey, value);
	}

	// Token: 0x04001D37 RID: 7479
	private const string KeyListPrefix = "Keys";

	// Token: 0x04001D38 RID: 7480
	private const char KeyListSeparator = '|';

	// Token: 0x04001D39 RID: 7481
	public const char PairSeparator = '^';
}
