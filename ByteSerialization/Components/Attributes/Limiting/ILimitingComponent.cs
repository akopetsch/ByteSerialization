// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Values.Composites.Collections;

namespace ByteSerialization.Attributes.Limiting
{
    public interface ILimitingComponent
    {
        bool IsDeserializedUntilEnd(CollectionComponent collection);
    }
}
