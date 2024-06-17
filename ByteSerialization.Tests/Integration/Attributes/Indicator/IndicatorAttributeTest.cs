// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using ByteSerialization.Tests.Integration.Attributes.Indicator.TestObjects;
using Xunit;
using Xunit.Abstractions;

namespace ByteSerialization.Tests.Integration.Attributes.Indicator
{
    public class IndicatorAttributeTest(ITestOutputHelper testOutputHelper) :
        IntegrationTestBase(testOutputHelper)
    {
        #region Fields

        private static readonly byte[] TestBytes =
            HexStringConverter.ToByteArray("41 58747261 42 43"); // 'A', 'Xtra', 'B', C'

        private static readonly IndicatorTestClass TestObject = new()
        {
            Byte0 = (byte)'A',
            Bar = new IndicatorTestClass2()
            {
                Byte0 = (byte)'B',
                Byte1 = (byte)'C',
            },
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
