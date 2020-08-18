using System;

namespace Pathfinding
{
	// Token: 0x02000546 RID: 1350
	public interface IUpdatableGraph
	{
		// Token: 0x060023F8 RID: 9208
		void UpdateArea(GraphUpdateObject o);

		// Token: 0x060023F9 RID: 9209
		void UpdateAreaInit(GraphUpdateObject o);

		// Token: 0x060023FA RID: 9210
		void UpdateAreaPost(GraphUpdateObject o);

		// Token: 0x060023FB RID: 9211
		GraphUpdateThreading CanUpdateAsync(GraphUpdateObject o);
	}
}
