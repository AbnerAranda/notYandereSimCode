using System;
using UnityEngine;

// Token: 0x02000429 RID: 1065
public class TitleSaveDataScript : MonoBehaviour
{
	// Token: 0x06001C61 RID: 7265 RVA: 0x00154DD0 File Offset: 0x00152FD0
	public void Start()
	{
		if (PlayerPrefs.GetInt("ProfileCreated_" + this.ID) == 1)
		{
			GameGlobals.Profile = this.ID;
			this.EmptyFile.SetActive(false);
			this.Data.SetActive(true);
			this.Kills.text = "Kills: " + PlayerGlobals.Kills;
			this.Mood.text = "Mood: " + Mathf.RoundToInt(SchoolGlobals.SchoolAtmosphere * 100f);
			this.Alerts.text = "Alerts: " + PlayerGlobals.Alerts;
			this.Week.text = "Week: " + 1;
			this.Day.text = "Day: " + DateGlobals.Weekday;
			this.Rival.text = "Rival: Osana";
			this.Rep.text = "Rep: " + PlayerGlobals.Reputation;
			this.Club.text = "Club: " + ClubGlobals.Club;
			this.Friends.text = "Friends: " + PlayerGlobals.Friends;
			if (PlayerGlobals.Kills == 0)
			{
				this.Blood.mainTexture = null;
				return;
			}
			if (PlayerGlobals.Kills > 0)
			{
				this.Blood.mainTexture = this.Bloods[1];
				return;
			}
			if (PlayerGlobals.Kills > 5)
			{
				this.Blood.mainTexture = this.Bloods[2];
				return;
			}
			if (PlayerGlobals.Kills > 10)
			{
				this.Blood.mainTexture = this.Bloods[3];
				return;
			}
			if (PlayerGlobals.Kills > 15)
			{
				this.Blood.mainTexture = this.Bloods[4];
				return;
			}
			if (PlayerGlobals.Kills > 20)
			{
				this.Blood.mainTexture = this.Bloods[5];
				return;
			}
		}
		else
		{
			this.EmptyFile.SetActive(true);
			this.Data.SetActive(false);
			this.Blood.enabled = false;
		}
	}

	// Token: 0x04003565 RID: 13669
	public GameObject EmptyFile;

	// Token: 0x04003566 RID: 13670
	public GameObject Data;

	// Token: 0x04003567 RID: 13671
	public Texture[] Bloods;

	// Token: 0x04003568 RID: 13672
	public UITexture Blood;

	// Token: 0x04003569 RID: 13673
	public UILabel Kills;

	// Token: 0x0400356A RID: 13674
	public UILabel Mood;

	// Token: 0x0400356B RID: 13675
	public UILabel Alerts;

	// Token: 0x0400356C RID: 13676
	public UILabel Week;

	// Token: 0x0400356D RID: 13677
	public UILabel Day;

	// Token: 0x0400356E RID: 13678
	public UILabel Rival;

	// Token: 0x0400356F RID: 13679
	public UILabel Rep;

	// Token: 0x04003570 RID: 13680
	public UILabel Club;

	// Token: 0x04003571 RID: 13681
	public UILabel Friends;

	// Token: 0x04003572 RID: 13682
	public int ID;
}
