using System;

// Token: 0x020003B5 RID: 949
[Serializable]
public class ConversationSaveData
{
	// Token: 0x06001A10 RID: 6672 RVA: 0x000FFAFC File Offset: 0x000FDCFC
	public static ConversationSaveData ReadFromGlobals()
	{
		ConversationSaveData conversationSaveData = new ConversationSaveData();
		foreach (int num in ConversationGlobals.KeysOfTopicDiscovered())
		{
			if (ConversationGlobals.GetTopicDiscovered(num))
			{
				conversationSaveData.topicDiscovered.Add(num);
			}
		}
		foreach (IntAndIntPair intAndIntPair in ConversationGlobals.KeysOfTopicLearnedByStudent())
		{
			if (ConversationGlobals.GetTopicLearnedByStudent(intAndIntPair.first, intAndIntPair.second))
			{
				conversationSaveData.topicLearnedByStudent.Add(intAndIntPair);
			}
		}
		return conversationSaveData;
	}

	// Token: 0x06001A11 RID: 6673 RVA: 0x000FFB7C File Offset: 0x000FDD7C
	public static void WriteToGlobals(ConversationSaveData data)
	{
		foreach (int topicID in data.topicDiscovered)
		{
			ConversationGlobals.SetTopicDiscovered(topicID, true);
		}
		foreach (IntAndIntPair intAndIntPair in data.topicLearnedByStudent)
		{
			ConversationGlobals.SetTopicLearnedByStudent(intAndIntPair.first, intAndIntPair.second, true);
		}
	}

	// Token: 0x040028F5 RID: 10485
	public IntHashSet topicDiscovered = new IntHashSet();

	// Token: 0x040028F6 RID: 10486
	public IntAndIntPairHashSet topicLearnedByStudent = new IntAndIntPairHashSet();
}
