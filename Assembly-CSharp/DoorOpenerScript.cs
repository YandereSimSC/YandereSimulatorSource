using System;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class DoorOpenerScript : MonoBehaviour
{
	// Token: 0x06001356 RID: 4950 RVA: 0x000A4B34 File Offset: 0x000A2D34
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student != null && !this.Student.Dying && !this.Door.Open && !this.Door.Locked)
			{
				this.Door.Student = this.Student;
				this.Door.OpenDoor();
			}
		}
	}

	// Token: 0x04001A45 RID: 6725
	public StudentScript Student;

	// Token: 0x04001A46 RID: 6726
	public DoorScript Door;
}
