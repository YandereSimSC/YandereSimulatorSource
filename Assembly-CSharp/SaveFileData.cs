using System;
using System.Xml.Serialization;

// Token: 0x020003C1 RID: 961
[XmlRoot]
[Serializable]
public class SaveFileData
{
	// Token: 0x04002919 RID: 10521
	public ApplicationSaveData applicationData = new ApplicationSaveData();

	// Token: 0x0400291A RID: 10522
	public ClassSaveData classData = new ClassSaveData();

	// Token: 0x0400291B RID: 10523
	public ClubSaveData clubData = new ClubSaveData();

	// Token: 0x0400291C RID: 10524
	public CollectibleSaveData collectibleData = new CollectibleSaveData();

	// Token: 0x0400291D RID: 10525
	public ConversationSaveData conversationData = new ConversationSaveData();

	// Token: 0x0400291E RID: 10526
	public DateSaveData dateData = new DateSaveData();

	// Token: 0x0400291F RID: 10527
	public DatingSaveData datingData = new DatingSaveData();

	// Token: 0x04002920 RID: 10528
	public EventSaveData eventData = new EventSaveData();

	// Token: 0x04002921 RID: 10529
	public GameSaveData gameData = new GameSaveData();

	// Token: 0x04002922 RID: 10530
	public HomeSaveData homeData = new HomeSaveData();

	// Token: 0x04002923 RID: 10531
	public MissionModeSaveData missionModeData = new MissionModeSaveData();

	// Token: 0x04002924 RID: 10532
	public OptionSaveData optionData = new OptionSaveData();

	// Token: 0x04002925 RID: 10533
	public PlayerSaveData playerData = new PlayerSaveData();

	// Token: 0x04002926 RID: 10534
	public PoseModeSaveData poseModeData = new PoseModeSaveData();

	// Token: 0x04002927 RID: 10535
	public SaveFileSaveData saveFileData = new SaveFileSaveData();

	// Token: 0x04002928 RID: 10536
	public SchemeSaveData schemeData = new SchemeSaveData();

	// Token: 0x04002929 RID: 10537
	public SchoolSaveData schoolData = new SchoolSaveData();

	// Token: 0x0400292A RID: 10538
	public SenpaiSaveData senpaiData = new SenpaiSaveData();

	// Token: 0x0400292B RID: 10539
	public StudentSaveData studentData = new StudentSaveData();

	// Token: 0x0400292C RID: 10540
	public TaskSaveData taskData = new TaskSaveData();

	// Token: 0x0400292D RID: 10541
	public YanvaniaSaveData yanvaniaData = new YanvaniaSaveData();
}
