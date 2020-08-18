using System;
using UnityEngine;

// Token: 0x0200047E RID: 1150
public class YanvaniaJarScript : MonoBehaviour
{
	// Token: 0x06001DBB RID: 7611 RVA: 0x00172210 File Offset: 0x00170410
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 19 && !this.Destroyed)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position + Vector3.up * 0.5f, Quaternion.identity);
			this.Destroyed = true;
			AudioClipPlayer.Play2D(this.Break, base.transform.position);
			for (int i = 1; i < 11; i++)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.Shard, base.transform.position + Vector3.up * UnityEngine.Random.Range(0f, 1f) + Vector3.right * UnityEngine.Random.Range(-0.5f, 0.5f), Quaternion.identity);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003AD1 RID: 15057
	public GameObject Explosion;

	// Token: 0x04003AD2 RID: 15058
	public bool Destroyed;

	// Token: 0x04003AD3 RID: 15059
	public AudioClip Break;

	// Token: 0x04003AD4 RID: 15060
	public GameObject Shard;
}
