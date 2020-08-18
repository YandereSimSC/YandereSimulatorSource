using System;
using UnityEngine;

// Token: 0x02000393 RID: 915
public class RivalBagScript : MonoBehaviour
{
	// Token: 0x060019AD RID: 6573 RVA: 0x000FB6DA File Offset: 0x000F98DA
	private void Start()
	{
		this.Prompt.enabled = false;
		this.Prompt.Hide();
		base.enabled = false;
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x000FB6FC File Offset: 0x000F98FC
	private void Update()
	{
		if (this.Clock.Period == 2 || this.Clock.Period == 4)
		{
			this.Prompt.HideButton[0] = true;
		}
		else if (this.Prompt.Yandere.Inventory.Cigs)
		{
			this.Prompt.HideButton[0] = false;
		}
		else
		{
			this.Prompt.HideButton[0] = true;
		}
		if (this.Prompt.Yandere.Inventory.Cigs)
		{
			this.Prompt.enabled = true;
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				SchemeGlobals.SetSchemeStage(3, 4);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.Cigs = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				base.enabled = false;
			}
		}
		if (this.Clock.Period == 2 || this.Clock.Period == 4)
		{
			this.Prompt.HideButton[1] = true;
		}
		else if (this.Prompt.Yandere.Inventory.Ring)
		{
			this.Prompt.HideButton[1] = false;
		}
		else
		{
			this.Prompt.HideButton[1] = true;
		}
		if (this.Prompt.Yandere.Inventory.Ring)
		{
			this.Prompt.enabled = true;
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				SchemeGlobals.SetSchemeStage(2, 3);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.Ring = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				base.enabled = false;
			}
		}
	}

	// Token: 0x040027AB RID: 10155
	public SchemesScript Schemes;

	// Token: 0x040027AC RID: 10156
	public ClockScript Clock;

	// Token: 0x040027AD RID: 10157
	public PromptScript Prompt;
}
