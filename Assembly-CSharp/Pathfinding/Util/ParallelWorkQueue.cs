using System;
using System.Collections.Generic;
using System.Threading;

namespace Pathfinding.Util
{
	// Token: 0x020005DF RID: 1503
	public class ParallelWorkQueue<T>
	{
		// Token: 0x06002971 RID: 10609 RVA: 0x001BE703 File Offset: 0x001BC903
		public ParallelWorkQueue(Queue<T> queue)
		{
			this.queue = queue;
			this.initialCount = queue.Count;
			this.threadCount = Math.Min(this.initialCount, Math.Max(1, AstarPath.CalculateThreadCount(ThreadCount.AutomaticHighLoad)));
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x001BE73C File Offset: 0x001BC93C
		public IEnumerable<int> Run(int progressTimeoutMillis)
		{
			if (this.initialCount != this.queue.Count)
			{
				throw new InvalidOperationException("Queue has been modified since the constructor");
			}
			if (this.initialCount == 0)
			{
				yield break;
			}
			this.waitEvents = new ManualResetEvent[this.threadCount];
			for (int i = 0; i < this.waitEvents.Length; i++)
			{
				this.waitEvents[i] = new ManualResetEvent(false);
				ThreadPool.QueueUserWorkItem(delegate(object threadIndex)
				{
					this.RunTask((int)threadIndex);
				}, i);
			}
			for (;;)
			{
				WaitHandle[] waitHandles = this.waitEvents;
				if (WaitHandle.WaitAll(waitHandles, progressTimeoutMillis))
				{
					break;
				}
				Queue<T> obj = this.queue;
				int count;
				lock (obj)
				{
					count = this.queue.Count;
				}
				yield return this.initialCount - count;
			}
			if (this.innerException != null)
			{
				throw this.innerException;
			}
			yield break;
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x001BE754 File Offset: 0x001BC954
		private void RunTask(int threadIndex)
		{
			try
			{
				for (;;)
				{
					Queue<T> obj = this.queue;
					T arg;
					lock (obj)
					{
						if (this.queue.Count == 0)
						{
							break;
						}
						arg = this.queue.Dequeue();
					}
					this.action(arg, threadIndex);
				}
			}
			catch (Exception ex)
			{
				this.innerException = ex;
				Queue<T> obj = this.queue;
				lock (obj)
				{
					this.queue.Clear();
				}
			}
			finally
			{
				this.waitEvents[threadIndex].Set();
			}
		}

		// Token: 0x0400432A RID: 17194
		public Action<T, int> action;

		// Token: 0x0400432B RID: 17195
		public readonly int threadCount;

		// Token: 0x0400432C RID: 17196
		private readonly Queue<T> queue;

		// Token: 0x0400432D RID: 17197
		private readonly int initialCount;

		// Token: 0x0400432E RID: 17198
		private ManualResetEvent[] waitEvents;

		// Token: 0x0400432F RID: 17199
		private Exception innerException;
	}
}
