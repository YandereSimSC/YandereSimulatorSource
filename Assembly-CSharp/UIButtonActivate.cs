using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
	// Token: 0x0600016C RID: 364 RVA: 0x00014465 File Offset: 0x00012665
	private void OnClick()
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, this.state);
		}
	}

	// Token: 0x04000304 RID: 772
	public GameObject target;

	// Token: 0x04000305 RID: 773
	public bool state = true;
}
