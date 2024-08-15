# .hpi File Format

*File extensions covered: .hpi, .ccx, .ufo*

The HPI format is an archive file containing the various files that *Total Annihilation* needs.

> For all practical purposes, you can think of the HPI file like their own home-grown ZIP file, containing its own directory structure and groups of files. *TA Design Guide (1998)*

The *Core Contingency* expansion used CCX files, while the small free downloadable units used UFO archives. However, both are just the HPI format using different file extensions. So what's convered here applies to those too.

Contents:
* [Structure](#structure)
  - [Header](#header)
  - [Directory Info](#directory-info)
  - [Directory List](#directory-list)
  - [File Data](#file-data)
* [References](#references)

## Structure

HPI files are broken down into 4 sections:
* header
* directory info
* directory list
* file data

### Header

The header describes general file information.

| Type | Bytes | Name | Description |
| ---- | ----- | ---- | ----------- |
| char[] | 8 | marker | Format identifier. Always `HAPI` |
| char[] | 8 | saveMarker | Save file identifier. If reading a save file it's `BANK`, otherwise it's `0x00001000`
| int | 4 | directorySize | Size in bytes of the header, directory info, and directory list sections |
| int | 4 | decryptionKey | Key used to decrypt the rest of the archive |
| int | 4 | directoryOffset | Offset to start of directory info section, which is almost always 20 |

After the header the rest of the file is encrypted, as such it requires decrypting with the `decryptionKey` before continuing.

The following psuedocode describes how to decrypt:

```
key = NOT ((decryptionKey * 4) OR (decryptionKey >> 6))
for i = 0; i < buffer.Length:
    tkey = (directoryIndex + i) XOR key
    buffer[i] = tkey XOR (NOT buffer[i])
```

### Directory Info

Directory info describes the entries at the top level of the archive.

| Type | Bytes | Name | Description |
| ---- | ----- | ---- | ----------- |
| int | 4 | totalEntries | Number of entries |
| int | 4 | entryListOffset | Offset to the start of the directory list section

### Directory List

Directory list describes the offsets of the entries at the archive's top level and whether each one is a subdirectory or a file.

From `directoryInfo.entryListOffset`, loop `directoryInfo.totalEntries` times to build the following struct each loop.

| Type | Bytes | Name | Description |
| ---- | ----- | ---- | ----------- |
| int | 4 | nameOffset | Offset to this entry's name |
| int | 4 | dataEntryOffset | Offset to this entry's data |
| byte | 1 | type | `0` marks this entry as a file, `1` for a subdirectory |

If the entry is a subdirectory then `dataEntryOffset` points to another pair of directory info and directory list sections, which describe the entries for that subdirectory.

If the entry is a file then `dataEntryOffset` points to the following struct.

| Type | Bytes | Name | Description |
| ---- | ----- | ---- | ----------- |
| int | 4 | dataOffset | Offset to the contents of the file |
| int | 4 | fileSize | Size of the decompressed file |
| byte | 1 | compressionType | `0` indicates the contents are uncompressed, `1` indicates the contents are compressed with LZ77, and `2` indicates the contents are compressed with ZLib

### File Data

File data describes the contents of each file. If the file was compressed then it was split into chunks of 64kb, then compressed.

Pseudocode for getting the chunks:

```
chunks = fileEntry.fileSize / 65536
if (size % 65536) > 0:
    chunks += 1
```

The first set of data at `fileEntry.dataOffset` is a list of ints that describe the size of each chunk. Immediately following this list is this struct.

| Type | Bytes | Name | Description |
| ---- | ----- | ---- | ----------- |
| char[] | 8 | marker | Chunk identifier. Always `SQSH` |
| byte | 1 | unknown1 | Always `0x02`. Joe suspects it might be a version number |
| byte | 1 | compressionMethod | Compression algorithm used. `1` indicates the contents are compressed with LZ77, and `2` indicates the contents are compressed with ZLib |
| byte | 1 | isEncrypted | Whether the chunk is encrypted |
| int | 4 | compressedSize | Size of the chunk |
| int | 4 | decompressedSize | Size of the chunk after decompression |
| int | 4 | checksum | Calculated by adding up every byte in the chunk |
| byte[] | X | compressedData | `compressedSize` bytes of data |

Calculate the checksum prior to decryption and decompressing.

The following psuedocode describes how to decrypt the chunk's data:

```
for i = 0; i < compressedSize:
    chunk.compressedData[i] = (chunk.compressedData[i] - i) XOR i
```

With the chunk decrypted it's now possible to decompress with either LZ77 or ZLib.

Joe has some LZ77 C code showing how to decrypt with that, alternatively check this project's code for a C# example that's a translated version of Joe's.

## References

* Joe D, "HPI File Format v. 1.4", (1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/ta-hpi-fmt.txt [Last accessed 08/08/2024]
* TA Design Guide, "Total Annihilation: The Nuts and Bolts", (November 1998), https://files.tauniverse.com/files/ta/resources/tutorials/ta-design-guide/browse-online/tadesign/ta-files.htm [Last accessed 08/08/2024]