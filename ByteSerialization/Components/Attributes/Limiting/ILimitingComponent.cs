// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Values.Composites.Collections;

namespace ByteSerialization.Attributes.Limiting
{
    public interface ILimitingComponent
    {
        bool IsDeserializedUntilEnd(CollectionComponent collection);
    }
}
