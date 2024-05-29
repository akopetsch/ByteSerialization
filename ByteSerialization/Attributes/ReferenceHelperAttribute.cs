// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Attributes.Reference;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(ReferenceHelperComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class ReferenceHelperAttribute : ByteSerializationAttribute
    {
        public IReferenceHelper Helper { get; }

        public ReferenceHelperAttribute(Type helperType) =>
            Helper = helperType.GetReferenceHelper();
    }
}
