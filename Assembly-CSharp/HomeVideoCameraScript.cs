using System;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class HomeVideoCameraScript : MonoBehaviour
{
	// Token: 0x0600174A RID: 5962 RVA: 0x000C8510 File Offset: 0x000C6710
	private void Update()
	{
		if (!this.TextSet && !HomeGlobals.Night)
		{
			this.Prompt.Label[0].text = "     Only Available At Night";
		}
		if (!HomeGlobals.Night)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.HomeCamera.Destination = this.HomeCamera.Destinations[11];
			this.HomeCamera.Target = this.HomeCamera.Targets[11];
			this.HomeCamera.ID = 11;
			this.HomePrisonerChan.LookAhead = true;
			this.HomeYandere.CanMove = false;
			this.HomeYandere.gameObject.SetActive(false);
		}
		if (this.HomeCamera.ID == 11 && !this.HomePrisoner.Bantering)
		{
			this.Timer += Time.deltaTime;
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.Timer > 2f && !this.AudioPlayed)
			{
				this.Subtitle.text = "...daddy...please...help...I'm scared...I don't wanna die...";
				this.AudioPlayed = true;
				component.Play();
			}
			if (this.Timer > 2f + component.clip.length)
			{
				this.Subtitle.text = string.Empty;
			}
			if (this.Timer > 3f + component.clip.length)
			{
				this.HomeDarkness.FadeSlow = true;
				this.HomeDarkness.FadeOut = true;
			}
		}
	}

	// Token: 0x04002030 RID: 8240
	public HomePrisonerChanScript HomePrisonerChan;

	// Token: 0x04002031 RID: 8241
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04002032 RID: 8242
	public HomePrisonerScript HomePrisoner;

	// Token: 0x04002033 RID: 8243
	public HomeYandereScript HomeYandere;

	// Token: 0x04002034 RID: 8244
	public HomeCameraScript HomeCamera;

	// Token: 0x04002035 RID: 8245
	public PromptScript Prompt;

	// Token: 0x04002036 RID: 8246
	public UILabel Subtitle;

	// Token: 0x04002037 RID: 8247
	public bool AudioPlayed;

	// Token: 0x04002038 RID: 8248
	public bool TextSet;

	// Token: 0x04002039 RID: 8249
	public float Timer;
}
