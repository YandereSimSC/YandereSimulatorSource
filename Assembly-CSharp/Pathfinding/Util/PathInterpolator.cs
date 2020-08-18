using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005D6 RID: 1494
	public class PathInterpolator
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x001BB604 File Offset: 0x001B9804
		public virtual Vector3 position
		{
			get
			{
				float t = (this.currentSegmentLength > 0.0001f) ? ((this.currentDistance - this.distanceToSegmentStart) / this.currentSegmentLength) : 0f;
				return Vector3.Lerp(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], t);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x001BB664 File Offset: 0x001B9864
		public Vector3 tangent
		{
			get
			{
				return this.path[this.segmentIndex + 1] - this.path[this.segmentIndex];
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x001BB68F File Offset: 0x001B988F
		// (set) Token: 0x0600291C RID: 10524 RVA: 0x001BB69E File Offset: 0x001B989E
		public float remainingDistance
		{
			get
			{
				return this.totalDistance - this.distance;
			}
			set
			{
				this.distance = this.totalDistance - value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x001BB6AE File Offset: 0x001B98AE
		// (set) Token: 0x0600291E RID: 10526 RVA: 0x001BB6B8 File Offset: 0x001B98B8
		public float distance
		{
			get
			{
				return this.currentDistance;
			}
			set
			{
				this.currentDistance = value;
				while (this.currentDistance < this.distanceToSegmentStart)
				{
					if (this.segmentIndex <= 0)
					{
						break;
					}
					this.PrevSegment();
				}
				while (this.currentDistance > this.distanceToSegmentStart + this.currentSegmentLength && this.segmentIndex < this.path.Count - 2)
				{
					this.NextSegment();
				}
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x001BB71D File Offset: 0x001B991D
		// (set) Token: 0x06002920 RID: 10528 RVA: 0x001BB725 File Offset: 0x001B9925
		public int segmentIndex { get; private set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x001BB72E File Offset: 0x001B992E
		public bool valid
		{
			get
			{
				return this.path != null;
			}
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x001BB73C File Offset: 0x001B993C
		public void SetPath(List<Vector3> path)
		{
			this.path = path;
			this.currentDistance = 0f;
			this.segmentIndex = 0;
			this.distanceToSegmentStart = 0f;
			if (path == null)
			{
				this.totalDistance = float.PositiveInfinity;
				this.currentSegmentLength = float.PositiveInfinity;
				return;
			}
			if (path.Count < 2)
			{
				throw new ArgumentException("Path must have a length of at least 2");
			}
			this.currentSegmentLength = (path[1] - path[0]).magnitude;
			this.totalDistance = 0f;
			Vector3 b = path[0];
			for (int i = 1; i < path.Count; i++)
			{
				Vector3 vector = path[i];
				this.totalDistance += (vector - b).magnitude;
				b = vector;
			}
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x001BB808 File Offset: 0x001B9A08
		public void MoveToSegment(int index, float fractionAlongSegment)
		{
			if (this.path == null)
			{
				return;
			}
			if (index < 0 || index >= this.path.Count - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			while (this.segmentIndex > index)
			{
				this.PrevSegment();
			}
			while (this.segmentIndex < index)
			{
				this.NextSegment();
			}
			this.distance = this.distanceToSegmentStart + Mathf.Clamp01(fractionAlongSegment) * this.currentSegmentLength;
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x001BB878 File Offset: 0x001B9A78
		public void MoveToClosestPoint(Vector3 point)
		{
			if (this.path == null)
			{
				return;
			}
			float num = float.PositiveInfinity;
			float fractionAlongSegment = 0f;
			int index = 0;
			for (int i = 0; i < this.path.Count - 1; i++)
			{
				float num2 = VectorMath.ClosestPointOnLineFactor(this.path[i], this.path[i + 1], point);
				Vector3 b = Vector3.Lerp(this.path[i], this.path[i + 1], num2);
				float sqrMagnitude = (point - b).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					fractionAlongSegment = num2;
					index = i;
				}
			}
			this.MoveToSegment(index, fractionAlongSegment);
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x001BB924 File Offset: 0x001B9B24
		public void MoveToLocallyClosestPoint(Vector3 point, bool allowForwards = true, bool allowBackwards = true)
		{
			if (this.path == null)
			{
				return;
			}
			while (allowForwards && this.segmentIndex < this.path.Count - 2)
			{
				if ((this.path[this.segmentIndex + 1] - point).sqrMagnitude > (this.path[this.segmentIndex] - point).sqrMagnitude)
				{
					break;
				}
				this.NextSegment();
			}
			while (allowBackwards && this.segmentIndex > 0 && (this.path[this.segmentIndex - 1] - point).sqrMagnitude <= (this.path[this.segmentIndex] - point).sqrMagnitude)
			{
				this.PrevSegment();
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = float.PositiveInfinity;
			float num4 = float.PositiveInfinity;
			if (this.segmentIndex > 0)
			{
				num = VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex - 1], this.path[this.segmentIndex], point);
				num3 = (Vector3.Lerp(this.path[this.segmentIndex - 1], this.path[this.segmentIndex], num) - point).sqrMagnitude;
			}
			if (this.segmentIndex < this.path.Count - 1)
			{
				num2 = VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], point);
				num4 = (Vector3.Lerp(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], num2) - point).sqrMagnitude;
			}
			if (num3 < num4)
			{
				this.MoveToSegment(this.segmentIndex - 1, num);
				return;
			}
			this.MoveToSegment(this.segmentIndex, num2);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x001BBB18 File Offset: 0x001B9D18
		public void MoveToCircleIntersection2D(Vector3 circleCenter3D, float radius, IMovementPlane transform)
		{
			if (this.path == null)
			{
				return;
			}
			while (this.segmentIndex < this.path.Count - 2 && VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], circleCenter3D) > 1f)
			{
				this.NextSegment();
			}
			Vector2 vector = transform.ToPlane(circleCenter3D);
			while (this.segmentIndex < this.path.Count - 2 && (transform.ToPlane(this.path[this.segmentIndex + 1]) - vector).sqrMagnitude <= radius * radius)
			{
				this.NextSegment();
			}
			float fractionAlongSegment = VectorMath.LineCircleIntersectionFactor(vector, transform.ToPlane(this.path[this.segmentIndex]), transform.ToPlane(this.path[this.segmentIndex + 1]), radius);
			this.MoveToSegment(this.segmentIndex, fractionAlongSegment);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x001BBC20 File Offset: 0x001B9E20
		protected virtual void PrevSegment()
		{
			int segmentIndex = this.segmentIndex;
			this.segmentIndex = segmentIndex - 1;
			this.currentSegmentLength = (this.path[this.segmentIndex + 1] - this.path[this.segmentIndex]).magnitude;
			this.distanceToSegmentStart -= this.currentSegmentLength;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x001BBC88 File Offset: 0x001B9E88
		protected virtual void NextSegment()
		{
			int segmentIndex = this.segmentIndex;
			this.segmentIndex = segmentIndex + 1;
			this.distanceToSegmentStart += this.currentSegmentLength;
			this.currentSegmentLength = (this.path[this.segmentIndex + 1] - this.path[this.segmentIndex]).magnitude;
		}

		// Token: 0x04004305 RID: 17157
		private List<Vector3> path;

		// Token: 0x04004306 RID: 17158
		private float distanceToSegmentStart;

		// Token: 0x04004307 RID: 17159
		private float currentDistance;

		// Token: 0x04004308 RID: 17160
		private float currentSegmentLength = float.PositiveInfinity;

		// Token: 0x04004309 RID: 17161
		private float totalDistance = float.PositiveInfinity;
	}
}
