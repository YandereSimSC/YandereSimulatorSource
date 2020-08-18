using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005C0 RID: 1472
	public class ObstacleVertex
	{
		// Token: 0x0400425F RID: 16991
		public bool ignore;

		// Token: 0x04004260 RID: 16992
		public Vector3 position;

		// Token: 0x04004261 RID: 16993
		public Vector2 dir;

		// Token: 0x04004262 RID: 16994
		public float height;

		// Token: 0x04004263 RID: 16995
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x04004264 RID: 16996
		public ObstacleVertex next;

		// Token: 0x04004265 RID: 16997
		public ObstacleVertex prev;
	}
}
