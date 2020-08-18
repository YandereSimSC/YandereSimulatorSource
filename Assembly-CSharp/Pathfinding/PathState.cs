using System;

namespace Pathfinding
{
	// Token: 0x02000554 RID: 1364
	public enum PathState
	{
		// Token: 0x04003FFF RID: 16383
		Created,
		// Token: 0x04004000 RID: 16384
		PathQueue,
		// Token: 0x04004001 RID: 16385
		Processing,
		// Token: 0x04004002 RID: 16386
		ReturnQueue,
		// Token: 0x04004003 RID: 16387
		Returned
	}
}
