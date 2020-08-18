using System;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class HeadsetScript : MonoBehaviour
{
	// Token: 0x060016EB RID: 5867 RVA: 0x000BE5F8 File Offset: 0x000BC7F8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Headset = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001E77 RID: 7799
	public PromptScript Prompt;
}
