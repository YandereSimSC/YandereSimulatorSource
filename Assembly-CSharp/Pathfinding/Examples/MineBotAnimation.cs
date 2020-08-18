using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F6 RID: 1526
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mine_bot_animation.php")]
	public class MineBotAnimation : VersionedMonoBehaviour
	{
		// Token: 0x060029FA RID: 10746 RVA: 0x001C26B3 File Offset: 0x001C08B3
		protected override void Awake()
		{
			base.Awake();
			this.ai = base.GetComponent<IAstarAI>();
			this.tr = base.GetComponent<Transform>();
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x001C26D4 File Offset: 0x001C08D4
		private void Start()
		{
			this.anim["forward"].layer = 10;
			this.anim.Play("awake");
			this.anim.Play("forward");
			this.anim["awake"].wrapMode = WrapMode.Once;
			this.anim["awake"].speed = 0f;
			this.anim["awake"].normalizedTime = 1f;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x001C2764 File Offset: 0x001C0964
		private void OnTargetReached()
		{
			if (this.endOfPathEffect != null && Vector3.Distance(this.tr.position, this.lastTarget) > 1f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.endOfPathEffect, this.tr.position, this.tr.rotation);
				this.lastTarget = this.tr.position;
			}
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x001C27D0 File Offset: 0x001C09D0
		protected void Update()
		{
			if (this.ai.reachedEndOfPath)
			{
				if (!this.isAtDestination)
				{
					this.OnTargetReached();
				}
				this.isAtDestination = true;
			}
			else
			{
				this.isAtDestination = false;
			}
			Vector3 vector = this.tr.InverseTransformDirection(this.ai.velocity);
			vector.y = 0f;
			if (vector.sqrMagnitude <= this.sleepVelocity * this.sleepVelocity)
			{
				this.anim.Blend("forward", 0f, 0.2f);
				return;
			}
			this.anim.Blend("forward", 1f, 0.2f);
			AnimationState animationState = this.anim["forward"];
			float z = vector.z;
			animationState.speed = z * this.animationSpeed;
		}

		// Token: 0x040043C7 RID: 17351
		public Animation anim;

		// Token: 0x040043C8 RID: 17352
		public float sleepVelocity = 0.4f;

		// Token: 0x040043C9 RID: 17353
		public float animationSpeed = 0.2f;

		// Token: 0x040043CA RID: 17354
		public GameObject endOfPathEffect;

		// Token: 0x040043CB RID: 17355
		private bool isAtDestination;

		// Token: 0x040043CC RID: 17356
		private IAstarAI ai;

		// Token: 0x040043CD RID: 17357
		private Transform tr;

		// Token: 0x040043CE RID: 17358
		protected Vector3 lastTarget;
	}
}
