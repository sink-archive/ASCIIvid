using System;
using System.Collections.Generic;
using AsciiVid.Cells;

namespace AsciiVid.AsciiImg
{
	public class SimpleImage : IImageBase
	{
		public SimpleCell[] Cells;

		public byte[] GetBinary()
		{
			var working = new List<byte>();
			for (var i = 0; i < Cells.Length; i += 2)
				working.Add(NibblePair.Combine(Cells[i].Brightness, Cells[i + 1].Brightness).RawBinary);
			return working.ToArray();
		}

		public AsciiImage Parse(byte[] binary) => throw new NotImplementedException();
	}
}