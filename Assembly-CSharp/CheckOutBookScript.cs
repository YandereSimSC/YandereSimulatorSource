using System;
using UnityEngine;

// Token: 0x02000231 RID: 561
public class CheckOutBookScript : MonoBehaviour
{
	// Token: 0x06001231 RID: 4657 RVA: 0x00080CB1 File Offset: 0x0007EEB1
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Book = true;
			this.UpdatePrompt();
		}
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x00080CE8 File Offset: 0x0007EEE8
	public void UpdatePrompt()
	{
		if (this.Prompt.Yandere.Inventory.Book)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			return;
		}
		this.Prompt.enabled = true;
		this.Prompt.Hide();
	}

	// Token: 0x04001574 RID: 5492
	public PromptScript Prompt;
}
