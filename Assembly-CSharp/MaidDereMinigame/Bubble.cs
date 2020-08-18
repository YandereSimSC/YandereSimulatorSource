using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004ED RID: 1261
	public class Bubble : MonoBehaviour
	{
		// Token: 0x06001FD7 RID: 8151 RVA: 0x00184B2E File Offset: 0x00182D2E
		private void Awake()
		{
			this.foodRenderer.sprite = null;
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00184B3C File Offset: 0x00182D3C
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00184B5E File Offset: 0x00182D5E
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00184B80 File Offset: 0x00182D80
		public void Pause(bool toPause)
		{
			if (toPause)
			{
				base.GetComponent<SpriteRenderer>().enabled = false;
				this.foodRenderer.gameObject.SetActive(false);
				return;
			}
			base.GetComponent<SpriteRenderer>().enabled = true;
			this.foodRenderer.gameObject.SetActive(true);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00184BC0 File Offset: 0x00182DC0
		public void BubbleReachedMax()
		{
			this.foodRenderer.gameObject.SetActive(true);
			this.foodRenderer.sprite = this.food.largeSprite;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x00184BE9 File Offset: 0x00182DE9
		public void BubbleClosing()
		{
			this.foodRenderer.gameObject.SetActive(false);
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x00177C43 File Offset: 0x00175E43
		public void KillBubble()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04003D97 RID: 15767
		[HideInInspector]
		public Food food;

		// Token: 0x04003D98 RID: 15768
		public SpriteRenderer foodRenderer;
	}
}
