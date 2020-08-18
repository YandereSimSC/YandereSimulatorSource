using System;
using UnityEngine;

// Token: 0x02000470 RID: 1136
public class YandereShoeLockerScript : MonoBehaviour
{
	// Token: 0x06001D92 RID: 7570 RVA: 0x0016FEF4 File Offset: 0x0016E0F4
	private void Update()
	{
		if (this.Yandere.Schoolwear == 1 && !this.Yandere.ClubAttire && !this.Yandere.Egg)
		{
			if (this.Label == 2)
			{
				this.Prompt.Label[0].text = "     Change Shoes";
				this.Label = 1;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Yandere.Casual = !this.Yandere.Casual;
				this.Yandere.ChangeSchoolwear();
				this.Yandere.CanMove = true;
				return;
			}
		}
		else
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Label == 1)
			{
				this.Prompt.Label[0].text = "     Not Available";
				this.Label = 2;
			}
		}
	}

	// Token: 0x04003A6C RID: 14956
	public YandereScript Yandere;

	// Token: 0x04003A6D RID: 14957
	public PromptScript Prompt;

	// Token: 0x04003A6E RID: 14958
	public int Label = 1;
}
