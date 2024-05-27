// Copyright 2024 Alexander Kopetsch
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
