using System;

// Token: 0x020003BA RID: 954
[Serializable]
public class SaveFileSaveData
{
	// Token: 0x06001A0D RID: 6669 RVA: 0x000FE173 File Offset: 0x000FC373
	public static SaveFileSaveData ReadFromGlobals()
	{
		return new SaveFileSaveData
		{
			currentSaveFile = SaveFileGlobals.CurrentSaveFile
		};
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x000FE185 File Offset: 0x000FC385
	public static void WriteToGlobals(SaveFileSaveData data)
	{
		SaveFileGlobals.CurrentSaveFile = data.currentSaveFile;
	}

	// Token: 0x040028DE RID: 10462
	public int currentSaveFile;
}
