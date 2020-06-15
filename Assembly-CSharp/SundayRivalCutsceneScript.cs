using System;
using UnityEngine;

// Token: 0x02000415 RID: 1045
public class SundayRivalCutsceneScript : MonoBehaviour
{
	// Token: 0x06001C14 RID: 7188 RVA: 0x0014A7E7 File Offset: 0x001489E7
	private void Start()
	{
		if (DateGlobals.Weekday != DayOfWeek.Sunday)
		{
			base.gameObject.SetActive(false);
		}
	}
}
