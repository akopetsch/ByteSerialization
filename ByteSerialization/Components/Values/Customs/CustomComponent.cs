// SPDX-License-Identifier: MIT

using System;

namespace ByteSerialization.Components.Values.Customs
{
    public class CustomComponent : ValueComponent
    {
        private ICustomSerializable CustomSerializable { get; set; }

        public override void Serialize()
        {
            CustomSerializable = Node.Value as ICustomSerializable;
            CustomSerializable.Serialize(this);
        }

        public override void Deserialize()
        {
            Node.Value = Activator.CreateInstance(Node.Type);
            CustomSerializable = Node.Value as ICustomSerializable;
            CustomSerializable.Deserialize(this);
        }
    }
}
