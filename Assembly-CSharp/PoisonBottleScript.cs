using System;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class PoisonBottleScript : MonoBehaviour
{
	// Token: 0x06001901 RID: 6401 RVA: 0x000EA338 File Offset: 0x000E8538
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Theft)
			{
				this.Prompt.Yandere.TheftTimer = 0.1f;
			}
			if (this.ID == 1)
			{
				this.Prompt.Yandere.Inventory.EmeticPoison = true;
			}
			else if (this.ID == 2)
			{
				this.Prompt.Yandere.Inventory.LethalPoison = true;
			}
			else if (this.ID == 3)
			{
				this.Prompt.Yandere.Inventory.RatPoison = true;
			}
			else if (this.ID == 4)
			{
				this.Prompt.Yandere.Inventory.HeadachePoison = true;
			}
			else if (this.ID == 5)
			{
				this.Prompt.Yandere.Inventory.Tranquilizer = true;
			}
			else if (this.ID == 6)
			{
				this.Prompt.Yandere.Inventory.Sedative = true;
			}
			this.Prompt.Yandere.StudentManager.UpdateAllBentos();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002590 RID: 9616
	public PromptScript Prompt;

	// Token: 0x04002591 RID: 9617
	public bool Theft;

	// Token: 0x04002592 RID: 9618
	public int ID;
}
