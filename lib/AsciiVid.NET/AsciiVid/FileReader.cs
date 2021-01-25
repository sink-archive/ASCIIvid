using System;
using System.IO;
using AsciiVid.AsciiImg;
using AsciiVid.AsciiVid;

namespace AsciiVid
{
	public static class FileReader
	{
		#region Images

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

		#endregion Images

		#region Videos

		public static AsciiVideo ReadAsciiVideo(string fileName)
		{
			var reader = new BinaryReader(new FileInfo(fileName).OpenRead());
			var bytes  = reader.ReadBytes((int) reader.BaseStream.Length);
			return AsciiVideo.Parse(bytes);
		}

		public static ColourVideo ReadColourVideo(string fileName)
		{
			var reader = new BinaryReader(new FileInfo(fileName).OpenRead());
			var bytes  = reader.ReadBytes((int) reader.BaseStream.Length);
			return ColourVideo.Parse(bytes);
		}

		public static SimpleVideo ReadSimpleVideo(string fileName)
		{
			var reader = new BinaryReader(new FileInfo(fileName).OpenRead());
			var bytes  = reader.ReadBytes((int) reader.BaseStream.Length);
			return SimpleVideo.Parse(bytes);
		}

		public static IVideoBase ReadVideo(string fileName, out Type VideoType)
		{
			switch (new FileInfo(fileName).Extension)
			{
				case "acv":
					VideoType = typeof(AsciiVideo);
					return ReadAsciiVideo(fileName);
				case "sacv":
					VideoType = typeof(SimpleVideo);
					return ReadSimpleVideo(fileName);
				case "cacv":
					VideoType = typeof(ColourVideo);
					return ReadColourVideo(fileName);
				default:
					throw new ArgumentException("That file is not an ASCIIvid file");
			}
		}

		#endregion
	}
}