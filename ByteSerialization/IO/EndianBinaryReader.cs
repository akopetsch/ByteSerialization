// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ByteSerialization.IO
{
    // TODO: make this class more like BinaryReader

    public class EndianBinaryReader : IDisposable
    {
        #region Fields

        private readonly BinaryReader _reader;
        private readonly Dictionary<Type, ReadFunc> _readFuncs =
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
            InitFuncs();
        }

        #endregion

        #region Methods

        #region Methods (: IDisposable)

        public void Dispose() => 
            _reader.Dispose();

        #endregion

        #region Methods (initialization)

        private void InitFuncs()
        {
            _readFuncs.Add(typeof(bool), () => ReadBoolean());
            _readFuncs.Add(typeof(byte), () => ReadByte());
            _readFuncs.Add(typeof(sbyte), () => ReadSByte());
            _readFuncs.Add(typeof(short), () => ReadInt16());
            _readFuncs.Add(typeof(ushort), () => ReadUInt16());
            _readFuncs.Add(typeof(int), () => ReadInt32());
            _readFuncs.Add(typeof(uint), () => ReadUInt32());
            _readFuncs.Add(typeof(long), () => ReadInt64());
            _readFuncs.Add(typeof(ulong), () => ReadUInt64());
            _readFuncs.Add(typeof(float), () => ReadSingle());
            _readFuncs.Add(typeof(double), () => ReadDouble());
            _readFuncs.Add(typeof(decimal), () => ReadDecimal());
            _readFuncs.Add(typeof(char), () => ReadChar());
            _readFuncs.Add(typeof(string), () => ReadString());
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
            BitConverter.ToSingle(ReadBytes(sizeof(float)).ReverseIf(IsBigEndian).ToArray(), 0);

        public double ReadDouble() =>
            BitConverter.ToDouble(ReadBytes(sizeof(double)).ReverseIf(IsBigEndian).ToArray(), 0);

        private object ReadPrimitiveType(Type t) =>
            _readFuncs[t].Invoke();

        #endregion

        #region Methods (Read...; other value types)

        public decimal ReadDecimal() =>
            ReadBytes(sizeof(decimal)).ReverseIf(IsBigEndian).ToArray().ToDecimal(0);

        public string ReadString() =>
            throw new NotImplementedException();

        #endregion

        #region Methods (Read...; array types)

        public byte[] ReadBytes(int count) =>
            _reader.ReadBytes(count);

        public char[] ReadChars(int count) =>
            _reader.ReadChars(count);

        #endregion

        #region Methods (Read(...); by type)

        public T Read<T>() =>
            (T)Read(typeof(T));

        public object Read(Type t)
        {
            if (t.IsValueType)
            {
                Type underlyingNullableType = Nullable.GetUnderlyingType(t);
                if (underlyingNullableType?.IsPrimitive == true)
                    return Read(underlyingNullableType);
                else if (t.IsEnum)
                    return Enum.ToObject(t, ReadPrimitiveType(Enum.GetUnderlyingType(t)));
                else if (t.IsPrimitive)
                    return ReadPrimitiveType(t);
            }
            throw new ArgumentException();
        }

        #endregion

        #region Methods (Peek(...))

        public T Peek<T>() => (T)Peek(typeof(T));

        public object Peek(Type t) =>
            AtPosition(BaseStream.Position, r => r.Read(t));

        public byte[] PeekBytes(int count) =>
            AtPosition(BaseStream.Position, r => r.ReadBytes(count));

        public char[] PeekChars(int count) =>
            AtPosition(BaseStream.Position, r => r.ReadChars(count));

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
