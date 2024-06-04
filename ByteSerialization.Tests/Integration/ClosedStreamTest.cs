// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Integration
{
    public class ClosedStreamTest
    {
        public class Struct1
        {
            [Order(0)]
            public byte Byte0 { get; set; }

            [Order(1)]
            public byte Byte1 { get; set; }
        }

        [Fact]
        public void Test()
        {
            byte[] bytes = [0x01, 0x02, 0x03];
            using var ms = new MemoryStream(bytes);

            using var ser = new ByteSerializer();
            var struct1 = ser.Deserialize<Struct1>(ms, Endianness.LittleEndian);
            byte byte2 = Convert.ToByte(ms.ReadByte());

            Assert.Equal(bytes[0], struct1.Byte0);
            Assert.Equal(bytes[1], struct1.Byte1);
            Assert.Equal(bytes[2], byte2);
        }
    }
}
