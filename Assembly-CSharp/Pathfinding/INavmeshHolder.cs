using System;

namespace Pathfinding
{
	// Token: 0x02000571 RID: 1393
	public interface INavmeshHolder : ITransformedGraph, INavmesh
	{
		// Token: 0x06002590 RID: 9616
		Int3 GetVertex(int i);

		// Token: 0x06002591 RID: 9617
		Int3 GetVertexInGraphSpace(int i);

		// Token: 0x06002592 RID: 9618
		int GetVertexArrayIndex(int index);

		// Token: 0x06002593 RID: 9619
		void GetTileCoordinates(int tileIndex, out int x, out int z);
	}
}
