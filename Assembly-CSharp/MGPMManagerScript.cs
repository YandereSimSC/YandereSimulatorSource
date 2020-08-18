using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200000F RID: 15
public class MGPMManagerScript : MonoBehaviour
{
	// Token: 0x06000082 RID: 130 RVA: 0x00005C1C File Offset: 0x00003E1C
	private void Start()
	{
		if (GameGlobals.HardMode)
		{
			this.Jukebox.clip = this.HardModeVoice;
			this.WaterRenderer[0].material.color = Color.red;
			this.WaterRenderer[1].material.color = Color.red;
			this.RightArtwork.material.mainTexture = this.RightBloody;
			this.LeftArtwork.material.mainTexture = this.LeftBloody;
		}
		this.Miyuki.transform.localPosition = new Vector3(0f, -300f, 0f);
		this.Black.material.color = new Color(0f, 0f, 0f, 1f);
		this.StartGraphic.SetActive(false);
		this.Miyuki.Gameplay = false;
		this.ID = 1;
		while (this.ID < this.EnemySpawner.Length)
		{
			this.EnemySpawner[this.ID].enabled = false;
			this.ID++;
		}
		Time.timeScale = 1f;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00005D44 File Offset: 0x00003F44
	private void Update()
	{
		this.ScoreLabel.text = "Score: " + this.Score * this.Miyuki.Health;
		if (this.StageClear)
		{
			this.GameOverTimer += Time.deltaTime;
			if (this.GameOverTimer > 1f)
			{
				this.Miyuki.transform.localPosition = new Vector3(this.Miyuki.transform.localPosition.x, this.Miyuki.transform.localPosition.y + Time.deltaTime * 10f, this.Miyuki.transform.localPosition.z);
				if (!this.StageClearGraphic.activeInHierarchy)
				{
					this.StageClearGraphic.SetActive(true);
					this.Jukebox.clip = this.VictoryMusic;
					this.Jukebox.loop = false;
					this.Jukebox.volume = 1f;
					this.Jukebox.Play();
				}
				if (this.GameOverTimer > 9f)
				{
					this.FadeOut = true;
				}
			}
			if (this.FadeOut)
			{
				this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 1f, Time.deltaTime));
				this.Jukebox.volume = 1f - this.Black.material.color.a;
				if (this.Black.material.color.a == 1f)
				{
					SceneManager.LoadScene("MiyukiThanksScene");
					return;
				}
			}
		}
		else if (!this.GameOver)
		{
			if (this.Intro)
			{
				if (this.FadeIn)
				{
					this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 0f, Time.deltaTime));
					if (this.Black.material.color.a == 0f)
					{
						this.Jukebox.Play();
						this.FadeIn = false;
					}
				}
				else
				{
					this.Miyuki.transform.localPosition = new Vector3(0f, Mathf.MoveTowards(this.Miyuki.transform.localPosition.y, -120f, Time.deltaTime * 60f), 0f);
					if (this.Miyuki.transform.localPosition.y == -120f)
					{
						if (!this.Jukebox.isPlaying)
						{
							this.Jukebox.loop = true;
							this.Jukebox.clip = this.BGM;
							this.Jukebox.Play();
							if (GameGlobals.HardMode)
							{
								this.Jukebox.pitch = 0.2f;
							}
						}
						this.StartGraphic.SetActive(true);
						this.Timer += Time.deltaTime;
						if ((double)this.Timer > 3.5)
						{
							this.StartGraphic.SetActive(false);
							this.ID = 1;
							while (this.ID < this.EnemySpawner.Length)
							{
								this.EnemySpawner[this.ID].enabled = true;
								this.ID++;
							}
							this.Miyuki.Gameplay = true;
							this.Intro = false;
						}
					}
				}
				if (Input.GetKeyDown("space"))
				{
					this.StartGraphic.SetActive(false);
					this.ID = 1;
					while (this.ID < this.EnemySpawner.Length)
					{
						this.EnemySpawner[this.ID].enabled = true;
						this.ID++;
					}
					this.Black.material.color = new Color(0f, 0f, 0f, 0f);
					this.Miyuki.Gameplay = true;
					this.Intro = false;
					this.Jukebox.loop = true;
					this.Jukebox.clip = this.BGM;
					this.Jukebox.Play();
					if (GameGlobals.HardMode)
					{
						this.Jukebox.pitch = 0.2f;
						return;
					}
				}
			}
		}
		else
		{
			this.GameOverTimer += Time.deltaTime;
			if (this.GameOverTimer > 3f)
			{
				if (!this.GameOverGraphic.activeInHierarchy)
				{
					this.GameOverGraphic.SetActive(true);
					this.Jukebox.clip = this.GameOverMusic;
					this.Jukebox.loop = false;
					this.Jukebox.Play();
				}
				else if (Input.anyKeyDown)
				{
					this.FadeOut = true;
				}
			}
			if (this.FadeOut)
			{
				this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 1f, Time.deltaTime));
				this.Jukebox.volume = 1f - this.Black.material.color.a;
				if (this.Black.material.color.a == 1f)
				{
					SceneManager.LoadScene("MiyukiTitleScene");
				}
			}
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000062CE File Offset: 0x000044CE
	public void BeginGameOver()
	{
		this.Jukebox.Stop();
		this.GameOver = true;
		this.Miyuki.enabled = false;
	}

	// Token: 0x040000C3 RID: 195
	public MGPMSpawnerScript[] EnemySpawner;

	// Token: 0x040000C4 RID: 196
	public MGPMMiyukiScript Miyuki;

	// Token: 0x040000C5 RID: 197
	public GameObject StageClearGraphic;

	// Token: 0x040000C6 RID: 198
	public GameObject GameOverGraphic;

	// Token: 0x040000C7 RID: 199
	public GameObject StartGraphic;

	// Token: 0x040000C8 RID: 200
	public Renderer[] WaterRenderer;

	// Token: 0x040000C9 RID: 201
	public Renderer RightArtwork;

	// Token: 0x040000CA RID: 202
	public Renderer LeftArtwork;

	// Token: 0x040000CB RID: 203
	public Texture RightBloody;

	// Token: 0x040000CC RID: 204
	public Texture LeftBloody;

	// Token: 0x040000CD RID: 205
	public AudioSource Jukebox;

	// Token: 0x040000CE RID: 206
	public AudioClip HardModeVoice;

	// Token: 0x040000CF RID: 207
	public AudioClip GameOverMusic;

	// Token: 0x040000D0 RID: 208
	public AudioClip VictoryMusic;

	// Token: 0x040000D1 RID: 209
	public AudioClip FinalBoss;

	// Token: 0x040000D2 RID: 210
	public AudioClip BGM;

	// Token: 0x040000D3 RID: 211
	public Renderer Black;

	// Token: 0x040000D4 RID: 212
	public Text ScoreLabel;

	// Token: 0x040000D5 RID: 213
	public bool StageClear;

	// Token: 0x040000D6 RID: 214
	public bool GameOver;

	// Token: 0x040000D7 RID: 215
	public bool FadeOut;

	// Token: 0x040000D8 RID: 216
	public bool FadeIn;

	// Token: 0x040000D9 RID: 217
	public bool Intro;

	// Token: 0x040000DA RID: 218
	public float GameOverTimer;

	// Token: 0x040000DB RID: 219
	public float Timer;

	// Token: 0x040000DC RID: 220
	public int Score;

	// Token: 0x040000DD RID: 221
	public int ID;
}
