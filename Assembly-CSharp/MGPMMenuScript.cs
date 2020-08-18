using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000010 RID: 16
public class MGPMMenuScript : MonoBehaviour
{
	// Token: 0x06000086 RID: 134 RVA: 0x000062EE File Offset: 0x000044EE
	private void Start()
	{
		this.Black.material.color = new Color(0f, 0f, 0f, 1f);
		Time.timeScale = 1f;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00006324 File Offset: 0x00004524
	private void Update()
	{
		this.Rotation -= Time.deltaTime * 3f;
		this.Background.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		if (this.FadeIn)
		{
			this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 0f, Time.deltaTime));
			if (this.Black.material.color.a == 0f)
			{
				this.Jukebox.Play();
				this.FadeIn = false;
			}
		}
		if (this.FadeOut)
		{
			this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 1f, Time.deltaTime));
			this.Jukebox.volume = 1f - this.Black.material.color.a;
			if (this.Black.material.color.a == 1f)
			{
				if (this.ID == 4)
				{
					SceneManager.LoadScene("HomeScene");
				}
				else
				{
					GameGlobals.HardMode = this.HardMode;
					SceneManager.LoadScene("MiyukiGameplayScene");
				}
			}
		}
		if (!this.FadeOut && !this.FadeIn)
		{
			if (!this.HardMode && Input.GetKeyDown("h"))
			{
				AudioSource.PlayClipAtPoint(this.HardModeClip, base.transform.position);
				this.Logo.material.mainTexture = this.BloodyLogo;
				this.HardMode = true;
				this.Vibrate = 0.1f;
			}
			if (this.HardMode)
			{
				this.Jukebox.pitch = Mathf.MoveTowards(this.Jukebox.pitch, 0.1f, Time.deltaTime);
				this.BG.material.color = new Color(Mathf.MoveTowards(this.BG.material.color.r, 0.5f, Time.deltaTime * 0.5f), Mathf.MoveTowards(this.BG.material.color.g, 0f, Time.deltaTime), Mathf.MoveTowards(this.BG.material.color.b, 0f, Time.deltaTime), 1f);
				this.Logo.transform.localPosition = new Vector3(0f, 0.5f, 2f) + new Vector3(UnityEngine.Random.Range(this.Vibrate * -1f, this.Vibrate), UnityEngine.Random.Range(this.Vibrate * -1f, this.Vibrate), 0f);
				this.Vibrate = Mathf.MoveTowards(this.Vibrate, 0f, Time.deltaTime * 0.1f);
			}
			if (this.Jukebox.clip != this.BGM && !this.Jukebox.isPlaying)
			{
				this.Jukebox.loop = true;
				this.Jukebox.clip = this.BGM;
				this.Jukebox.Play();
			}
			if (!this.WindowDisplaying)
			{
				if (this.InputManager.TappedDown)
				{
					this.ID++;
					this.UpdateHighlight();
				}
				if (this.InputManager.TappedUp)
				{
					this.ID--;
					this.UpdateHighlight();
				}
				if (Input.GetButtonDown("A") || Input.GetKeyDown("z") || (Input.GetKeyDown("return") | Input.GetKeyDown("space")))
				{
					if (!this.MainMenu.activeInHierarchy)
					{
						if (this.ID == 2)
						{
							GameGlobals.EasyMode = false;
						}
						else
						{
							GameGlobals.EasyMode = true;
						}
						this.FadeOut = true;
						return;
					}
					if (this.ID == 1)
					{
						this.DifficultySelect.SetActive(true);
						this.MainMenu.SetActive(false);
						this.ID = 2;
						this.UpdateHighlight();
						return;
					}
					if (this.ID == 2)
					{
						this.Highlight.gameObject.SetActive(false);
						this.Controls.SetActive(true);
						this.WindowDisplaying = true;
						return;
					}
					if (this.ID == 3)
					{
						this.Highlight.gameObject.SetActive(false);
						this.Credits.SetActive(true);
						this.WindowDisplaying = true;
						return;
					}
					if (this.ID == 4)
					{
						this.FadeOut = true;
						return;
					}
				}
			}
			else if (Input.GetButtonDown("B"))
			{
				this.Highlight.gameObject.SetActive(true);
				this.Controls.SetActive(false);
				this.Credits.SetActive(false);
				this.WindowDisplaying = false;
			}
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00006820 File Offset: 0x00004A20
	private void UpdateHighlight()
	{
		if (this.MainMenu.activeInHierarchy)
		{
			if (this.ID == 0)
			{
				this.ID = 4;
			}
			else if (this.ID == 5)
			{
				this.ID = 1;
			}
		}
		else if (this.ID == 1)
		{
			this.ID = 3;
		}
		else if (this.ID == 4)
		{
			this.ID = 2;
		}
		this.Highlight.transform.position = new Vector3(0f, -0.2f * (float)this.ID, this.Highlight.transform.position.z);
	}

	// Token: 0x040000DE RID: 222
	public InputManagerScript InputManager;

	// Token: 0x040000DF RID: 223
	public AudioSource Jukebox;

	// Token: 0x040000E0 RID: 224
	public AudioClip HardModeClip;

	// Token: 0x040000E1 RID: 225
	public bool WindowDisplaying;

	// Token: 0x040000E2 RID: 226
	public Transform Highlight;

	// Token: 0x040000E3 RID: 227
	public Transform Background;

	// Token: 0x040000E4 RID: 228
	public GameObject Controls;

	// Token: 0x040000E5 RID: 229
	public GameObject Credits;

	// Token: 0x040000E6 RID: 230
	public GameObject DifficultySelect;

	// Token: 0x040000E7 RID: 231
	public GameObject MainMenu;

	// Token: 0x040000E8 RID: 232
	public Renderer Black;

	// Token: 0x040000E9 RID: 233
	public Renderer Logo;

	// Token: 0x040000EA RID: 234
	public Renderer BG;

	// Token: 0x040000EB RID: 235
	public Texture BloodyLogo;

	// Token: 0x040000EC RID: 236
	public AudioClip BGM;

	// Token: 0x040000ED RID: 237
	public float Rotation;

	// Token: 0x040000EE RID: 238
	public float Vibrate;

	// Token: 0x040000EF RID: 239
	public bool HardMode;

	// Token: 0x040000F0 RID: 240
	public bool FadeOut;

	// Token: 0x040000F1 RID: 241
	public bool FadeIn;

	// Token: 0x040000F2 RID: 242
	public int ID;
}
