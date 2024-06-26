﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Values.Composites.Records;

namespace ByteSerialization.Attributes.Types.TypeIdentifier
{
    public class RecordTypeIdentifierComponent : AbstractTypeIdentifierComponent<RecordTypeIdentifierAttribute>
    {
        // TODO: really inherit AbstractTypeIdentifierComponent?
        // does not affect property type but parent record type (also see TypeIdentifierComponent)

        #region Properties

        private PropertyComponent Property { get; set; }
        private RecordComponent Record => Property?.Record;

        #endregion

        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Property = Get<PropertyComponent>();
            Property.Node.AfterDeserializing += AfterDeserializingProperty;
        }

        private void AfterDeserializingProperty() =>
            Record.Node.Type = TypesByIdentifier[Property.Value];

        #endregion
    }
}
