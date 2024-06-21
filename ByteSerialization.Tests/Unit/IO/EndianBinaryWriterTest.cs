// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Unit.IO
{
    public class EndianBinaryWriterTest
    {
        [Fact]
        public void Test_WriteSingle_BE() =>
            AssertWriteResult("4158 a6ff", 13.5407705f, Endianness.BigEndian, (r, x) => r.Write(x));

        private void AssertWriteResult<T>(string expectedHexString, T valueToWrite, Endianness endianness, Action<EndianBinaryWriter, T> writeFunc)
        {
            using var ms = new MemoryStream();
            using var writer = new EndianBinaryWriter(ms, endianness);
            writeFunc.Invoke(writer, valueToWrite);
            byte[] actualBytes = ms.ToArray();
            byte[] expectedBytes = HexStringConverter.ToByteArray(expectedHexString);
            Assert.True(Enumerable.SequenceEqual(expectedBytes, actualBytes));
        }
    }
}
