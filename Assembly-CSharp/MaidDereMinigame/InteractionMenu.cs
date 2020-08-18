using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000506 RID: 1286
	public class InteractionMenu : MonoBehaviour
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x00185CB4 File Offset: 0x00183EB4
		public static InteractionMenu Instance
		{
			get
			{
				if (InteractionMenu.instance == null)
				{
					InteractionMenu.instance = UnityEngine.Object.FindObjectOfType<InteractionMenu>();
				}
				return InteractionMenu.instance;
			}
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00185CD2 File Offset: 0x00183ED2
		private void Awake()
		{
			InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
			InteractionMenu.SetBButton(false);
			InteractionMenu.SetADButton(true);
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x00185CE8 File Offset: 0x00183EE8
		public static void SetAButton(InteractionMenu.AButtonText text)
		{
			for (int i = 0; i < InteractionMenu.Instance.aButtonSprites.Length; i++)
			{
				if (i == (int)text)
				{
					InteractionMenu.Instance.aButtonSprites[i].gameObject.SetActive(true);
				}
				else
				{
					InteractionMenu.Instance.aButtonSprites[i].gameObject.SetActive(false);
				}
			}
			SpriteRenderer[] array = InteractionMenu.Instance.aButtons;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].gameObject.SetActive(text != InteractionMenu.AButtonText.None);
			}
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x00185D70 File Offset: 0x00183F70
		public static void SetBButton(bool on)
		{
			SpriteRenderer[] array = InteractionMenu.Instance.backButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(on);
			}
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x00185DA4 File Offset: 0x00183FA4
		public static void SetADButton(bool on)
		{
			SpriteRenderer[] array = InteractionMenu.Instance.moveButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(on);
			}
		}

		// Token: 0x04003DF2 RID: 15858
		private static InteractionMenu instance;

		// Token: 0x04003DF3 RID: 15859
		public GameObject interactObject;

		// Token: 0x04003DF4 RID: 15860
		public GameObject backObject;

		// Token: 0x04003DF5 RID: 15861
		public GameObject moveObject;

		// Token: 0x04003DF6 RID: 15862
		public SpriteRenderer[] aButtons;

		// Token: 0x04003DF7 RID: 15863
		public SpriteRenderer[] aButtonSprites;

		// Token: 0x04003DF8 RID: 15864
		public SpriteRenderer[] backButtons;

		// Token: 0x04003DF9 RID: 15865
		public SpriteRenderer[] moveButtons;

		// Token: 0x02000708 RID: 1800
		public enum AButtonText
		{
			// Token: 0x040048A9 RID: 18601
			ChoosePlate,
			// Token: 0x040048AA RID: 18602
			GrabPlate,
			// Token: 0x040048AB RID: 18603
			KitchenMenu,
			// Token: 0x040048AC RID: 18604
			PlaceOrder,
			// Token: 0x040048AD RID: 18605
			TakeOrder,
			// Token: 0x040048AE RID: 18606
			TossPlate,
			// Token: 0x040048AF RID: 18607
			GiveFood,
			// Token: 0x040048B0 RID: 18608
			None
		}
	}
}
