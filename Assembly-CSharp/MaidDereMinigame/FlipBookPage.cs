using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004FD RID: 1277
	public class FlipBookPage : MonoBehaviour
	{
		// Token: 0x06002014 RID: 8212 RVA: 0x001853A7 File Offset: 0x001835A7
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x001853C1 File Offset: 0x001835C1
		public void Transition(bool toOpen)
		{
			this.animator.SetTrigger(toOpen ? "OpenPage" : "ClosePage");
			if (this.objectToActivate != null)
			{
				this.objectToActivate.SetActive(false);
			}
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x001853F7 File Offset: 0x001835F7
		public void SwitchSort()
		{
			this.spriteRenderer.sortingOrder = 10 - this.spriteRenderer.sortingOrder;
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x00185412 File Offset: 0x00183612
		public void ObjectActive(bool toActive = true)
		{
			if (this.objectToActivate != null)
			{
				this.objectToActivate.SetActive(toActive);
			}
		}

		// Token: 0x04003DD1 RID: 15825
		[HideInInspector]
		public Animator animator;

		// Token: 0x04003DD2 RID: 15826
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		// Token: 0x04003DD3 RID: 15827
		public GameObject objectToActivate;
	}
}
