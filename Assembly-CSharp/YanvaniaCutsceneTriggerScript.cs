using System;
using UnityEngine;

// Token: 0x0200047A RID: 1146
public class YanvaniaCutsceneTriggerScript : MonoBehaviour
{
	// Token: 0x06001DAB RID: 7595 RVA: 0x001709FC File Offset: 0x0016EBFC
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YanmontChan")
		{
			this.BossBattleWall.SetActive(true);
			this.Yanmont.EnterCutscene = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003A93 RID: 14995
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003A94 RID: 14996
	public GameObject BossBattleWall;
}
