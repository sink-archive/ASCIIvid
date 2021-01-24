using System;
using System.Diagnostics;

namespace AsciiVid
{
	[DebuggerDisplay("{FirstNibble}, {SecondNibble}")]
	/// <summary>
	///     Stores one or two nibbles (4 bits)
	/// </summary>
	public struct NibblePair
	{
		public bool Pair;

		public byte RawBinary { get; }

		public NibblePair(Nibble nibble) : this()
		{
			RawBinary.SetLowNibble(nibble.Value);
		}

		public NibblePair(byte nibble) : this(new Nibble(nibble))
		{
		}

		public NibblePair(Nibble firstNibble, Nibble secondNibble) : this(firstNibble)
		{
			RawBinary.SeHighNibble(secondNibble.Value);
		}

		public NibblePair(byte firstNibble, byte secondNibble) : this(new Nibble(firstNibble), new Nibble(secondNibble))
		{
		}

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

		public static NibblePair Parse(byte firstNibble, byte secondNibble) =>
			new NibblePair(firstNibble, secondNibble);

		public static NibblePair Combine(Nibble firstNibble, Nibble secondNibble) =>
			new NibblePair(firstNibble, secondNibble);
	}

	[DebuggerDisplay("{Value}")]
	public struct Nibble
	{
		public Nibble(byte firstNibble) : this() => Value = firstNibble;

		public byte RawBinary { get; }

		public byte Value
		{
			get => RawBinary.GetHighNibble();
			set => RawBinary.SetLowNibble(value);
		}
	}
}