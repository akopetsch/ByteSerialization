// SPDX-License-Identifier: GPL-2.0-only

namespace ByteSerialization.Components.Values.Customs
{
    public interface ICustomSerializable
    {
        void Serialize(CustomComponent customComponent);
        void Deserialize(CustomComponent customComponent);
    }
}
