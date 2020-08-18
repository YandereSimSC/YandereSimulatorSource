using System;

namespace Pathfinding
{
	// Token: 0x02000529 RID: 1321
	public struct Int2 : IEquatable<Int2>
	{
		// Token: 0x060022BB RID: 8891 RVA: 0x00191809 File Offset: 0x0018FA09
		public Int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x00191819 File Offset: 0x0018FA19
		public long sqrMagnitudeLong
		{
			get
			{
				return (long)this.x * (long)this.x + (long)this.y * (long)this.y;
			}
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0019183A File Offset: 0x0018FA3A
		public static Int2 operator +(Int2 a, Int2 b)
		{
			return new Int2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0019185B File Offset: 0x0018FA5B
		public static Int2 operator -(Int2 a, Int2 b)
		{
			return new Int2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0019187C File Offset: 0x0018FA7C
		public static bool operator ==(Int2 a, Int2 b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0019189C File Offset: 0x0018FA9C
		public static bool operator !=(Int2 a, Int2 b)
		{
			return a.x != b.x || a.y != b.y;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x001918BF File Offset: 0x0018FABF
		public static long DotLong(Int2 a, Int2 b)
		{
			return (long)a.x * (long)b.x + (long)a.y * (long)b.y;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x001918E0 File Offset: 0x0018FAE0
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			Int2 @int = (Int2)o;
			return this.x == @int.x && this.y == @int.y;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0019187C File Offset: 0x0018FA7C
		public bool Equals(Int2 other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x00191917 File Offset: 0x0018FB17
		public override int GetHashCode()
		{
			return this.x * 49157 + this.y * 98317;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x00191934 File Offset: 0x0018FB34
		[Obsolete("Deprecated becuase it is not used by any part of the A* Pathfinding Project")]
		public static Int2 Rotate(Int2 v, int r)
		{
			r %= 4;
			return new Int2(v.x * Int2.Rotations[r * 4] + v.y * Int2.Rotations[r * 4 + 1], v.x * Int2.Rotations[r * 4 + 2] + v.y * Int2.Rotations[r * 4 + 3]);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00191993 File Offset: 0x0018FB93
		public static Int2 Min(Int2 a, Int2 b)
		{
			return new Int2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x001919BC File Offset: 0x0018FBBC
		public static Int2 Max(Int2 a, Int2 b)
		{
			return new Int2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x001919E5 File Offset: 0x0018FBE5
		public static Int2 FromInt3XZ(Int3 o)
		{
			return new Int2(o.x, o.z);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x001919F8 File Offset: 0x0018FBF8
		public static Int3 ToInt3XZ(Int2 o)
		{
			return new Int3(o.x, 0, o.y);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x00191A0C File Offset: 0x0018FC0C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.x,
				", ",
				this.y,
				")"
			});
		}

		// Token: 0x04003F15 RID: 16149
		public int x;

		// Token: 0x04003F16 RID: 16150
		public int y;

		// Token: 0x04003F17 RID: 16151
		private static readonly int[] Rotations = new int[]
		{
			1,
			0,
			0,
			1,
			0,
			1,
			-1,
			0,
			-1,
			0,
			0,
			-1,
			0,
			-1,
			1,
			0
		};
	}
}
