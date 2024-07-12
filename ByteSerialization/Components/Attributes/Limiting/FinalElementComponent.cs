// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Limiting;
using ByteSerialization.Components.Values.Composites.Collections;
using System;
using System.Linq;

namespace ByteSerialization.Components.Attributes.Limiting
{
    public class FinalElementComponent :
        AttributeComponent<FinalElementAttribute>, ILimitingComponent
    {
        #region Properties

        private object _expectedFinalElementValue;

        #endregion

        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _expectedFinalElementValue = Attribute.Value ?? Activator.CreateInstance(Attribute.ValueType);
        }

        public bool IsDeserializedUntilEnd(CollectionComponent collection) =>
            collection.Elements.LastOrDefault()?.Value.Equals(_expectedFinalElementValue) ?? false;

        #endregion
    }
}
