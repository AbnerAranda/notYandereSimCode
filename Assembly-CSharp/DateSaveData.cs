using System;

// Token: 0x020003B6 RID: 950
[Serializable]
public class DateSaveData
{
	// Token: 0x06001A13 RID: 6675 RVA: 0x000FFC3A File Offset: 0x000FDE3A
	public static DateSaveData ReadFromGlobals()
	{
		return new DateSaveData
		{
			week = DateGlobals.Week,
			weekday = DateGlobals.Weekday
		};
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x000FFC57 File Offset: 0x000FDE57
	public static void WriteToGlobals(DateSaveData data)
	{
		DateGlobals.Week = data.week;
		DateGlobals.Weekday = data.weekday;
	}

	// Token: 0x040028F7 RID: 10487
	public int week;

	// Token: 0x040028F8 RID: 10488
	public DayOfWeek weekday;
}
