using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000417 RID: 1047
public class TaskListScript : MonoBehaviour
{
	// Token: 0x06001C0B RID: 7179 RVA: 0x0014E9CC File Offset: 0x0014CBCC
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			if (this.ID == 1)
			{
				this.ListPosition--;
				if (this.ListPosition < 0)
				{
					this.ListPosition = 84;
					this.ID = 16;
				}
			}
			else
			{
				this.ID--;
			}
			this.UpdateTaskList();
			base.StartCoroutine(this.UpdateTaskInfo());
		}
		if (this.InputManager.TappedDown)
		{
			if (this.ID == 16)
			{
				this.ListPosition++;
				if (this.ListPosition > 84)
				{
					this.ListPosition = 0;
					this.ID = 1;
				}
			}
			else
			{
				this.ID++;
			}
			this.UpdateTaskList();
			base.StartCoroutine(this.UpdateTaskInfo());
		}
		if (Input.GetButtonDown("B"))
		{
			this.PauseScreen.PromptBar.ClearButtons();
			this.PauseScreen.PromptBar.Label[0].text = "Accept";
			this.PauseScreen.PromptBar.Label[1].text = "Back";
			this.PauseScreen.PromptBar.Label[4].text = "Choose";
			this.PauseScreen.PromptBar.Label[5].text = "Choose";
			this.PauseScreen.PromptBar.UpdateButtons();
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			this.MainMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x0014EB68 File Offset: 0x0014CD68
	public void UpdateTaskList()
	{
		for (int i = 1; i < this.TaskNameLabels.Length; i++)
		{
			if (TaskGlobals.GetTaskStatus(i + this.ListPosition) == 0)
			{
				this.TaskNameLabels[i].text = "Undiscovered Task #" + (i + this.ListPosition);
			}
			else
			{
				this.TaskNameLabels[i].text = this.JSON.Students[i + this.ListPosition].Name + "'s Task";
			}
			this.Checkmarks[i].enabled = (TaskGlobals.GetTaskStatus(i + this.ListPosition) == 3);
		}
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x0014EC10 File Offset: 0x0014CE10
	public IEnumerator UpdateTaskInfo()
	{
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.ID, this.Highlight.localPosition.z);
		if (TaskGlobals.GetTaskStatus(this.ID + this.ListPosition) == 0)
		{
			this.StudentIcon.mainTexture = this.Silhouette;
			this.TaskIcon.mainTexture = this.QuestionMark;
			this.TaskDesc.text = "This task has not been discovered yet.";
		}
		else
		{
			string url = string.Concat(new string[]
			{
				"file:///",
				Application.streamingAssetsPath,
				"/Portraits/Student_",
				(this.ID + this.ListPosition).ToString(),
				".png"
			});
			WWW www = new WWW(url);
			yield return www;
			this.StudentIcon.mainTexture = www.texture;
			this.TaskWindow.AltGenericCheck(this.ID + this.ListPosition);
			if (this.TaskWindow.Generic)
			{
				this.TaskIcon.mainTexture = this.TaskWindow.Icons[0];
				this.TaskDesc.text = this.TaskWindow.Descriptions[0];
			}
			else
			{
				this.TaskIcon.mainTexture = this.TaskWindow.Icons[this.ID + this.ListPosition];
				this.TaskDesc.text = this.TaskWindow.Descriptions[this.ID + this.ListPosition];
			}
			www = null;
		}
		yield break;
	}

	// Token: 0x0400345C RID: 13404
	public InputManagerScript InputManager;

	// Token: 0x0400345D RID: 13405
	public PauseScreenScript PauseScreen;

	// Token: 0x0400345E RID: 13406
	public TaskWindowScript TaskWindow;

	// Token: 0x0400345F RID: 13407
	public JsonScript JSON;

	// Token: 0x04003460 RID: 13408
	public GameObject MainMenu;

	// Token: 0x04003461 RID: 13409
	public UITexture StudentIcon;

	// Token: 0x04003462 RID: 13410
	public UITexture TaskIcon;

	// Token: 0x04003463 RID: 13411
	public UILabel TaskDesc;

	// Token: 0x04003464 RID: 13412
	public Texture QuestionMark;

	// Token: 0x04003465 RID: 13413
	public Transform Highlight;

	// Token: 0x04003466 RID: 13414
	public Texture Silhouette;

	// Token: 0x04003467 RID: 13415
	public UILabel[] TaskNameLabels;

	// Token: 0x04003468 RID: 13416
	public UISprite[] Checkmarks;

	// Token: 0x04003469 RID: 13417
	public int ListPosition;

	// Token: 0x0400346A RID: 13418
	public int ID = 1;
}
