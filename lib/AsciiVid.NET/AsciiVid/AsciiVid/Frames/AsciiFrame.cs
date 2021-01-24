using System.Linq;
using AsciiVid.Cells;

namespace AsciiVid.AsciiVid.Frames
{
	public class AsciiFrame
	{
		public Cell[] Cells;

		public AsciiFrame(Cell[] cells)
		{
			Cells = cells;
		}

		public AsciiFrame(byte[] binary)
		{
			var parsed = Parse(binary);
			Cells = parsed.Cells;
		}

		public byte[] GetBinary() => Cells.Select(cell => cell.GetBinary()).ToArray();

		public static AsciiFrame Parse(byte[] binary) => new AsciiFrame(binary.Select(Cell.Parse).ToArray());
	}
}