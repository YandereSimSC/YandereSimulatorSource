using System;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class FindStudentLockerScript : MonoBehaviour
{
	// Token: 0x06001407 RID: 5127 RVA: 0x000AF3BC File Offset: 0x000AD5BC
	private void Update()
	{
		if (this.TargetedStudent == null)
		{
			if (this.Prompt.DistanceSqr < 5f)
			{
				this.TutorialWindow.ShowLockerMessage = true;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Yandere.PauseScreen.StudentInfoMenu.FindingLocker = true;
				this.Prompt.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
				this.Prompt.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
				this.Prompt.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
				this.Prompt.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
				this.Prompt.StartCoroutine(this.Prompt.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
				this.Prompt.Yandere.PauseScreen.MainMenu.SetActive(false);
				this.Prompt.Yandere.PauseScreen.Panel.enabled = true;
				this.Prompt.Yandere.PauseScreen.Sideways = true;
				this.Prompt.Yandere.PauseScreen.Show = true;
				Time.timeScale = 0.0001f;
				this.Prompt.Yandere.PromptBar.ClearButtons();
				this.Prompt.Yandere.PromptBar.Label[1].text = "Cancel";
				this.Prompt.Yandere.PromptBar.UpdateButtons();
				this.Prompt.Yandere.PromptBar.Show = true;
				return;
			}
		}
		else if (this.Phase == 1)
		{
			if (this.TargetedStudent.Meeting)
			{
				this.Phase++;
				return;
			}
		}
		else if (!this.TargetedStudent.Meeting)
		{
			this.Prompt.Label[0].text = "     Find Student Locker";
			this.TargetedStudent = null;
			this.Prompt.enabled = true;
			this.Phase = 1;
		}
	}

	// Token: 0x04001C2A RID: 7210
	public TutorialWindowScript TutorialWindow;

	// Token: 0x04001C2B RID: 7211
	public StudentScript TargetedStudent;

	// Token: 0x04001C2C RID: 7212
	public PromptScript Prompt;

	// Token: 0x04001C2D RID: 7213
	public int Phase = 1;
}
