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

        private static readonly byte[] Indicated_TestBytes =
            HexStringConverter.ToByteArray("41 58747261 42 43"); // 'A', 'Xtra', 'B', C'

        private static readonly IndicatorTestClass Indicated_TestObject = new()
        {
            Byte0 = (byte)'A',
            Bar = new IndicatorTestClass2()
            {
                Byte0 = (byte)'B',
                Byte1 = (byte)'C',
            },
        };

        private static readonly byte[] NotIndicated_TestBytes =
            HexStringConverter.ToByteArray("41"); // 'A'

        private static readonly IndicatorTestClass NotIndicated_TestObject = new()
        {
            Byte0 = (byte)'A',
            Bar = null,
        };

        #endregion

        #region Methods

        [Fact]
        public void Test_Indicated_Deserialization() =>
            AssertDeserializedObject(
                Indicated_TestObject, Indicated_TestBytes, Endianness.BigEndian);

        [Fact]
        public void Test_Indicated_Serialization() =>
            AssertSerializedObject(
                Indicated_TestBytes, Indicated_TestObject, Endianness.BigEndian);

        [Fact]
        public void Test_NotIndicated_Deserialization() =>
            AssertDeserializedObject(
                NotIndicated_TestObject, [ 0x41, 0, 0, 0, 0], Endianness.BigEndian); // FIXME: testBytes

        [Fact]
        public void Test_NotIndicated_Serialization() =>
            AssertSerializedObject(
                NotIndicated_TestBytes, NotIndicated_TestObject, Endianness.BigEndian);

        #endregion
    }
}
