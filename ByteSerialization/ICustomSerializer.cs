// SPDX-License-Identifier: MIT

using ByteSerialization.IO;

namespace ByteSerialization
{
    public interface ICustomSerializer
    {
        object Deserialize(EndianBinaryReader reader);
        void Serialize(EndianBinaryWriter writer, object value);
    }
}
