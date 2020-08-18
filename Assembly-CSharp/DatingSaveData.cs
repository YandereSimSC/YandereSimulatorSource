﻿using System;
using System.Collections.Generic;

// Token: 0x020003B2 RID: 946
[Serializable]
public class DatingSaveData
{
	// Token: 0x060019F5 RID: 6645 RVA: 0x000FD56C File Offset: 0x000FB76C
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

	// Token: 0x060019F6 RID: 6646 RVA: 0x000FD688 File Offset: 0x000FB888
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

	// Token: 0x0400289C RID: 10396
	public float affection;

	// Token: 0x0400289D RID: 10397
	public float affectionLevel;

	// Token: 0x0400289E RID: 10398
	public IntHashSet complimentGiven = new IntHashSet();

	// Token: 0x0400289F RID: 10399
	public IntHashSet suitorCheck = new IntHashSet();

	// Token: 0x040028A0 RID: 10400
	public int suitorProgress;

	// Token: 0x040028A1 RID: 10401
	public IntAndIntDictionary suitorTrait = new IntAndIntDictionary();

	// Token: 0x040028A2 RID: 10402
	public IntHashSet topicDiscussed = new IntHashSet();

	// Token: 0x040028A3 RID: 10403
	public IntAndIntDictionary traitDemonstrated = new IntAndIntDictionary();
}
