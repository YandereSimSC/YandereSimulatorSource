using System;
using UnityEngine;

// Token: 0x0200022C RID: 556
public class ChainScript : MonoBehaviour
{
	// Token: 0x06001225 RID: 4645 RVA: 0x00080398 File Offset: 0x0007E598
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Yandere.Inventory.MysteriousKeys > 0)
			{
				AudioSource.PlayClipAtPoint(this.ChainRattle, base.transform.position);
				this.Unlocked++;
				this.Chains[this.Unlocked].SetActive(false);
				this.Prompt.Yandere.Inventory.MysteriousKeys--;
				if (this.Unlocked == 5)
				{
					this.Tarp.Prompt.enabled = true;
					this.Tarp.enabled = true;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x0400154D RID: 5453
	public PromptScript Prompt;

	// Token: 0x0400154E RID: 5454
	public TarpScript Tarp;

	// Token: 0x0400154F RID: 5455
	public AudioClip ChainRattle;

	// Token: 0x04001550 RID: 5456
	public GameObject[] Chains;

	// Token: 0x04001551 RID: 5457
	public int Unlocked;
}
