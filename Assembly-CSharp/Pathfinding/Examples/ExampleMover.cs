using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005EF RID: 1519
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_example_mover.php")]
	public class ExampleMover : MonoBehaviour
	{
		// Token: 0x060029DC RID: 10716 RVA: 0x001C1903 File Offset: 0x001BFB03
		private void Awake()
		{
			this.agent = base.GetComponent<RVOExampleAgent>();
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x001C1911 File Offset: 0x001BFB11
		private void Start()
		{
			this.agent.SetTarget(this.target.position);
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x001C1929 File Offset: 0x001BFB29
		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				this.agent.SetTarget(this.target.position);
			}
		}

		// Token: 0x0400439C RID: 17308
		private RVOExampleAgent agent;

		// Token: 0x0400439D RID: 17309
		public Transform target;
	}
}
