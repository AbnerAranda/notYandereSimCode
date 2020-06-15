using System;

// Token: 0x0200043E RID: 1086
[Serializable]
public class ScheduleBlockArrayWrapper : ArrayWrapper<ScheduleBlock>
{
	// Token: 0x06001CAF RID: 7343 RVA: 0x0015852D File Offset: 0x0015672D
	public ScheduleBlockArrayWrapper(int size) : base(size)
	{
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x00158536 File Offset: 0x00156736
	public ScheduleBlockArrayWrapper(ScheduleBlock[] elements) : base(elements)
	{
	}
}
