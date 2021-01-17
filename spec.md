# Spec Draft v0.1

Current thoughts on ASCIIvid and how it should work:

## File extensions and capabilities

### Video Formats

| **Format**      | **Extension** | **Focus**                 | **Extra notes**                                              |
| --------------- | ------------- | ------------------------- | ------------------------------------------------------------ |
| ASCIIvid        | .acv          | General use ASCII  videos | To be suitable for  all uses. Excels at handmade ASCII videos. |
| Simple ASCIIvid | .sacv         | Small size ASCII  videos  | Less versatile,  smaller size. Good for auto-generated ASCII videos. |
| Colour ASCIIvid | .cacv         | Colour ASCII  videos      | Move aside file  sizes, we're here for the main event: colour!!! |

### Image Formats

| **Format**      | **Extension** | **Focus**              | **Extra notes**                                              |
| --------------- | ------------- | ---------------------- | ------------------------------------------------------------ |
| ASCIIimg        | .aii          | General use ASCII  art | To be suitable for  all uses. Excels at handmade ASCII art.  |
| Simple ASCIIimg | .saii         | Small size ASCII  art  | Less versatile,  smaller size. Good for auto-generated ASCII art. |
| Colour ASCIIimg | .caii         | Colour ASCII art       | Move aside file  sizes, we're here for the main event: colour!!! |

## Binary format

### Video Formats

#### Header

| **Amount** | **Type** | **Description** | **Extra notes**                                              |
| ---------- | -------- | --------------- | ------------------------------------------------------------ |
| 2 bytes    | UInt16   | width           | supports up to  65,536 pixels                                |
| 2 bytes    | UInt16   | height          | supports up to  65,536 pixels                                |
| 1 byte     | UInt8    | framerate       | supports from 0 to  255fps, no decimal places (may consider Float16 instead) |

#### Body

A repeating pattern of the following, until the file ends.

##### *ASCIIvid*

| **Amount** | **Type**     | **Description** | **Extra notes**                          |
| ---------- | ------------ | --------------- | ---------------------------------------- |
| 1 byte     | UInt8 / Char | ASCII character | Supports any  character in the ASCII set |

##### *Simple ASCIIvid*

| **Amount** | **Type** | **Description**  | **Extra notes**                                              |
| ---------- | -------- | ---------------- | ------------------------------------------------------------ |
| 3 bits     | N/A      | Brightness level | Allows for storing  8 brightness levels (0-7). Bring your own character set. |

##### *Colour ASCIIvid*

| **Amount** | **Type**     | **Description** | **Extra notes**                          |
| ---------- | ------------ | --------------- | ---------------------------------------- |
| 1 byte     | UInt8 / Char | ASCII character | Supports any  character in the ASCII set |
| 1 byte     | UInt8        | Red Channel     | The red channel of  the colour.          |
| 1 byte     | UInt8        | Green Channel   | The green channel  of the colour.        |
| 1 byte     | UInt8        | Blue Channel    | The green channel  of the colour.        |

### Image Formats

#### Header

| **Amount** | **Type** | **Description** | **Extra notes**               |
| ---------- | -------- | --------------- | ----------------------------- |
| 2 bytes    | UInt16   | width           | supports up to  65,536 pixels |
| 2 bytes    | UInt16   | height          | supports up to  65,536 pixels |


#### Body

A repeating pattern of the following, until the file ends.

##### *ASCIIimg*

| **Amount** | **Type**     | **Description** | **Extra notes**                          |
| ---------- | ------------ | --------------- | ---------------------------------------- |
| 1 byte     | UInt8 / Char | ASCII character | Supports any  character in the ASCII set |

##### *Simple ASCIIimg*

| **Amount**        | **Type** | **Description**  | **Extra notes**                                              |
| ----------------- | -------- | ---------------- | ------------------------------------------------------------ |
| 4 bits (1 nibble) | N/A      | Brightness level | Allows for storing 16 brightness levels (0-15). Bring your own character set. |

##### *Colour ASCIIimg*

| **Amount** | **Type**     | **Description** | **Extra notes**                          |
| ---------- | ------------ | --------------- | ---------------------------------------- |
| 1 byte     | UInt8 / Char | ASCII character | Supports any  character in the ASCII set |
| 1 byte     | UInt8        | Red Channel     | The red channel of  the colour.          |
| 1 byte     | UInt8        | Green Channel   | The green channel  of the colour.        |
| 1 byte     | UInt8        | Blue Channel    | The green channel  of the colour.        |