using System;
using UnityEngine;

// Token: 0x02000238 RID: 568
public class CirnoIceAttackScript : MonoBehaviour
{
	// Token: 0x06001241 RID: 4673 RVA: 0x000813EA File Offset: 0x0007F5EA
	private void Start()
	{
		Physics.IgnoreLayerCollision(18, 13, true);
		Physics.IgnoreLayerCollision(18, 18, true);
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x00081400 File Offset: 0x0007F600
	private void OnCollisionEnter(Collision collision)
	{
		UnityEngine.Object.Instantiate<GameObject>(this.IceExplosion, base.transform.position, Quaternion.identity);
		if (collision.gameObject.layer == 9)
		{
			StudentScript component = collision.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				component.SpawnAlarmDisc();
				component.BecomeRagdoll();
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400158D RID: 5517
	public GameObject IceExplosion;
}
