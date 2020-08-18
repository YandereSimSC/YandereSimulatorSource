using System;
using System.Threading;

namespace Pathfinding
{
	// Token: 0x02000532 RID: 1330
	internal class ThreadControlQueue
	{
		// Token: 0x06002326 RID: 8998 RVA: 0x00193CFC File Offset: 0x00191EFC
		public ThreadControlQueue(int numReceivers)
		{
			this.numReceivers = numReceivers;
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x00193D22 File Offset: 0x00191F22
		public bool IsEmpty
		{
			get
			{
				return this.head == null;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x00193D2D File Offset: 0x00191F2D
		public bool IsTerminating
		{
			get
			{
				return this.terminate;
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x00193D38 File Offset: 0x00191F38
		public void Block()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = true;
				this.block.Reset();
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x00193D88 File Offset: 0x00191F88
		public void Unblock()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = false;
				this.block.Set();
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x00193DD8 File Offset: 0x00191FD8
		public void Lock()
		{
			Monitor.Enter(this.lockObj);
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00193DE5 File Offset: 0x00191FE5
		public void Unlock()
		{
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x00193DF4 File Offset: 0x00191FF4
		public bool AllReceiversBlocked
		{
			get
			{
				object obj = this.lockObj;
				bool result;
				lock (obj)
				{
					result = (this.blocked && this.blockedReceivers == this.numReceivers);
				}
				return result;
			}
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00193E4C File Offset: 0x0019204C
		public void PushFront(Path path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (!this.terminate)
				{
					if (this.tail == null)
					{
						this.head = path;
						this.tail = path;
						if (this.starving && !this.blocked)
						{
							this.starving = false;
							this.block.Set();
						}
						else
						{
							this.starving = false;
						}
					}
					else
					{
						path.next = this.head;
						this.head = path;
					}
				}
			}
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00193EE8 File Offset: 0x001920E8
		public void Push(Path path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (!this.terminate)
				{
					if (this.tail == null)
					{
						this.head = path;
						this.tail = path;
						if (this.starving && !this.blocked)
						{
							this.starving = false;
							this.block.Set();
						}
						else
						{
							this.starving = false;
						}
					}
					else
					{
						this.tail.next = path;
						this.tail = path;
					}
				}
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00193F84 File Offset: 0x00192184
		private void Starving()
		{
			this.starving = true;
			this.block.Reset();
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x00193F9C File Offset: 0x0019219C
		public void TerminateReceivers()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.terminate = true;
				this.block.Set();
			}
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00193FEC File Offset: 0x001921EC
		public Path Pop()
		{
			Path result;
			lock (this.lockObj)
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				while (this.blocked || this.starving)
				{
					this.blockedReceivers++;
					if (this.blockedReceivers > this.numReceivers)
					{
						throw new InvalidOperationException(string.Concat(new object[]
						{
							"More receivers are blocked than specified in constructor (",
							this.blockedReceivers,
							" > ",
							this.numReceivers,
							")"
						}));
					}
					Monitor.Exit(this.lockObj);
					this.block.WaitOne();
					Monitor.Enter(this.lockObj);
					if (this.terminate)
					{
						throw new ThreadControlQueue.QueueTerminationException();
					}
					this.blockedReceivers--;
					if (this.head == null)
					{
						this.Starving();
					}
				}
				Path path = this.head;
				Path next = this.head.next;
				if (next == null)
				{
					this.tail = null;
				}
				this.head.next = null;
				this.head = next;
				result = path;
			}
			return result;
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00194154 File Offset: 0x00192354
		public void ReceiverTerminated()
		{
			Monitor.Enter(this.lockObj);
			this.blockedReceivers++;
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x0019417C File Offset: 0x0019237C
		public Path PopNoBlock(bool blockedBefore)
		{
			Path result;
			lock (this.lockObj)
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				if (this.blocked || this.starving)
				{
					if (!blockedBefore)
					{
						this.blockedReceivers++;
						if (this.terminate)
						{
							throw new ThreadControlQueue.QueueTerminationException();
						}
						if (this.blockedReceivers != this.numReceivers && this.blockedReceivers > this.numReceivers)
						{
							throw new InvalidOperationException(string.Concat(new object[]
							{
								"More receivers are blocked than specified in constructor (",
								this.blockedReceivers,
								" > ",
								this.numReceivers,
								")"
							}));
						}
					}
					result = null;
				}
				else
				{
					if (blockedBefore)
					{
						this.blockedReceivers--;
					}
					Path path = this.head;
					Path next = this.head.next;
					if (next == null)
					{
						this.tail = null;
					}
					this.head.next = null;
					this.head = next;
					result = path;
				}
			}
			return result;
		}

		// Token: 0x04003F4A RID: 16202
		private Path head;

		// Token: 0x04003F4B RID: 16203
		private Path tail;

		// Token: 0x04003F4C RID: 16204
		private readonly object lockObj = new object();

		// Token: 0x04003F4D RID: 16205
		private readonly int numReceivers;

		// Token: 0x04003F4E RID: 16206
		private bool blocked;

		// Token: 0x04003F4F RID: 16207
		private int blockedReceivers;

		// Token: 0x04003F50 RID: 16208
		private bool starving;

		// Token: 0x04003F51 RID: 16209
		private bool terminate;

		// Token: 0x04003F52 RID: 16210
		private ManualResetEvent block = new ManualResetEvent(true);

		// Token: 0x02000722 RID: 1826
		public class QueueTerminationException : Exception
		{
		}
	}
}
