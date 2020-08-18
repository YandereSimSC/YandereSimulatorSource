using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005B0 RID: 1456
	public struct VoxelCell
	{
		// Token: 0x0600277F RID: 10111 RVA: 0x001AF558 File Offset: 0x001AD758
		public void AddSpan(uint bottom, uint top, int area, int voxelWalkableClimb)
		{
			VoxelSpan voxelSpan = new VoxelSpan(bottom, top, area);
			if (this.firstSpan == null)
			{
				this.firstSpan = voxelSpan;
				return;
			}
			VoxelSpan voxelSpan2 = null;
			VoxelSpan voxelSpan3 = this.firstSpan;
			while (voxelSpan3 != null && voxelSpan3.bottom <= voxelSpan.top)
			{
				if (voxelSpan3.top < voxelSpan.bottom)
				{
					voxelSpan2 = voxelSpan3;
					voxelSpan3 = voxelSpan3.next;
				}
				else
				{
					if (voxelSpan3.bottom < bottom)
					{
						voxelSpan.bottom = voxelSpan3.bottom;
					}
					if (voxelSpan3.top > top)
					{
						voxelSpan.top = voxelSpan3.top;
					}
					if (Math.Abs((int)(voxelSpan.top - voxelSpan3.top)) <= voxelWalkableClimb)
					{
						voxelSpan.area = Math.Max(voxelSpan.area, voxelSpan3.area);
					}
					VoxelSpan next = voxelSpan3.next;
					if (voxelSpan2 != null)
					{
						voxelSpan2.next = next;
					}
					else
					{
						this.firstSpan = next;
					}
					voxelSpan3 = next;
				}
			}
			if (voxelSpan2 != null)
			{
				voxelSpan.next = voxelSpan2.next;
				voxelSpan2.next = voxelSpan;
				return;
			}
			voxelSpan.next = this.firstSpan;
			this.firstSpan = voxelSpan;
		}

		// Token: 0x04004208 RID: 16904
		public VoxelSpan firstSpan;
	}
}
