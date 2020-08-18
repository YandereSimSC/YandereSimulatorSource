using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004F1 RID: 1265
	[RequireComponent(typeof(Camera))]
	public class CameraForcedAspect : MonoBehaviour
	{
		// Token: 0x06001FE5 RID: 8165 RVA: 0x00184C8F File Offset: 0x00182E8F
		private void Awake()
		{
			this.cam = base.GetComponent<Camera>();
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x00184CA0 File Offset: 0x00182EA0
		private void Start()
		{
			float num = this.targetAspect.x / this.targetAspect.y;
			float num2 = (float)Screen.width / (float)Screen.height / num;
			if (num2 < 1f)
			{
				Rect rect = this.cam.rect;
				rect.width = 1f;
				rect.height = num2;
				rect.x = 0f;
				rect.y = (1f - num2) / 2f;
				this.cam.rect = rect;
				return;
			}
			Rect rect2 = this.cam.rect;
			float num3 = 1f / num2;
			rect2.width = num3;
			rect2.height = 1f;
			rect2.x = (1f - num3) / 2f;
			rect2.y = 0f;
			this.cam.rect = rect2;
		}

		// Token: 0x04003DA1 RID: 15777
		public Vector2 targetAspect = new Vector2(16f, 9f);

		// Token: 0x04003DA2 RID: 15778
		private Camera cam;
	}
}
