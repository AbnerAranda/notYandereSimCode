using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000444 RID: 1092
[Serializable]
public class Clock
{
	// Token: 0x06001CBD RID: 7357 RVA: 0x001588E0 File Offset: 0x00156AE0
	public Clock(int hours, int minutes, int seconds, float currentSecond)
	{
		this.hours = hours;
		this.minutes = minutes;
		this.seconds = seconds;
		this.currentSecond = currentSecond;
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x00158905 File Offset: 0x00156B05
	public Clock(int hours, int minutes, int seconds) : this(hours, minutes, seconds, 0f)
	{
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x00158915 File Offset: 0x00156B15
	public Clock() : this(0, 0, 0, 0f)
	{
	}

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00158925 File Offset: 0x00156B25
	public int Hours24
	{
		get
		{
			return this.hours;
		}
	}

	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x00158930 File Offset: 0x00156B30
	public int Hours12
	{
		get
		{
			int num = this.hours % 12;
			if (num != 0)
			{
				return num;
			}
			return 12;
		}
	}

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0015894E File Offset: 0x00156B4E
	public int Minutes
	{
		get
		{
			return this.minutes;
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x00158956 File Offset: 0x00156B56
	public int Seconds
	{
		get
		{
			return this.seconds;
		}
	}

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0015895E File Offset: 0x00156B5E
	public float CurrentSecond
	{
		get
		{
			return this.currentSecond;
		}
	}

	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x00158966 File Offset: 0x00156B66
	public int TotalSeconds
	{
		get
		{
			return this.hours * 3600 + this.minutes * 60 + this.seconds;
		}
	}

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00158985 File Offset: 0x00156B85
	public float PreciseTotalSeconds
	{
		get
		{
			return (float)this.TotalSeconds + this.currentSecond;
		}
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x00158995 File Offset: 0x00156B95
	public bool IsAM
	{
		get
		{
			return this.hours < 12;
		}
	}

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x001589A4 File Offset: 0x00156BA4
	public TimeOfDay TimeOfDay
	{
		get
		{
			if (this.hours < 3)
			{
				return TimeOfDay.Midnight;
			}
			if (this.hours < 6)
			{
				return TimeOfDay.EarlyMorning;
			}
			if (this.hours < 9)
			{
				return TimeOfDay.Morning;
			}
			if (this.hours < 12)
			{
				return TimeOfDay.LateMorning;
			}
			if (this.hours < 15)
			{
				return TimeOfDay.Noon;
			}
			if (this.hours < 18)
			{
				return TimeOfDay.Afternoon;
			}
			if (this.hours < 21)
			{
				return TimeOfDay.Evening;
			}
			return TimeOfDay.Night;
		}
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00158A04 File Offset: 0x00156C04
	public string TimeOfDayString
	{
		get
		{
			return Clock.TimeOfDayStrings[this.TimeOfDay];
		}
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x00158A16 File Offset: 0x00156C16
	public bool IsBefore(Clock clock)
	{
		return this.TotalSeconds < clock.TotalSeconds;
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x00158A26 File Offset: 0x00156C26
	public bool IsAfter(Clock clock)
	{
		return this.TotalSeconds > clock.TotalSeconds;
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x00158A36 File Offset: 0x00156C36
	public void IncrementHour()
	{
		this.hours++;
		if (this.hours == 24)
		{
			this.hours = 0;
		}
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x00158A57 File Offset: 0x00156C57
	public void IncrementMinute()
	{
		this.minutes++;
		if (this.minutes == 60)
		{
			this.IncrementHour();
			this.minutes = 0;
		}
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x00158A7E File Offset: 0x00156C7E
	public void IncrementSecond()
	{
		this.seconds++;
		if (this.seconds == 60)
		{
			this.IncrementMinute();
			this.seconds = 0;
		}
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x00158AA5 File Offset: 0x00156CA5
	public void Tick(float dt)
	{
		this.currentSecond += dt;
		while (this.currentSecond >= 1f)
		{
			this.IncrementSecond();
			this.currentSecond -= 1f;
		}
	}

	// Token: 0x0400365D RID: 13917
	[SerializeField]
	private int hours;

	// Token: 0x0400365E RID: 13918
	[SerializeField]
	private int minutes;

	// Token: 0x0400365F RID: 13919
	[SerializeField]
	private int seconds;

	// Token: 0x04003660 RID: 13920
	[SerializeField]
	private float currentSecond;

	// Token: 0x04003661 RID: 13921
	private static readonly Dictionary<TimeOfDay, string> TimeOfDayStrings = new Dictionary<TimeOfDay, string>
	{
		{
			TimeOfDay.Midnight,
			"Midnight"
		},
		{
			TimeOfDay.EarlyMorning,
			"Early Morning"
		},
		{
			TimeOfDay.Morning,
			"Morning"
		},
		{
			TimeOfDay.LateMorning,
			"Late Morning"
		},
		{
			TimeOfDay.Noon,
			"Noon"
		},
		{
			TimeOfDay.Afternoon,
			"Afternoon"
		},
		{
			TimeOfDay.Evening,
			"Evening"
		},
		{
			TimeOfDay.Night,
			"Night"
		}
	};
}
