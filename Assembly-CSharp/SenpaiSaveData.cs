using System;

// Token: 0x020003BD RID: 957
[Serializable]
public class SenpaiSaveData
{
	// Token: 0x06001A16 RID: 6678 RVA: 0x000FE614 File Offset: 0x000FC814
	public static SenpaiSaveData ReadFromGlobals()
	{
		return new SenpaiSaveData
		{
			customSenpai = SenpaiGlobals.CustomSenpai,
			senpaiEyeColor = SenpaiGlobals.SenpaiEyeColor,
			senpaiEyeWear = SenpaiGlobals.SenpaiEyeWear,
			senpaiFacialHair = SenpaiGlobals.SenpaiFacialHair,
			senpaiHairColor = SenpaiGlobals.SenpaiHairColor,
			senpaiHairStyle = SenpaiGlobals.SenpaiHairStyle,
			senpaiSkinColor = SenpaiGlobals.SenpaiSkinColor
		};
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x000FE674 File Offset: 0x000FC874
	public static void WriteToGlobals(SenpaiSaveData data)
	{
		SenpaiGlobals.CustomSenpai = data.customSenpai;
		SenpaiGlobals.SenpaiEyeColor = data.senpaiEyeColor;
		SenpaiGlobals.SenpaiEyeWear = data.senpaiEyeWear;
		SenpaiGlobals.SenpaiFacialHair = data.senpaiFacialHair;
		SenpaiGlobals.SenpaiHairColor = data.senpaiHairColor;
		SenpaiGlobals.SenpaiHairStyle = data.senpaiHairStyle;
		SenpaiGlobals.SenpaiSkinColor = data.senpaiSkinColor;
	}

	// Token: 0x040028EE RID: 10478
	public bool customSenpai;

	// Token: 0x040028EF RID: 10479
	public string senpaiEyeColor = string.Empty;

	// Token: 0x040028F0 RID: 10480
	public int senpaiEyeWear;

	// Token: 0x040028F1 RID: 10481
	public int senpaiFacialHair;

	// Token: 0x040028F2 RID: 10482
	public string senpaiHairColor = string.Empty;

	// Token: 0x040028F3 RID: 10483
	public int senpaiHairStyle;

	// Token: 0x040028F4 RID: 10484
	public int senpaiSkinColor;
}
