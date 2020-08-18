using System;
using UnityEngine;

// Token: 0x0200048C RID: 1164
public class YouTubeScript : MonoBehaviour
{
	// Token: 0x06001DF0 RID: 7664 RVA: 0x00176E6C File Offset: 0x0017506C
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			this.Strength += Time.deltaTime;
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(0f, 1.15f, 1f), Time.deltaTime * this.Strength);
		}
	}

	// Token: 0x04003BAB RID: 15275
	public float Strength;

	// Token: 0x04003BAC RID: 15276
	public bool Begin;
}
