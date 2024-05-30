// SPDX-License-Identifier: MIT

using System;
using System.Text;

namespace ByteSerialization.IO
{
    public static class HexStringConverter
    {
        public static byte[] ToByteArray(string hexString)
        {
            string s = hexString.Replace(" ", string.Empty);
            byte[] byteArray = new byte[s.Length / 2];
            for (int i = 0; i < byteArray.Length; i++)
                byteArray[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
            return byteArray;
        }

        public static string ToCompactHexString(this int value) =>
            ((long)value).ToCompactHexString();

        public static string ToCompactHexString(this long value)
        {
            string s = value.ToString("x");
            int n = s.Length;
            return n % 2 == 1 ?
                "0" + s : s;
        }

        public static string ToHexString(this byte[] bytes)
        {
            var result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString("x2"));
            return result.ToString();
        }
    }
}
