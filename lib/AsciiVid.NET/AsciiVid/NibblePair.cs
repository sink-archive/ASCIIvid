using System;

namespace AsciiVid
{
	/// <summary>
	///     Stores one or two nibbles (4 bits)
	/// </summary>
	public struct NibblePair
	{
		public bool Pair;

		public byte RawBinary { get; }

		public byte FirstNibble
		{
			get => RawBinary.GetLowNibble();
			set => RawBinary.SetLowNibble(value);
		}

		public byte SecondNibble
		{
			get =>
				Pair
					? RawBinary.GetHighNibble()
					: throw new InvalidOperationException("Only one nibble is stored. Use FirstNibble instead.");
			set
			{
				RawBinary.SeHighNibble(value);
				Pair = true;
			}
		}

		public void ClearSecondNibble()
		{
			SecondNibble = 0;
			Pair         = false;
		}

		public static NibblePair Parse(byte nibble) => new NibblePair {FirstNibble = nibble};

		public static NibblePair Parse(byte firstNibble, byte secondNibble) => new NibblePair
			{FirstNibble = firstNibble, SecondNibble = secondNibble};

		public static NibblePair Combine(Nibble firstNibble, Nibble secondNibble) => new NibblePair
			{FirstNibble = firstNibble.Value, SecondNibble = secondNibble.Value};
	}

	public struct Nibble
	{
		public byte RawBinary { get; }

		public byte Value
		{
			get => RawBinary.GetLowNibble();
			set => RawBinary.SetLowNibble(value);
		}
	}

	public static class NibbleTools
	{
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