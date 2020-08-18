using System;
using UnityEngine;

// Token: 0x020003B9 RID: 953
[Serializable]
public class PoseModeSaveData
{
	// Token: 0x06001A0A RID: 6666 RVA: 0x000FE128 File Offset: 0x000FC328
	public static PoseModeSaveData ReadFromGlobals()
	{
		return new PoseModeSaveData
		{
			posePosition = PoseModeGlobals.PosePosition,
			poseRotation = PoseModeGlobals.PoseRotation,
			poseScale = PoseModeGlobals.PoseScale
		};
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x000FE150 File Offset: 0x000FC350
	public static void WriteToGlobals(PoseModeSaveData data)
	{
		PoseModeGlobals.PosePosition = data.posePosition;
		PoseModeGlobals.PoseRotation = data.poseRotation;
		PoseModeGlobals.PoseScale = data.poseScale;
	}

	// Token: 0x040028DB RID: 10459
	public Vector3 posePosition;

	// Token: 0x040028DC RID: 10460
	public Vector3 poseRotation;

	// Token: 0x040028DD RID: 10461
	public Vector3 poseScale;
}
