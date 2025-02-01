# Total Annihilation File Formats

Total Annihilation makes use of a variety of file types. They are:

* General data
  - .ota - Text - Map settings and mission objectives.
  - .gui - Text - Interface layout.
  - .sct - Binary - Map tiles.
  - .txt - Text - AI profiles.
  - [.tdf - Text - Very similar to FBI files, used for lots of other bits of gamedata like sound groups, weapons, factions, etc.](./TDF.md)
  - [.fbi - Text - Unit information, including references to other files such as model and sounds.](./FBI.md)
  - .tnt - Binary - Maps, terrain and height info.
* Art
  - [.gaf - Binary -  Media format for images, animated images, UI elements, etc.](./GAF.md)
  - .bmp - Binary - Standard Bitmap images.
  - [.pcx - Binary - Standard image format. Static images for UI elements. Usually unitpics.](./PCX.md)
  - .fnt - Binary - Standard font file.
  - .3do - Binary - 3D models.
* Audio
  - .wav - Binary - Standard Wave audio. Used for the sound effects.
  - .mp3 - Binary - Standard MP3 audio. Used for the music.
* Scripts
  - [.bos - Text - Script for unit animations.](./BOS.md)
  - .cob - Binary - Compiled form of BOS.
  - .h - Binary - Script libraries.
* Archives
  - .hpi - Binary - Archive, just a compressed directory.
  - .ccx - Binary - Essentially HPI, but used by CC expansion.
  - .ufo - Binary - Essentially HPI, but used by the free unit DLCs.
* Other
  - .bat - Binary - Standard batch file. Not used directly by the game, only for compiling the script files.
  - .pal - Palette file format, defines the colours that TA can display.
  - .alp - Palette file format, defines the colours that TA can display.
  - .lht - Palette file format, defines the colours that TA can display.
  - .shd - Palette file format, defines the colours that TA can display.
* Unknown
  - .xls - under units called *ALLUNITS* so possibly a list format

## References

This project is very much standing on the shoulders of giants, as this would not have been possible