// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Attributes.Limiting;
using ByteSerialization.Components.Values.Composites.Collections;
using System;

namespace ByteSerialization.Attributes.Limiting.SerializeUntil
{
    public class SerializeUntilComponent : 
        AbstractSerializeUntilComponent<SerializeUntilAttribute>
    {
        #region Methods
        
        protected override void AfterDeserializing() =>
            Reader.Read(Attribute.Value.GetType());

        protected override void AfterSerializing() =>
            Writer.Write(Attribute.Value);

        public override bool IsDeserializedUntilEnd(CollectionComponent collection)
        {
            object delimiter = Attribute.Value;
            if (delimiter == null || !delimiter.GetType().IsPrimitive)
                throw new InvalidOperationException();
            // TODO: implement validation algorithm somewhere else
            return Reader.Peek(delimiter.GetType()).Equals(delimiter);
        }

        #endregion
    }
}
