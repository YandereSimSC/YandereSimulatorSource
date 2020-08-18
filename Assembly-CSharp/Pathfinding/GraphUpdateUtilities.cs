using System;
using System.Collections.Generic;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x020005A2 RID: 1442
	public static class GraphUpdateUtilities
	{
		// Token: 0x06002744 RID: 10052 RVA: 0x001AD314 File Offset: 0x001AB514
		public static bool UpdateGraphsNoBlock(GraphUpdateObject guo, GraphNode node1, GraphNode node2, bool alwaysRevert = false)
		{
			List<GraphNode> list = ListPool<GraphNode>.Claim();
			list.Add(node1);
			list.Add(node2);
			bool result = GraphUpdateUtilities.UpdateGraphsNoBlock(guo, list, alwaysRevert);
			ListPool<GraphNode>.Release(ref list);
			return result;
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x001AD344 File Offset: 0x001AB544
		public static bool UpdateGraphsNoBlock(GraphUpdateObject guo, List<GraphNode> nodes, bool alwaysRevert = false)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (!nodes[i].Walkable)
				{
					return false;
				}
			}
			guo.trackChangedNodes = true;
			PathProcessor.GraphUpdateLock graphUpdateLock = AstarPath.active.PausePathfinding();
			bool flag;
			try
			{
				AstarPath.active.UpdateGraphs(guo);
				AstarPath.active.FlushGraphUpdates();
				flag = PathUtilities.IsPathPossible(nodes);
				if (!flag || alwaysRevert)
				{
					guo.RevertFromBackup();
					AstarPath.active.FloodFill();
				}
			}
			finally
			{
				graphUpdateLock.Release();
			}
			guo.trackChangedNodes = false;
			return flag;
		}
	}
}
