using System;
using System.Collections.Generic;
using AsciiVid.Cells;

namespace AsciiVid.AsciiImg
{
	public class ColourImage : IImageBase
	{
		public ColourCell[] Cells;

		public byte[] GetBinary()
		{
			var working = new List<byte>();
			foreach (var cell in Cells) working.AddRange(cell.GetBinary());
			return working.ToArray();
		}

		public AsciiImage Parse(byte[] binary) => throw new NotImplementedException();
	}
}