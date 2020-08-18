using System;
using UnityEngine;

// Token: 0x02000468 RID: 1128
public class WritheScript : MonoBehaviour
{
	// Token: 0x06001D26 RID: 7462 RVA: 0x0015C71B File Offset: 0x0015A91B
	private void Start()
	{
		this.StartTime = Time.time;
		this.Duration = UnityEngine.Random.Range(1f, 5f);
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x0015C740 File Offset: 0x0015A940
	private void Update()
	{
		if (this.Rotation == this.EndValue)
		{
			this.StartValue = this.EndValue;
			this.EndValue = UnityEngine.Random.Range(-45f, 45f);
			this.StartTime = Time.time;
			this.Duration = UnityEngine.Random.Range(1f, 5f);
		}
		float t = (Time.time - this.StartTime) / this.Duration;
		this.Rotation = Mathf.SmoothStep(this.StartValue, this.EndValue, t);
		switch (this.ID)
		{
		case 1:
			base.transform.localEulerAngles = new Vector3(this.Rotation, base.transform.localEulerAngles.y, base.transform.localEulerAngles.z);
			return;
		case 2:
			if (this.SpecialCase)
			{
				this.Rotation += 180f;
			}
			base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
			return;
		case 3:
			base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, this.Rotation);
			return;
		default:
			return;
		}
	}

	// Token: 0x04003702 RID: 14082
	public float Rotation;

	// Token: 0x04003703 RID: 14083
	public float StartTime;

	// Token: 0x04003704 RID: 14084
	public float Duration;

	// Token: 0x04003705 RID: 14085
	public float StartValue;

	// Token: 0x04003706 RID: 14086
	public float EndValue;

	// Token: 0x04003707 RID: 14087
	public int ID;

	// Token: 0x04003708 RID: 14088
	public bool SpecialCase;
}
