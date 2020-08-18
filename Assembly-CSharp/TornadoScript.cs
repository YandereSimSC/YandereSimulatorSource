using System;
using UnityEngine;

// Token: 0x0200042A RID: 1066
public class TornadoScript : MonoBehaviour
{
	// Token: 0x06001C57 RID: 7255 RVA: 0x00152C44 File Offset: 0x00150E44
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 0.5f)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime, base.transform.position.z);
			this.MyCollider.enabled = true;
		}
	}

	// Token: 0x06001C58 RID: 7256 RVA: 0x00152CC4 File Offset: 0x00150EC4
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				this.Scream = UnityEngine.Object.Instantiate<GameObject>(component.Male ? this.MaleBloodyScream : this.FemaleBloodyScream, component.transform.position + Vector3.up, Quaternion.identity);
				this.Scream.transform.parent = component.HipCollider.transform;
				this.Scream.transform.localPosition = Vector3.zero;
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
				Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
				rigidbody.isKinematic = false;
				rigidbody.AddForce(Vector3.up * this.Strength);
			}
		}
	}

	// Token: 0x0400352F RID: 13615
	public GameObject FemaleBloodyScream;

	// Token: 0x04003530 RID: 13616
	public GameObject MaleBloodyScream;

	// Token: 0x04003531 RID: 13617
	public GameObject Scream;

	// Token: 0x04003532 RID: 13618
	public Collider MyCollider;

	// Token: 0x04003533 RID: 13619
	public float Strength = 10000f;

	// Token: 0x04003534 RID: 13620
	public float Timer;
}
