using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class BoneScript : MonoBehaviour
{
	// Token: 0x06000A67 RID: 2663 RVA: 0x00055980 File Offset: 0x00053B80
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, UnityEngine.Random.Range(0f, 360f), base.transform.eulerAngles.z);
		this.Origin = base.transform.position.y;
		base.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f, 1.1f);
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x000559FC File Offset: 0x00053BFC
	private void Update()
	{
		if (this.Drop)
		{
			this.Height -= Time.deltaTime;
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + this.Height, base.transform.position.z);
			if (base.transform.position.y < this.Origin - 2.155f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		if (base.transform.position.y < this.Origin + 2f - 0.0001f)
		{
			base.transform.position = new Vector3(base.transform.position.x, Mathf.Lerp(base.transform.position.y, this.Origin + 2f, Time.deltaTime * 10f), base.transform.position.z);
			return;
		}
		this.Drop = true;
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x00055B20 File Offset: 0x00053D20
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
				rigidbody.AddForce(base.transform.up);
			}
		}
	}

	// Token: 0x04000AC3 RID: 2755
	public float Height;

	// Token: 0x04000AC4 RID: 2756
	public float Origin;

	// Token: 0x04000AC5 RID: 2757
	public bool Drop;
}
