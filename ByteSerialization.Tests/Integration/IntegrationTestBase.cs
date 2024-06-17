// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;
using Xunit.Abstractions;

namespace ByteSerialization.Tests.Integration
{
    public abstract class IntegrationTestBase<TTestObject>(ITestOutputHelper testOutputHelper)
    {
        #region Properties

        protected ITestOutputHelper TestOutputHelper { get; } = testOutputHelper;

        protected abstract TTestObject TestObject { get; }
        protected abstract byte[] TestObjectBytes { get; }
        protected abstract Endianness Endianness { get; }

        #endregion

        #region Methods

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

            WriteTestOutput(nameof(expected), expected);
            WriteTestOutput(nameof(actual), actual);

            Assert.True(expected.SequenceEqual(actual));
        }

        private void WriteTestOutput(string name, byte[] bytes)
        {
            TestOutputHelper.WriteLine($"{name}:");
            TestOutputHelper.WriteLine(HexStringConverter.ToHexString(bytes, 2));
            TestOutputHelper.WriteLine(string.Empty);
        }

        #endregion
    }
}
