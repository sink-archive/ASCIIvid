using System.Collections.Generic;
using AsciiVid.Cells;
using static AsciiVid.Utilities;

namespace AsciiVid.AsciiImg
{
	public class ColourImage : IImageBase
	{
		public ColourCell[] Cells;

		public ushort Width;
		public ushort Height;

		public ColourImage(ColourCell[] cells, ushort width, ushort height)
		{
			Cells  = cells;
			Width  = width;
			Height = height;
		}

		public ColourImage(byte[] binary)
		{
			var parsed = Parse(binary);
			Cells  = parsed.Cells;
			Width  = parsed.Width;
			Height = parsed.Height;
		}

		public byte[] GetBinary()
		{
			var working = new List<byte> {(byte) Width, (byte) Height};
			foreach (var cell in Cells) working.AddRange(cell.GetBinary());
			return working.ToArray();
		}

		public static ColourImage Parse(byte[] binary)
		{
			var working = new List<ColourCell>();
			for (var i = 4;
			     i < binary.Length;
			     i += 4) // Parse cells. Start at 4 to skip the header. Step in 4s as each cell takes 4 bytes
				working.Add(ColourCell.Parse(new[] {binary[i], binary[i + 1], binary[i + 2], binary[i + 3]}));

			return new ColourImage(working.ToArray(),
			                       ToUInt16(binary[0], binary[1]), ToUInt16(binary[2], binary[3])); // Parse Header
		}
	}
}