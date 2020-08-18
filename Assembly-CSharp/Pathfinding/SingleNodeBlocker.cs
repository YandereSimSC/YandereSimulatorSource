using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200059F RID: 1439
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_single_node_blocker.php")]
	public class SingleNodeBlocker : VersionedMonoBehaviour
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x001AC0F7 File Offset: 0x001AA2F7
		// (set) Token: 0x06002726 RID: 10022 RVA: 0x001AC0FF File Offset: 0x001AA2FF
		public GraphNode lastBlocked { get; private set; }

		// Token: 0x06002727 RID: 10023 RVA: 0x001AC108 File Offset: 0x001AA308
		public void BlockAtCurrentPosition()
		{
			this.BlockAt(base.transform.position);
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x001AC11C File Offset: 0x001AA31C
		public void BlockAt(Vector3 position)
		{
			this.Unblock();
			GraphNode node = AstarPath.active.GetNearest(position, NNConstraint.None).node;
			if (node != null)
			{
				this.Block(node);
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x001AC14F File Offset: 0x001AA34F
		public void Block(GraphNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.manager.InternalBlock(node, this);
			this.lastBlocked = node;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x001AC173 File Offset: 0x001AA373
		public void Unblock()
		{
			if (this.lastBlocked == null || this.lastBlocked.Destroyed)
			{
				this.lastBlocked = null;
				return;
			}
			this.manager.InternalUnblock(this.lastBlocked, this);
			this.lastBlocked = null;
		}

		// Token: 0x040041BC RID: 16828
		public BlockManager manager;
	}
}
