// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes.Elements.ElementReference;
using ByteSerialization.Components.Attributes.Reference;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(ElementReferenceComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class ElementReferenceAttribute : ByteSerializationAttribute
    {
        public ReferenceConfiguration Configuration { get; } = new ReferenceConfiguration();

        public ElementReferenceAttribute() { }
        public ElementReferenceAttribute(ReferenceHandling handling) => Configuration.Handling = handling;
        public ElementReferenceAttribute(ReferenceConfiguration configuration) => Configuration = configuration;

        // TODO: code duplication in ReferenceAttribute
    }
}
