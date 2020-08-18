using System;
using UnityEngine;

// Token: 0x020003F3 RID: 1011
public class StandPunchScript : MonoBehaviour
{
	// Token: 0x06001ADD RID: 6877 RVA: 0x0010DA5C File Offset: 0x0010BC5C
	private void OnTriggerEnter(Collider other)
	{
		StudentScript component = other.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID > 1)
		{
			component.JojoReact();
		}
	}

	// Token: 0x04002B8A RID: 11146
	public Collider MyCollider;
}
