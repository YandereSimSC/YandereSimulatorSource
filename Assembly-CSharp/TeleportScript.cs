using System;
using UnityEngine;

// Token: 0x0200041B RID: 1051
public class TeleportScript : MonoBehaviour
{
	// Token: 0x06001C1E RID: 7198 RVA: 0x0014F925 File Offset: 0x0014DB25
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.transform.position = this.Destination.position;
			Physics.SyncTransforms();
		}
	}

	// Token: 0x04003486 RID: 13446
	public PromptScript Prompt;

	// Token: 0x04003487 RID: 13447
	public Transform Destination;
}
