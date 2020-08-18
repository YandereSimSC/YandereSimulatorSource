using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200051E RID: 1310
	public static class Polygon
	{
		// Token: 0x0600220E RID: 8718 RVA: 0x0018CF00 File Offset: 0x0018B100
		[Obsolete("Use VectorMath.SignedTriangleAreaTimes2XZ instead")]
		public static long TriangleArea2(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.SignedTriangleAreaTimes2XZ(a, b, c);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0018CF0A File Offset: 0x0018B10A
		[Obsolete("Use VectorMath.SignedTriangleAreaTimes2XZ instead")]
		public static float TriangleArea2(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.SignedTriangleAreaTimes2XZ(a, b, c);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0018CF14 File Offset: 0x0018B114
		[Obsolete("Use TriangleArea2 instead to avoid confusion regarding the factor 2")]
		public static long TriangleArea(Int3 a, Int3 b, Int3 c)
		{
			return Polygon.TriangleArea2(a, b, c);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0018CF1E File Offset: 0x0018B11E
		[Obsolete("Use TriangleArea2 instead to avoid confusion regarding the factor 2")]
		public static float TriangleArea(Vector3 a, Vector3 b, Vector3 c)
		{
			return Polygon.TriangleArea2(a, b, c);
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0018CF28 File Offset: 0x0018B128
		[Obsolete("Use ContainsPointXZ instead")]
		public static bool ContainsPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			return Polygon.ContainsPointXZ(a, b, c, p);
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0018CF33 File Offset: 0x0018B133
		public static bool ContainsPointXZ(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			return VectorMath.IsClockwiseMarginXZ(a, b, p) && VectorMath.IsClockwiseMarginXZ(b, c, p) && VectorMath.IsClockwiseMarginXZ(c, a, p);
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0018CF53 File Offset: 0x0018B153
		[Obsolete("Use ContainsPointXZ instead")]
		public static bool ContainsPoint(Int3 a, Int3 b, Int3 c, Int3 p)
		{
			return Polygon.ContainsPointXZ(a, b, c, p);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0018CF5E File Offset: 0x0018B15E
		public static bool ContainsPointXZ(Int3 a, Int3 b, Int3 c, Int3 p)
		{
			return VectorMath.IsClockwiseOrColinearXZ(a, b, p) && VectorMath.IsClockwiseOrColinearXZ(b, c, p) && VectorMath.IsClockwiseOrColinearXZ(c, a, p);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0018CF7E File Offset: 0x0018B17E
		public static bool ContainsPoint(Int2 a, Int2 b, Int2 c, Int2 p)
		{
			return VectorMath.IsClockwiseOrColinear(a, b, p) && VectorMath.IsClockwiseOrColinear(b, c, p) && VectorMath.IsClockwiseOrColinear(c, a, p);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0018CF9E File Offset: 0x0018B19E
		[Obsolete("Use ContainsPointXZ instead")]
		public static bool ContainsPoint(Vector3[] polyPoints, Vector3 p)
		{
			return Polygon.ContainsPointXZ(polyPoints, p);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0018CFA8 File Offset: 0x0018B1A8
		public static bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].y <= p.y && p.y < polyPoints[num].y) || (polyPoints[num].y <= p.y && p.y < polyPoints[i].y)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[num].y - polyPoints[i].y) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0018D088 File Offset: 0x0018B288
		public static bool ContainsPointXZ(Vector3[] polyPoints, Vector3 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].z <= p.z && p.z < polyPoints[num].z) || (polyPoints[num].z <= p.z && p.z < polyPoints[i].z)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.z - polyPoints[i].z) / (polyPoints[num].z - polyPoints[i].z) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0018D168 File Offset: 0x0018B368
		public static int SampleYCoordinateInTriangle(Int3 p1, Int3 p2, Int3 p3, Int3 p)
		{
			double num = (double)(p2.z - p3.z) * (double)(p1.x - p3.x) + (double)(p3.x - p2.x) * (double)(p1.z - p3.z);
			double num2 = ((double)(p2.z - p3.z) * (double)(p.x - p3.x) + (double)(p3.x - p2.x) * (double)(p.z - p3.z)) / num;
			double num3 = ((double)(p3.z - p1.z) * (double)(p.x - p3.x) + (double)(p1.x - p3.x) * (double)(p.z - p3.z)) / num;
			return (int)Math.Round(num2 * (double)p1.y + num3 * (double)p2.y + (1.0 - num2 - num3) * (double)p3.y);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0018D25C File Offset: 0x0018B45C
		[Obsolete("Use VectorMath.RightXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool LeftNotColinear(Vector3 a, Vector3 b, Vector3 p)
		{
			return VectorMath.RightXZ(a, b, p);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0018D266 File Offset: 0x0018B466
		[Obsolete("Use VectorMath.RightOrColinearXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Vector3 a, Vector3 b, Vector3 p)
		{
			return VectorMath.RightOrColinearXZ(a, b, p);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x0018D270 File Offset: 0x0018B470
		[Obsolete("Use VectorMath.RightOrColinear instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Vector2 a, Vector2 b, Vector2 p)
		{
			return VectorMath.RightOrColinear(a, b, p);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0018BF68 File Offset: 0x0018A168
		[Obsolete("Use VectorMath.RightOrColinearXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Int3 a, Int3 b, Int3 p)
		{
			return VectorMath.RightOrColinearXZ(a, b, p);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0018BF5E File Offset: 0x0018A15E
		[Obsolete("Use VectorMath.RightXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool LeftNotColinear(Int3 a, Int3 b, Int3 p)
		{
			return VectorMath.RightXZ(a, b, p);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0018BF72 File Offset: 0x0018A172
		[Obsolete("Use VectorMath.RightOrColinear instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Int2 a, Int2 b, Int2 p)
		{
			return VectorMath.RightOrColinear(a, b, p);
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0018D27A File Offset: 0x0018B47A
		[Obsolete("Use VectorMath.IsClockwiseMarginXZ instead")]
		public static bool IsClockwiseMargin(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.IsClockwiseMarginXZ(a, b, c);
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0018D284 File Offset: 0x0018B484
		[Obsolete("Use VectorMath.IsClockwiseXZ instead")]
		public static bool IsClockwise(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.IsClockwiseXZ(a, b, c);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0018D28E File Offset: 0x0018B48E
		[Obsolete("Use VectorMath.IsClockwiseXZ instead")]
		public static bool IsClockwise(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsClockwiseXZ(a, b, c);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0018D298 File Offset: 0x0018B498
		[Obsolete("Use VectorMath.IsClockwiseOrColinearXZ instead")]
		public static bool IsClockwiseMargin(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsClockwiseOrColinearXZ(a, b, c);
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0018D2A2 File Offset: 0x0018B4A2
		[Obsolete("Use VectorMath.IsClockwiseOrColinear instead")]
		public static bool IsClockwiseMargin(Int2 a, Int2 b, Int2 c)
		{
			return VectorMath.IsClockwiseOrColinear(a, b, c);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0018D2AC File Offset: 0x0018B4AC
		[Obsolete("Use VectorMath.IsColinearXZ instead")]
		public static bool IsColinear(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsColinearXZ(a, b, c);
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0018D2B6 File Offset: 0x0018B4B6
		[Obsolete("Use VectorMath.IsColinearAlmostXZ instead")]
		public static bool IsColinearAlmost(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsColinearAlmostXZ(a, b, c);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0018D2C0 File Offset: 0x0018B4C0
		[Obsolete("Use VectorMath.IsColinearXZ instead")]
		public static bool IsColinear(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.IsColinearXZ(a, b, c);
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0018D2CA File Offset: 0x0018B4CA
		[Obsolete("Marked for removal since it is not used by any part of the A* Pathfinding Project")]
		public static bool IntersectsUnclamped(Vector3 a, Vector3 b, Vector3 a2, Vector3 b2)
		{
			return VectorMath.RightOrColinearXZ(a, b, a2) != VectorMath.RightOrColinearXZ(a, b, b2);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0018D2E1 File Offset: 0x0018B4E1
		[Obsolete("Use VectorMath.SegmentsIntersect instead")]
		public static bool Intersects(Int2 start1, Int2 end1, Int2 start2, Int2 end2)
		{
			return VectorMath.SegmentsIntersect(start1, end1, start2, end2);
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0018D2EC File Offset: 0x0018B4EC
		[Obsolete("Use VectorMath.SegmentsIntersectXZ instead")]
		public static bool Intersects(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.SegmentsIntersectXZ(start1, end1, start2, end2);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0018D2F7 File Offset: 0x0018B4F7
		[Obsolete("Use VectorMath.SegmentsIntersectXZ instead")]
		public static bool Intersects(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			return VectorMath.SegmentsIntersectXZ(start1, end1, start2, end2);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0018D302 File Offset: 0x0018B502
		[Obsolete("Use VectorMath.LineDirIntersectionPointXZ instead")]
		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2)
		{
			return VectorMath.LineDirIntersectionPointXZ(start1, dir1, start2, dir2);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0018D30D File Offset: 0x0018B50D
		[Obsolete("Use VectorMath.LineDirIntersectionPointXZ instead")]
		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2, out bool intersects)
		{
			return VectorMath.LineDirIntersectionPointXZ(start1, dir1, start2, dir2, out intersects);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0018D31A File Offset: 0x0018B51A
		[Obsolete("Use VectorMath.RaySegmentIntersectXZ instead")]
		public static bool IntersectionFactorRaySegment(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.RaySegmentIntersectXZ(start1, end1, start2, end2);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0018D325 File Offset: 0x0018B525
		[Obsolete("Use VectorMath.LineIntersectionFactorXZ instead")]
		public static bool IntersectionFactor(Int3 start1, Int3 end1, Int3 start2, Int3 end2, out float factor1, out float factor2)
		{
			return VectorMath.LineIntersectionFactorXZ(start1, end1, start2, end2, out factor1, out factor2);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0018D334 File Offset: 0x0018B534
		[Obsolete("Use VectorMath.LineIntersectionFactorXZ instead")]
		public static bool IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out float factor1, out float factor2)
		{
			return VectorMath.LineIntersectionFactorXZ(start1, end1, start2, end2, out factor1, out factor2);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0018D343 File Offset: 0x0018B543
		[Obsolete("Use VectorMath.LineRayIntersectionFactorXZ instead")]
		public static float IntersectionFactorRay(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.LineRayIntersectionFactorXZ(start1, end1, start2, end2);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0018D34E File Offset: 0x0018B54E
		[Obsolete("Use VectorMath.LineIntersectionFactorXZ instead")]
		public static float IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			return VectorMath.LineIntersectionFactorXZ(start1, end1, start2, end2);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0018D359 File Offset: 0x0018B559
		[Obsolete("Use VectorMath.LineIntersectionPointXZ instead")]
		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			return VectorMath.LineIntersectionPointXZ(start1, end1, start2, end2);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0018D364 File Offset: 0x0018B564
		[Obsolete("Use VectorMath.LineIntersectionPointXZ instead")]
		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			return VectorMath.LineIntersectionPointXZ(start1, end1, start2, end2, out intersects);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x0018D371 File Offset: 0x0018B571
		[Obsolete("Use VectorMath.LineIntersectionPoint instead")]
		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2)
		{
			return VectorMath.LineIntersectionPoint(start1, end1, start2, end2);
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0018D37C File Offset: 0x0018B57C
		[Obsolete("Use VectorMath.LineIntersectionPoint instead")]
		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2, out bool intersects)
		{
			return VectorMath.LineIntersectionPoint(start1, end1, start2, end2, out intersects);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0018D389 File Offset: 0x0018B589
		[Obsolete("Use VectorMath.SegmentIntersectionPointXZ instead")]
		public static Vector3 SegmentIntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			return VectorMath.SegmentIntersectionPointXZ(start1, end1, start2, end2, out intersects);
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0018D396 File Offset: 0x0018B596
		[Obsolete("Use ConvexHullXZ instead")]
		public static Vector3[] ConvexHull(Vector3[] points)
		{
			return Polygon.ConvexHullXZ(points);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0018D3A0 File Offset: 0x0018B5A0
		public static Vector3[] ConvexHullXZ(Vector3[] points)
		{
			if (points.Length == 0)
			{
				return new Vector3[0];
			}
			List<Vector3> list = ListPool<Vector3>.Claim();
			int num = 0;
			for (int i = 1; i < points.Length; i++)
			{
				if (points[i].x < points[num].x)
				{
					num = i;
				}
			}
			int num2 = num;
			int num3 = 0;
			for (;;)
			{
				list.Add(points[num]);
				int num4 = 0;
				for (int j = 0; j < points.Length; j++)
				{
					if (num4 == num || !VectorMath.RightOrColinearXZ(points[num], points[num4], points[j]))
					{
						num4 = j;
					}
				}
				num = num4;
				num3++;
				if (num3 > 10000)
				{
					break;
				}
				if (num == num2)
				{
					goto IL_AF;
				}
			}
			Debug.LogWarning("Infinite Loop in Convex Hull Calculation");
			IL_AF:
			Vector3[] result = list.ToArray();
			ListPool<Vector3>.Release(list);
			return result;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x0018D46C File Offset: 0x0018B66C
		[Obsolete("Use VectorMath.SegmentIntersectsBounds instead")]
		public static bool LineIntersectsBounds(Bounds bounds, Vector3 a, Vector3 b)
		{
			return VectorMath.SegmentIntersectsBounds(bounds, a, b);
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0018D476 File Offset: 0x0018B676
		[Obsolete("Scheduled for removal since it is not used by any part of the A* Pathfinding Project")]
		public static Vector3 ClosestPointOnTriangle(Vector3[] triangle, Vector3 point)
		{
			return Polygon.ClosestPointOnTriangle(triangle[0], triangle[1], triangle[2], point);
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0018D494 File Offset: 0x0018B694
		public static Vector2 ClosestPointOnTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 p)
		{
			Vector2 vector = b - a;
			Vector2 vector2 = c - a;
			Vector2 rhs = p - a;
			float num = Vector2.Dot(vector, rhs);
			float num2 = Vector2.Dot(vector2, rhs);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector2 rhs2 = p - b;
			float num3 = Vector2.Dot(vector, rhs2);
			float num4 = Vector2.Dot(vector2, rhs2);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			if (num >= 0f && num3 <= 0f && num * num4 - num3 * num2 <= 0f)
			{
				float d = num / (num - num3);
				return a + vector * d;
			}
			Vector2 rhs3 = p - c;
			float num5 = Vector2.Dot(vector, rhs3);
			float num6 = Vector2.Dot(vector2, rhs3);
			if (num6 >= 0f && num5 <= num6)
			{
				return c;
			}
			if (num2 >= 0f && num6 <= 0f && num5 * num2 - num * num6 <= 0f)
			{
				float d2 = num2 / (num2 - num6);
				return a + vector2 * d2;
			}
			if (num4 - num3 >= 0f && num5 - num6 >= 0f && num3 * num6 - num5 * num4 <= 0f)
			{
				float d3 = (num4 - num3) / (num4 - num3 + (num5 - num6));
				return b + (c - b) * d3;
			}
			return p;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0018D604 File Offset: 0x0018B804
		public static Vector3 ClosestPointOnTriangleXZ(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			Vector2 lhs = new Vector2(b.x - a.x, b.z - a.z);
			Vector2 lhs2 = new Vector2(c.x - a.x, c.z - a.z);
			Vector2 rhs = new Vector2(p.x - a.x, p.z - a.z);
			float num = Vector2.Dot(lhs, rhs);
			float num2 = Vector2.Dot(lhs2, rhs);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector2 rhs2 = new Vector2(p.x - b.x, p.z - b.z);
			float num3 = Vector2.Dot(lhs, rhs2);
			float num4 = Vector2.Dot(lhs2, rhs2);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			float num5 = num * num4 - num3 * num2;
			if (num >= 0f && num3 <= 0f && num5 <= 0f)
			{
				float num6 = num / (num - num3);
				return (1f - num6) * a + num6 * b;
			}
			Vector2 rhs3 = new Vector2(p.x - c.x, p.z - c.z);
			float num7 = Vector2.Dot(lhs, rhs3);
			float num8 = Vector2.Dot(lhs2, rhs3);
			if (num8 >= 0f && num7 <= num8)
			{
				return c;
			}
			float num9 = num7 * num2 - num * num8;
			if (num2 >= 0f && num8 <= 0f && num9 <= 0f)
			{
				float num10 = num2 / (num2 - num8);
				return (1f - num10) * a + num10 * c;
			}
			float num11 = num3 * num8 - num7 * num4;
			if (num4 - num3 >= 0f && num7 - num8 >= 0f && num11 <= 0f)
			{
				float d = (num4 - num3) / (num4 - num3 + (num7 - num8));
				return b + (c - b) * d;
			}
			float num12 = 1f / (num11 + num9 + num5);
			float num13 = num9 * num12;
			float num14 = num5 * num12;
			return new Vector3(p.x, (1f - num13 - num14) * a.y + num13 * b.y + num14 * c.y, p.z);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0018D868 File Offset: 0x0018BA68
		public static Vector3 ClosestPointOnTriangle(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = c - a;
			Vector3 rhs = p - a;
			float num = Vector3.Dot(vector, rhs);
			float num2 = Vector3.Dot(vector2, rhs);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector3 rhs2 = p - b;
			float num3 = Vector3.Dot(vector, rhs2);
			float num4 = Vector3.Dot(vector2, rhs2);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			float num5 = num * num4 - num3 * num2;
			if (num >= 0f && num3 <= 0f && num5 <= 0f)
			{
				float d = num / (num - num3);
				return a + vector * d;
			}
			Vector3 rhs3 = p - c;
			float num6 = Vector3.Dot(vector, rhs3);
			float num7 = Vector3.Dot(vector2, rhs3);
			if (num7 >= 0f && num6 <= num7)
			{
				return c;
			}
			float num8 = num6 * num2 - num * num7;
			if (num2 >= 0f && num7 <= 0f && num8 <= 0f)
			{
				float d2 = num2 / (num2 - num7);
				return a + vector2 * d2;
			}
			float num9 = num3 * num7 - num6 * num4;
			if (num4 - num3 >= 0f && num6 - num7 >= 0f && num9 <= 0f)
			{
				float d3 = (num4 - num3) / (num4 - num3 + (num6 - num7));
				return b + (c - b) * d3;
			}
			float num10 = 1f / (num9 + num8 + num5);
			float d4 = num8 * num10;
			float d5 = num5 * num10;
			return a + vector * d4 + vector2 * d5;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0018DA19 File Offset: 0x0018BC19
		[Obsolete("Use VectorMath.SqrDistanceSegmentSegment instead")]
		public static float DistanceSegmentSegment3D(Vector3 s1, Vector3 e1, Vector3 s2, Vector3 e2)
		{
			return VectorMath.SqrDistanceSegmentSegment(s1, e1, s2, e2);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0018DA24 File Offset: 0x0018BC24
		public static void CompressMesh(List<Int3> vertices, List<int> triangles, out Int3[] outVertices, out int[] outTriangles)
		{
			Dictionary<Int3, int> dictionary = Polygon.cached_Int3_int_dict;
			dictionary.Clear();
			int[] array = ArrayPool<int>.Claim(vertices.Count);
			int num = 0;
			for (int i = 0; i < vertices.Count; i++)
			{
				int num2;
				if (!dictionary.TryGetValue(vertices[i], out num2) && !dictionary.TryGetValue(vertices[i] + new Int3(0, 1, 0), out num2) && !dictionary.TryGetValue(vertices[i] + new Int3(0, -1, 0), out num2))
				{
					dictionary.Add(vertices[i], num);
					array[i] = num;
					vertices[num] = vertices[i];
					num++;
				}
				else
				{
					array[i] = num2;
				}
			}
			outTriangles = new int[triangles.Count];
			for (int j = 0; j < outTriangles.Length; j++)
			{
				outTriangles[j] = array[triangles[j]];
			}
			outVertices = new Int3[num];
			for (int k = 0; k < num; k++)
			{
				outVertices[k] = vertices[k];
			}
			ArrayPool<int>.Release(ref array, false);
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0018DB38 File Offset: 0x0018BD38
		public static void TraceContours(Dictionary<int, int> outline, HashSet<int> hasInEdge, Action<List<int>, bool> results)
		{
			List<int> list = ListPool<int>.Claim();
			List<int> list2 = ListPool<int>.Claim();
			list2.AddRange(outline.Keys);
			for (int i = 0; i <= 1; i++)
			{
				bool flag = i == 1;
				for (int j = 0; j < list2.Count; j++)
				{
					int num = list2[j];
					if (flag || !hasInEdge.Contains(num))
					{
						int num2 = num;
						list.Clear();
						list.Add(num2);
						while (outline.ContainsKey(num2))
						{
							int num3 = outline[num2];
							outline.Remove(num2);
							list.Add(num3);
							if (num3 == num)
							{
								break;
							}
							num2 = num3;
						}
						if (list.Count > 1)
						{
							results(list, flag);
						}
					}
				}
			}
			ListPool<int>.Release(ref list2);
			ListPool<int>.Release(ref list);
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x0018DC04 File Offset: 0x0018BE04
		public static void Subdivide(List<Vector3> points, List<Vector3> result, int subSegments)
		{
			for (int i = 0; i < points.Count - 1; i++)
			{
				for (int j = 0; j < subSegments; j++)
				{
					result.Add(Vector3.Lerp(points[i], points[i + 1], (float)j / (float)subSegments));
				}
			}
			result.Add(points[points.Count - 1]);
		}

		// Token: 0x04003EB1 RID: 16049
		private static readonly Dictionary<Int3, int> cached_Int3_int_dict = new Dictionary<Int3, int>();
	}
}
