using System;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class DumpScript : MonoBehaviour
{
	// Token: 0x0600136F RID: 4975 RVA: 0x000A6F28 File Offset: 0x000A5128
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 5f)
		{
			this.Incinerator.Corpses++;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001A8E RID: 6798
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04001A8F RID: 6799
	public IncineratorScript Incinerator;

	// Token: 0x04001A90 RID: 6800
	public float Timer;
}
