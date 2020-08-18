using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200050B RID: 1291
	public class TipPage : MonoBehaviour
	{
		// Token: 0x0600204F RID: 8271 RVA: 0x001860BC File Offset: 0x001842BC
		public void Init()
		{
			this.cards = new List<TipCard>();
			foreach (object obj in base.transform.GetChild(0))
			{
				foreach (object obj2 in ((Transform)obj))
				{
					Transform transform = (Transform)obj2;
					this.cards.Add(transform.GetComponent<TipCard>());
				}
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00186178 File Offset: 0x00184378
		public void DisplayTips(List<float> tips)
		{
			if (tips == null)
			{
				tips = new List<float>();
			}
			base.gameObject.SetActive(true);
			float num = 0f;
			for (int i = 0; i < this.cards.Count; i++)
			{
				if (tips.Count > i)
				{
					this.cards[i].SetTip(tips[i]);
					num += tips[i];
				}
				else
				{
					this.cards[i].SetTip(0f);
				}
			}
			float basePay = GameController.Instance.activeDifficultyVariables.basePay;
			GameController.Instance.totalPayout = num + basePay;
			this.wageCard.SetTip(basePay);
			this.totalCard.SetTip(num + basePay);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00186231 File Offset: 0x00184431
		private void Update()
		{
			if (this.stopInteraction)
			{
				return;
			}
			if (Input.GetButtonDown("A"))
			{
				GameController.GoToExitScene(true);
				this.stopInteraction = true;
			}
		}

		// Token: 0x04003E02 RID: 15874
		public TipCard wageCard;

		// Token: 0x04003E03 RID: 15875
		public TipCard totalCard;

		// Token: 0x04003E04 RID: 15876
		private List<TipCard> cards;

		// Token: 0x04003E05 RID: 15877
		private bool stopInteraction;
	}
}
