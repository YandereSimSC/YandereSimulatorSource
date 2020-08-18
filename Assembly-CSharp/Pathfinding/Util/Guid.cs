using System;
using System.Text;

namespace Pathfinding.Util
{
	// Token: 0x020005E0 RID: 1504
	public struct Guid
	{
		// Token: 0x06002975 RID: 10613 RVA: 0x001BE830 File Offset: 0x001BCA30
		public Guid(byte[] bytes)
		{
			ulong num = (ulong)bytes[0] | (ulong)bytes[1] << 8 | (ulong)bytes[2] << 16 | (ulong)bytes[3] << 24 | (ulong)bytes[4] << 32 | (ulong)bytes[5] << 40 | (ulong)bytes[6] << 48 | (ulong)bytes[7] << 56;
			ulong num2 = (ulong)bytes[8] | (ulong)bytes[9] << 8 | (ulong)bytes[10] << 16 | (ulong)bytes[11] << 24 | (ulong)bytes[12] << 32 | (ulong)bytes[13] << 40 | (ulong)bytes[14] << 48 | (ulong)bytes[15] << 56;
			this._a = (BitConverter.IsLittleEndian ? num : Guid.SwapEndianness(num));
			this._b = (BitConverter.IsLittleEndian ? num2 : Guid.SwapEndianness(num2));
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x001BE8E8 File Offset: 0x001BCAE8
		public Guid(string str)
		{
			this._a = 0UL;
			this._b = 0UL;
			if (str.Length < 32)
			{
				throw new FormatException("Invalid Guid format");
			}
			int i = 0;
			int num = 0;
			int num2 = 60;
			while (i < 16)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c = str[num];
				if (c != '-')
				{
					int num3 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c));
					if (num3 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c.ToString() + " is not a hexadecimal character");
					}
					this._a |= (ulong)((ulong)((long)num3) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
			num2 = 60;
			while (i < 32)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c2 = str[num];
				if (c2 != '-')
				{
					int num4 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c2));
					if (num4 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c2.ToString() + " is not a hexadecimal character");
					}
					this._b |= (ulong)((ulong)((long)num4) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x001BEA1F File Offset: 0x001BCC1F
		public static Guid Parse(string input)
		{
			return new Guid(input);
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x001BEA28 File Offset: 0x001BCC28
		private static ulong SwapEndianness(ulong value)
		{
			ulong num = value & 255UL;
			ulong num2 = value >> 8 & 255UL;
			ulong num3 = value >> 16 & 255UL;
			ulong num4 = value >> 24 & 255UL;
			ulong num5 = value >> 32 & 255UL;
			ulong num6 = value >> 40 & 255UL;
			ulong num7 = value >> 48 & 255UL;
			ulong num8 = value >> 56 & 255UL;
			return num << 56 | num2 << 48 | num3 << 40 | num4 << 32 | num5 << 24 | num6 << 16 | num7 << 8 | num8;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x001BEAB8 File Offset: 0x001BCCB8
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes((!BitConverter.IsLittleEndian) ? Guid.SwapEndianness(this._a) : this._a);
			byte[] bytes2 = BitConverter.GetBytes((!BitConverter.IsLittleEndian) ? Guid.SwapEndianness(this._b) : this._b);
			for (int i = 0; i < 8; i++)
			{
				array[i] = bytes[i];
				array[i + 8] = bytes2[i];
			}
			return array;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x001BEB28 File Offset: 0x001BCD28
		public static Guid NewGuid()
		{
			byte[] array = new byte[16];
			Guid.random.NextBytes(array);
			return new Guid(array);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x001BEB4E File Offset: 0x001BCD4E
		public static bool operator ==(Guid lhs, Guid rhs)
		{
			return lhs._a == rhs._a && lhs._b == rhs._b;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x001BEB6E File Offset: 0x001BCD6E
		public static bool operator !=(Guid lhs, Guid rhs)
		{
			return lhs._a != rhs._a || lhs._b != rhs._b;
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x001BEB94 File Offset: 0x001BCD94
		public override bool Equals(object _rhs)
		{
			if (!(_rhs is Guid))
			{
				return false;
			}
			Guid guid = (Guid)_rhs;
			return this._a == guid._a && this._b == guid._b;
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x001BEBD0 File Offset: 0x001BCDD0
		public override int GetHashCode()
		{
			ulong num = this._a ^ this._b;
			return (int)(num >> 32) ^ (int)num;
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x001BEBF4 File Offset: 0x001BCDF4
		public override string ToString()
		{
			if (Guid.text == null)
			{
				Guid.text = new StringBuilder();
			}
			StringBuilder obj = Guid.text;
			string result;
			lock (obj)
			{
				Guid.text.Length = 0;
				Guid.text.Append(this._a.ToString("x16")).Append('-').Append(this._b.ToString("x16"));
				result = Guid.text.ToString();
			}
			return result;
		}

		// Token: 0x04004330 RID: 17200
		private const string hex = "0123456789ABCDEF";

		// Token: 0x04004331 RID: 17201
		public static readonly Guid zero = new Guid(new byte[16]);

		// Token: 0x04004332 RID: 17202
		public static readonly string zeroString = new Guid(new byte[16]).ToString();

		// Token: 0x04004333 RID: 17203
		private readonly ulong _a;

		// Token: 0x04004334 RID: 17204
		private readonly ulong _b;

		// Token: 0x04004335 RID: 17205
		private static Random random = new Random();

		// Token: 0x04004336 RID: 17206
		private static StringBuilder text;
	}
}
