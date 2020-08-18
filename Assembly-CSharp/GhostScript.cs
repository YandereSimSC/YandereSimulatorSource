using System;
using UnityEngine;

// Token: 0x020002B4 RID: 692
public class GhostScript : MonoBehaviour
{
	// Token: 0x06001443 RID: 5187 RVA: 0x000B3238 File Offset: 0x000B1438
	private void Update()
	{
		if (Time.timeScale > 0.0001f)
		{
			if (this.Frame > 0)
			{
				base.GetComponent<Animation>().enabled = false;
				base.gameObject.SetActive(false);
				this.Frame = 0;
			}
			this.Frame++;
		}
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x000B3287 File Offset: 0x000B1487
	public void Look()
	{
		this.Neck.LookAt(this.SmartphoneCamera.position);
	}

	// Token: 0x04001CE4 RID: 7396
	public Transform SmartphoneCamera;

	// Token: 0x04001CE5 RID: 7397
	public Transform Neck;

	// Token: 0x04001CE6 RID: 7398
	public Transform GhostEyeLocation;

	// Token: 0x04001CE7 RID: 7399
	public Transform GhostEye;

	// Token: 0x04001CE8 RID: 7400
	public int Frame;

	// Token: 0x04001CE9 RID: 7401
	public bool Move;
}
