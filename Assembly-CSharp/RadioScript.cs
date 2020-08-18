using System;
using UnityEngine;

// Token: 0x02000382 RID: 898
public class RadioScript : MonoBehaviour
{
	// Token: 0x06001970 RID: 6512 RVA: 0x000F61A0 File Offset: 0x000F43A0
	private void Update()
	{
		if (base.transform.parent == null)
		{
			if (this.CooldownTimer > 0f)
			{
				this.CooldownTimer = Mathf.MoveTowards(this.CooldownTimer, 0f, Time.deltaTime);
				if (this.CooldownTimer == 0f)
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				UISprite uisprite = this.Prompt.Circle[0];
				if (uisprite.fillAmount == 0f)
				{
					uisprite.fillAmount = 1f;
					if (!this.On)
					{
						this.Prompt.Label[0].text = "     Turn Off";
						this.MyRenderer.material.mainTexture = this.OnTexture;
						base.GetComponent<AudioSource>().Play();
						this.RadioNotes.SetActive(true);
						this.On = true;
					}
					else
					{
						this.CooldownTimer = 1f;
						this.TurnOff();
					}
				}
			}
			if (this.On && this.Victim == null && this.AlarmDisc != null)
			{
				AlarmDiscScript component = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position + Vector3.up, Quaternion.identity).GetComponent<AlarmDiscScript>();
				component.SourceRadio = this;
				component.NoScream = true;
				component.Radio = true;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
		if (this.Delinquent)
		{
			this.Proximity = 0;
			this.ID = 1;
			while (this.ID < 6)
			{
				if (this.StudentManager.Students[75 + this.ID] != null && Vector3.Distance(base.transform.position, this.StudentManager.Students[75 + this.ID].transform.position) < 1.1f)
				{
					if (!this.StudentManager.Students[75 + this.ID].Alarmed && !this.StudentManager.Students[75 + this.ID].Threatened && this.StudentManager.Students[75 + this.ID].Alive)
					{
						this.Proximity++;
					}
					else
					{
						this.Proximity = -100;
						this.ID = 5;
						this.MyAudio.Stop();
						this.Jukebox.ClubDip = 0f;
					}
				}
				this.ID++;
			}
			if (this.Proximity > 0)
			{
				if (!base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Play();
				}
				float num = Vector3.Distance(this.Prompt.Yandere.transform.position, base.transform.position);
				if (num < 11f)
				{
					this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, (10f - num) * 0.2f * this.Jukebox.Volume, Time.deltaTime);
					if (this.Jukebox.ClubDip < 0f)
					{
						this.Jukebox.ClubDip = 0f;
					}
					if (this.Jukebox.ClubDip > this.Jukebox.Volume)
					{
						this.Jukebox.ClubDip = this.Jukebox.Volume;
						return;
					}
				}
			}
			else if (this.MyAudio.isPlaying)
			{
				this.MyAudio.Stop();
				this.Jukebox.ClubDip = 0f;
			}
		}
	}

	// Token: 0x06001971 RID: 6513 RVA: 0x000F654C File Offset: 0x000F474C
	public void TurnOff()
	{
		this.Prompt.Label[0].text = "     Turn On";
		this.Prompt.enabled = false;
		this.Prompt.Hide();
		this.MyRenderer.material.mainTexture = this.OffTexture;
		base.GetComponent<AudioSource>().Stop();
		this.RadioNotes.SetActive(false);
		this.CooldownTimer = 1f;
		this.Victim = null;
		this.On = false;
	}

	// Token: 0x040026EE RID: 9966
	public StudentManagerScript StudentManager;

	// Token: 0x040026EF RID: 9967
	public JukeboxScript Jukebox;

	// Token: 0x040026F0 RID: 9968
	public GameObject RadioNotes;

	// Token: 0x040026F1 RID: 9969
	public GameObject AlarmDisc;

	// Token: 0x040026F2 RID: 9970
	public AudioSource MyAudio;

	// Token: 0x040026F3 RID: 9971
	public Renderer MyRenderer;

	// Token: 0x040026F4 RID: 9972
	public Texture OffTexture;

	// Token: 0x040026F5 RID: 9973
	public Texture OnTexture;

	// Token: 0x040026F6 RID: 9974
	public StudentScript Victim;

	// Token: 0x040026F7 RID: 9975
	public PromptScript Prompt;

	// Token: 0x040026F8 RID: 9976
	public float CooldownTimer;

	// Token: 0x040026F9 RID: 9977
	public bool Delinquent;

	// Token: 0x040026FA RID: 9978
	public bool On;

	// Token: 0x040026FB RID: 9979
	public int Proximity;

	// Token: 0x040026FC RID: 9980
	public int ID;
}
