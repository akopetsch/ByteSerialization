// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Integration
{
    public abstract class IntegrationTestBase
    {
        protected abstract object TestObject { get; }
        protected abstract byte[] TestObjectBytes { get; }

        [Fact]
        public void TestSerialization()
        {
            byte[] expected = TestObjectBytes;

            using var ms = new MemoryStream();
            using var ser = new ByteSerializer();
            ser.Serialize(ms, TestObject, Endianness.BigEndian);
            byte[] actual = ms.ToArray();

            Assert.True(expected.SequenceEqual(actual));
        }
    }
}
