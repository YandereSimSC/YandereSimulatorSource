using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200051B RID: 1307
	internal static class AstarSplines
	{
		// Token: 0x060021A7 RID: 8615 RVA: 0x0018B4E0 File Offset: 0x001896E0
		public static Vector3 CatmullRom(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float elapsedTime)
		{
			float num = elapsedTime * elapsedTime;
			float num2 = num * elapsedTime;
			return previous * (-0.5f * num2 + num - 0.5f * elapsedTime) + start * (1.5f * num2 + -2.5f * num + 1f) + end * (-1.5f * num2 + 2f * num + 0.5f * elapsedTime) + next * (0.5f * num2 - 0.5f * num);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0018B56B File Offset: 0x0018976B
		[Obsolete("Use CatmullRom")]
		public static Vector3 CatmullRomOLD(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float elapsedTime)
		{
			return AstarSplines.CatmullRom(previous, start, end, next, elapsedTime);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0018B578 File Offset: 0x00189778
		public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return num * num * num * p0 + 3f * num * num * t * p1 + 3f * num * t * t * p2 + t * t * t * p3;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0018B5E4 File Offset: 0x001897E4
		public static Vector3 CubicBezierDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return 3f * num * num * (p1 - p0) + 6f * num * t * (p2 - p1) + 3f * t * t * (p3 - p2);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0018B650 File Offset: 0x00189850
		public static Vector3 CubicBezierSecondDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return 6f * num * (p2 - 2f * p1 + p0) + 6f * t * (p3 - 2f * p2 + p1);
		}
	}
}
