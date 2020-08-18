using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000599 RID: 1433
	public class RandomPath : ABPath
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x0002291C File Offset: 0x00020B1C
		internal override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x0002D171 File Offset: 0x0002B371
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x001AB818 File Offset: 0x001A9A18
		protected override void Reset()
		{
			base.Reset();
			this.searchLength = 5000;
			this.spread = 5000;
			this.aimStrength = 0f;
			this.chosenNodeR = null;
			this.maxGScoreNodeR = null;
			this.maxGScore = 0;
			this.aim = Vector3.zero;
			this.nodesEvaluatedRep = 0;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x001AB873 File Offset: 0x001A9A73
		public RandomPath()
		{
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x001AB891 File Offset: 0x001A9A91
		[Obsolete("This constructor is obsolete. Please use the pooling API and the Construct methods")]
		public RandomPath(Vector3 start, int length, OnPathDelegate callback = null)
		{
			throw new Exception("This constructor is obsolete. Please use the pooling API and the Setup methods");
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x001AB8B9 File Offset: 0x001A9AB9
		public static RandomPath Construct(Vector3 start, int length, OnPathDelegate callback = null)
		{
			RandomPath path = PathPool.GetPath<RandomPath>();
			path.Setup(start, length, callback);
			return path;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x001AB8CC File Offset: 0x001A9ACC
		protected RandomPath Setup(Vector3 start, int length, OnPathDelegate callback)
		{
			this.callback = callback;
			this.searchLength = length;
			this.originalStartPoint = start;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = start;
			this.endPoint = Vector3.zero;
			this.startIntPoint = (Int3)start;
			return this;
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x001AB918 File Offset: 0x001A9B18
		protected override void ReturnPath()
		{
			if (this.path != null && this.path.Count > 0)
			{
				this.endNode = this.path[this.path.Count - 1];
				this.endPoint = (Vector3)this.endNode.position;
				this.originalEndPoint = this.endPoint;
				this.hTarget = this.endNode.position;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x001AB9A0 File Offset: 0x001A9BA0
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			this.startPoint = nearest.position;
			this.endPoint = this.startPoint;
			this.startIntPoint = (Int3)this.startPoint;
			this.hTarget = (Int3)this.aim;
			this.startNode = nearest.node;
			this.endNode = this.startNode;
			if (this.startNode == null || this.endNode == null)
			{
				base.FailWithError("Couldn't find close nodes to the start point");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			this.heuristicScale = this.aimStrength;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x001ABA6C File Offset: 0x001A9C6C
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			if (this.searchLength + this.spread <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.Trace(pathNode);
				return;
			}
			pathNode.pathID = base.pathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.heap.isEmpty)
			{
				base.FailWithError("No open points, the start node didn't open any nodes");
				return;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x001ABB4C File Offset: 0x001A9D4C
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if ((ulong)this.currentR.G >= (ulong)((long)this.searchLength))
				{
					if ((ulong)this.currentR.G > (ulong)((long)(this.searchLength + this.spread)))
					{
						if (this.chosenNodeR == null)
						{
							this.chosenNodeR = this.currentR;
						}
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					this.nodesEvaluatedRep++;
					if (this.rnd.NextDouble() <= (double)(1f / (float)this.nodesEvaluatedRep))
					{
						this.chosenNodeR = this.currentR;
					}
				}
				else if ((ulong)this.currentR.G > (ulong)((long)this.maxGScore))
				{
					this.maxGScore = (int)this.currentR.G;
					this.maxGScoreNodeR = this.currentR;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					if (this.chosenNodeR != null)
					{
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					if (this.maxGScoreNodeR != null)
					{
						this.chosenNodeR = this.maxGScoreNodeR;
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					base.FailWithError("Not a single node found to search");
					break;
				}
				else
				{
					this.currentR = this.pathHandler.heap.Remove();
					if (num > 500)
					{
						if (DateTime.UtcNow.Ticks >= targetTick)
						{
							return;
						}
						num = 0;
						if (this.searchedNodes > 1000000)
						{
							throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
						}
					}
					num++;
				}
			}
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.Trace(this.chosenNodeR);
			}
		}

		// Token: 0x040041AD RID: 16813
		public int searchLength;

		// Token: 0x040041AE RID: 16814
		public int spread = 5000;

		// Token: 0x040041AF RID: 16815
		public float aimStrength;

		// Token: 0x040041B0 RID: 16816
		private PathNode chosenNodeR;

		// Token: 0x040041B1 RID: 16817
		private PathNode maxGScoreNodeR;

		// Token: 0x040041B2 RID: 16818
		private int maxGScore;

		// Token: 0x040041B3 RID: 16819
		public Vector3 aim;

		// Token: 0x040041B4 RID: 16820
		private int nodesEvaluatedRep;

		// Token: 0x040041B5 RID: 16821
		private readonly System.Random rnd = new System.Random();
	}
}
