// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;

namespace ByteSerialization.Components.Attributes.Conditional
{
    public interface IConditionalComponent
    {
        bool IsSerialized(Node node);
    }
}
