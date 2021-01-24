﻿using System;
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
	}
}