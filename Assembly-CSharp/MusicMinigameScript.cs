﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000018 RID: 24
public class MusicMinigameScript : MonoBehaviour
{
	// Token: 0x0600009E RID: 158 RVA: 0x000080C8 File Offset: 0x000062C8
	private void Start()
	{
		Application.targetFrameRate = 60;
		Time.timeScale = 1f;
		this.Black.gameObject.SetActive(true);
		this.GameOverScreen.SetActive(false);
		this.Scales[0].localPosition = new Vector3(-1f, 0f, 0f);
		this.Scales[1].localPosition = new Vector3(0f, 0f, 0f);
		this.Scales[2].localPosition = new Vector3(1f, 0f, 0f);
		this.Scales[3].localPosition = new Vector3(2f, 0f, 0f);
		this.ID = 0;
		while (this.ID < this.Phase1Times.Length)
		{
			this.Times[this.ID] = this.Phase1Times[this.ID];
			this.Notes[this.ID] = this.Phase1Notes[this.ID];
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.Phase2Times.Length)
		{
			this.Times[this.ID + 216] = this.Phase2Times[this.ID];
			this.Notes[this.ID + 216] = this.Phase2Notes[this.ID];
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.Times.Length)
		{
			this.Times[this.ID] += 3f;
			this.ID++;
		}
		this.UpdateHealthBar();
		this.ReputationBar.localScale = new Vector3(0f, 0f, 0f);
		this.Black.material.color = new Color(0f, 0f, 0f, 1f);
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000082D8 File Offset: 0x000064D8
	private void Update()
	{
		this.ID = 0;
		while (this.ID < this.Scales.Length)
		{
			this.Scales[this.ID].localPosition -= new Vector3(Time.deltaTime * this.Speed, 0f, 0f);
			if (this.Scales[this.ID].localPosition.x < -2f)
			{
				this.Scales[this.ID].localPosition += new Vector3(4f, 0f, 0f);
			}
			this.ID++;
		}
		if (Input.GetKeyDown("escape"))
		{
			this.GameOver = true;
			this.Timer = 9f;
		}
		if (Input.GetKeyDown("l"))
		{
			this.LockHealth = !this.LockHealth;
		}
		if (this.GameOver)
		{
			this.MyAudio.pitch = Mathf.MoveTowards(this.MyAudio.pitch, 0f, Time.deltaTime * 0.33333f);
			this.Timer += Time.deltaTime;
			if (this.Timer > 4f)
			{
				if (!this.GameOverScreen.activeInHierarchy)
				{
					this.SadMiyuji.localPosition = new Vector3(-0.51f, -0.1f, -0.2f);
					this.SadAyano.localPosition = new Vector3(0.495f, -0.1f, -0.2f);
					this.GameOverScreen.SetActive(true);
				}
				this.SadMiyuji.localPosition = Vector3.Lerp(this.SadMiyuji.localPosition, new Vector3(-0.455f, -0.1f, -0.2f), Time.deltaTime);
				this.SadAyano.localPosition = Vector3.Lerp(this.SadAyano.localPosition, new Vector3(0.44f, -0.1f, -0.2f), Time.deltaTime);
				if (this.Timer > 9f)
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
					this.Black.material.color = new Color(0f, 0f, 0f, this.Alpha);
					if (this.Alpha == 1f)
					{
						this.Quit();
						return;
					}
				}
			}
		}
		else if (!this.Won)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
			this.Black.material.color = new Color(0f, 0f, 0f, this.Alpha);
			this.Timer += Time.deltaTime;
			if (!this.MyAudio.isPlaying)
			{
				if (this.Timer > 3f || Input.GetKeyDown("space"))
				{
					if (this.Timer < this.MyAudio.clip.length)
					{
						this.MyAudio.Play();
					}
					else
					{
						this.ChibiRenderer[1].material.mainTexture = this.ChibiCelebrate[1];
						this.ChibiRenderer[2].material.mainTexture = this.ChibiCelebrate[2];
						this.ChibiRenderer[3].material.mainTexture = this.ChibiCelebrate[3];
						this.ChibiRenderer[4].material.mainTexture = this.ChibiCelebrate[4];
						this.ChibiRenderer[5].material.mainTexture = this.ChibiCelebrate[5];
						this.ChibiRenderer[6].material.mainTexture = this.ChibiCelebrate[6];
						this.Jump[1] = this.JumpStrength;
						this.Jump[2] = this.JumpStrength * 0.9f;
						this.Jump[3] = this.JumpStrength * 0.8f;
						this.Jump[4] = this.JumpStrength * 0.7f;
						this.Jump[5] = this.JumpStrength * 0.6f;
						this.Jump[6] = this.JumpStrength * 0.5f;
						if (this.Health == 200f)
						{
							this.Excitement = 3;
						}
						else if (this.Health > 0f)
						{
							this.Excitement = 2;
						}
						else
						{
							this.Excitement = 1;
						}
						this.MyAudio.clip = this.Celebrations[this.Excitement];
						this.MyAudio.loop = false;
						this.MyAudio.Play();
						this.Won = true;
						this.Timer = 0f;
					}
				}
			}
			else
			{
				if (Input.GetKeyDown("space"))
				{
					this.MyAudio.time += 10f;
					this.Timer = this.MyAudio.time + 3f;
				}
				if (Input.GetKeyDown("z"))
				{
					this.MyAudio.time = this.MyAudio.clip.length - Time.deltaTime;
				}
				if (this.MyAudio.time > 131f)
				{
					this.ChibiSway[2] = false;
					this.ChibiSway[6] = false;
					this.ChibiSway[3] = false;
					this.ChibiSway[5] = false;
					this.ChibiSway[4] = false;
				}
				else if ((double)this.MyAudio.time > 88.2833333)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = true;
					this.ChibiSway[3] = true;
					this.ChibiSway[5] = true;
					this.ChibiSway[4] = true;
				}
				else if ((double)this.MyAudio.time > 74.25)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = true;
					this.ChibiSway[3] = true;
					this.ChibiSway[5] = false;
					this.ChibiSway[4] = false;
				}
				else if (this.MyAudio.time > 60f)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = true;
					this.ChibiSway[3] = false;
					this.ChibiSway[5] = false;
					this.ChibiSway[4] = false;
				}
				else if ((double)this.MyAudio.time > 45.933333)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = true;
					this.ChibiSway[3] = true;
					this.ChibiSway[5] = true;
					this.ChibiSway[4] = true;
				}
				else if ((double)this.MyAudio.time > 45.08)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = true;
					this.ChibiSway[3] = false;
					this.ChibiSway[5] = true;
					this.ChibiSway[4] = true;
				}
				else if ((double)this.MyAudio.time > 35.33333)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = false;
					this.ChibiSway[3] = false;
					this.ChibiSway[5] = true;
					this.ChibiSway[4] = true;
				}
				else if ((double)this.MyAudio.time > 31.833333)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = false;
					this.ChibiSway[3] = false;
					this.ChibiSway[5] = false;
					this.ChibiSway[4] = false;
				}
				else if ((double)this.MyAudio.time > 30.33333)
				{
					this.ChibiSway[2] = false;
					this.ChibiSway[6] = false;
					this.ChibiSway[3] = false;
					this.ChibiSway[5] = false;
					this.ChibiSway[4] = false;
				}
				else if ((double)this.MyAudio.time > 28.2833333)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = true;
					this.ChibiSway[3] = false;
					this.ChibiSway[5] = false;
					this.ChibiSway[4] = false;
				}
				else if ((double)this.MyAudio.time > 7.1166666)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = true;
					this.ChibiSway[3] = true;
					this.ChibiSway[5] = true;
					this.ChibiSway[4] = false;
				}
				else if ((double)this.MyAudio.time > 3.5833333)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = true;
					this.ChibiSway[3] = false;
					this.ChibiSway[5] = false;
					this.ChibiSway[4] = false;
				}
				else if (this.MyAudio.time > 0f)
				{
					this.ChibiSway[2] = true;
					this.ChibiSway[6] = false;
					this.ChibiSway[3] = false;
					this.ChibiSway[5] = false;
					this.ChibiSway[4] = false;
				}
				if (this.MyAudio.time > 33f && this.MyAudio.time < 36.833332f)
				{
					this.ChibiSway[1] = true;
				}
				else if (this.MyAudio.time > 39.5f && this.MyAudio.time < 43.25f)
				{
					this.ChibiSway[1] = true;
				}
				else if (this.MyAudio.time > 46.833332f && this.MyAudio.time < 49.75f)
				{
					this.ChibiSway[1] = true;
				}
				else if (this.MyAudio.time > 50.383335f && this.MyAudio.time < 53f)
				{
					this.ChibiSway[1] = true;
				}
				else if (this.MyAudio.time > 53.916668f && this.MyAudio.time < 59f)
				{
					this.ChibiSway[1] = true;
				}
				else if (this.MyAudio.time > 59.5f && this.MyAudio.time < 74.33333f)
				{
					this.ChibiSway[1] = true;
				}
				else if (this.MyAudio.time > 77f && this.MyAudio.time < 80.33333f)
				{
					this.ChibiSway[1] = true;
				}
				else if (this.MyAudio.time > 84.05f && this.MyAudio.time < 88.166664f)
				{
					this.ChibiSway[1] = true;
				}
				else if (this.MyAudio.time > 91f && this.MyAudio.time < 98.5f)
				{
					this.ChibiSway[1] = true;
				}
				else if (this.MyAudio.time > 101.833336f && this.MyAudio.time < 130.58333f)
				{
					this.ChibiSway[1] = true;
				}
				else
				{
					this.ChibiSway[1] = false;
				}
				if (this.CringeTimer == 0f)
				{
					this.MyAudio.volume = 1f;
				}
				this.ID = 1;
				while (this.ID < this.ChibiSway.Length)
				{
					if (this.CringeTimer > 0f)
					{
						this.ChibiRenderer[this.ID].transform.localPosition = new Vector3(UnityEngine.Random.Range(-0.01f, 0.01f), 0.15f + UnityEngine.Random.Range(-0.01f, 0.01f), 0f);
						this.CringeTimer = Mathf.MoveTowards(this.CringeTimer, 0f, Time.deltaTime);
						if (this.CringeTimer == 0f)
						{
							this.ChibiRenderer[this.ID].transform.localPosition = new Vector3(0f, 0.15f, 0f);
						}
					}
					else if (this.ChibiSway[this.ID])
					{
						if (!this.MusicNotes[this.ID].isPlaying)
						{
							this.MusicNotes[this.ID].Play();
						}
						this.AnimTimer[this.ID] += Time.deltaTime;
						if (this.AnimTimer[this.ID] > 0.2f)
						{
							this.FrameB[this.ID] = !this.FrameB[this.ID];
							this.AnimTimer[this.ID] = 0f;
						}
						if (this.FrameB[this.ID])
						{
							this.ChibiRenderer[this.ID].material.mainTexture = this.ChibiPerform[this.ID];
						}
						else
						{
							this.ChibiRenderer[this.ID].material.mainTexture = this.ChibiPerformB[this.ID];
						}
						if (this.ID < 6)
						{
							if (this.Ping[this.ID])
							{
								this.PingPong[this.ID] += Time.deltaTime * 5f;
								if (this.PingPong[this.ID] > 1f)
								{
									this.Ping[this.ID] = false;
								}
							}
							else
							{
								this.PingPong[this.ID] -= Time.deltaTime * 5f;
								if (this.PingPong[this.ID] < -1f)
								{
									this.Ping[this.ID] = true;
								}
							}
							this.Rotation[this.ID] += this.PingPong[this.ID] * Time.deltaTime * 10f;
							if (this.Rotation[this.ID] > 7.5f)
							{
								this.Rotation[this.ID] = 7.5f;
							}
							else if (this.Rotation[this.ID] < -7.5f)
							{
								this.Rotation[this.ID] = -7.5f;
							}
						}
					}
					else
					{
						if (this.ID < 6)
						{
							this.Rotation[this.ID] = Mathf.MoveTowards(this.Rotation[this.ID], 0f, Time.deltaTime * 100f);
						}
						if (this.ChibiRenderer[this.ID].material.mainTexture != this.ChibiIdle[this.ID])
						{
							this.ChibiRenderer[this.ID].material.mainTexture = this.ChibiIdle[this.ID];
							this.MusicNotes[this.ID].Stop();
							this.PingPong[this.ID] = -1f;
							this.Ping[this.ID] = false;
						}
					}
					this.Instruments[this.ID].localEulerAngles = new Vector3(0f, 0f, this.Rotation[this.ID]);
					this.ID++;
				}
			}
			if (this.SettingNotes)
			{
				if (Input.GetKeyDown("up"))
				{
					if (this.Phase == 1)
					{
						this.Phase1Times[this.Note] = this.MyAudio.time;
						this.Phase1Notes[this.Note] = 1;
					}
					else
					{
						this.Phase2Times[this.Note] = this.MyAudio.time;
						this.Phase2Notes[this.Note] = 1;
					}
					this.Note++;
					return;
				}
				if (Input.GetKeyDown("right"))
				{
					if (this.Phase == 1)
					{
						this.Phase1Times[this.Note] = this.MyAudio.time;
						this.Phase1Notes[this.Note] = 2;
					}
					else
					{
						this.Phase2Times[this.Note] = this.MyAudio.time;
						this.Phase2Notes[this.Note] = 2;
					}
					this.Note++;
					return;
				}
				if (Input.GetKeyDown("left"))
				{
					if (this.Phase == 1)
					{
						this.Phase1Times[this.Note] = this.MyAudio.time;
						this.Phase1Notes[this.Note] = 3;
					}
					else
					{
						this.Phase2Times[this.Note] = this.MyAudio.time;
						this.Phase2Notes[this.Note] = 3;
					}
					this.Note++;
					return;
				}
				if (Input.GetKeyDown("down"))
				{
					if (this.Phase == 1)
					{
						this.Phase1Times[this.Note] = this.MyAudio.time;
						this.Phase1Notes[this.Note] = 4;
					}
					else
					{
						this.Phase2Times[this.Note] = this.MyAudio.time;
						this.Phase2Notes[this.Note] = 4;
					}
					this.Note++;
					return;
				}
			}
			else
			{
				if (Input.GetKeyUp("up") || Input.GetKeyUp("right") || Input.GetKeyUp("down") || Input.GetKeyUp("left"))
				{
					this.KeyDown = false;
				}
				if (!this.InputManager.TappedUp && !this.InputManager.TappedDown && !this.InputManager.TappedLeft && !this.InputManager.TappedRight)
				{
					this.KeyDown = false;
				}
				if (this.Note < this.Notes.Length && this.Notes[this.Note] > 0 && this.Timer + 2f > this.Times[this.Note])
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.NoteIcons[this.Notes[this.Note]], base.transform.position, Quaternion.identity);
					gameObject.GetComponent<MusicNoteScript>().InputManager = this.InputManager;
					gameObject.GetComponent<MusicNoteScript>().MusicMinigame = this;
					gameObject.GetComponent<MusicNoteScript>().ID = this.Note;
					gameObject.transform.parent = this.Scales[0].parent;
					if (this.Notes[this.Note] == 1)
					{
						gameObject.transform.localPosition = new Vector3(1.5f, 0.15f, -0.0001f);
					}
					else if (this.Notes[this.Note] == 2)
					{
						gameObject.transform.localPosition = new Vector3(1.5f, 0.05f, -0.0001f);
					}
					else if (this.Notes[this.Note] == 3)
					{
						gameObject.transform.localPosition = new Vector3(1.5f, -0.05f, -0.0001f);
					}
					else if (this.Notes[this.Note] == 4)
					{
						gameObject.transform.localPosition = new Vector3(1.5f, -0.15f, -0.0001f);
					}
					gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
					this.Note++;
					return;
				}
			}
		}
		else
		{
			this.ID = 1;
			while (this.ID < this.Instruments.Length)
			{
				if (this.ID != 2 && this.ID != 6)
				{
					this.ChibiRenderer[this.ID].transform.localPosition += new Vector3(0f, this.Jump[this.ID], 0f);
					this.Jump[this.ID] -= Time.deltaTime * 0.01f;
					if (this.ChibiRenderer[this.ID].transform.localPosition.y < 0.15f)
					{
						this.ChibiRenderer[this.ID].transform.localPosition = new Vector3(0f, 0.15f, 0f);
						this.Jump[this.ID] = this.JumpStrength;
					}
				}
				this.ID++;
			}
			if (!this.MyAudio.isPlaying)
			{
				if (this.Timer == 0f)
				{
					this.StartRep = PlayerPrefs.GetFloat("TempReputation");
					this.CurrentRep.text = string.Concat(this.StartRep);
					if (this.Health > 100f)
					{
						this.RepBonus.text = "+" + (this.Health - 100f);
					}
					this.ReputationMarker.localPosition = new Vector3(this.StartRep * 0.01f, 0f, 0f);
				}
				this.ReputationBar.localScale = Vector3.Lerp(this.ReputationBar.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				this.Timer += Time.deltaTime;
				if (this.Timer > 1f && this.Health > 100f)
				{
					float num = this.StartRep + (this.Health - 100f);
					if (num > 100f)
					{
						num = 100f;
					}
					this.CurrentRep.text = string.Concat(num);
					this.Power += Time.deltaTime;
					this.ReputationMarker.localPosition = Vector3.Lerp(this.ReputationMarker.localPosition, new Vector3(num * 0.01f, 0f, -0.0002f), this.Power);
				}
				if (this.Timer > 5f)
				{
					this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
					this.Black.material.color = new Color(0f, 0f, 0f, this.Alpha);
					if (this.Alpha == 1f)
					{
						this.Quit();
					}
				}
			}
		}
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000988C File Offset: 0x00007A8C
	public void UpdateHealthBar()
	{
		if (this.Health > 200f)
		{
			this.Health = 200f;
		}
		if (this.Health <= 0f)
		{
			this.MyAudio.volume = 1f;
			this.GameOver = true;
			this.Health = 0f;
			this.Timer = 0f;
		}
		else
		{
			this.HealthBar.localScale = new Vector3(1f, this.Health / 200f, 1f);
			this.HealthBarRenderer.material.color = new Color(1f - this.Health / 200f, this.Health / 200f, 0f, 1f);
		}
		if (this.Health > 100f)
		{
			this.Stars[1].material.mainTexture = this.GoldStar;
		}
		else
		{
			this.Stars[1].material.mainTexture = this.EmptyStar;
		}
		if (this.Health > 125f)
		{
			this.Stars[2].material.mainTexture = this.GoldStar;
		}
		else
		{
			this.Stars[2].material.mainTexture = this.EmptyStar;
		}
		if (this.Health > 150f)
		{
			this.Stars[3].material.mainTexture = this.GoldStar;
		}
		else
		{
			this.Stars[3].material.mainTexture = this.EmptyStar;
		}
		if (this.Health > 175f)
		{
			this.Stars[4].material.mainTexture = this.GoldStar;
		}
		else
		{
			this.Stars[4].material.mainTexture = this.EmptyStar;
		}
		if (this.Health == 200f)
		{
			this.Stars[5].material.mainTexture = this.GoldStar;
			return;
		}
		this.Stars[5].material.mainTexture = this.EmptyStar;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00009A8C File Offset: 0x00007C8C
	public void Cringe()
	{
		this.ID = 1;
		while (this.ID < this.ChibiRenderer.Length)
		{
			this.ChibiRenderer[this.ID].material.mainTexture = this.ChibiCringe[this.ID];
			this.MusicNotes[this.ID].Stop();
			this.Rotation[this.ID] = 0f;
			this.ID++;
		}
		this.MyAudio.volume = 0f;
		this.CringeTimer = 1f;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00009B24 File Offset: 0x00007D24
	public void Quit()
	{
		if (this.Health > 100f)
		{
			PlayerPrefs.SetFloat("TempReputation", this.StartRep + (this.Health - 100f));
		}
		else
		{
			PlayerPrefs.SetFloat("TempReputation", 0f);
		}
		GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
		for (int i = 0; i < rootGameObjects.Length; i++)
		{
			rootGameObjects[i].SetActive(true);
		}
		SceneManager.UnloadSceneAsync(22);
	}

	// Token: 0x04000134 RID: 308
	public GameObject[] NoteIcons;

	// Token: 0x04000135 RID: 309
	public Transform[] Scales;

	// Token: 0x04000136 RID: 310
	public Renderer[] Stars;

	// Token: 0x04000137 RID: 311
	public InputManagerScript InputManager;

	// Token: 0x04000138 RID: 312
	public Renderer HealthBarRenderer;

	// Token: 0x04000139 RID: 313
	public Renderer Black;

	// Token: 0x0400013A RID: 314
	public Transform ReputationMarker;

	// Token: 0x0400013B RID: 315
	public Transform ReputationBar;

	// Token: 0x0400013C RID: 316
	public Transform HealthBar;

	// Token: 0x0400013D RID: 317
	public Transform SadMiyuji;

	// Token: 0x0400013E RID: 318
	public Transform SadAyano;

	// Token: 0x0400013F RID: 319
	public GameObject GameOverScreen;

	// Token: 0x04000140 RID: 320
	public AudioSource MyAudio;

	// Token: 0x04000141 RID: 321
	public UILabel CurrentRep;

	// Token: 0x04000142 RID: 322
	public UILabel RepBonus;

	// Token: 0x04000143 RID: 323
	public Texture EmptyStar;

	// Token: 0x04000144 RID: 324
	public Texture GoldStar;

	// Token: 0x04000145 RID: 325
	public float JumpStrength;

	// Token: 0x04000146 RID: 326
	public float CringeTimer;

	// Token: 0x04000147 RID: 327
	public float StartRep;

	// Token: 0x04000148 RID: 328
	public float Health;

	// Token: 0x04000149 RID: 329
	public float Alpha;

	// Token: 0x0400014A RID: 330
	public float Power;

	// Token: 0x0400014B RID: 331
	public float Speed;

	// Token: 0x0400014C RID: 332
	public float Timer;

	// Token: 0x0400014D RID: 333
	public float[] Phase1Times;

	// Token: 0x0400014E RID: 334
	public int[] Phase1Notes;

	// Token: 0x0400014F RID: 335
	public float[] Phase2Times;

	// Token: 0x04000150 RID: 336
	public int[] Phase2Notes;

	// Token: 0x04000151 RID: 337
	public float[] Times;

	// Token: 0x04000152 RID: 338
	public int[] Notes;

	// Token: 0x04000153 RID: 339
	public int CurrentNote;

	// Token: 0x04000154 RID: 340
	public int Excitement;

	// Token: 0x04000155 RID: 341
	public int Phase;

	// Token: 0x04000156 RID: 342
	public int Note;

	// Token: 0x04000157 RID: 343
	public int ID;

	// Token: 0x04000158 RID: 344
	public bool SettingNotes;

	// Token: 0x04000159 RID: 345
	public bool LockHealth;

	// Token: 0x0400015A RID: 346
	public bool GameOver;

	// Token: 0x0400015B RID: 347
	public bool KeyDown;

	// Token: 0x0400015C RID: 348
	public bool Won;

	// Token: 0x0400015D RID: 349
	public Texture[] ChibiCelebrate;

	// Token: 0x0400015E RID: 350
	public Texture[] ChibiPerform;

	// Token: 0x0400015F RID: 351
	public Texture[] ChibiPerformB;

	// Token: 0x04000160 RID: 352
	public Texture[] ChibiCringe;

	// Token: 0x04000161 RID: 353
	public Texture[] ChibiIdle;

	// Token: 0x04000162 RID: 354
	public ParticleSystem[] MusicNotes;

	// Token: 0x04000163 RID: 355
	public AudioClip[] Celebrations;

	// Token: 0x04000164 RID: 356
	public Renderer[] ChibiRenderer;

	// Token: 0x04000165 RID: 357
	public Transform[] Instruments;

	// Token: 0x04000166 RID: 358
	public float[] AnimTimer;

	// Token: 0x04000167 RID: 359
	public float[] PingPong;

	// Token: 0x04000168 RID: 360
	public float[] Rotation;

	// Token: 0x04000169 RID: 361
	public float[] Jump;

	// Token: 0x0400016A RID: 362
	public bool[] ChibiSway;

	// Token: 0x0400016B RID: 363
	public bool[] FrameB;

	// Token: 0x0400016C RID: 364
	public bool[] Ping;
}
