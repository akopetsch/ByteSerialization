// SPDX-License-Identifier: GPL-2.0-only

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
