// SPDX-License-Identifier: GPL-2.0-only

namespace ByteSerialization.Components
{
    public interface ISerializableComponent
    {
        void Serialize();
        void Deserialize();
    }
}
