using System;
using UnityEngine;

// Token: 0x020002B6 RID: 694
public class GlobalKnifeArrayScript : MonoBehaviour
{
	// Token: 0x0600144A RID: 5194 RVA: 0x000B3B00 File Offset: 0x000B1D00
	public void ActivateKnives()
	{
		foreach (TimeStopKnifeScript timeStopKnifeScript in this.Knives)
		{
			if (timeStopKnifeScript != null)
			{
				timeStopKnifeScript.Unfreeze = true;
			}
		}
		this.ID = 0;
	}

	// Token: 0x04001CF0 RID: 7408
	public TimeStopKnifeScript[] Knives;

	// Token: 0x04001CF1 RID: 7409
	public int ID;
}
