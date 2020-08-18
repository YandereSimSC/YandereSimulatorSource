using System;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class CheerScript : MonoBehaviour
{
	// Token: 0x06001234 RID: 4660 RVA: 0x00080D3C File Offset: 0x0007EF3C
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 5f)
		{
			this.MyAudio.clip = this.Cheers[UnityEngine.Random.Range(1, this.Cheers.Length)];
			this.MyAudio.Play();
			this.Timer = 0f;
		}
	}

	// Token: 0x04001575 RID: 5493
	public AudioSource MyAudio;

	// Token: 0x04001576 RID: 5494
	public AudioClip[] Cheers;

	// Token: 0x04001577 RID: 5495
	public float Timer;
}
