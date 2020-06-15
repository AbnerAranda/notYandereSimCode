using System;
using UnityEngine;

// Token: 0x02000259 RID: 601
public class DateReverseScript : MonoBehaviour
{
	// Token: 0x060012FC RID: 4860 RVA: 0x00099DC7 File Offset: 0x00097FC7
	private void Start()
	{
		Time.timeScale = 1f;
		this.UpdateDate();
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x00099DDC File Offset: 0x00097FDC
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Rollback = true;
		}
		if (this.Rollback)
		{
			this.LifeTime += Time.deltaTime;
			this.Timer += Time.deltaTime;
			if (this.Timer > this.TimeLimit)
			{
				if ((this.Year == this.SlowYear && this.Month == this.SlowMonth && this.Day < this.SlowDay) || (this.Year == this.SlowYear && this.Month < this.SlowMonth))
				{
					this.TimeLimit *= 1.09f;
					if (this.Month == this.EndMonth && this.Day == this.EndDay + 1)
					{
						this.MyAudio.clip = this.Finish;
						this.Label.color = new Color(1f, 0f, 0f, 1f);
						base.enabled = false;
					}
				}
				else if (this.TimeLimit > 0.01f)
				{
					this.TimeLimit *= 0.9f;
				}
				else
				{
					this.Day += this.RollDirection * 19;
				}
				this.Timer = 0f;
				this.Day += this.RollDirection;
				this.UpdateDate();
				this.MyAudio.Play();
				if (!(this.MyAudio.clip != this.Finish))
				{
					this.MyAudio.pitch = 1f;
				}
			}
		}
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x00099F84 File Offset: 0x00098184
	private void UpdateDate()
	{
		if (this.Day < 1)
		{
			this.Day = 31;
			this.Month--;
			if (this.Month < 1)
			{
				this.Month = 12;
				this.Year--;
			}
		}
		else if (this.Day > 31)
		{
			this.Day = 1;
			this.Month++;
			if (this.Month > 11)
			{
				this.Month = 1;
				this.Year++;
			}
		}
		if (this.Day == 1 || this.Day == 21 || this.Day == 31)
		{
			this.Prefix = "st";
		}
		else if (this.Day == 2 || this.Day == 22)
		{
			this.Prefix = "nd";
		}
		else if (this.Day == 3 || this.Day == 23)
		{
			this.Prefix = "rd";
		}
		else
		{
			this.Prefix = "th";
		}
		this.Label.text = string.Concat(new object[]
		{
			this.MonthName[this.Month],
			" ",
			this.Day,
			this.Prefix,
			", ",
			this.Year
		});
	}

	// Token: 0x040018ED RID: 6381
	public AudioSource MyAudio;

	// Token: 0x040018EE RID: 6382
	public string[] MonthName;

	// Token: 0x040018EF RID: 6383
	public string Prefix;

	// Token: 0x040018F0 RID: 6384
	public UILabel Label;

	// Token: 0x040018F1 RID: 6385
	public AudioClip Finish;

	// Token: 0x040018F2 RID: 6386
	public float TimeLimit;

	// Token: 0x040018F3 RID: 6387
	public float LifeTime;

	// Token: 0x040018F4 RID: 6388
	public float Timer;

	// Token: 0x040018F5 RID: 6389
	public int RollDirection;

	// Token: 0x040018F6 RID: 6390
	public int Month;

	// Token: 0x040018F7 RID: 6391
	public int Year;

	// Token: 0x040018F8 RID: 6392
	public int Day;

	// Token: 0x040018F9 RID: 6393
	public int SlowMonth;

	// Token: 0x040018FA RID: 6394
	public int SlowYear;

	// Token: 0x040018FB RID: 6395
	public int SlowDay;

	// Token: 0x040018FC RID: 6396
	public int EndMonth;

	// Token: 0x040018FD RID: 6397
	public int EndYear;

	// Token: 0x040018FE RID: 6398
	public int EndDay;

	// Token: 0x040018FF RID: 6399
	public bool Rollback;
}
