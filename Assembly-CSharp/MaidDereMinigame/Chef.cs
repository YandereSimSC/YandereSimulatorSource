using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004E3 RID: 1251
	[RequireComponent(typeof(Animator))]
	public class Chef : MonoBehaviour
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x00182ECC File Offset: 0x001810CC
		public static Chef Instance
		{
			get
			{
				if (Chef.instance == null)
				{
					Chef.instance = UnityEngine.Object.FindObjectOfType<Chef>();
				}
				return Chef.instance;
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00182EEA File Offset: 0x001810EA
		private void Awake()
		{
			this.cookQueue = new Foods();
			this.animator = base.GetComponent<Animator>();
			this.cookMeter.gameObject.SetActive(false);
			this.isPaused = true;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00182F1B File Offset: 0x0018111B
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x00182F3D File Offset: 0x0018113D
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00182F5F File Offset: 0x0018115F
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
			this.animator.speed = (float)(this.isPaused ? 0 : 1);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x00182F80 File Offset: 0x00181180
		public static void AddToQueue(Food foodItem)
		{
			Chef.Instance.cookQueue.Add(foodItem);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x00182F92 File Offset: 0x00181192
		public static Food GrabFromQueue()
		{
			Food result = Chef.Instance.cookQueue[0];
			Chef.Instance.cookQueue.RemoveAt(0);
			return result;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x00182FB4 File Offset: 0x001811B4
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			Chef.ChefState chefState = this.state;
			if (chefState != Chef.ChefState.Queueing)
			{
				if (chefState != Chef.ChefState.Cooking)
				{
					return;
				}
				if (this.timeToFinishDish <= 0f)
				{
					this.state = Chef.ChefState.Delivering;
					this.animator.SetTrigger("PlateCooked");
					this.cookMeter.gameObject.SetActive(false);
					return;
				}
				this.timeToFinishDish -= Time.deltaTime;
				this.cookMeter.SetFill(1f - this.timeToFinishDish / (this.currentPlate.cookTimeMultiplier * this.cookTime));
			}
			else if (this.cookQueue.Count > 0)
			{
				this.currentPlate = Chef.GrabFromQueue();
				this.timeToFinishDish = this.currentPlate.cookTimeMultiplier * this.cookTime;
				this.state = Chef.ChefState.Cooking;
				this.cookMeter.gameObject.SetActive(true);
				return;
			}
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x00183098 File Offset: 0x00181298
		public void Deliver()
		{
			UnityEngine.Object.FindObjectOfType<ServingCounter>().AddPlate(this.currentPlate);
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x001830AA File Offset: 0x001812AA
		public void Queue()
		{
			this.state = Chef.ChefState.Queueing;
		}

		// Token: 0x04003D4B RID: 15691
		private static Chef instance;

		// Token: 0x04003D4C RID: 15692
		[Reorderable]
		public Foods cookQueue;

		// Token: 0x04003D4D RID: 15693
		public FoodMenu foodMenu;

		// Token: 0x04003D4E RID: 15694
		public Meter cookMeter;

		// Token: 0x04003D4F RID: 15695
		public float cookTime = 3f;

		// Token: 0x04003D50 RID: 15696
		private Chef.ChefState state;

		// Token: 0x04003D51 RID: 15697
		private Food currentPlate;

		// Token: 0x04003D52 RID: 15698
		private Animator animator;

		// Token: 0x04003D53 RID: 15699
		private float timeToFinishDish;

		// Token: 0x04003D54 RID: 15700
		private bool isPaused;

		// Token: 0x020006FB RID: 1787
		public enum ChefState
		{
			// Token: 0x04004860 RID: 18528
			Queueing,
			// Token: 0x04004861 RID: 18529
			Cooking,
			// Token: 0x04004862 RID: 18530
			Delivering
		}
	}
}
