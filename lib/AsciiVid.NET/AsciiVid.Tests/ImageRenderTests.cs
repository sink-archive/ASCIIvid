using System;
using System.Drawing;
using System.Linq;
using AsciiVid.AsciiImg;
using AsciiVid.Cells;
using AsciiVid.Render;
using NUnit.Framework;

namespace AsciiVid.Tests
{
	public class ImageRenderTests
	{
		[Test]
		public void AsciiImageRender()
		{
			var image = new AsciiImage(new[]
			{
				new Cell(' '), new Cell('x'), new Cell(' '), new Cell('#'), new Cell(' '), new Cell('O'), new Cell(' '),
				new Cell('&'), new Cell(' '), new Cell('G'), new Cell(' '), new Cell('%'), new Cell(' '), new Cell('@'),
				new Cell(' '), new Cell('='), new Cell(' '), new Cell('$'), new Cell(' '), new Cell('9')
			}, 5, 5);

			var renderer = new ImageRenderer(image);
			var actual   = renderer.RenderAsciiImage();

			var newline = Environment.NewLine;

			var expected = $" x # {newline}O & G{newline} % @ {newline}= $ 9";

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void SimpleImageRender()
		{
			var image = new SimpleImage(new[]
			{
				new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(15)), new SimpleCell(new Nibble(0)),
				new SimpleCell(new Nibble(15)), new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(11)),
				new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(11)), new SimpleCell(new Nibble(0)),
				new SimpleCell(new Nibble(11)), new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(7)),
				new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(7)), new SimpleCell(new Nibble(0)),
				new SimpleCell(new Nibble(3)), new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(3)),
				new SimpleCell(new Nibble(0)), new SimpleCell(new Nibble(3))
			}, 5, 5);

			var renderer = new ImageRenderer(image);
			var actual   = renderer.RenderSimpleImage();

			var newline = Environment.NewLine;

			var expected = $" █ █ {newline}▓ ▓ ▓{newline} ▒ ▒ {newline}░ ░ ░";

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void FullColourRender()
		{
			var image = new ColourImage(new[]
			{
				new ColourCell(' ', 0, 0, 0), new ColourCell('x', 255, 0, 0), new ColourCell(' ', 0, 0, 0),
				new ColourCell('#', 255, 0, 0), new ColourCell(' ', 0, 0, 0), new ColourCell('O', 0, 255, 0),
				new ColourCell(' ', 0, 0, 0), new ColourCell('&', 0, 255, 0), new ColourCell(' ', 0, 0, 0),
				new ColourCell('G', 0, 255, 0), new ColourCell(' ', 0, 0, 0), new ColourCell('%', 0, 0, 255),
				new ColourCell(' ', 0, 0, 0), new ColourCell('@', 0, 0, 255), new ColourCell(' ', 0, 0, 0),
				new ColourCell('=', 255, 255, 255), new ColourCell(' ', 0, 0, 0), new ColourCell('$', 255, 255, 255),
				new ColourCell(' ', 0, 0, 0), new ColourCell('9', 255, 255, 255)
			}, 5, 5);

			var renderer = new ImageRenderer(image);
			var actual   = renderer.RenderFullColourImage();

			var actualText    = new string(actual.Select(c => c.Char).ToArray());
			var actualColours = actual.Select(c => c.Colour).ToArray();

			var newline = Environment.NewLine;

			var expectedText = $" x # {newline}O & G{newline} % @ {newline}= $ 9";
			var expectedColours = new[]
			{
				Color.FromArgb(0, 0, 0), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 0), Color.FromArgb(255, 0, 0),
				Color.FromArgb(0, 0, 0), Color.Black, Color.Black, Color.FromArgb(0, 255, 0), Color.FromArgb(0, 0, 0),
				Color.FromArgb(0, 255, 0), Color.FromArgb(0, 0, 0), Color.FromArgb(0, 255, 0), Color.Black, Color.Black,
				Color.FromArgb(0, 0, 0), Color.FromArgb(0, 0, 255), Color.FromArgb(0, 0, 0), Color.FromArgb(0, 0, 255),
				Color.FromArgb(0, 0, 0), Color.Black, Color.Black, Color.FromArgb(255, 255, 255),
				Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(0, 0, 0),
				Color.FromArgb(255, 255, 255)
			};

			Assert.AreEqual(expectedText, actualText);
			Assert.AreEqual(expectedColours, actualColours);
		}
	}
}