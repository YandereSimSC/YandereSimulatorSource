using System;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class RingScript : MonoBehaviour
{
	// Token: 0x060019AB RID: 6571 RVA: 0x000FB65C File Offset: 0x000F985C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			SchemeGlobals.SetSchemeStage(2, 2);
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Ring = true;
			this.Prompt.Yandere.TheftTimer = 0.1f;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040027AA RID: 10154
	public PromptScript Prompt;
}
