using System;

// Token: 0x020003AD RID: 941
[Serializable]
public class ClassSaveData
{
	// Token: 0x060019E6 RID: 6630 RVA: 0x000FCE40 File Offset: 0x000FB040
	public static ClassSaveData ReadFromGlobals()
	{
		return new ClassSaveData
		{
			biology = ClassGlobals.Biology,
			biologyBonus = ClassGlobals.BiologyBonus,
			biologyGrade = ClassGlobals.BiologyGrade,
			chemistry = ClassGlobals.Chemistry,
			chemistryBonus = ClassGlobals.ChemistryBonus,
			chemistryGrade = ClassGlobals.ChemistryGrade,
			language = ClassGlobals.Language,
			languageBonus = ClassGlobals.LanguageBonus,
			languageGrade = ClassGlobals.LanguageGrade,
			physical = ClassGlobals.Physical,
			physicalBonus = ClassGlobals.PhysicalBonus,
			physicalGrade = ClassGlobals.PhysicalGrade,
			psychology = ClassGlobals.Psychology,
			psychologyBonus = ClassGlobals.PsychologyBonus,
			psychologyGrade = ClassGlobals.PsychologyGrade
		};
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x000FCEF8 File Offset: 0x000FB0F8
	public static void WriteToGlobals(ClassSaveData data)
	{
		ClassGlobals.Biology = data.biology;
		ClassGlobals.BiologyBonus = data.biologyBonus;
		ClassGlobals.BiologyGrade = data.biologyGrade;
		ClassGlobals.Chemistry = data.chemistry;
		ClassGlobals.ChemistryBonus = data.chemistryBonus;
		ClassGlobals.ChemistryGrade = data.chemistryGrade;
		ClassGlobals.Language = data.language;
		ClassGlobals.LanguageBonus = data.languageBonus;
		ClassGlobals.LanguageGrade = data.languageGrade;
		ClassGlobals.Physical = data.physical;
		ClassGlobals.PhysicalBonus = data.physicalBonus;
		ClassGlobals.PhysicalGrade = data.physicalGrade;
		ClassGlobals.Psychology = data.psychology;
		ClassGlobals.PsychologyBonus = data.psychologyBonus;
		ClassGlobals.PsychologyGrade = data.psychologyGrade;
	}

	// Token: 0x04002880 RID: 10368
	public int biology;

	// Token: 0x04002881 RID: 10369
	public int biologyBonus;

	// Token: 0x04002882 RID: 10370
	public int biologyGrade;

	// Token: 0x04002883 RID: 10371
	public int chemistry;

	// Token: 0x04002884 RID: 10372
	public int chemistryBonus;

	// Token: 0x04002885 RID: 10373
	public int chemistryGrade;

	// Token: 0x04002886 RID: 10374
	public int language;

	// Token: 0x04002887 RID: 10375
	public int languageBonus;

	// Token: 0x04002888 RID: 10376
	public int languageGrade;

	// Token: 0x04002889 RID: 10377
	public int physical;

	// Token: 0x0400288A RID: 10378
	public int physicalBonus;

	// Token: 0x0400288B RID: 10379
	public int physicalGrade;

	// Token: 0x0400288C RID: 10380
	public int psychology;

	// Token: 0x0400288D RID: 10381
	public int psychologyBonus;

	// Token: 0x0400288E RID: 10382
	public int psychologyGrade;
}
