// SPDX-License-Identifier: MIT

using AKopetsch.IOHelper;
using ByteSerialization.Tests.Integration.Attributes.FinalElement.TestObjects;
using Xunit;
using Xunit.Abstractions;

namespace ByteSerialization.Tests.Integration.Attributes.FinalElement
{
    public class FinalElementAttributeTest(ITestOutputHelper testOutputHelper) :
        IntegrationTestBase(testOutputHelper)
    {
        #region Fields

        private static readonly byte[] TestBytes =
            HexStringConverter.ToByteArray("01 02 03 FF 05");

        private static readonly ListContainer TestObject = new()
        {
            Bytes = [1, 2, 3, 0xFF],
            EndByte = 5,
        };

        #endregion

        #region Methods

        [Fact]
        public void Test_Deserialization() =>
            AssertDeserializedObject(
                TestObject, TestBytes, Endianness.BigEndian);

        [Fact]
        public void Test_Serialization() =>
            AssertSerializedObject(
                TestBytes, TestObject, Endianness.BigEndian);

        #endregion
    }
}
