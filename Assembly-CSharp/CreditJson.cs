using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000313 RID: 787
[Serializable]
public class CreditJson : JsonData
{
	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000D1EA4 File Offset: 0x000D00A4
	public static string FilePath
	{
		get
		{
			return Path.Combine(JsonData.FolderPath, "Credits.json");
		}
	}

	// Token: 0x060017D1 RID: 6097 RVA: 0x000D1EB8 File Offset: 0x000D00B8
	public static CreditJson[] LoadFromJson(string path)
	{
		List<CreditJson> list = new List<CreditJson>();
		foreach (Dictionary<string, object> dictionary in JsonData.Deserialize(path))
		{
			list.Add(new CreditJson
			{
				name = TFUtils.LoadString(dictionary, "Name"),
				size = TFUtils.LoadInt(dictionary, "Size")
			});
		}
		return list.ToArray();
	}

	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000D1F1D File Offset: 0x000D011D
	public string Name
	{
		get
		{
			return this.name;
		}
	}

	// Token: 0x17000455 RID: 1109
	// (get) Token: 0x060017D3 RID: 6099 RVA: 0x000D1F25 File Offset: 0x000D0125
	public int Size
	{
		get
		{
			return this.size;
		}
	}

	// Token: 0x040021F8 RID: 8696
	[SerializeField]
	private string name;

	// Token: 0x040021F9 RID: 8697
	[SerializeField]
	private int size;
}
