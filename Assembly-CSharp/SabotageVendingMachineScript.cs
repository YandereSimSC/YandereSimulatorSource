using System;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class SabotageVendingMachineScript : MonoBehaviour
{
	// Token: 0x060019DA RID: 6618 RVA: 0x000FC9AD File Offset: 0x000FABAD
	private void Start()
	{
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x060019DB RID: 6619 RVA: 0x000FC9C8 File Offset: 0x000FABC8
	private void Update()
	{
		if (this.Yandere.Armed)
		{
			if (this.Yandere.EquippedWeapon.WeaponID == 6)
			{
				this.Prompt.enabled = true;
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					if (SchemeGlobals.GetSchemeStage(4) == 2)
					{
						SchemeGlobals.SetSchemeStage(4, 3);
						this.Yandere.PauseScreen.Schemes.UpdateInstructions();
					}
					if (this.Yandere.StudentManager.Students[11] != null && DateGlobals.Weekday == DayOfWeek.Thursday)
					{
						this.Yandere.StudentManager.Students[11].Hungry = true;
						this.Yandere.StudentManager.Students[11].Fed = false;
					}
					UnityEngine.Object.Instantiate<GameObject>(this.SabotageSparks, new Vector3(-2.5f, 5.3605f, -32.982f), Quaternion.identity);
					this.VendingMachine.Sabotaged = true;
					this.Prompt.enabled = false;
					this.Prompt.Hide();
					base.enabled = false;
					return;
				}
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
	}

	// Token: 0x04002872 RID: 10354
	public VendingMachineScript VendingMachine;

	// Token: 0x04002873 RID: 10355
	public GameObject SabotageSparks;

	// Token: 0x04002874 RID: 10356
	public YandereScript Yandere;

	// Token: 0x04002875 RID: 10357
	public PromptScript Prompt;
}
