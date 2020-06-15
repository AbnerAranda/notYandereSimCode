using System;
using UnityEngine;

// Token: 0x0200028F RID: 655
[Serializable]
public class WitnessMemory
{
	// Token: 0x060013D1 RID: 5073 RVA: 0x000AD770 File Offset: 0x000AB970
	public WitnessMemory()
	{
		this.memories = new float[Enum.GetValues(typeof(WitnessMemoryType)).Length];
		for (int i = 0; i < this.memories.Length; i++)
		{
			this.memories[i] = float.PositiveInfinity;
		}
		this.memorySpan = 1800f;
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x000AD7CD File Offset: 0x000AB9CD
	public bool Remembers(WitnessMemoryType type)
	{
		return this.memories[(int)type] < this.memorySpan;
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x000AD7DF File Offset: 0x000AB9DF
	public void Refresh(WitnessMemoryType type)
	{
		this.memories[(int)type] = 0f;
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x000AD7F0 File Offset: 0x000AB9F0
	public void Tick(float dt)
	{
		for (int i = 0; i < this.memories.Length; i++)
		{
			this.memories[i] += dt;
		}
	}

	// Token: 0x04001BAF RID: 7087
	[SerializeField]
	private float[] memories;

	// Token: 0x04001BB0 RID: 7088
	[SerializeField]
	private float memorySpan;

	// Token: 0x04001BB1 RID: 7089
	private const float LongMemorySpan = 28800f;

	// Token: 0x04001BB2 RID: 7090
	private const float MediumMemorySpan = 7200f;

	// Token: 0x04001BB3 RID: 7091
	private const float ShortMemorySpan = 1800f;

	// Token: 0x04001BB4 RID: 7092
	private const float VeryShortMemorySpan = 120f;
}
