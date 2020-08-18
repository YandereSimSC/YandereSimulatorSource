using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200035A RID: 858
public class PhoneScript : MonoBehaviour
{
	// Token: 0x060018B3 RID: 6323 RVA: 0x000E394C File Offset: 0x000E1B4C
	private void Start()
	{
		this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, -135f, this.Buttons.localPosition.z);
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		if (DateGlobals.Week > 1 && DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			this.Darkness.color = new Color(0f, 0f, 0f, 0f);
		}
		if (EventGlobals.KidnapConversation)
		{
			this.VoiceClips = this.KidnapClip;
			this.Speaker = this.KidnapSpeaker;
			this.Text = this.KidnapText;
			this.Height = this.KidnapHeight;
			EventGlobals.BefriendConversation = true;
			EventGlobals.KidnapConversation = false;
		}
		else if (EventGlobals.BefriendConversation)
		{
			this.VoiceClips = this.BefriendClip;
			this.Speaker = this.BefriendSpeaker;
			this.Text = this.BefriendText;
			this.Height = this.BefriendHeight;
			EventGlobals.LivingRoom = true;
			EventGlobals.BefriendConversation = false;
		}
		if (GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = Color.black;
			this.LoveSickColorSwap();
		}
		if (this.PostElimination && GameGlobals.NonlethalElimination)
		{
			this.VoiceClips[1] = this.NonlethalClip[1];
			this.VoiceClips[2] = this.NonlethalClip[2];
			this.VoiceClips[3] = this.NonlethalClip[3];
			this.Text[1] = this.NonlethalText[1];
			this.Text[2] = this.NonlethalText[2];
			this.Text[3] = this.NonlethalText[3];
			this.Height[1] = this.NonlethalHeight[1];
			this.Height[2] = this.NonlethalHeight[2];
			this.Height[3] = this.NonlethalHeight[3];
		}
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x000E3B50 File Offset: 0x000E1D50
	private void Update()
	{
		if (!this.FadeOut)
		{
			if (this.Timer > 0f && this.Buttons.gameObject.activeInHierarchy)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
				if (this.Darkness.color.a == 0f)
				{
					if (!this.Jukebox.isPlaying)
					{
						this.Jukebox.Play();
					}
					if (this.NewMessage == null)
					{
						this.SpawnMessage();
					}
				}
			}
			if (this.NewMessage != null)
			{
				this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, Mathf.Lerp(this.Buttons.localPosition.y, 0f, Time.deltaTime * 10f), this.Buttons.localPosition.z);
				this.AutoTimer += Time.deltaTime;
				if ((this.Auto && this.AutoTimer > this.VoiceClips[this.ID].length + 1f) || Input.GetButtonDown("A"))
				{
					this.AutoTimer = 0f;
					if (this.ID < this.Text.Length - 1)
					{
						this.ID++;
						this.SpawnMessage();
					}
					else
					{
						this.Darkness.color = new Color(0f, 0f, 0f, 0f);
						this.FadeOut = true;
						if (!this.Buttons.gameObject.activeInHierarchy)
						{
							this.Darkness.color = new Color(0f, 0f, 0f, 1f);
						}
					}
				}
				if (Input.GetButtonDown("X"))
				{
					this.FadeOut = true;
				}
			}
		}
		else
		{
			this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, Mathf.Lerp(this.Buttons.localPosition.y, -135f, Time.deltaTime * 10f), this.Buttons.localPosition.z);
			base.GetComponent<AudioSource>().volume = 1f - this.Darkness.color.a;
			this.Jukebox.volume = 1f - this.Darkness.color.a;
			if (this.Darkness.color.a >= 1f)
			{
				if (DateGlobals.Week == 2)
				{
					SceneManager.LoadScene("CreditsScene");
				}
				else if (DateGlobals.Weekday == DayOfWeek.Sunday)
				{
					SceneManager.LoadScene("OsanaWarningScene");
				}
				else if (!EventGlobals.BefriendConversation && !EventGlobals.LivingRoom)
				{
					SceneManager.LoadScene("CalendarScene");
				}
				else if (EventGlobals.LivingRoom)
				{
					SceneManager.LoadScene("LivingRoomScene");
				}
				else
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
		}
		this.Timer += Time.deltaTime;
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x000E3F0C File Offset: 0x000E210C
	private void SpawnMessage()
	{
		if (this.NewMessage != null)
		{
			this.NewMessage.transform.parent = this.OldMessages;
			this.OldMessages.localPosition = new Vector3(this.OldMessages.localPosition.x, this.OldMessages.localPosition.y + (72f + (float)this.Height[this.ID] * 32f), this.OldMessages.localPosition.z);
		}
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.VoiceClips[this.ID];
		component.Play();
		if (this.Speaker[this.ID] == 1)
		{
			this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.LeftMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(-225f, -375f, 0f);
			this.NewMessage.transform.localScale = Vector3.zero;
		}
		else
		{
			this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.RightMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(225f, -375f, 0f);
			this.NewMessage.transform.localScale = Vector3.zero;
			if (this.Speaker == this.KidnapSpeaker && this.Height[this.ID] == 8)
			{
				this.NewMessage.GetComponent<TextMessageScript>().Attachment = true;
			}
		}
		if (this.Height[this.ID] == 9 && this.Speaker[this.ID] == 2)
		{
			this.Buttons.gameObject.SetActive(false);
			this.Darkness.enabled = true;
			this.Jukebox.Stop();
			this.Timer = -99999f;
		}
		this.AutoLimit = (float)(this.Height[this.ID] + 1);
		this.NewMessage.GetComponent<TextMessageScript>().Label.text = this.Text[this.ID];
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x000E4164 File Offset: 0x000E2364
	private void LoveSickColorSwap()
	{
		foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
		{
			UISprite component = gameObject.GetComponent<UISprite>();
			if (component != null && component.color != Color.black && component.transform.parent)
			{
				component.color = new Color(1f, 0f, 0f, component.color.a);
			}
			UILabel component2 = gameObject.GetComponent<UILabel>();
			if (component2 != null && component2.color != Color.black)
			{
				component2.color = new Color(1f, 0f, 0f, component2.color.a);
			}
			this.Darkness.color = Color.black;
		}
	}

	// Token: 0x040024AB RID: 9387
	public GameObject[] RightMessage;

	// Token: 0x040024AC RID: 9388
	public GameObject[] LeftMessage;

	// Token: 0x040024AD RID: 9389
	public AudioClip[] VoiceClips;

	// Token: 0x040024AE RID: 9390
	public GameObject NewMessage;

	// Token: 0x040024AF RID: 9391
	public AudioSource Jukebox;

	// Token: 0x040024B0 RID: 9392
	public Transform OldMessages;

	// Token: 0x040024B1 RID: 9393
	public Transform Buttons;

	// Token: 0x040024B2 RID: 9394
	public Transform Panel;

	// Token: 0x040024B3 RID: 9395
	public Vignetting Vignette;

	// Token: 0x040024B4 RID: 9396
	public UISprite Darkness;

	// Token: 0x040024B5 RID: 9397
	public UISprite Sprite;

	// Token: 0x040024B6 RID: 9398
	public int[] Speaker;

	// Token: 0x040024B7 RID: 9399
	public string[] Text;

	// Token: 0x040024B8 RID: 9400
	public int[] Height;

	// Token: 0x040024B9 RID: 9401
	public AudioClip[] KidnapClip;

	// Token: 0x040024BA RID: 9402
	public int[] KidnapSpeaker;

	// Token: 0x040024BB RID: 9403
	public string[] KidnapText;

	// Token: 0x040024BC RID: 9404
	public int[] KidnapHeight;

	// Token: 0x040024BD RID: 9405
	public AudioClip[] BefriendClip;

	// Token: 0x040024BE RID: 9406
	public int[] BefriendSpeaker;

	// Token: 0x040024BF RID: 9407
	public string[] BefriendText;

	// Token: 0x040024C0 RID: 9408
	public int[] BefriendHeight;

	// Token: 0x040024C1 RID: 9409
	public AudioClip[] NonlethalClip;

	// Token: 0x040024C2 RID: 9410
	public string[] NonlethalText;

	// Token: 0x040024C3 RID: 9411
	public int[] NonlethalHeight;

	// Token: 0x040024C4 RID: 9412
	public bool PostElimination;

	// Token: 0x040024C5 RID: 9413
	public bool FadeOut;

	// Token: 0x040024C6 RID: 9414
	public bool Auto;

	// Token: 0x040024C7 RID: 9415
	public float AutoLimit;

	// Token: 0x040024C8 RID: 9416
	public float AutoTimer;

	// Token: 0x040024C9 RID: 9417
	public float Timer;

	// Token: 0x040024CA RID: 9418
	public int ID;
}
