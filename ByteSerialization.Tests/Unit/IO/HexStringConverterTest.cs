// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Unit.IO
{
    public class HexStringConverterTest
    {
        private const string hexString = "deadbeef";
        private const string hexString_groupSize2 = "dead beef";
        private static readonly byte[] byteArray = [0xDE, 0xAD, 0xBE, 0xEF];

        [Fact]
        public void Test_ToByteArray()
        {
            byte[] expected = byteArray;
            byte[] actual = HexStringConverter.ToByteArray(hexString);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_ToHexString() =>
            Assert.Equal(
                expected: hexString, 
                actual: HexStringConverter.ToHexString(byteArray));

        [Fact]
        public void Test_ToHexString_groupSize2() =>
            Assert.Equal(
                expected: hexString_groupSize2,
                actual: HexStringConverter.ToHexString(byteArray, groupSize: 2));
    }
}
