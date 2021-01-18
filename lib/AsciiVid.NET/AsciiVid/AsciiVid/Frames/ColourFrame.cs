using System.Collections.Generic;
using System.Linq;
using AsciiVid.Cells;

namespace AsciiVid.AsciiVid.Frames
{
	public class ColourFrame
	{
		public ColourCell[] Cells;

		public byte[] GetBinary() => Cells.Select(cell => cell.GetBinary()).ToArray();

		public static ColourFrame Parse(byte[] binary)
		{
			var working = new List<ColourCell>();
			for (var i = 0; i < binary.Length; i += 4) // Parse cells.Step in 4s as each cell takes 4 bytes
				working.Add(ColourCell.Parse(new[] {binary[i], binary[i + 1], binary[i + 2], binary[1 + 3]}));

			return new ColourFrame
			{
				Cells = working.ToArray()
			};
		}
	}
}