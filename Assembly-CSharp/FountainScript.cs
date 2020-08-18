using System;
using UnityEngine;

// Token: 0x020002A9 RID: 681
public class FountainScript : MonoBehaviour
{
	// Token: 0x0600141A RID: 5146 RVA: 0x000B0293 File Offset: 0x000AE493
	private void Start()
	{
		this.SpraySFX.volume = 0.1f;
		this.DropsSFX.volume = 0.1f;
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x000B02B8 File Offset: 0x000AE4B8
	private void Update()
	{
		if (this.StartTimer < 1f)
		{
			this.StartTimer += Time.deltaTime;
			if (this.StartTimer > 1f)
			{
				this.SpraySFX.gameObject.SetActive(true);
				this.DropsSFX.gameObject.SetActive(true);
			}
		}
		if (this.Drowning)
		{
			if (this.Timer == 0f && this.EventSubtitle.transform.localScale.x < 1f)
			{
				this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
				this.EventSubtitle.text = "Hey, what are you -";
				base.GetComponent<AudioSource>().Play();
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > 3f && this.EventSubtitle.transform.localScale.x > 0f)
			{
				this.EventSubtitle.transform.localScale = Vector3.zero;
				this.EventSubtitle.text = string.Empty;
				this.Splashes.Play();
			}
			if (this.Timer > 9f)
			{
				this.Drowning = false;
				this.Splashes.Stop();
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x04001C64 RID: 7268
	public ParticleSystem Splashes;

	// Token: 0x04001C65 RID: 7269
	public UILabel EventSubtitle;

	// Token: 0x04001C66 RID: 7270
	public Collider[] Colliders;

	// Token: 0x04001C67 RID: 7271
	public bool Drowning;

	// Token: 0x04001C68 RID: 7272
	public AudioSource SpraySFX;

	// Token: 0x04001C69 RID: 7273
	public AudioSource DropsSFX;

	// Token: 0x04001C6A RID: 7274
	public float StartTimer;

	// Token: 0x04001C6B RID: 7275
	public float Timer;
}
