// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Components.Values.Customs;
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

            if (type.IsValueType)
            {
                if (Nullable.GetUnderlyingType(type)?.IsPrimitive == true)
                    return typeof(NullablePrimitiveComponent);
                if (type.IsPrimitive)
                    return typeof(PrimitiveComponent);
                if (type.IsEnum)
                    return typeof(EnumComponent);
            }

            if (typeof(ICustomSerializable).IsAssignableFrom(type))
                return typeof(CustomComponent);

            if (type.IsArray)
                return typeof(ArrayComponent);
            if (type.IsBuiltinList())
                return typeof(ListComponent);
            if (type.IsClass)
                return typeof(RecordComponent);

            throw new InvalidOperationException();
        }
    }
}
