using System;
using UnityEngine;

// Token: 0x02000235 RID: 565
public class ChinaDressScript : MonoBehaviour
{
	// Token: 0x0600123A RID: 4666 RVA: 0x000810BC File Offset: 0x0007F2BC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.WearChinaDress();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x04001585 RID: 5509
	public PromptScript Prompt;
}
