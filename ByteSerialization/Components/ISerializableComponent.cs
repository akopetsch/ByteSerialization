// SPDX-License-Identifier: MIT

namespace ByteSerialization.Components
{
    public interface ISerializableComponent
    {
        void Serialize();
        void Deserialize();
    }
}
