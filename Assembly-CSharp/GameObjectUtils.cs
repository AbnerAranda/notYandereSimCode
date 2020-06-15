using System;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public static class GameObjectUtils
{
	// Token: 0x06001CD9 RID: 7385 RVA: 0x00158C38 File Offset: 0x00156E38
	public static void SetLayerRecursively(GameObject obj, int newLayer)
	{
		obj.layer = newLayer;
		foreach (object obj2 in obj.transform)
		{
			GameObjectUtils.SetLayerRecursively(((Transform)obj2).gameObject, newLayer);
		}
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x00158C9C File Offset: 0x00156E9C
	public static void SetTagRecursively(GameObject obj, string newTag)
	{
		obj.tag = newTag;
		foreach (object obj2 in obj.transform)
		{
			GameObjectUtils.SetTagRecursively(((Transform)obj2).gameObject, newTag);
		}
	}
}
