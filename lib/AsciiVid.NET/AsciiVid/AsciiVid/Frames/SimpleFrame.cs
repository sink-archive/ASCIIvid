using System.Collections.Generic;
using System.Linq;
using AsciiVid.Cells;

namespace AsciiVid.AsciiVid.Frames
{
	public class SimpleFrame
	{
		public SimpleCell[] Cells;

		public SimpleFrame(SimpleCell[] cells)
		{
			Cells = cells;
		}

		public SimpleFrame(byte[] binary)
		{
			var parsed = Parse(binary);
			Cells = parsed.Cells;
		}

		public byte[] GetBinary() => Cells.Select(cell => cell.GetBinary()).ToArray();

		public static SimpleFrame Parse(byte[] binary)
		{
			var working = new List<SimpleCell>();
			foreach (var b in binary)
			{
				working.Add(SimpleCell.ParseSingle(b.GetLowNibble()));
				working.Add(SimpleCell.ParseSingle(b.GetHighNibble()));
			}

			return new SimpleFrame
			{
				Cells = working.ToArray()
			};
		}
	}
}