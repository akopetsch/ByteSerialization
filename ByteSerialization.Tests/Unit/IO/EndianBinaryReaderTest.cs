// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Unit.IO
{
    public class EndianBinaryReaderTest
    {
        [Fact]
        public void Test_ReadSingle_BE() =>
            AssertReadResult(13.5407705f, "4158 a6ff", Endianness.BigEndian, r => r.ReadSingle());

        private void AssertReadResult<T>(T expectedValue, string hexStringToRead, Endianness endianness, Func<EndianBinaryReader, T> readFunc)
        {
            using var ms = new MemoryStream(HexStringConverter.ToByteArray(hexStringToRead));
            using var reader = new EndianBinaryReader(ms, endianness);
            T actualValue = readFunc.Invoke(reader);
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
