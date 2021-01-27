using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using AsciiVid.AsciiImg;
using AsciiVid.Cells;
using static AsciiVid.Render.ConsoleColorParser;

namespace AsciiVid.Render
{
	public class ImageRenderer
	{
		public CharacterSet CharSet = CharacterSet.DefaultSet;
		public IImageBase   Image;

		public ImageRenderer(IImageBase image) => Image = image;

		public string RenderAsciiImage()
		{
			var working = new List<char>();
			var img     = (AsciiImage) Image;
			for (var i = 0; i < img.Cells.Length; i++)
			{
				working.Add(img.Cells[i].Character);
				if ((i + 1) % img.Width == 0) working.AddRange(Environment.NewLine);
			}

			RemoveTrailingNewline(ref working);

			return new string(working.ToArray());
		}

		public string RenderSimpleImage()
		{
			var working = new List<char>();
			var img     = (SimpleImage) Image;
			for (var i = 0; i < img.Cells.Length; i++)
			{
				working.Add(CharSet.BrightnessChars[img.Cells[i].Brightness.Value]);
				if ((i + 1) % img.Width == 0) working.AddRange(Environment.NewLine);
			}

			RemoveTrailingNewline(ref working);

			return new string(working.ToArray());
		}

		public ColouredChar[] RenderFullColourImage()
		{
			var working = new List<ColouredChar>();
			var img     = (ColourImage) Image;
			for (var i = 0; i < img.Cells.Length; i++)
			{
				var c = img.Cells[i];
				working.Add(new ColouredChar(c.Character,
				                             Color.FromArgb(
					                             c.RedChannel,
					                             c.GreenChannel,
					                             c.BlueChannel)));
				if ((i + 1) % img.Width == 0)
					working
					   .AddRange(Environment.NewLine
					                        .Select(n => new ColouredChar(n, Color.Black)));
			}

			RemoveTrailingNewline(ref working);

			return working.ToArray();
		}

		public ConsoleColouredChar[] RenderConsoleColourImage()
		{
			var working = new List<ConsoleColouredChar>();
			var img     = (ColourImage) Image;
			for (var i = 0; i < img.Cells.Length; i++)
			{
				var c = img.Cells[i];
				working.Add(new ConsoleColouredChar(c.Character,
				                                    ClosestConsoleColor(
					                                    c.RedChannel,
					                                    c.GreenChannel,
					                                    c.BlueChannel)));
				if ((i + 1) % img.Width == 0)
					working
					   .AddRange(Environment.NewLine
					                        .Select(n => new ConsoleColouredChar(n, ConsoleColor.Black)));
			}

			RemoveTrailingNewline(ref working);

			return working.ToArray();
		}

		private static void RemoveTrailingNewline(ref List<char> working)
		{
			if (new string(working.ToArray()).EndsWith(Environment.NewLine))
				working.RemoveRange(working.Count - Environment.NewLine.Length,
				                    Environment.NewLine.Length); // trailing newline
		}

		private static void RemoveTrailingNewline(ref List<ColouredChar> working)
		{
			if (new string(working.Select(c => c.Char).ToArray()).EndsWith(Environment.NewLine))
				working.RemoveRange(working.Count - Environment.NewLine.Length,
				                    Environment.NewLine.Length); // trailing newline
		}

		private static void RemoveTrailingNewline(ref List<ConsoleColouredChar> working)
		{
			if (new string(working.Select(c => c.Char).ToArray()).EndsWith(Environment.NewLine))
				working.RemoveRange(working.Count - Environment.NewLine.Length,
				                    Environment.NewLine.Length); // trailing newline
		}
	}

	[DebuggerDisplay("{Char}, {Colour}")]
	public class ColouredChar
	{
		public char  Char;
		public Color Colour;

		public ColouredChar(char c, Color colour)
		{
			Char   = c;
			Colour = colour;
		}
	}

	[DebuggerDisplay("{Char}, {Colour}")]
	public class ConsoleColouredChar
	{
		public char         Char;
		public ConsoleColor Colour;

		public ConsoleColouredChar(char c, ConsoleColor colour)
		{
			Char   = c;
			Colour = colour;
		}
	}
}