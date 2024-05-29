// SPDX-License-Identifier: GPL-2.0-only

namespace ByteSerialization.IO.Extensions
{
    public static class NibbleExtensions
    {
        public static byte GetHighNibble(this byte b) => 
            (byte)((b >> 4) & 0x0F);

        public static byte GetLowNibble(this byte b) => 
            (byte)(b & 0x0F);

        public static byte GetNibble(this byte[] bytes, int i) =>
            i % 2 == 0 ?
                bytes[i / 2].GetHighNibble() :
                bytes[i / 2].GetLowNibble();

        public static int GetNibblesCount(this byte[] bytes) =>
            bytes.Length * 2;
    }
}
