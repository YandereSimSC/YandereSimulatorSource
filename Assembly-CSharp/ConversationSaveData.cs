using System;

// Token: 0x020003B0 RID: 944
[Serializable]
public class ConversationSaveData
{
	// Token: 0x060019EF RID: 6639 RVA: 0x000FD3F8 File Offset: 0x000FB5F8
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

	// Token: 0x060019F0 RID: 6640 RVA: 0x000FD478 File Offset: 0x000FB678
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

	// Token: 0x04002898 RID: 10392
	public IntHashSet topicDiscovered = new IntHashSet();

	// Token: 0x04002899 RID: 10393
	public IntAndIntPairHashSet topicLearnedByStudent = new IntAndIntPairHashSet();
}
