using System;
using UnityEngine;

// Token: 0x02000420 RID: 1056
public class TimePortalScript : MonoBehaviour
{
	// Token: 0x06001C2C RID: 7212 RVA: 0x001501E4 File Offset: 0x0014E3E4
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Suck = true;
		}
		if (this.Suck)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BlackHole, base.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1.1f)
			{
				this.Delinquent[this.ID].Suck = true;
				this.Timer = 1f;
				this.ID++;
				if (this.ID > 9)
				{
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x040034AD RID: 13485
	public DelinquentScript[] Delinquent;

	// Token: 0x040034AE RID: 13486
	public GameObject BlackHole;

	// Token: 0x040034AF RID: 13487
	public float Timer;

	// Token: 0x040034B0 RID: 13488
	public bool Suck;

	// Token: 0x040034B1 RID: 13489
	public int ID;
}
