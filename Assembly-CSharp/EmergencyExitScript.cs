using System;
using UnityEngine;

// Token: 0x0200027D RID: 637
public class EmergencyExitScript : MonoBehaviour
{
	// Token: 0x06001393 RID: 5011 RVA: 0x000A840C File Offset: 0x000A660C
	private void Update()
	{
		if (Vector3.Distance(this.Yandere.position, base.transform.position) < 2f)
		{
			this.Open = true;
		}
		else if (this.Timer == 0f)
		{
			this.Student = null;
			this.Open = false;
		}
		if (!this.Open)
		{
			this.Pivot.localEulerAngles = new Vector3(this.Pivot.localEulerAngles.x, Mathf.Lerp(this.Pivot.localEulerAngles.y, 0f, Time.deltaTime * 10f), this.Pivot.localEulerAngles.z);
			return;
		}
		this.Pivot.localEulerAngles = new Vector3(this.Pivot.localEulerAngles.x, Mathf.Lerp(this.Pivot.localEulerAngles.y, 90f, Time.deltaTime * 10f), this.Pivot.localEulerAngles.z);
		this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x000A852D File Offset: 0x000A672D
	private void OnTriggerStay(Collider other)
	{
		this.Student = other.gameObject.GetComponent<StudentScript>();
		if (this.Student != null)
		{
			this.Timer = 1f;
			this.Open = true;
		}
	}

	// Token: 0x04001AC3 RID: 6851
	public StudentScript Student;

	// Token: 0x04001AC4 RID: 6852
	public Transform Yandere;

	// Token: 0x04001AC5 RID: 6853
	public Transform Pivot;

	// Token: 0x04001AC6 RID: 6854
	public float Timer;

	// Token: 0x04001AC7 RID: 6855
	public bool Open;
}
