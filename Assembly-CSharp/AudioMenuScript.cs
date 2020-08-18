using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class AudioMenuScript : MonoBehaviour
{
	// Token: 0x06000A37 RID: 2615 RVA: 0x00053A15 File Offset: 0x00051C15
	private void Start()
	{
		this.UpdateText();
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00053A20 File Offset: 0x00051C20
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.CustomMusicMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			this.UpdateHighlight();
		}
		else if (this.InputManager.TappedDown)
		{
			this.Selected++;
			this.UpdateHighlight();
		}
		if (this.Selected == 1)
		{
			if (this.InputManager.TappedRight)
			{
				if (this.Jukebox.Volume < 1f)
				{
					this.Jukebox.Volume += 0.05f;
				}
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				if (this.Jukebox.Volume > 0f)
				{
					this.Jukebox.Volume -= 0.05f;
				}
				this.UpdateText();
			}
		}
		else if (this.Selected == 2 && (this.InputManager.TappedRight || this.InputManager.TappedLeft))
		{
			this.Jukebox.StartStopMusic();
			this.UpdateText();
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.ScreenBlur.enabled = true;
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00053BFC File Offset: 0x00051DFC
	public void UpdateText()
	{
		if (this.Jukebox != null)
		{
			this.CurrentTrackLabel.text = "Current Track: " + this.Jukebox.BGM;
			this.MusicVolumeLabel.text = ((this.Jukebox.Volume * 10f).ToString("F1") ?? "");
			if (this.Jukebox.Volume == 0f)
			{
				this.MusicOnOffLabel.text = "Off";
				return;
			}
			this.MusicOnOffLabel.text = "On";
		}
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00053CA4 File Offset: 0x00051EA4
	private void UpdateHighlight()
	{
		if (this.Selected == 0)
		{
			this.Selected = this.SelectionLimit;
		}
		else if (this.Selected > this.SelectionLimit)
		{
			this.Selected = 1;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 440f - 60f * (float)this.Selected, this.Highlight.localPosition.z);
	}

	// Token: 0x04000A55 RID: 2645
	public InputManagerScript InputManager;

	// Token: 0x04000A56 RID: 2646
	public PauseScreenScript PauseScreen;

	// Token: 0x04000A57 RID: 2647
	public PromptBarScript PromptBar;

	// Token: 0x04000A58 RID: 2648
	public JukeboxScript Jukebox;

	// Token: 0x04000A59 RID: 2649
	public UILabel CurrentTrackLabel;

	// Token: 0x04000A5A RID: 2650
	public UILabel MusicVolumeLabel;

	// Token: 0x04000A5B RID: 2651
	public UILabel MusicOnOffLabel;

	// Token: 0x04000A5C RID: 2652
	public int SelectionLimit = 5;

	// Token: 0x04000A5D RID: 2653
	public int Selected = 1;

	// Token: 0x04000A5E RID: 2654
	public Transform Highlight;

	// Token: 0x04000A5F RID: 2655
	public GameObject CustomMusicMenu;
}
