// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes.Conditional;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(IndicatorComponent))]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class IndicatorAttribute : ByteSerializationAttribute
    {
        public string Value { get; }

        public IndicatorAttribute(string value) =>
            Value = value;
    }
}
