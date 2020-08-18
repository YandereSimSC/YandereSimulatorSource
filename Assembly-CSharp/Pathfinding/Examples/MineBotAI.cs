using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F5 RID: 1525
	[RequireComponent(typeof(Seeker))]
	[Obsolete("This script has been replaced by Pathfinding.Examples.MineBotAnimation. Any uses of this script in the Unity editor will be automatically replaced by one AIPath component and one MineBotAnimation component.")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mine_bot_a_i.php")]
	public class MineBotAI : AIPath
	{
		// Token: 0x040043C3 RID: 17347
		public Animation anim;

		// Token: 0x040043C4 RID: 17348
		public float sleepVelocity = 0.4f;

		// Token: 0x040043C5 RID: 17349
		public float animationSpeed = 0.2f;

		// Token: 0x040043C6 RID: 17350
		public GameObject endOfPathEffect;
	}
}
