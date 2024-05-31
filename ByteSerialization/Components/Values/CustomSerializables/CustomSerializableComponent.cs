// SPDX-License-Identifier: MIT

using System;

namespace ByteSerialization.Components.Values.CustomSerializables
{
    public class CustomSerializableComponent : ValueComponent
    {
        private ICustomSerializable CustomSerializable { get; set; }

        public override void Serialize()
        {
            CustomSerializable = Node.Value as ICustomSerializable;
            CustomSerializable.Serialize(Writer);
        }

        public override void Deserialize()
        {
            Node.Value = Activator.CreateInstance(Node.Type);
            CustomSerializable = Node.Value as ICustomSerializable;
            CustomSerializable.Deserialize(Reader);
        }
    }
}
