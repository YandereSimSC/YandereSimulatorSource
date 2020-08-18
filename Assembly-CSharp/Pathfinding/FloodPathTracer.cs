using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000597 RID: 1431
	public class FloodPathTracer : ABPath
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x0002D171 File Offset: 0x0002B371
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x001AA770 File Offset: 0x001A8970
		public static FloodPathTracer Construct(Vector3 start, FloodPath flood, OnPathDelegate callback = null)
		{
			FloodPathTracer path = PathPool.GetPath<FloodPathTracer>();
			path.Setup(start, flood, callback);
			return path;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x001AA780 File Offset: 0x001A8980
		protected void Setup(Vector3 start, FloodPath flood, OnPathDelegate callback)
		{
			this.flood = flood;
			if (flood == null || flood.PipelineState < PathState.Returned)
			{
				throw new ArgumentException("You must supply a calculated FloodPath to the 'flood' argument");
			}
			base.Setup(start, flood.originalStartPoint, callback);
			this.nnConstraint = new FloodPathConstraint(flood);
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x001AA7BA File Offset: 0x001A89BA
		protected override void Reset()
		{
			base.Reset();
			this.flood = null;
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x001AA7C9 File Offset: 0x001A89C9
		protected override void Initialize()
		{
			if (this.startNode != null && this.flood.HasPathTo(this.startNode))
			{
				this.Trace(this.startNode);
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			base.FailWithError("Could not find valid start node");
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x001AA805 File Offset: 0x001A8A05
		protected override void CalculateStep(long targetTick)
		{
			if (!base.IsDone())
			{
				throw new Exception("Something went wrong. At this point the path should be completed");
			}
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x001AA81C File Offset: 0x001A8A1C
		public void Trace(GraphNode from)
		{
			GraphNode graphNode = from;
			int num = 0;
			while (graphNode != null)
			{
				this.path.Add(graphNode);
				this.vectorPath.Add((Vector3)graphNode.position);
				graphNode = this.flood.GetParent(graphNode);
				num++;
				if (num > 1024)
				{
					Debug.LogWarning("Inifinity loop? >1024 node path. Remove this message if you really have that long paths (FloodPathTracer.cs, Trace function)");
					return;
				}
			}
		}

		// Token: 0x0400419F RID: 16799
		protected FloodPath flood;
	}
}
