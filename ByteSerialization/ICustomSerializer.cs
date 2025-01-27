// SPDX-License-Identifier: MIT

using AKopetsch.IOHelper;

namespace ByteSerialization
{
    public interface ICustomSerializer
    {
        object Deserialize(EndianBinaryReader reader);
        void Serialize(EndianBinaryWriter writer, object value);
    }
}
