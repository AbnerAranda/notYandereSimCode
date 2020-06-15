using System;
using System.Collections.Generic;
using System.IO;
using JsonFx.Json;
using UnityEngine;

// Token: 0x02000311 RID: 785
public abstract class JsonData
{
	// Token: 0x1700043F RID: 1087
	// (get) Token: 0x060017AE RID: 6062 RVA: 0x000D1A94 File Offset: 0x000CFC94
	protected static string FolderPath
	{
		get
		{
			return Path.Combine(Application.streamingAssetsPath, "JSON");
		}
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x000D1AA5 File Offset: 0x000CFCA5
	protected static Dictionary<string, object>[] Deserialize(string filename)
	{
		return JsonReader.Deserialize<Dictionary<string, object>[]>(File.ReadAllText(filename));
	}
}
