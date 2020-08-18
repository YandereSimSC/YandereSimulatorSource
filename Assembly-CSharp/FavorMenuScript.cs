using System;
using UnityEngine;

// Token: 0x020002A0 RID: 672
public class FavorMenuScript : MonoBehaviour
{
	// Token: 0x06001404 RID: 5124 RVA: 0x000AF068 File Offset: 0x000AD268
	private void Update()
	{
		if (this.InputManager.TappedRight)
		{
			this.ID++;
			this.UpdateHighlight();
		}
		else if (this.InputManager.TappedLeft)
		{
			this.ID--;
			this.UpdateHighlight();
		}
		if (!this.TutorialWindow.Hide && !this.TutorialWindow.Show)
		{
			if (Input.GetButtonDown("A"))
			{
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				if (this.ID != 1)
				{
					if (this.ID == 2)
					{
						this.ServicesMenu.UpdatePantyCount();
						this.ServicesMenu.UpdateList();
						this.ServicesMenu.UpdateDesc();
						this.ServicesMenu.gameObject.SetActive(true);
						base.gameObject.SetActive(false);
					}
					else if (this.ID == 3)
					{
						this.DropsMenu.UpdatePantyCount();
						this.DropsMenu.UpdateList();
						this.DropsMenu.UpdateDesc();
						this.DropsMenu.gameObject.SetActive(true);
						base.gameObject.SetActive(false);
					}
				}
			}
			if (Input.GetButtonDown("X"))
			{
				TutorialGlobals.IgnoreClothing = true;
				this.TutorialWindow.IgnoreClothing = true;
				this.TutorialWindow.TitleLabel.text = "Info Points";
				this.TutorialWindow.TutorialLabel.text = this.TutorialWindow.PointsString;
				this.TutorialWindow.TutorialLabel.text = this.TutorialWindow.TutorialLabel.text.Replace('@', '\n');
				this.TutorialWindow.TutorialImage.mainTexture = this.TutorialWindow.InfoTexture;
				this.TutorialWindow.enabled = true;
				this.TutorialWindow.SummonWindow();
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
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x000AF32C File Offset: 0x000AD52C
	private void UpdateHighlight()
	{
		if (this.ID > 3)
		{
			this.ID = 1;
		}
		else if (this.ID < 1)
		{
			this.ID = 3;
		}
		this.Highlight.transform.localPosition = new Vector3(-500f + 250f * (float)this.ID, this.Highlight.transform.localPosition.y, this.Highlight.transform.localPosition.z);
	}

	// Token: 0x04001C21 RID: 7201
	public TutorialWindowScript TutorialWindow;

	// Token: 0x04001C22 RID: 7202
	public InputManagerScript InputManager;

	// Token: 0x04001C23 RID: 7203
	public PauseScreenScript PauseScreen;

	// Token: 0x04001C24 RID: 7204
	public ServicesScript ServicesMenu;

	// Token: 0x04001C25 RID: 7205
	public SchemesScript SchemesMenu;

	// Token: 0x04001C26 RID: 7206
	public DropsScript DropsMenu;

	// Token: 0x04001C27 RID: 7207
	public PromptBarScript PromptBar;

	// Token: 0x04001C28 RID: 7208
	public Transform Highlight;

	// Token: 0x04001C29 RID: 7209
	public int ID = 1;
}
