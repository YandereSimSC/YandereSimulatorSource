using System;
using UnityEngine;

// Token: 0x02000492 RID: 1170
public class BarScript : MonoBehaviour
{
	// Token: 0x06001E01 RID: 7681 RVA: 0x00178126 File Offset: 0x00176326
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 1f, 1f);
	}

	// Token: 0x06001E02 RID: 7682 RVA: 0x00178148 File Offset: 0x00176348
	private void Update()
	{
		base.transform.localScale = new Vector3(base.transform.localScale.x + this.Speed * Time.deltaTime, 1f, 1f);
		if ((double)base.transform.localScale.x > 0.1)
		{
			base.transform.localScale = new Vector3(0f, 1f, 1f);
		}
	}

	// Token: 0x04003BDB RID: 15323
	public float Speed;
}
