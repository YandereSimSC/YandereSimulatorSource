using System;
using Pathfinding.RVO;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F4 RID: 1524
	[RequireComponent(typeof(RVOController))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_manual_r_v_o_agent.php")]
	public class ManualRVOAgent : MonoBehaviour
	{
		// Token: 0x060029F6 RID: 10742 RVA: 0x001C260C File Offset: 0x001C080C
		private void Awake()
		{
			this.rvo = base.GetComponent<RVOController>();
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x001C261C File Offset: 0x001C081C
		private void Update()
		{
			float axis = Input.GetAxis("Horizontal");
			float axis2 = Input.GetAxis("Vertical");
			Vector3 vector = new Vector3(axis, 0f, axis2) * this.speed;
			this.rvo.velocity = vector;
			base.transform.position += vector * Time.deltaTime;
		}

		// Token: 0x040043C1 RID: 17345
		private RVOController rvo;

		// Token: 0x040043C2 RID: 17346
		public float speed = 1f;
	}
}
