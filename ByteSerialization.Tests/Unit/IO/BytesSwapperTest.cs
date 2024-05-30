// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Unit.IO
{
    public class BytesSwapperTest
    {
#pragma warning disable IDE0004
        // Disable 'Remove unnecessary cast (IDE0004)'
        // because casts make the code more readable here.

        [Fact]
        public void Test_Swap_Int32() =>
            Assert.Equal(
                expected:
                    unchecked((int)0xDDCCBBAA),
                actual: BytesSwapper.Swap(
                    unchecked((int)0xAABBCCDD)));

        [Fact]
        public void Test_Swap_UInt32() =>
            Assert.Equal(
                expected:
                    (uint)0xAABBCCDD,
                actual: BytesSwapper.Swap(
                    (uint)0xDDCCBBAA));

        [Fact]
        public void Test_Swap_Int16() =>
            Assert.Equal(
                expected:
                    unchecked((short)0xC0A0),
                actual: BytesSwapper.Swap(
                    unchecked((short)0xA0C0)));

        [Fact]
        public void Test_Swap_UInt16() =>
            Assert.Equal(
                expected:
                    (ushort)0xC0A0,
                actual: BytesSwapper.Swap(
                    (ushort)0xA0C0));

        // TODO: Test_Swap_Int64

        [Fact]
        public void Test_Swap_UInt64() =>
            Assert.Equal(
                expected: 
                    (ulong)0x2211ffeeddccbbaa,
                actual: BytesSwapper.Swap(
                    (ulong)0xaabbccddeeff1122));

#pragma warning restore IDE0004
    }
}
