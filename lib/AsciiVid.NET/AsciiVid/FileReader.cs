using System;
using System.IO;
using AsciiVid.AsciiImg;

namespace AsciiVid
{
	public static class FileReader
	{
		public static AsciiImage ReadAsciiImage(string fileName)
		{
			var reader = new BinaryReader(new FileInfo(fileName).OpenRead());
			var bytes  = reader.ReadBytes((int) reader.BaseStream.Length);
			return AsciiImage.Parse(bytes);
		}

		public static ColourImage ReadColourImage(string fileName)
		{
			var reader = new BinaryReader(new FileInfo(fileName).OpenRead());
			var bytes  = reader.ReadBytes((int) reader.BaseStream.Length);
			return ColourImage.Parse(bytes);
		}

		public static SimpleImage ReadSimpleImage(string fileName)
		{
			var reader = new BinaryReader(new FileInfo(fileName).OpenRead());
			var bytes  = reader.ReadBytes((int) reader.BaseStream.Length);
			return SimpleImage.Parse(bytes);
		}

		public static IImageBase ReadImage(string fileName, out Type imageType)
		{
			switch (new FileInfo(fileName).Extension)
			{
				case "aii":
					imageType = typeof(AsciiImage);
					return ReadAsciiImage(fileName);
				case "saii":
					imageType = typeof(SimpleImage);
					return ReadSimpleImage(fileName);
				case "caii":
					imageType = typeof(ColourImage);
					return ReadColourImage(fileName);
				default:
					throw new ArgumentException("That file is not an ASCIIimg file");
			}
		}
	}
}