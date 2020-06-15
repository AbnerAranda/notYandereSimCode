using System;
using UnityEngine;

// Token: 0x0200028B RID: 651
[Serializable]
public class RivalData
{
	// Token: 0x060013CB RID: 5067 RVA: 0x000AD71C File Offset: 0x000AB91C
	public RivalData(int week)
	{
		this.week = week;
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x060013CC RID: 5068 RVA: 0x000AD72B File Offset: 0x000AB92B
	public int Week
	{
		get
		{
			return this.week;
		}
	}

	// Token: 0x04001BA2 RID: 7074
	[SerializeField]
	private int week;
}
