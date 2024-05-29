// SPDX-License-Identifier: MIT

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
