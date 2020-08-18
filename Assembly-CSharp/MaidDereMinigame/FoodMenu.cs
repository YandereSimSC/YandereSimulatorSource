using System;
using System.Collections.Generic;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000505 RID: 1285
	public class FoodMenu : MonoBehaviour
	{
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x001859F4 File Offset: 0x00183BF4
		public static FoodMenu Instance
		{
			get
			{
				if (FoodMenu.instance == null)
				{
					FoodMenu.instance = UnityEngine.Object.FindObjectOfType<FoodMenu>();
				}
				return FoodMenu.instance;
			}
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00185A14 File Offset: 0x00183C14
		private void Awake()
		{
			this.SetMenuIcons();
			this.menuSelectorTarget = this.menuSlots[0].position.x;
			this.startY = this.menuSelector.position.y;
			this.startZ = this.menuSelector.position.z;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00185A70 File Offset: 0x00183C70
		public void SetMenuIcons()
		{
			this.menuSlots = new List<Transform>();
			for (int i = 0; i < this.menuSlotParent.childCount; i++)
			{
				Transform child = this.menuSlotParent.GetChild(i);
				this.menuSlots.Add(child);
				if (this.foodItems.Count >= i)
				{
					child.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.foodItems[i].largeSprite;
				}
			}
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00185AE7 File Offset: 0x00183CE7
		public void SetActive(int index)
		{
			this.menuSelectorTarget = this.menuSlots[index].position.x;
			this.interpolator = 0f;
			this.activeIndex = index;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00185B17 File Offset: 0x00183D17
		public Food GetActiveFood()
		{
			Food food = UnityEngine.Object.Instantiate<Food>(this.foodItems[this.activeIndex]);
			food.name = this.foodItems[this.activeIndex].name;
			return food;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00185B4C File Offset: 0x00183D4C
		public Food GetRandomFood()
		{
			int index = UnityEngine.Random.Range(0, this.foodItems.Count);
			Food food = UnityEngine.Object.Instantiate<Food>(this.foodItems[index]);
			food.name = this.foodItems[index].name;
			return food;
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00185B94 File Offset: 0x00183D94
		private void Update()
		{
			if (this.interpolator < 1f)
			{
				float x = Mathf.Lerp(this.menuSelector.position.x, this.menuSelectorTarget, this.interpolator);
				this.menuSelector.position = new Vector3(x, this.startY, this.startZ);
				this.interpolator += Time.deltaTime * this.selectorMoveSpeed;
			}
			else
			{
				this.menuSelector.transform.position = new Vector3(this.menuSelectorTarget, this.startY, this.startZ);
			}
			if (YandereController.rightButton)
			{
				this.IncrementSelection();
				return;
			}
			if (YandereController.leftButton)
			{
				this.DecrementSelection();
			}
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00185C4A File Offset: 0x00183E4A
		private void IncrementSelection()
		{
			this.SetActive((this.activeIndex + 1) % this.menuSlots.Count);
			SFXController.PlaySound(SFXController.Sounds.MenuSelect);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00185C6D File Offset: 0x00183E6D
		private void DecrementSelection()
		{
			if (this.activeIndex == 0)
			{
				this.SetActive(this.menuSlots.Count - 1);
			}
			else
			{
				this.SetActive(this.activeIndex - 1);
			}
			SFXController.PlaySound(SFXController.Sounds.MenuSelect);
		}

		// Token: 0x04003DE7 RID: 15847
		private static FoodMenu instance;

		// Token: 0x04003DE8 RID: 15848
		[Reorderable]
		public Foods foodItems;

		// Token: 0x04003DE9 RID: 15849
		public Transform menuSelector;

		// Token: 0x04003DEA RID: 15850
		public Transform menuSlotParent;

		// Token: 0x04003DEB RID: 15851
		public float selectorMoveSpeed = 3f;

		// Token: 0x04003DEC RID: 15852
		private List<Transform> menuSlots;

		// Token: 0x04003DED RID: 15853
		private float menuSelectorTarget;

		// Token: 0x04003DEE RID: 15854
		private float startY;

		// Token: 0x04003DEF RID: 15855
		private float startZ;

		// Token: 0x04003DF0 RID: 15856
		private float interpolator;

		// Token: 0x04003DF1 RID: 15857
		private int activeIndex;
	}
}
