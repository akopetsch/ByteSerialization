// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes.Elements.ElementTypeHelper;
using ByteSerialization.Attributes.Helpers;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(ElementTypeHelperComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class ElementTypeHelperAttribute : ByteSerializationAttribute
    {
        public IElementTypeHelper Helper { get; }
        
        public ElementTypeHelperAttribute(Type helperType) => 
            Helper = helperType.GetElementTypeHelper();
    }
}
