// Copyright 2024 Alexander Kopetsch
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
