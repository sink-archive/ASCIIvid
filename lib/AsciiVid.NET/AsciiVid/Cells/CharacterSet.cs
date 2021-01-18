namespace AsciiVid.Cells
{
	/// <summary>
	///     Represents a set of characters to be used for Simple Cells
	/// </summary>
	public class CharacterSet
	{
		/// <summary>
		///     The default set
		/// </summary>
		public static CharacterSet DefaultSet =
			new CharacterSet(' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ');

		public CharacterSet(params char[] brightness1Char)
		{
			BrightnessChars = brightness1Char;
		}

		/// <summary>
		///     The characters in this set.
		/// </summary>
		public char[] BrightnessChars;
	}
}