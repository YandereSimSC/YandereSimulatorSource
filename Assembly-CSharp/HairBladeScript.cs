using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class HairBladeScript : MonoBehaviour
{
	// Token: 0x060016DB RID: 5851 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x000BD088 File Offset: 0x000BB288
	private void OnTriggerEnter(Collider other)
	{
		GameObject gameObject = other.gameObject.transform.root.gameObject;
		if (gameObject.GetComponent<StudentScript>() != null)
		{
			this.Student = gameObject.GetComponent<StudentScript>();
			if (this.Student.StudentID != 1 && this.Student.Alive)
			{
				this.Student.DeathType = DeathType.EasterEgg;
				UnityEngine.Object.Instantiate<GameObject>(this.Student.Male ? this.MaleBloodyScream : this.FemaleBloodyScream, this.Student.transform.position + Vector3.up, Quaternion.identity);
				this.Student.BecomeRagdoll();
				this.Student.Ragdoll.Dismember();
				base.GetComponent<AudioSource>().Play();
			}
		}
	}

	// Token: 0x04001E2A RID: 7722
	public GameObject FemaleBloodyScream;

	// Token: 0x04001E2B RID: 7723
	public GameObject MaleBloodyScream;

	// Token: 0x04001E2C RID: 7724
	public Vector3 PreviousPosition;

	// Token: 0x04001E2D RID: 7725
	public Collider MyCollider;

	// Token: 0x04001E2E RID: 7726
	public float Timer;

	// Token: 0x04001E2F RID: 7727
	public StudentScript Student;
}
