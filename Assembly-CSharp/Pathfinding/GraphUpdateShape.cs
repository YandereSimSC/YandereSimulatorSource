using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000520 RID: 1312
	public class GraphUpdateShape
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x0018E5B0 File Offset: 0x0018C7B0
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x0018E5B8 File Offset: 0x0018C7B8
		public Vector3[] points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
				if (this.convex)
				{
					this.CalculateConvexHull();
				}
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0018E5CF File Offset: 0x0018C7CF
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x0018E5D7 File Offset: 0x0018C7D7
		public bool convex
		{
			get
			{
				return this._convex;
			}
			set
			{
				if (this._convex != value && value)
				{
					this.CalculateConvexHull();
				}
				this._convex = value;
			}
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0018E5F6 File Offset: 0x0018C7F6
		public GraphUpdateShape()
		{
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0018E620 File Offset: 0x0018C820
		public GraphUpdateShape(Vector3[] points, bool convex, Matrix4x4 matrix, float minimumHeight)
		{
			this.convex = convex;
			this.points = points;
			this.origin = matrix.MultiplyPoint3x4(Vector3.zero);
			this.right = matrix.MultiplyPoint3x4(Vector3.right) - this.origin;
			this.up = matrix.MultiplyPoint3x4(Vector3.up) - this.origin;
			this.forward = matrix.MultiplyPoint3x4(Vector3.forward) - this.origin;
			this.minimumHeight = minimumHeight;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0018E6D3 File Offset: 0x0018C8D3
		private void CalculateConvexHull()
		{
			this._convexPoints = ((this.points != null) ? Polygon.ConvexHullXZ(this.points) : null);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0018E6F1 File Offset: 0x0018C8F1
		public Bounds GetBounds()
		{
			return GraphUpdateShape.GetBounds(this.convex ? this._convexPoints : this.points, this.right, this.up, this.forward, this.origin, this.minimumHeight);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0018E72C File Offset: 0x0018C92C
		public static Bounds GetBounds(Vector3[] points, Matrix4x4 matrix, float minimumHeight)
		{
			Vector3 b = matrix.MultiplyPoint3x4(Vector3.zero);
			Vector3 vector = matrix.MultiplyPoint3x4(Vector3.right) - b;
			Vector3 vector2 = matrix.MultiplyPoint3x4(Vector3.up) - b;
			Vector3 vector3 = matrix.MultiplyPoint3x4(Vector3.forward) - b;
			return GraphUpdateShape.GetBounds(points, vector, vector2, vector3, b, minimumHeight);
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0018E78C File Offset: 0x0018C98C
		private static Bounds GetBounds(Vector3[] points, Vector3 right, Vector3 up, Vector3 forward, Vector3 origin, float minimumHeight)
		{
			if (points == null || points.Length == 0)
			{
				return default(Bounds);
			}
			float num = points[0].y;
			float num2 = points[0].y;
			for (int i = 0; i < points.Length; i++)
			{
				num = Mathf.Min(num, points[i].y);
				num2 = Mathf.Max(num2, points[i].y);
			}
			float num3 = Mathf.Max(minimumHeight - (num2 - num), 0f) * 0.5f;
			num -= num3;
			num2 += num3;
			Vector3 vector = right * points[0].x + up * points[0].y + forward * points[0].z;
			Vector3 vector2 = vector;
			for (int j = 0; j < points.Length; j++)
			{
				Vector3 a = right * points[j].x + forward * points[j].z;
				Vector3 rhs = a + up * num;
				Vector3 rhs2 = a + up * num2;
				vector = Vector3.Min(vector, rhs);
				vector = Vector3.Min(vector, rhs2);
				vector2 = Vector3.Max(vector2, rhs);
				vector2 = Vector3.Max(vector2, rhs2);
			}
			return new Bounds((vector + vector2) * 0.5f + origin, vector2 - vector);
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0018E90A File Offset: 0x0018CB0A
		public bool Contains(GraphNode node)
		{
			return this.Contains((Vector3)node.position);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0018E920 File Offset: 0x0018CB20
		public bool Contains(Vector3 point)
		{
			point -= this.origin;
			Vector3 p = new Vector3(Vector3.Dot(point, this.right) / this.right.sqrMagnitude, 0f, Vector3.Dot(point, this.forward) / this.forward.sqrMagnitude);
			if (!this.convex)
			{
				return this._points != null && Polygon.ContainsPointXZ(this._points, p);
			}
			if (this._convexPoints == null)
			{
				return false;
			}
			int i = 0;
			int num = this._convexPoints.Length - 1;
			while (i < this._convexPoints.Length)
			{
				if (VectorMath.RightOrColinearXZ(this._convexPoints[i], this._convexPoints[num], p))
				{
					return false;
				}
				num = i;
				i++;
			}
			return true;
		}

		// Token: 0x04003EC5 RID: 16069
		private Vector3[] _points;

		// Token: 0x04003EC6 RID: 16070
		private Vector3[] _convexPoints;

		// Token: 0x04003EC7 RID: 16071
		private bool _convex;

		// Token: 0x04003EC8 RID: 16072
		private Vector3 right = Vector3.right;

		// Token: 0x04003EC9 RID: 16073
		private Vector3 forward = Vector3.forward;

		// Token: 0x04003ECA RID: 16074
		private Vector3 up = Vector3.up;

		// Token: 0x04003ECB RID: 16075
		private Vector3 origin;

		// Token: 0x04003ECC RID: 16076
		public float minimumHeight;
	}
}
