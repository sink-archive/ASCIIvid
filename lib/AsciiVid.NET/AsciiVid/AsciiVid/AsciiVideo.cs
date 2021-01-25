using System.Collections.Generic;
using AsciiVid.AsciiVid.Frames;
using static AsciiVid.Utilities;

namespace AsciiVid.AsciiVid
{
	public class AsciiVideo : IVideoBase
	{
		public byte         Framerate;
		public AsciiFrame[] Frames;
		public ushort       Height;
		public ushort       Width;

		public AsciiVideo(AsciiFrame[] frames, ushort width, ushort height, byte framerate)
		{
			Frames    = frames;
			Width     = width;
			Height    = height;
			Framerate = framerate;
		}

		public AsciiVideo(byte[] binary)
		{
			var parsed = Parse(binary);
			Frames    = parsed.Frames;
			Width     = parsed.Width;
			Height    = parsed.Height;
			Framerate = parsed.Framerate;
		}

		public static AsciiVideo Parse(byte[] binary)
		{
			var width     = ToUInt16(binary[0], binary[1]);
			var height    = ToUInt16(binary[2], binary[3]);
			var framerate = binary[4];

			var bytesPerFrame = width * height;

			var working = new List<AsciiFrame>();

			for (var i = 5; i < binary.Length; i++)
			for (var j = 0; j < bytesPerFrame; j++)
			{
				var bytes = new List<byte>();
				for (var k = 0; k < bytesPerFrame; k++) bytes.Add(binary[j + k]);
				working.Add(AsciiFrame.Parse(bytes.ToArray()));
			}

			return new AsciiVideo(working.ToArray(),
			                      width, height, framerate);
		}

		public byte[] GetRawBinary()
		{
			var working = new List<byte> {(byte) Width, (byte) Height, Framerate};
			foreach (var frame in Frames) working.AddRange(frame.GetBinary());
			return working.ToArray();
		}
	}
}