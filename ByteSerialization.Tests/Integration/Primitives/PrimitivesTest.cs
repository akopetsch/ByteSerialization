// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.IO;

namespace ByteSerialization.Tests.Integration.Primitives
{
    public class PrimitivesTest : IntegrationTestBase
    {
        #region Classes

        private enum SensorStatus : byte
        {
            Active = 1,
            Inactive = 2,
            Error = 3,
        }

        private enum SensorType : short
        {
            Temperature = 1,
            Pressure = 2,
            Humidity = 3,
        }

        private class Sensor
        {
            [Order(0)]
            public byte SensorId { get; set; }
            [Order(1)]
            public SensorStatus Status { get; set; }
            [Order(2)]
            public SensorType? SensorType { get; set; }
        }

        #endregion

        #region Methods

        protected override object TestObject => new Sensor()
        {
            SensorId = 128,
            Status = SensorStatus.Inactive,
            SensorType = SensorType.Temperature,
        };

        protected override byte[] TestObjectBytes =>
            HexStringConverter.ToByteArray("80 02 0001");

        #endregion
    }
}
