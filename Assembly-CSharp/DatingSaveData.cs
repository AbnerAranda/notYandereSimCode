using System;
using System.Collections.Generic;

// Token: 0x020003B7 RID: 951
[Serializable]
public class DatingSaveData
{
	// Token: 0x06001A16 RID: 6678 RVA: 0x000FFC70 File Offset: 0x000FDE70
	public static DatingSaveData ReadFromGlobals()
	{
		DatingSaveData datingSaveData = new DatingSaveData();
		datingSaveData.affection = DatingGlobals.Affection;
		datingSaveData.affectionLevel = DatingGlobals.AffectionLevel;
		foreach (int num in DatingGlobals.KeysOfComplimentGiven())
		{
			if (DatingGlobals.GetComplimentGiven(num))
			{
				datingSaveData.complimentGiven.Add(num);
			}
		}
		foreach (int num2 in DatingGlobals.KeysOfSuitorCheck())
		{
			if (DatingGlobals.GetSuitorCheck(num2))
			{
				datingSaveData.suitorCheck.Add(num2);
			}
		}
		datingSaveData.suitorProgress = DatingGlobals.SuitorProgress;
		foreach (int num3 in DatingGlobals.KeysOfSuitorTrait())
		{
			datingSaveData.suitorTrait.Add(num3, DatingGlobals.GetSuitorTrait(num3));
		}
		foreach (int num4 in DatingGlobals.KeysOfTopicDiscussed())
		{
			if (DatingGlobals.GetTopicDiscussed(num4))
			{
				datingSaveData.topicDiscussed.Add(num4);
			}
		}
		foreach (int num5 in DatingGlobals.KeysOfTraitDemonstrated())
		{
			datingSaveData.traitDemonstrated.Add(num5, DatingGlobals.GetTraitDemonstrated(num5));
		}
		return datingSaveData;
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x000FFD8C File Offset: 0x000FDF8C
	public static void WriteToGlobals(DatingSaveData data)
	{
		DatingGlobals.Affection = data.affection;
		DatingGlobals.AffectionLevel = data.affectionLevel;
		foreach (int complimentID in data.complimentGiven)
		{
			DatingGlobals.SetComplimentGiven(complimentID, true);
		}
		foreach (int checkID in data.suitorCheck)
		{
			DatingGlobals.SetSuitorCheck(checkID, true);
		}
		DatingGlobals.SuitorProgress = data.suitorProgress;
		foreach (KeyValuePair<int, int> keyValuePair in data.suitorTrait)
		{
			DatingGlobals.SetSuitorTrait(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (int topicID in data.topicDiscussed)
		{
			DatingGlobals.SetTopicDiscussed(topicID, true);
		}
		foreach (KeyValuePair<int, int> keyValuePair2 in data.traitDemonstrated)
		{
			DatingGlobals.SetTraitDemonstrated(keyValuePair2.Key, keyValuePair2.Value);
		}
	}

	// Token: 0x040028F9 RID: 10489
	public float affection;

	// Token: 0x040028FA RID: 10490
	public float affectionLevel;

	// Token: 0x040028FB RID: 10491
	public IntHashSet complimentGiven = new IntHashSet();

	// Token: 0x040028FC RID: 10492
	public IntHashSet suitorCheck = new IntHashSet();

	// Token: 0x040028FD RID: 10493
	public int suitorProgress;

	// Token: 0x040028FE RID: 10494
	public IntAndIntDictionary suitorTrait = new IntAndIntDictionary();

	// Token: 0x040028FF RID: 10495
	public IntHashSet topicDiscussed = new IntHashSet();

	// Token: 0x04002900 RID: 10496
	public IntAndIntDictionary traitDemonstrated = new IntAndIntDictionary();
}
