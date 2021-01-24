using System.IO;
using AsciiVid.AsciiImg;

namespace AsciiVid
{
	public static class FileWriter
	{
		public static void WriteImage(string fileName, IImageBase image)
			=> new BinaryWriter(
					new FileInfo(fileName).OpenWrite())
			   .Write(image.GetBinary());
	}
}