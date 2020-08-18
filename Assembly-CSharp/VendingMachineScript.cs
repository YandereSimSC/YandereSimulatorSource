using System;
using UnityEngine;

// Token: 0x0200045A RID: 1114
public class VendingMachineScript : MonoBehaviour
{
	// Token: 0x06001CE8 RID: 7400 RVA: 0x00155DD0 File Offset: 0x00153FD0
	private void Start()
	{
		if (this.SnackMachine)
		{
			this.Prompt.Text[0] = "Buy Snack for $" + this.Price + ".00";
		}
		else
		{
			this.Prompt.Text[0] = "Buy Drink for $" + this.Price + ".00";
		}
		this.Prompt.Label[0].text = "     " + this.Prompt.Text[0];
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x00155E60 File Offset: 0x00154060
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Yandere.Inventory.Money >= (float)this.Price)
			{
				if (!this.Sabotaged)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.Cans[UnityEngine.Random.Range(0, this.Cans.Length)], this.CanSpawn.position, this.CanSpawn.rotation).GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f, 1.1f);
				}
				if (this.SnackMachine && SchemeGlobals.GetSchemeStage(4) == 3)
				{
					SchemeGlobals.SetSchemeStage(4, 4);
					this.Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
				}
				this.Prompt.Yandere.Inventory.Money -= (float)this.Price;
				this.Prompt.Yandere.Inventory.UpdateMoney();
				return;
			}
			this.Prompt.Yandere.NotificationManager.CustomText = "Not enough money!";
			this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
		}
	}

	// Token: 0x040035FE RID: 13822
	public PromptScript Prompt;

	// Token: 0x040035FF RID: 13823
	public Transform CanSpawn;

	// Token: 0x04003600 RID: 13824
	public GameObject[] Cans;

	// Token: 0x04003601 RID: 13825
	public bool SnackMachine;

	// Token: 0x04003602 RID: 13826
	public bool Sabotaged;

	// Token: 0x04003603 RID: 13827
	public int Price;
}
