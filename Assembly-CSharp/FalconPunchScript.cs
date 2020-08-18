using System;
using UnityEngine;

// Token: 0x0200029C RID: 668
public class FalconPunchScript : MonoBehaviour
{
	// Token: 0x060013F8 RID: 5112 RVA: 0x000AE3C7 File Offset: 0x000AC5C7
	private void Start()
	{
		if (this.Mecha)
		{
			this.MyRigidbody.AddForce(base.transform.forward * this.Speed * 10f);
		}
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x000AE3FC File Offset: 0x000AC5FC
	private void Update()
	{
		if (!this.IgnoreTime)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > this.TimeLimit)
			{
				this.MyCollider.enabled = false;
			}
		}
		if (this.Shipgirl)
		{
			this.MyRigidbody.AddForce(base.transform.forward * this.Speed);
		}
	}

	// Token: 0x060013FA RID: 5114 RVA: 0x000AE468 File Offset: 0x000AC668
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("A punch collided with something.");
		if (other.gameObject.layer == 9)
		{
			Debug.Log("A punch collided with something on the Characters layer.");
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				Debug.Log("A punch collided with a student.");
				if (component.StudentID > 1)
				{
					Debug.Log("A punch collided with a student and killed them.");
					UnityEngine.Object.Instantiate<GameObject>(this.FalconExplosion, component.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
					component.DeathType = DeathType.EasterEgg;
					component.BecomeRagdoll();
					Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
					rigidbody.isKinematic = false;
					Vector3 vector = rigidbody.transform.position - component.Yandere.transform.position;
					if (this.Falcon)
					{
						rigidbody.AddForce(vector * this.Strength);
					}
					else if (this.Bancho)
					{
						rigidbody.AddForce(vector.x * this.Strength, 5000f, vector.z * this.Strength);
					}
					else
					{
						rigidbody.AddForce(vector.x * this.Strength, 10000f, vector.z * this.Strength);
					}
				}
			}
		}
		if (this.Destructive && other.gameObject.layer != 2 && other.gameObject.layer != 8 && other.gameObject.layer != 9 && other.gameObject.layer != 13 && other.gameObject.layer != 17)
		{
			GameObject gameObject = null;
			StudentScript component2 = other.gameObject.transform.root.GetComponent<StudentScript>();
			if (component2 != null)
			{
				if (component2.StudentID <= 1)
				{
					gameObject = component2.gameObject;
				}
			}
			else
			{
				gameObject = other.gameObject;
			}
			if (gameObject != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.FalconExplosion, base.transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
				UnityEngine.Object.Destroy(gameObject);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04001BFB RID: 7163
	public GameObject FalconExplosion;

	// Token: 0x04001BFC RID: 7164
	public Rigidbody MyRigidbody;

	// Token: 0x04001BFD RID: 7165
	public Collider MyCollider;

	// Token: 0x04001BFE RID: 7166
	public float Strength = 100f;

	// Token: 0x04001BFF RID: 7167
	public float Speed = 100f;

	// Token: 0x04001C00 RID: 7168
	public bool Destructive;

	// Token: 0x04001C01 RID: 7169
	public bool IgnoreTime;

	// Token: 0x04001C02 RID: 7170
	public bool Shipgirl;

	// Token: 0x04001C03 RID: 7171
	public bool Bancho;

	// Token: 0x04001C04 RID: 7172
	public bool Falcon;

	// Token: 0x04001C05 RID: 7173
	public bool Mecha;

	// Token: 0x04001C06 RID: 7174
	public float TimeLimit = 0.5f;

	// Token: 0x04001C07 RID: 7175
	public float Timer;
}
