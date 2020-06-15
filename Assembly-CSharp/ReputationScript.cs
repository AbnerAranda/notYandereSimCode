using System;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class ReputationScript : MonoBehaviour
{
	// Token: 0x060019B4 RID: 6580 RVA: 0x000FC007 File Offset: 0x000FA207
	private void Start()
	{
		if (MissionModeGlobals.MissionMode)
		{
			this.MissionMode = true;
		}
		this.Reputation = PlayerGlobals.Reputation;
		this.Bully();
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x000FC028 File Offset: 0x000FA228
	private void Update()
	{
		if (this.Phase == 1)
		{
			if (this.Clock.PresentTime / 60f > 8.5f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.Clock.PresentTime / 60f > 13.5f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 3 && this.Clock.PresentTime / 60f > 18f)
		{
			this.Phase++;
		}
		if (this.PendingRep < 0f)
		{
			this.StudentManager.TutorialWindow.ShowRepMessage = true;
		}
		if (this.CheckedRep < this.Phase && !this.StudentManager.Yandere.Struggling && !this.StudentManager.Yandere.DelinquentFighting && !this.StudentManager.Yandere.Pickpocketing && !this.StudentManager.Yandere.Noticed && !this.ArmDetector.SummonDemon)
		{
			this.UpdateRep();
			if (this.Reputation <= -100f)
			{
				this.Portal.EndDay();
			}
		}
		if (!this.MissionMode)
		{
			this.CurrentRepMarker.localPosition = new Vector3(Mathf.Lerp(this.CurrentRepMarker.localPosition.x, -830f + this.Reputation * 1.5f, Time.deltaTime * 10f), this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
			this.PendingRepMarker.localPosition = new Vector3(Mathf.Lerp(this.PendingRepMarker.localPosition.x, this.CurrentRepMarker.transform.localPosition.x + this.PendingRep * 1.5f, Time.deltaTime * 10f), this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		else
		{
			this.PendingRepMarker.localPosition = new Vector3(Mathf.Lerp(this.PendingRepMarker.localPosition.x, -980f + this.PendingRep * -3f, Time.deltaTime * 10f), this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (this.CurrentRepMarker.localPosition.x < -980f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(-980f, this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
		}
		if (this.PendingRepMarker.localPosition.x < -980f)
		{
			this.PendingRepMarker.localPosition = new Vector3(-980f, this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (this.CurrentRepMarker.localPosition.x > -680f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(-680f, this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
		}
		if (this.PendingRepMarker.localPosition.x > -680f)
		{
			this.PendingRepMarker.localPosition = new Vector3(-680f, this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (!this.MissionMode)
		{
			if (this.PendingRep > 0f)
			{
				this.PendingRepLabel.text = "+" + this.PendingRep.ToString();
				return;
			}
			if (this.PendingRep < 0f)
			{
				this.PendingRepLabel.text = this.PendingRep.ToString();
				return;
			}
			this.PendingRepLabel.text = string.Empty;
			return;
		}
		else
		{
			if (this.PendingRep < 0f)
			{
				this.PendingRepLabel.text = (-this.PendingRep).ToString();
				return;
			}
			this.PendingRepLabel.text = string.Empty;
			return;
		}
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x000FC47B File Offset: 0x000FA67B
	private void Bully()
	{
		this.FlowerVase.SetActive(false);
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x000FC48C File Offset: 0x000FA68C
	public void UpdateRep()
	{
		this.Reputation += this.PendingRep;
		this.PendingRep = 0f;
		this.CheckedRep++;
		if (this.StudentManager.Yandere.Club == ClubType.Delinquent && this.Reputation > -33.33333f)
		{
			this.Reputation = -33.33333f;
		}
		this.StudentManager.WipePendingRep();
	}

	// Token: 0x040027CF RID: 10191
	public StudentManagerScript StudentManager;

	// Token: 0x040027D0 RID: 10192
	public ArmDetectorScript ArmDetector;

	// Token: 0x040027D1 RID: 10193
	public PortalScript Portal;

	// Token: 0x040027D2 RID: 10194
	public Transform CurrentRepMarker;

	// Token: 0x040027D3 RID: 10195
	public Transform PendingRepMarker;

	// Token: 0x040027D4 RID: 10196
	public UILabel PendingRepLabel;

	// Token: 0x040027D5 RID: 10197
	public ClockScript Clock;

	// Token: 0x040027D6 RID: 10198
	public float Reputation;

	// Token: 0x040027D7 RID: 10199
	public float PendingRep;

	// Token: 0x040027D8 RID: 10200
	public int CheckedRep = 1;

	// Token: 0x040027D9 RID: 10201
	public int Phase;

	// Token: 0x040027DA RID: 10202
	public bool MissionMode;

	// Token: 0x040027DB RID: 10203
	public GameObject FlowerVase;

	// Token: 0x040027DC RID: 10204
	public GameObject Grafitti;
}
