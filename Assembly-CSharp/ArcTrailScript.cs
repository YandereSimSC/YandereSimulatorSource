using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class ArcTrailScript : MonoBehaviour
{
	// Token: 0x06000A12 RID: 2578 RVA: 0x0004FC35 File Offset: 0x0004DE35
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Trail.material.SetColor("_TintColor", ArcTrailScript.TRAIL_TINT_COLOR);
		}
	}

	// Token: 0x04000A13 RID: 2579
	private static readonly Color TRAIL_TINT_COLOR = new Color(1f, 0f, 0f, 1f);

	// Token: 0x04000A14 RID: 2580
	public TrailRenderer Trail;
}
