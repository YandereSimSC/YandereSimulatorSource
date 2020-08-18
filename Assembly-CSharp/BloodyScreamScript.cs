using System;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class BloodyScreamScript : MonoBehaviour
{
	// Token: 0x06000A5F RID: 2655 RVA: 0x00055603 File Offset: 0x00053803
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Screams[UnityEngine.Random.Range(0, this.Screams.Length)];
		component.Play();
	}

	// Token: 0x04000AAB RID: 2731
	public AudioClip[] Screams;
}
