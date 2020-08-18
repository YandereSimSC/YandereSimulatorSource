using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200033A RID: 826
public class MusicMenuScript : MonoBehaviour
{
	// Token: 0x06001846 RID: 6214 RVA: 0x000DA000 File Offset: 0x000D8200
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.AudioMenu.SetActive(true);
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
		if (Input.GetButtonDown("A"))
		{
			base.StartCoroutine(this.DownloadCoroutine());
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x000DA12A File Offset: 0x000D832A
	private IEnumerator DownloadCoroutine()
	{
		WWW CurrentDownload = new WWW(string.Concat(new object[]
		{
			"File:///",
			Application.streamingAssetsPath,
			"/Music/track",
			this.Selected,
			".ogg"
		}));
		yield return CurrentDownload;
		this.CustomMusic = CurrentDownload.GetAudioClipCompressed();
		this.Jukebox.Custom.clip = this.CustomMusic;
		this.Jukebox.PlayCustom();
		yield break;
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x000DA13C File Offset: 0x000D833C
	private void UpdateHighlight()
	{
		if (this.Selected < 0)
		{
			this.Selected = this.SelectionLimit;
		}
		else if (this.Selected > this.SelectionLimit)
		{
			this.Selected = 0;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 365f - 80f * (float)this.Selected, this.Highlight.localPosition.z);
	}

	// Token: 0x04002344 RID: 9028
	public InputManagerScript InputManager;

	// Token: 0x04002345 RID: 9029
	public PauseScreenScript PauseScreen;

	// Token: 0x04002346 RID: 9030
	public PromptBarScript PromptBar;

	// Token: 0x04002347 RID: 9031
	public GameObject AudioMenu;

	// Token: 0x04002348 RID: 9032
	public JukeboxScript Jukebox;

	// Token: 0x04002349 RID: 9033
	public int SelectionLimit = 9;

	// Token: 0x0400234A RID: 9034
	public int Selected;

	// Token: 0x0400234B RID: 9035
	public Transform Highlight;

	// Token: 0x0400234C RID: 9036
	public string path = string.Empty;

	// Token: 0x0400234D RID: 9037
	public AudioClip CustomMusic;
}
