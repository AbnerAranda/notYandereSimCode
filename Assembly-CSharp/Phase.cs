using System;
using UnityEngine;

// Token: 0x0200035E RID: 862
[Serializable]
public class Phase
{
	// Token: 0x060018D2 RID: 6354 RVA: 0x000E5D1F File Offset: 0x000E3F1F
	public Phase(PhaseOfDay type)
	{
		this.type = type;
	}

	// Token: 0x1700045B RID: 1115
	// (get) Token: 0x060018D3 RID: 6355 RVA: 0x000E5D2E File Offset: 0x000E3F2E
	public PhaseOfDay Type
	{
		get
		{
			return this.type;
		}
	}

	// Token: 0x040024FF RID: 9471
	[SerializeField]
	private PhaseOfDay type;
}
