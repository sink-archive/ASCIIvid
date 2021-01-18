# ASCIIvid & ASCIIimg
## Modern, efficient, versatile ASCII art image and video storage formats
You have reached the home of ASCIIvid and ASCIIimg. File formats for ASCII art images and video thought up by some random person on the internet one evening. To read the current draft of the spec click [here](https://github.com/cainy-a/ASCIIvid/blob/master/spec.md).
## Modern
ASCIIvid and ASCIIimg have / are planned to have official libraries for modern languages as and when I get around to it. Current list I would like to support are:
1. .NET (dotnet) Standard 2 => Works with .NET (dotnet) 5, while maintaining support for .NET (dotnet) Core 3.1 LTS
2. Python 3.x (I’m not touching 2.7 with a ten metre pole)
3. Javascript (Priority of supported platforms: Node.js first, browser second, Deno third)
4. Java (maybe) - this one may never happen as I don’t know Java, but considering it’s so similar to C# (my strongest language by far) I will hopefully try give it a shot.
## Efficient
ASCIIvid uses minimal space while supporting a full ASCII character set and optionally colour. For the most extreme space saving, the simple variants swap out each 1 byte character for 4 bits to represent 16 brightness levels, where the parser program chooses it’s own character set.
## Versatile
ASCIIvid supports both video and images. Both image and video formats come in the following variants:
- Standard - general format. Great for handmade ASCII art images / videos as you can choose your own characters.
- Simple - efficiency format. Great for auto-generated ASCII art images / videos. [See above](#efficient).
- Colour - supports RGB 8 colour with 8 bits per channel. Pretty awesome.
## Where do I start?
When I get around to adding them, there will be example files under `/examples/`. If you want to see or work on the libraries, they are developed on the [`develop` branch](https://github.com/cainy-a/ASCIIvid/tree/develop/lib).
