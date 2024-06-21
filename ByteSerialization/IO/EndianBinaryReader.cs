// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
using ByteSerialization.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace ByteSerialization.IO
{
    // TODO: make this class more like BinaryReader

    public class EndianBinaryReader : IDisposable
    {
        #region Fields

        private readonly BinaryReader _reader;
        private readonly Dictionary<Type, ReadFunc> _primitiveReadFuncs =
            new Dictionary<Type, ReadFunc>();

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

        #region Constructor / Dispose

        public EndianBinaryReader(Stream stream, Endianness endianness)
        {
            BaseStream = stream;
            Endianness = endianness;

            _reader = new BinaryReader(stream);
            InitPrimitiveReadFuncs();
        }

        #endregion

        #region Methods

        #region Methods (: IDisposable)

        public void Dispose() => 
            _reader.Dispose();

        #endregion

        #region Methods (initialization)

        private void InitPrimitiveReadFuncs()
        {
            _primitiveReadFuncs.Add(typeof(bool), () => ReadBoolean());
            _primitiveReadFuncs.Add(typeof(byte), () => ReadByte());
            _primitiveReadFuncs.Add(typeof(sbyte), () => ReadSByte());
            _primitiveReadFuncs.Add(typeof(char), () => ReadChar());
            _primitiveReadFuncs.Add(typeof(short), () => ReadInt16());
            _primitiveReadFuncs.Add(typeof(ushort), () => ReadUInt16());
            _primitiveReadFuncs.Add(typeof(int), () => ReadInt32());
            _primitiveReadFuncs.Add(typeof(uint), () => ReadUInt32());
            _primitiveReadFuncs.Add(typeof(long), () => ReadInt64());
            _primitiveReadFuncs.Add(typeof(ulong), () => ReadUInt64());
            _primitiveReadFuncs.Add(typeof(float), () => ReadSingle());
            _primitiveReadFuncs.Add(typeof(double), () => ReadDouble());
        }

        #endregion

        #region Methods (Read...; primitive types)

        public bool ReadBoolean() =>
            _reader.ReadBoolean();

        public byte ReadByte() =>
            _reader.ReadByte();

        public sbyte ReadSByte() =>
            _reader.ReadSByte();

        public char ReadChar() =>
            _reader.ReadChar(); // no byte-swapping because of UTF-8

        public short ReadInt16() =>
            BytesSwapper.SwapIf(_reader.ReadInt16(), IsBigEndian);

        public ushort ReadUInt16() =>
            BytesSwapper.SwapIf(_reader.ReadUInt16(), IsBigEndian);

        public int ReadInt32() =>
            BytesSwapper.SwapIf(_reader.ReadInt32(), IsBigEndian);

        public uint ReadUInt32() =>
            BytesSwapper.SwapIf(_reader.ReadUInt32(), IsBigEndian);

        public long ReadInt64() =>
            BytesSwapper.SwapIf(_reader.ReadInt64(), IsBigEndian);

        public ulong ReadUInt64() =>
            BytesSwapper.SwapIf(_reader.ReadUInt64(), IsBigEndian);

        public float ReadSingle() =>
            BitConverter.Int32BitsToSingle(BytesSwapper.SwapIf(_reader.ReadInt32(), IsBigEndian));
        // HACK: regular ``BytesSwapper.SwapIf(_reader.ReadSingle(), IsBigEndian);`` is not deterministic across .NET 8 and Unity 2022.3.30f1 (LTS)

        public double ReadDouble() =>
            BitConverter.Int64BitsToDouble(BytesSwapper.SwapIf(_reader.ReadInt64(), IsBigEndian));
        // HACK: regular ``BytesSwapper.SwapIf(_reader.ReadDouble(), IsBigEndian);`` is probably also not deterministic across .NET 8 and Unity 2022.3.30f1 (LTS)

        #endregion

        #region Methods (Read(...); by type)

        public T Read<T>() =>
            (T)Read(typeof(T));

        public object Read(Type type)
        {
            if (!type.IsBasicSerializable())
                throw new ArgumentException();

            Type underlyingNullableType = Nullable.GetUnderlyingType(type);
            if (underlyingNullableType?.IsPrimitiveOrEnum() ?? false)
            {
                object nullableValue = Read(underlyingNullableType);
                return nullableValue;
            }
            else if (type.IsEnum)
            {
                Type underlyingEnumType = Enum.GetUnderlyingType(type);
                object underlyingValue = Read(underlyingEnumType);
                object enumValue = Enum.ToObject(type, underlyingValue);
                return enumValue;
            }
            else if (type.IsPrimitive)
            {
                object primitiveValue = _primitiveReadFuncs[type].Invoke();
                return primitiveValue;
            }
            else
                throw new InvalidOperationException();
        }

        public T[] Read<T>(int count) =>
            (T[])Read(typeof(T), count);

        public Array Read(Type elementType, int count)
        {
            if (!elementType.IsBasicSerializable())
                throw new ArgumentException();

            if (elementType == typeof(byte))
                return _reader.ReadBytes(count);

            Array array = Array.CreateInstance(elementType, count);
            for (int i = 0; i < count; i++)
                array.SetValue(Read(elementType), i);
            return array;
        }

        #endregion

        #region Methods (Peek(...))

        public T Peek<T>() => 
            (T)Peek(typeof(T));

        public object Peek(Type t) =>
            AtPosition(BaseStream.Position, r => r.Read(t));

        public T[] Peek<T>(int count) =>
            (T[])Peek(typeof(T), count);

        public Array Peek(Type elementType, int count) =>
            AtPosition(BaseStream.Position, r => r.Read(elementType, count));

        #endregion

        #region Methods (TryRead(...))

        public bool TryRead(object value)
        {
            long oldPosition = BaseStream.Position;
            Type type = value.GetType();
            if (type.IsBasicSerializable())
            {
                object readValue = Read(type);
                if (readValue.Equals(value))
                    return true;
                else
                {
                    BaseStream.Position = oldPosition;
                    return false;
                }
            }
            else if (type.IsBasicSerializableArray())
            {
                var array = (Array)value;
                Array readArray = Read(type, array.Length);
                if (ArrayComparer.AreEqual(readArray, array))
                    return true;
                else
                {
                    BaseStream.Position = oldPosition;
                    return false;
                }
            }
            else
                throw new ArgumentException();
        }

        #endregion

        #region Methods (AtPosition(...))

        public T AtPosition<T>(long position, Func<EndianBinaryReader, T> action)
        {
            long oldPosition = BaseStream.Position;
            BaseStream.Position = position;
            T result = action.Invoke(this);
            BaseStream.Position = oldPosition;
            return result;
        }

        #endregion

        #endregion
    }
}
