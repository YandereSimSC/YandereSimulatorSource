using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000592 RID: 1426
	public class ConstantPath : Path
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x0002291C File Offset: 0x00020B1C
		internal override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x001AA061 File Offset: 0x001A8261
		public static ConstantPath Construct(Vector3 start, int maxGScore, OnPathDelegate callback = null)
		{
			ConstantPath path = PathPool.GetPath<ConstantPath>();
			path.Setup(start, maxGScore, callback);
			return path;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x001AA071 File Offset: 0x001A8271
		protected void Setup(Vector3 start, int maxGScore, OnPathDelegate callback)
		{
			this.callback = callback;
			this.startPoint = start;
			this.originalStartPoint = this.startPoint;
			this.endingCondition = new EndingConditionDistance(this, maxGScore);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x001AA09A File Offset: 0x001A829A
		protected override void OnEnterPool()
		{
			base.OnEnterPool();
			if (this.allNodes != null)
			{
				ListPool<GraphNode>.Release(ref this.allNodes);
			}
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x001AA0B5 File Offset: 0x001A82B5
		protected override void Reset()
		{
			base.Reset();
			this.allNodes = ListPool<GraphNode>.Claim();
			this.endingCondition = null;
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x001AA0F4 File Offset: 0x001A82F4
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			this.startNode = nearest.node;
			if (this.startNode == null)
			{
				base.FailWithError("Could not find close node to the start point");
				return;
			}
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x001AA14C File Offset: 0x001A834C
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			pathNode.flag1 = true;
			this.allNodes.Add(this.startNode);
			if (this.pathHandler.heap.isEmpty)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x001AA224 File Offset: 0x001A8424
		protected override void Cleanup()
		{
			int count = this.allNodes.Count;
			for (int i = 0; i < count; i++)
			{
				this.pathHandler.GetPathNode(this.allNodes[i]).flag1 = false;
			}
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x001AA268 File Offset: 0x001A8468
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.endingCondition.TargetFound(this.currentR))
				{
					base.CompleteState = PathCompleteState.Complete;
					return;
				}
				if (!this.currentR.flag1)
				{
					this.allNodes.Add(this.currentR.node);
					this.currentR.flag1 = true;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					base.CompleteState = PathCompleteState.Complete;
					return;
				}
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

		// Token: 0x04004193 RID: 16787
		public GraphNode startNode;

		// Token: 0x04004194 RID: 16788
		public Vector3 startPoint;

		// Token: 0x04004195 RID: 16789
		public Vector3 originalStartPoint;

		// Token: 0x04004196 RID: 16790
		public List<GraphNode> allNodes;

		// Token: 0x04004197 RID: 16791
		public PathEndingCondition endingCondition;
	}
}
