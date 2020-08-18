using System;
using UnityEngine;

// Token: 0x020003E7 RID: 999
public class SodaScript : MonoBehaviour
{
	// Token: 0x06001AB9 RID: 6841 RVA: 0x0010C310 File Offset: 0x0010A510
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Soda = true;
			this.Prompt.Yandere.StudentManager.TaskManager.UpdateTaskStatus();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002B3A RID: 11066
	public PromptScript Prompt;
}
