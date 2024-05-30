// SPDX-License-Identifier: MIT

namespace ByteSerialization.IO
{
    public static class NibbleHelper
    {
        public static byte GetHighNibble(byte b) =>
            (byte)((b >> 4) & 0x0F);

        public static byte GetLowNibble(byte b) =>
            (byte)(b & 0x0F);

        public static byte GetNibble(byte[] bytes, int i) =>
            i % 2 == 0 ?
                GetHighNibble(bytes[i / 2]) :
                GetLowNibble(bytes[i / 2]);

        public static int GetNibblesCount(byte[] bytes) =>
            bytes.Length * 2;
    }
}
