// SPDX-License-Identifier: MIT

namespace ByteSerialization.Components.Values.Primitives
{
    public class PrimitiveComponent : ValueComponent
    {
        public override void Serialize() =>
            Writer.Write(Node.Value);

        public override void Deserialize() => 
            Node.Value = Reader.Read(Node.Type);
    }
}
