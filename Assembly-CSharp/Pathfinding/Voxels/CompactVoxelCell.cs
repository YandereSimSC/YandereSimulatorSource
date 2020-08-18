using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005B1 RID: 1457
	public struct CompactVoxelCell
	{
		// Token: 0x06002780 RID: 10112 RVA: 0x001AF659 File Offset: 0x001AD859
		public CompactVoxelCell(uint i, uint c)
		{
			this.index = i;
			this.count = c;
		}

		// Token: 0x04004209 RID: 16905
		public uint index;

		// Token: 0x0400420A RID: 16906
		public uint count;
	}
}
