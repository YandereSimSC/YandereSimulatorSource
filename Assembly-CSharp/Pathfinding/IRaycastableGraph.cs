using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000549 RID: 1353
	public interface IRaycastableGraph
	{
		// Token: 0x06002402 RID: 9218
		bool Linecast(Vector3 start, Vector3 end);

		// Token: 0x06002403 RID: 9219
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint);

		// Token: 0x06002404 RID: 9220
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint, out GraphHitInfo hit);

		// Token: 0x06002405 RID: 9221
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace);
	}
}
