using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F2 RID: 1522
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_astar_smooth_follow2.php")]
	public class AstarSmoothFollow2 : MonoBehaviour
	{
		// Token: 0x060029F0 RID: 10736 RVA: 0x001C238C File Offset: 0x001C058C
		private void LateUpdate()
		{
			Vector3 b;
			if (this.staticOffset)
			{
				b = this.target.position + new Vector3(0f, this.height, this.distance);
			}
			else if (this.followBehind)
			{
				b = this.target.TransformPoint(0f, this.height, -this.distance);
			}
			else
			{
				b = this.target.TransformPoint(0f, this.height, this.distance);
			}
			base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * this.damping);
			if (this.smoothRotation)
			{
				Quaternion b2 = Quaternion.LookRotation(this.target.position - base.transform.position, this.target.up);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b2, Time.deltaTime * this.rotationDamping);
				return;
			}
			base.transform.LookAt(this.target, this.target.up);
		}

		// Token: 0x040043B3 RID: 17331
		public Transform target;

		// Token: 0x040043B4 RID: 17332
		public float distance = 3f;

		// Token: 0x040043B5 RID: 17333
		public float height = 3f;

		// Token: 0x040043B6 RID: 17334
		public float damping = 5f;

		// Token: 0x040043B7 RID: 17335
		public bool smoothRotation = true;

		// Token: 0x040043B8 RID: 17336
		public bool followBehind = true;

		// Token: 0x040043B9 RID: 17337
		public float rotationDamping = 10f;

		// Token: 0x040043BA RID: 17338
		public bool staticOffset;
	}
}
