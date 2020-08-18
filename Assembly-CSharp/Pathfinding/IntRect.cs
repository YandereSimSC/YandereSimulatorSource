using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200054A RID: 1354
	[Serializable]
	public struct IntRect
	{
		// Token: 0x06002406 RID: 9222 RVA: 0x001961EE File Offset: 0x001943EE
		public IntRect(int xmin, int ymin, int xmax, int ymax)
		{
			this.xmin = xmin;
			this.xmax = xmax;
			this.ymin = ymin;
			this.ymax = ymax;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x0019620D File Offset: 0x0019440D
		public bool Contains(int x, int y)
		{
			return x >= this.xmin && y >= this.ymin && x <= this.xmax && y <= this.ymax;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x00196238 File Offset: 0x00194438
		public int Width
		{
			get
			{
				return this.xmax - this.xmin + 1;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x00196249 File Offset: 0x00194449
		public int Height
		{
			get
			{
				return this.ymax - this.ymin + 1;
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x0019625A File Offset: 0x0019445A
		public bool IsValid()
		{
			return this.xmin <= this.xmax && this.ymin <= this.ymax;
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0019627D File Offset: 0x0019447D
		public static bool operator ==(IntRect a, IntRect b)
		{
			return a.xmin == b.xmin && a.xmax == b.xmax && a.ymin == b.ymin && a.ymax == b.ymax;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x001962B9 File Offset: 0x001944B9
		public static bool operator !=(IntRect a, IntRect b)
		{
			return a.xmin != b.xmin || a.xmax != b.xmax || a.ymin != b.ymin || a.ymax != b.ymax;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x001962F8 File Offset: 0x001944F8
		public override bool Equals(object obj)
		{
			IntRect intRect = (IntRect)obj;
			return this.xmin == intRect.xmin && this.xmax == intRect.xmax && this.ymin == intRect.ymin && this.ymax == intRect.ymax;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00196346 File Offset: 0x00194546
		public override int GetHashCode()
		{
			return this.xmin * 131071 ^ this.xmax * 3571 ^ this.ymin * 3109 ^ this.ymax * 7;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00196378 File Offset: 0x00194578
		public static IntRect Intersection(IntRect a, IntRect b)
		{
			return new IntRect(Math.Max(a.xmin, b.xmin), Math.Max(a.ymin, b.ymin), Math.Min(a.xmax, b.xmax), Math.Min(a.ymax, b.ymax));
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x001963CE File Offset: 0x001945CE
		public static bool Intersects(IntRect a, IntRect b)
		{
			return a.xmin <= b.xmax && a.ymin <= b.ymax && a.xmax >= b.xmin && a.ymax >= b.ymin;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00196410 File Offset: 0x00194610
		public static IntRect Union(IntRect a, IntRect b)
		{
			return new IntRect(Math.Min(a.xmin, b.xmin), Math.Min(a.ymin, b.ymin), Math.Max(a.xmax, b.xmax), Math.Max(a.ymax, b.ymax));
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00196466 File Offset: 0x00194666
		public IntRect ExpandToContain(int x, int y)
		{
			return new IntRect(Math.Min(this.xmin, x), Math.Min(this.ymin, y), Math.Max(this.xmax, x), Math.Max(this.ymax, y));
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0019649D File Offset: 0x0019469D
		public IntRect Expand(int range)
		{
			return new IntRect(this.xmin - range, this.ymin - range, this.xmax + range, this.ymax + range);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x001964C4 File Offset: 0x001946C4
		public IntRect Rotate(int r)
		{
			int num = IntRect.Rotations[r * 4];
			int num2 = IntRect.Rotations[r * 4 + 1];
			int num3 = IntRect.Rotations[r * 4 + 2];
			int num4 = IntRect.Rotations[r * 4 + 3];
			int val = num * this.xmin + num2 * this.ymin;
			int val2 = num3 * this.xmin + num4 * this.ymin;
			int val3 = num * this.xmax + num2 * this.ymax;
			int val4 = num3 * this.xmax + num4 * this.ymax;
			return new IntRect(Math.Min(val, val3), Math.Min(val2, val4), Math.Max(val, val3), Math.Max(val2, val4));
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x0019656F File Offset: 0x0019476F
		public IntRect Offset(Int2 offset)
		{
			return new IntRect(this.xmin + offset.x, this.ymin + offset.y, this.xmax + offset.x, this.ymax + offset.y);
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x001965AA File Offset: 0x001947AA
		public IntRect Offset(int x, int y)
		{
			return new IntRect(this.xmin + x, this.ymin + y, this.xmax + x, this.ymax + y);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x001965D4 File Offset: 0x001947D4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[x: ",
				this.xmin,
				"...",
				this.xmax,
				", y: ",
				this.ymin,
				"...",
				this.ymax,
				"]"
			});
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x00196650 File Offset: 0x00194850
		public void DebugDraw(GraphTransform transform, Color color)
		{
			Vector3 vector = transform.Transform(new Vector3((float)this.xmin, 0f, (float)this.ymin));
			Vector3 vector2 = transform.Transform(new Vector3((float)this.xmin, 0f, (float)this.ymax));
			Vector3 vector3 = transform.Transform(new Vector3((float)this.xmax, 0f, (float)this.ymax));
			Vector3 vector4 = transform.Transform(new Vector3((float)this.xmax, 0f, (float)this.ymin));
			Debug.DrawLine(vector, vector2, color);
			Debug.DrawLine(vector2, vector3, color);
			Debug.DrawLine(vector3, vector4, color);
			Debug.DrawLine(vector4, vector, color);
		}

		// Token: 0x04003FD4 RID: 16340
		public int xmin;

		// Token: 0x04003FD5 RID: 16341
		public int ymin;

		// Token: 0x04003FD6 RID: 16342
		public int xmax;

		// Token: 0x04003FD7 RID: 16343
		public int ymax;

		// Token: 0x04003FD8 RID: 16344
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
