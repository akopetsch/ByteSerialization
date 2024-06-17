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

        #region Methods ([Fact]; short/ushort)

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

        #endregion

        #region Methods ([Fact]; int/uint)

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

        #endregion

        #region Methods ([Fact]; int/uint)

        [Fact]
        public void Test_Swap_Int64() =>
            Assert.Equal(
                expected:
                    (long)0x2211ffeeddccbb00,
                actual: BytesSwapper.Swap(
                    (long)0x00bbccddeeff1122));

        [Fact]
        public void Test_Swap_UInt64() =>
            Assert.Equal(
                expected: 
                    (ulong)0x2211ffeeddccbbaa,
                actual: BytesSwapper.Swap(
                    (ulong)0xaabbccddeeff1122));

        #endregion

        #region Methods ([Fact]; float/double)

        // https://evanw.github.io/float-toy/

        [Fact]
        public void Test_Swap_Float32() =>
            Assert.Equal(
                expected:
                    (float)-40331460896358400, // DB0F4940
                actual: BytesSwapper.Swap(
                    (float)3.1415927)); //        40490FDB

        [Fact]
        public void Test_Swap_Float64() =>
            Assert.Equal(
                expected:
                    (double)3.207375630676366e-192, // 182D4454FB210940
                actual: BytesSwapper.Swap(
                    (double)3.141592653589793)); //    400921FB54442D18

        #endregion

#pragma warning restore IDE0004
    }
}
