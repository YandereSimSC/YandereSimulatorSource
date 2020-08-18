using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200057E RID: 1406
	[AddComponentMenu("Pathfinding/Modifiers/Advanced Smooth")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_advanced_smooth.php")]
	[Serializable]
	public class AdvancedSmooth : MonoModifier
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x001A5A6B File Offset: 0x001A3C6B
		public override int Order
		{
			get
			{
				return 40;
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x001A5A70 File Offset: 0x001A3C70
		public override void Apply(Path p)
		{
			Vector3[] array = p.vectorPath.ToArray();
			if (array == null || array.Length <= 2)
			{
				return;
			}
			List<Vector3> list = new List<Vector3>();
			list.Add(array[0]);
			AdvancedSmooth.TurnConstructor.turningRadius = this.turningRadius;
			for (int i = 1; i < array.Length - 1; i++)
			{
				List<AdvancedSmooth.Turn> turnList = new List<AdvancedSmooth.Turn>();
				AdvancedSmooth.TurnConstructor.Setup(i, array);
				this.turnConstruct1.Prepare(i, array);
				this.turnConstruct2.Prepare(i, array);
				AdvancedSmooth.TurnConstructor.PostPrepare();
				if (i == 1)
				{
					this.turnConstruct1.PointToTangent(turnList);
					this.turnConstruct2.PointToTangent(turnList);
				}
				else
				{
					this.turnConstruct1.TangentToTangent(turnList);
					this.turnConstruct2.TangentToTangent(turnList);
				}
				this.EvaluatePaths(turnList, list);
				if (i == array.Length - 2)
				{
					this.turnConstruct1.TangentToPoint(turnList);
					this.turnConstruct2.TangentToPoint(turnList);
				}
				this.EvaluatePaths(turnList, list);
			}
			list.Add(array[array.Length - 1]);
			p.vectorPath = list;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x001A5B74 File Offset: 0x001A3D74
		private void EvaluatePaths(List<AdvancedSmooth.Turn> turnList, List<Vector3> output)
		{
			turnList.Sort();
			for (int i = 0; i < turnList.Count; i++)
			{
				if (i == 0)
				{
					turnList[i].GetPath(output);
				}
			}
			turnList.Clear();
			if (AdvancedSmooth.TurnConstructor.changedPreviousTangent)
			{
				this.turnConstruct1.OnTangentUpdate();
				this.turnConstruct2.OnTangentUpdate();
			}
		}

		// Token: 0x04004125 RID: 16677
		public float turningRadius = 1f;

		// Token: 0x04004126 RID: 16678
		public AdvancedSmooth.MaxTurn turnConstruct1 = new AdvancedSmooth.MaxTurn();

		// Token: 0x04004127 RID: 16679
		public AdvancedSmooth.ConstantTurn turnConstruct2 = new AdvancedSmooth.ConstantTurn();

		// Token: 0x0200074E RID: 1870
		[Serializable]
		public class MaxTurn : AdvancedSmooth.TurnConstructor
		{
			// Token: 0x06002D40 RID: 11584 RVA: 0x001CDBE8 File Offset: 0x001CBDE8
			public override void OnTangentUpdate()
			{
				this.rightCircleCenter = AdvancedSmooth.TurnConstructor.current + AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.leftCircleCenter = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.vaRight = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.rightCircleCenter);
				this.vaLeft = this.vaRight + 3.141592653589793;
			}

			// Token: 0x06002D41 RID: 11585 RVA: 0x001CDC68 File Offset: 0x001CBE68
			public override void Prepare(int i, Vector3[] vectorPath)
			{
				this.preRightCircleCenter = this.rightCircleCenter;
				this.preLeftCircleCenter = this.leftCircleCenter;
				this.rightCircleCenter = AdvancedSmooth.TurnConstructor.current + AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.leftCircleCenter = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.preVaRight = this.vaRight;
				this.preVaLeft = this.vaLeft;
				this.vaRight = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.rightCircleCenter);
				this.vaLeft = this.vaRight + 3.141592653589793;
			}

			// Token: 0x06002D42 RID: 11586 RVA: 0x001CDD18 File Offset: 0x001CBF18
			public override void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
			{
				this.alfaRightRight = base.Atan2(this.rightCircleCenter - this.preRightCircleCenter);
				this.alfaLeftLeft = base.Atan2(this.leftCircleCenter - this.preLeftCircleCenter);
				this.alfaRightLeft = base.Atan2(this.leftCircleCenter - this.preRightCircleCenter);
				this.alfaLeftRight = base.Atan2(this.rightCircleCenter - this.preLeftCircleCenter);
				double num = (double)(this.leftCircleCenter - this.preRightCircleCenter).magnitude;
				double num2 = (double)(this.rightCircleCenter - this.preLeftCircleCenter).magnitude;
				bool flag = false;
				bool flag2 = false;
				if (num < (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f))
				{
					num = (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f);
					flag = true;
				}
				if (num2 < (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f))
				{
					num2 = (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f);
					flag2 = true;
				}
				this.deltaRightLeft = (flag ? 0.0 : (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f) / num)));
				this.deltaLeftRight = (flag2 ? 0.0 : (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f) / num2)));
				this.betaRightRight = base.ClockwiseAngle(this.preVaRight, this.alfaRightRight - 1.5707963267948966);
				this.betaRightLeft = base.ClockwiseAngle(this.preVaRight, this.alfaRightLeft - this.deltaRightLeft);
				this.betaLeftRight = base.CounterClockwiseAngle(this.preVaLeft, this.alfaLeftRight + this.deltaLeftRight);
				this.betaLeftLeft = base.CounterClockwiseAngle(this.preVaLeft, this.alfaLeftLeft + 1.5707963267948966);
				this.betaRightRight += base.ClockwiseAngle(this.alfaRightRight - 1.5707963267948966, this.vaRight);
				this.betaRightLeft += base.CounterClockwiseAngle(this.alfaRightLeft + this.deltaRightLeft, this.vaLeft);
				this.betaLeftRight += base.ClockwiseAngle(this.alfaLeftRight - this.deltaLeftRight, this.vaRight);
				this.betaLeftLeft += base.CounterClockwiseAngle(this.alfaLeftLeft + 1.5707963267948966, this.vaLeft);
				this.betaRightRight = base.GetLengthFromAngle(this.betaRightRight, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				this.betaRightLeft = base.GetLengthFromAngle(this.betaRightLeft, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				this.betaLeftRight = base.GetLengthFromAngle(this.betaLeftRight, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				this.betaLeftLeft = base.GetLengthFromAngle(this.betaLeftLeft, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				Vector3 a = base.AngleToVector(this.alfaRightRight - 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.preRightCircleCenter;
				Vector3 a2 = base.AngleToVector(this.alfaRightLeft - this.deltaRightLeft) * AdvancedSmooth.TurnConstructor.turningRadius + this.preRightCircleCenter;
				Vector3 a3 = base.AngleToVector(this.alfaLeftRight + this.deltaLeftRight) * AdvancedSmooth.TurnConstructor.turningRadius + this.preLeftCircleCenter;
				Vector3 a4 = base.AngleToVector(this.alfaLeftLeft + 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.preLeftCircleCenter;
				Vector3 b = base.AngleToVector(this.alfaRightRight - 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.rightCircleCenter;
				Vector3 b2 = base.AngleToVector(this.alfaRightLeft - this.deltaRightLeft + 3.141592653589793) * AdvancedSmooth.TurnConstructor.turningRadius + this.leftCircleCenter;
				Vector3 b3 = base.AngleToVector(this.alfaLeftRight + this.deltaLeftRight + 3.141592653589793) * AdvancedSmooth.TurnConstructor.turningRadius + this.rightCircleCenter;
				Vector3 b4 = base.AngleToVector(this.alfaLeftLeft + 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.leftCircleCenter;
				this.betaRightRight += (double)(a - b).magnitude;
				this.betaRightLeft += (double)(a2 - b2).magnitude;
				this.betaLeftRight += (double)(a3 - b3).magnitude;
				this.betaLeftLeft += (double)(a4 - b4).magnitude;
				if (flag)
				{
					this.betaRightLeft += 10000000.0;
				}
				if (flag2)
				{
					this.betaLeftRight += 10000000.0;
				}
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaRightRight, this, 2));
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaRightLeft, this, 3));
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaLeftRight, this, 4));
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaLeftLeft, this, 5));
			}

			// Token: 0x06002D43 RID: 11587 RVA: 0x001CE268 File Offset: 0x001CC468
			public override void PointToTangent(List<AdvancedSmooth.Turn> turnList)
			{
				bool flag = false;
				bool flag2 = false;
				float magnitude = (AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter).magnitude;
				float magnitude2 = (AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter).magnitude;
				if (magnitude < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag = true;
				}
				if (magnitude2 < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag2 = true;
				}
				double num = flag ? 0.0 : base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter);
				double num2 = flag ? 0.0 : (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / (AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter).magnitude)));
				this.gammaRight = num + num2;
				double num3 = flag ? 0.0 : base.ClockwiseAngle(this.gammaRight, this.vaRight);
				double num4 = flag2 ? 0.0 : base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter);
				double num5 = flag2 ? 0.0 : (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / (AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter).magnitude)));
				this.gammaLeft = num4 - num5;
				double num6 = flag2 ? 0.0 : base.CounterClockwiseAngle(this.gammaLeft, this.vaLeft);
				if (!flag)
				{
					turnList.Add(new AdvancedSmooth.Turn((float)num3, this, 0));
				}
				if (!flag2)
				{
					turnList.Add(new AdvancedSmooth.Turn((float)num6, this, 1));
				}
			}

			// Token: 0x06002D44 RID: 11588 RVA: 0x001CE408 File Offset: 0x001CC608
			public override void TangentToPoint(List<AdvancedSmooth.Turn> turnList)
			{
				bool flag = false;
				bool flag2 = false;
				float magnitude = (AdvancedSmooth.TurnConstructor.next - this.rightCircleCenter).magnitude;
				float magnitude2 = (AdvancedSmooth.TurnConstructor.next - this.leftCircleCenter).magnitude;
				if (magnitude < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag = true;
				}
				if (magnitude2 < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag2 = true;
				}
				if (!flag)
				{
					double num = base.Atan2(AdvancedSmooth.TurnConstructor.next - this.rightCircleCenter);
					double num2 = 1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / magnitude));
					this.gammaRight = num - num2;
					double num3 = base.ClockwiseAngle(this.vaRight, this.gammaRight);
					turnList.Add(new AdvancedSmooth.Turn((float)num3, this, 6));
				}
				if (!flag2)
				{
					double num4 = base.Atan2(AdvancedSmooth.TurnConstructor.next - this.leftCircleCenter);
					double num5 = 1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / magnitude2));
					this.gammaLeft = num4 + num5;
					double num6 = base.CounterClockwiseAngle(this.vaLeft, this.gammaLeft);
					turnList.Add(new AdvancedSmooth.Turn((float)num6, this, 7));
				}
			}

			// Token: 0x06002D45 RID: 11589 RVA: 0x001CE528 File Offset: 0x001CC728
			public override void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output)
			{
				switch (turn.id)
				{
				case 0:
					base.AddCircleSegment(this.gammaRight, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 1:
					base.AddCircleSegment(this.gammaLeft, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 2:
					base.AddCircleSegment(this.preVaRight, this.alfaRightRight - 1.5707963267948966, true, this.preRightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaRightRight - 1.5707963267948966, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 3:
					base.AddCircleSegment(this.preVaRight, this.alfaRightLeft - this.deltaRightLeft, true, this.preRightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaRightLeft - this.deltaRightLeft + 3.141592653589793, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 4:
					base.AddCircleSegment(this.preVaLeft, this.alfaLeftRight + this.deltaLeftRight, false, this.preLeftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaLeftRight + this.deltaLeftRight + 3.141592653589793, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 5:
					base.AddCircleSegment(this.preVaLeft, this.alfaLeftLeft + 1.5707963267948966, false, this.preLeftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaLeftLeft + 1.5707963267948966, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 6:
					base.AddCircleSegment(this.vaRight, this.gammaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 7:
					base.AddCircleSegment(this.vaLeft, this.gammaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				default:
					return;
				}
			}

			// Token: 0x040049BD RID: 18877
			private Vector3 preRightCircleCenter = Vector3.zero;

			// Token: 0x040049BE RID: 18878
			private Vector3 preLeftCircleCenter = Vector3.zero;

			// Token: 0x040049BF RID: 18879
			private Vector3 rightCircleCenter;

			// Token: 0x040049C0 RID: 18880
			private Vector3 leftCircleCenter;

			// Token: 0x040049C1 RID: 18881
			private double vaRight;

			// Token: 0x040049C2 RID: 18882
			private double vaLeft;

			// Token: 0x040049C3 RID: 18883
			private double preVaLeft;

			// Token: 0x040049C4 RID: 18884
			private double preVaRight;

			// Token: 0x040049C5 RID: 18885
			private double gammaLeft;

			// Token: 0x040049C6 RID: 18886
			private double gammaRight;

			// Token: 0x040049C7 RID: 18887
			private double betaRightRight;

			// Token: 0x040049C8 RID: 18888
			private double betaRightLeft;

			// Token: 0x040049C9 RID: 18889
			private double betaLeftRight;

			// Token: 0x040049CA RID: 18890
			private double betaLeftLeft;

			// Token: 0x040049CB RID: 18891
			private double deltaRightLeft;

			// Token: 0x040049CC RID: 18892
			private double deltaLeftRight;

			// Token: 0x040049CD RID: 18893
			private double alfaRightRight;

			// Token: 0x040049CE RID: 18894
			private double alfaLeftLeft;

			// Token: 0x040049CF RID: 18895
			private double alfaRightLeft;

			// Token: 0x040049D0 RID: 18896
			private double alfaLeftRight;
		}

		// Token: 0x0200074F RID: 1871
		[Serializable]
		public class ConstantTurn : AdvancedSmooth.TurnConstructor
		{
			// Token: 0x06002D47 RID: 11591 RVA: 0x00002ACE File Offset: 0x00000CCE
			public override void Prepare(int i, Vector3[] vectorPath)
			{
			}

			// Token: 0x06002D48 RID: 11592 RVA: 0x001CE754 File Offset: 0x001CC954
			public override void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
			{
				Vector3 dir = Vector3.Cross(AdvancedSmooth.TurnConstructor.t1, Vector3.up);
				Vector3 vector = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.prev;
				Vector3 start = vector * 0.5f + AdvancedSmooth.TurnConstructor.prev;
				vector = Vector3.Cross(vector, Vector3.up);
				bool flag;
				this.circleCenter = VectorMath.LineDirIntersectionPointXZ(AdvancedSmooth.TurnConstructor.prev, dir, start, vector, out flag);
				if (!flag)
				{
					return;
				}
				this.gamma1 = base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.circleCenter);
				this.gamma2 = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.circleCenter);
				this.clockwise = !VectorMath.RightOrColinearXZ(this.circleCenter, AdvancedSmooth.TurnConstructor.prev, AdvancedSmooth.TurnConstructor.prev + AdvancedSmooth.TurnConstructor.t1);
				double num = this.clockwise ? base.ClockwiseAngle(this.gamma1, this.gamma2) : base.CounterClockwiseAngle(this.gamma1, this.gamma2);
				num = base.GetLengthFromAngle(num, (double)(this.circleCenter - AdvancedSmooth.TurnConstructor.current).magnitude);
				turnList.Add(new AdvancedSmooth.Turn((float)num, this, 0));
			}

			// Token: 0x06002D49 RID: 11593 RVA: 0x001CE880 File Offset: 0x001CCA80
			public override void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output)
			{
				base.AddCircleSegment(this.gamma1, this.gamma2, this.clockwise, this.circleCenter, output, (this.circleCenter - AdvancedSmooth.TurnConstructor.current).magnitude);
				AdvancedSmooth.TurnConstructor.normal = (AdvancedSmooth.TurnConstructor.current - this.circleCenter).normalized;
				AdvancedSmooth.TurnConstructor.t2 = Vector3.Cross(AdvancedSmooth.TurnConstructor.normal, Vector3.up).normalized;
				AdvancedSmooth.TurnConstructor.normal = -AdvancedSmooth.TurnConstructor.normal;
				if (!this.clockwise)
				{
					AdvancedSmooth.TurnConstructor.t2 = -AdvancedSmooth.TurnConstructor.t2;
					AdvancedSmooth.TurnConstructor.normal = -AdvancedSmooth.TurnConstructor.normal;
				}
				AdvancedSmooth.TurnConstructor.changedPreviousTangent = true;
			}

			// Token: 0x040049D1 RID: 18897
			private Vector3 circleCenter;

			// Token: 0x040049D2 RID: 18898
			private double gamma1;

			// Token: 0x040049D3 RID: 18899
			private double gamma2;

			// Token: 0x040049D4 RID: 18900
			private bool clockwise;
		}

		// Token: 0x02000750 RID: 1872
		public abstract class TurnConstructor
		{
			// Token: 0x06002D4B RID: 11595
			public abstract void Prepare(int i, Vector3[] vectorPath);

			// Token: 0x06002D4C RID: 11596 RVA: 0x00002ACE File Offset: 0x00000CCE
			public virtual void OnTangentUpdate()
			{
			}

			// Token: 0x06002D4D RID: 11597 RVA: 0x00002ACE File Offset: 0x00000CCE
			public virtual void PointToTangent(List<AdvancedSmooth.Turn> turnList)
			{
			}

			// Token: 0x06002D4E RID: 11598 RVA: 0x00002ACE File Offset: 0x00000CCE
			public virtual void TangentToPoint(List<AdvancedSmooth.Turn> turnList)
			{
			}

			// Token: 0x06002D4F RID: 11599 RVA: 0x00002ACE File Offset: 0x00000CCE
			public virtual void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
			{
			}

			// Token: 0x06002D50 RID: 11600
			public abstract void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output);

			// Token: 0x06002D51 RID: 11601 RVA: 0x001CE940 File Offset: 0x001CCB40
			public static void Setup(int i, Vector3[] vectorPath)
			{
				AdvancedSmooth.TurnConstructor.current = vectorPath[i];
				AdvancedSmooth.TurnConstructor.prev = vectorPath[i - 1];
				AdvancedSmooth.TurnConstructor.next = vectorPath[i + 1];
				AdvancedSmooth.TurnConstructor.prev.y = AdvancedSmooth.TurnConstructor.current.y;
				AdvancedSmooth.TurnConstructor.next.y = AdvancedSmooth.TurnConstructor.current.y;
				AdvancedSmooth.TurnConstructor.t1 = AdvancedSmooth.TurnConstructor.t2;
				AdvancedSmooth.TurnConstructor.t2 = (AdvancedSmooth.TurnConstructor.next - AdvancedSmooth.TurnConstructor.current).normalized - (AdvancedSmooth.TurnConstructor.prev - AdvancedSmooth.TurnConstructor.current).normalized;
				AdvancedSmooth.TurnConstructor.t2 = AdvancedSmooth.TurnConstructor.t2.normalized;
				AdvancedSmooth.TurnConstructor.prevNormal = AdvancedSmooth.TurnConstructor.normal;
				AdvancedSmooth.TurnConstructor.normal = Vector3.Cross(AdvancedSmooth.TurnConstructor.t2, Vector3.up);
				AdvancedSmooth.TurnConstructor.normal = AdvancedSmooth.TurnConstructor.normal.normalized;
			}

			// Token: 0x06002D52 RID: 11602 RVA: 0x001CEA1B File Offset: 0x001CCC1B
			public static void PostPrepare()
			{
				AdvancedSmooth.TurnConstructor.changedPreviousTangent = false;
			}

			// Token: 0x06002D53 RID: 11603 RVA: 0x001CEA24 File Offset: 0x001CCC24
			public void AddCircleSegment(double startAngle, double endAngle, bool clockwise, Vector3 center, List<Vector3> output, float radius)
			{
				double num = 0.06283185307179587;
				if (clockwise)
				{
					while (endAngle > startAngle + 6.283185307179586)
					{
						endAngle -= 6.283185307179586;
					}
					while (endAngle < startAngle)
					{
						endAngle += 6.283185307179586;
					}
				}
				else
				{
					while (endAngle < startAngle - 6.283185307179586)
					{
						endAngle += 6.283185307179586;
					}
					while (endAngle > startAngle)
					{
						endAngle -= 6.283185307179586;
					}
				}
				if (clockwise)
				{
					for (double num2 = startAngle; num2 < endAngle; num2 += num)
					{
						output.Add(this.AngleToVector(num2) * radius + center);
					}
				}
				else
				{
					for (double num3 = startAngle; num3 > endAngle; num3 -= num)
					{
						output.Add(this.AngleToVector(num3) * radius + center);
					}
				}
				output.Add(this.AngleToVector(endAngle) * radius + center);
			}

			// Token: 0x06002D54 RID: 11604 RVA: 0x001CEB10 File Offset: 0x001CCD10
			public void DebugCircleSegment(Vector3 center, double startAngle, double endAngle, double radius, Color color)
			{
				double num = 0.06283185307179587;
				while (endAngle < startAngle)
				{
					endAngle += 6.283185307179586;
				}
				Vector3 start = this.AngleToVector(startAngle) * (float)radius + center;
				for (double num2 = startAngle + num; num2 < endAngle; num2 += num)
				{
					Debug.DrawLine(start, this.AngleToVector(num2) * (float)radius + center);
				}
				Debug.DrawLine(start, this.AngleToVector(endAngle) * (float)radius + center);
			}

			// Token: 0x06002D55 RID: 11605 RVA: 0x001CEB94 File Offset: 0x001CCD94
			public void DebugCircle(Vector3 center, double radius, Color color)
			{
				double num = 0.06283185307179587;
				Vector3 start = this.AngleToVector(-num) * (float)radius + center;
				for (double num2 = 0.0; num2 < 6.283185307179586; num2 += num)
				{
					Vector3 vector = this.AngleToVector(num2) * (float)radius + center;
					Debug.DrawLine(start, vector, color);
					start = vector;
				}
			}

			// Token: 0x06002D56 RID: 11606 RVA: 0x001CEBFC File Offset: 0x001CCDFC
			public double GetLengthFromAngle(double angle, double radius)
			{
				return radius * angle;
			}

			// Token: 0x06002D57 RID: 11607 RVA: 0x001CEC01 File Offset: 0x001CCE01
			public double ClockwiseAngle(double from, double to)
			{
				return this.ClampAngle(to - from);
			}

			// Token: 0x06002D58 RID: 11608 RVA: 0x001CEC0C File Offset: 0x001CCE0C
			public double CounterClockwiseAngle(double from, double to)
			{
				return this.ClampAngle(from - to);
			}

			// Token: 0x06002D59 RID: 11609 RVA: 0x001CEC17 File Offset: 0x001CCE17
			public Vector3 AngleToVector(double a)
			{
				return new Vector3((float)Math.Cos(a), 0f, (float)Math.Sin(a));
			}

			// Token: 0x06002D5A RID: 11610 RVA: 0x001CEC31 File Offset: 0x001CCE31
			public double ToDegrees(double rad)
			{
				return rad * 57.295780181884766;
			}

			// Token: 0x06002D5B RID: 11611 RVA: 0x001CEC3E File Offset: 0x001CCE3E
			public double ClampAngle(double a)
			{
				while (a < 0.0)
				{
					a += 6.283185307179586;
				}
				while (a > 6.283185307179586)
				{
					a -= 6.283185307179586;
				}
				return a;
			}

			// Token: 0x06002D5C RID: 11612 RVA: 0x001CEC77 File Offset: 0x001CCE77
			public double Atan2(Vector3 v)
			{
				return Math.Atan2((double)v.z, (double)v.x);
			}

			// Token: 0x040049D5 RID: 18901
			public float constantBias;

			// Token: 0x040049D6 RID: 18902
			public float factorBias = 1f;

			// Token: 0x040049D7 RID: 18903
			public static float turningRadius = 1f;

			// Token: 0x040049D8 RID: 18904
			public const double ThreeSixtyRadians = 6.283185307179586;

			// Token: 0x040049D9 RID: 18905
			public static Vector3 prev;

			// Token: 0x040049DA RID: 18906
			public static Vector3 current;

			// Token: 0x040049DB RID: 18907
			public static Vector3 next;

			// Token: 0x040049DC RID: 18908
			public static Vector3 t1;

			// Token: 0x040049DD RID: 18909
			public static Vector3 t2;

			// Token: 0x040049DE RID: 18910
			public static Vector3 normal;

			// Token: 0x040049DF RID: 18911
			public static Vector3 prevNormal;

			// Token: 0x040049E0 RID: 18912
			public static bool changedPreviousTangent = false;
		}

		// Token: 0x02000751 RID: 1873
		public struct Turn : IComparable<AdvancedSmooth.Turn>
		{
			// Token: 0x1700068B RID: 1675
			// (get) Token: 0x06002D5F RID: 11615 RVA: 0x001CECB1 File Offset: 0x001CCEB1
			public float score
			{
				get
				{
					return this.length * this.constructor.factorBias + this.constructor.constantBias;
				}
			}

			// Token: 0x06002D60 RID: 11616 RVA: 0x001CECD1 File Offset: 0x001CCED1
			public Turn(float length, AdvancedSmooth.TurnConstructor constructor, int id = 0)
			{
				this.length = length;
				this.id = id;
				this.constructor = constructor;
			}

			// Token: 0x06002D61 RID: 11617 RVA: 0x001CECE8 File Offset: 0x001CCEE8
			public void GetPath(List<Vector3> output)
			{
				this.constructor.GetPath(this, output);
			}

			// Token: 0x06002D62 RID: 11618 RVA: 0x001CECFC File Offset: 0x001CCEFC
			public int CompareTo(AdvancedSmooth.Turn t)
			{
				if (t.score > this.score)
				{
					return -1;
				}
				if (t.score >= this.score)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06002D63 RID: 11619 RVA: 0x001CED21 File Offset: 0x001CCF21
			public static bool operator <(AdvancedSmooth.Turn lhs, AdvancedSmooth.Turn rhs)
			{
				return lhs.score < rhs.score;
			}

			// Token: 0x06002D64 RID: 11620 RVA: 0x001CED33 File Offset: 0x001CCF33
			public static bool operator >(AdvancedSmooth.Turn lhs, AdvancedSmooth.Turn rhs)
			{
				return lhs.score > rhs.score;
			}

			// Token: 0x040049E1 RID: 18913
			public float length;

			// Token: 0x040049E2 RID: 18914
			public int id;

			// Token: 0x040049E3 RID: 18915
			public AdvancedSmooth.TurnConstructor constructor;
		}
	}
}
