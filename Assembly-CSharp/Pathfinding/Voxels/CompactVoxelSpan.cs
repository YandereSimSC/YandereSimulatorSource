using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005B2 RID: 1458
	public struct CompactVoxelSpan
	{
		// Token: 0x06002781 RID: 10113 RVA: 0x001AF669 File Offset: 0x001AD869
		public CompactVoxelSpan(ushort bottom, uint height)
		{
			this.con = 24u;
			this.y = bottom;
			this.h = height;
			this.reg = 0;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x001AF688 File Offset: 0x001AD888
		public void SetConnection(int dir, uint value)
		{
			int num = dir * 6;
			this.con = (uint)(((ulong)this.con & (ulong)(~(63L << (num & 31)))) | (ulong)((ulong)(value & 63u) << num));
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x001AF6BC File Offset: 0x001AD8BC
		public int GetConnection(int dir)
		{
			return (int)this.con >> dir * 6 & 63;
		}

		// Token: 0x0400420B RID: 16907
		public ushort y;

		// Token: 0x0400420C RID: 16908
		public uint con;

		// Token: 0x0400420D RID: 16909
		public uint h;

		// Token: 0x0400420E RID: 16910
		public int reg;
	}
}
