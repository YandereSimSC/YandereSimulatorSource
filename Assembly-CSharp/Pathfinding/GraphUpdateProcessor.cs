using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000526 RID: 1318
	internal class GraphUpdateProcessor
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06002288 RID: 8840 RVA: 0x00190638 File Offset: 0x0018E838
		// (remove) Token: 0x06002289 RID: 8841 RVA: 0x00190670 File Offset: 0x0018E870
		public event Action OnGraphsUpdated;

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x001906A5 File Offset: 0x0018E8A5
		public bool IsAnyGraphUpdateQueued
		{
			get
			{
				return this.graphUpdateQueue.Count > 0;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x001906B5 File Offset: 0x0018E8B5
		public bool IsAnyGraphUpdateInProgress
		{
			get
			{
				return this.anyGraphUpdateInProgress;
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x001906C0 File Offset: 0x0018E8C0
		public GraphUpdateProcessor(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0019072A File Offset: 0x0018E92A
		public AstarWorkItem GetWorkItem()
		{
			return new AstarWorkItem(new Action(this.QueueGraphUpdatesInternal), new Func<bool, bool>(this.ProcessGraphUpdates));
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0019074C File Offset: 0x0018E94C
		public void EnableMultithreading()
		{
			if (this.graphUpdateThread == null || !this.graphUpdateThread.IsAlive)
			{
				this.graphUpdateThread = new Thread(new ThreadStart(this.ProcessGraphUpdatesAsync));
				this.graphUpdateThread.IsBackground = true;
				this.graphUpdateThread.Priority = System.Threading.ThreadPriority.Lowest;
				this.graphUpdateThread.Start();
			}
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x001907A8 File Offset: 0x0018E9A8
		public void DisableMultithreading()
		{
			if (this.graphUpdateThread != null && this.graphUpdateThread.IsAlive)
			{
				this.exitAsyncThread.Set();
				if (!this.graphUpdateThread.Join(5000))
				{
					Debug.LogError("Graph update thread did not exit in 5 seconds");
				}
				this.graphUpdateThread = null;
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x001907F9 File Offset: 0x0018E9F9
		public void AddToQueue(GraphUpdateObject ob)
		{
			this.graphUpdateQueue.Enqueue(ob);
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x00190808 File Offset: 0x0018EA08
		private void QueueGraphUpdatesInternal()
		{
			bool flag = false;
			while (this.graphUpdateQueue.Count > 0)
			{
				GraphUpdateObject graphUpdateObject = this.graphUpdateQueue.Dequeue();
				if (graphUpdateObject.requiresFloodFill)
				{
					flag = true;
				}
				foreach (object obj in this.astar.data.GetUpdateableGraphs())
				{
					IUpdatableGraph updatableGraph = (IUpdatableGraph)obj;
					NavGraph graph = updatableGraph as NavGraph;
					if (graphUpdateObject.nnConstraint == null || graphUpdateObject.nnConstraint.SuitableGraph(this.astar.data.GetGraphIndex(graph), graph))
					{
						GraphUpdateProcessor.GUOSingle item = default(GraphUpdateProcessor.GUOSingle);
						item.order = GraphUpdateProcessor.GraphUpdateOrder.GraphUpdate;
						item.obj = graphUpdateObject;
						item.graph = updatableGraph;
						this.graphUpdateQueueRegular.Enqueue(item);
					}
				}
			}
			if (flag)
			{
				GraphUpdateProcessor.GUOSingle item2 = default(GraphUpdateProcessor.GUOSingle);
				item2.order = GraphUpdateProcessor.GraphUpdateOrder.FloodFill;
				this.graphUpdateQueueRegular.Enqueue(item2);
			}
			GraphModifier.TriggerEvent(GraphModifier.EventType.PreUpdate);
			this.anyGraphUpdateInProgress = true;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x00190920 File Offset: 0x0018EB20
		private bool ProcessGraphUpdates(bool force)
		{
			if (force)
			{
				this.asyncGraphUpdatesComplete.WaitOne();
			}
			else if (!this.asyncGraphUpdatesComplete.WaitOne(0))
			{
				return false;
			}
			this.ProcessPostUpdates();
			if (!this.ProcessRegularUpdates(force))
			{
				return false;
			}
			GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdate);
			if (this.OnGraphsUpdated != null)
			{
				this.OnGraphsUpdated();
			}
			this.anyGraphUpdateInProgress = false;
			return true;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00190984 File Offset: 0x0018EB84
		private bool ProcessRegularUpdates(bool force)
		{
			while (this.graphUpdateQueueRegular.Count > 0)
			{
				GraphUpdateProcessor.GUOSingle guosingle = this.graphUpdateQueueRegular.Peek();
				GraphUpdateThreading graphUpdateThreading = (guosingle.order == GraphUpdateProcessor.GraphUpdateOrder.FloodFill) ? GraphUpdateThreading.SeparateThread : guosingle.graph.CanUpdateAsync(guosingle.obj);
				if (force || !Application.isPlaying || this.graphUpdateThread == null || !this.graphUpdateThread.IsAlive)
				{
					graphUpdateThreading &= (GraphUpdateThreading)(-2);
				}
				if ((graphUpdateThreading & GraphUpdateThreading.UnityInit) != GraphUpdateThreading.UnityThread)
				{
					if (this.StartAsyncUpdatesIfQueued())
					{
						return false;
					}
					guosingle.graph.UpdateAreaInit(guosingle.obj);
				}
				if ((graphUpdateThreading & GraphUpdateThreading.SeparateThread) != GraphUpdateThreading.UnityThread)
				{
					this.graphUpdateQueueRegular.Dequeue();
					this.graphUpdateQueueAsync.Enqueue(guosingle);
					if ((graphUpdateThreading & GraphUpdateThreading.UnityPost) != GraphUpdateThreading.UnityThread && this.StartAsyncUpdatesIfQueued())
					{
						return false;
					}
				}
				else
				{
					if (this.StartAsyncUpdatesIfQueued())
					{
						return false;
					}
					this.graphUpdateQueueRegular.Dequeue();
					if (guosingle.order == GraphUpdateProcessor.GraphUpdateOrder.FloodFill)
					{
						this.FloodFill();
					}
					else
					{
						try
						{
							guosingle.graph.UpdateArea(guosingle.obj);
						}
						catch (Exception arg)
						{
							Debug.LogError("Error while updating graphs\n" + arg);
						}
					}
					if ((graphUpdateThreading & GraphUpdateThreading.UnityPost) != GraphUpdateThreading.UnityThread)
					{
						guosingle.graph.UpdateAreaPost(guosingle.obj);
					}
				}
			}
			return !this.StartAsyncUpdatesIfQueued();
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x00190AC0 File Offset: 0x0018ECC0
		private bool StartAsyncUpdatesIfQueued()
		{
			if (this.graphUpdateQueueAsync.Count > 0)
			{
				this.asyncGraphUpdatesComplete.Reset();
				this.graphUpdateAsyncEvent.Set();
				return true;
			}
			return false;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x00190AEC File Offset: 0x0018ECEC
		private void ProcessPostUpdates()
		{
			while (this.graphUpdateQueuePost.Count > 0)
			{
				GraphUpdateProcessor.GUOSingle guosingle = this.graphUpdateQueuePost.Dequeue();
				if ((guosingle.graph.CanUpdateAsync(guosingle.obj) & GraphUpdateThreading.UnityPost) != GraphUpdateThreading.UnityThread)
				{
					try
					{
						guosingle.graph.UpdateAreaPost(guosingle.obj);
					}
					catch (Exception arg)
					{
						Debug.LogError("Error while updating graphs (post step)\n" + arg);
					}
				}
			}
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x00190B60 File Offset: 0x0018ED60
		private void ProcessGraphUpdatesAsync()
		{
			AutoResetEvent[] array = new AutoResetEvent[]
			{
				this.graphUpdateAsyncEvent,
				this.exitAsyncThread
			};
			for (;;)
			{
				WaitHandle[] waitHandles = array;
				if (WaitHandle.WaitAny(waitHandles) == 1)
				{
					break;
				}
				while (this.graphUpdateQueueAsync.Count > 0)
				{
					GraphUpdateProcessor.GUOSingle guosingle = this.graphUpdateQueueAsync.Dequeue();
					try
					{
						if (guosingle.order == GraphUpdateProcessor.GraphUpdateOrder.GraphUpdate)
						{
							guosingle.graph.UpdateArea(guosingle.obj);
							this.graphUpdateQueuePost.Enqueue(guosingle);
						}
						else
						{
							if (guosingle.order != GraphUpdateProcessor.GraphUpdateOrder.FloodFill)
							{
								throw new NotSupportedException(string.Concat(guosingle.order));
							}
							this.FloodFill();
						}
					}
					catch (Exception arg)
					{
						Debug.LogError("Exception while updating graphs:\n" + arg);
					}
				}
				this.asyncGraphUpdatesComplete.Set();
			}
			this.graphUpdateQueueAsync.Clear();
			this.asyncGraphUpdatesComplete.Set();
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x00190C4C File Offset: 0x0018EE4C
		public void FloodFill(GraphNode seed)
		{
			this.FloodFill(seed, this.lastUniqueAreaIndex + 1u);
			this.lastUniqueAreaIndex += 1u;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00190C6C File Offset: 0x0018EE6C
		public void FloodFill(GraphNode seed, uint area)
		{
			if (area > 131071u)
			{
				Debug.LogError("Too high area index - The maximum area index is " + 131071u);
				return;
			}
			if (area < 0u)
			{
				Debug.LogError("Too low area index - The minimum area index is 0");
				return;
			}
			Stack<GraphNode> stack = StackPool<GraphNode>.Claim();
			stack.Push(seed);
			seed.Area = area;
			while (stack.Count > 0)
			{
				stack.Pop().FloodFill(stack, area);
			}
			StackPool<GraphNode>.Release(stack);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x00190CDC File Offset: 0x0018EEDC
		public void FloodFill()
		{
			NavGraph[] graphs = this.astar.graphs;
			if (graphs == null)
			{
				return;
			}
			foreach (NavGraph navGraph in graphs)
			{
				if (navGraph != null)
				{
					navGraph.GetNodes(delegate(GraphNode node)
					{
						node.Area = 0u;
					});
				}
			}
			this.lastUniqueAreaIndex = 0u;
			uint area = 0u;
			int forcedSmallAreas = 0;
			Stack<GraphNode> stack = StackPool<GraphNode>.Claim();
			Action<GraphNode> <>9__1;
			foreach (NavGraph navGraph2 in graphs)
			{
				if (navGraph2 != null)
				{
					NavGraph navGraph3 = navGraph2;
					Action<GraphNode> action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate(GraphNode node)
						{
							if (node.Walkable && node.Area == 0u)
							{
								uint area = area;
								area += 1u;
								uint area2 = area;
								if (area > 131071u)
								{
									area = area;
									area -= 1u;
									area2 = area;
									int forcedSmallAreas;
									if (forcedSmallAreas == 0)
									{
										forcedSmallAreas = 1;
									}
									forcedSmallAreas = forcedSmallAreas;
									forcedSmallAreas++;
								}
								stack.Clear();
								stack.Push(node);
								int num = 1;
								node.Area = area2;
								while (stack.Count > 0)
								{
									num++;
									stack.Pop().FloodFill(stack, area2);
								}
							}
						});
					}
					navGraph3.GetNodes(action);
				}
			}
			this.lastUniqueAreaIndex = area;
			if (forcedSmallAreas > 0)
			{
				Debug.LogError(string.Concat(new object[]
				{
					forcedSmallAreas,
					" areas had to share IDs. This usually doesn't affect pathfinding in any significant way (you might get 'Searched whole area but could not find target' as a reason for path failure) however some path requests may take longer to calculate (specifically those that fail with the 'Searched whole area' error).The maximum number of areas is ",
					131071u,
					"."
				}));
			}
			StackPool<GraphNode>.Release(stack);
		}

		// Token: 0x04003F04 RID: 16132
		private readonly AstarPath astar;

		// Token: 0x04003F05 RID: 16133
		private Thread graphUpdateThread;

		// Token: 0x04003F06 RID: 16134
		private bool anyGraphUpdateInProgress;

		// Token: 0x04003F07 RID: 16135
		private readonly Queue<GraphUpdateObject> graphUpdateQueue = new Queue<GraphUpdateObject>();

		// Token: 0x04003F08 RID: 16136
		private readonly Queue<GraphUpdateProcessor.GUOSingle> graphUpdateQueueAsync = new Queue<GraphUpdateProcessor.GUOSingle>();

		// Token: 0x04003F09 RID: 16137
		private readonly Queue<GraphUpdateProcessor.GUOSingle> graphUpdateQueuePost = new Queue<GraphUpdateProcessor.GUOSingle>();

		// Token: 0x04003F0A RID: 16138
		private readonly Queue<GraphUpdateProcessor.GUOSingle> graphUpdateQueueRegular = new Queue<GraphUpdateProcessor.GUOSingle>();

		// Token: 0x04003F0B RID: 16139
		private readonly ManualResetEvent asyncGraphUpdatesComplete = new ManualResetEvent(true);

		// Token: 0x04003F0C RID: 16140
		private readonly AutoResetEvent graphUpdateAsyncEvent = new AutoResetEvent(false);

		// Token: 0x04003F0D RID: 16141
		private readonly AutoResetEvent exitAsyncThread = new AutoResetEvent(false);

		// Token: 0x04003F0E RID: 16142
		private uint lastUniqueAreaIndex;

		// Token: 0x02000719 RID: 1817
		private enum GraphUpdateOrder
		{
			// Token: 0x040048F6 RID: 18678
			GraphUpdate,
			// Token: 0x040048F7 RID: 18679
			FloodFill
		}

		// Token: 0x0200071A RID: 1818
		private struct GUOSingle
		{
			// Token: 0x040048F8 RID: 18680
			public GraphUpdateProcessor.GraphUpdateOrder order;

			// Token: 0x040048F9 RID: 18681
			public IUpdatableGraph graph;

			// Token: 0x040048FA RID: 18682
			public GraphUpdateObject obj;
		}
	}
}
