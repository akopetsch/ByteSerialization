// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Components.Values.CustomSerializables;
using ByteSerialization.Components.Values.CustomSerializers;
using ByteSerialization.Components.Values.Primitives;
using ByteSerialization.Extensions;
using System;

namespace ByteSerialization.Components.Values
{
    public class ValueComponentFactory
    {
        public ByteSerializerContext Context { get; }

        public ValueComponentFactory(ByteSerializerContext context) =>
            Context = context;

        public Type GetComponentType(Type type)
        {
            if (Context.Serializer.GetCustomSerializer(type) != null)
                return typeof(CustomSerializerComponent);

            if (type.GetUnderlyingTypeOrSelf()?.IsPrimitive ?? false)
                return typeof(PrimitiveComponent);

            if (typeof(ICustomSerializable).IsAssignableFrom(type))
                return typeof(CustomSerializableComponent);

            if (type.IsArray)
                return typeof(ArrayComponent);
            if (type.IsBuiltinList())
                return typeof(ListComponent);
            if (type.IsClass)
                return typeof(RecordComponent);

            throw new ArgumentException();
        }
    }
}
