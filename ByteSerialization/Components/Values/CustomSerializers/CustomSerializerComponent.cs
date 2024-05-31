// SPDX-License-Identifier: MIT

namespace ByteSerialization.Components.Values.CustomSerializers
{
    public class CustomSerializerComponent : ValueComponent
    {
        public override void Serialize()
        {
            ICustomSerializer customSerializer = GetCustomSerializer();
            customSerializer.Serialize(Writer, Value);
        }

        public override void Deserialize()
        {
            ICustomSerializer customSerializer = GetCustomSerializer();
            Node.Value = customSerializer.Deserialize(Reader);
        }

        private ICustomSerializer GetCustomSerializer() =>
            Context.Serializer.GetCustomSerializer(Type);
    }
}
