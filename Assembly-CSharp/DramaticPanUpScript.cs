using System;
using UnityEngine;

// Token: 0x02000271 RID: 625
public class DramaticPanUpScript : MonoBehaviour
{
	// Token: 0x06001363 RID: 4963 RVA: 0x000A67BC File Offset: 0x000A49BC
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Pan = true;
		}
		if (this.Pan)
		{
			this.Power += Time.deltaTime * 0.5f;
			this.Height = Mathf.Lerp(this.Height, 1.4f, this.Power * Time.deltaTime);
			base.transform.localPosition = new Vector3(0f, this.Height, 1f);
		}
	}

	// Token: 0x04001A6D RID: 6765
	public bool Pan;

	// Token: 0x04001A6E RID: 6766
	public float Height;

	// Token: 0x04001A6F RID: 6767
	public float Power;
}
