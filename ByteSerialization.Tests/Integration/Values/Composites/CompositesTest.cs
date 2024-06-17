// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using ByteSerialization.Tests.Integration.Values.Composites.TestObjects;
using Xunit;
using Xunit.Abstractions;

namespace ByteSerialization.Tests.Integration.Values.Composites
{
    public class CompositesTest(ITestOutputHelper testOutputHelper) : 
        IntegrationTestBase(testOutputHelper)
    {
        #region Fields

        private static readonly Car TestObject =
            new("Car1", new Manufacturer("MF"));

        private static readonly byte[] TestBytes =
            HexStringConverter.ToByteArray("0443 6172 3100 0000 0902 4d46"); // .Car1.....MF

        #endregion

        #region Methods

        [Fact]
        public void Test_Deserialization() =>
            AssertDeserializedObject(
                TestObject, TestBytes, Endianness.BigEndian);

        [Fact]
        public void Test_Serialization() =>
            AssertSerializedObject(
                TestBytes, TestObject, Endianness.BigEndian);


        #endregion
    }
}
