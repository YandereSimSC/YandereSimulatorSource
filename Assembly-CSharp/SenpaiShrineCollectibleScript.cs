using System;
using UnityEngine;

// Token: 0x020003D1 RID: 977
public class SenpaiShrineCollectibleScript : MonoBehaviour
{
	// Token: 0x06001A5B RID: 6747 RVA: 0x00101EEA File Offset: 0x001000EA
	private void Start()
	{
		if (PlayerGlobals.GetShrineCollectible(this.ID))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x00101F04 File Offset: 0x00100104
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.ShrineCollectibles[this.ID] = true;
			this.Prompt.Hide();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040029BF RID: 10687
	public PromptScript Prompt;

	// Token: 0x040029C0 RID: 10688
	public int ID;
}
