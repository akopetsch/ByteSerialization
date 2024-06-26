﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;

namespace ByteSerialization.Attributes.Types.TypeHelper
{
    public class TypeHelperComponent : AttributeComponent<TypeHelperAttribute>
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Node.BeforeDeserializing += DefineType;
        }

        private void DefineType()
        {
            RecordComponent record = (Target as PropertyComponent).Record;
            Node.Type = Attribute.Helper.GetPropertyType(record);

            if (!Has<ReferenceComponent>() && Node.Get<ValueComponent>() == null)
                Node.AddValueComponent(Node.Type);
        }
    }
}
