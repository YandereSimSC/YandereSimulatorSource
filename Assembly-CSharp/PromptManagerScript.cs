using System;
using UnityEngine;

// Token: 0x0200037C RID: 892
public class PromptManagerScript : MonoBehaviour
{
	// Token: 0x0600194E RID: 6478 RVA: 0x000F3240 File Offset: 0x000F1440
	private void Update()
	{
		if (this.Yandere.transform.position.z < -38f)
		{
			if (!this.Outside)
			{
				this.Outside = true;
				foreach (PromptScript promptScript in this.Prompts)
				{
					if (promptScript != null)
					{
						promptScript.enabled = false;
					}
				}
				return;
			}
		}
		else if (this.Outside)
		{
			this.Outside = false;
			foreach (PromptScript promptScript2 in this.Prompts)
			{
				if (promptScript2 != null)
				{
					promptScript2.enabled = true;
				}
			}
		}
	}

	// Token: 0x0400268C RID: 9868
	public PromptScript[] Prompts;

	// Token: 0x0400268D RID: 9869
	public int ID;

	// Token: 0x0400268E RID: 9870
	public Transform Yandere;

	// Token: 0x0400268F RID: 9871
	public bool Outside;
}
