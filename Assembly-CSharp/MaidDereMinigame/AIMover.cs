using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004E7 RID: 1255
	public abstract class AIMover : MonoBehaviour
	{
		// Token: 0x06001FB6 RID: 8118
		public abstract ControlInput GetInput();

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00183B34 File Offset: 0x00181D34
		private void FixedUpdate()
		{
			ControlInput input = this.GetInput();
			base.transform.Translate(new Vector2(input.horizontal, 0f) * Time.fixedDeltaTime * this.moveSpeed);
		}

		// Token: 0x04003D71 RID: 15729
		protected float moveSpeed = 3f;
	}
}
