using System;
using System.Text;

namespace Pathfinding.Util
{
	// Token: 0x020005F6 RID: 1526
	public struct Guid
	{
		// Token: 0x060029CB RID: 10699 RVA: 0x001C441C File Offset: 0x001C261C
		public Guid(byte[] bytes)
		{
			ulong num = (ulong)bytes[0] | (ulong)bytes[1] << 8 | (ulong)bytes[2] << 16 | (ulong)bytes[3] << 24 | (ulong)bytes[4] << 32 | (ulong)bytes[5] << 40 | (ulong)bytes[6] << 48 | (ulong)bytes[7] << 56;
			ulong num2 = (ulong)bytes[8] | (ulong)bytes[9] << 8 | (ulong)bytes[10] << 16 | (ulong)bytes[11] << 24 | (ulong)bytes[12] << 32 | (ulong)bytes[13] << 40 | (ulong)bytes[14] << 48 | (ulong)bytes[15] << 56;
			this._a = (BitConverter.IsLittleEndian ? num : Guid.SwapEndianness(num));
			this._b = (BitConverter.IsLittleEndian ? num2 : Guid.SwapEndianness(num2));
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x001C44D4 File Offset: 0x001C26D4
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

		// Token: 0x060029CD RID: 10701 RVA: 0x001C460B File Offset: 0x001C280B
		public static Guid Parse(string input)
		{
			return new Guid(input);
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x001C4614 File Offset: 0x001C2814
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

		// Token: 0x060029CF RID: 10703 RVA: 0x001C46A4 File Offset: 0x001C28A4
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

		// Token: 0x060029D0 RID: 10704 RVA: 0x001C4714 File Offset: 0x001C2914
		public static Guid NewGuid()
		{
			byte[] array = new byte[16];
			Guid.random.NextBytes(array);
			return new Guid(array);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x001C473A File Offset: 0x001C293A
		public static bool operator ==(Guid lhs, Guid rhs)
		{
			return lhs._a == rhs._a && lhs._b == rhs._b;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x001C475A File Offset: 0x001C295A
		public static bool operator !=(Guid lhs, Guid rhs)
		{
			return lhs._a != rhs._a || lhs._b != rhs._b;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x001C4780 File Offset: 0x001C2980
		public override bool Equals(object _rhs)
		{
			if (!(_rhs is Guid))
			{
				return false;
			}
			Guid guid = (Guid)_rhs;
			return this._a == guid._a && this._b == guid._b;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x001C47BC File Offset: 0x001C29BC
		public override int GetHashCode()
		{
			ulong num = this._a ^ this._b;
			return (int)(num >> 32) ^ (int)num;
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x001C47E0 File Offset: 0x001C29E0
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

		// Token: 0x0400440E RID: 17422
		private const string hex = "0123456789ABCDEF";

		// Token: 0x0400440F RID: 17423
		public static readonly Guid zero = new Guid(new byte[16]);

		// Token: 0x04004410 RID: 17424
		public static readonly string zeroString = new Guid(new byte[16]).ToString();

		// Token: 0x04004411 RID: 17425
		private readonly ulong _a;

		// Token: 0x04004412 RID: 17426
		private readonly ulong _b;

		// Token: 0x04004413 RID: 17427
		private static Random random = new Random();

		// Token: 0x04004414 RID: 17428
		private static StringBuilder text;
	}
}
