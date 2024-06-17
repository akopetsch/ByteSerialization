// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using ByteSerialization.Tests.Integration.Values.BasicSerializables.TestObjects;
using Xunit.Abstractions;

namespace ByteSerialization.Tests.Integration.Values.Primitives
{
    public class BasicSerializablesTest(ITestOutputHelper testOutputHelper) : 
        IntegrationTestBase<Sensor>(testOutputHelper)
    {
        #region Properties

        protected override Sensor TestObject => new()
        {
            SensorId = 128,
            Status = SensorStatus.Inactive,
            SensorType = SensorType.Temperature,
        };

        protected override byte[] TestObjectBytes =>
            HexStringConverter.ToByteArray("80 02 0001");

        protected override Endianness Endianness =>
            Endianness.BigEndian;

        #endregion
    }
}
