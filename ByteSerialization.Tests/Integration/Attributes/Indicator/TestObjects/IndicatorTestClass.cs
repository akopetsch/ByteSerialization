// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace ByteSerialization.Tests.Integration.Attributes.Indicator.TestObjects
{
    public class IndicatorTestClass
    {
        #region Fields (const)

        public const int IndicatorValue = 
            ('X' << 24) + ('t' << 16) + ('r' << 8) + ('a' << 0); // 'Xtra'

        #endregion

        #region Properties

        [Order(0)]
        public byte Byte0 { get; set; }
        [Order(1), Indicator(IndicatorValue)]
        public IndicatorTestClass2 Bar { get; set; }

        #endregion

        #region Methods (: object)

        public override bool Equals(object obj)
        {
            if (obj is IndicatorTestClass other)
            {
                if (!Equals(Byte0, other.Byte0))
                    return false;
                if (!Equals(Bar, other.Bar))
                    return false;
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(Byte0, Bar?.GetHashCode());

        #endregion
    }
}
