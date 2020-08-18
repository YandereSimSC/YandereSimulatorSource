using System;
using UnityEngine;

// Token: 0x0200026D RID: 621
public class DoorBoxScript : MonoBehaviour
{
	// Token: 0x06001350 RID: 4944 RVA: 0x000A455C File Offset: 0x000A275C
	private void Update()
	{
		float y = Mathf.Lerp(base.transform.localPosition.y, this.Show ? -530f : -630f, Time.deltaTime * 10f);
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, y, base.transform.localPosition.z);
	}

	// Token: 0x04001A3B RID: 6715
	public UILabel Label;

	// Token: 0x04001A3C RID: 6716
	public bool Show;
}
