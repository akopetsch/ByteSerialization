// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace ByteSerialization.Tests.Integration.TestObjects
{
    public class Sensor
    {
        #region Properties

        [Order(0)]
        public byte SensorId { get; set; }
        [Order(1)]
        public SensorStatus Status { get; set; }
        [Order(2)]
        public SensorType? SensorType { get; set; }

        #endregion

        #region Methods (: object)

        public override bool Equals(object obj)
        {
            if (obj is Sensor other)
            {
                if (!Equals(SensorId, other.SensorId))
                    return false;
                if (!Equals(Status, other.Status))
                    return false;
                if (!Equals(SensorType, other.SensorType))
                    return false;
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(
                    SensorId.GetHashCode(),
                    Status.GetHashCode(),
                    SensorType?.GetHashCode());

        #endregion
    }
}
