// SPDX-License-Identifier: MIT

using AKopetsch.IOHelper;
using ByteSerialization.Tests.Integration.Values.BasicSerializables.TestObjects;
using Xunit;
using Xunit.Abstractions;

namespace ByteSerialization.Tests.Integration.Values.Primitives
{
    public class BasicSerializablesTest(ITestOutputHelper testOutputHelper) : 
        IntegrationTestBase(testOutputHelper)
    {
        #region Fields

        private static readonly Sensor TestObject = new()
        {
            SensorId = 128,
            Status = SensorStatus.Inactive,
            SensorType = SensorType.Temperature,
        };

        private static readonly byte[] TestBytes =
            HexStringConverter.ToByteArray("80 02 0001");

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
