using System;
using UnityEngine;

// Token: 0x02000421 RID: 1057
public class TimeStopKnifeScript : MonoBehaviour
{
	// Token: 0x06001C2E RID: 7214 RVA: 0x001502A0 File Offset: 0x0014E4A0
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x001502C4 File Offset: 0x0014E4C4
	private void Update()
	{
		if (!this.Unfreeze)
		{
			this.Speed = Mathf.MoveTowards(this.Speed, 0f, Time.deltaTime);
			if (base.transform.localScale.x < 0.99f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			}
		}
		else
		{
			this.Speed = 10f;
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		base.transform.Translate(Vector3.forward * this.Speed * Time.deltaTime, Space.Self);
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x001503A4 File Offset: 0x0014E5A4
	private void OnTriggerEnter(Collider other)
	{
		if (this.Unfreeze && other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				if (component.Male)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.MaleScream, base.transform.position, Quaternion.identity);
				}
				else
				{
					UnityEngine.Object.Instantiate<GameObject>(this.FemaleScream, base.transform.position, Quaternion.identity);
				}
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
			}
		}
	}

	// Token: 0x040034B2 RID: 13490
	public GameObject FemaleScream;

	// Token: 0x040034B3 RID: 13491
	public GameObject MaleScream;

	// Token: 0x040034B4 RID: 13492
	public bool Unfreeze;

	// Token: 0x040034B5 RID: 13493
	public float Speed = 0.1f;

	// Token: 0x040034B6 RID: 13494
	private float Timer;
}
