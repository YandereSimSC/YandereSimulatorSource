using System;

namespace Pathfinding
{
	// Token: 0x0200056B RID: 1387
	public interface INavmesh
	{
		// Token: 0x06002510 RID: 9488
		void GetNodes(Action<GraphNode> del);
	}
}
