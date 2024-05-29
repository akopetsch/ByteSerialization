// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes.Offset;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(OffsetComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class OffsetAttribute : ByteSerializationAttribute
    {
        public int Value { get; }

        public OffsetAttribute(int value) => 
            Value = value;
    }
}
