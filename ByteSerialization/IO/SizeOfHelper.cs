// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ByteSerialization.IO
{
    public class SizeOfHelper
    {
        #region Properties

        public Encoding Encoding { get; }

        #endregion

        #region Constructor

        public SizeOfHelper() : 
            this(Encoding.UTF8)
        { }

        public SizeOfHelper(Encoding encoding) =>
            Encoding = encoding;

        #endregion

        #region Methods

        public int GetSizeOfType<T>() =>
            GetSizeOfType(typeof(T));

        public int GetSizeOfType(Type type)
        {
            if (!type.IsBasicSerializable())
                throw new ArgumentException();

            Type underlyingNullableType = Nullable.GetUnderlyingType(type);
            if (underlyingNullableType?.IsPrimitiveOrEnum() ?? false)
                return GetSizeOfType(underlyingNullableType);
            else if (type.IsEnum)
                return GetSizeOfType(Enum.GetUnderlyingType(type));
            else if (type.IsPrimitive)
            {
                if (type == typeof(char))
                {
                    if (Encoding.IsSingleByte)
                        return 1;
                    else
                        throw new ArgumentException();
                }
                else
                    return Marshal.SizeOf(type);
            }
            else
                throw new InvalidOperationException();
        }

        public int GetSizeOfObject(object value)
        {
            Type type = value.GetType();
            if (!type.IsBasicSerializableOrArray())
                throw new ArgumentException();
            if (type.IsArray)
            {
                int size = 0;
                var array = (value as Array);
                int length = array.Length;
                for (int i = 0; i < length; i++)
                {
                    object element = array.GetValue(i);
                    size += GetSizeOfObject(element);
                }
                return size;
            }
            else if (type == typeof(char))
                return Encoding.GetByteCount(new char[] { (char)value });
            else
                return GetSizeOfType(type);
        }

        #endregion
    }
}
