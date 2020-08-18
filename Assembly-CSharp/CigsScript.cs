using System;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class CigsScript : MonoBehaviour
{
	// Token: 0x0600123F RID: 4671 RVA: 0x00081368 File Offset: 0x0007F568
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			SchemeGlobals.SetSchemeStage(3, 3);
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Cigs = true;
			UnityEngine.Object.Destroy(base.gameObject);
			this.Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
		}
	}

	// Token: 0x0400158C RID: 5516
	public PromptScript Prompt;
}
