// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes.Padding;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(PaddingComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class PaddingAttribute : ByteSerializationAttribute
    {
        public int Alignment { get; }

        public PaddingAttribute(int alignment) =>
            Alignment = alignment;
    }
}
