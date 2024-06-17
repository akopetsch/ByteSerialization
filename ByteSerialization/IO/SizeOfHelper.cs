// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
using System;
using System.Runtime.InteropServices;

namespace ByteSerialization.IO
{
    public static class SizeOfHelper
    {
        public static int GetSizeOf<T>() =>
            GetSizeOf(typeof(T));

        public static int GetSizeOf(Type type)
        {
            if (!type.IsBasicSerializable())
                throw new ArgumentException();

            Type underlyingNullableType = Nullable.GetUnderlyingType(type);
            if (underlyingNullableType?.IsPrimitiveOrEnum() ?? false)
                return GetSizeOf(underlyingNullableType);
            else if (type.IsEnum)
                return GetSizeOf(Enum.GetUnderlyingType(type));
            else if (type.IsPrimitive)
                return Marshal.SizeOf(type);
            else
                throw new InvalidOperationException();
        }

        public static int GetSizeOfObject(object value)
        {
            Type type = value.GetType();
            if (!type.IsBasicSerializable())
                throw new ArgumentException();
            if (type.IsArray)
            {
                int length = (value as Array).Length;
                int elementSize = GetSizeOf(type.GetElementType());
                return length * elementSize;
            }
            else
                return GetSizeOf(type);
        }
    }
}
