using System;
using UnityEngine;

// Token: 0x02000388 RID: 904
public class RandomSoundScript : MonoBehaviour
{
	// Token: 0x06001988 RID: 6536 RVA: 0x000F8BA4 File Offset: 0x000F6DA4
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Clips[UnityEngine.Random.Range(1, this.Clips.Length)];
		component.Play();
	}

	// Token: 0x0400275A RID: 10074
	public AudioClip[] Clips;
}
