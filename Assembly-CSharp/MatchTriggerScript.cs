using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class MatchTriggerScript : MonoBehaviour
{
	// Token: 0x06001811 RID: 6161 RVA: 0x000D65AC File Offset: 0x000D47AC
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student == null)
			{
				GameObject gameObject = other.gameObject.transform.root.gameObject;
				this.Student = gameObject.GetComponent<StudentScript>();
			}
			if (this.Student != null && (this.Student.Gas || this.Fireball))
			{
				this.Student.Combust();
				if (this.PickUp != null && this.PickUp.Yandere.PickUp != null && this.PickUp.Yandere.PickUp == this.PickUp)
				{
					this.PickUp.Yandere.TargetStudent = this.Student;
					this.PickUp.Yandere.MurderousActionTimer = 1f;
				}
				if (this.Fireball)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x040022D0 RID: 8912
	public PickUpScript PickUp;

	// Token: 0x040022D1 RID: 8913
	public StudentScript Student;

	// Token: 0x040022D2 RID: 8914
	public bool Fireball;
}
