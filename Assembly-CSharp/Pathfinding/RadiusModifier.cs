using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000584 RID: 1412
	[AddComponentMenu("Pathfinding/Modifiers/Radius Offset")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_radius_modifier.php")]
	public class RadiusModifier : MonoModifier
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x001A5F36 File Offset: 0x001A4136
		public override int Order
		{
			get
			{
				return 41;
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x001A5F3C File Offset: 0x001A413C
		private bool CalculateCircleInner(Vector3 p1, Vector3 p2, float r1, float r2, out float a, out float sigma)
		{
			float magnitude = (p1 - p2).magnitude;
			if (r1 + r2 > magnitude)
			{
				a = 0f;
				sigma = 0f;
				return false;
			}
			a = (float)Math.Acos((double)((r1 + r2) / magnitude));
			sigma = (float)Math.Atan2((double)(p2.z - p1.z), (double)(p2.x - p1.x));
			return true;
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x001A5FA8 File Offset: 0x001A41A8
		private bool CalculateCircleOuter(Vector3 p1, Vector3 p2, float r1, float r2, out float a, out float sigma)
		{
			float magnitude = (p1 - p2).magnitude;
			if (Math.Abs(r1 - r2) > magnitude)
			{
				a = 0f;
				sigma = 0f;
				return false;
			}
			a = (float)Math.Acos((double)((r1 - r2) / magnitude));
			sigma = (float)Math.Atan2((double)(p2.z - p1.z), (double)(p2.x - p1.x));
			return true;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x001A601C File Offset: 0x001A421C
		private RadiusModifier.TangentType CalculateTangentType(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
		{
			bool flag = VectorMath.RightOrColinearXZ(p1, p2, p3);
			bool flag2 = VectorMath.RightOrColinearXZ(p2, p3, p4);
			return (RadiusModifier.TangentType)(1 << ((flag ? 2 : 0) + (flag2 ? 1 : 0) & 31));
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x001A6050 File Offset: 0x001A4250
		private RadiusModifier.TangentType CalculateTangentTypeSimple(Vector3 p1, Vector3 p2, Vector3 p3)
		{
			bool flag = VectorMath.RightOrColinearXZ(p1, p2, p3);
			bool flag2 = flag;
			return (RadiusModifier.TangentType)(1 << ((flag2 ? 2 : 0) + (flag ? 1 : 0) & 31));
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x001A607C File Offset: 0x001A427C
		public override void Apply(Path p)
		{
			List<Vector3> vectorPath = p.vectorPath;
			List<Vector3> list = this.Apply(vectorPath);
			if (list != vectorPath)
			{
				ListPool<Vector3>.Release(ref p.vectorPath);
				p.vectorPath = list;
			}
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x001A60B0 File Offset: 0x001A42B0
		public List<Vector3> Apply(List<Vector3> vs)
		{
			if (vs == null || vs.Count < 3)
			{
				return vs;
			}
			if (this.radi.Length < vs.Count)
			{
				this.radi = new float[vs.Count];
				this.a1 = new float[vs.Count];
				this.a2 = new float[vs.Count];
				this.dir = new bool[vs.Count];
			}
			for (int i = 0; i < vs.Count; i++)
			{
				this.radi[i] = this.radius;
			}
			this.radi[0] = 0f;
			this.radi[vs.Count - 1] = 0f;
			int num = 0;
			for (int j = 0; j < vs.Count - 1; j++)
			{
				num++;
				if (num > 2 * vs.Count)
				{
					Debug.LogWarning("Could not resolve radiuses, the path is too complex. Try reducing the base radius");
					break;
				}
				RadiusModifier.TangentType tangentType;
				if (j == 0)
				{
					tangentType = this.CalculateTangentTypeSimple(vs[j], vs[j + 1], vs[j + 2]);
				}
				else if (j == vs.Count - 2)
				{
					tangentType = this.CalculateTangentTypeSimple(vs[j - 1], vs[j], vs[j + 1]);
				}
				else
				{
					tangentType = this.CalculateTangentType(vs[j - 1], vs[j], vs[j + 1], vs[j + 2]);
				}
				float num4;
				float num5;
				if ((tangentType & RadiusModifier.TangentType.Inner) != (RadiusModifier.TangentType)0)
				{
					float num2;
					float num3;
					if (!this.CalculateCircleInner(vs[j], vs[j + 1], this.radi[j], this.radi[j + 1], out num2, out num3))
					{
						float magnitude = (vs[j + 1] - vs[j]).magnitude;
						this.radi[j] = magnitude * (this.radi[j] / (this.radi[j] + this.radi[j + 1]));
						this.radi[j + 1] = magnitude - this.radi[j];
						this.radi[j] *= 0.99f;
						this.radi[j + 1] *= 0.99f;
						j -= 2;
					}
					else if (tangentType == RadiusModifier.TangentType.InnerRightLeft)
					{
						this.a2[j] = num3 - num2;
						this.a1[j + 1] = num3 - num2 + 3.1415927f;
						this.dir[j] = true;
					}
					else
					{
						this.a2[j] = num3 + num2;
						this.a1[j + 1] = num3 + num2 + 3.1415927f;
						this.dir[j] = false;
					}
				}
				else if (!this.CalculateCircleOuter(vs[j], vs[j + 1], this.radi[j], this.radi[j + 1], out num4, out num5))
				{
					if (j == vs.Count - 2)
					{
						this.radi[j] = (vs[j + 1] - vs[j]).magnitude;
						this.radi[j] *= 0.99f;
						j--;
					}
					else
					{
						if (this.radi[j] > this.radi[j + 1])
						{
							this.radi[j + 1] = this.radi[j] - (vs[j + 1] - vs[j]).magnitude;
						}
						else
						{
							this.radi[j + 1] = this.radi[j] + (vs[j + 1] - vs[j]).magnitude;
						}
						this.radi[j + 1] *= 0.99f;
					}
					j--;
				}
				else if (tangentType == RadiusModifier.TangentType.OuterRight)
				{
					this.a2[j] = num5 - num4;
					this.a1[j + 1] = num5 - num4;
					this.dir[j] = true;
				}
				else
				{
					this.a2[j] = num5 + num4;
					this.a1[j + 1] = num5 + num4;
					this.dir[j] = false;
				}
			}
			List<Vector3> list = ListPool<Vector3>.Claim();
			list.Add(vs[0]);
			if (this.detail < 1f)
			{
				this.detail = 1f;
			}
			float num6 = 6.2831855f / this.detail;
			for (int k = 1; k < vs.Count - 1; k++)
			{
				float num7 = this.a1[k];
				float num8 = this.a2[k];
				float d = this.radi[k];
				if (this.dir[k])
				{
					if (num8 < num7)
					{
						num8 += 6.2831855f;
					}
					for (float num9 = num7; num9 < num8; num9 += num6)
					{
						list.Add(new Vector3((float)Math.Cos((double)num9), 0f, (float)Math.Sin((double)num9)) * d + vs[k]);
					}
				}
				else
				{
					if (num7 < num8)
					{
						num7 += 6.2831855f;
					}
					for (float num10 = num7; num10 > num8; num10 -= num6)
					{
						list.Add(new Vector3((float)Math.Cos((double)num10), 0f, (float)Math.Sin((double)num10)) * d + vs[k]);
					}
				}
			}
			list.Add(vs[vs.Count - 1]);
			return list;
		}

		// Token: 0x04004132 RID: 16690
		public float radius = 1f;

		// Token: 0x04004133 RID: 16691
		public float detail = 10f;

		// Token: 0x04004134 RID: 16692
		private float[] radi = new float[10];

		// Token: 0x04004135 RID: 16693
		private float[] a1 = new float[10];

		// Token: 0x04004136 RID: 16694
		private float[] a2 = new float[10];

		// Token: 0x04004137 RID: 16695
		private bool[] dir = new bool[10];

		// Token: 0x02000752 RID: 1874
		[Flags]
		private enum TangentType
		{
			// Token: 0x040049E5 RID: 18917
			OuterRight = 1,
			// Token: 0x040049E6 RID: 18918
			InnerRightLeft = 2,
			// Token: 0x040049E7 RID: 18919
			InnerLeftRight = 4,
			// Token: 0x040049E8 RID: 18920
			OuterLeft = 8,
			// Token: 0x040049E9 RID: 18921
			Outer = 9,
			// Token: 0x040049EA RID: 18922
			Inner = 6
		}
	}
}
