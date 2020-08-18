using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000528 RID: 1320
	public struct Int3 : IEquatable<Int3>
	{
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x00191228 File Offset: 0x0018F428
		public static Int3 zero
		{
			get
			{
				return default(Int3);
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x00191240 File Offset: 0x0018F440
		public Int3(Vector3 position)
		{
			this.x = (int)Math.Round((double)(position.x * 1000f));
			this.y = (int)Math.Round((double)(position.y * 1000f));
			this.z = (int)Math.Round((double)(position.z * 1000f));
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x00191298 File Offset: 0x0018F498
		public Int3(int _x, int _y, int _z)
		{
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x001912AF File Offset: 0x0018F4AF
		public static bool operator ==(Int3 lhs, Int3 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x001912DD File Offset: 0x0018F4DD
		public static bool operator !=(Int3 lhs, Int3 rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0019130E File Offset: 0x0018F50E
		public static explicit operator Int3(Vector3 ob)
		{
			return new Int3((int)Math.Round((double)(ob.x * 1000f)), (int)Math.Round((double)(ob.y * 1000f)), (int)Math.Round((double)(ob.z * 1000f)));
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0019134E File Offset: 0x0018F54E
		public static explicit operator Vector3(Int3 ob)
		{
			return new Vector3((float)ob.x * 0.001f, (float)ob.y * 0.001f, (float)ob.z * 0.001f);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0019137C File Offset: 0x0018F57C
		public static Int3 operator -(Int3 lhs, Int3 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			lhs.z -= rhs.z;
			return lhs;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x001913B2 File Offset: 0x0018F5B2
		public static Int3 operator -(Int3 lhs)
		{
			lhs.x = -lhs.x;
			lhs.y = -lhs.y;
			lhs.z = -lhs.z;
			return lhs;
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x001913DF File Offset: 0x0018F5DF
		public static Int3 operator +(Int3 lhs, Int3 rhs)
		{
			lhs.x += rhs.x;
			lhs.y += rhs.y;
			lhs.z += rhs.z;
			return lhs;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x00191415 File Offset: 0x0018F615
		public static Int3 operator *(Int3 lhs, int rhs)
		{
			lhs.x *= rhs;
			lhs.y *= rhs;
			lhs.z *= rhs;
			return lhs;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0019143C File Offset: 0x0018F63C
		public static Int3 operator *(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x * rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y * rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z * rhs));
			return lhs;
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00191490 File Offset: 0x0018F690
		public static Int3 operator *(Int3 lhs, double rhs)
		{
			lhs.x = (int)Math.Round((double)lhs.x * rhs);
			lhs.y = (int)Math.Round((double)lhs.y * rhs);
			lhs.z = (int)Math.Round((double)lhs.z * rhs);
			return lhs;
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x001914E0 File Offset: 0x0018F6E0
		public static Int3 operator /(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x / rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y / rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z / rhs));
			return lhs;
		}

		// Token: 0x17000537 RID: 1335
		public int this[int i]
		{
			get
			{
				if (i == 0)
				{
					return this.x;
				}
				if (i != 1)
				{
					return this.z;
				}
				return this.y;
			}
			set
			{
				if (i == 0)
				{
					this.x = value;
					return;
				}
				if (i == 1)
				{
					this.y = value;
					return;
				}
				this.z = value;
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x00191570 File Offset: 0x0018F770
		public static float Angle(Int3 lhs, Int3 rhs)
		{
			double num = (double)Int3.Dot(lhs, rhs) / ((double)lhs.magnitude * (double)rhs.magnitude);
			num = ((num < -1.0) ? -1.0 : ((num > 1.0) ? 1.0 : num));
			return (float)Math.Acos(num);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x001915CF File Offset: 0x0018F7CF
		public static int Dot(Int3 lhs, Int3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x001915FA File Offset: 0x0018F7FA
		public static long DotLong(Int3 lhs, Int3 rhs)
		{
			return (long)lhs.x * (long)rhs.x + (long)lhs.y * (long)rhs.y + (long)lhs.z * (long)rhs.z;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0019162B File Offset: 0x0018F82B
		public Int3 Normal2D()
		{
			return new Int3(this.z, this.y, -this.x);
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x00191648 File Offset: 0x0018F848
		public float magnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x0019167C File Offset: 0x0018F87C
		public int costMagnitude
		{
			get
			{
				return (int)Math.Round((double)this.magnitude);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x0019168C File Offset: 0x0018F88C
		[Obsolete("This property is deprecated. Use magnitude or cast to a Vector3")]
		public float worldMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3) * 0.001f;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x001916C8 File Offset: 0x0018F8C8
		public float sqrMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x001916F8 File Offset: 0x0018F8F8
		public long sqrMagnitudeLong
		{
			get
			{
				long num = (long)this.x;
				long num2 = (long)this.y;
				long num3 = (long)this.z;
				return num * num + num2 * num2 + num3 * num3;
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00191726 File Offset: 0x0018F926
		public static implicit operator string(Int3 obj)
		{
			return obj.ToString();
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00191738 File Offset: 0x0018F938
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.x,
				", ",
				this.y,
				", ",
				this.z,
				")"
			});
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0019179C File Offset: 0x0018F99C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Int3 @int = (Int3)obj;
			return this.x == @int.x && this.y == @int.y && this.z == @int.z;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x001912AF File Offset: 0x0018F4AF
		public bool Equals(Int3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x001917E1 File Offset: 0x0018F9E1
		public override int GetHashCode()
		{
			return this.x * 73856093 ^ this.y * 19349663 ^ this.z * 83492791;
		}

		// Token: 0x04003F0F RID: 16143
		public int x;

		// Token: 0x04003F10 RID: 16144
		public int y;

		// Token: 0x04003F11 RID: 16145
		public int z;

		// Token: 0x04003F12 RID: 16146
		public const int Precision = 1000;

		// Token: 0x04003F13 RID: 16147
		public const float FloatPrecision = 1000f;

		// Token: 0x04003F14 RID: 16148
		public const float PrecisionFactor = 0.001f;
	}
}
