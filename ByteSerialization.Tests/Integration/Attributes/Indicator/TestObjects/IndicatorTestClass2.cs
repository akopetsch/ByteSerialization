// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace ByteSerialization.Tests.Integration.Attributes.Indicator.TestObjects
{
    public class IndicatorTestClass2
    {
        #region Properties

        [Order(0)]
        public byte Byte0 { get; set; }
        [Order(1)]
        public byte Byte1 { get; set; }

        #endregion

        #region Methods (: object)

        public override bool Equals(object obj)
        {
            if (obj is IndicatorTestClass2 other)
            {
                if (!Equals(Byte0, other.Byte0))
                    return false;
                if (!Equals(Byte1, other.Byte1))
                    return false;
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(Byte0, Byte1);

        #endregion
    }
}
