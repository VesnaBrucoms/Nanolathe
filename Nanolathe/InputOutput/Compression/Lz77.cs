using static System.BitConverter;

namespace Nanolathe.InputOutput.Compression
{
    /// <summary>
    /// Decompression functions of LZ77 compression, with 4096 byte window.
    /// </summary>
    internal static class Lz77
    {
        private const int WindowSize = 4096;

        /// <summary>
        /// Decompresses input byte array to output array of outputLength.
        /// </summary>
        /// <param name="input">Byte array to decompress.</param>
        /// <param name="outputLength">Length of output byte array.</param>
        /// <returns>Decompressed byte array.</returns>
        public static byte[] Decompress(byte[] input, int outputLength)
        {
            byte[] window = new byte[WindowSize];
            byte[] output = new byte[outputLength];

            ushort count;
            short inputIndex = 0;
            short outputIndex = 0;
            short windowInputIndex;
            short windowOutputIndex = 1;
            short tag;

            bool done = false;
            while (!done)
            {
                tag = input[inputIndex++];
                for (int i = 0; i < 8; i++)
                {
                    if ((tag & 1) == 0) // Next is an actual byte
                    {
                        output[outputIndex++] = input[inputIndex];
                        window[windowOutputIndex] = input[inputIndex];
                        windowOutputIndex = (short)((windowOutputIndex + 1) & 0xFFF);
                        inputIndex++;
                    }
                    else
                    {
                        ushort packed = ToUInt16([input[inputIndex], input[inputIndex + 1]]);
                        inputIndex += 2;
                        windowInputIndex = (short)(packed >> 4);
                        if (windowInputIndex == 0)
                        {
                            done = true;
                            break;
                        }
                        else
                        {
                            count = (ushort)((packed & 0x0F) + 2);
                            for (int x = 0; x < count; x++)
                            {
                                output[outputIndex++] = window[windowInputIndex];
                                window[windowOutputIndex] = window[windowInputIndex];
                                windowInputIndex = (short)((windowInputIndex + 1) & 0xFFF);
                                windowOutputIndex = (short)((windowOutputIndex + 1) & 0xFFF);
                            }
                        }
                    }
                    tag >>= 1;
                }
            }
            return output;
        }
    }
}
