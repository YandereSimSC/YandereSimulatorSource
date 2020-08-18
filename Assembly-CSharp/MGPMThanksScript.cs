using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000015 RID: 21
public class MGPMThanksScript : MonoBehaviour
{
	// Token: 0x06000096 RID: 150 RVA: 0x00007CD3 File Offset: 0x00005ED3
	private void Start()
	{
		this.Black.material.color = new Color(0f, 0f, 0f, 1f);
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00007D00 File Offset: 0x00005F00
	private void Update()
	{
		if (this.Phase == 1)
		{
			this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 0f, Time.deltaTime));
			if (this.Black.material.color.a == 0f)
			{
				this.Jukebox.Play();
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 2)
		{
			if (!this.Jukebox.isPlaying)
			{
				this.Jukebox.clip = this.ThanksMusic;
				this.Jukebox.loop = true;
				this.Jukebox.Play();
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 3)
		{
			if (Input.anyKeyDown)
			{
				this.Phase++;
				return;
			}
		}
		else
		{
			this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 1f, Time.deltaTime));
			this.Jukebox.volume = 1f - this.Black.material.color.a;
			if (this.Black.material.color.a == 1f)
			{
				HomeGlobals.MiyukiDefeated = true;
				SceneManager.LoadScene("HomeScene");
			}
		}
	}

	// Token: 0x04000125 RID: 293
	public AudioClip ThanksMusic;

	// Token: 0x04000126 RID: 294
	public AudioSource Jukebox;

	// Token: 0x04000127 RID: 295
	public Renderer Black;

	// Token: 0x04000128 RID: 296
	public int Phase = 1;
}
