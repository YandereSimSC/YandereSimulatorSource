using System;
using UnityEngine;

// Token: 0x02000436 RID: 1078
public class UpthrustScript : MonoBehaviour
{
	// Token: 0x06001C7D RID: 7293 RVA: 0x00154ED0 File Offset: 0x001530D0
	private void Start()
	{
		this.startPosition = base.transform.localPosition;
	}

	// Token: 0x06001C7E RID: 7294 RVA: 0x00154EE4 File Offset: 0x001530E4
	private void Update()
	{
		float d = this.amplitude * Mathf.Sin(6.2831855f * this.frequency * Time.time);
		base.transform.localPosition = this.startPosition + this.evaluatePosition(Time.time);
		base.transform.Rotate(this.rotationAmplitude * d);
	}

	// Token: 0x06001C7F RID: 7295 RVA: 0x00154F48 File Offset: 0x00153148
	private Vector3 evaluatePosition(float time)
	{
		float y = this.amplitude * Mathf.Sin(6.2831855f * this.frequency * time);
		return new Vector3(0f, y, 0f);
	}

	// Token: 0x040035D5 RID: 13781
	[SerializeField]
	private float amplitude = 0.1f;

	// Token: 0x040035D6 RID: 13782
	[SerializeField]
	private float frequency = 0.6f;

	// Token: 0x040035D7 RID: 13783
	[SerializeField]
	private Vector3 rotationAmplitude = new Vector3(4.45f, 4.45f, 4.45f);

	// Token: 0x040035D8 RID: 13784
	private Vector3 startPosition;
}
