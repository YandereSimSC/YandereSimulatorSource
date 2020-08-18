using System;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class CameraShake : MonoBehaviour
{
	// Token: 0x06001216 RID: 4630 RVA: 0x0007FBB5 File Offset: 0x0007DDB5
	private void Awake()
	{
		if (this.camTransform == null)
		{
			this.camTransform = base.GetComponent<Transform>();
		}
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x0007FBD1 File Offset: 0x0007DDD1
	private void OnEnable()
	{
		this.originalPos = this.camTransform.localPosition;
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x0007FBE4 File Offset: 0x0007DDE4
	private void Update()
	{
		if (this.shake > 0f)
		{
			this.camTransform.localPosition = this.originalPos + UnityEngine.Random.insideUnitSphere * this.shakeAmount;
			this.shake -= 0.016666668f * this.decreaseFactor;
			return;
		}
		this.shake = 0f;
		this.camTransform.localPosition = this.originalPos;
	}

	// Token: 0x0400153C RID: 5436
	public Transform camTransform;

	// Token: 0x0400153D RID: 5437
	public float shake;

	// Token: 0x0400153E RID: 5438
	public float shakeAmount = 0.7f;

	// Token: 0x0400153F RID: 5439
	public float decreaseFactor = 1f;

	// Token: 0x04001540 RID: 5440
	private Vector3 originalPos;
}
