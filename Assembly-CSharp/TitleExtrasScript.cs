using System;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class TitleExtrasScript : MonoBehaviour
{
	// Token: 0x06001C32 RID: 7218 RVA: 0x00150448 File Offset: 0x0014E648
	private void Start()
	{
		base.transform.localPosition = new Vector3(1050f, base.transform.localPosition.y, base.transform.localPosition.z);
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x00150480 File Offset: 0x0014E680
	private void Update()
	{
		if (!this.Show)
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 1050f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			return;
		}
		base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
	}

	// Token: 0x040034B7 RID: 13495
	public bool Show;
}
