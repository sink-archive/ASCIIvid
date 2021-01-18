using System.Collections.Generic;
using System.Linq;
using AsciiVid.Cells;
using static AsciiVid.Utilities;

namespace AsciiVid.AsciiImg
{
	public class AsciiImage : IImageBase
	{
		public Cell[] Cells;

		public ushort Width;
		public ushort Height;

		public byte[] GetBinary() => Cells
		                            .Select(cell => cell.GetBinary())
		                            .Aggregate(new[] {(byte) Width, (byte) Height}, (current, b) => current
			                            .Append(b)
			                            .ToArray());

		public static AsciiImage Parse(byte[] binary)
		{
			var working = new List<Cell>();
			for (var i = 4; i < binary.Length; i++) // Parse cells. Start at 4 to skip the header.
			{
				var b = binary[i];
				working.Add(Cell.Parse(b));
			}

			return new AsciiImage
			{
				Cells  = working.ToArray(),
				Width  = ToUInt16(binary[0], binary[1]), // Parse Header
				Height = ToUInt16(binary[2], binary[3])  // Parse Header
			};
		}
	}
}