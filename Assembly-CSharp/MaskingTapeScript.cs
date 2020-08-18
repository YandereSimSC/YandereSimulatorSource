using System;
using UnityEngine;

// Token: 0x02000328 RID: 808
public class MaskingTapeScript : MonoBehaviour
{
	// Token: 0x0600180D RID: 6157 RVA: 0x000D645C File Offset: 0x000D465C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.MaskingTape = true;
			this.Box.Prompt.enabled = true;
			this.Box.enabled = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040022CC RID: 8908
	public CarryableCardboardBoxScript Box;

	// Token: 0x040022CD RID: 8909
	public PromptScript Prompt;
}
