using System;

namespace Pathfinding
{
	// Token: 0x02000534 RID: 1332
	public interface IWorkItemContext
	{
		// Token: 0x06002339 RID: 9017
		void QueueFloodFill();

		// Token: 0x0600233A RID: 9018
		void EnsureValidFloodFill();
	}
}
