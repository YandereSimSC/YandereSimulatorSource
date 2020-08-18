using System;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class DemonPortalScript : MonoBehaviour
{
	// Token: 0x0600132D RID: 4909 RVA: 0x0009FBE0 File Offset: 0x0009DDE0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMove = false;
			UnityEngine.Object.Instantiate<GameObject>(this.DarkAura, this.Yandere.transform.position + Vector3.up * 0.81f, Quaternion.identity);
			this.Timer += Time.deltaTime;
		}
		this.DemonRealmAudio.volume = Mathf.MoveTowards(this.DemonRealmAudio.volume, (this.Yandere.transform.position.y > 1000f) ? 0.5f : 0f, Time.deltaTime * 0.1f);
		if (this.Timer > 0f)
		{
			if (this.Yandere.transform.position.y > 1000f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 4f)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
					if (this.Darkness.color.a == 1f)
					{
						this.Yandere.transform.position = new Vector3(12f, 0f, 28f);
						this.Yandere.Character.SetActive(true);
						this.Yandere.SetAnimationLayers();
						this.HeartbeatCamera.SetActive(true);
						this.FPS.SetActive(true);
						this.HUD.SetActive(true);
						return;
					}
				}
				else if (this.Timer > 1f)
				{
					this.Yandere.Character.SetActive(false);
					return;
				}
			}
			else
			{
				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0.5f, Time.deltaTime * 0.5f);
				if (this.Jukebox.Volume == 0.5f)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
					if (this.Darkness.color.a == 0f)
					{
						base.transform.parent.gameObject.SetActive(false);
						this.Darkness.enabled = false;
						this.Yandere.CanMove = true;
						this.Clock.StopTime = false;
						this.Timer = 0f;
					}
				}
			}
		}
	}

	// Token: 0x040019CE RID: 6606
	public YandereScript Yandere;

	// Token: 0x040019CF RID: 6607
	public JukeboxScript Jukebox;

	// Token: 0x040019D0 RID: 6608
	public PromptScript Prompt;

	// Token: 0x040019D1 RID: 6609
	public ClockScript Clock;

	// Token: 0x040019D2 RID: 6610
	public AudioSource DemonRealmAudio;

	// Token: 0x040019D3 RID: 6611
	public GameObject HeartbeatCamera;

	// Token: 0x040019D4 RID: 6612
	public GameObject DarkAura;

	// Token: 0x040019D5 RID: 6613
	public GameObject FPS;

	// Token: 0x040019D6 RID: 6614
	public GameObject HUD;

	// Token: 0x040019D7 RID: 6615
	public UISprite Darkness;

	// Token: 0x040019D8 RID: 6616
	public float Timer;
}
