// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.IO;

namespace ByteSerialization.Tests.Integration.Attributes.FinalElement.TestObjects
{
    internal class ListContainer
    {
        #region Properties

        [Order(0), FinalElement(byte.MaxValue)]
        public List<byte> Bytes { get; set; }
        [Order(1)]
        public byte EndByte { get; set; }

        #endregion

        #region Methods (: object)

        public override bool Equals(object obj)
        {
            if (obj is ListContainer other)
            {
                if (!Enumerable.SequenceEqual(Bytes, other.Bytes))
                    return false;
                if (!Equals(EndByte, other.EndByte))
                    return false;
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(
                HashCodeHelper.CombineHashCodes(Bytes?.ToArray()), 
                EndByte);

        #endregion
    }
}
