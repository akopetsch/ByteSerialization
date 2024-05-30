// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Unit.IO
{
    public class HexStringConverterTest
    {
        [Fact]
        public void Test_ToByteArray()
        {
            byte[] expected = [0xDE, 0xAD, 0xBE, 0xEF];
            byte[] actual = HexStringConverter.ToByteArray("deadbeef");
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_ToHexString()
        {
            string expected = "deadbeef";
            string actual = HexStringConverter.ToHexString([0xDE, 0xAD, 0xBE, 0xEF]);
            Assert.Equal(expected, actual);
        }
    }
}
