// SPDX-License-Identifier: MIT

using System;

namespace ByteSerialization.IO
{
    public static class BitsHelper
    {
        public const int BitsPerByte = 8;
        public const int MaxBitIndex = BitsPerByte - 1;

        public static bool[] GetBits(byte byteValue, BitOrder bitOrder)
        {
            bool[] bits = new bool[BitsPerByte];
            for (int bitIndex = 0; bitIndex < BitsPerByte; bitIndex++)
            {
                byte bitMask = GetBitMask(bitIndex, bitOrder);
                bits[bitIndex] = (byteValue & bitMask) != 0;
            }
            return bits;
        }

        public static byte GetBitMask(int bitIndex, BitOrder bitOrder)
        {
            if (bitIndex < 0 || bitIndex > MaxBitIndex)
                throw new ArgumentOutOfRangeException(
                    nameof(bitIndex), $"Must be between {0} and {MaxBitIndex}");

            return bitOrder switch
            {
                BitOrder.MsbFirst => (byte)(1 << MaxBitIndex >> bitIndex),
                BitOrder.LsbFirst => (byte)(1 << bitIndex),
                _ => throw new ArgumentException("Must have a valid value.", nameof(bitOrder)),
            };
        }
    }
}
