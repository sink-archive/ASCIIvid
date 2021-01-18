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
			get => RawBinary.GetHighNibble();
			set => RawBinary.SetLowNibble(value);
		}
	}
}