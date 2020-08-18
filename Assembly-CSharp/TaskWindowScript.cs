using System;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public class TaskWindowScript : MonoBehaviour
{
	// Token: 0x06001C15 RID: 7189 RVA: 0x0014F349 File Offset: 0x0014D549
	private void Start()
	{
		this.Window.SetActive(false);
		this.UpdateTaskObjects(30);
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x0014F360 File Offset: 0x0014D560
	public void UpdateWindow(int ID)
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[0].text = "Accept";
		this.PromptBar.Label[1].text = "Refuse";
		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;
		this.GetPortrait(ID);
		this.StudentID = ID;
		this.GenericCheck();
		if (this.Generic)
		{
			ID = 0;
			this.Generic = false;
		}
		this.TaskDescLabel.transform.parent.gameObject.SetActive(true);
		this.TaskDescLabel.text = this.Descriptions[ID];
		this.Icon.mainTexture = this.Icons[ID];
		this.Window.SetActive(true);
		Time.timeScale = 0.0001f;
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x0014F43C File Offset: 0x0014D63C
	private void Update()
	{
		if (this.Window.activeInHierarchy)
		{
			if (Input.GetButtonDown("A"))
			{
				TaskGlobals.SetTaskStatus(this.StudentID, 1);
				this.Yandere.TargetStudent.TalkTimer = 100f;
				this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
				this.Yandere.TargetStudent.TaskPhase = 4;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Window.SetActive(false);
				this.UpdateTaskObjects(this.StudentID);
				Time.timeScale = 1f;
			}
			else if (Input.GetButtonDown("B"))
			{
				this.Yandere.TargetStudent.TalkTimer = 100f;
				this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
				this.Yandere.TargetStudent.TaskPhase = 0;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Window.SetActive(false);
				Time.timeScale = 1f;
			}
		}
		if (this.TaskComplete)
		{
			if (this.TrueTimer == 0f)
			{
				base.GetComponent<AudioSource>().Play();
			}
			this.TrueTimer += Time.deltaTime;
			this.Timer += Time.deltaTime;
			if (this.ID < this.TaskCompleteLetters.Length && this.Timer > 0.05f)
			{
				this.TaskCompleteLetters[this.ID].SetActive(true);
				this.Timer = 0f;
				this.ID++;
			}
			if (this.TaskCompleteLetters[12].transform.localPosition.y < -725f)
			{
				this.ID = 0;
				while (this.ID < this.TaskCompleteLetters.Length)
				{
					this.TaskCompleteLetters[this.ID].GetComponent<GrowShrinkScript>().Return();
					this.ID++;
				}
				this.TaskCheck();
				this.DialogueWheel.End();
				this.TaskComplete = false;
				this.TrueTimer = 0f;
				this.Timer = 0f;
				this.ID = 0;
			}
		}
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x0014F678 File Offset: 0x0014D878
	private void TaskCheck()
	{
		if (this.Yandere.TargetStudent.StudentID == 37)
		{
			this.DialogueWheel.Yandere.TargetStudent.Cosmetic.MaleAccessories[1].SetActive(true);
		}
		this.GenericCheck();
		if (this.Generic)
		{
			this.Yandere.Inventory.Book = false;
			this.CheckOutBook.UpdatePrompt();
			this.Generic = false;
		}
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x0014F6EC File Offset: 0x0014D8EC
	private void GetPortrait(int ID)
	{
		WWW www = new WWW(string.Concat(new string[]
		{
			"file:///",
			Application.streamingAssetsPath,
			"/Portraits/Student_",
			ID.ToString(),
			".png"
		}));
		this.Portrait.mainTexture = www.texture;
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x0014F745 File Offset: 0x0014D945
	private void UpdateTaskObjects(int StudentID)
	{
		if (this.StudentID == 30)
		{
			this.SewingMachine.Check = true;
		}
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x0014F760 File Offset: 0x0014D960
	public void GenericCheck()
	{
		this.Generic = false;
		if (this.Yandere.TargetStudent.StudentID != 8 && this.Yandere.TargetStudent.StudentID != 11 && this.Yandere.TargetStudent.StudentID != 25 && this.Yandere.TargetStudent.StudentID != 28 && this.Yandere.TargetStudent.StudentID != 30 && this.Yandere.TargetStudent.StudentID != 36 && this.Yandere.TargetStudent.StudentID != 37 && this.Yandere.TargetStudent.StudentID != 38 && this.Yandere.TargetStudent.StudentID != 52 && this.Yandere.TargetStudent.StudentID != 76 && this.Yandere.TargetStudent.StudentID != 77 && this.Yandere.TargetStudent.StudentID != 78 && this.Yandere.TargetStudent.StudentID != 79 && this.Yandere.TargetStudent.StudentID != 80 && this.Yandere.TargetStudent.StudentID != 81)
		{
			this.Generic = true;
		}
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x0014F8C0 File Offset: 0x0014DAC0
	public void AltGenericCheck(int TempID)
	{
		this.Generic = false;
		if (TempID != 8 && TempID != 11 && TempID != 25 && TempID != 28 && TempID != 30 && TempID != 36 && TempID != 37 && TempID != 38 && TempID != 52 && TempID != 76 && TempID != 77 && TempID != 78 && TempID != 79 && TempID != 80 && TempID != 81)
		{
			this.Generic = true;
		}
	}

	// Token: 0x04003472 RID: 13426
	public DialogueWheelScript DialogueWheel;

	// Token: 0x04003473 RID: 13427
	public SewingMachineScript SewingMachine;

	// Token: 0x04003474 RID: 13428
	public CheckOutBookScript CheckOutBook;

	// Token: 0x04003475 RID: 13429
	public TaskManagerScript TaskManager;

	// Token: 0x04003476 RID: 13430
	public PromptBarScript PromptBar;

	// Token: 0x04003477 RID: 13431
	public UILabel TaskDescLabel;

	// Token: 0x04003478 RID: 13432
	public YandereScript Yandere;

	// Token: 0x04003479 RID: 13433
	public UITexture Portrait;

	// Token: 0x0400347A RID: 13434
	public UITexture Icon;

	// Token: 0x0400347B RID: 13435
	public GameObject[] TaskCompleteLetters;

	// Token: 0x0400347C RID: 13436
	public string[] Descriptions;

	// Token: 0x0400347D RID: 13437
	public Texture[] Portraits;

	// Token: 0x0400347E RID: 13438
	public Texture[] Icons;

	// Token: 0x0400347F RID: 13439
	public bool TaskComplete;

	// Token: 0x04003480 RID: 13440
	public bool Generic;

	// Token: 0x04003481 RID: 13441
	public GameObject Window;

	// Token: 0x04003482 RID: 13442
	public int StudentID;

	// Token: 0x04003483 RID: 13443
	public int ID;

	// Token: 0x04003484 RID: 13444
	public float TrueTimer;

	// Token: 0x04003485 RID: 13445
	public float Timer;
}
