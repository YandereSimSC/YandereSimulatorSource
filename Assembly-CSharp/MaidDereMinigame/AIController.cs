using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004E6 RID: 1254
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Collider2D))]
	public class AIController : AIMover
	{
		// Token: 0x06001FA7 RID: 8103 RVA: 0x00183360 File Offset: 0x00181560
		public void Init()
		{
			this.animator = base.GetComponent<Animator>();
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.throbObject.SetActive(false);
			this.targetChair = Chair.RandomChair;
			this.collider2d = base.GetComponent<Collider2D>();
			this.collider2d.enabled = false;
			if (this.targetChair == null)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			this.happinessMeter.gameObject.SetActive(false);
			this.speechBubble.gameObject.SetActive(false);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x001833F0 File Offset: 0x001815F0
		private void Start()
		{
			this.leaveTarget.GetComponent<CustomerSpawner>().OpenDoor();
			this.moveSpeed = GameController.Instance.activeDifficultyVariables.customerMoveSpeed;
			this.timeToOrder = GameController.Instance.activeDifficultyVariables.timeSpentOrdering;
			this.eatTime = GameController.Instance.activeDifficultyVariables.timeSpentEatingFood;
			this.patienceDegradation = GameController.Instance.activeDifficultyVariables.customerPatienceDegradation;
			this.timeToEat = GameController.Instance.activeDifficultyVariables.timeSpentEatingFood;
			SFXController.PlaySound(SFXController.Sounds.DoorBell);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0018347C File Offset: 0x0018167C
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0018349E File Offset: 0x0018169E
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x001834C0 File Offset: 0x001816C0
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
			base.GetComponent<Animator>().speed = (float)(this.isPaused ? 0 : 1);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x001834E4 File Offset: 0x001816E4
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			switch (this.state)
			{
			case AIController.AIState.Entering:
				if (Mathf.Abs(base.transform.position.x - this.targetChair.transform.position.x) <= this.distanceThreshold)
				{
					this.SitDown();
					this.happiness = 100f;
					this.happinessMeter.SetFill(this.happiness / 100f);
					this.state = AIController.AIState.Menu;
					return;
				}
				break;
			case AIController.AIState.Menu:
				if (this.happiness <= 0f)
				{
					this.StandUp();
					this.state = AIController.AIState.Leaving;
					GameController.AddAngryCustomer();
					return;
				}
				this.ReduceHappiness();
				return;
			case AIController.AIState.Ordering:
				if (this.orderTime <= 0f)
				{
					this.state = AIController.AIState.Waiting;
					this.speechBubble.GetComponent<Animator>().SetTrigger("BubbleDrop");
					this.animator.SetTrigger("DoneOrdering");
					return;
				}
				this.orderTime -= Time.deltaTime;
				return;
			case AIController.AIState.Waiting:
				if (this.happiness <= 0f)
				{
					this.StandUp();
					this.state = AIController.AIState.Leaving;
					GameController.AddAngryCustomer();
					return;
				}
				this.ReduceHappiness();
				return;
			case AIController.AIState.Eating:
				if (this.eatTime <= 0f)
				{
					this.StandUp();
					this.state = AIController.AIState.Leaving;
					return;
				}
				this.eatTime -= Time.deltaTime;
				return;
			case AIController.AIState.Leaving:
				if (Mathf.Abs(base.transform.position.x - this.leaveTarget.position.x) <= this.distanceThreshold)
				{
					UnityEngine.Object.Destroy(base.gameObject);
					this.leaveTarget.GetComponent<CustomerSpawner>().OpenDoor();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00183698 File Offset: 0x00181898
		public override ControlInput GetInput()
		{
			ControlInput result = default(ControlInput);
			if (this.isPaused)
			{
				return result;
			}
			AIController.AIState aistate = this.state;
			if (aistate != AIController.AIState.Entering)
			{
				if (aistate == AIController.AIState.Leaving)
				{
					if (this.leaveTarget.position.x > base.transform.position.x)
					{
						result.horizontal = 1f;
						this.SetFlip(false);
					}
					else
					{
						result.horizontal = -1f;
						this.SetFlip(true);
					}
				}
			}
			else if (this.targetChair.transform.position.x > base.transform.position.x)
			{
				result.horizontal = 1f;
				this.SetFlip(false);
			}
			else
			{
				result.horizontal = -1f;
				this.SetFlip(true);
			}
			return result;
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00183768 File Offset: 0x00181968
		public void TakeOrder()
		{
			this.state = AIController.AIState.Ordering;
			this.happiness = 100f;
			this.happinessMeter.SetFill(this.happiness / 100f);
			this.orderTime = this.timeToOrder;
			this.animator.SetTrigger("OrderTaken");
			this.animator.SetFloat("Happiness", this.happiness);
			this.desiredFood = FoodMenu.Instance.GetRandomFood();
			this.speechBubble.gameObject.SetActive(true);
			this.speechBubble.food = this.desiredFood;
			if (this.Male)
			{
				SFXController.PlaySound(SFXController.Sounds.MaleCustomerGreet);
				return;
			}
			SFXController.PlaySound(SFXController.Sounds.FemaleCustomerGreet);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0018381C File Offset: 0x00181A1C
		public void DeliverFood(Food deliveredFood)
		{
			if (deliveredFood.name == this.desiredFood.name)
			{
				this.state = AIController.AIState.Eating;
				this.animator.SetTrigger("ServedFood");
				this.eatTime = this.timeToEat;
				GameController.AddTip(GameController.Instance.activeDifficultyVariables.baseTip * this.happiness);
				if (this.happiness <= 50f)
				{
					this.happiness = 50f;
					this.animator.SetFloat("Happiness", this.happiness);
				}
				if (this.Male)
				{
					SFXController.PlaySound(SFXController.Sounds.MaleCustomerThank);
				}
				else
				{
					SFXController.PlaySound(SFXController.Sounds.FemaleCustomerThank);
				}
			}
			else
			{
				this.state = AIController.AIState.Leaving;
				this.happiness = 0f;
				this.animator.SetFloat("Happiness", this.happiness);
				GameController.AddAngryCustomer();
				this.StandUp();
				if (this.Male)
				{
					SFXController.PlaySound(SFXController.Sounds.MaleCustomerLeave);
				}
				else
				{
					SFXController.PlaySound(SFXController.Sounds.FemaleCustomerLeave);
				}
			}
			this.happinessMeter.gameObject.SetActive(false);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00183928 File Offset: 0x00181B28
		private void SitDown()
		{
			base.transform.position = new Vector3(this.targetChair.transform.position.x, base.transform.position.y, base.transform.position.z);
			this.animator.SetTrigger("SitDown");
			this.SetFlip(this.targetChair.transform.localScale.x <= 0f);
			this.SetSortingLayer(true);
			this.collider2d.enabled = true;
			this.happinessMeter.gameObject.SetActive(true);
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x001839D4 File Offset: 0x00181BD4
		private void StandUp()
		{
			this.animator.SetTrigger("StandUp");
			this.SetSortingLayer(false);
			this.targetChair.available = true;
			this.collider2d.enabled = false;
			this.happinessMeter.gameObject.SetActive(false);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00183A24 File Offset: 0x00181C24
		private void ReduceHappiness()
		{
			this.happiness -= Time.deltaTime * this.patienceDegradation;
			this.animator.SetFloat("Happiness", this.happiness);
			this.happinessMeter.SetFill(this.happiness / 100f);
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00183A77 File Offset: 0x00181C77
		private void SetFlip(bool flip)
		{
			this.spriteRenderer.flipX = flip;
			base.GetComponentInChildren<CharacterHairPlacer>().hairInstance.flipX = flip;
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00183A98 File Offset: 0x00181C98
		public void SetSortingLayer(bool back)
		{
			this.spriteRenderer.sortingLayerName = (back ? "CustomerSitting" : "Default");
			base.GetComponent<CharacterHairPlacer>().hairInstance.sortingLayerName = (back ? "CustomerSitting" : "Default");
			this.throbObject.GetComponent<SpriteRenderer>().sortingLayerName = (back ? "CustomerSitting" : "Default");
		}

		// Token: 0x04003D5D RID: 15709
		public GameObject throbObject;

		// Token: 0x04003D5E RID: 15710
		public Meter happinessMeter;

		// Token: 0x04003D5F RID: 15711
		public Bubble speechBubble;

		// Token: 0x04003D60 RID: 15712
		public float distanceThreshold = 0.5f;

		// Token: 0x04003D61 RID: 15713
		private Food desiredFood;

		// Token: 0x04003D62 RID: 15714
		private Collider2D collider2d;

		// Token: 0x04003D63 RID: 15715
		private Chair targetChair;

		// Token: 0x04003D64 RID: 15716
		[HideInInspector]
		public Transform leaveTarget;

		// Token: 0x04003D65 RID: 15717
		[HideInInspector]
		public AIController.AIState state;

		// Token: 0x04003D66 RID: 15718
		private Animator animator;

		// Token: 0x04003D67 RID: 15719
		private SpriteRenderer spriteRenderer;

		// Token: 0x04003D68 RID: 15720
		private float patienceDegradation = 2f;

		// Token: 0x04003D69 RID: 15721
		private float timeToOrder = 0.5f;

		// Token: 0x04003D6A RID: 15722
		private float timeToEat;

		// Token: 0x04003D6B RID: 15723
		private float happiness = 50f;

		// Token: 0x04003D6C RID: 15724
		private float orderTime;

		// Token: 0x04003D6D RID: 15725
		private float eatTime;

		// Token: 0x04003D6E RID: 15726
		private int normalSortingLayer;

		// Token: 0x04003D6F RID: 15727
		private bool isPaused;

		// Token: 0x04003D70 RID: 15728
		public bool Male;

		// Token: 0x020006FC RID: 1788
		public enum AIState
		{
			// Token: 0x04004864 RID: 18532
			Entering,
			// Token: 0x04004865 RID: 18533
			Menu,
			// Token: 0x04004866 RID: 18534
			Ordering,
			// Token: 0x04004867 RID: 18535
			Waiting,
			// Token: 0x04004868 RID: 18536
			Eating,
			// Token: 0x04004869 RID: 18537
			Leaving
		}
	}
}
