using System;
using UnityEngine;

// Token: 0x02000479 RID: 1145
public class YanvaniaCandlestickScript : MonoBehaviour
{
	// Token: 0x06001DA9 RID: 7593 RVA: 0x00170980 File Offset: 0x0016EB80
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 19 && !this.Destroyed)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.DestroyedCandlestick, base.transform.position, Quaternion.identity).transform.localScale = base.transform.localScale;
			this.Destroyed = true;
			AudioClipPlayer.Play2D(this.Break, base.transform.position);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003A90 RID: 14992
	public GameObject DestroyedCandlestick;

	// Token: 0x04003A91 RID: 14993
	public bool Destroyed;

	// Token: 0x04003A92 RID: 14994
	public AudioClip Break;
}
