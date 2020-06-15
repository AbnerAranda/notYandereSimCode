using System;
using UnityEngine;

// Token: 0x0200028D RID: 653
[Serializable]
public class Stance
{
	// Token: 0x060013CD RID: 5069 RVA: 0x000AD733 File Offset: 0x000AB933
	public Stance(StanceType initialStance)
	{
		this.current = initialStance;
		this.previous = initialStance;
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x060013CE RID: 5070 RVA: 0x000AD749 File Offset: 0x000AB949
	// (set) Token: 0x060013CF RID: 5071 RVA: 0x000AD751 File Offset: 0x000AB951
	public StanceType Current
	{
		get
		{
			return this.current;
		}
		set
		{
			this.previous = this.current;
			this.current = value;
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x060013D0 RID: 5072 RVA: 0x000AD766 File Offset: 0x000AB966
	public StanceType Previous
	{
		get
		{
			return this.previous;
		}
	}

	// Token: 0x04001BA7 RID: 7079
	[SerializeField]
	private StanceType current;

	// Token: 0x04001BA8 RID: 7080
	[SerializeField]
	private StanceType previous;
}
