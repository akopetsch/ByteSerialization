// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using Xunit;
using Xunit.Abstractions;

namespace ByteSerialization.Tests.Integration
{
    public abstract class IntegrationTestBase(ITestOutputHelper testOutputHelper)
    {
        #region Properties

        protected ITestOutputHelper TestOutputHelper { get; } = testOutputHelper;

        #endregion

        #region Methods

        protected static void AssertDeserializedObject<TTestObject>(
            TTestObject expectedObject, byte[] testBytes, Endianness endianness)
        {
            // get actualObject
            using var ms = new MemoryStream(testBytes);
            using var ser = new ByteSerializer();
            object actualObject = ser.Deserialize<TTestObject>(ms, endianness);

            Assert.Equal(expectedObject, actualObject);
        }

        protected void AssertSerializedObject<TTestObject>(
            byte[] expectedBytes, TTestObject testObject, Endianness endianness)
        {
            // get actualBytes
            using var ms = new MemoryStream();
            using var ser = new ByteSerializer();
            ser.Serialize(ms, testObject, endianness);
            byte[] actualBytes = ms.ToArray();

            WriteTestOutput(nameof(expectedBytes), expectedBytes);
            WriteTestOutput(nameof(actualBytes), actualBytes);

            Assert.True(expectedBytes.SequenceEqual(actualBytes));
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
