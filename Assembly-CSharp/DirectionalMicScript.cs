using System;
using UnityEngine;

// Token: 0x02000269 RID: 617
public class DirectionalMicScript : MonoBehaviour
{
	// Token: 0x06001347 RID: 4935 RVA: 0x000A4163 File Offset: 0x000A2363
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.DirectionalMic = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001A2E RID: 6702
	public PromptScript Prompt;
}
