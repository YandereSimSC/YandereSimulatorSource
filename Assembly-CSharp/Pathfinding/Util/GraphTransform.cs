using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005DA RID: 1498
	public class GraphTransform : IMovementPlane, ITransform
	{
		// Token: 0x06002935 RID: 10549 RVA: 0x001BBE08 File Offset: 0x001BA008
		public GraphTransform(Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.inverseMatrix = matrix.inverse;
			this.identity = matrix.isIdentity;
			this.onlyTranslational = GraphTransform.MatrixIsTranslational(matrix);
			this.up = matrix.MultiplyVector(Vector3.up).normalized;
			this.translation = matrix.MultiplyPoint3x4(Vector3.zero);
			this.i3translation = (Int3)this.translation;
			this.rotation = Quaternion.LookRotation(this.TransformVector(Vector3.forward), this.TransformVector(Vector3.up));
			this.inverseRotation = Quaternion.Inverse(this.rotation);
			this.isXY = (this.rotation == Quaternion.Euler(-90f, 0f, 0f));
			this.isXZ = (this.rotation == Quaternion.Euler(0f, 0f, 0f));
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x001BBF01 File Offset: 0x001BA101
		public Vector3 WorldUpAtGraphPosition(Vector3 point)
		{
			return this.up;
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x001BBF0C File Offset: 0x001BA10C
		private static bool MatrixIsTranslational(Matrix4x4 matrix)
		{
			return matrix.GetColumn(0) == new Vector4(1f, 0f, 0f, 0f) && matrix.GetColumn(1) == new Vector4(0f, 1f, 0f, 0f) && matrix.GetColumn(2) == new Vector4(0f, 0f, 1f, 0f) && matrix.m33 == 1f;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x001BBFA0 File Offset: 0x001BA1A0
		public Vector3 Transform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point + this.translation;
			}
			return this.matrix.MultiplyPoint3x4(point);
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x001BBFD4 File Offset: 0x001BA1D4
		public Vector3 TransformVector(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point;
			}
			return this.matrix.MultiplyVector(point);
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x001BBFFC File Offset: 0x001BA1FC
		public void Transform(Int3[] arr)
		{
			if (this.onlyTranslational)
			{
				for (int i = arr.Length - 1; i >= 0; i--)
				{
					arr[i] += this.i3translation;
				}
				return;
			}
			for (int j = arr.Length - 1; j >= 0; j--)
			{
				arr[j] = (Int3)this.matrix.MultiplyPoint3x4((Vector3)arr[j]);
			}
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x001BC078 File Offset: 0x001BA278
		public void Transform(Vector3[] arr)
		{
			if (this.onlyTranslational)
			{
				for (int i = arr.Length - 1; i >= 0; i--)
				{
					arr[i] += this.translation;
				}
				return;
			}
			for (int j = arr.Length - 1; j >= 0; j--)
			{
				arr[j] = this.matrix.MultiplyPoint3x4(arr[j]);
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x001BC0E8 File Offset: 0x001BA2E8
		public Vector3 InverseTransform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.translation;
			}
			return this.inverseMatrix.MultiplyPoint3x4(point);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x001BC11C File Offset: 0x001BA31C
		public Int3 InverseTransform(Int3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.i3translation;
			}
			return (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)point);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x001BC158 File Offset: 0x001BA358
		public void InverseTransform(Int3[] arr)
		{
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				arr[i] = (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)arr[i]);
			}
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x001BC19B File Offset: 0x001BA39B
		public static GraphTransform operator *(GraphTransform lhs, Matrix4x4 rhs)
		{
			return new GraphTransform(lhs.matrix * rhs);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x001BC1AE File Offset: 0x001BA3AE
		public static GraphTransform operator *(Matrix4x4 lhs, GraphTransform rhs)
		{
			return new GraphTransform(lhs * rhs.matrix);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x001BC1C4 File Offset: 0x001BA3C4
		public Bounds Transform(Bounds bounds)
		{
			if (this.onlyTranslational)
			{
				return new Bounds(bounds.center + this.translation, bounds.size);
			}
			Vector3[] array = ArrayPool<Vector3>.Claim(8);
			Vector3 extents = bounds.extents;
			array[0] = this.Transform(bounds.center + new Vector3(extents.x, extents.y, extents.z));
			array[1] = this.Transform(bounds.center + new Vector3(extents.x, extents.y, -extents.z));
			array[2] = this.Transform(bounds.center + new Vector3(extents.x, -extents.y, extents.z));
			array[3] = this.Transform(bounds.center + new Vector3(extents.x, -extents.y, -extents.z));
			array[4] = this.Transform(bounds.center + new Vector3(-extents.x, extents.y, extents.z));
			array[5] = this.Transform(bounds.center + new Vector3(-extents.x, extents.y, -extents.z));
			array[6] = this.Transform(bounds.center + new Vector3(-extents.x, -extents.y, extents.z));
			array[7] = this.Transform(bounds.center + new Vector3(-extents.x, -extents.y, -extents.z));
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int i = 1; i < 8; i++)
			{
				vector = Vector3.Min(vector, array[i]);
				vector2 = Vector3.Max(vector2, array[i]);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x001BC3F8 File Offset: 0x001BA5F8
		public Bounds InverseTransform(Bounds bounds)
		{
			if (this.onlyTranslational)
			{
				return new Bounds(bounds.center - this.translation, bounds.size);
			}
			Vector3[] array = ArrayPool<Vector3>.Claim(8);
			Vector3 extents = bounds.extents;
			array[0] = this.InverseTransform(bounds.center + new Vector3(extents.x, extents.y, extents.z));
			array[1] = this.InverseTransform(bounds.center + new Vector3(extents.x, extents.y, -extents.z));
			array[2] = this.InverseTransform(bounds.center + new Vector3(extents.x, -extents.y, extents.z));
			array[3] = this.InverseTransform(bounds.center + new Vector3(extents.x, -extents.y, -extents.z));
			array[4] = this.InverseTransform(bounds.center + new Vector3(-extents.x, extents.y, extents.z));
			array[5] = this.InverseTransform(bounds.center + new Vector3(-extents.x, extents.y, -extents.z));
			array[6] = this.InverseTransform(bounds.center + new Vector3(-extents.x, -extents.y, extents.z));
			array[7] = this.InverseTransform(bounds.center + new Vector3(-extents.x, -extents.y, -extents.z));
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int i = 1; i < 8; i++)
			{
				vector = Vector3.Min(vector, array[i]);
				vector2 = Vector3.Max(vector2, array[i]);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x001BC62C File Offset: 0x001BA82C
		Vector2 IMovementPlane.ToPlane(Vector3 point)
		{
			if (this.isXY)
			{
				return new Vector2(point.x, point.y);
			}
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			return new Vector2(point.x, point.z);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x001BC67A File Offset: 0x001BA87A
		Vector2 IMovementPlane.ToPlane(Vector3 point, out float elevation)
		{
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			elevation = point.y;
			return new Vector2(point.x, point.z);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x001BC6AB File Offset: 0x001BA8AB
		Vector3 IMovementPlane.ToWorld(Vector2 point, float elevation)
		{
			return this.rotation * new Vector3(point.x, elevation, point.y);
		}

		// Token: 0x0400430C RID: 17164
		public readonly bool identity;

		// Token: 0x0400430D RID: 17165
		public readonly bool onlyTranslational;

		// Token: 0x0400430E RID: 17166
		private readonly bool isXY;

		// Token: 0x0400430F RID: 17167
		private readonly bool isXZ;

		// Token: 0x04004310 RID: 17168
		private readonly Matrix4x4 matrix;

		// Token: 0x04004311 RID: 17169
		private readonly Matrix4x4 inverseMatrix;

		// Token: 0x04004312 RID: 17170
		private readonly Vector3 up;

		// Token: 0x04004313 RID: 17171
		private readonly Vector3 translation;

		// Token: 0x04004314 RID: 17172
		private readonly Int3 i3translation;

		// Token: 0x04004315 RID: 17173
		private readonly Quaternion rotation;

		// Token: 0x04004316 RID: 17174
		private readonly Quaternion inverseRotation;

		// Token: 0x04004317 RID: 17175
		public static readonly GraphTransform identityTransform = new GraphTransform(Matrix4x4.identity);
	}
}
