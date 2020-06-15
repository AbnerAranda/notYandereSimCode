using System;

// Token: 0x020003B3 RID: 947
[Serializable]
public class ClubSaveData
{
	// Token: 0x06001A0A RID: 6666 RVA: 0x000FF6B0 File Offset: 0x000FD8B0
	public static ClubSaveData ReadFromGlobals()
	{
		ClubSaveData clubSaveData = new ClubSaveData();
		clubSaveData.club = ClubGlobals.Club;
		foreach (ClubType clubType in ClubGlobals.KeysOfClubClosed())
		{
			if (ClubGlobals.GetClubClosed(clubType))
			{
				clubSaveData.clubClosed.Add(clubType);
			}
		}
		foreach (ClubType clubType2 in ClubGlobals.KeysOfClubKicked())
		{
			if (ClubGlobals.GetClubKicked(clubType2))
			{
				clubSaveData.clubKicked.Add(clubType2);
			}
		}
		foreach (ClubType clubType3 in ClubGlobals.KeysOfQuitClub())
		{
			if (ClubGlobals.GetQuitClub(clubType3))
			{
				clubSaveData.quitClub.Add(clubType3);
			}
		}
		return clubSaveData;
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x000FF75C File Offset: 0x000FD95C
	public static void WriteToGlobals(ClubSaveData data)
	{
		ClubGlobals.Club = data.club;
		foreach (ClubType clubID in data.clubClosed)
		{
			ClubGlobals.SetClubClosed(clubID, true);
		}
		foreach (ClubType clubID2 in data.clubKicked)
		{
			ClubGlobals.SetClubKicked(clubID2, true);
		}
		foreach (ClubType clubID3 in data.quitClub)
		{
			ClubGlobals.SetQuitClub(clubID3, true);
		}
	}

	// Token: 0x040028EC RID: 10476
	public ClubType club;

	// Token: 0x040028ED RID: 10477
	public ClubTypeHashSet clubClosed = new ClubTypeHashSet();

	// Token: 0x040028EE RID: 10478
	public ClubTypeHashSet clubKicked = new ClubTypeHashSet();

	// Token: 0x040028EF RID: 10479
	public ClubTypeHashSet quitClub = new ClubTypeHashSet();
}
