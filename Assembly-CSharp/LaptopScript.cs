using System;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class LaptopScript : MonoBehaviour
{
	// Token: 0x060017DA RID: 6106 RVA: 0x000D1890 File Offset: 0x000CFA90
	private void Start()
	{
		if (SchoolGlobals.SCP)
		{
			this.LaptopScreen.localScale = Vector3.zero;
			this.LaptopCamera.enabled = false;
			this.SCP.SetActive(false);
			base.enabled = false;
			return;
		}
		this.SCPRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];
		Animation component = this.SCP.GetComponent<Animation>();
		component["f02_scp_00"].speed = 0f;
		component["f02_scp_00"].time = 0f;
		this.MyAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x060017DB RID: 6107 RVA: 0x000D192C File Offset: 0x000CFB2C
	private void Update()
	{
		if (this.FirstFrame == 2)
		{
			this.LaptopCamera.enabled = false;
		}
		this.FirstFrame++;
		if (!this.Off)
		{
			Animation component = this.SCP.GetComponent<Animation>();
			if (!this.React)
			{
				if (this.Yandere.transform.position.x > base.transform.position.x + 1f && Vector3.Distance(this.Yandere.transform.position, new Vector3(base.transform.position.x, 4f, base.transform.position.z)) < 2f && this.Yandere.Followers == 0)
				{
					this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
					component["f02_scp_00"].time = 0f;
					this.LaptopCamera.enabled = true;
					component.Play();
					this.Hair.enabled = true;
					this.Jukebox.Dip = 0.5f;
					this.MyAudio.Play();
					this.React = true;
				}
			}
			else
			{
				this.MyAudio.pitch = Time.timeScale;
				this.MyAudio.volume = 1f;
				if (this.Yandere.transform.position.y > base.transform.position.y + 3f || this.Yandere.transform.position.y < base.transform.position.y - 3f)
				{
					this.MyAudio.volume = 0f;
				}
				for (int i = 0; i < this.Cues.Length; i++)
				{
					if (this.MyAudio.time > this.Cues[i])
					{
						this.EventSubtitle.text = this.Subs[i];
					}
				}
				if (this.MyAudio.time >= this.MyAudio.clip.length - 1f || this.MyAudio.time == 0f)
				{
					component["f02_scp_00"].speed = 1f;
					this.Timer += Time.deltaTime;
				}
				else
				{
					component["f02_scp_00"].time = this.MyAudio.time;
				}
				if (this.Timer > 1f || Vector3.Distance(this.Yandere.transform.position, new Vector3(base.transform.position.x, 4f, base.transform.position.z)) > 5f)
				{
					this.TurnOff();
				}
			}
			if (this.Yandere.StudentManager.Clock.HourTime > 16f || this.Yandere.Police.FadeOut)
			{
				this.TurnOff();
				return;
			}
		}
		else
		{
			if (this.LaptopScreen.localScale.x > 0.1f)
			{
				this.LaptopScreen.localScale = Vector3.Lerp(this.LaptopScreen.localScale, Vector3.zero, Time.deltaTime * 10f);
				return;
			}
			if (base.enabled)
			{
				this.LaptopScreen.localScale = Vector3.zero;
				this.Hair.enabled = false;
				base.enabled = false;
			}
		}
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x000D1CC0 File Offset: 0x000CFEC0
	private void TurnOff()
	{
		this.MyAudio.clip = this.ShutDown;
		this.MyAudio.Play();
		this.EventSubtitle.text = string.Empty;
		SchoolGlobals.SCP = true;
		this.LaptopCamera.enabled = false;
		this.Jukebox.Dip = 1f;
		this.React = false;
		this.Off = true;
	}

	// Token: 0x04002214 RID: 8724
	public SkinnedMeshRenderer SCPRenderer;

	// Token: 0x04002215 RID: 8725
	public Camera LaptopCamera;

	// Token: 0x04002216 RID: 8726
	public JukeboxScript Jukebox;

	// Token: 0x04002217 RID: 8727
	public YandereScript Yandere;

	// Token: 0x04002218 RID: 8728
	public AudioSource MyAudio;

	// Token: 0x04002219 RID: 8729
	public DynamicBone Hair;

	// Token: 0x0400221A RID: 8730
	public Transform LaptopScreen;

	// Token: 0x0400221B RID: 8731
	public AudioClip ShutDown;

	// Token: 0x0400221C RID: 8732
	public GameObject SCP;

	// Token: 0x0400221D RID: 8733
	public bool React;

	// Token: 0x0400221E RID: 8734
	public bool Off;

	// Token: 0x0400221F RID: 8735
	public float[] Cues;

	// Token: 0x04002220 RID: 8736
	public string[] Subs;

	// Token: 0x04002221 RID: 8737
	public Mesh[] Uniforms;

	// Token: 0x04002222 RID: 8738
	public int FirstFrame;

	// Token: 0x04002223 RID: 8739
	public float Timer;

	// Token: 0x04002224 RID: 8740
	public UILabel EventSubtitle;
}
