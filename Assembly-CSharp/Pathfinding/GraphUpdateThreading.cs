using System;

namespace Pathfinding
{
	// Token: 0x0200054F RID: 1359
	public enum GraphUpdateThreading
	{
		// Token: 0x04003FDA RID: 16346
		UnityThread,
		// Token: 0x04003FDB RID: 16347
		SeparateThread,
		// Token: 0x04003FDC RID: 16348
		UnityInit,
		// Token: 0x04003FDD RID: 16349
		UnityPost = 4,
		// Token: 0x04003FDE RID: 16350
		SeparateAndUnityInit = 3
	}
}
