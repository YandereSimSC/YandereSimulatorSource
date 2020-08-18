using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000540 RID: 1344
	public struct GraphHitInfo
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x00195D10 File Offset: 0x00193F10
		public float distance
		{
			get
			{
				return (this.point - this.origin).magnitude;
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00195D36 File Offset: 0x00193F36
		public GraphHitInfo(Vector3 point)
		{
			this.tangentOrigin = Vector3.zero;
			this.origin = Vector3.zero;
			this.point = point;
			this.node = null;
			this.tangent = Vector3.zero;
		}

		// Token: 0x04003FAE RID: 16302
		public Vector3 origin;

		// Token: 0x04003FAF RID: 16303
		public Vector3 point;

		// Token: 0x04003FB0 RID: 16304
		public GraphNode node;

		// Token: 0x04003FB1 RID: 16305
		public Vector3 tangentOrigin;

		// Token: 0x04003FB2 RID: 16306
		public Vector3 tangent;
	}
}
