using System;
using UnityEngine;

// Token: 0x0200042E RID: 1070
public class TranquilizerScript : MonoBehaviour
{
	// Token: 0x06001C63 RID: 7267 RVA: 0x00153524 File Offset: 0x00151724
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Tranquilizer = true;
			this.Prompt.Yandere.StudentManager.UpdateAllBentos();
			this.Prompt.Yandere.TheftTimer = 0.1f;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400354C RID: 13644
	public PromptScript Prompt;
}
