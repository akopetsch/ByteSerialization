// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using ByteSerialization.Tests.Integration.TestObjects;

namespace ByteSerialization.Tests.Integration.Composites
{
    public class CompositesTest : IntegrationTestBase<Car>
    {
        #region Properties

        protected override Car TestObject =>
            new("Car1", new Manufacturer("MF"));

        protected override byte[] TestObjectBytes =>
            HexStringConverter.ToByteArray("0443 6172 3100 0000 0902 4d46"); // .Car1.....MF

        protected override Endianness Endianness => 
            Endianness.BigEndian;

        #endregion
    }
}
