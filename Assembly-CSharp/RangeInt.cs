using System;
using UnityEngine;

// Token: 0x02000447 RID: 1095
[Serializable]
public class RangeInt
{
	// Token: 0x06001CDB RID: 7387 RVA: 0x00158D00 File Offset: 0x00156F00
	public RangeInt(int value, int min, int max)
	{
		this.value = value;
		this.min = min;
		this.max = max;
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x00158D1D File Offset: 0x00156F1D
	public RangeInt(int min, int max) : this(min, min, max)
	{
	}

	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x06001CDD RID: 7389 RVA: 0x00158D28 File Offset: 0x00156F28
	// (set) Token: 0x06001CDE RID: 7390 RVA: 0x00158D30 File Offset: 0x00156F30
	public int Value
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x06001CDF RID: 7391 RVA: 0x00158D39 File Offset: 0x00156F39
	public int Min
	{
		get
		{
			return this.min;
		}
	}

	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00158D41 File Offset: 0x00156F41
	public int Max
	{
		get
		{
			return this.max;
		}
	}

	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00158D49 File Offset: 0x00156F49
	public int Next
	{
		get
		{
			if (this.value != this.max)
			{
				return this.value + 1;
			}
			return this.min;
		}
	}

	// Token: 0x17000488 RID: 1160
	// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00158D68 File Offset: 0x00156F68
	public int Previous
	{
		get
		{
			if (this.value != this.min)
			{
				return this.value - 1;
			}
			return this.max;
		}
	}

	// Token: 0x04003665 RID: 13925
	[SerializeField]
	private int value;

	// Token: 0x04003666 RID: 13926
	[SerializeField]
	private int min;

	// Token: 0x04003667 RID: 13927
	[SerializeField]
	private int max;
}
