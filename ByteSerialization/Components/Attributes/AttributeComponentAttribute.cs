// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeComponentAttribute : ByteSerializationAttribute
    {
        public Type ComponentType { get; set; }

        public AttributeComponentAttribute(Type componentType)
        {
            if (!(ComponentType = componentType).Is<AttributeComponent>())
                throw new InvalidOperationException();
        }
    }
}
