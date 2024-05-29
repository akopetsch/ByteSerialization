// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Attributes.Types.TypeHelper;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(TypeHelperComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class TypeHelperAttribute : ByteSerializationAttribute
    {
        public ITypeHelper Helper { get; }
        public Type HelperType { get; }

        public TypeHelperAttribute(Type helperType) =>
            Helper = helperType.GetTypeHelper();
    }
}
