using System;

// Token: 0x02000291 RID: 657
[Serializable]
public class ScheduleBlock
{
	// Token: 0x060013DA RID: 5082 RVA: 0x000AE447 File Offset: 0x000AC647
	public ScheduleBlock(float time, string destination, string action)
	{
		this.time = time;
		this.destination = destination;
		this.action = action;
	}

	// Token: 0x04001BD1 RID: 7121
	public float time;

	// Token: 0x04001BD2 RID: 7122
	public string destination;

	// Token: 0x04001BD3 RID: 7123
	public string action;
}
