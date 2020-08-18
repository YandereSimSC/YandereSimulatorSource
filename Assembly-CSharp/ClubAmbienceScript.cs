using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
public class ClubAmbienceScript : MonoBehaviour
{
	// Token: 0x0600125D RID: 4701 RVA: 0x00083F2C File Offset: 0x0008212C
	private void Update()
	{
		if (this.Yandere.position.y > base.transform.position.y - 0.1f && this.Yandere.position.y < base.transform.position.y + 0.1f)
		{
			if (Vector3.Distance(base.transform.position, this.Yandere.position) < 4f)
			{
				this.CreateAmbience = true;
				this.EffectJukebox = true;
			}
			else
			{
				this.CreateAmbience = false;
			}
		}
		if (this.EffectJukebox)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.CreateAmbience)
			{
				component.volume = Mathf.MoveTowards(component.volume, this.MaxVolume, Time.deltaTime * 0.1f);
				this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, this.ClubDip, Time.deltaTime * 0.1f);
				return;
			}
			component.volume = Mathf.MoveTowards(component.volume, 0f, Time.deltaTime * 0.1f);
			this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, 0f, Time.deltaTime * 0.1f);
			if (this.Jukebox.ClubDip == 0f)
			{
				this.EffectJukebox = false;
			}
		}
	}

	// Token: 0x040015ED RID: 5613
	public JukeboxScript Jukebox;

	// Token: 0x040015EE RID: 5614
	public Transform Yandere;

	// Token: 0x040015EF RID: 5615
	public bool CreateAmbience;

	// Token: 0x040015F0 RID: 5616
	public bool EffectJukebox;

	// Token: 0x040015F1 RID: 5617
	public float ClubDip;

	// Token: 0x040015F2 RID: 5618
	public float MaxVolume;
}
