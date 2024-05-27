﻿// Copyright 2024 Alexander Kopetsch
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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