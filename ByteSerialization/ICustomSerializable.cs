// SPDX-License-Identifier: MIT

using AKopetsch.IOHelper;

namespace ByteSerialization
{
    public interface ICustomSerializable
    {
        void Serialize(EndianBinaryWriter writer);
        void Deserialize(EndianBinaryReader reader);
    }
}
