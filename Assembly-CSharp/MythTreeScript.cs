using System;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class MythTreeScript : MonoBehaviour
{
	// Token: 0x0600184D RID: 6221 RVA: 0x000DA34A File Offset: 0x000D854A
	private void Start()
	{
		if (SchemeGlobals.GetSchemeStage(2) > 2)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x000DA35C File Offset: 0x000D855C
	private void Update()
	{
		if (!this.Spoken)
		{
			if (this.Yandere.Inventory.Ring && Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 5f)
			{
				this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
				this.EventSubtitle.text = "...that...ring...";
				this.Jukebox.Dip = 0.5f;
				this.Spoken = true;
				this.MyAudio.Play();
				return;
			}
		}
		else if (!this.MyAudio.isPlaying)
		{
			this.EventSubtitle.transform.localScale = Vector3.zero;
			this.EventSubtitle.text = string.Empty;
			this.Jukebox.Dip = 1f;
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x04002352 RID: 9042
	public UILabel EventSubtitle;

	// Token: 0x04002353 RID: 9043
	public JukeboxScript Jukebox;

	// Token: 0x04002354 RID: 9044
	public YandereScript Yandere;

	// Token: 0x04002355 RID: 9045
	public bool Spoken;

	// Token: 0x04002356 RID: 9046
	public AudioSource MyAudio;
}
