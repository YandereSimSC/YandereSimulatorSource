using System;
using UnityEngine;

// Token: 0x02000410 RID: 1040
public class SundayRivalCutsceneScript : MonoBehaviour
{
	// Token: 0x06001BF0 RID: 7152 RVA: 0x0014744F File Offset: 0x0014564F
	private void Start()
	{
		if (DateGlobals.Weekday != DayOfWeek.Sunday)
		{
			base.gameObject.SetActive(false);
		}
	}
}
