using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000503 RID: 1283
	public class ExitMaidMinigame : MonoBehaviour
	{
		// Token: 0x0600202B RID: 8235 RVA: 0x0018591C File Offset: 0x00183B1C
		private void OnMouseOver()
		{
			if (Input.GetMouseButtonDown(0))
			{
				GameController.GoToExitScene(true);
			}
		}
	}
}
