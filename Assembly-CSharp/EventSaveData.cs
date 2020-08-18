using System;

// Token: 0x020003B3 RID: 947
[Serializable]
public class EventSaveData
{
	// Token: 0x060019F8 RID: 6648 RVA: 0x000FD857 File Offset: 0x000FBA57
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

	// Token: 0x060019F9 RID: 6649 RVA: 0x000FD895 File Offset: 0x000FBA95
	public static void WriteToGlobals(EventSaveData data)
	{
		EventGlobals.BefriendConversation = data.befriendConversation;
		EventGlobals.Event1 = data.event1;
		EventGlobals.Event2 = data.event2;
		EventGlobals.KidnapConversation = data.kidnapConversation;
		EventGlobals.LivingRoom = data.livingRoom;
	}

	// Token: 0x040028A4 RID: 10404
	public bool befriendConversation;

	// Token: 0x040028A5 RID: 10405
	public bool event1;

	// Token: 0x040028A6 RID: 10406
	public bool event2;

	// Token: 0x040028A7 RID: 10407
	public bool kidnapConversation;

	// Token: 0x040028A8 RID: 10408
	public bool livingRoom;
}
