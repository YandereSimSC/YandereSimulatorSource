using System;
using UnityEngine;

// Token: 0x02000480 RID: 1152
public class YanvaniaJukeboxScript : MonoBehaviour
{
	// Token: 0x06001DC0 RID: 7616 RVA: 0x001723B8 File Offset: 0x001705B8
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (component.time + Time.deltaTime > component.clip.length)
		{
			component.clip = (this.Boss ? this.BossMain : this.ApproachMain);
			component.loop = true;
			component.Play();
		}
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x0017240E File Offset: 0x0017060E
	public void BossBattle()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.BossIntro;
		component.loop = false;
		component.volume = 0.25f;
		component.Play();
		this.Boss = true;
	}

	// Token: 0x04003AD7 RID: 15063
	public AudioClip BossIntro;

	// Token: 0x04003AD8 RID: 15064
	public AudioClip BossMain;

	// Token: 0x04003AD9 RID: 15065
	public AudioClip ApproachIntro;

	// Token: 0x04003ADA RID: 15066
	public AudioClip ApproachMain;

	// Token: 0x04003ADB RID: 15067
	public bool Boss;
}
