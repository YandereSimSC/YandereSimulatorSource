using System;
using UnityEngine;

// Token: 0x02000362 RID: 866
public class PianoScript : MonoBehaviour
{
	// Token: 0x060018E9 RID: 6377 RVA: 0x000E83F4 File Offset: 0x000E65F4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount < 1f && this.Prompt.Circle[0].fillAmount > 0f)
		{
			this.Prompt.Circle[0].fillAmount = 0f;
			this.Notes[this.ID].Play();
			this.ID++;
			if (this.ID == this.Notes.Length)
			{
				this.ID = 0;
			}
		}
	}

	// Token: 0x0400252C RID: 9516
	public PromptScript Prompt;

	// Token: 0x0400252D RID: 9517
	public AudioSource[] Notes;

	// Token: 0x0400252E RID: 9518
	public int ID;
}
