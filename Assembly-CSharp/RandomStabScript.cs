using System;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class RandomStabScript : MonoBehaviour
{
	// Token: 0x0600198A RID: 6538 RVA: 0x000F8BCC File Offset: 0x000F6DCC
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.Biting)
		{
			component.clip = this.Stabs[UnityEngine.Random.Range(0, this.Stabs.Length)];
			component.Play();
			return;
		}
		component.clip = this.Bite;
		component.pitch = UnityEngine.Random.Range(0.5f, 1f);
		component.Play();
	}

	// Token: 0x0400275B RID: 10075
	public AudioClip[] Stabs;

	// Token: 0x0400275C RID: 10076
	public AudioClip Bite;

	// Token: 0x0400275D RID: 10077
	public bool Biting;
}
