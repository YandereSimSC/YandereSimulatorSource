using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000568 RID: 1384
	internal class LinkedLevelNode
	{
		// Token: 0x0400408A RID: 16522
		public Vector3 position;

		// Token: 0x0400408B RID: 16523
		public bool walkable;

		// Token: 0x0400408C RID: 16524
		public RaycastHit hit;

		// Token: 0x0400408D RID: 16525
		public float height;

		// Token: 0x0400408E RID: 16526
		public LinkedLevelNode next;
	}
}
