using System;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class GentlemanScript : MonoBehaviour
{
	// Token: 0x06001441 RID: 5185 RVA: 0x000B31D4 File Offset: 0x000B13D4
	private void Update()
	{
		if (Input.GetButtonDown("RB"))
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (!component.isPlaying)
			{
				component.clip = this.Clips[UnityEngine.Random.Range(0, this.Clips.Length - 1)];
				component.Play();
				this.Yandere.Sanity += 10f;
			}
		}
	}

	// Token: 0x04001CE2 RID: 7394
	public YandereScript Yandere;

	// Token: 0x04001CE3 RID: 7395
	public AudioClip[] Clips;
}
