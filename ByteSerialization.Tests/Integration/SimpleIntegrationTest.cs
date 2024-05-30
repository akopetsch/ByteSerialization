// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.IO;
using Xunit;

namespace ByteSerialization.Tests.Integration
{
    public class SimpleIntegrationTest
    {
        public class LengthPrefixedString
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

        public class Manufacturer
        {
            [Order(0)]
            public LengthPrefixedString Name { get; set; }

            public Manufacturer() { }

            public Manufacturer(string name)
            {
                Name = new LengthPrefixedString(name);
            }
        }

        public class Car
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

        [Fact]
        public void Test()
        {
            // setup
            var manufacturer = new Manufacturer("MF");
            var car = new Car("Car1", manufacturer);
            
            // serialize
            using var ms = new MemoryStream();
            new ByteSerializer().Serialize(ms, car, Endianness.BigEndian);

            // compare
            byte[] expected = HexStringConverter.ToByteArray("0443 6172 3100 0000 0902 4d46"); // .Car1.....MF
            byte[] actual = ms.ToArray();
            Assert.True(expected.SequenceEqual(actual));
        }
    }
}
