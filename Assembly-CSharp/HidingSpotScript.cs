using System;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class HidingSpotScript : MonoBehaviour
{
	// Token: 0x060016FB RID: 5883 RVA: 0x000C1B5C File Offset: 0x000BFD5C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0 && this.Prompt.Yandere.Pursuer == null)
			{
				this.Prompt.Yandere.MyController.center = new Vector3(this.Prompt.Yandere.MyController.center.x, 0.3f, this.Prompt.Yandere.MyController.center.z);
				this.Prompt.Yandere.MyController.radius = 0f;
				this.Prompt.Yandere.MyController.height = 0.5f;
				this.Prompt.Yandere.HideAnim = this.AnimName;
				this.Prompt.Yandere.HidingSpot = this.Spot;
				this.Prompt.Yandere.ExitSpot = this.Exit;
				this.Prompt.Yandere.CanMove = false;
				this.Prompt.Yandere.Hiding = true;
				this.Prompt.Yandere.EmptyHands();
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}
	}

	// Token: 0x04001EEF RID: 7919
	public PromptBarScript PromptBar;

	// Token: 0x04001EF0 RID: 7920
	public PromptScript Prompt;

	// Token: 0x04001EF1 RID: 7921
	public Transform Exit;

	// Token: 0x04001EF2 RID: 7922
	public Transform Spot;

	// Token: 0x04001EF3 RID: 7923
	public string AnimName;
}
