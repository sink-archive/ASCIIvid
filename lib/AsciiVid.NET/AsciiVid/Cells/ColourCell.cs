using System.Text;

namespace AsciiVid.Cells
{
	public class ColourCell : Cell
	{
		/// <summary>
		///     The red colour channel of the cell
		/// </summary>
		public byte RedChannel;

		/// <summary>
		///     The green colour channel of the cell
		/// </summary>
		public byte GreenChannel;

		/// <summary>
		///     The blue colour channel of the cell
		/// </summary>
		public byte BlueChannel;

		public new byte[] GetBinary() => new[]
			{Encoding.ASCII.GetBytes(new[] {Character})[0], RedChannel, GreenChannel, BlueChannel};
	}
}