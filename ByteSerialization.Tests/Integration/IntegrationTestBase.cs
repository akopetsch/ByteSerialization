// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Integration
{
    public abstract class IntegrationTestBase<TTestObject>
    {
        protected abstract TTestObject TestObject { get; }
        protected abstract byte[] TestObjectBytes { get; }
        protected abstract Endianness Endianness { get; }

        [Fact]
        public void TestDeserialization()
        {
            TTestObject expected = TestObject;

            using var ms = new MemoryStream(TestObjectBytes);
            using var ser = new ByteSerializer();
            object actual = ser.Deserialize<TTestObject>(ms, Endianness);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestSerialization()
        {
            byte[] expected = TestObjectBytes;

            using var ms = new MemoryStream();
            using var ser = new ByteSerializer();
            ser.Serialize(ms, TestObject, Endianness);
            byte[] actual = ms.ToArray();

            Assert.True(expected.SequenceEqual(actual));
        }
    }
}
