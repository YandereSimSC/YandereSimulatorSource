using System;
using UnityEngine;

// Token: 0x020002E8 RID: 744
public class HomeCorkboardScript : MonoBehaviour
{
	// Token: 0x0600170D RID: 5901 RVA: 0x000C2B80 File Offset: 0x000C0D80
	private void Update()
	{
		if (!this.HomeYandere.CanMove)
		{
			if (!this.Loaded)
			{
				this.PhotoGallery.LoadingScreen.SetActive(false);
				this.PhotoGallery.UpdateButtonPrompts();
				this.PhotoGallery.enabled = true;
				this.PhotoGallery.gameObject.SetActive(true);
				this.Loaded = true;
			}
			if (!this.PhotoGallery.Adjusting && !this.PhotoGallery.Viewing && !this.PhotoGallery.LoadingScreen.activeInHierarchy && Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeCamera.CorkboardLabel.SetActive(true);
				this.PhotoGallery.PromptBar.Show = false;
				this.PhotoGallery.enabled = false;
				this.HomeYandere.CanMove = true;
				this.HomeYandere.gameObject.SetActive(true);
				this.HomeWindow.Show = false;
				base.enabled = false;
				this.Loaded = false;
				this.PhotoGallery.SaveAllPhotographs();
				this.PhotoGallery.SaveAllStrings();
			}
		}
	}

	// Token: 0x04001F2E RID: 7982
	public InputManagerScript InputManager;

	// Token: 0x04001F2F RID: 7983
	public PhotoGalleryScript PhotoGallery;

	// Token: 0x04001F30 RID: 7984
	public HomeYandereScript HomeYandere;

	// Token: 0x04001F31 RID: 7985
	public HomeCameraScript HomeCamera;

	// Token: 0x04001F32 RID: 7986
	public HomeWindowScript HomeWindow;

	// Token: 0x04001F33 RID: 7987
	public bool Loaded;
}
