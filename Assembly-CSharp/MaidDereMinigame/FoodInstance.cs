using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004F0 RID: 1264
	[RequireComponent(typeof(SpriteRenderer))]
	public class FoodInstance : MonoBehaviour
	{
		// Token: 0x06001FE1 RID: 8161 RVA: 0x00184C17 File Offset: 0x00182E17
		private void Start()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.spriteRenderer.sprite = this.food.smallSprite;
			this.heat = this.timeToCool;
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x00184C47 File Offset: 0x00182E47
		private void Update()
		{
			this.heat -= Time.deltaTime;
			this.warmthMeter.SetFill(this.heat / this.timeToCool);
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x00184C73 File Offset: 0x00182E73
		public void SetHeat(float newHeat)
		{
			this.heat = newHeat;
		}

		// Token: 0x04003D9C RID: 15772
		public Food food;

		// Token: 0x04003D9D RID: 15773
		public Meter warmthMeter;

		// Token: 0x04003D9E RID: 15774
		public float timeToCool = 30f;

		// Token: 0x04003D9F RID: 15775
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		// Token: 0x04003DA0 RID: 15776
		private float heat;
	}
}
