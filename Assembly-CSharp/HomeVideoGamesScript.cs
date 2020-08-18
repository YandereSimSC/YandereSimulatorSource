using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002F8 RID: 760
public class HomeVideoGamesScript : MonoBehaviour
{
	// Token: 0x0600174C RID: 5964 RVA: 0x000C86AC File Offset: 0x000C68AC
	private void Start()
	{
		if (TaskGlobals.GetTaskStatus(38) == 0)
		{
			this.TitleScreens[1] = this.TitleScreens[5];
			UILabel uilabel = this.GameTitles[1];
			uilabel.text = this.GameTitles[5].text;
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
		}
		this.TitleScreen.mainTexture = this.TitleScreens[1];
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x000C8734 File Offset: 0x000C6934
	private void Update()
	{
		if (this.HomeCamera.Destination == this.HomeCamera.Destinations[5])
		{
			if (Input.GetKeyDown("y"))
			{
				TaskGlobals.SetTaskStatus(38, 1);
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			this.TV.localScale = Vector3.Lerp(this.TV.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (!this.HomeYandere.CanMove)
			{
				if (this.HomeDarkness.FadeOut)
				{
					Transform transform = this.HomeCamera.Destinations[5];
					Transform transform2 = this.HomeCamera.Targets[5];
					transform.position = new Vector3(Mathf.Lerp(transform.position.x, transform2.position.x, Time.deltaTime * 0.75f), Mathf.Lerp(transform.position.y, transform2.position.y, Time.deltaTime * 10f), Mathf.Lerp(transform.position.z, transform2.position.z, Time.deltaTime * 10f));
					return;
				}
				if (this.InputManager.TappedDown)
				{
					this.ID++;
					if (this.ID > 5)
					{
						this.ID = 1;
					}
					this.TitleScreen.mainTexture = this.TitleScreens[this.ID];
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 150f - (float)this.ID * 50f, this.Highlight.localPosition.z);
				}
				if (this.InputManager.TappedUp)
				{
					this.ID--;
					if (this.ID < 1)
					{
						this.ID = 5;
					}
					this.TitleScreen.mainTexture = this.TitleScreens[this.ID];
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 150f - (float)this.ID * 50f, this.Highlight.localPosition.z);
				}
				if (Input.GetButtonDown("A") && this.GameTitles[this.ID].color.a == 1f)
				{
					Transform transform3 = this.HomeCamera.Targets[5];
					transform3.localPosition = new Vector3(transform3.localPosition.x, 1.153333f, transform3.localPosition.z);
					this.HomeDarkness.Sprite.color = new Color(this.HomeDarkness.Sprite.color.r, this.HomeDarkness.Sprite.color.g, this.HomeDarkness.Sprite.color.b, -1f);
					this.HomeDarkness.FadeOut = true;
					this.HomeWindow.Show = false;
					this.PromptBar.Show = false;
					this.HomeCamera.ID = 5;
				}
				if (Input.GetButtonDown("B"))
				{
					this.Quit();
					return;
				}
			}
		}
		else
		{
			this.TV.localScale = Vector3.Lerp(this.TV.localScale, Vector3.zero, Time.deltaTime * 10f);
		}
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000C8AB8 File Offset: 0x000C6CB8
	public void Quit()
	{
		this.Controller.transform.localPosition = new Vector3(0.20385f, 0.0595f, 0.0215f);
		this.Controller.transform.localEulerAngles = new Vector3(-90f, -90f, 0f);
		this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
		this.HomeCamera.Target = this.HomeCamera.Targets[0];
		this.HomeYandere.CanMove = true;
		this.HomeYandere.enabled = true;
		this.HomeWindow.Show = false;
		this.HomeCamera.PlayMusic();
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
	}

	// Token: 0x0400203A RID: 8250
	public InputManagerScript InputManager;

	// Token: 0x0400203B RID: 8251
	public HomeDarknessScript HomeDarkness;

	// Token: 0x0400203C RID: 8252
	public HomeYandereScript HomeYandere;

	// Token: 0x0400203D RID: 8253
	public HomeCameraScript HomeCamera;

	// Token: 0x0400203E RID: 8254
	public HomeWindowScript HomeWindow;

	// Token: 0x0400203F RID: 8255
	public PromptBarScript PromptBar;

	// Token: 0x04002040 RID: 8256
	public Texture[] TitleScreens;

	// Token: 0x04002041 RID: 8257
	public UITexture TitleScreen;

	// Token: 0x04002042 RID: 8258
	public GameObject Controller;

	// Token: 0x04002043 RID: 8259
	public Transform Highlight;

	// Token: 0x04002044 RID: 8260
	public UILabel[] GameTitles;

	// Token: 0x04002045 RID: 8261
	public Transform TV;

	// Token: 0x04002046 RID: 8262
	public int ID = 1;
}
