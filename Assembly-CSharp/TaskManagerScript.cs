using System;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class TaskManagerScript : MonoBehaviour
{
	// Token: 0x06001C0F RID: 7183 RVA: 0x0014EC2E File Offset: 0x0014CE2E
	private void Start()
	{
		this.UpdateTaskStatus();
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x0014EC38 File Offset: 0x0014CE38
	public void CheckTaskPickups()
	{
		Debug.Log("Checking Tasks that are completed by picking something up!");
		if (TaskGlobals.GetTaskStatus(11) == 1 && this.Prompts[11].Circle[3] != null && this.Prompts[11].Circle[3].fillAmount == 0f)
		{
			if (this.StudentManager.Students[11] != null)
			{
				this.StudentManager.Students[11].TaskPhase = 5;
			}
			ConversationGlobals.SetTopicDiscovered(15, true);
			this.Yandere.NotificationManager.TopicName = "Cats";
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
			ConversationGlobals.SetTopicLearnedByStudent(15, 11, true);
			TaskGlobals.SetTaskStatus(11, 2);
			UnityEngine.Object.Destroy(this.TaskObjects[11]);
		}
		if (TaskGlobals.GetTaskStatus(25) == 1 && this.Prompts[25].Circle[3].fillAmount == 0f)
		{
			if (this.StudentManager.Students[25] != null)
			{
				this.StudentManager.Students[25].TaskPhase = 5;
			}
			TaskGlobals.SetTaskStatus(25, 2);
			UnityEngine.Object.Destroy(this.TaskObjects[25]);
		}
		if (TaskGlobals.GetTaskStatus(37) == 1 && this.Prompts[37].Circle[3] != null && this.Prompts[37].Circle[3].fillAmount == 0f)
		{
			if (this.StudentManager.Students[37] != null)
			{
				this.StudentManager.Students[37].TaskPhase = 5;
			}
			TaskGlobals.SetTaskStatus(37, 2);
			UnityEngine.Object.Destroy(this.TaskObjects[37]);
		}
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x0014EE04 File Offset: 0x0014D004
	public void UpdateTaskStatus()
	{
		if (TaskGlobals.GetTaskStatus(8) == 1 && this.StudentManager.Students[8] != null)
		{
			if (this.StudentManager.Students[8].TaskPhase == 0)
			{
				this.StudentManager.Students[8].TaskPhase = 4;
			}
			if (this.Yandere.Inventory.Soda)
			{
				this.StudentManager.Students[8].TaskPhase = 5;
			}
		}
		if (TaskGlobals.GetTaskStatus(11) == 1)
		{
			if (this.StudentManager.Students[11] != null)
			{
				if (this.StudentManager.Students[11].TaskPhase == 0)
				{
					this.StudentManager.Students[11].TaskPhase = 4;
				}
				this.TaskObjects[11].SetActive(true);
			}
		}
		else if (this.TaskObjects[11] != null)
		{
			this.TaskObjects[11].SetActive(false);
		}
		if (TaskGlobals.GetTaskStatus(25) == 1)
		{
			if (this.StudentManager.Students[25] != null)
			{
				if (this.StudentManager.Students[25].TaskPhase == 0)
				{
					this.StudentManager.Students[25].TaskPhase = 4;
				}
				this.TaskObjects[25].SetActive(true);
			}
		}
		else if (this.TaskObjects[25] != null)
		{
			this.TaskObjects[25].SetActive(false);
		}
		if (TaskGlobals.GetTaskStatus(28) == 1 && this.StudentManager.Students[28] != null)
		{
			if (this.StudentManager.Students[28].TaskPhase == 0)
			{
				this.StudentManager.Students[28].TaskPhase = 4;
			}
			for (int i = 1; i < 26; i++)
			{
				if (TaskGlobals.GetKittenPhoto(i))
				{
					Debug.Log("Riku's Task can be turned in.");
					this.StudentManager.Students[28].TaskPhase = 5;
				}
			}
		}
		if (TaskGlobals.GetTaskStatus(30) == 1 && this.StudentManager.Students[30] != null && this.StudentManager.Students[30].TaskPhase == 0)
		{
			this.StudentManager.Students[30].TaskPhase = 4;
		}
		if (TaskGlobals.GetTaskStatus(36) == 1 && this.StudentManager.Students[36] != null)
		{
			if (this.StudentManager.Students[36].TaskPhase == 0)
			{
				this.StudentManager.Students[36].TaskPhase = 4;
			}
			if (this.GirlsQuestioned[1] && this.GirlsQuestioned[2] && this.GirlsQuestioned[3] && this.GirlsQuestioned[4] && this.GirlsQuestioned[5])
			{
				Debug.Log("Gema's task should be ready to turn in!");
				this.StudentManager.Students[36].TaskPhase = 5;
			}
		}
		if (TaskGlobals.GetTaskStatus(37) == 1)
		{
			if (this.StudentManager.Students[37] != null)
			{
				if (this.StudentManager.Students[37].TaskPhase == 0)
				{
					this.StudentManager.Students[37].TaskPhase = 4;
				}
				this.TaskObjects[37].SetActive(true);
			}
		}
		else if (this.TaskObjects[37] != null)
		{
			this.TaskObjects[37].SetActive(false);
		}
		if (TaskGlobals.GetTaskStatus(38) == 1)
		{
			if (this.StudentManager.Students[38] != null && this.StudentManager.Students[38].TaskPhase == 0)
			{
				this.StudentManager.Students[38].TaskPhase = 4;
			}
		}
		else if (TaskGlobals.GetTaskStatus(38) == 2 && this.StudentManager.Students[38] != null)
		{
			this.StudentManager.Students[38].TaskPhase = 5;
		}
		if (ClubGlobals.GetClubClosed(ClubType.LightMusic) || this.StudentManager.Students[51] == null)
		{
			if (this.StudentManager.Students[52] != null)
			{
				this.StudentManager.Students[52].TaskPhase = 100;
			}
			TaskGlobals.SetTaskStatus(52, 100);
		}
		else if (TaskGlobals.GetTaskStatus(52) == 1 && this.StudentManager.Students[52] != null)
		{
			this.StudentManager.Students[52].TaskPhase = 4;
			for (int j = 1; j < 52; j++)
			{
				if (TaskGlobals.GetGuitarPhoto(j))
				{
					this.StudentManager.Students[52].TaskPhase = 5;
				}
			}
		}
		if (TaskGlobals.GetTaskStatus(81) == 1 && this.StudentManager.Students[81] != null)
		{
			if (this.StudentManager.Students[81].TaskPhase == 0)
			{
				this.StudentManager.Students[81].TaskPhase = 4;
			}
			for (int k = 1; k < 26; k++)
			{
				if (TaskGlobals.GetHorudaPhoto(k))
				{
					Debug.Log("Musume's Task can be turned in.");
					this.StudentManager.Students[81].TaskPhase = 5;
				}
			}
		}
		TaskGlobals.GetTaskStatus(81);
	}

	// Token: 0x0400346B RID: 13419
	public StudentManagerScript StudentManager;

	// Token: 0x0400346C RID: 13420
	public YandereScript Yandere;

	// Token: 0x0400346D RID: 13421
	public GameObject[] TaskObjects;

	// Token: 0x0400346E RID: 13422
	public PromptScript[] Prompts;

	// Token: 0x0400346F RID: 13423
	public bool[] GirlsQuestioned;
}
