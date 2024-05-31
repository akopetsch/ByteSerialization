// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using System;
using System.Linq;

namespace ByteSerialization.Components.Values
{
    public abstract class ValueComponent : Component, ISerializableComponent
    {
        #region Properties

        public override string GetDebuggerDisplay() => $"{Type.GetFriendlyName()}";

        #endregion

        #region Methods

        public abstract void Serialize();
        public abstract void Deserialize();

        #endregion
    }

    public static class ValueComponentExtensions
    {
        public static TValue GetAncestorValue<TValue>(this Component component) => 
            (TValue)component.GetAncestorValueComponent<TValue>().Node.Value;

        public static ValueComponent GetAncestorValueComponent<TValue>(this Component component) =>
            component.GetAncestors<ValueComponent>(c => typeof(TValue).IsAssignableFrom(c.Node.Type)).FirstOrDefault();

        public static ValueComponent AddValueComponent(this Node node, Type type)
        {
            Type tvc = node.Context.ValueComponentFactory.GetComponentType(type);
            return (ValueComponent)node.Add(tvc);
        }
    }
}
