using System.Diagnostics;
using System.Text;

namespace AsciiVid.Cells
{
	[DebuggerDisplay("{Character}")]
	/// <summary>
	///     Represents one ASCII art cell
	/// </summary>
	public class Cell
	{
		/// <summary>
		///     The character for this cell
		/// </summary>
		public char Character;

		public Cell(char character) => Character = character;

		public Cell(byte binary)
		{
			var parsed = Parse(binary);
			Character = parsed.Character;
		}

		/// <summary>
		///     Gets the cell as a string
		/// </summary>
		public override string ToString() => Character.ToString();

		/// <summary>
		///     Gets the raw binary representation of this cell
		/// </summary>
		/// <returns></returns>
		public byte GetBinary() => Encoding.ASCII.GetBytes(new[] {Character})[0];

		/// <summary>
		///     Parses raw binary representing a cell
		/// </summary>
		/// <param name="binary"></param>
		/// <returns></returns>
		public static Cell Parse(byte binary) => new Cell(Encoding.ASCII.GetChars(new[] {binary})[0]);

		/// <summary>
		///     Attempts to parse raw binary data representing a cell
		/// </summary>
		/// <param name="binary"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public static bool TryParse(byte binary, out Cell result)
		{
			try
			{
				result = Parse(binary);
			}
			catch
			{
				result = null;
				return false;
			}

			return true;
		}
	}
}