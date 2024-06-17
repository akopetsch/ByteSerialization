// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace ByteSerialization.IO
{
    public class EndianBinaryWriter : IDisposable
    {
        #region Fields

        private readonly BinaryWriter _writer;
        private readonly Dictionary<Type, WriteFunc> _primitiveWriteFuncs = 
            new Dictionary<Type, WriteFunc>();

        #endregion

        #region Properties (input)

        public Stream BaseStream { get; set; }
        public Endianness Endianness { get; set; }

        #endregion

        #region Properties (dynamic)

        public bool IsBigEndian => 
            Endianness == Endianness.BigEndian;
        public bool IsLittleEndian => 
            Endianness == Endianness.LittleEndian;

        #endregion

        #region Constructor

        public EndianBinaryWriter(Stream stream, Endianness endianness)
        {
            BaseStream = stream;
            Endianness = endianness;

            _writer = new BinaryWriter(stream);
            InitPrimitiveWriteFuncs();
        }

        #endregion

        #region Methods (: IDisposable)

        public void Dispose() => 
            _writer.Dispose();

        #endregion

        #region Methods

        #region Methods (initialization)

        private void InitPrimitiveWriteFuncs()
        {
            _primitiveWriteFuncs.Add(typeof(bool), x => Write((bool)x));
            _primitiveWriteFuncs.Add(typeof(byte), x => Write((byte)x));
            _primitiveWriteFuncs.Add(typeof(sbyte), x => Write((sbyte)x));
            _primitiveWriteFuncs.Add(typeof(short), x => Write((short)x));
            _primitiveWriteFuncs.Add(typeof(ushort), x => Write((ushort)x));
            _primitiveWriteFuncs.Add(typeof(int), x => Write((int)x));
            _primitiveWriteFuncs.Add(typeof(uint), x => Write((uint)x));
            _primitiveWriteFuncs.Add(typeof(long), x => Write((long)x));
            _primitiveWriteFuncs.Add(typeof(ulong), x => Write((ulong)x));
            _primitiveWriteFuncs.Add(typeof(float), x => Write((float)x));
            _primitiveWriteFuncs.Add(typeof(double), x => Write((double)x));
            _primitiveWriteFuncs.Add(typeof(decimal), x => Write((decimal)x));
            _primitiveWriteFuncs.Add(typeof(char), x => Write((char)x));
            _primitiveWriteFuncs.Add(typeof(string), x => Write((string)x));
        }

        #endregion

        #region Methods (Write(...); primitive types)

        public void Write(bool value) =>
            _writer.Write(value);

        public void Write(byte value) =>
            _writer.Write(value);

        public void Write(sbyte value) =>
            _writer.Write(value);

        public void Write(char value) =>
            _writer.Write(value); // no byte-swapping because of UTF-8

        public void Write(short value) =>
            _writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));

        public void Write(ushort value) =>
            _writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));

        public void Write(int value) =>
            _writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));

        public void Write(uint value) =>
            _writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));

        public void Write(long value) =>
            _writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));

        public void Write(ulong value) =>
            _writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));

        public void Write(float value) =>
            _writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));

        public void Write(double value) =>
            _writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));

        #endregion

        #region Methods (Write(...); other types)

        public void Write(decimal value) =>
            throw new NotImplementedException();

        public void Write(string value) =>
            _writer.Write(value);

        #endregion

        #region Write (Write(...); object values)

        public void Write(object value)
        {
            Type type = value.GetType();
            if (!type.IsBasicSerializableOrArray())
                throw new ArgumentException();

            if (type.IsArray)
            {
                if (value is byte[] byteArray)
                    _writer.Write(byteArray);
                else
                {
                    var array = value as Array;
                    for (int i = 0; i < array.Length; i++)
                        Write(array.GetValue(i));
                }
            }
            else
            {
                Type underlyingNullableType = Nullable.GetUnderlyingType(type);
                if (underlyingNullableType?.IsPrimitiveOrEnum() ?? false)
                {
                    object underlyingValue = Convert.ChangeType(value, underlyingNullableType);
                    Write(underlyingValue);
                    return;
                }
                else if (type.IsEnum)
                {
                    Type underlyingEnumType = Enum.GetUnderlyingType(type);
                    object underlyingValue = Convert.ChangeType(value, underlyingEnumType);
                    Write(underlyingValue);
                    return;
                }
                else if (type.IsPrimitive)
                {
                    _primitiveWriteFuncs[value.GetType()].Invoke(value);
                    return;
                }
                else
                    throw new InvalidOperationException();
            }
        }

        #endregion

        #region Methods (AtPosition(...))

        public void AtPosition(long position, Action<EndianBinaryWriter> action)
        {
            long oldPosition = BaseStream.Position;
            BaseStream.Position = position;
            action.Invoke(this);
            BaseStream.Position = oldPosition;
        }

        #endregion

        #endregion
    }
}
