// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.IO;

namespace ByteSerialization.Tests.Integration
{
    public class ComplexIntegrationTest : IntegrationTestBase
    {
        #region Classes

        private class LengthPrefixedString
        {
            [Order(0)]
            public byte Length { get; set; }

            [Order(1), Length(nameof(Length))]
            public char[] CharArray { get; set; }

            public LengthPrefixedString() { }

            public LengthPrefixedString(string s)
            {
                Length = Convert.ToByte(s.Length);
                CharArray = s.ToCharArray();
            }
        }

        private class Manufacturer
        {
            [Order(0)]
            public LengthPrefixedString Name { get; set; }

            public Manufacturer() { }

            public Manufacturer(string name)
            {
                Name = new LengthPrefixedString(name);
            }
        }

        private class Car
        {
            [Order(0)]
            public LengthPrefixedString Name { get; set; }

            [Order(1), Reference]
            public Manufacturer Manufacturer { get; set; }

            public Car() { }

            public Car(string name, Manufacturer manufacturer)
            {
                Name = new LengthPrefixedString(name);
                Manufacturer = manufacturer;
            }
        }

        #endregion

        #region Properties

        protected override object TestObject
        {
            get
            {
                var manufacturer = new Manufacturer("MF");
                return new Car("Car1", manufacturer);
            }
        }

        protected override byte[] TestObjectBytes =>
            HexStringConverter.ToByteArray("0443 6172 3100 0000 0902 4d46"); // .Car1.....MF

        #endregion
    }
}
