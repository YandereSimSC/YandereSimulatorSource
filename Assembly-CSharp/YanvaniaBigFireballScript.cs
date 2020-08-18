using System;
using UnityEngine;

// Token: 0x02000473 RID: 1139
public class YanvaniaBigFireballScript : MonoBehaviour
{
	// Token: 0x06001D98 RID: 7576 RVA: 0x00170410 File Offset: 0x0016E610
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YanmontChan")
		{
			other.gameObject.GetComponent<YanvaniaYanmontScript>().TakeDamage(15);
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003A7D RID: 14973
	public GameObject Explosion;
}
