using System;
using System.Linq;
using AsciiVid.Cells;

namespace AsciiVid.AsciiImg
{
	public class AsciiImage : IImageBase
	{
		public Cell[] Cells;

		public byte[] GetBinary() => Cells.Select(cell => cell.GetBinary()).ToArray();

		public AsciiImage Parse(byte[] binary) => throw new NotImplementedException();
	}
}