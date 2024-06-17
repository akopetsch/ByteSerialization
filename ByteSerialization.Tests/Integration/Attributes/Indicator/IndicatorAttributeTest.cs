// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using ByteSerialization.Tests.Integration.Attributes.Indicator.TestObjects;
using Xunit.Abstractions;

namespace ByteSerialization.Tests.Integration.Attributes.Indicator
{
    public class IndicatorAttributeTest(ITestOutputHelper testOutputHelper) :
        IntegrationTestBase<IndicatorTestClass>(testOutputHelper)
    {
        #region Properties

        protected override IndicatorTestClass TestObject => new()
        {
            Byte0 = (byte)'A',
            Bar = new IndicatorTestClass2()
            {
                Byte0 = (byte)'B',
                Byte1 = (byte)'C',
            },
        };

        protected override byte[] TestObjectBytes =>
            HexStringConverter.ToByteArray("41 58747261 42 43"); // 'A', 'Xtra', 'B', C'

        protected override Endianness Endianness =>
            Endianness.BigEndian;

        #endregion
    }
}
