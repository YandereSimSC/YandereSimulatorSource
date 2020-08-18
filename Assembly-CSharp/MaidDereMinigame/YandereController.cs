using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004E9 RID: 1257
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Animator))]
	public class YandereController : AIMover
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x00183B90 File Offset: 0x00181D90
		public static YandereController Instance
		{
			get
			{
				if (YandereController.instance == null)
				{
					YandereController.instance = UnityEngine.Object.FindObjectOfType<YandereController>();
				}
				return YandereController.instance;
			}
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00183BB0 File Offset: 0x00181DB0
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.animator = base.GetComponent<Animator>();
			this.plateTransform.gameObject.SetActive(false);
			this.moveSpeed = GameController.Instance.activeDifficultyVariables.playerMoveSpeed;
			this.isPaused = true;
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00183C02 File Offset: 0x00181E02
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00183C24 File Offset: 0x00181E24
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00183C46 File Offset: 0x00181E46
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
			if (this.isPaused)
			{
				this.animator.SetBool("Moving", false);
			}
			this.animator.speed = (float)(this.isPaused ? 0 : 1);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00183C80 File Offset: 0x00181E80
		private void Update()
		{
			YandereController.rightButton = false;
			YandereController.leftButton = false;
			if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetKey("right") || Input.GetAxis("DpadX") > 0.5f)
			{
				if (!this.rightButtonPast)
				{
					this.rightButtonPast = true;
					YandereController.rightButton = true;
				}
			}
			else if (Input.GetAxisRaw("Horizontal") < 0f || Input.GetKey("left") || Input.GetAxis("DpadX") < -0.5f)
			{
				if (!this.leftButtonPast)
				{
					this.leftButtonPast = true;
					YandereController.leftButton = true;
				}
			}
			else
			{
				this.leftButtonPast = false;
				this.rightButtonPast = false;
			}
			if (base.transform.position.x < this.leftBounds.position.x)
			{
				base.transform.position = new Vector3(this.leftBounds.position.x, base.transform.position.y, base.transform.position.z);
			}
			if (base.transform.position.x > this.rightBounds.position.x)
			{
				base.transform.position = new Vector3(this.rightBounds.position.x, base.transform.position.y, base.transform.position.z);
			}
			if (Input.GetButtonDown("A") && this.aiTarget != null)
			{
				if (this.aiTarget.state == AIController.AIState.Menu)
				{
					this.aiTarget.TakeOrder();
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
				}
				else if (this.aiTarget.state == AIController.AIState.Waiting && this.heldItem != null)
				{
					this.aiTarget.DeliverFood(this.heldItem);
					SFXController.PlaySound(SFXController.Sounds.Plate);
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
					this.DropTray();
				}
			}
			if (this.aiTarget != null)
			{
				this.interactionIndicator.gameObject.SetActive(true);
				this.interactionIndicator.position = new Vector3(this.aiTarget.transform.position.x, this.aiTarget.transform.position.y + 0.6f, this.aiTarget.transform.position.z);
				return;
			}
			this.interactionIndicator.gameObject.SetActive(false);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00183EF8 File Offset: 0x001820F8
		public override ControlInput GetInput()
		{
			if (this.isPaused)
			{
				this.animator.SetBool("Moving", false);
				return default(ControlInput);
			}
			float horizontal = 0f;
			if (this.rightButtonPast)
			{
				horizontal = 1f;
			}
			else if (this.leftButtonPast)
			{
				horizontal = -1f;
			}
			ControlInput controlInput = new ControlInput
			{
				horizontal = horizontal
			};
			if (controlInput.horizontal != 0f)
			{
				if (controlInput.horizontal < 0f)
				{
					this.spriteRenderer.flipX = true;
				}
				else if (controlInput.horizontal > 0f)
				{
					this.spriteRenderer.flipX = false;
				}
				this.animator.SetBool("Moving", true);
			}
			else
			{
				this.animator.SetBool("Moving", false);
			}
			return controlInput;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00183FC4 File Offset: 0x001821C4
		public void PickUpTray(Food plate)
		{
			this.animator.SetTrigger("GetTray");
			this.heldItem = plate;
			this.plateTransform.gameObject.SetActive(false);
			this.plateTransform.GetComponent<SpriteRenderer>().sprite = this.heldItem.smallSprite;
			this.plateTransform.gameObject.SetActive(true);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00184025 File Offset: 0x00182225
		public void DropTray()
		{
			this.plateTransform.gameObject.SetActive(false);
			this.animator.SetTrigger("DropTray");
			this.heldItem = null;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00184050 File Offset: 0x00182250
		private void OnTriggerEnter2D(Collider2D collision)
		{
			AIController component = collision.GetComponent<AIController>();
			if (component != null)
			{
				if (component.state == AIController.AIState.Menu)
				{
					this.aiTarget = component;
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.TakeOrder);
				}
				if (component.state == AIController.AIState.Waiting && this.heldItem != null)
				{
					this.aiTarget = component;
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.GiveFood);
				}
			}
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x001840A8 File Offset: 0x001822A8
		private void OnTriggerExit2D(Collider2D collision)
		{
			AIController component = collision.GetComponent<AIController>();
			if (component != null && component == this.aiTarget)
			{
				this.aiTarget = null;
				InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
			}
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x001840E0 File Offset: 0x001822E0
		public void SetPause(bool toPause)
		{
			this.isPaused = toPause;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x001840EC File Offset: 0x001822EC
		public void PositionTray(string point)
		{
			string[] array = point.Split(new char[]
			{
				','
			});
			float num;
			float.TryParse(array[0], out num);
			float y;
			float.TryParse(array[1], out y);
			this.plateTransform.localPosition = new Vector3(this.spriteRenderer.flipX ? (-num) : num, y, 0f);
		}

		// Token: 0x04003D73 RID: 15731
		private static YandereController instance;

		// Token: 0x04003D74 RID: 15732
		public static bool leftButton;

		// Token: 0x04003D75 RID: 15733
		public static bool rightButton;

		// Token: 0x04003D76 RID: 15734
		public Transform leftBounds;

		// Token: 0x04003D77 RID: 15735
		public Transform rightBounds;

		// Token: 0x04003D78 RID: 15736
		public Transform interactionIndicator;

		// Token: 0x04003D79 RID: 15737
		public Transform plateTransform;

		// Token: 0x04003D7A RID: 15738
		public Food heldItem;

		// Token: 0x04003D7B RID: 15739
		private SpriteRenderer spriteRenderer;

		// Token: 0x04003D7C RID: 15740
		private Animator animator;

		// Token: 0x04003D7D RID: 15741
		private AIController aiTarget;

		// Token: 0x04003D7E RID: 15742
		public bool leftButtonPast;

		// Token: 0x04003D7F RID: 15743
		public bool rightButtonPast;

		// Token: 0x04003D80 RID: 15744
		private bool isPaused;
	}
}
