using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000544 RID: 1348
	public struct NNInfo
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x00195EFF File Offset: 0x001940FF
		[Obsolete("This field has been renamed to 'position'")]
		public Vector3 clampedPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x00195F07 File Offset: 0x00194107
		public NNInfo(NNInfoInternal internalInfo)
		{
			this.node = internalInfo.node;
			this.position = internalInfo.clampedPosition;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00195EFF File Offset: 0x001940FF
		public static explicit operator Vector3(NNInfo ob)
		{
			return ob.position;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00195F21 File Offset: 0x00194121
		public static explicit operator GraphNode(NNInfo ob)
		{
			return ob.node;
		}

		// Token: 0x04003FC0 RID: 16320
		public readonly GraphNode node;

		// Token: 0x04003FC1 RID: 16321
		public readonly Vector3 position;
	}
}
