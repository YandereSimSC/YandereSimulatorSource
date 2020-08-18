using System;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x02000531 RID: 1329
	internal class PathReturnQueue
	{
		// Token: 0x06002323 RID: 8995 RVA: 0x00193BE6 File Offset: 0x00191DE6
		public PathReturnQueue(object pathsClaimedSilentlyBy)
		{
			this.pathsClaimedSilentlyBy = pathsClaimedSilentlyBy;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00193C00 File Offset: 0x00191E00
		public void Enqueue(Path path)
		{
			Queue<Path> obj = this.pathReturnQueue;
			lock (obj)
			{
				this.pathReturnQueue.Enqueue(path);
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00193C48 File Offset: 0x00191E48
		public void ReturnPaths(bool timeSlice)
		{
			long num = timeSlice ? (DateTime.UtcNow.Ticks + 10000L) : 0L;
			int num2 = 0;
			for (;;)
			{
				Queue<Path> obj = this.pathReturnQueue;
				Path path;
				lock (obj)
				{
					if (this.pathReturnQueue.Count == 0)
					{
						return;
					}
					path = this.pathReturnQueue.Dequeue();
				}
				((IPathInternals)path).ReturnPath();
				((IPathInternals)path).AdvanceState(PathState.Returned);
				path.Release(this.pathsClaimedSilentlyBy, true);
				num2++;
				if (num2 > 5 && timeSlice)
				{
					num2 = 0;
					if (DateTime.UtcNow.Ticks >= num)
					{
						break;
					}
				}
			}
		}

		// Token: 0x04003F48 RID: 16200
		private Queue<Path> pathReturnQueue = new Queue<Path>();

		// Token: 0x04003F49 RID: 16201
		private object pathsClaimedSilentlyBy;
	}
}
