// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes.Limiting.Length;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(LengthComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute : BindingAttribute
    {
        public LengthAttribute(int value) : 
            base(value) { }

        public LengthAttribute(string propertyName) : 
            base(propertyName) { }

        public LengthAttribute(Type bindingHelperType) : 
            base(bindingHelperType) { }
    }
}
