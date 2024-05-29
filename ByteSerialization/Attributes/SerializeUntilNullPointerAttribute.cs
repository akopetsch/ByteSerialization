// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Attributes.Limiting;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(SerializeUntilNullPointerComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class SerializeUntilNullPointerAttribute : ByteSerializationAttribute
    {

    }
}
