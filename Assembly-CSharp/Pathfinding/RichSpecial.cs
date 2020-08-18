using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000518 RID: 1304
	public class RichSpecial : RichPathPart
	{
		// Token: 0x06002159 RID: 8537 RVA: 0x0018A407 File Offset: 0x00188607
		public override void OnEnterPool()
		{
			this.nodeLink = null;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0018A410 File Offset: 0x00188610
		public RichSpecial Initialize(NodeLink2 nodeLink, GraphNode first)
		{
			this.nodeLink = nodeLink;
			if (first == nodeLink.startNode)
			{
				this.first = nodeLink.StartTransform;
				this.second = nodeLink.EndTransform;
				this.reverse = false;
			}
			else
			{
				this.first = nodeLink.EndTransform;
				this.second = nodeLink.StartTransform;
				this.reverse = true;
			}
			return this;
		}

		// Token: 0x04003E8E RID: 16014
		public NodeLink2 nodeLink;

		// Token: 0x04003E8F RID: 16015
		public Transform first;

		// Token: 0x04003E90 RID: 16016
		public Transform second;

		// Token: 0x04003E91 RID: 16017
		public bool reverse;
	}
}
