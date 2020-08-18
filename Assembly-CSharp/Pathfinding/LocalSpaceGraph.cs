using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055A RID: 1370
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_local_space_graph.php")]
	public class LocalSpaceGraph : VersionedMonoBehaviour
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x0019686A File Offset: 0x00194A6A
		// (set) Token: 0x06002433 RID: 9267 RVA: 0x00196872 File Offset: 0x00194A72
		public GraphTransform transformation { get; private set; }

		// Token: 0x06002434 RID: 9268 RVA: 0x0019687B File Offset: 0x00194A7B
		private void Start()
		{
			this.originalMatrix = base.transform.worldToLocalMatrix;
			base.transform.hasChanged = true;
			this.Refresh();
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x001968A0 File Offset: 0x00194AA0
		public void Refresh()
		{
			if (base.transform.hasChanged)
			{
				this.transformation = new GraphTransform(base.transform.localToWorldMatrix * this.originalMatrix);
				base.transform.hasChanged = false;
			}
		}

		// Token: 0x0400401B RID: 16411
		private Matrix4x4 originalMatrix;
	}
}
