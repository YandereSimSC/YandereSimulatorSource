using System;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class PeekScript : MonoBehaviour
{
	// Token: 0x060018AC RID: 6316 RVA: 0x000E3543 File Offset: 0x000E1743
	private void Start()
	{
		this.Prompt.Door = true;
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x000E3554 File Offset: 0x000E1754
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Prompt.Yandere.transform.position) < 2f)
		{
			this.Prompt.Yandere.StudentManager.TutorialWindow.ShowInfoMessage = true;
		}
		if (this.InfoChanWindow.Drop)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0)
			{
				this.Prompt.Yandere.CanMove = false;
				this.PeekCamera.SetActive(true);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}
		if (this.PeekCamera.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f && !this.Spoke)
			{
				this.Subtitle.UpdateLabel(SubtitleType.InfoNotice, 0, 6.5f);
				this.Spoke = true;
				base.GetComponent<AudioSource>().Play();
			}
			if (Input.GetButtonDown("B") || this.Prompt.Yandere.Noticed || this.Prompt.Yandere.Sprayed)
			{
				if (!this.Prompt.Yandere.Noticed && !this.Prompt.Yandere.Sprayed)
				{
					this.Prompt.Yandere.CanMove = true;
				}
				this.PeekCamera.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x04002487 RID: 9351
	public InfoChanWindowScript InfoChanWindow;

	// Token: 0x04002488 RID: 9352
	public PromptBarScript PromptBar;

	// Token: 0x04002489 RID: 9353
	public SubtitleScript Subtitle;

	// Token: 0x0400248A RID: 9354
	public JukeboxScript Jukebox;

	// Token: 0x0400248B RID: 9355
	public PromptScript Prompt;

	// Token: 0x0400248C RID: 9356
	public GameObject PeekCamera;

	// Token: 0x0400248D RID: 9357
	public bool Spoke;

	// Token: 0x0400248E RID: 9358
	public float Timer;
}
