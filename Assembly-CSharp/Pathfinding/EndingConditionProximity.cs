using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200059D RID: 1437
	public class EndingConditionProximity : ABPathEndingCondition
	{
		// Token: 0x0600271D RID: 10013 RVA: 0x001ABF24 File Offset: 0x001AA124
		public EndingConditionProximity(ABPath p, float maxDistance) : base(p)
		{
			this.maxDistance = maxDistance;
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x001ABF40 File Offset: 0x001AA140
		public override bool TargetFound(PathNode node)
		{
			return ((Vector3)node.node.position - this.abPath.originalEndPoint).sqrMagnitude <= this.maxDistance * this.maxDistance;
		}

		// Token: 0x040041B9 RID: 16825
		public float maxDistance = 10f;
	}
}
