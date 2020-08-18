using System;
using UnityEngine;

// Token: 0x02000476 RID: 1142
public class YanvaniaBossHeadScript : MonoBehaviour
{
	// Token: 0x06001DA0 RID: 7584 RVA: 0x00170621 File Offset: 0x0016E821
	private void Update()
	{
		this.Timer -= Time.deltaTime;
	}

	// Token: 0x06001DA1 RID: 7585 RVA: 0x00170638 File Offset: 0x0016E838
	private void OnTriggerEnter(Collider other)
	{
		if (this.Timer <= 0f && this.Dracula.NewTeleportEffect == null && other.gameObject.name == "Heart")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, base.transform.position, Quaternion.identity);
			this.Timer = 1f;
			this.Dracula.TakeDamage();
		}
	}

	// Token: 0x04003A84 RID: 14980
	public YanvaniaDraculaScript Dracula;

	// Token: 0x04003A85 RID: 14981
	public GameObject HitEffect;

	// Token: 0x04003A86 RID: 14982
	public float Timer;
}
