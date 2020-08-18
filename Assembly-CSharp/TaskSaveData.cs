using System;
using System.Collections.Generic;

// Token: 0x020003BF RID: 959
[Serializable]
public class TaskSaveData
{
	// Token: 0x06001A1C RID: 6684 RVA: 0x000FF190 File Offset: 0x000FD390
	public static TaskSaveData ReadFromGlobals()
	{
		TaskSaveData taskSaveData = new TaskSaveData();
		foreach (int num in TaskGlobals.KeysOfGuitarPhoto())
		{
			if (TaskGlobals.GetGuitarPhoto(num))
			{
				taskSaveData.guitarPhoto.Add(num);
			}
		}
		foreach (int num2 in TaskGlobals.KeysOfKittenPhoto())
		{
			if (TaskGlobals.GetKittenPhoto(num2))
			{
				taskSaveData.kittenPhoto.Add(num2);
			}
		}
		foreach (int num3 in TaskGlobals.KeysOfHorudaPhoto())
		{
			if (TaskGlobals.GetHorudaPhoto(num3))
			{
				taskSaveData.horudaPhoto.Add(num3);
			}
		}
		foreach (int num4 in TaskGlobals.KeysOfTaskStatus())
		{
			taskSaveData.taskStatus.Add(num4, TaskGlobals.GetTaskStatus(num4));
		}
		return taskSaveData;
	}

	// Token: 0x06001A1D RID: 6685 RVA: 0x000FF260 File Offset: 0x000FD460
	public static void WriteToGlobals(TaskSaveData data)
	{
		foreach (int photoID in data.kittenPhoto)
		{
			TaskGlobals.SetKittenPhoto(photoID, true);
		}
		foreach (int photoID2 in data.guitarPhoto)
		{
			TaskGlobals.SetGuitarPhoto(photoID2, true);
		}
		foreach (KeyValuePair<int, int> keyValuePair in data.taskStatus)
		{
			TaskGlobals.SetTaskStatus(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x04002913 RID: 10515
	public IntHashSet guitarPhoto = new IntHashSet();

	// Token: 0x04002914 RID: 10516
	public IntHashSet kittenPhoto = new IntHashSet();

	// Token: 0x04002915 RID: 10517
	public IntHashSet horudaPhoto = new IntHashSet();

	// Token: 0x04002916 RID: 10518
	public IntAndIntDictionary taskStatus = new IntAndIntDictionary();
}
