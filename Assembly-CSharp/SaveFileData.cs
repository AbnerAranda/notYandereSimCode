using System;
using System.Xml.Serialization;

// Token: 0x020003C6 RID: 966
[XmlRoot]
[Serializable]
public class SaveFileData
{
	// Token: 0x04002976 RID: 10614
	public ApplicationSaveData applicationData = new ApplicationSaveData();

	// Token: 0x04002977 RID: 10615
	public ClassSaveData classData = new ClassSaveData();

	// Token: 0x04002978 RID: 10616
	public ClubSaveData clubData = new ClubSaveData();

	// Token: 0x04002979 RID: 10617
	public CollectibleSaveData collectibleData = new CollectibleSaveData();

	// Token: 0x0400297A RID: 10618
	public ConversationSaveData conversationData = new ConversationSaveData();

	// Token: 0x0400297B RID: 10619
	public DateSaveData dateData = new DateSaveData();

	// Token: 0x0400297C RID: 10620
	public DatingSaveData datingData = new DatingSaveData();

	// Token: 0x0400297D RID: 10621
	public EventSaveData eventData = new EventSaveData();

	// Token: 0x0400297E RID: 10622
	public GameSaveData gameData = new GameSaveData();

	// Token: 0x0400297F RID: 10623
	public HomeSaveData homeData = new HomeSaveData();

	// Token: 0x04002980 RID: 10624
	public MissionModeSaveData missionModeData = new MissionModeSaveData();

	// Token: 0x04002981 RID: 10625
	public OptionSaveData optionData = new OptionSaveData();

	// Token: 0x04002982 RID: 10626
	public PlayerSaveData playerData = new PlayerSaveData();

	// Token: 0x04002983 RID: 10627
	public PoseModeSaveData poseModeData = new PoseModeSaveData();

	// Token: 0x04002984 RID: 10628
	public SaveFileSaveData saveFileData = new SaveFileSaveData();

	// Token: 0x04002985 RID: 10629
	public SchemeSaveData schemeData = new SchemeSaveData();

	// Token: 0x04002986 RID: 10630
	public SchoolSaveData schoolData = new SchoolSaveData();

	// Token: 0x04002987 RID: 10631
	public SenpaiSaveData senpaiData = new SenpaiSaveData();

	// Token: 0x04002988 RID: 10632
	public StudentSaveData studentData = new StudentSaveData();

	// Token: 0x04002989 RID: 10633
	public TaskSaveData taskData = new TaskSaveData();

	// Token: 0x0400298A RID: 10634
	public YanvaniaSaveData yanvaniaData = new YanvaniaSaveData();
}
