using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005E3 RID: 1507
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_a_i.php")]
	public class TurnBasedAI : VersionedMonoBehaviour
	{
		// Token: 0x0600299C RID: 10652 RVA: 0x001BF3E1 File Offset: 0x001BD5E1
		private void Start()
		{
			this.blocker.BlockAtCurrentPosition();
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x001BF3EE File Offset: 0x001BD5EE
		protected override void Awake()
		{
			base.Awake();
			this.traversalProvider = new BlockManager.TraversalProvider(this.blockManager, BlockManager.BlockMode.AllExceptSelector, new List<SingleNodeBlocker>
			{
				this.blocker
			});
		}

		// Token: 0x04004349 RID: 17225
		public int movementPoints = 2;

		// Token: 0x0400434A RID: 17226
		public BlockManager blockManager;

		// Token: 0x0400434B RID: 17227
		public SingleNodeBlocker blocker;

		// Token: 0x0400434C RID: 17228
		public GraphNode targetNode;

		// Token: 0x0400434D RID: 17229
		public BlockManager.TraversalProvider traversalProvider;
	}
}
