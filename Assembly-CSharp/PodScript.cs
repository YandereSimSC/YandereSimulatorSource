using System;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class PodScript : MonoBehaviour
{
	// Token: 0x060018FE RID: 6398 RVA: 0x000EA210 File Offset: 0x000E8410
	private void Start()
	{
		this.Timer = 1f;
	}

	// Token: 0x060018FF RID: 6399 RVA: 0x000EA220 File Offset: 0x000E8420
	private void LateUpdate()
	{
		this.PodTarget.transform.parent.eulerAngles = new Vector3(0f, this.AimTarget.parent.eulerAngles.y, 0f);
		base.transform.position = Vector3.Lerp(base.transform.position, this.PodTarget.position, Time.deltaTime * 100f);
		base.transform.rotation = this.AimTarget.parent.rotation;
		base.transform.eulerAngles += new Vector3(-15f, 7.5f, 0f);
		if (Input.GetButton("RB"))
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > this.FireRate)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position, base.transform.rotation);
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x0400258A RID: 9610
	public GameObject Projectile;

	// Token: 0x0400258B RID: 9611
	public Transform SpawnPoint;

	// Token: 0x0400258C RID: 9612
	public Transform PodTarget;

	// Token: 0x0400258D RID: 9613
	public Transform AimTarget;

	// Token: 0x0400258E RID: 9614
	public float FireRate;

	// Token: 0x0400258F RID: 9615
	public float Timer;
}
