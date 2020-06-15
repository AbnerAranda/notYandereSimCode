using System;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class IfElseScript : MonoBehaviour
{
	// Token: 0x06001782 RID: 6018 RVA: 0x000CBDA0 File Offset: 0x000C9FA0
	private void Start()
	{
		this.SwitchCase();
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x000CBDA8 File Offset: 0x000C9FA8
	private void IfElse()
	{
		if (this.ID == 1)
		{
			this.Day = "Monday";
			return;
		}
		if (this.ID == 2)
		{
			this.Day = "Tuesday";
			return;
		}
		if (this.ID == 3)
		{
			this.Day = "Wednesday";
			return;
		}
		if (this.ID == 4)
		{
			this.Day = "Thursday";
			return;
		}
		if (this.ID == 5)
		{
			this.Day = "Friday";
			return;
		}
		if (this.ID == 6)
		{
			this.Day = "Saturday";
			return;
		}
		if (this.ID == 7)
		{
			this.Day = "Sunday";
		}
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x000CBE48 File Offset: 0x000CA048
	private void SwitchCase()
	{
		switch (this.ID)
		{
		case 1:
			this.Day = "Monday";
			return;
		case 2:
			this.Day = "Tuesday";
			return;
		case 3:
			this.Day = "Wednesday";
			return;
		case 4:
			this.Day = "Thursday";
			return;
		case 5:
			this.Day = "Friday";
			return;
		case 6:
			this.Day = "Saturday";
			return;
		case 7:
			this.Day = "Sunday";
			return;
		default:
			return;
		}
	}

	// Token: 0x040020C0 RID: 8384
	public int ID;

	// Token: 0x040020C1 RID: 8385
	public string Day;
}
