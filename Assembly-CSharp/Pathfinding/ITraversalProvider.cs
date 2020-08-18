using System;

namespace Pathfinding
{
	// Token: 0x02000539 RID: 1337
	public interface ITraversalProvider
	{
		// Token: 0x06002382 RID: 9090
		bool CanTraverse(Path path, GraphNode node);

		// Token: 0x06002383 RID: 9091
		uint GetTraversalCost(Path path, GraphNode node);
	}
}
