using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200059A RID: 1434
	public class XPath : ABPath
	{
		// Token: 0x06002712 RID: 10002 RVA: 0x001ABD02 File Offset: 0x001A9F02
		public new static XPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			XPath path = PathPool.GetPath<XPath>();
			path.Setup(start, end, callback);
			path.endingCondition = new ABPathEndingCondition(path);
			return path;
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x001ABD1E File Offset: 0x001A9F1E
		protected override void Reset()
		{
			base.Reset();
			this.endingCondition = null;
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0002D171 File Offset: 0x0002B371
		protected override bool EndPointGridGraphSpecialCase(GraphNode endNode)
		{
			return false;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x001ABD30 File Offset: 0x001A9F30
		protected override void CompletePathIfStartIsValidTarget()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			if (this.endingCondition.TargetFound(pathNode))
			{
				this.ChangeEndNode(this.startNode);
				this.Trace(pathNode);
				base.CompleteState = PathCompleteState.Complete;
			}
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x001ABD78 File Offset: 0x001A9F78
		private void ChangeEndNode(GraphNode target)
		{
			if (this.endNode != null && this.endNode != this.startNode)
			{
				PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
				pathNode.flag1 = (pathNode.flag2 = false);
			}
			this.endNode = target;
			this.endPoint = (Vector3)target.position;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x001ABDD4 File Offset: 0x001A9FD4
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.endingCondition.TargetFound(this.currentR))
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					base.FailWithError("Searched whole area but could not find target");
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
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.ChangeEndNode(this.currentR.node);
				this.Trace(this.currentR);
			}
		}

		// Token: 0x040041B6 RID: 16822
		public PathEndingCondition endingCondition;
	}
}
