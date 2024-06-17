// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace ByteSerialization.Tests.Integration.Values.Composites.TestObjects
{
    public class Manufacturer
    {
        #region Properties

        [Order(0)]
        public LengthPrefixedString Name { get; set; }

        #endregion

        #region Constructor

        public Manufacturer()
        { }

        public Manufacturer(string name) =>
            Name = new LengthPrefixedString(name);

        #endregion

        #region Methods (: object)

        public override bool Equals(object obj)
        {
            if (obj is Manufacturer other)
            {
                if (!Equals(Name, other.Name))
                    return false;
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(
                Name?.GetHashCode());

        #endregion
    }
}
