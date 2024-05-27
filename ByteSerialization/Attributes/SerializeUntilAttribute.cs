﻿// Copyright 2024 Alexander Kopetsch
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Limiting.SerializeUntil;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(SerializeUntilComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class SerializeUntilAttribute : ByteSerializationAttribute
    {
        public object Value { get; }

        public SerializeUntilAttribute(object value) =>
            Value = value;
    }
}