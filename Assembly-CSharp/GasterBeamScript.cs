using System;
using UnityEngine;

// Token: 0x020002AE RID: 686
public class GasterBeamScript : MonoBehaviour
{
	// Token: 0x0600142E RID: 5166 RVA: 0x000B1BEA File Offset: 0x000AFDEA
	private void Start()
	{
		if (this.LoveLoveBeam)
		{
			base.transform.localScale = new Vector3(0f, 0f, 0f);
		}
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x000B1C14 File Offset: 0x000AFE14
	private void Update()
	{
		if (this.LoveLoveBeam)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(100f, this.Target, this.Target), Time.deltaTime * 10f);
			if (base.transform.localScale.x > 99.99f)
			{
				this.Target = 0f;
				if (base.transform.localScale.y < 0.1f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x000B1CAC File Offset: 0x000AFEAC
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
				Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
				rigidbody.isKinematic = false;
				rigidbody.AddForce((rigidbody.transform.root.position - base.transform.root.position) * this.Strength);
				rigidbody.AddForce(Vector3.up * 1000f);
			}
		}
	}

	// Token: 0x04001CAE RID: 7342
	public float Strength = 1000f;

	// Token: 0x04001CAF RID: 7343
	public float Target = 2f;

	// Token: 0x04001CB0 RID: 7344
	public bool LoveLoveBeam;
}
