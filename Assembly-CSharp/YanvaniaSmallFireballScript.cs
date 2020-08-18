using System;
using UnityEngine;

// Token: 0x02000481 RID: 1153
public class YanvaniaSmallFireballScript : MonoBehaviour
{
	// Token: 0x06001DC3 RID: 7619 RVA: 0x00172440 File Offset: 0x00170640
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Heart")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (other.gameObject.name == "YanmontChan")
		{
			other.gameObject.GetComponent<YanvaniaYanmontScript>().TakeDamage(10);
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003ADC RID: 15068
	public GameObject Explosion;
}
