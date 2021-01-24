namespace AsciiVid.Cells
{
	/// <summary>
	///     Represents a simple cell
	/// </summary>
	public class SimpleCell : ICellBase
	{
		/// <summary>
		///     The brightness level of the cell
		/// </summary>
		public Nibble Brightness;

		public SimpleCell(Nibble brightness)
		{
			Brightness = brightness;
		}

		public SimpleCell(byte binary)
		{
			var parsed = ParseSingle(binary);
			Brightness = parsed.Brightness;
		}

		/// <summary>
		///     Gets the raw binary of this cell
		/// </summary>
		/// ]
		public byte GetBinary() => Brightness.Value;

		/// <summary>
		///     Combines two simple cells into one byte ready for storage
		/// </summary>
		/// <param name="firstCell"></param>
		/// <param name="secondCell"></param>
		/// <returns></returns>
		public static byte GetCombinedBinary(SimpleCell firstCell, SimpleCell secondCell) =>
			NibblePair.Combine(firstCell.Brightness, secondCell.Brightness).RawBinary;

		/// <summary>
		///     Parses a single simple cell
		/// </summary>
		public static SimpleCell ParseSingle(byte input) => ParseSingle(new Nibble {Value = input});

		/// <summary>
		///     Parses a single simple cell
		/// </summary>
		public static SimpleCell ParseSingle(Nibble input) => new SimpleCell(input);

		/// <summary>
		///     Parses a pair of simple cells
		/// </summary>
		public static Pair<SimpleCell> ParsePair(byte input) => ParsePair(new NibblePair
		{
			FirstNibble = input.GetLowNibble(), SecondNibble = input.GetHighNibble()
		});

		/// <summary>
		///     Parses a pair of simple cells
		/// </summary>
		public static Pair<SimpleCell> ParsePair(NibblePair input) => new Pair<SimpleCell>
		{
			FirstItem  = new SimpleCell(new Nibble {Value = input.FirstNibble}),
			SecondItem = new SimpleCell(new Nibble {Value = input.SecondNibble})
		};

		/// <summary>
		///     Gets the character of this cell from the given character set
		/// </summary>
		public char ToChar(CharacterSet charSet) => charSet.BrightnessChars[Brightness.Value - 1];

		/// <summary>
		///     Gets the character of this cell from the given default set
		/// </summary>
		public char ToChar() => ToChar(CharacterSet.DefaultSet);

		/// <summary>
		///     Gets the string representation of this cell from the standard character set
		/// </summary>
		public override string ToString() => ToChar().ToString();

		/// <summary>
		///     Gets the string representation of this cell from the given character set
		/// </summary>
		public string ToString(CharacterSet charSet) => ToChar(charSet).ToString();
	}

	public class Pair<T>
	{
		public T FirstItem;
		public T SecondItem;
	}
}