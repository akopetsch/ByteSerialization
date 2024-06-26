﻿// SPDX-License-Identifier: MIT

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
