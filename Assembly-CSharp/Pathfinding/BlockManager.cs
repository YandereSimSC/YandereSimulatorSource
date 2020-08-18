using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200059E RID: 1438
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_block_manager.php")]
	public class BlockManager : VersionedMonoBehaviour
	{
		// Token: 0x0600271F RID: 10015 RVA: 0x001ABF87 File Offset: 0x001AA187
		private void Start()
		{
			if (!AstarPath.active)
			{
				throw new Exception("No AstarPath object in the scene");
			}
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x001ABFA0 File Offset: 0x001AA1A0
		public bool NodeContainsAnyOf(GraphNode node, List<SingleNodeBlocker> selector)
		{
			List<SingleNodeBlocker> list;
			if (!this.blocked.TryGetValue(node, out list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				SingleNodeBlocker singleNodeBlocker = list[i];
				for (int j = 0; j < selector.Count; j++)
				{
					if (singleNodeBlocker == selector[j])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x001ABFF8 File Offset: 0x001AA1F8
		public bool NodeContainsAnyExcept(GraphNode node, List<SingleNodeBlocker> selector)
		{
			List<SingleNodeBlocker> list;
			if (!this.blocked.TryGetValue(node, out list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				SingleNodeBlocker singleNodeBlocker = list[i];
				bool flag = false;
				for (int j = 0; j < selector.Count; j++)
				{
					if (singleNodeBlocker == selector[j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x001AC05C File Offset: 0x001AA25C
		public void InternalBlock(GraphNode node, SingleNodeBlocker blocker)
		{
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate()
			{
				List<SingleNodeBlocker> list;
				if (!this.blocked.TryGetValue(node, out list))
				{
					list = (this.blocked[node] = ListPool<SingleNodeBlocker>.Claim());
				}
				list.Add(blocker);
			}, null));
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x001AC0A0 File Offset: 0x001AA2A0
		public void InternalUnblock(GraphNode node, SingleNodeBlocker blocker)
		{
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate()
			{
				List<SingleNodeBlocker> list;
				if (this.blocked.TryGetValue(node, out list))
				{
					list.Remove(blocker);
					if (list.Count == 0)
					{
						this.blocked.Remove(node);
						ListPool<SingleNodeBlocker>.Release(ref list);
					}
				}
			}, null));
		}

		// Token: 0x040041BA RID: 16826
		private Dictionary<GraphNode, List<SingleNodeBlocker>> blocked = new Dictionary<GraphNode, List<SingleNodeBlocker>>();

		// Token: 0x02000759 RID: 1881
		public enum BlockMode
		{
			// Token: 0x04004A0A RID: 18954
			AllExceptSelector,
			// Token: 0x04004A0B RID: 18955
			OnlySelector
		}

		// Token: 0x0200075A RID: 1882
		public class TraversalProvider : ITraversalProvider
		{
			// Token: 0x1700068C RID: 1676
			// (get) Token: 0x06002D65 RID: 11621 RVA: 0x001CED45 File Offset: 0x001CCF45
			// (set) Token: 0x06002D66 RID: 11622 RVA: 0x001CED4D File Offset: 0x001CCF4D
			public BlockManager.BlockMode mode { get; private set; }

			// Token: 0x06002D67 RID: 11623 RVA: 0x001CED56 File Offset: 0x001CCF56
			public TraversalProvider(BlockManager blockManager, BlockManager.BlockMode mode, List<SingleNodeBlocker> selector)
			{
				if (blockManager == null)
				{
					throw new ArgumentNullException("blockManager");
				}
				if (selector == null)
				{
					throw new ArgumentNullException("selector");
				}
				this.blockManager = blockManager;
				this.mode = mode;
				this.selector = selector;
			}

			// Token: 0x06002D68 RID: 11624 RVA: 0x001CED98 File Offset: 0x001CCF98
			public bool CanTraverse(Path path, GraphNode node)
			{
				if (!node.Walkable || (path.enabledTags >> (int)node.Tag & 1) == 0)
				{
					return false;
				}
				if (this.mode == BlockManager.BlockMode.OnlySelector)
				{
					return !this.blockManager.NodeContainsAnyOf(node, this.selector);
				}
				return !this.blockManager.NodeContainsAnyExcept(node, this.selector);
			}

			// Token: 0x06002D69 RID: 11625 RVA: 0x001CEDF7 File Offset: 0x001CCFF7
			public uint GetTraversalCost(Path path, GraphNode node)
			{
				return path.GetTagPenalty((int)node.Tag) + node.Penalty;
			}

			// Token: 0x04004A0C RID: 18956
			private readonly BlockManager blockManager;

			// Token: 0x04004A0E RID: 18958
			private readonly List<SingleNodeBlocker> selector;
		}
	}
}
