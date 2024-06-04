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

        private BinaryReader reader;
        private Dictionary<Type, ReadFunc> funcs =
            new Dictionary<Type, ReadFunc>();

        #endregion

        #region Properties

        public Stream BaseStream { get; set; }
        public Endianness Endianness { get; set; }
        public ulong Count { get; private set; } = 0;

        public bool IsBigEndian => Endianness == Endianness.BigEndian;
        public bool IsLittleEndian => Endianness == Endianness.LittleEndian;

        #endregion

        #region Constructor / Dispose

        public EndianBinaryReader(Stream stream, Endianness endianness)
        {
            BaseStream = stream;
            Endianness = endianness;

            reader = new BinaryReader(stream);
            InitFuncs();
        }

        private void InitFuncs()
        {
            funcs.Add(typeof(bool), () => ReadBoolean());
            funcs.Add(typeof(byte), () => ReadByte());
            funcs.Add(typeof(sbyte), () => ReadSByte());
            funcs.Add(typeof(short), () => ReadInt16());
            funcs.Add(typeof(ushort), () => ReadUInt16());
            funcs.Add(typeof(int), () => ReadInt32());
            funcs.Add(typeof(uint), () => ReadUInt32());
            funcs.Add(typeof(long), () => ReadInt64());
            funcs.Add(typeof(ulong), () => ReadUInt64());
            funcs.Add(typeof(float), () => ReadSingle());
            funcs.Add(typeof(double), () => ReadDouble());
            funcs.Add(typeof(decimal), () => ReadDecimal());
            funcs.Add(typeof(char), () => ReadChar());
            funcs.Add(typeof(string), () => ReadString());
        }

        public void Dispose() => 
            reader.Dispose();

        #endregion

        #region Methods

        public bool ReadBoolean() => reader.ReadBoolean();
        public byte ReadByte() => reader.ReadByte();
        public sbyte ReadSByte() => reader.ReadSByte();
        public char ReadChar() => reader.ReadChar();
        public short ReadInt16() => BytesSwapper.SwapIf(reader.ReadInt16(), IsBigEndian);
        public ushort ReadUInt16() => BytesSwapper.SwapIf(reader.ReadUInt16(), IsBigEndian);
        public int ReadInt32() => BytesSwapper.SwapIf(reader.ReadInt32(), IsBigEndian);
        public uint ReadUInt32() => BytesSwapper.SwapIf(reader.ReadUInt32(), IsBigEndian);
        public long ReadInt64() => BytesSwapper.SwapIf(reader.ReadInt64(), IsBigEndian);
        public ulong ReadUInt64() => BytesSwapper.SwapIf(reader.ReadUInt64(), IsBigEndian);
        public float ReadSingle() => BitConverter.ToSingle(ReadBytes(sizeof(float)).ReverseIf(IsBigEndian).ToArray(), 0);
        public double ReadDouble() => BitConverter.ToDouble(ReadBytes(sizeof(double)).ReverseIf(IsBigEndian).ToArray(), 0);
        public decimal ReadDecimal() => ReadBytes(sizeof(decimal)).ReverseIf(IsBigEndian).ToArray().ToDecimal(0);
        public byte[] ReadBytes(int count) => reader.ReadBytes(count);
        public char[] ReadChars(int count) => reader.ReadChars(count);
        public string ReadString() => throw new NotImplementedException();

        public int[] ReadInt32(int count) => Read(ReadInt32, count);

        private T[] Read<T>(Func<T> func, int count)
        {
            var array = new T[count];
            for (int i = 0; i < count; i++)
                array[i] = func.Invoke();
            return array;
        }

        public ReadFunc GetFunc(Type t) => funcs[t];
        public object Read(Type t) => funcs[t].Invoke();
        public T Read<T>() => (T)Read(typeof(T));

        public T Peek<T>() => (T)Peek(typeof(T));
        public object Peek(Type t) => AtPosition(BaseStream.Position, r => r.Read(t));
        public byte[] PeekBytes(int count) => AtPosition(BaseStream.Position, r => r.ReadBytes(count));
        public char[] PeekChars(int count) => AtPosition(BaseStream.Position, r => r.ReadChars(count));

        public T AtPosition<T>(long position, Func<EndianBinaryReader, T> action)
        {
            long oldPosition = BaseStream.Position;
            BaseStream.Position = position;
            T result = action.Invoke(this);
            BaseStream.Position = oldPosition;
            return result;
        }

        #endregion
    }
}
