using System;

namespace Scytale
{
    public static class StringExtensions
    {
        public static string ToHex(this byte[] the_bytes, char separator)
        {
            return BitConverter.ToString(the_bytes, 0).Replace('-', separator);
        }

        public static byte[] ToBytes(this string the_string)
        {
            char separator = the_string[2];

            string[] pairs = the_string.Split(separator);
            byte[] bytes = new byte[pairs.Length];
            for (int i = 0; i < pairs.Length; i++)
                bytes[i] = Convert.ToByte(pairs[i], 16);
            return bytes;
        }
    }
}
