// SPDX-License-Identifier: MIT

using ByteSerialization.IO;

namespace ByteSerialization
{
    public interface ICustomSerializable
    {
        void Serialize(EndianBinaryWriter writer);
        void Deserialize(EndianBinaryReader reader);
    }
}
