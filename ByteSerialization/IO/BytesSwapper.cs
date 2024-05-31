// SPDX-License-Identifier: MIT

using System.Runtime.CompilerServices;

namespace ByteSerialization.IO
{
    public static class BytesSwapper
    {
        #region Fields (const)

        private const uint mask0 = unchecked((uint)0xFF << 0 * 8);
        private const uint mask1 = unchecked((uint)0xFF << 1 * 8);
        private const uint mask2 = unchecked((uint)0xFF << 2 * 8);
        private const uint mask3 = unchecked((uint)0xFF << 3 * 8);
        private const ulong mask4 = unchecked((ulong)0xFF << 4 * 8);
        private const ulong mask5 = unchecked((ulong)0xFF << 5 * 8);
        private const ulong mask6 = unchecked((ulong)0xFF << 6 * 8);
        private const ulong mask7 = unchecked((ulong)0xFF << 7 * 8);

        #endregion

        #region Methods (short/ushort)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Swap(short x) => 
            (short)Swap((ushort)x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Swap(ushort x) =>
            (ushort)(x << 8 | (byte)(x >> 8));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short SwapIf(short x, bool condition) =>
            condition ? Swap(x) : x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort SwapIf(ushort x, bool condition) =>
            condition ? Swap(x) : x;

        #endregion

        #region Methods (int/uint)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Swap(int x) => 
            (int)Swap((uint)x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Swap(uint x) =>
            /* 3 */ x << 24 |
            /* 2 */ x << 8 & mask2 |
            /* 1 */ x >> 8 & mask1 |
            /* 0 */ x >> 24;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SwapIf(int x, bool condition) =>
            condition ? Swap(x) : x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint SwapIf(uint x, bool condition) =>
            condition ? Swap(x) : x;

        #endregion

        #region Methods (long/ulong)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Swap(long x) => 
            (long)Swap((ulong)x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Swap(ulong x) =>
            /* 7 */ x << 56 & mask7 |
            /* 6 */ x << 40 & mask6 |
            /* 5 */ x << 24 & mask5 |
            /* 4 */ x << 8 & mask4 |
            /* 3 */ x >> 8 & mask3 |
            /* 2 */ x >> 24 & mask2 |
            /* 1 */ x >> 40 & mask1 |
            /* 0 */ x >> 56;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long SwapIf(long x, bool condition) =>
            condition ? Swap(x) : x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong SwapIf(ulong x, bool condition) =>
            condition ? Swap(x) : x;

        #endregion
    }
}
