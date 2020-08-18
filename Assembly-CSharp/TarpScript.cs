using System;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class TarpScript : MonoBehaviour
{
	// Token: 0x06001C08 RID: 7176 RVA: 0x0014E84A File Offset: 0x0014CA4A
	private void Start()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x0014E86C File Offset: 0x0014CA6C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			AudioSource.PlayClipAtPoint(this.Tarp, base.transform.position);
			this.Unwrap = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Mecha.enabled = true;
			this.Mecha.Prompt.enabled = true;
		}
		if (this.Unwrap)
		{
			this.Speed += Time.deltaTime * 10f;
			base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, new Vector3(90f, 90f, 0f), Time.deltaTime * this.Speed);
			if (base.transform.localEulerAngles.x > 45f)
			{
				if (this.PreviousSpeed == 0f)
				{
					this.PreviousSpeed = this.Speed;
				}
				this.Speed += Time.deltaTime * 10f;
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 0.0001f), (this.Speed - this.PreviousSpeed) * Time.deltaTime);
			}
		}
	}

	// Token: 0x04003456 RID: 13398
	public PromptScript Prompt;

	// Token: 0x04003457 RID: 13399
	public MechaScript Mecha;

	// Token: 0x04003458 RID: 13400
	public AudioClip Tarp;

	// Token: 0x04003459 RID: 13401
	public float PreviousSpeed;

	// Token: 0x0400345A RID: 13402
	public float Speed;

	// Token: 0x0400345B RID: 13403
	public bool Unwrap;
}
