using System;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class RoseBushScript : MonoBehaviour
{
	// Token: 0x060019D1 RID: 6609 RVA: 0x000FC610 File Offset: 0x000FA810
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Rose = true;
			base.enabled = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04002864 RID: 10340
	public PromptScript Prompt;
}
