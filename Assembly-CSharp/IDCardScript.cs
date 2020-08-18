using System;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class IDCardScript : MonoBehaviour
{
	// Token: 0x06001763 RID: 5987 RVA: 0x000C9AC4 File Offset: 0x000C7CC4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Prompt.Yandere.StolenObject = base.gameObject;
			if (!this.Fake)
			{
				this.Prompt.Yandere.Inventory.IDCard = true;
				this.Prompt.Yandere.TheftTimer = 1f;
			}
			else
			{
				this.Prompt.Yandere.Inventory.FakeID = true;
			}
			this.Prompt.Hide();
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04002071 RID: 8305
	public PromptScript Prompt;

	// Token: 0x04002072 RID: 8306
	public bool Fake;
}
