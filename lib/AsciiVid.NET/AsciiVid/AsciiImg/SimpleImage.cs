using System.Collections.Generic;
using AsciiVid.Cells;
using static AsciiVid.Utilities;

namespace AsciiVid.AsciiImg
{
	public class SimpleImage : IImageBase
	{
		public SimpleCell[] Cells;

		public ushort Width;
		public ushort Height;

		public byte[] GetBinary()
		{
			var working = new List<byte> {(byte) Width, (byte) Height};
			for (var i = 0; i < Cells.Length; i += 2)
				working.Add(NibblePair.Combine(Cells[i].Brightness, Cells[i + 1].Brightness).RawBinary);
			return working.ToArray();
		}

		public static SimpleImage Parse(byte[] binary)
		{
			var working = new List<SimpleCell>();
			for (var i = 4; i < binary.Length; i++) // Parse cells. Start at 4 to skip the header.
			{
				var b = binary[i];
				working.Add(SimpleCell.ParseSingle(b.GetLowNibble()));
				working.Add(SimpleCell.ParseSingle(b.GetHighNibble()));
			}

			return new SimpleImage
			{
				Cells  = working.ToArray(),
				Width  = ToUInt16(binary[0], binary[1]), // Parse Header
				Height = ToUInt16(binary[2], binary[3])  // Parse Header
			};
		}
	}
}