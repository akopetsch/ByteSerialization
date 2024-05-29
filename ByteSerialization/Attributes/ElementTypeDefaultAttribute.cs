// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes.Elements.ElementTypeDefault;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(ElementTypeDefaultComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class ElementTypeDefaultAttribute : ByteSerializationAttribute
    {
        public Type Type { get; }

        public ElementTypeDefaultAttribute(Type type) => 
            Type = type;
    }
}
