using System;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class PhoneCharmScript : MonoBehaviour
{
	// Token: 0x060018BB RID: 6331 RVA: 0x000C5719 File Offset: 0x000C3919
	private void Update()
	{
		base.transform.eulerAngles = new Vector3(90f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}
}
