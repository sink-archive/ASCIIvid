using System;

namespace AsciiVid
{
	public static class Utilities
	{
		public static ushort ToUInt16(byte firstByte, byte secondByte) => (ushort) (firstByte + (secondByte << 8));

		public static byte GetLowNibble(this  byte input) => (byte) ((input & 0xF0) >> 4);
		public static byte GetHighNibble(this byte input) => (byte) (input & 0x0F);

		public static void SetLowNibble(this byte input, byte newValue)
		{
			if (!newValue.IsValidNibble())
				throw new ArgumentOutOfRangeException(nameof(newValue),
				                                      "Too big for a nibble! Store a value between 0-15");
			input = (byte) (input & (0xF0 + newValue));
		}

		public static void SeHighNibble(this byte input, byte newValue)
		{
			if (!newValue.IsValidNibble())
				throw new ArgumentOutOfRangeException(nameof(newValue),
				                                      "Too big for a nibble! Store a value between 0-15");
			input = (byte) (input & (0x0F + (newValue << 4)));
		}

		public static bool IsValidNibble(this byte input) => input < 16;
	}
}