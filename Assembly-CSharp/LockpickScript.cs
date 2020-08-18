using System;
using UnityEngine;

// Token: 0x02000322 RID: 802
public class LockpickScript : MonoBehaviour
{
	// Token: 0x060017F8 RID: 6136 RVA: 0x000D40A9 File Offset: 0x000D22A9
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.LockPick = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002285 RID: 8837
	public PromptScript Prompt;
}
