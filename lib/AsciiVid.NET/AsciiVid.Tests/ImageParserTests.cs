using System.Linq;
using AsciiVid.AsciiImg;
using AsciiVid.Cells;
using NUnit.Framework;

namespace AsciiVid.Tests
{
	public class ImageParserTests
	{
		[Test]
		public void AsciiImageTest()
		{
			var input = new byte[]
			{
				0x05, 0x00, 0x05, 0x00, 0x20, 0x78, 0x20, 0x23, 0x20, 0x4F, 0x20, 0x26, 0x20, 0x47, 0x20, 0x25, 0x20,
				0x40, 0x20, 0x3D, 0x20, 0x24, 0x20, 0x39
			};

			var parsed = AsciiImage.Parse(input);

			Assert.AreEqual(5, parsed.Height);
			Assert.AreEqual(5, parsed.Width);
			Assert.AreEqual(new[]
			{
				new Cell(' '), new Cell('x'), new Cell(' '), new Cell('#'), new Cell(' '), new Cell('O'), new Cell(' '),
				new Cell('&'), new Cell(' '), new Cell('G'), new Cell(' '), new Cell('%'), new Cell(' '), new Cell('@'),
				new Cell(' '), new Cell('='), new Cell(' '), new Cell('$'), new Cell(' '), new Cell('9')
			}.Select(c => c.Character), parsed.Cells.Select(c => c.Character));
		}

		[Test]
		public void SimpleImageTest()
		{
			var input = new byte[] {0x05, 0x00, 0x05, 0x00, 0x0F, 0x0F, 0x0D, 0x0D, 0x0D, 0x07, 0x07, 0x03, 0x03, 0x03};

			var parsed = SimpleImage.Parse(input);

			Assert.AreEqual(5, parsed.Height);
			Assert.AreEqual(5, parsed.Width);
			Assert.AreEqual(new[]
			{
				new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(15)), new SimpleCell(new Nibble(0)),
				new SimpleCell(new Nibble(15)), new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(11)),
				new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(11)), new SimpleCell(new Nibble(0)),
				new SimpleCell(new Nibble(11)), new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(7)),
				new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(7)), new SimpleCell(new Nibble(0)),
				new SimpleCell(new Nibble(3)), new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(3)),
				new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(3))
			}.Select(c => c.Brightness.Value), parsed.Cells.Select(c => c.Brightness.Value));
		}

		[Test]
		public void ColourImageTest()
		{
			var input = new byte[]
			{
				0x05, 0x00, 0x05, 0x00, 0x20, 0x00, 0x00, 0x00, 0x78, 0xFF, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x23,
				0xFF, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x4F, 0x00, 0xFF, 0x00, 0x20, 0x00, 0x00, 0x00, 0x26, 0x00,
				0xFF, 0x00, 0x20, 0x00, 0x00, 0x00, 0x47, 0x00, 0xFF, 0x00, 0x20, 0x00, 0x00, 0x00, 0x25, 0x00, 0x00,
				0xFF, 0x20, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0xFF, 0x20, 0x00, 0x00, 0x00, 0x3D, 0xFF, 0xFF, 0xFF,
				0x20, 0x00, 0x00, 0x00, 0x24, 0xFF, 0xFF, 0xFF, 0x20, 0x00, 0x00, 0x00, 0x39, 0xFF, 0xFF, 0xFF
			};

			var parsed = ColourImage.Parse(input);

			Assert.AreEqual(5, parsed.Height);
			Assert.AreEqual(5, parsed.Width);
			var expectedCells = new[]
			{
				new ColourCell(' ', 0, 0, 0), new ColourCell('x', 255, 0, 0), new ColourCell(' ', 0, 0, 0),
				new ColourCell('#', 255, 0, 0), new ColourCell(' ', 0, 0, 0), new ColourCell('O', 0, 255, 0),
				new ColourCell(' ', 0, 0, 0), new ColourCell('&', 0, 255, 0), new ColourCell(' ', 0, 0, 0),
				new ColourCell('G', 0, 255, 0), new ColourCell(' ', 0, 0, 0), new ColourCell('%', 0, 0, 255),
				new ColourCell(' ', 0, 0, 0), new ColourCell('@', 0, 0, 255), new ColourCell(' ', 0, 0, 0),
				new ColourCell('=', 255, 255, 255), new ColourCell(' ', 0, 0, 0), new ColourCell('$', 255, 255, 255),
				new ColourCell(' ', 0, 0, 0), new ColourCell('9', 255, 255, 255)
			};
			Assert.AreEqual(expectedCells.Select(c => c.Character), parsed.Cells.Select(c => c.Character));
			Assert.AreEqual(expectedCells.Select(c => c.RedChannel), parsed.Cells.Select(c => c.RedChannel));
			Assert.AreEqual(expectedCells.Select(c => c.GreenChannel), parsed.Cells.Select(c => c.GreenChannel));
			Assert.AreEqual(expectedCells.Select(c => c.BlueChannel), parsed.Cells.Select(c => c.BlueChannel));
		}
	}
}