using System;
using UnityEngine;

// Token: 0x02000394 RID: 916
public class RivalDeskScript : MonoBehaviour
{
	// Token: 0x060019B0 RID: 6576 RVA: 0x000FB8CF File Offset: 0x000F9ACF
	private void Start()
	{
		if (DateGlobals.Weekday != DayOfWeek.Friday)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x000FB8E0 File Offset: 0x000F9AE0
	private void Update()
	{
		if (!this.Prompt.Yandere.Inventory.AnswerSheet && this.Prompt.Yandere.Inventory.DuplicateSheet)
		{
			this.Prompt.enabled = true;
			if (this.Clock.HourTime > 13f)
			{
				this.Prompt.HideButton[0] = false;
				if (this.Clock.HourTime > 13.5f)
				{
					SchemeGlobals.SetSchemeStage(5, 100);
					this.Schemes.UpdateInstructions();
					this.Prompt.HideButton[0] = true;
				}
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				SchemeGlobals.SetSchemeStage(5, 9);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.DuplicateSheet = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Cheating = true;
				base.enabled = false;
			}
		}
	}

	// Token: 0x040027AE RID: 10158
	public SchemesScript Schemes;

	// Token: 0x040027AF RID: 10159
	public ClockScript Clock;

	// Token: 0x040027B0 RID: 10160
	public PromptScript Prompt;

	// Token: 0x040027B1 RID: 10161
	public bool Cheating;
}
