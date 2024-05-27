// Copyright 2024 Alexander Kopetsch
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Types.TypeIdentifier;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(RecordTypeIdentifierComponent))]
    public class RecordTypeIdentifierAttribute : AbstractTypeIdentifierAttribute
    {
        public RecordTypeIdentifierAttribute(object identifier, Type type) :
            base(identifier, type)
        { }
    }
}
