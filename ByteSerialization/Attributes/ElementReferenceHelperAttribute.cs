// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes.Elements.ElementReferenceHelper;
using ByteSerialization.Attributes.Helpers;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(ElementReferenceHelperComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class ElementReferenceHelperAttribute : ByteSerializationAttribute
    {
        public IElementReferenceHelper Helper { get; }

        public ElementReferenceHelperAttribute(Type helperType) => 
            Helper = helperType.GetElementReferenceHelper();
    }
}
