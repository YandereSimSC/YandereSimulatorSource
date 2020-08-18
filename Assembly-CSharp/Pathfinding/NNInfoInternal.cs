using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000543 RID: 1347
	public struct NNInfoInternal
	{
		// Token: 0x060023EF RID: 9199 RVA: 0x00195E7C File Offset: 0x0019407C
		public NNInfoInternal(GraphNode node)
		{
			this.node = node;
			this.constrainedNode = null;
			this.clampedPosition = Vector3.zero;
			this.constClampedPosition = Vector3.zero;
			this.UpdateInfo();
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00195EA8 File Offset: 0x001940A8
		public void UpdateInfo()
		{
			this.clampedPosition = ((this.node != null) ? ((Vector3)this.node.position) : Vector3.zero);
			this.constClampedPosition = ((this.constrainedNode != null) ? ((Vector3)this.constrainedNode.position) : Vector3.zero);
		}

		// Token: 0x04003FBC RID: 16316
		public GraphNode node;

		// Token: 0x04003FBD RID: 16317
		public GraphNode constrainedNode;

		// Token: 0x04003FBE RID: 16318
		public Vector3 clampedPosition;

		// Token: 0x04003FBF RID: 16319
		public Vector3 constClampedPosition;
	}
}
