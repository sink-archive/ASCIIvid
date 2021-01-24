using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using AsciiVid;
using AsciiVid.AsciiImg;
using AsciiVid.Cells;
using ImageMagick;

namespace Asciivid.Convert2Ascii
{
	public class Img2AsciiImg
	{
		public IImageBase Image;
		public string     ImagePath;
		public Type       ImageType;

		public void RenderAsciiImage(CharacterSet charSet = null)
		{
			charSet ??= CharacterSet.DefaultSet;

			if (string.IsNullOrWhiteSpace(ImagePath) || !File.Exists(ImagePath))
				throw new InvalidOperationException("That image does not exist");
			var processor   = new ImageProcessor {ImagePath = ImagePath};
			var magickImage = new MagickImage(ImagePath);

			var colours  = GetColoursOfImage(processor);
			var rendered = RenderColoursToChars(colours, charSet);
			var image = new AsciiImage(rendered.Select(c => new Cell(c)).ToArray(),
			                           (ushort) magickImage.Width,
			                           (ushort) magickImage.Height);
			Image = image;
		}

		public void RenderColourImage(CharacterSet charSet = null)
		{
			charSet ??= CharacterSet.DefaultSet;

			if (string.IsNullOrWhiteSpace(ImagePath) || !File.Exists(ImagePath))
				throw new InvalidOperationException("That image does not exist");
			var processor   = new ImageProcessor {ImagePath = ImagePath};
			var magickImage = new MagickImage(ImagePath);

			var colours  = GetColoursOfImage(processor);
			var rendered = RenderColoursToColouredChars(colours, charSet);
			var image = new ColourImage((from c in rendered
			                             let colour = c.Colour
			                             select new ColourCell(c.Char, colour.R, colour.G, colour.B))
			                           .ToArray(),
			                            (ushort) magickImage.Width,
			                            (ushort) magickImage.Height);
			Image = image;
		}

		public void RenderSimpleImage(CharacterSet charSet = null)
		{
			charSet ??= CharacterSet.DefaultSet;

			if (string.IsNullOrWhiteSpace(ImagePath) || !File.Exists(ImagePath))
				throw new InvalidOperationException("That image does not exist");
			var processor   = new ImageProcessor {ImagePath = ImagePath};
			var magickImage = new MagickImage(ImagePath);

			var colours     = GetColoursOfImage(processor);
			var intensities = RenderColoursToIntensities(colours, charSet.BrightnessChars.Length);
			var image = new SimpleImage(intensities.Select(i => new SimpleCell(new Nibble((byte) i))).ToArray(),
			                            (ushort) magickImage.Width,
			                            (ushort) magickImage.Height);
			Image = image;
		}

		private Color[] GetColoursOfImage(ImageProcessor processor)
		{
			var img     = new MagickImage(ImagePath);
			var working = new List<Color>();
			for (var y = 0; y < img.BaseHeight; y++)
			for (var x = 0; x < img.BaseWidth; x++)
				working.Add(processor.GetColourFromPixelCoordinate(x, y));
			return working.ToArray();
		}

		private char RenderColourToChar(Color colour, CharacterSet characterSet = null)
		{
			characterSet ??= CharacterSet.DefaultSet;
			var intensity = GetIntensity(colour, characterSet.BrightnessChars.Length);
			return characterSet.BrightnessChars[intensity];
		}

		private ushort GetIntensity(Color colour, int charSetLength)
		{
			var intensity = (colour.R + colour.G + colour.B) / 3;

			var intensityScaleFactor = 255 / charSetLength;
			// ReSharper disable once PossibleLossOfFraction
			var scaledIntensity       = (int) Math.Round((double) (intensity / intensityScaleFactor));
			var offsetScaledIntensity = scaledIntensity - 1;
			var limitedIntensity      = Math.Min(Math.Max(offsetScaledIntensity, 0), charSetLength - 1);
			return (ushort) limitedIntensity;
		}

		private char[] RenderColoursToChars(IEnumerable<Color> colours, CharacterSet characterSet = null)
			=> colours.Select(c => RenderColourToChar(c, characterSet)).ToArray();

		private ColouredChar[] RenderColoursToColouredChars(IEnumerable<Color> colours,
		                                                    CharacterSet       characterSet = null)
			=> colours.Select(c => new ColouredChar(RenderColourToChar(c, characterSet), c)).ToArray();

		private ushort[] RenderColoursToIntensities(IEnumerable<Color> colours, int charSetLength)
			=> colours.Select(c => GetIntensity(c, charSetLength)).ToArray();
	}

	public class Coordinate
	{
		public int X;
		public int Y;

		public Coordinate(int x, int y)
		{
			X = x;
			Y = y;
		}
	}

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
}