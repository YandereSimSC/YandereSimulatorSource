using System;

namespace Pathfinding
{
	// Token: 0x02000593 RID: 1427
	public class EndingConditionDistance : PathEndingCondition
	{
		// Token: 0x060026D6 RID: 9942 RVA: 0x001AA363 File Offset: 0x001A8563
		public EndingConditionDistance(Path p, int maxGScore) : base(p)
		{
			this.maxGScore = maxGScore;
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x001AA37B File Offset: 0x001A857B
		public override bool TargetFound(PathNode node)
		{
			return (ulong)node.G >= (ulong)((long)this.maxGScore);
		}

		// Token: 0x04004198 RID: 16792
		public int maxGScore = 100;
	}
}
