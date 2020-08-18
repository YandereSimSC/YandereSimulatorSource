using System;

namespace Pathfinding
{
	// Token: 0x0200059B RID: 1435
	public abstract class PathEndingCondition
	{
		// Token: 0x06002718 RID: 10008 RVA: 0x000045DB File Offset: 0x000027DB
		protected PathEndingCondition()
		{
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x001ABECE File Offset: 0x001AA0CE
		public PathEndingCondition(Path p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.path = p;
		}

		// Token: 0x0600271A RID: 10010
		public abstract bool TargetFound(PathNode node);

		// Token: 0x040041B7 RID: 16823
		protected Path path;
	}
}
