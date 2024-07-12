// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Attributes.Limiting;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(FinalElementComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class FinalElementAttribute : ByteSerializationAttribute
    {
        #region Properties

        public object Value { get; }
        public Type ValueType { get; }

        #endregion

        #region Constructor

        public FinalElementAttribute(object value) =>
            Value = value;

        public FinalElementAttribute(Type valueType) =>
            ValueType = valueType;

        #endregion
    }
}
