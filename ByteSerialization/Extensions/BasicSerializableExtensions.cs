// SPDX-License-Identifier: MIT

using System;

namespace ByteSerialization.Extensions
{
    public static class BasicSerializableExtensions
    {
        public static bool IsBasicSerializable(this Type type)
        {
            Type underlyingNullableType = Nullable.GetUnderlyingType(type);
            if (underlyingNullableType?.IsPrimitiveOrEnum() ?? false)
                return true;
            else if (type.IsEnum)
                return true;
            else if (type.IsPrimitive)
                return true;
            else
                return false;
        }

        public static bool IsBasicSerializableOrArray(this Type type) =>
            type.IsBasicSerializable() || type.IsBasicSerializableArray();

        public static bool IsBasicSerializableArray(this Type type) =>
            type.IsArray && type.GetElementType().IsBasicSerializable();
    }
}
