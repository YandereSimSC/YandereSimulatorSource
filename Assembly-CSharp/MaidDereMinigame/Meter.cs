using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000507 RID: 1287
	public class Meter : MonoBehaviour
	{
		// Token: 0x06002043 RID: 8259 RVA: 0x00185DD8 File Offset: 0x00183FD8
		private void Awake()
		{
			this.startPos = this.fillBar.transform.localPosition.x;
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00185DF8 File Offset: 0x00183FF8
		public void SetFill(float interpolater)
		{
			float num = Mathf.Lerp(this.emptyPos, this.startPos, interpolater);
			num = Mathf.Round(num * 50f) / 50f;
			this.fillBar.transform.localPosition = new Vector3(num, 0f, 0f);
		}

		// Token: 0x04003DFA RID: 15866
		public SpriteRenderer fillBar;

		// Token: 0x04003DFB RID: 15867
		public float emptyPos;

		// Token: 0x04003DFC RID: 15868
		private float startPos;
	}
}
