using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000C9 RID: 201
public class AntiCheatScript : MonoBehaviour
{
	// Token: 0x06000A00 RID: 2560 RVA: 0x0004F014 File Offset: 0x0004D214
	private void Update()
	{
		if (this.Check && !base.GetComponent<AudioSource>().isPlaying)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0004F048 File Offset: 0x0004D248
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YandereChan")
		{
			this.Jukebox.SetActive(false);
			this.Check = true;
			base.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x040009DE RID: 2526
	public GameObject Jukebox;

	// Token: 0x040009DF RID: 2527
	public bool Check;
}
