// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Attributes.Types;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(TypeDefaultComponent))]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TypeDefaultAttribute : ByteSerializationAttribute
    {
        public Type Type { get; }

        public TypeDefaultAttribute(Type type) =>
            Type = type;
    }
}
