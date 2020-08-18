using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200057C RID: 1404
	public class RecastBBTreeBox
	{
		// Token: 0x06002630 RID: 9776 RVA: 0x001A56FC File Offset: 0x001A38FC
		public RecastBBTreeBox(RecastMeshObj mesh)
		{
			this.mesh = mesh;
			Vector3 min = mesh.bounds.min;
			Vector3 max = mesh.bounds.max;
			this.rect = Rect.MinMaxRect(min.x, min.z, max.x, max.z);
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x001A5751 File Offset: 0x001A3951
		public bool Contains(Vector3 p)
		{
			return this.rect.Contains(p);
		}

		// Token: 0x0400411A RID: 16666
		public Rect rect;

		// Token: 0x0400411B RID: 16667
		public RecastMeshObj mesh;

		// Token: 0x0400411C RID: 16668
		public RecastBBTreeBox c1;

		// Token: 0x0400411D RID: 16669
		public RecastBBTreeBox c2;
	}
}
