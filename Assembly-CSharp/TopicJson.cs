using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000314 RID: 788
[Serializable]
public class TopicJson : JsonData
{
	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x060017D5 RID: 6101 RVA: 0x000D1F2D File Offset: 0x000D012D
	public static string FilePath
	{
		get
		{
			return Path.Combine(JsonData.FolderPath, "Topics.json");
		}
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x000D1F40 File Offset: 0x000D0140
	public static TopicJson[] LoadFromJson(string path)
	{
		TopicJson[] array = new TopicJson[101];
		foreach (Dictionary<string, object> d in JsonData.Deserialize(path))
		{
			int num = TFUtils.LoadInt(d, "ID");
			if (num == 0)
			{
				break;
			}
			array[num] = new TopicJson();
			TopicJson topicJson = array[num];
			topicJson.topics = new int[26];
			for (int j = 1; j <= 25; j++)
			{
				topicJson.topics[j] = TFUtils.LoadInt(d, j.ToString());
			}
		}
		return array;
	}

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x060017D7 RID: 6103 RVA: 0x000D1FC5 File Offset: 0x000D01C5
	public int[] Topics
	{
		get
		{
			return this.topics;
		}
	}

	// Token: 0x040021FA RID: 8698
	[SerializeField]
	private int[] topics;
}
