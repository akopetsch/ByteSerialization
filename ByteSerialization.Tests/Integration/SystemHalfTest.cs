// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Integration
{
    public class SystemHalfTest
    {
        #region Classes (ICustomSerializer)

        public class SystemHalfCustomSerializer : ICustomSerializer
        {
            public object Deserialize(EndianBinaryReader reader) =>
                BitConverter.ToHalf(
                    BitConverter.GetBytes(
                        reader.ReadInt16()));

            public void Serialize(EndianBinaryWriter writer, object value) =>
                writer.Write(
                    BitConverter.ToInt16(
                        BitConverter.GetBytes((Half)value)));
        }

        private static readonly Half Pi = (Half)3.141;
        private static readonly byte[] PiBytesBE = [0x42, 0x48];
        private static readonly byte[] PiBytesLE = [0x48, 0x42];

        #endregion

        #region Methods ([Fact])

        [Fact]
        public void Test_Serialize_BigEndian() =>
            Assert.True(PiBytesBE.SequenceEqual(Serialize(Pi, Endianness.BigEndian)));

        [Fact]
        public void Test_Serialize_LittleEndian() =>
            Assert.True(PiBytesLE.SequenceEqual(Serialize(Pi, Endianness.LittleEndian)));

        [Fact]
        public void Test_Deserialize_BigEndian() =>
            Assert.Equal(expected: Pi, actual: Deserialize(PiBytesBE, Endianness.BigEndian));

        [Fact]
        public void Test_Deserialize_LittleEndian() =>
            Assert.Equal(expected: Pi, actual: Deserialize(PiBytesLE, Endianness.LittleEndian));

        #endregion

        #region Methods (helper)

        private static Half Deserialize(byte[] bytes, Endianness endianness) =>
            GetSerializer().Deserialize<Half>(bytes, endianness);

        private static byte[] Serialize(Half value, Endianness endianness) =>
            GetSerializer().Serialize(value, endianness);

        private static ByteSerializer GetSerializer()
        {
            var serializer = new ByteSerializer();
            serializer.RegisterCustomSerializer<Half>(new SystemHalfCustomSerializer());
            return serializer;
        }

        #endregion
    }
}
