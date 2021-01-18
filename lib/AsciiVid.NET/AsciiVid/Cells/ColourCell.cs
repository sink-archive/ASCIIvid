using System;
using System.Drawing;
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
		/// <summary>
		///		The corresponsing Sytem.Drawing.Color for the cell
		/// </summary>
		public Color Colour => Color.FromArgb(RedChannel, GreenChannel, BlueChannel);

		public new byte[] GetBinary() => new[]
			{Encoding.ASCII.GetBytes(new[] {Character})[0], RedChannel, GreenChannel, BlueChannel};

		public static ColourCell Parse(byte[] binary)
		{
			return new ColourCell
			{
				Character = Encoding.ASCII.GetChars(new[] {binary[0]})[0], // Character is first byte
				RedChannel = binary[1], // Next three bytes are colour
				GreenChannel = binary[2],
				BlueChannel = binary[3]
			}
		}

		public new static ColourCell Parse(byte binary) => throw new ArgumentException();
	}
}