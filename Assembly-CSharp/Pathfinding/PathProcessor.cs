using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000530 RID: 1328
	public class PathProcessor
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600230F RID: 8975 RVA: 0x001934A8 File Offset: 0x001916A8
		// (remove) Token: 0x06002310 RID: 8976 RVA: 0x001934E0 File Offset: 0x001916E0
		public event Action<Path> OnPathPreSearch;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06002311 RID: 8977 RVA: 0x00193518 File Offset: 0x00191718
		// (remove) Token: 0x06002312 RID: 8978 RVA: 0x00193550 File Offset: 0x00191750
		public event Action<Path> OnPathPostSearch;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06002313 RID: 8979 RVA: 0x00193588 File Offset: 0x00191788
		// (remove) Token: 0x06002314 RID: 8980 RVA: 0x001935C0 File Offset: 0x001917C0
		public event Action OnQueueUnblocked;

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x001935F5 File Offset: 0x001917F5
		public int NumThreads
		{
			get
			{
				return this.pathHandlers.Length;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x001935FF File Offset: 0x001917FF
		public bool IsUsingMultithreading
		{
			get
			{
				return this.threads != null;
			}
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x0019360C File Offset: 0x0019180C
		internal PathProcessor(AstarPath astar, PathReturnQueue returnQueue, int processors, bool multithreaded)
		{
			this.astar = astar;
			this.returnQueue = returnQueue;
			if (processors < 0)
			{
				throw new ArgumentOutOfRangeException("processors");
			}
			if (!multithreaded && processors != 1)
			{
				throw new Exception("Only a single non-multithreaded processor is allowed");
			}
			this.queue = new ThreadControlQueue(processors);
			this.pathHandlers = new PathHandler[processors];
			for (int i = 0; i < processors; i++)
			{
				this.pathHandlers[i] = new PathHandler(i, processors);
			}
			if (multithreaded)
			{
				this.threads = new Thread[processors];
				for (int j = 0; j < processors; j++)
				{
					PathHandler pathHandler = this.pathHandlers[j];
					this.threads[j] = new Thread(delegate()
					{
						this.CalculatePathsThreaded(pathHandler);
					});
					this.threads[j].Name = "Pathfinding Thread " + j;
					this.threads[j].IsBackground = true;
					this.threads[j].Start();
				}
				return;
			}
			this.threadCoroutine = this.CalculatePaths(this.pathHandlers[0]);
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00193740 File Offset: 0x00191940
		private int Lock(bool block)
		{
			this.queue.Block();
			if (block)
			{
				while (!this.queue.AllReceiversBlocked)
				{
					if (this.IsUsingMultithreading)
					{
						Thread.Sleep(1);
					}
					else
					{
						this.TickNonMultithreaded();
					}
				}
			}
			this.nextLockID++;
			this.locks.Add(this.nextLockID);
			return this.nextLockID;
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x001937A8 File Offset: 0x001919A8
		private void Unlock(int id)
		{
			if (!this.locks.Remove(id))
			{
				throw new ArgumentException("This lock has already been released");
			}
			if (this.locks.Count == 0)
			{
				if (this.OnQueueUnblocked != null)
				{
					this.OnQueueUnblocked();
				}
				this.queue.Unblock();
			}
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x001937F9 File Offset: 0x001919F9
		public PathProcessor.GraphUpdateLock PausePathfinding(bool block)
		{
			return new PathProcessor.GraphUpdateLock(this, block);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00193804 File Offset: 0x00191A04
		public void TickNonMultithreaded()
		{
			if (this.threadCoroutine != null)
			{
				try
				{
					this.threadCoroutine.MoveNext();
				}
				catch (Exception ex)
				{
					this.threadCoroutine = null;
					if (!(ex is ThreadControlQueue.QueueTerminationException))
					{
						Debug.LogException(ex);
						Debug.LogError("Unhandled exception during pathfinding. Terminating.");
						this.queue.TerminateReceivers();
						try
						{
							this.queue.PopNoBlock(false);
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00193884 File Offset: 0x00191A84
		public void JoinThreads()
		{
			if (this.threads != null)
			{
				for (int i = 0; i < this.threads.Length; i++)
				{
					if (!this.threads[i].Join(50))
					{
						Debug.LogError("Could not terminate pathfinding thread[" + i + "] in 50ms, trying Thread.Abort");
						this.threads[i].Abort();
					}
				}
			}
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x001938E4 File Offset: 0x00191AE4
		public void AbortThreads()
		{
			if (this.threads == null)
			{
				return;
			}
			for (int i = 0; i < this.threads.Length; i++)
			{
				if (this.threads[i] != null && this.threads[i].IsAlive)
				{
					this.threads[i].Abort();
				}
			}
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00193934 File Offset: 0x00191B34
		public int GetNewNodeIndex()
		{
			if (this.nodeIndexPool.Count <= 0)
			{
				int num = this.nextNodeIndex;
				this.nextNodeIndex = num + 1;
				return num;
			}
			return this.nodeIndexPool.Pop();
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x0019396C File Offset: 0x00191B6C
		public void InitializeNode(GraphNode node)
		{
			if (!this.queue.AllReceiversBlocked)
			{
				throw new Exception("Trying to initialize a node when it is not safe to initialize any nodes. Must be done during a graph update. See http://arongranberg.com/astar/docs/graph-updates.php#direct");
			}
			for (int i = 0; i < this.pathHandlers.Length; i++)
			{
				this.pathHandlers[i].InitializeNode(node);
			}
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x001939B4 File Offset: 0x00191BB4
		public void DestroyNode(GraphNode node)
		{
			if (node.NodeIndex == -1)
			{
				return;
			}
			this.nodeIndexPool.Push(node.NodeIndex);
			for (int i = 0; i < this.pathHandlers.Length; i++)
			{
				this.pathHandlers[i].DestroyNode(node);
			}
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x00193A00 File Offset: 0x00191C00
		private void CalculatePathsThreaded(PathHandler pathHandler)
		{
			try
			{
				long num = 100000L;
				long targetTick = DateTime.UtcNow.Ticks + num;
				for (;;)
				{
					Path path = this.queue.Pop();
					IPathInternals pathInternals = path;
					pathInternals.PrepareBase(pathHandler);
					pathInternals.AdvanceState(PathState.Processing);
					if (this.OnPathPreSearch != null)
					{
						this.OnPathPreSearch(path);
					}
					long ticks = DateTime.UtcNow.Ticks;
					pathInternals.Prepare();
					if (!path.IsDone())
					{
						this.astar.debugPathData = pathInternals.PathHandler;
						this.astar.debugPathID = path.pathID;
						pathInternals.Initialize();
						while (!path.IsDone())
						{
							pathInternals.CalculateStep(targetTick);
							targetTick = DateTime.UtcNow.Ticks + num;
							if (this.queue.IsTerminating)
							{
								path.FailWithError("AstarPath object destroyed");
							}
						}
						path.duration = (float)(DateTime.UtcNow.Ticks - ticks) * 0.0001f;
					}
					pathInternals.Cleanup();
					if (path.immediateCallback != null)
					{
						path.immediateCallback(path);
					}
					if (this.OnPathPostSearch != null)
					{
						this.OnPathPostSearch(path);
					}
					this.returnQueue.Enqueue(path);
					pathInternals.AdvanceState(PathState.ReturnQueue);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is ThreadControlQueue.QueueTerminationException)
				{
					if (this.astar.logPathResults == PathLog.Heavy)
					{
						Debug.LogWarning("Shutting down pathfinding thread #" + pathHandler.threadID);
					}
					return;
				}
				Debug.LogException(ex);
				Debug.LogError("Unhandled exception during pathfinding. Terminating.");
				this.queue.TerminateReceivers();
			}
			Debug.LogError("Error : This part should never be reached.");
			this.queue.ReceiverTerminated();
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x00193BD0 File Offset: 0x00191DD0
		private IEnumerator CalculatePaths(PathHandler pathHandler)
		{
			long maxTicks = (long)(this.astar.maxFrameTime * 10000f);
			long targetTick = DateTime.UtcNow.Ticks + maxTicks;
			for (;;)
			{
				Path p = null;
				bool blockedBefore = false;
				while (p == null)
				{
					try
					{
						p = this.queue.PopNoBlock(blockedBefore);
						blockedBefore |= (p == null);
					}
					catch (ThreadControlQueue.QueueTerminationException)
					{
						yield break;
					}
					if (p == null)
					{
						yield return null;
					}
				}
				IPathInternals ip = p;
				maxTicks = (long)(this.astar.maxFrameTime * 10000f);
				ip.PrepareBase(pathHandler);
				ip.AdvanceState(PathState.Processing);
				Action<Path> onPathPreSearch = this.OnPathPreSearch;
				if (onPathPreSearch != null)
				{
					onPathPreSearch(p);
				}
				long ticks = DateTime.UtcNow.Ticks;
				long totalTicks = 0L;
				ip.Prepare();
				if (!p.IsDone())
				{
					this.astar.debugPathData = ip.PathHandler;
					this.astar.debugPathID = p.pathID;
					ip.Initialize();
					while (!p.IsDone())
					{
						ip.CalculateStep(targetTick);
						if (p.IsDone())
						{
							break;
						}
						totalTicks += DateTime.UtcNow.Ticks - ticks;
						yield return null;
						ticks = DateTime.UtcNow.Ticks;
						if (this.queue.IsTerminating)
						{
							p.FailWithError("AstarPath object destroyed");
						}
						targetTick = DateTime.UtcNow.Ticks + maxTicks;
					}
					totalTicks += DateTime.UtcNow.Ticks - ticks;
					p.duration = (float)totalTicks * 0.0001f;
				}
				ip.Cleanup();
				OnPathDelegate immediateCallback = p.immediateCallback;
				if (immediateCallback != null)
				{
					immediateCallback(p);
				}
				Action<Path> onPathPostSearch = this.OnPathPostSearch;
				if (onPathPostSearch != null)
				{
					onPathPostSearch(p);
				}
				this.returnQueue.Enqueue(p);
				ip.AdvanceState(PathState.ReturnQueue);
				if (DateTime.UtcNow.Ticks > targetTick)
				{
					yield return null;
					targetTick = DateTime.UtcNow.Ticks + maxTicks;
				}
				p = null;
				ip = null;
			}
			yield break;
		}

		// Token: 0x04003F3E RID: 16190
		internal readonly ThreadControlQueue queue;

		// Token: 0x04003F3F RID: 16191
		private readonly AstarPath astar;

		// Token: 0x04003F40 RID: 16192
		private readonly PathReturnQueue returnQueue;

		// Token: 0x04003F41 RID: 16193
		private readonly PathHandler[] pathHandlers;

		// Token: 0x04003F42 RID: 16194
		private readonly Thread[] threads;

		// Token: 0x04003F43 RID: 16195
		private IEnumerator threadCoroutine;

		// Token: 0x04003F44 RID: 16196
		private int nextNodeIndex = 1;

		// Token: 0x04003F45 RID: 16197
		private readonly Stack<int> nodeIndexPool = new Stack<int>();

		// Token: 0x04003F46 RID: 16198
		private readonly List<int> locks = new List<int>();

		// Token: 0x04003F47 RID: 16199
		private int nextLockID;

		// Token: 0x0200071F RID: 1823
		public struct GraphUpdateLock
		{
			// Token: 0x06002C9D RID: 11421 RVA: 0x001CA345 File Offset: 0x001C8545
			public GraphUpdateLock(PathProcessor pathProcessor, bool block)
			{
				this.pathProcessor = pathProcessor;
				this.id = pathProcessor.Lock(block);
			}

			// Token: 0x17000673 RID: 1651
			// (get) Token: 0x06002C9E RID: 11422 RVA: 0x001CA35B File Offset: 0x001C855B
			public bool Held
			{
				get
				{
					return this.pathProcessor != null && this.pathProcessor.locks.Contains(this.id);
				}
			}

			// Token: 0x06002C9F RID: 11423 RVA: 0x001CA37D File Offset: 0x001C857D
			public void Release()
			{
				this.pathProcessor.Unlock(this.id);
			}

			// Token: 0x04004907 RID: 18695
			private PathProcessor pathProcessor;

			// Token: 0x04004908 RID: 18696
			private int id;
		}
	}
}
