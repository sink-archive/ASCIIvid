# ASCIIvid & ASCIIimg
**IMPORTANT NOTICE**  
This repository uses *British naming*, this means colour instead of color, etc. in docs and official libraries.  
Make sure you understand this before opening some issue about a typo.
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
- Colour - supports RGB colour with 8 bits per channel. Pretty awesome.
## Where do I start?
There are example files under `/examples/` (if there aren't yet, I'll probably add them while creating unit tests :P). *Example files have been created with HxD. Each example is accompanied by a `.txt` file showing the example in a readable format.*

Libraries are developed on their own branches, and releases are merged into `master`.

- [.NET (dotnet)](https://github.com/cainy-a/ASCIIvid/tree/dotnet-develop/lib/AsciiVid.NET)