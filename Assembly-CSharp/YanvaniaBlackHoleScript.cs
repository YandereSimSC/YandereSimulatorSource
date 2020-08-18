using System;
using UnityEngine;

// Token: 0x02000475 RID: 1141
public class YanvaniaBlackHoleScript : MonoBehaviour
{
	// Token: 0x06001D9E RID: 7582 RVA: 0x00170598 File Offset: 0x0016E798
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 1f)
		{
			this.SpawnTimer -= Time.deltaTime;
			if (this.SpawnTimer <= 0f && this.Attacks < 5)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BlackHoleAttack, base.transform.position, Quaternion.identity);
				this.SpawnTimer = 0.5f;
				this.Attacks++;
			}
		}
	}

	// Token: 0x04003A80 RID: 14976
	public GameObject BlackHoleAttack;

	// Token: 0x04003A81 RID: 14977
	public int Attacks;

	// Token: 0x04003A82 RID: 14978
	public float SpawnTimer;

	// Token: 0x04003A83 RID: 14979
	public float Timer;
}
