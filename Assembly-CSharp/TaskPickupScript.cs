using System;
using UnityEngine;

// Token: 0x02000419 RID: 1049
public class TaskPickupScript : MonoBehaviour
{
	// Token: 0x06001C13 RID: 7187 RVA: 0x0014F300 File Offset: 0x0014D500
	private void Update()
	{
		if (this.Prompt.Circle[this.ButtonID].fillAmount == 0f)
		{
			this.Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
		}
	}

	// Token: 0x04003470 RID: 13424
	public PromptScript Prompt;

	// Token: 0x04003471 RID: 13425
	public int ButtonID = 3;
}
