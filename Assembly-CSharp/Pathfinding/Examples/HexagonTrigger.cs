using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pathfinding.Examples
{
	// Token: 0x020005EC RID: 1516
	[RequireComponent(typeof(Animator))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_hexagon_trigger.php")]
	public class HexagonTrigger : MonoBehaviour
	{
		// Token: 0x060029C7 RID: 10695 RVA: 0x001C1501 File Offset: 0x001BF701
		private void Awake()
		{
			this.anim = base.GetComponent<Animator>();
			this.button.interactable = false;
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x001C151C File Offset: 0x001BF71C
		private void OnTriggerEnter(Collider coll)
		{
			TurnBasedAI componentInParent = coll.GetComponentInParent<TurnBasedAI>();
			GraphNode node = AstarPath.active.GetNearest(base.transform.position).node;
			if (componentInParent != null && componentInParent.targetNode == node)
			{
				this.button.interactable = true;
				this.visible = true;
				this.anim.CrossFade("show", 0.1f);
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x001C1585 File Offset: 0x001BF785
		private void OnTriggerExit(Collider coll)
		{
			if (coll.GetComponentInParent<TurnBasedAI>() != null && this.visible)
			{
				this.button.interactable = false;
				this.anim.CrossFade("hide", 0.1f);
			}
		}

		// Token: 0x0400438F RID: 17295
		public Button button;

		// Token: 0x04004390 RID: 17296
		private Animator anim;

		// Token: 0x04004391 RID: 17297
		private bool visible;
	}
}
