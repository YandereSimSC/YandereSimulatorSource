using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class BlasterScript : MonoBehaviour
{
	// Token: 0x06000A47 RID: 2631 RVA: 0x000548BA File Offset: 0x00052ABA
	private void Start()
	{
		this.Skull.localScale = Vector3.zero;
		this.Beam.localScale = Vector3.zero;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x000548DC File Offset: 0x00052ADC
	private void Update()
	{
		AnimationState animationState = base.GetComponent<Animation>()["Blast"];
		if (animationState.time > 1f)
		{
			this.Beam.localScale = Vector3.Lerp(this.Beam.localScale, new Vector3(15f, 1f, 1f), Time.deltaTime * 10f);
			this.Eyes.material.color = new Color(1f, 0f, 0f, 1f);
		}
		if (animationState.time >= animationState.length)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x00054984 File Offset: 0x00052B84
	private void LateUpdate()
	{
		AnimationState animationState = base.GetComponent<Animation>()["Blast"];
		this.Size = ((animationState.time < 1.5f) ? Mathf.Lerp(this.Size, 2f, Time.deltaTime * 5f) : Mathf.Lerp(this.Size, 0f, Time.deltaTime * 10f));
		this.Skull.localScale = new Vector3(this.Size, this.Size, this.Size);
	}

	// Token: 0x04000A83 RID: 2691
	public Transform Skull;

	// Token: 0x04000A84 RID: 2692
	public Renderer Eyes;

	// Token: 0x04000A85 RID: 2693
	public Transform Beam;

	// Token: 0x04000A86 RID: 2694
	public float Size;
}
