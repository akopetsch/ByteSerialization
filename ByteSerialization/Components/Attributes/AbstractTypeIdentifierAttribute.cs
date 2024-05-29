// SPDX-License-Identifier: GPL-2.0-only

using System;
using System.Diagnostics;

namespace ByteSerialization.Attributes
{
    // TODO: consider different folder

    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class AbstractTypeIdentifierAttribute : ByteSerializationAttribute
    {
        private string DebuggerDisplay =>
            $"{Identifier} -> {Type.Name}";

        public object Identifier { get; }
        public Type Type { get; }

        protected AbstractTypeIdentifierAttribute(object identifier, Type type)
        {
            Identifier = identifier;
            Type = type;
        }
    }
}
