using System;
using UnityEngine;

// Token: 0x0200040D RID: 1037
public class SuckScript : MonoBehaviour
{
	// Token: 0x06001BE8 RID: 7144 RVA: 0x001469AC File Offset: 0x00144BAC
	private void Update()
	{
		this.Strength += Time.deltaTime;
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Student.Yandere.Hips.position + base.transform.up * 0.25f, Time.deltaTime * this.Strength);
		if (Vector3.Distance(base.transform.position, this.Student.Yandere.Hips.position + base.transform.up * 0.25f) < 1f)
		{
			base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, Vector3.zero, Time.deltaTime);
			if (base.transform.localScale == Vector3.zero)
			{
				base.transform.parent.parent.parent.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x040033CA RID: 13258
	public StudentScript Student;

	// Token: 0x040033CB RID: 13259
	public float Strength;
}
