using System.Text;

namespace Nanolathe.InputOutput
{
    /// <summary>
    /// Common functions used for reading and writing files.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// Reads bytes from pointer until the terminator is reached.
        /// </summary>
        /// <param name="bytes">Byte array to read the string from.</param>
        /// <param name="pointer">Start index to start reading from.</param>
        /// <param name="terminator">Character to stop reading at. Defaults to '\0'.</param>
        /// <returns></returns>
        public static string GetAsciiString(byte[] bytes, int pointer, char terminator = '\0')
        {
            StringBuilder newString = new StringBuilder();
            while (true)
            {
                char character = (char)bytes[pointer];
                pointer++;
                if (character == terminator)
                {
                    break;
                }
                else
                {
                    newString.Append(character);
                }
            }
            return newString.ToString();
        }

        /// <summary>
        /// Reads bytes from startIndex for length bytes.
        /// </summary>
        /// <param name="bytes">Byte array to read the string from.</param>
        /// <param name="startIndex">Start index to start reading from.</param>
        /// <param name="length">Number of bytes to read.</param>
        /// <returns></returns>
        public static string GetAsciiString(byte[] bytes, int startIndex, int length)
        {
            StringBuilder newString = new StringBuilder();
            for (int i = 0; i < (startIndex + length); i++)
            {
                newString.Append((char)bytes[i]);
            }
            return newString.ToString();
        }
    }
}
