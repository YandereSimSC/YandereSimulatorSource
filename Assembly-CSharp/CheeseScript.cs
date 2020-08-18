using System;
using UnityEngine;

// Token: 0x02000233 RID: 563
public class CheeseScript : MonoBehaviour
{
	// Token: 0x06001236 RID: 4662 RVA: 0x00080DA0 File Offset: 0x0007EFA0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Subtitle.text = "Knowing the mouse might one day leave its hole and get the cheese...It fills you with determination.";
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.GlowingEye.SetActive(true);
			this.Timer = 5f;
		}
		if (this.Timer > 0f)
		{
			this.Timer -= Time.deltaTime;
			if (this.Timer <= 0f)
			{
				this.Prompt.enabled = true;
				this.Subtitle.text = string.Empty;
			}
		}
	}

	// Token: 0x04001578 RID: 5496
	public GameObject GlowingEye;

	// Token: 0x04001579 RID: 5497
	public PromptScript Prompt;

	// Token: 0x0400157A RID: 5498
	public UILabel Subtitle;

	// Token: 0x0400157B RID: 5499
	public float Timer;
}
