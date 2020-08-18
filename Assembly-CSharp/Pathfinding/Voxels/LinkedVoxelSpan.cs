using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005AA RID: 1450
	public struct LinkedVoxelSpan
	{
		// Token: 0x06002775 RID: 10101 RVA: 0x001AF3D6 File Offset: 0x001AD5D6
		public LinkedVoxelSpan(uint bottom, uint top, int area)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = -1;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x001AF3F4 File Offset: 0x001AD5F4
		public LinkedVoxelSpan(uint bottom, uint top, int area, int next)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = next;
		}

		// Token: 0x040041F1 RID: 16881
		public uint bottom;

		// Token: 0x040041F2 RID: 16882
		public uint top;

		// Token: 0x040041F3 RID: 16883
		public int next;

		// Token: 0x040041F4 RID: 16884
		public int area;
	}
}
