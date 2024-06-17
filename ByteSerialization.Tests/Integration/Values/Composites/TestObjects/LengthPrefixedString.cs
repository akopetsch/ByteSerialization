// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.IO;

namespace ByteSerialization.Tests.Integration.Values.Composites.TestObjects
{
    public class LengthPrefixedString
    {
        #region Properties

        [Order(0)]
        public byte Length { get; set; }

        [Order(1), Length(nameof(Length))]
        public char[] CharArray { get; set; }

        #endregion

        #region Constructor

        public LengthPrefixedString()
        { }

        public LengthPrefixedString(string s)
        {
            Length = Convert.ToByte(s.Length);
            CharArray = s.ToCharArray();
        }

        #endregion

        #region Methods (: object)

        public override bool Equals(object obj)
        {
            if (obj is LengthPrefixedString other)
            {
                if (!Equals(Length, other.Length))
                    return false;
                if (!CharArray.SequenceEqual(other.CharArray))
                    return false;
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(
                    Length.GetHashCode(),
                    HashCodeHelper.CombineHashCodes(CharArray));

        #endregion
    }
}
