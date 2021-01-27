﻿using System;
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
		public void AsciiImageRenderTest()
		{
			var image = new AsciiImage(new[]
			{
				c(' '), c('x'), c(' '), c('#'), c(' '), c('O'), c(' '), c('&'), c(' '), c('G'), c(' '), c('%'), c(' '),
				c('@'), c(' '), c('='), c(' '), c('$'), c(' '), c('9')
			}, 5, 5);

			var renderer = new ImageRenderer(image);
			var actual   = renderer.RenderAsciiImage();

			var newline = Environment.NewLine;

			var expected = $" x # {newline}O & G{newline} % @ {newline}= $ 9";

			Assert.AreEqual(expected, actual);

			static Cell c(char value) => new Cell(value); // thank god for space saving
		}

		[Test]
		public void SimpleImageRenderTest()
		{
			var image = new SimpleImage(new[]
			{
				c(00), c(15), c(00), c(15), c(00),
				c(11), c(00), c(11), c(00), c(11),
				c(00), c(07), c(00), c(07), c(00),
				c(03), c(00), c(03), c(00), c(03)
			}, 5, 5);

			var renderer = new ImageRenderer(image);
			var actual   = renderer.RenderSimpleImage();

			var newline = Environment.NewLine;

			var expected = $" █ █ {newline}▓ ▓ ▓{newline} ▒ ▒ {newline}░ ░ ░";

			Assert.AreEqual(expected, actual);

			static SimpleCell c(byte value) => new SimpleCell(new Nibble(value)); // thank god for space saving
		}

		[Test]
		public void FullColourRenderTest()
		{
			var image = new ColourImage(new[]
			{
				c(' ', 0, 0, 0), c('x', 255, 0, 0), c(' ', 0, 0, 0), c('#', 255, 0, 0), c(' ', 0, 0, 0),
				c('O', 0, 255, 0), c(' ', 0, 0, 0), c('&', 0, 255, 0), c(' ', 0, 0, 0), c('G', 0, 255, 0),
				c(' ', 0, 0, 0), c('%', 0, 0, 255), c(' ', 0, 0, 0), c('@', 0, 0, 255), c(' ', 0, 0, 0),
				c('=', 255, 255, 255), c(' ', 0, 0, 0), c('$', 255, 255, 255), c(' ', 0, 0, 0), c('9', 255, 255, 255)
			}, 5, 5);

			var renderer = new ImageRenderer(image);
			var actual   = renderer.RenderFullColourImage();

			var actualText    = new string(actual.Select(c => c.Char).ToArray());
			var actualColours = actual.Select(c => c.Colour).ToArray();

			var newline = Environment.NewLine;

			var expectedText = $" x # {newline}O & G{newline} % @ {newline}= $ 9";
			var expectedColours = Environment.NewLine.Length switch
			{
				2 => new[]
				{
					colour(0, 0, 0), colour(255, 0, 0), colour(0, 0, 0), colour(255, 0, 0), colour(0, 0, 0),
					Color.Black, Color.Black,
					colour(0, 255, 0), colour(0, 0, 0), colour(0, 255, 0), colour(0, 0, 0), colour(0, 255, 0),
					Color.Black, Color.Black,
					colour(0, 0, 0), colour(0, 0, 255), colour(0, 0, 0), colour(0, 0, 255), colour(0, 0, 0),
					Color.Black, Color.Black,
					colour(255, 255, 255), colour(0, 0, 0), colour(255, 255, 255), colour(0, 0, 0),
					colour(255, 255, 255)
				},
				1 => new[]
				{
					colour(0, 0, 0), colour(255, 0, 0), colour(0, 0, 0), colour(255, 0, 0), colour(0, 0, 0),
					Color.Black,
					colour(0, 255, 0), colour(0, 0, 0), colour(0, 255, 0), colour(0, 0, 0), colour(0, 255, 0),
					Color.Black,
					colour(0, 0, 0), colour(0, 0, 255), colour(0, 0, 0), colour(0, 0, 255), colour(0, 0, 0),
					Color.Black,
					colour(255, 255, 255), colour(0, 0, 0), colour(255, 255, 255), colour(0, 0, 0),
					colour(255, 255, 255)
				},
				_ => new Color[0]
			};

			if (!expectedColours.Any()) throw new Exception("Newline string is not 1 or 2 characters for some reason");

			Assert.AreEqual(expectedText, actualText);
			Assert.AreEqual(expectedColours, actualColours);

			static ColourCell c(char value, byte r, byte g, byte b)
				=> new ColourCell(value, r, g, b); // thank god for space saving

			static Color colour(byte r, byte g, byte b) => Color.FromArgb(r, g, b);
		}

		[Test]
		public void ConsoleColourRenderTest()
		{
			var image = new ColourImage(new[]
			{
				c(' ', 0, 0, 0), c('x', 255, 0, 0), c(' ', 0, 0, 0), c('#', 255, 0, 0), c(' ', 0, 0, 0),
				c('O', 0, 255, 0), c(' ', 0, 0, 0), c('&', 0, 255, 0), c(' ', 0, 0, 0), c('G', 0, 255, 0),
				c(' ', 0, 0, 0), c('%', 0, 0, 255), c(' ', 0, 0, 0), c('@', 0, 0, 255), c(' ', 0, 0, 0),
				c('=', 255, 255, 255), c(' ', 0, 0, 0), c('$', 255, 255, 255), c(' ', 0, 0, 0), c('9', 255, 255, 255)
			}, 5, 5);

			var renderer = new ImageRenderer(image);
			var actual   = renderer.RenderConsoleColourImage();

			var actualText    = new string(actual.Select(c => c.Char).ToArray());
			var actualColours = actual.Select(c => c.Colour).ToArray();

			var newline = Environment.NewLine;

			var expectedText = $" x # {newline}O & G{newline} % @ {newline}= $ 9";
			var expectedColours = Environment.NewLine.Length switch
			{
				2 => new[]
				{
					ConsoleColor.Black, ConsoleColor.Red, ConsoleColor.Black, ConsoleColor.Red, ConsoleColor.Black,
					ConsoleColor.Black, ConsoleColor.Black, ConsoleColor.Green, ConsoleColor.Black,
					ConsoleColor.Green, ConsoleColor.Black, ConsoleColor.Green, ConsoleColor.Black,
					ConsoleColor.Black, ConsoleColor.Black, ConsoleColor.Blue, ConsoleColor.Black,
					ConsoleColor.Blue, ConsoleColor.Black, ConsoleColor.Black, ConsoleColor.Black,
					ConsoleColor.White, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Black,
					ConsoleColor.White
				},
				1 => new[]
				{
					ConsoleColor.Black, ConsoleColor.Red, ConsoleColor.Black, ConsoleColor.Red, ConsoleColor.Black,
					ConsoleColor.Black, ConsoleColor.Green, ConsoleColor.Black, ConsoleColor.Green,
					ConsoleColor.Black, ConsoleColor.Green, ConsoleColor.Black, ConsoleColor.Black,
					ConsoleColor.Blue, ConsoleColor.Black, ConsoleColor.Blue, ConsoleColor.Black,
					ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.White,
					ConsoleColor.Black, ConsoleColor.White
				},
				_ => new ConsoleColor[0]
			};

			if (!expectedColours.Any()) throw new Exception("Newline string is not 1 or 2 characters for some reason");

			Assert.AreEqual(expectedText, actualText);
			Assert.AreEqual(expectedColours, actualColours);

			static ColourCell c(char value, byte r, byte g, byte b)
				=> new ColourCell(value, r, g, b); // thank god for space saving
		}
	}
}