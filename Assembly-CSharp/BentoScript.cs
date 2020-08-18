using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class BentoScript : MonoBehaviour
{
	// Token: 0x06000A44 RID: 2628 RVA: 0x000545BF File Offset: 0x000527BF
	private void Start()
	{
		if (this.Prompt.Yandere != null)
		{
			this.Yandere = this.Prompt.Yandere;
		}
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x000545E8 File Offset: 0x000527E8
	private void Update()
	{
		if (this.Yandere == null)
		{
			if (this.Prompt.Yandere != null)
			{
				this.Yandere = this.Prompt.Yandere;
				return;
			}
		}
		else if (this.Yandere.Inventory.EmeticPoison || this.Yandere.Inventory.RatPoison || this.Yandere.Inventory.LethalPoison)
		{
			this.Prompt.enabled = true;
			if (!this.Yandere.Inventory.EmeticPoison && !this.Yandere.Inventory.RatPoison)
			{
				this.Prompt.HideButton[0] = true;
			}
			else
			{
				this.Prompt.HideButton[0] = false;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				if (this.Yandere.Inventory.EmeticPoison)
				{
					this.Yandere.Inventory.EmeticPoison = false;
					this.Yandere.PoisonType = 1;
				}
				else
				{
					this.Yandere.Inventory.RatPoison = false;
					this.Yandere.PoisonType = 3;
				}
				this.Yandere.CharacterAnimation.CrossFade("f02_poisoning_00");
				this.Yandere.PoisonSpot = this.PoisonSpot;
				this.Yandere.Poisoning = true;
				this.Yandere.CanMove = false;
				base.enabled = false;
				this.Poison = 1;
				if (this.ID != 1)
				{
					this.StudentManager.Students[this.ID].Emetic = true;
				}
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
			if (this.ID == 11 || this.ID == 6)
			{
				this.Prompt.HideButton[1] = !this.Prompt.Yandere.Inventory.LethalPoison;
				if (this.Prompt.Circle[1].fillAmount == 0f)
				{
					this.Prompt.Yandere.CharacterAnimation.CrossFade("f02_poisoning_00");
					this.Prompt.Yandere.Inventory.LethalPoison = false;
					this.StudentManager.Students[this.ID].Lethal = true;
					this.Prompt.Yandere.PoisonSpot = this.PoisonSpot;
					this.Prompt.Yandere.Poisoning = true;
					this.Prompt.Yandere.CanMove = false;
					this.Prompt.Yandere.PoisonType = 2;
					base.enabled = false;
					this.Poison = 2;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					return;
				}
			}
		}
		else
		{
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04000A7D RID: 2685
	public StudentManagerScript StudentManager;

	// Token: 0x04000A7E RID: 2686
	public YandereScript Yandere;

	// Token: 0x04000A7F RID: 2687
	public Transform PoisonSpot;

	// Token: 0x04000A80 RID: 2688
	public PromptScript Prompt;

	// Token: 0x04000A81 RID: 2689
	public int Poison;

	// Token: 0x04000A82 RID: 2690
	public int ID;
}
