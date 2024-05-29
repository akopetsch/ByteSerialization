// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;

namespace ByteSerialization.Components.Attributes.Conditional
{
    public interface IConditionalComponent
    {
        bool IsSerialized(Node node);
    }
}
