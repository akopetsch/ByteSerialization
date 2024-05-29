// SPDX-License-Identifier: MIT

namespace ByteSerialization.Components.Values.Customs
{
    public interface ICustomSerializable
    {
        void Serialize(CustomComponent customComponent);
        void Deserialize(CustomComponent customComponent);
    }
}
