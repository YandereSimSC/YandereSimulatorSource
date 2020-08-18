using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000587 RID: 1415
	[Serializable]
	public class StartEndModifier : PathModifier
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x0002D171 File Offset: 0x0002B371
		public override int Order
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x001A76A4 File Offset: 0x001A58A4
		public override void Apply(Path _p)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null || abpath.vectorPath.Count == 0)
			{
				return;
			}
			if (abpath.vectorPath.Count == 1 && !this.addPoints)
			{
				abpath.vectorPath.Add(abpath.vectorPath[0]);
			}
			bool flag;
			Vector3 vector = this.Snap(abpath, this.exactStartPoint, true, out flag);
			bool flag2;
			Vector3 vector2 = this.Snap(abpath, this.exactEndPoint, false, out flag2);
			if ((flag || this.addPoints) && this.exactStartPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Insert(0, vector);
			}
			else
			{
				abpath.vectorPath[0] = vector;
			}
			if ((flag2 || this.addPoints) && this.exactEndPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Add(vector2);
				return;
			}
			abpath.vectorPath[abpath.vectorPath.Count - 1] = vector2;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x001A7784 File Offset: 0x001A5984
		private Vector3 Snap(ABPath path, StartEndModifier.Exactness mode, bool start, out bool forceAddPoint)
		{
			int num = start ? 0 : (path.path.Count - 1);
			GraphNode graphNode = path.path[num];
			Vector3 vector = (Vector3)graphNode.position;
			forceAddPoint = false;
			switch (mode)
			{
			case StartEndModifier.Exactness.SnapToNode:
				return vector;
			case StartEndModifier.Exactness.Original:
			case StartEndModifier.Exactness.Interpolate:
			case StartEndModifier.Exactness.NodeConnection:
			{
				Vector3 vector2;
				if (start)
				{
					vector2 = ((this.adjustStartPoint != null) ? this.adjustStartPoint() : path.originalStartPoint);
				}
				else
				{
					vector2 = path.originalEndPoint;
				}
				switch (mode)
				{
				case StartEndModifier.Exactness.Original:
					return this.GetClampedPoint(vector, vector2, graphNode);
				case StartEndModifier.Exactness.Interpolate:
				{
					GraphNode graphNode2 = path.path[Mathf.Clamp(num + (start ? 1 : -1), 0, path.path.Count - 1)];
					return VectorMath.ClosestPointOnSegment(vector, (Vector3)graphNode2.position, vector2);
				}
				case StartEndModifier.Exactness.NodeConnection:
				{
					this.connectionBuffer = (this.connectionBuffer ?? new List<GraphNode>());
					Action<GraphNode> action;
					if ((action = this.connectionBufferAddDelegate) == null)
					{
						action = new Action<GraphNode>(this.connectionBuffer.Add);
					}
					this.connectionBufferAddDelegate = action;
					GraphNode graphNode2 = path.path[Mathf.Clamp(num + (start ? 1 : -1), 0, path.path.Count - 1)];
					graphNode.GetConnections(this.connectionBufferAddDelegate);
					Vector3 result = vector;
					float num2 = float.PositiveInfinity;
					for (int i = this.connectionBuffer.Count - 1; i >= 0; i--)
					{
						GraphNode graphNode3 = this.connectionBuffer[i];
						Vector3 vector3 = VectorMath.ClosestPointOnSegment(vector, (Vector3)graphNode3.position, vector2);
						float sqrMagnitude = (vector3 - vector2).sqrMagnitude;
						if (sqrMagnitude < num2)
						{
							result = vector3;
							num2 = sqrMagnitude;
							forceAddPoint = (graphNode3 != graphNode2);
						}
					}
					this.connectionBuffer.Clear();
					return result;
				}
				}
				throw new ArgumentException("Cannot reach this point, but the compiler is not smart enough to realize that.");
			}
			case StartEndModifier.Exactness.ClosestOnNode:
				if (!start)
				{
					return path.endPoint;
				}
				return path.startPoint;
			default:
				throw new ArgumentException("Invalid mode");
			}
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x001A7988 File Offset: 0x001A5B88
		protected Vector3 GetClampedPoint(Vector3 from, Vector3 to, GraphNode hint)
		{
			Vector3 vector = to;
			RaycastHit raycastHit;
			if (this.useRaycasting && Physics.Linecast(from, to, out raycastHit, this.mask))
			{
				vector = raycastHit.point;
			}
			if (this.useGraphRaycasting && hint != null)
			{
				IRaycastableGraph raycastableGraph = AstarData.GetGraph(hint) as IRaycastableGraph;
				GraphHitInfo graphHitInfo;
				if (raycastableGraph != null && raycastableGraph.Linecast(from, vector, hint, out graphHitInfo))
				{
					vector = graphHitInfo.point;
				}
			}
			return vector;
		}

		// Token: 0x0400414D RID: 16717
		public bool addPoints;

		// Token: 0x0400414E RID: 16718
		public StartEndModifier.Exactness exactStartPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x0400414F RID: 16719
		public StartEndModifier.Exactness exactEndPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x04004150 RID: 16720
		public Func<Vector3> adjustStartPoint;

		// Token: 0x04004151 RID: 16721
		public bool useRaycasting;

		// Token: 0x04004152 RID: 16722
		public LayerMask mask = -1;

		// Token: 0x04004153 RID: 16723
		public bool useGraphRaycasting;

		// Token: 0x04004154 RID: 16724
		private List<GraphNode> connectionBuffer;

		// Token: 0x04004155 RID: 16725
		private Action<GraphNode> connectionBufferAddDelegate;

		// Token: 0x02000755 RID: 1877
		public enum Exactness
		{
			// Token: 0x040049F6 RID: 18934
			SnapToNode,
			// Token: 0x040049F7 RID: 18935
			Original,
			// Token: 0x040049F8 RID: 18936
			Interpolate,
			// Token: 0x040049F9 RID: 18937
			ClosestOnNode,
			// Token: 0x040049FA RID: 18938
			NodeConnection
		}
	}
}
