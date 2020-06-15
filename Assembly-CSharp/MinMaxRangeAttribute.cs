using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class MinMaxRangeAttribute : PropertyAttribute
{
	// Token: 0x060003C2 RID: 962 RVA: 0x00022B04 File Offset: 0x00020D04
	public MinMaxRangeAttribute(float minLimit, float maxLimit)
	{
		this.minLimit = minLimit;
		this.maxLimit = maxLimit;
	}

	// Token: 0x040004CF RID: 1231
	public float minLimit;

	// Token: 0x040004D0 RID: 1232
	public float maxLimit;
}
