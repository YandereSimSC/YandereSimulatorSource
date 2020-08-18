using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005B3 RID: 1459
	public class VoxelSpan
	{
		// Token: 0x06002784 RID: 10116 RVA: 0x001AF6CE File Offset: 0x001AD8CE
		public VoxelSpan(uint b, uint t, int area)
		{
			this.bottom = b;
			this.top = t;
			this.area = area;
		}

		// Token: 0x0400420F RID: 16911
		public uint bottom;

		// Token: 0x04004210 RID: 16912
		public uint top;

		// Token: 0x04004211 RID: 16913
		public VoxelSpan next;

		// Token: 0x04004212 RID: 16914
		public int area;
	}
}
