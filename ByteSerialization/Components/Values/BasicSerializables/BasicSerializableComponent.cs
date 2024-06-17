// SPDX-License-Identifier: MIT

namespace ByteSerialization.Components.Values.BasicSerializables
{
    public class BasicSerializableComponent : ValueComponent
    {
        public override void Serialize() =>
            Writer.Write(Node.Value);

        public override void Deserialize() => 
            Node.Value = Reader.Read(Node.Type);
    }
}
