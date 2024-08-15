using EasyCompressor;
using Nanolathe.InputOutput.Compression;
using Nanolathe.Models.Hpi;
using System.Diagnostics;
using System.IO;
using static Nanolathe.InputOutput.Utils;
using static System.BitConverter;
using static System.IO.File;

namespace Nanolathe.InputOutput
{
    static class Hpi
    {
        private const string HpiMarker = "HAPI";
        private const string SaveMarker = "BANK";
        private const int ChunkSize = 65536;

        private enum CompressionType
        {
            Uncompressed = 0,
            LZ77 = 1,
            ZLib = 2
        }

        public static void Extract(string sourceArchivePath, string sourceArchiveName, string outputPath)
        {
            byte[] archiveBytes = ReadAllBytes($"{sourceArchivePath}\\{sourceArchiveName}");

            int pointer = 0;
            byte[] fullDecrypted = new byte[archiveBytes.Length];
            archiveBytes = ReadHeaderAndDecryptArchive(archiveBytes, ref pointer);
            Array.Copy(archiveBytes, 0, fullDecrypted, 20, archiveBytes.Length);

            HpiEntry[] topArchiveEntries = ReadDirectoryEntries(fullDecrypted, ref pointer);
            ArchiveDirectory archive = ReadDirectory(fullDecrypted, sourceArchiveName, topArchiveEntries, ref pointer);

            WriteExtractedArchive(outputPath, archive);
        }

        private static byte[] ReadHeaderAndDecryptArchive(byte[] archive, ref int pointer)
        {
            string hpiMarker = GetAsciiString(archive, pointer, 4);
            pointer += 4;
            if (hpiMarker != HpiMarker)
            {
                Trace.WriteLine("Not HPI file");
            }

            string saveMarker = GetAsciiString(archive, pointer, 4);
            pointer += 4;
            if (saveMarker == SaveMarker)
            {
                Trace.WriteLine("Is save file");
            }
            
            int directorySize = ToInt32(archive, pointer);
            pointer += 4;
            int decryptKey = ToInt32(archive, pointer);
            pointer += 4;
            int fileOffset = ToInt32(archive, pointer);

            pointer = fileOffset;

            return DecryptFile(archive, decryptKey, fileOffset);
        }

        private static HpiEntry[] ReadDirectoryEntries(byte[] bytes, ref int pointer)
        {
            int totalEntries = ToInt32(bytes, pointer);
            pointer += 4;
            pointer = ToInt32(bytes, pointer);

            HpiEntry[] entries = new HpiEntry[totalEntries];
            for (int i = 0; i < entries.Length; i++)
            {
                int nameOffset = ToInt32(bytes, pointer);
                pointer += 4;

                int dataOffset = ToInt32(bytes, pointer);
                pointer += 4;

                HpiEntry.EntryType type = (HpiEntry.EntryType)bytes[pointer];
                pointer += 1;

                entries[i] = new HpiEntry(nameOffset, dataOffset, type);
            }
            return entries;
        }

        private static ArchiveDirectory ReadDirectory(byte[] bytes, string name, HpiEntry[] entries, ref int pointer)
        {
            ArchiveDirectory directory = new ArchiveDirectory(name);
            for (int i = 0; i < entries.Length; i++)
            {
                string entryName = GetAsciiString(bytes, entries[i].NameOffset);
                if (entries[i].Type == HpiEntry.EntryType.Subdirectory)
                {
                    pointer = entries[i].DataOffset;
                    HpiEntry[] subdirectoryEntries = ReadDirectoryEntries(bytes, ref pointer);
                    directory.Subdirectories.Add(ReadDirectory(bytes, entryName, subdirectoryEntries, ref pointer));
                }
                else if (entries[i].Type == HpiEntry.EntryType.File)
                {
                    int dataOffset = ToInt32(bytes, entries[i].DataOffset);
                    int fileSize = ToInt32(bytes, entries[i].DataOffset + 4);
                    CompressionType type = (CompressionType)bytes[entries[i].DataOffset + 8];
                    
                    byte[] data = ReadFileData(bytes, dataOffset, fileSize, type);
                    directory.Files.Add(new ArchiveFile(entryName, data));
                }
                else
                {
                    Trace.WriteLine("Unknown entry type");
                }
            }
            return directory;
        }

        private static byte[] ReadFileData(byte[] bytes, int pointer, int size, CompressionType compressionType)
        {
            int chunks = GetNumberOfChunks(size);

            int[] chunkSizes = new int[chunks];
            for (int i = 0; i < chunks; i++)
            {
                chunkSizes[i] = ToInt32(bytes, pointer);
                pointer += 4;
            }

            ZLibCompressor zlib = new ZLibCompressor();
            List<byte> output = new List<byte>();
            for (int i = 0; i < chunks; i++)
            {
                string marker = GetAsciiString(bytes, pointer, 4);
                pointer += 4;
                byte unknown1 = bytes[pointer];
                pointer += 1;
                CompressionType compMethod = (CompressionType)bytes[pointer];
                pointer += 1;
                bool isEncrypted = ToBoolean(bytes, pointer);
                pointer += 1;
                int compressedSize = ToInt32(bytes, pointer);
                byte[] data = new byte[compressedSize];
                pointer += 4;
                int decompressedSize = ToInt32(bytes, pointer);
                pointer += 4;
                int checksum = ToInt32(bytes, pointer);
                pointer += 4;
                Array.Copy(bytes, pointer, data, 0, compressedSize);

                // CALC CHECKSUM BEFORE DECRYPTION

                if (isEncrypted)
                {
                    data = DecryptChunk(data);
                }

                if (compMethod == CompressionType.LZ77)
                {
                    output.AddRange(Lz77.Decompress(data, decompressedSize));
                }
                else if (compMethod == CompressionType.ZLib)
                {
                    output.AddRange(zlib.Decompress(data));
                }
            }
            return output.ToArray();
        }

        private static void WriteExtractedArchive(string outputPath, ArchiveDirectory archive)
        {
            string clean = archive.Name.Substring(0, archive.Name.Length - 4);
            string fullPath = $"{outputPath}\\{clean}";
            Directory.CreateDirectory(fullPath);
            WriteDirectoriesAndFiles(fullPath, archive.Subdirectories, archive.Files);
        }

        private static void WriteDirectoriesAndFiles(string outputPath, List<ArchiveDirectory> directories, List<ArchiveFile> files)
        {
            foreach (ArchiveDirectory dir in directories)
            {
                string fullPath = $"{outputPath}\\{dir.Name}";
                Trace.WriteLine(fullPath);
                Directory.CreateDirectory(fullPath);
                WriteDirectoriesAndFiles(fullPath, dir.Subdirectories, dir.Files);
            }
            foreach (ArchiveFile file in files)
            {
                string fullPath = $"{outputPath}\\{file.Name}";
                Trace.WriteLine(fullPath);
                WriteAllBytes(fullPath, file.Data);
            }
        }

        private static int GetNumberOfChunks(int fileSize)
        {
            int chunks = fileSize / ChunkSize;
            if ((fileSize % ChunkSize) > 0)
            {
                chunks++;
            }
            return chunks;
        }

        private static byte[] DecryptFile(byte[] input, int decryptKey, int fileOffset)
        {
            int key = ~((decryptKey * 4) | (decryptKey >> 6));
            byte[] buffer = new byte[input.Length - 20];
            Array.Copy(input, fileOffset, buffer, 0, buffer.Length);
            for (int i = 0; i < buffer.Length; i++)
            {
                short tkey = (short)((fileOffset + i) ^ key);
                buffer[i] = (byte)(tkey ^ ~buffer[i]);
            }
            return buffer;
        }

        private static byte[] DecryptChunk(byte[] input)
        {
            byte[] output = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (byte)((input[i] - i) ^ i);
            }
            return output;
        }
    }
}
