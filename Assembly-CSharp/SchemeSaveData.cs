using System;
using System.Collections.Generic;

// Token: 0x020003BB RID: 955
[Serializable]
public class SchemeSaveData
{
	// Token: 0x06001A10 RID: 6672 RVA: 0x000FE194 File Offset: 0x000FC394
	public static SchemeSaveData ReadFromGlobals()
	{
		SchemeSaveData schemeSaveData = new SchemeSaveData();
		schemeSaveData.currentScheme = SchemeGlobals.CurrentScheme;
		schemeSaveData.darkSecret = SchemeGlobals.DarkSecret;
		foreach (int num in SchemeGlobals.KeysOfSchemePreviousStage())
		{
			schemeSaveData.schemePreviousStage.Add(num, SchemeGlobals.GetSchemePreviousStage(num));
		}
		foreach (int num2 in SchemeGlobals.KeysOfSchemeStage())
		{
			schemeSaveData.schemeStage.Add(num2, SchemeGlobals.GetSchemeStage(num2));
		}
		foreach (int num3 in SchemeGlobals.KeysOfSchemeStatus())
		{
			if (SchemeGlobals.GetSchemeStatus(num3))
			{
				schemeSaveData.schemeStatus.Add(num3);
			}
		}
		foreach (int num4 in SchemeGlobals.KeysOfSchemeUnlocked())
		{
			if (SchemeGlobals.GetSchemeUnlocked(num4))
			{
				schemeSaveData.schemeUnlocked.Add(num4);
			}
		}
		foreach (int num5 in SchemeGlobals.KeysOfServicePurchased())
		{
			if (SchemeGlobals.GetServicePurchased(num5))
			{
				schemeSaveData.servicePurchased.Add(num5);
			}
		}
		return schemeSaveData;
	}

	// Token: 0x06001A11 RID: 6673 RVA: 0x000FE2A8 File Offset: 0x000FC4A8
	public static void WriteToGlobals(SchemeSaveData data)
	{
		SchemeGlobals.CurrentScheme = data.currentScheme;
		SchemeGlobals.DarkSecret = data.darkSecret;
		foreach (KeyValuePair<int, int> keyValuePair in data.schemePreviousStage)
		{
			SchemeGlobals.SetSchemePreviousStage(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (KeyValuePair<int, int> keyValuePair2 in data.schemeStage)
		{
			SchemeGlobals.SetSchemeStage(keyValuePair2.Key, keyValuePair2.Value);
		}
		foreach (int schemeID in data.schemeStatus)
		{
			SchemeGlobals.SetSchemeStatus(schemeID, true);
		}
		foreach (int schemeID2 in data.schemeUnlocked)
		{
			SchemeGlobals.SetSchemeUnlocked(schemeID2, true);
		}
		foreach (int serviceID in data.servicePurchased)
		{
			SchemeGlobals.SetServicePurchased(serviceID, true);
		}
	}

	// Token: 0x040028DF RID: 10463
	public int currentScheme;

	// Token: 0x040028E0 RID: 10464
	public bool darkSecret;

	// Token: 0x040028E1 RID: 10465
	public IntAndIntDictionary schemePreviousStage = new IntAndIntDictionary();

	// Token: 0x040028E2 RID: 10466
	public IntAndIntDictionary schemeStage = new IntAndIntDictionary();

	// Token: 0x040028E3 RID: 10467
	public IntHashSet schemeStatus = new IntHashSet();

	// Token: 0x040028E4 RID: 10468
	public IntHashSet schemeUnlocked = new IntHashSet();

	// Token: 0x040028E5 RID: 10469
	public IntHashSet servicePurchased = new IntHashSet();
}
