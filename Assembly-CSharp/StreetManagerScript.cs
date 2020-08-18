using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003FA RID: 1018
public class StreetManagerScript : MonoBehaviour
{
	// Token: 0x06001AF5 RID: 6901 RVA: 0x0010F1B4 File Offset: 0x0010D3B4
	private void Start()
	{
		this.MaidAnimation["f02_faceCouncilGrace_00"].layer = 1;
		this.MaidAnimation.Play("f02_faceCouncilGrace_00");
		this.MaidAnimation["f02_faceCouncilGrace_00"].weight = 1f;
		this.Gossip1["f02_socialSit_00"].layer = 1;
		this.Gossip1.Play("f02_socialSit_00");
		this.Gossip1["f02_socialSit_00"].weight = 1f;
		this.Gossip2["f02_socialSit_00"].layer = 1;
		this.Gossip2.Play("f02_socialSit_00");
		this.Gossip2["f02_socialSit_00"].weight = 1f;
		for (int i = 2; i < 5; i++)
		{
			this.Civilian[i]["f02_smile_00"].layer = 1;
			this.Civilian[i].Play("f02_smile_00");
			this.Civilian[i]["f02_smile_00"].weight = 1f;
		}
		this.Darkness.color = new Color(1f, 1f, 1f, 1f);
		this.CurrentlyActiveJukebox = this.JukeboxNight;
		this.Alpha = 1f;
		if (StudentGlobals.GetStudentDead(30) || StudentGlobals.GetStudentKidnapped(30) || StudentGlobals.GetStudentBroken(81))
		{
			this.Couple.SetActive(false);
		}
		this.Sunlight.shadows = LightShadows.None;
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x0010F348 File Offset: 0x0010D548
	private void Update()
	{
		if (Input.GetKeyDown("m"))
		{
			PlayerGlobals.Money += 1f;
			this.Clock.UpdateMoneyLabel();
			if (this.JukeboxNight.isPlaying)
			{
				this.JukeboxNight.Stop();
				this.JukeboxDay.Stop();
			}
			else
			{
				this.JukeboxNight.Play();
				this.JukeboxDay.Stop();
			}
		}
		if (Input.GetKeyDown("f"))
		{
			PlayerGlobals.FakeID = !PlayerGlobals.FakeID;
			this.StreetShopInterface.UpdateFakeID();
		}
		this.Timer += Time.deltaTime;
		if (this.Timer > 0.5f)
		{
			if (this.Alpha == 1f)
			{
				this.JukeboxNight.volume = 0.5f;
				this.JukeboxNight.Play();
				this.JukeboxDay.volume = 0f;
				this.JukeboxDay.Play();
			}
			if (!this.FadeOut)
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
				this.Darkness.color = new Color(1f, 1f, 1f, this.Alpha);
			}
			else
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
				this.CurrentlyActiveJukebox.volume = (1f - this.Alpha) * 0.5f;
				if (this.GoToCafe)
				{
					this.Darkness.color = new Color(1f, 1f, 1f, this.Alpha);
					if (this.Alpha == 1f)
					{
						SceneManager.LoadScene("MaidMenuScene");
					}
				}
				else
				{
					this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
					if (this.Alpha == 1f)
					{
						SceneManager.LoadScene("HomeScene");
					}
				}
			}
		}
		if (!this.FadeOut && !this.BinocularCamera.gameObject.activeInHierarchy)
		{
			if (Vector3.Distance(this.Yandere.position, this.Yakuza.transform.position) > 5f)
			{
				this.DesiredValue = 0.5f;
			}
			else
			{
				this.DesiredValue = Vector3.Distance(this.Yandere.position, this.Yakuza.transform.position) * 0.1f;
			}
			if (this.Day)
			{
				this.JukeboxDay.volume = Mathf.Lerp(this.JukeboxDay.volume, this.DesiredValue, Time.deltaTime * 10f);
				this.JukeboxNight.volume = Mathf.Lerp(this.JukeboxNight.volume, 0f, Time.deltaTime * 10f);
			}
			else
			{
				this.JukeboxDay.volume = Mathf.Lerp(this.JukeboxDay.volume, 0f, Time.deltaTime * 10f);
				this.JukeboxNight.volume = Mathf.Lerp(this.JukeboxNight.volume, this.DesiredValue, Time.deltaTime * 10f);
			}
			if (Vector3.Distance(this.Yandere.position, this.Yakuza.transform.position) < 1f && !this.Threatened)
			{
				this.Threatened = true;
				this.Yakuza.Play();
			}
		}
		if (Input.GetKeyDown("space"))
		{
			this.Day = !this.Day;
			if (this.Day)
			{
				this.Clock.HourLabel.text = "12:00 PM";
				this.Sunlight.shadows = LightShadows.Soft;
			}
			else
			{
				this.Clock.HourLabel.text = "8:00 PM";
				this.Sunlight.shadows = LightShadows.None;
			}
		}
		if (this.Day)
		{
			this.CurrentlyActiveJukebox = this.JukeboxDay;
			this.Rotation = Mathf.Lerp(this.Rotation, 45f, Time.deltaTime * 10f);
			this.StarAlpha = Mathf.Lerp(this.StarAlpha, 0f, Time.deltaTime * 10f);
		}
		else
		{
			this.CurrentlyActiveJukebox = this.JukeboxNight;
			this.Rotation = Mathf.Lerp(this.Rotation, -45f, Time.deltaTime * 10f);
			this.StarAlpha = Mathf.Lerp(this.StarAlpha, 1f, Time.deltaTime * 10f);
		}
		this.Sun.transform.eulerAngles = new Vector3(this.Rotation, this.Rotation, 0f);
		this.Stars.material.SetColor("_TintColor", new Color(1f, 1f, 1f, this.StarAlpha));
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x0010F822 File Offset: 0x0010DA22
	private void LateUpdate()
	{
		this.Hips.LookAt(this.BinocularCamera.position);
	}

	// Token: 0x04002BBA RID: 11194
	public StreetShopInterfaceScript StreetShopInterface;

	// Token: 0x04002BBB RID: 11195
	public Transform BinocularCamera;

	// Token: 0x04002BBC RID: 11196
	public Transform Yandere;

	// Token: 0x04002BBD RID: 11197
	public Transform Hips;

	// Token: 0x04002BBE RID: 11198
	public Transform Sun;

	// Token: 0x04002BBF RID: 11199
	public Animation MaidAnimation;

	// Token: 0x04002BC0 RID: 11200
	public Animation Gossip1;

	// Token: 0x04002BC1 RID: 11201
	public Animation Gossip2;

	// Token: 0x04002BC2 RID: 11202
	public AudioSource CurrentlyActiveJukebox;

	// Token: 0x04002BC3 RID: 11203
	public AudioSource JukeboxNight;

	// Token: 0x04002BC4 RID: 11204
	public AudioSource JukeboxDay;

	// Token: 0x04002BC5 RID: 11205
	public AudioSource Yakuza;

	// Token: 0x04002BC6 RID: 11206
	public HomeClockScript Clock;

	// Token: 0x04002BC7 RID: 11207
	public Animation[] Civilian;

	// Token: 0x04002BC8 RID: 11208
	public GameObject Couple;

	// Token: 0x04002BC9 RID: 11209
	public UISprite Darkness;

	// Token: 0x04002BCA RID: 11210
	public Renderer Stars;

	// Token: 0x04002BCB RID: 11211
	public Light Sunlight;

	// Token: 0x04002BCC RID: 11212
	public bool Threatened;

	// Token: 0x04002BCD RID: 11213
	public bool GoToCafe;

	// Token: 0x04002BCE RID: 11214
	public bool FadeOut;

	// Token: 0x04002BCF RID: 11215
	public bool Day;

	// Token: 0x04002BD0 RID: 11216
	public float Rotation;

	// Token: 0x04002BD1 RID: 11217
	public float Timer;

	// Token: 0x04002BD2 RID: 11218
	public float DesiredValue;

	// Token: 0x04002BD3 RID: 11219
	public float StarAlpha;

	// Token: 0x04002BD4 RID: 11220
	public float Alpha;
}
