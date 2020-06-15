using System;
using UnityEngine;

// Token: 0x02000359 RID: 857
public class PassTimeScript : MonoBehaviour
{
	// Token: 0x060018C2 RID: 6338 RVA: 0x000E3710 File Offset: 0x000E1910
	private void Update()
	{
		if (this.InputManager.TappedLeft || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.Selected--;
			if (this.Selected < 1)
			{
				this.Selected = 3;
			}
			this.UpdateHighlightPosition();
		}
		if (this.InputManager.TappedRight || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.Selected++;
			if (this.Selected > 3)
			{
				this.Selected = 1;
			}
			this.UpdateHighlightPosition();
		}
		if (this.InputManager.TappedUp || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.UpdateTime(1);
		}
		if (this.InputManager.TappedDown || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			this.UpdateTime(-1);
		}
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x000E37FC File Offset: 0x000E19FC
	private void UpdateHighlightPosition()
	{
		if (this.Selected == 1)
		{
			this.Highlight.localPosition = new Vector3(-130f, this.Highlight.localPosition.y, this.Highlight.localPosition.z);
			return;
		}
		if (this.Selected == 2)
		{
			this.Highlight.localPosition = new Vector3(-40f, this.Highlight.localPosition.y, this.Highlight.localPosition.z);
			return;
		}
		if (this.Selected == 3)
		{
			this.Highlight.localPosition = new Vector3(15f, this.Highlight.localPosition.y, this.Highlight.localPosition.z);
		}
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x000E38C8 File Offset: 0x000E1AC8
	public void GetCurrentTime()
	{
		this.Digits[1] = this.Clock.Hour;
		if (this.Clock.Minute < 9f)
		{
			this.Digits[2] = 0f;
			this.Digits[3] = this.Clock.Minute;
		}
		else
		{
			this.Digits[2] = this.Clock.Minute * 0.1f;
			this.Digits[2] = Mathf.Floor(this.Digits[2]);
			this.Digits[3] = this.Clock.Minute - this.Digits[2] * 10f;
		}
		this.MinimumDigits[1] = this.Digits[1];
		this.MinimumDigits[2] = this.Digits[2];
		this.MinimumDigits[3] = this.Digits[3];
		this.UpdateTime(0);
	}

	// Token: 0x060018C5 RID: 6341 RVA: 0x000E39A4 File Offset: 0x000E1BA4
	private void UpdateTime(int Increment)
	{
		this.Digits[this.Selected] += (float)Increment;
		if (this.Selected == 1)
		{
			if (this.Digits[1] < this.MinimumDigits[1])
			{
				this.Digits[1] = this.MinimumDigits[1];
			}
			else if (this.Digits[1] > 17f)
			{
				this.Digits[1] = 17f;
			}
			if (this.Digits[1] == this.MinimumDigits[1])
			{
				if (this.Digits[2] < this.MinimumDigits[2])
				{
					this.Digits[2] = this.MinimumDigits[2];
				}
				if (this.Digits[2] == this.MinimumDigits[2] && this.Digits[3] < this.MinimumDigits[3])
				{
					this.Digits[3] = this.MinimumDigits[3];
				}
			}
		}
		else if (this.Selected == 2)
		{
			if (this.Digits[1] == this.MinimumDigits[1])
			{
				if (this.Digits[2] < this.MinimumDigits[2])
				{
					this.Digits[2] = this.MinimumDigits[2];
				}
				else if (this.Digits[2] > 5f)
				{
					this.Digits[2] = this.MinimumDigits[2];
				}
				if (this.Digits[2] == this.MinimumDigits[2] && this.Digits[3] < this.MinimumDigits[3])
				{
					this.Digits[3] = this.MinimumDigits[3];
				}
			}
			else if (this.Digits[2] < 0f)
			{
				this.Digits[2] = 5f;
			}
			else if (this.Digits[2] > 5f)
			{
				this.Digits[2] = 0f;
			}
		}
		else if (this.Selected == 3)
		{
			if (this.Digits[1] == this.MinimumDigits[1] && this.Digits[2] == this.MinimumDigits[2])
			{
				if (this.Digits[3] < this.MinimumDigits[3])
				{
					this.Digits[3] = this.MinimumDigits[3];
				}
				else if (this.Digits[3] > 9f)
				{
					this.Digits[3] = this.MinimumDigits[3];
				}
			}
			else if (this.Digits[3] < 0f)
			{
				this.Digits[3] = 9f;
			}
			else if (this.Digits[3] > 9f)
			{
				this.Digits[3] = 0f;
			}
		}
		if (this.Digits[1] < 12f)
		{
			this.AMPM = " AM";
		}
		else
		{
			this.AMPM = " PM";
		}
		if (this.Digits[1] < 10f)
		{
			this.TimeDisplay.text = string.Concat(new object[]
			{
				"0",
				this.Digits[1],
				":",
				this.Digits[2],
				this.Digits[3],
				this.AMPM
			});
		}
		else if (this.Digits[1] < 13f)
		{
			this.TimeDisplay.text = string.Concat(new object[]
			{
				this.Digits[1],
				":",
				this.Digits[2],
				this.Digits[3],
				this.AMPM
			});
		}
		else
		{
			this.TimeDisplay.text = string.Concat(new object[]
			{
				"0",
				this.Digits[1] - 12f,
				":",
				this.Digits[2],
				this.Digits[3],
				this.AMPM
			});
		}
		this.TargetTime = (int)(this.Digits[1] * 60f + this.Digits[2] * 10f + this.Digits[3]);
	}

	// Token: 0x0400249D RID: 9373
	public InputManagerScript InputManager;

	// Token: 0x0400249E RID: 9374
	public ClockScript Clock;

	// Token: 0x0400249F RID: 9375
	public UILabel TimeDisplay;

	// Token: 0x040024A0 RID: 9376
	public Transform Highlight;

	// Token: 0x040024A1 RID: 9377
	public float[] MinimumDigits;

	// Token: 0x040024A2 RID: 9378
	public float[] Digits;

	// Token: 0x040024A3 RID: 9379
	public int TargetTime;

	// Token: 0x040024A4 RID: 9380
	public int Selected = 1;

	// Token: 0x040024A5 RID: 9381
	public string AMPM = "AM";
}
