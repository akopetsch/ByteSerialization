// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes.Limiting.Sizeof;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(SizeofComponent))]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class SizeofAttribute : BindingAttribute
    {
        public int Multiplier { get; set; } = 1;
        
        public SizeofAttribute(int value) : 
            base(value) { }

        public SizeofAttribute(string propertyName) : 
            base(propertyName) { }

        public SizeofAttribute(Type bindingHelperType) : 
            base(bindingHelperType) { }
    }
}
