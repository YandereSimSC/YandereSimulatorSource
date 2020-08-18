using System;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class PaintBucketScript : MonoBehaviour
{
	// Token: 0x06001899 RID: 6297 RVA: 0x000E0FD0 File Offset: 0x000DF1D0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Yandere.Bloodiness == 0f)
			{
				this.Prompt.Yandere.Bloodiness += 100f;
				this.Prompt.Yandere.RedPaint = true;
			}
		}
	}

	// Token: 0x0400243B RID: 9275
	public PromptScript Prompt;
}
