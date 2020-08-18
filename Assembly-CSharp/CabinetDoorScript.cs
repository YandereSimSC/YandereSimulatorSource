using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class CabinetDoorScript : MonoBehaviour
{
	// Token: 0x06000A9E RID: 2718 RVA: 0x00057D98 File Offset: 0x00055F98
	private void Update()
	{
		if (this.Timer < 2f)
		{
			this.Timer += Time.deltaTime;
			if (this.Open)
			{
				base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0.41775f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
				return;
			}
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		}
	}

	// Token: 0x04000B38 RID: 2872
	public PromptScript Prompt;

	// Token: 0x04000B39 RID: 2873
	public bool Locked;

	// Token: 0x04000B3A RID: 2874
	public bool Open;

	// Token: 0x04000B3B RID: 2875
	public float Timer;
}
