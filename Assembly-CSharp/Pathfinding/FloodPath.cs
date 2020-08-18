using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000595 RID: 1429
	public class FloodPath : Path
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x0002291C File Offset: 0x00020B1C
		internal override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x001AA3F9 File Offset: 0x001A85F9
		public bool HasPathTo(GraphNode node)
		{
			return this.parents != null && this.parents.ContainsKey(node);
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x001AA411 File Offset: 0x001A8611
		public GraphNode GetParent(GraphNode node)
		{
			return this.parents[node];
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x001AA42E File Offset: 0x001A862E
		public static FloodPath Construct(Vector3 start, OnPathDelegate callback = null)
		{
			FloodPath path = PathPool.GetPath<FloodPath>();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x001AA43D File Offset: 0x001A863D
		public static FloodPath Construct(GraphNode start, OnPathDelegate callback = null)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			FloodPath path = PathPool.GetPath<FloodPath>();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x001AA45A File Offset: 0x001A865A
		protected void Setup(Vector3 start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x001AA478 File Offset: 0x001A8678
		protected void Setup(GraphNode start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = (Vector3)start.position;
			this.startNode = start;
			this.startPoint = (Vector3)start.position;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x001AA4B1 File Offset: 0x001A86B1
		protected override void Reset()
		{
			base.Reset();
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.parents = new Dictionary<GraphNode, GraphNode>();
			this.saveParents = true;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x001AA4E8 File Offset: 0x001A86E8
		protected override void Prepare()
		{
			if (this.startNode == null)
			{
				this.nnConstraint.tags = this.enabledTags;
				NNInfo nearest = AstarPath.active.GetNearest(this.originalStartPoint, this.nnConstraint);
				this.startPoint = nearest.position;
				this.startNode = nearest.node;
			}
			else
			{
				this.startPoint = (Vector3)this.startNode.position;
			}
			if (this.startNode == null)
			{
				base.FailWithError("Couldn't find a close node to the start point");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x001AA584 File Offset: 0x001A8784
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.parents[this.startNode] = null;
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.heap.isEmpty)
			{
				base.CompleteState = PathCompleteState.Complete;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x001AA654 File Offset: 0x001A8854
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.saveParents)
				{
					this.parents[this.currentR.node] = this.currentR.parent.node;
				}
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

		// Token: 0x04004199 RID: 16793
		public Vector3 originalStartPoint;

		// Token: 0x0400419A RID: 16794
		public Vector3 startPoint;

		// Token: 0x0400419B RID: 16795
		public GraphNode startNode;

		// Token: 0x0400419C RID: 16796
		public bool saveParents = true;

		// Token: 0x0400419D RID: 16797
		protected Dictionary<GraphNode, GraphNode> parents;
	}
}
