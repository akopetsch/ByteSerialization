// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes.Elements.ElementTypeIdentifier;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(ElementTypeIdentifierComponent))]
    public class ElementTypeIdentifierAttribute : AbstractTypeIdentifierAttribute
    {
        public ElementTypeIdentifierAttribute(object identifier, Type type) :
            base(identifier, type)
        { }
    }
}
