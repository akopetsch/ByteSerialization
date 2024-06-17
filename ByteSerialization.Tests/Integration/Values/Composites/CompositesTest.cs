// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using ByteSerialization.Tests.Integration.Values.Composites.TestObjects;
using Xunit.Abstractions;

namespace ByteSerialization.Tests.Integration.Values.Composites
{
    public class CompositesTest(ITestOutputHelper testOutputHelper) : 
        IntegrationTestBase<Car>(testOutputHelper)
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
