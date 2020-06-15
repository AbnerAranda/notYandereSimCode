using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004B1 RID: 1201
	public static class PlayerPrefsHelper
	{
		// Token: 0x06001E70 RID: 7792 RVA: 0x000B569C File Offset: 0x000B389C
		public static void SetBool(string name, bool flag)
		{
			PlayerPrefs.SetInt(name, flag ? 1 : 0);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0017E6C1 File Offset: 0x0017C8C1
		public static bool GetBool(string name)
		{
			return PlayerPrefs.GetInt(name) == 1;
		}
	}
}
