using System;
using UnityEngine;

// Token: 0x020003AB RID: 939
public class SakeScript : MonoBehaviour
{
	// Token: 0x060019E0 RID: 6624 RVA: 0x000FCD86 File Offset: 0x000FAF86
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Sake = true;
			this.UpdatePrompt();
		}
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x000FCDC0 File Offset: 0x000FAFC0
	public void UpdatePrompt()
	{
		if (this.Prompt.Yandere.Inventory.Sake)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.Prompt.enabled = true;
		this.Prompt.Hide();
	}

	// Token: 0x0400287E RID: 10366
	public PromptScript Prompt;
}
