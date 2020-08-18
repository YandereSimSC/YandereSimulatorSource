using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class LeaveGiftScript : MonoBehaviour
{
	// Token: 0x060017DE RID: 6110 RVA: 0x000D1D2C File Offset: 0x000CFF2C
	private void Start()
	{
		this.Box.SetActive(false);
		this.EndOfDay.SenpaiGifts = CollectibleGlobals.SenpaiGifts;
		if (this.EndOfDay.SenpaiGifts == 0)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x060017DF RID: 6111 RVA: 0x000D1D80 File Offset: 0x000CFF80
	private void Update()
	{
		Debug.Log(Vector3.Distance(this.Prompt.Yandere.transform.position, this.Prompt.Yandere.Senpai.position));
		if (this.Prompt.InView)
		{
			if (Vector3.Distance(this.Prompt.Yandere.transform.position, this.Prompt.Yandere.Senpai.position) > 10f)
			{
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.EndOfDay.SenpaiGifts--;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					this.Box.SetActive(true);
					base.enabled = false;
					return;
				}
			}
			else
			{
				this.Prompt.Hide();
			}
		}
	}

	// Token: 0x04002225 RID: 8741
	public EndOfDayScript EndOfDay;

	// Token: 0x04002226 RID: 8742
	public PromptScript Prompt;

	// Token: 0x04002227 RID: 8743
	public GameObject Box;
}
