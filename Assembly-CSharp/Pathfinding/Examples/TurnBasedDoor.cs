using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005ED RID: 1517
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(SingleNodeBlocker))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_door.php")]
	public class TurnBasedDoor : MonoBehaviour
	{
		// Token: 0x060029CB RID: 10699 RVA: 0x001C15BE File Offset: 0x001BF7BE
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.blocker = base.GetComponent<SingleNodeBlocker>();
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x001C15D8 File Offset: 0x001BF7D8
		private void Start()
		{
			this.blocker.BlockAtCurrentPosition();
			this.animator.CrossFade("close", 0.2f);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x001C15FA File Offset: 0x001BF7FA
		public void Close()
		{
			base.StartCoroutine(this.WaitAndClose());
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x001C1609 File Offset: 0x001BF809
		private IEnumerator WaitAndClose()
		{
			List<SingleNodeBlocker> selector = new List<SingleNodeBlocker>
			{
				this.blocker
			};
			GraphNode node = AstarPath.active.GetNearest(base.transform.position).node;
			if (this.blocker.manager.NodeContainsAnyExcept(node, selector))
			{
				this.animator.CrossFade("blocked", 0.2f);
			}
			while (this.blocker.manager.NodeContainsAnyExcept(node, selector))
			{
				yield return null;
			}
			this.open = false;
			this.animator.CrossFade("close", 0.2f);
			this.blocker.BlockAtCurrentPosition();
			yield break;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x001C1618 File Offset: 0x001BF818
		public void Open()
		{
			base.StopAllCoroutines();
			this.animator.CrossFade("open", 0.2f);
			this.open = true;
			this.blocker.Unblock();
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x001C1647 File Offset: 0x001BF847
		public void Toggle()
		{
			if (this.open)
			{
				this.Close();
				return;
			}
			this.Open();
		}

		// Token: 0x04004392 RID: 17298
		private Animator animator;

		// Token: 0x04004393 RID: 17299
		private SingleNodeBlocker blocker;

		// Token: 0x04004394 RID: 17300
		private bool open;
	}
}
