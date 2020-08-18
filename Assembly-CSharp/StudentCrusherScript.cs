using System;
using UnityEngine;

// Token: 0x02000401 RID: 1025
public class StudentCrusherScript : MonoBehaviour
{
	// Token: 0x06001B0E RID: 6926 RVA: 0x00111324 File Offset: 0x0010F524
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.gameObject.layer == 9)
		{
			StudentScript component = other.transform.root.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				if (this.Mecha.Speed > 0.9f)
				{
					UnityEngine.Object.Instantiate<GameObject>(component.BloodyScream, base.transform.position + Vector3.up, Quaternion.identity);
					component.BecomeRagdoll();
				}
				if (this.Mecha.Speed > 5f)
				{
					component.Ragdoll.Dismember();
				}
			}
		}
	}

	// Token: 0x04002C3C RID: 11324
	public MechaScript Mecha;
}
