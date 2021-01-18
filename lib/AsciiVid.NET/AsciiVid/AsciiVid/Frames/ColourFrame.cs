using System.Collections.Generic;
using AsciiVid.Cells;

namespace AsciiVid.AsciiVid.Frames
{
	public class ColourFrame
	{
		public ColourCell[] Cells;

		public ColourFrame(ColourCell[] cells)
		{
			Cells = cells;
		}

		public ColourFrame(byte[] binary)
		{
			var parsed = Parse(binary);
			Cells = parsed.Cells;
		}

		public byte[] GetBinary()
		{
			var working = new List<byte>();
			foreach (var cell in Cells) working.AddRange(cell.GetBinary());
			return working.ToArray();
		}

		public static ColourFrame Parse(byte[] binary)
		{
			var working = new List<ColourCell>();
			for (var i = 0; i < binary.Length; i += 4) // Parse cells.Step in 4s as each cell takes 4 bytes
				working.Add(ColourCell.Parse(new[] {binary[i], binary[i + 1], binary[i + 2], binary[1 + 3]}));

			return new ColourFrame(working.ToArray());
		}
	}
}