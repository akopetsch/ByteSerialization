// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace ByteSerialization.Tests.Integration.TestObjects
{
    public class Car
    {
        #region Properties

        [Order(0)]
        public LengthPrefixedString Name { get; set; }

        [Order(1), Reference]
        public Manufacturer Manufacturer { get; set; }

        #endregion

        #region Constructor

        public Car()
        { }

        public Car(string name, Manufacturer manufacturer)
        {
            Name = new LengthPrefixedString(name);
            Manufacturer = manufacturer;
        }

        #endregion

        #region Methods (: object)

        public override bool Equals(object obj)
        {
            if (obj is Car other)
            {
                if (!Equals(Name, other.Name))
                    return false;
                if (!Equals(Manufacturer, other.Manufacturer))
                    return false;
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(
                Name?.GetHashCode(), 
                Manufacturer?.GetHashCode());

        #endregion
    }
}
