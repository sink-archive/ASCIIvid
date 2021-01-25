﻿using System;
using AsciiVid.AsciiImg;
using AsciiVid.AsciiVid;
using AsciiVid.Cells;
using Asciivid.Convert2Ascii;
using static AsciiVid.FileReader;

namespace AsciiVid.Fluent
{
	public static class AsciiMediaItem
	{
		public static ImageMediaItem Image(IImageBase image) => new ImageMediaItem(image);

		public static VideoMediaItem Video(IVideoBase video) => new VideoMediaItem(video);

		public static TextMediaItem Text(string text) => new TextMediaItem(text);

		public static ImageMediaItem ImageFromDisk(string filePath) => new ImageMediaItem(filePath);

		public static VideoMediaItem VideoFromDisk(string filePath) => new VideoMediaItem(filePath);

		public static ImageMediaItem SimpleImageFromBitmapOnDisk(string filePath, CharacterSet charSet = null)
		{
			var converter = new Img2AsciiImg {CharSet = charSet ?? CharacterSet.DefaultSet, ImagePath = filePath};
			converter.RenderSimpleImage();
			return Image(converter.Image);
		}
	}

	public class VideoMediaItem
	{
		public IVideoBase Video { get; }
		public VideoTypes Type  { get; }

		internal VideoMediaItem(IVideoBase video)
		{
			Video = video;
			var type = video.GetType();
			if (type == typeof(AsciiVideo))
				Type = VideoTypes.Ascii;
			else if (type == typeof(SimpleVideo))
				Type = VideoTypes.Simple;
			else if (type == typeof(ColourVideo))
				Type = VideoTypes.Colour;
		}

		private VideoMediaItem(IVideoBase video, Type type)
		{
			Video = video;
			if (type == typeof(AsciiVideo))
				Type = VideoTypes.Ascii;
			else if (type == typeof(SimpleVideo))
				Type = VideoTypes.Simple;
			else if (type == typeof(ColourVideo))
				Type = VideoTypes.Colour;
		}

		internal VideoMediaItem(string filePath) : this(ReadVideo(filePath, out var type), type)
		{
		}
	}

	public class ImageMediaItem
	{
		public IImageBase Image { get; set; }
		public ImageTypes Type  { get; }

		internal ImageMediaItem(IImageBase image)
		{
			Image = image;
			var type = Image.GetType();
			if (type == typeof(AsciiImage))
				Type = ImageTypes.Ascii;
			else if (type == typeof(SimpleImage))
				Type = ImageTypes.Simple;
			else if (type == typeof(ColourImage))
				Type = ImageTypes.Colour;
		}

		private ImageMediaItem(IImageBase image, Type type)
		{
			Image = image;
			if (type == typeof(AsciiImage))
				Type = ImageTypes.Ascii;
			else if (type == typeof(SimpleImage))
				Type = ImageTypes.Simple;
			else if (type == typeof(ColourImage))
				Type = ImageTypes.Colour;
		}

		internal ImageMediaItem(string filePath) : this(ReadImage(filePath, out var type), type)
		{
		}
	}

	public class TextMediaItem
	{
		public string Text { get; set; }

		internal TextMediaItem(string text)
		{
			Text = text;
		}
	}

	public enum VideoTypes
	{
		Ascii,
		Simple,
		Colour
	}

	public enum ImageTypes
	{
		Ascii,
		Simple,
		Colour
	}
}