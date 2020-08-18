using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200051D RID: 1309
	public static class AstarMath
	{
		// Token: 0x060021E3 RID: 8675 RVA: 0x0018CC5F File Offset: 0x0018AE5F
		[Obsolete("Use VectorMath.ClosestPointOnLine instead")]
		public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnLine(lineStart, lineEnd, point);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0018CC69 File Offset: 0x0018AE69
		[Obsolete("Use VectorMath.ClosestPointOnLineFactor instead")]
		public static float NearestPointFactor(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnLineFactor(lineStart, lineEnd, point);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0018CC73 File Offset: 0x0018AE73
		[Obsolete("Use VectorMath.ClosestPointOnLineFactor instead")]
		public static float NearestPointFactor(Int3 lineStart, Int3 lineEnd, Int3 point)
		{
			return VectorMath.ClosestPointOnLineFactor(lineStart, lineEnd, point);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0018CC7D File Offset: 0x0018AE7D
		[Obsolete("Use VectorMath.ClosestPointOnLineFactor instead")]
		public static float NearestPointFactor(Int2 lineStart, Int2 lineEnd, Int2 point)
		{
			return VectorMath.ClosestPointOnLineFactor(lineStart, lineEnd, point);
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0018CC87 File Offset: 0x0018AE87
		[Obsolete("Use VectorMath.ClosestPointOnSegment instead")]
		public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnSegment(lineStart, lineEnd, point);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0018CC91 File Offset: 0x0018AE91
		[Obsolete("Use VectorMath.ClosestPointOnSegmentXZ instead")]
		public static Vector3 NearestPointStrictXZ(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnSegmentXZ(lineStart, lineEnd, point);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0018CC9B File Offset: 0x0018AE9B
		[Obsolete("Use VectorMath.SqrDistancePointSegmentApproximate instead")]
		public static float DistancePointSegment(int x, int z, int px, int pz, int qx, int qz)
		{
			return VectorMath.SqrDistancePointSegmentApproximate(x, z, px, pz, qx, qz);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0018CCAA File Offset: 0x0018AEAA
		[Obsolete("Use VectorMath.SqrDistancePointSegmentApproximate instead")]
		public static float DistancePointSegment(Int3 a, Int3 b, Int3 p)
		{
			return VectorMath.SqrDistancePointSegmentApproximate(a, b, p);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0018CCB4 File Offset: 0x0018AEB4
		[Obsolete("Use VectorMath.SqrDistancePointSegment instead")]
		public static float DistancePointSegmentStrict(Vector3 a, Vector3 b, Vector3 p)
		{
			return VectorMath.SqrDistancePointSegment(a, b, p);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0018CCBE File Offset: 0x0018AEBE
		[Obsolete("Use AstarSplines.CubicBezier instead")]
		public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			return AstarSplines.CubicBezier(p0, p1, p2, p3, t);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0018CCCB File Offset: 0x0018AECB
		[Obsolete("Use Mathf.InverseLerp instead")]
		public static float MapTo(float startMin, float startMax, float value)
		{
			return Mathf.InverseLerp(startMin, startMax, value);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0018CCD5 File Offset: 0x0018AED5
		public static float MapTo(float startMin, float startMax, float targetMin, float targetMax, float value)
		{
			return Mathf.Lerp(targetMin, targetMax, Mathf.InverseLerp(startMin, startMax, value));
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0018CCE8 File Offset: 0x0018AEE8
		public static string FormatBytesBinary(int bytes)
		{
			double num = (bytes >= 0) ? 1.0 : -1.0;
			bytes = Mathf.Abs(bytes);
			if (bytes < 1024)
			{
				return (double)bytes * num + " bytes";
			}
			if (bytes < 1048576)
			{
				return ((double)bytes / 1024.0 * num).ToString("0.0") + " KiB";
			}
			if (bytes < 1073741824)
			{
				return ((double)bytes / 1048576.0 * num).ToString("0.0") + " MiB";
			}
			return ((double)bytes / 1073741824.0 * num).ToString("0.0") + " GiB";
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0018CDB3 File Offset: 0x0018AFB3
		private static int Bit(int a, int b)
		{
			return a >> b & 1;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0018CDC0 File Offset: 0x0018AFC0
		public static Color IntToColor(int i, float a)
		{
			float num = (float)(AstarMath.Bit(i, 2) + AstarMath.Bit(i, 3) * 2 + 1);
			int num2 = AstarMath.Bit(i, 1) + AstarMath.Bit(i, 4) * 2 + 1;
			int num3 = AstarMath.Bit(i, 0) + AstarMath.Bit(i, 5) * 2 + 1;
			return new Color(num * 0.25f, (float)num2 * 0.25f, (float)num3 * 0.25f, a);
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0018CE28 File Offset: 0x0018B028
		public static Color HSVToRGB(float h, float s, float v)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = s * v;
			float num5 = h / 60f;
			float num6 = num4 * (1f - Math.Abs(num5 % 2f - 1f));
			if (num5 < 1f)
			{
				num = num4;
				num2 = num6;
			}
			else if (num5 < 2f)
			{
				num = num6;
				num2 = num4;
			}
			else if (num5 < 3f)
			{
				num2 = num4;
				num3 = num6;
			}
			else if (num5 < 4f)
			{
				num2 = num6;
				num3 = num4;
			}
			else if (num5 < 5f)
			{
				num = num6;
				num3 = num4;
			}
			else if (num5 < 6f)
			{
				num = num4;
				num3 = num6;
			}
			float num7 = v - num4;
			num += num7;
			num2 += num7;
			num3 += num7;
			return new Color(num, num2, num3);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0018CEEB File Offset: 0x0018B0EB
		[Obsolete("Use VectorMath.SqrDistanceXZ instead")]
		public static float SqrMagnitudeXZ(Vector3 a, Vector3 b)
		{
			return VectorMath.SqrDistanceXZ(a, b);
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Obsolete", true)]
		public static float DistancePointSegment2(int x, int z, int px, int pz, int qx, int qz)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Obsolete", true)]
		public static float DistancePointSegment2(Vector3 a, Vector3 b, Vector3 p)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Int3.GetHashCode instead", true)]
		public static int ComputeVertexHash(int x, int y, int z)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Obsolete", true)]
		public static float Hermite(float start, float end, float value)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Obsolete", true)]
		public static float MapToRange(float targetMin, float targetMax, float value)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Obsolete", true)]
		public static string FormatBytes(int bytes)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Obsolete", true)]
		public static float MagnitudeXZ(Vector3 a, Vector3 b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Obsolete", true)]
		public static int Repeat(int i, int n)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Abs instead", true)]
		public static float Abs(float a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Abs instead", true)]
		public static int Abs(int a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Min instead", true)]
		public static float Min(float a, float b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Min instead", true)]
		public static int Min(int a, int b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Min instead", true)]
		public static uint Min(uint a, uint b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Max instead", true)]
		public static float Max(float a, float b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Max instead", true)]
		public static int Max(int a, int b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Max instead", true)]
		public static uint Max(uint a, uint b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Max instead", true)]
		public static ushort Max(ushort a, ushort b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Sign instead", true)]
		public static float Sign(float a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Sign instead", true)]
		public static int Sign(int a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Clamp instead", true)]
		public static float Clamp(float a, float b, float c)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Clamp instead", true)]
		public static int Clamp(int a, int b, int c)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Clamp01 instead", true)]
		public static float Clamp01(float a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Clamp01 instead", true)]
		public static int Clamp01(int a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.Lerp instead", true)]
		public static float Lerp(float a, float b, float t)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.RoundToInt instead", true)]
		public static int RoundToInt(float v)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0018CEF4 File Offset: 0x0018B0F4
		[Obsolete("Use Mathf.RoundToInt instead", true)]
		public static int RoundToInt(double v)
		{
			throw new NotImplementedException("Obsolete");
		}
	}
}
