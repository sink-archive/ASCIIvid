using System.Linq;
using AsciiVid.Cells;

namespace AsciiVid.AsciiVid.Frames
{
	public class AsciiFrame
	{
		public Cell[] Cells;

		public byte[] GetBinary() => Cells.Select(cell => cell.GetBinary()).ToArray();

		public static AsciiFrame Parse(byte[] binary) =>
			new AsciiFrame
			{
				Cells = binary.Select(Cell.Parse).ToArray()
			};
	}
}