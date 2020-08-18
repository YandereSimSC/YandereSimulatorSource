using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000535 RID: 1333
	internal class WorkItemProcessor : IWorkItemContext
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x0019433C File Offset: 0x0019253C
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x00194344 File Offset: 0x00192544
		public bool workItemsInProgressRightNow { get; private set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x0019434D File Offset: 0x0019254D
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x00194355 File Offset: 0x00192555
		public bool workItemsInProgress { get; private set; }

		// Token: 0x0600233F RID: 9023 RVA: 0x0019435E File Offset: 0x0019255E
		void IWorkItemContext.QueueFloodFill()
		{
			this.queuedWorkItemFloodFill = true;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x00194367 File Offset: 0x00192567
		public void EnsureValidFloodFill()
		{
			if (this.queuedWorkItemFloodFill)
			{
				this.astar.FloodFill();
			}
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x0019437C File Offset: 0x0019257C
		public WorkItemProcessor(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x00194396 File Offset: 0x00192596
		public void OnFloodFill()
		{
			this.queuedWorkItemFloodFill = false;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0019439F File Offset: 0x0019259F
		public void AddWorkItem(AstarWorkItem item)
		{
			this.workItems.Enqueue(item);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x001943B0 File Offset: 0x001925B0
		public bool ProcessWorkItems(bool force)
		{
			if (this.workItemsInProgressRightNow)
			{
				throw new Exception("Processing work items recursively. Please do not wait for other work items to be completed inside work items. If you think this is not caused by any of your scripts, this might be a bug.");
			}
			this.workItemsInProgressRightNow = true;
			this.astar.data.LockGraphStructure(true);
			while (this.workItems.Count > 0)
			{
				if (!this.workItemsInProgress)
				{
					this.workItemsInProgress = true;
					this.queuedWorkItemFloodFill = false;
				}
				AstarWorkItem astarWorkItem = this.workItems[0];
				if (astarWorkItem.init != null)
				{
					astarWorkItem.init();
					astarWorkItem.init = null;
				}
				if (astarWorkItem.initWithContext != null)
				{
					astarWorkItem.initWithContext(this);
					astarWorkItem.initWithContext = null;
				}
				this.workItems[0] = astarWorkItem;
				bool flag;
				try
				{
					if (astarWorkItem.update != null)
					{
						flag = astarWorkItem.update(force);
					}
					else
					{
						flag = (astarWorkItem.updateWithContext == null || astarWorkItem.updateWithContext(this, force));
					}
				}
				catch
				{
					this.workItems.Dequeue();
					this.workItemsInProgressRightNow = false;
					this.astar.data.UnlockGraphStructure();
					throw;
				}
				if (!flag)
				{
					if (force)
					{
						Debug.LogError("Misbehaving WorkItem. 'force'=true but the work item did not complete.\nIf force=true is passed to a WorkItem it should always return true.");
					}
					this.workItemsInProgressRightNow = false;
					this.astar.data.UnlockGraphStructure();
					return false;
				}
				this.workItems.Dequeue();
			}
			this.EnsureValidFloodFill();
			this.workItemsInProgressRightNow = false;
			this.workItemsInProgress = false;
			this.astar.data.UnlockGraphStructure();
			return true;
		}

		// Token: 0x04003F58 RID: 16216
		private readonly AstarPath astar;

		// Token: 0x04003F59 RID: 16217
		private readonly WorkItemProcessor.IndexedQueue<AstarWorkItem> workItems = new WorkItemProcessor.IndexedQueue<AstarWorkItem>();

		// Token: 0x04003F5A RID: 16218
		private bool queuedWorkItemFloodFill;

		// Token: 0x02000723 RID: 1827
		private class IndexedQueue<T>
		{
			// Token: 0x17000676 RID: 1654
			public T this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					return this.buffer[(this.start + index) % this.buffer.Length];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					this.buffer[(this.start + index) % this.buffer.Length] = value;
				}
			}

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x06002CAB RID: 11435 RVA: 0x001CA773 File Offset: 0x001C8973
			// (set) Token: 0x06002CAC RID: 11436 RVA: 0x001CA77B File Offset: 0x001C897B
			public int Count { get; private set; }

			// Token: 0x06002CAD RID: 11437 RVA: 0x001CA784 File Offset: 0x001C8984
			public void Enqueue(T item)
			{
				if (this.Count == this.buffer.Length)
				{
					T[] array = new T[this.buffer.Length * 2];
					for (int i = 0; i < this.Count; i++)
					{
						array[i] = this[i];
					}
					this.buffer = array;
					this.start = 0;
				}
				this.buffer[(this.start + this.Count) % this.buffer.Length] = item;
				int count = this.Count;
				this.Count = count + 1;
			}

			// Token: 0x06002CAE RID: 11438 RVA: 0x001CA810 File Offset: 0x001C8A10
			public T Dequeue()
			{
				if (this.Count == 0)
				{
					throw new InvalidOperationException();
				}
				T result = this.buffer[this.start];
				this.start = (this.start + 1) % this.buffer.Length;
				int count = this.Count;
				this.Count = count - 1;
				return result;
			}

			// Token: 0x04004915 RID: 18709
			private T[] buffer = new T[4];

			// Token: 0x04004916 RID: 18710
			private int start;
		}
	}
}
