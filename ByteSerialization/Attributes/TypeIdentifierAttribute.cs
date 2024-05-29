// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Attributes.Types;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(TypeIdentifierComponent))]
    public class TypeIdentifierAttribute : AbstractTypeIdentifierAttribute
    {
        public TypeIdentifierAttribute(object identifier, Type type) : 
            base(identifier, type)
        { }
    }
}
