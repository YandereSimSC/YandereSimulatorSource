using System;

namespace Pathfinding
{
	// Token: 0x0200053A RID: 1338
	public static class DefaultITraversalProvider
	{
		// Token: 0x06002384 RID: 9092 RVA: 0x00194DB8 File Offset: 0x00192FB8
		public static bool CanTraverse(Path path, GraphNode node)
		{
			return node.Walkable && (path.enabledTags >> (int)node.Tag & 1) != 0;
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00194DD9 File Offset: 0x00192FD9
		public static uint GetTraversalCost(Path path, GraphNode node)
		{
			return path.GetTagPenalty((int)node.Tag) + node.Penalty;
		}
	}
}
