using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class AreaScript : MonoBehaviour
{
	// Token: 0x06000A15 RID: 2581 RVA: 0x0004FC80 File Offset: 0x0004DE80
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Student"))
		{
			StudentScript component = other.GetComponent<StudentScript>();
			this.Students.Add(component);
			this.Population++;
		}
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x0004FCBC File Offset: 0x0004DEBC
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Student"))
		{
			StudentScript component = other.GetComponent<StudentScript>();
			this.Students.Remove(component);
			this.Population--;
		}
	}

	// Token: 0x04000A15 RID: 2581
	[Header("Do not touch any of these values. They get updated at runtime.")]
	[Tooltip("The amount of students in this area.")]
	public int Population;

	// Token: 0x04000A16 RID: 2582
	[Tooltip("A list of students in this area.")]
	public List<StudentScript> Students;

	// Token: 0x04000A17 RID: 2583
	[Tooltip("This area's crowd. Students will go here.")]
	public List<StudentScript> Crowd;
}
