using System;

// Token: 0x020003B8 RID: 952
[Serializable]
public class EventSaveData
{
	// Token: 0x06001A19 RID: 6681 RVA: 0x000FFF5B File Offset: 0x000FE15B
	public static EventSaveData ReadFromGlobals()
	{
		return new EventSaveData
		{
			befriendConversation = EventGlobals.BefriendConversation,
			event1 = EventGlobals.Event1,
			event2 = EventGlobals.Event2,
			kidnapConversation = EventGlobals.KidnapConversation,
			livingRoom = EventGlobals.LivingRoom
		};
	}

	// Token: 0x06001A1A RID: 6682 RVA: 0x000FFF99 File Offset: 0x000FE199
	public static void WriteToGlobals(EventSaveData data)
	{
		EventGlobals.BefriendConversation = data.befriendConversation;
		EventGlobals.Event1 = data.event1;
		EventGlobals.Event2 = data.event2;
		EventGlobals.KidnapConversation = data.kidnapConversation;
		EventGlobals.LivingRoom = data.livingRoom;
	}

	// Token: 0x04002901 RID: 10497
	public bool befriendConversation;

	// Token: 0x04002902 RID: 10498
	public bool event1;

	// Token: 0x04002903 RID: 10499
	public bool event2;

	// Token: 0x04002904 RID: 10500
	public bool kidnapConversation;

	// Token: 0x04002905 RID: 10501
	public bool livingRoom;
}
