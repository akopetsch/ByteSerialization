﻿// Copyright 2024 Alexander Kopetsch
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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