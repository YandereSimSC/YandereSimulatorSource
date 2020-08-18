using System;
using System.Collections.Generic;

// Token: 0x020003B6 RID: 950
[Serializable]
public class MissionModeSaveData
{
	// Token: 0x06001A01 RID: 6657 RVA: 0x000FD964 File Offset: 0x000FBB64
	public static MissionModeSaveData ReadFromGlobals()
	{
		MissionModeSaveData missionModeSaveData = new MissionModeSaveData();
		foreach (int num in MissionModeGlobals.KeysOfMissionCondition())
		{
			missionModeSaveData.missionCondition.Add(num, MissionModeGlobals.GetMissionCondition(num));
		}
		missionModeSaveData.missionDifficulty = MissionModeGlobals.MissionDifficulty;
		missionModeSaveData.missionMode = MissionModeGlobals.MissionMode;
		missionModeSaveData.missionRequiredClothing = MissionModeGlobals.MissionRequiredClothing;
		missionModeSaveData.missionRequiredDisposal = MissionModeGlobals.MissionRequiredDisposal;
		missionModeSaveData.missionRequiredWeapon = MissionModeGlobals.MissionRequiredWeapon;
		missionModeSaveData.missionTarget = MissionModeGlobals.MissionTarget;
		missionModeSaveData.missionTargetName = MissionModeGlobals.MissionTargetName;
		missionModeSaveData.nemesisDifficulty = MissionModeGlobals.NemesisDifficulty;
		return missionModeSaveData;
	}

	// Token: 0x06001A02 RID: 6658 RVA: 0x000FD9FC File Offset: 0x000FBBFC
	public static void WriteToGlobals(MissionModeSaveData data)
	{
		foreach (KeyValuePair<int, int> keyValuePair in data.missionCondition)
		{
			MissionModeGlobals.SetMissionCondition(keyValuePair.Key, keyValuePair.Value);
		}
		MissionModeGlobals.MissionDifficulty = data.missionDifficulty;
		MissionModeGlobals.MissionMode = data.missionMode;
		MissionModeGlobals.MissionRequiredClothing = data.missionRequiredClothing;
		MissionModeGlobals.MissionRequiredDisposal = data.missionRequiredDisposal;
		MissionModeGlobals.MissionRequiredWeapon = data.missionRequiredWeapon;
		MissionModeGlobals.MissionTarget = data.missionTarget;
		MissionModeGlobals.MissionTargetName = data.missionTargetName;
		MissionModeGlobals.NemesisDifficulty = data.nemesisDifficulty;
	}

	// Token: 0x040028AF RID: 10415
	public IntAndIntDictionary missionCondition = new IntAndIntDictionary();

	// Token: 0x040028B0 RID: 10416
	public int missionDifficulty;

	// Token: 0x040028B1 RID: 10417
	public bool missionMode;

	// Token: 0x040028B2 RID: 10418
	public int missionRequiredClothing;

	// Token: 0x040028B3 RID: 10419
	public int missionRequiredDisposal;

	// Token: 0x040028B4 RID: 10420
	public int missionRequiredWeapon;

	// Token: 0x040028B5 RID: 10421
	public int missionTarget;

	// Token: 0x040028B6 RID: 10422
	public string missionTargetName = string.Empty;

	// Token: 0x040028B7 RID: 10423
	public int nemesisDifficulty;
}
