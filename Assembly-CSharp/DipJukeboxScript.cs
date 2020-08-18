using System;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class DipJukeboxScript : MonoBehaviour
{
	// Token: 0x06001345 RID: 4933 RVA: 0x000A4078 File Offset: 0x000A2278
	private void Update()
	{
		if (this.MyAudio.isPlaying)
		{
			float num = Vector3.Distance(this.Yandere.position, base.transform.position);
			if (num < 8f)
			{
				this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, (7f - num) * 0.25f * this.Jukebox.Volume, Time.deltaTime);
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
			this.Jukebox.ClubDip = 0f;
		}
	}

	// Token: 0x04001A2B RID: 6699
	public JukeboxScript Jukebox;

	// Token: 0x04001A2C RID: 6700
	public AudioSource MyAudio;

	// Token: 0x04001A2D RID: 6701
	public Transform Yandere;
}
