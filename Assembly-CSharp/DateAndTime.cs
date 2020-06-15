using System;
using UnityEngine;

// Token: 0x02000445 RID: 1093
[Serializable]
public class DateAndTime
{
	// Token: 0x06001CD1 RID: 7377 RVA: 0x00158B53 File Offset: 0x00156D53
	public DateAndTime(int week, DayOfWeek weekday, Clock clock)
	{
		this.week = week;
		this.weekday = weekday;
		this.clock = clock;
	}

	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00158B70 File Offset: 0x00156D70
	public int Week
	{
		get
		{
			return this.week;
		}
	}

	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00158B78 File Offset: 0x00156D78
	public DayOfWeek Weekday
	{
		get
		{
			return this.weekday;
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x00158B80 File Offset: 0x00156D80
	public Clock Clock
	{
		get
		{
			return this.clock;
		}
	}

	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00158B88 File Offset: 0x00156D88
	public int TotalSeconds
	{
		get
		{
			int num = this.week * 604800;
			int num2 = (int)(this.weekday * (DayOfWeek)86400);
			int totalSeconds = this.clock.TotalSeconds;
			return num + num2 + totalSeconds;
		}
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x00158BBE File Offset: 0x00156DBE
	public void IncrementWeek()
	{
		this.week++;
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x00158BD0 File Offset: 0x00156DD0
	public void IncrementWeekday()
	{
		int num = (int)this.weekday;
		num++;
		if (num == 7)
		{
			this.IncrementWeek();
			num = 0;
		}
		this.weekday = (DayOfWeek)num;
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x00158BFC File Offset: 0x00156DFC
	public void Tick(float dt)
	{
		int hours = this.clock.Hours24;
		this.clock.Tick(dt);
		if (this.clock.Hours24 < hours)
		{
			this.IncrementWeekday();
		}
	}

	// Token: 0x04003662 RID: 13922
	[SerializeField]
	private int week;

	// Token: 0x04003663 RID: 13923
	[SerializeField]
	private DayOfWeek weekday;

	// Token: 0x04003664 RID: 13924
	[SerializeField]
	private Clock clock;
}
