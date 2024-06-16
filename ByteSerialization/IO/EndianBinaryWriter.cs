﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ByteSerialization.IO
{
    // TODO: make this class more like BinaryWriter

    public class EndianBinaryWriter : IDisposable
    {
        #region Fields

        private BinaryWriter writer;
        private Dictionary<Type, WriteFunc> funcs =
            new Dictionary<Type, WriteFunc>();

        #endregion

        #region Properties

        public Stream BaseStream { get; set; }
        public Endianness Endianness { get; set; }
        public ulong Count { get; private set; } = 0;

        public bool IsBigEndian => Endianness == Endianness.BigEndian;
        public bool IsLittleEndian => Endianness == Endianness.LittleEndian;

        #endregion

        #region Constructor

        public EndianBinaryWriter(Stream stream, Endianness endianness)
        {
            BaseStream = stream;
            Endianness = endianness;

            writer = new BinaryWriter(stream);
            InitFuncs();
        }

        private void InitFuncs()
        {
            funcs.Add(typeof(bool), x => Write((bool)x));
            funcs.Add(typeof(byte), x => Write((byte)x));
            funcs.Add(typeof(sbyte), x => Write((sbyte)x));
            funcs.Add(typeof(short), x => Write((short)x));
            funcs.Add(typeof(ushort), x => Write((ushort)x));
            funcs.Add(typeof(int), x => Write((int)x));
            funcs.Add(typeof(uint), x => Write((uint)x));
            funcs.Add(typeof(long), x => Write((long)x));
            funcs.Add(typeof(ulong), x => Write((ulong)x));
            funcs.Add(typeof(float), x => Write((float)x));
            funcs.Add(typeof(double), x => Write((double)x));
            funcs.Add(typeof(decimal), x => Write((decimal)x));
            funcs.Add(typeof(char), x => Write((char)x));
            funcs.Add(typeof(string), x => Write((string)x));
        }

        public void Dispose() => 
            writer.Dispose();

        #endregion

        #region Methods

        public void Write(byte value) => writer.Write(value);
        public void Write(sbyte value) => writer.Write(value);
        public void Write(bool value) => writer.Write(value);
        public void Write(short value) => writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));
        public void Write(ushort value) => writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));
        public void Write(int value) => writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));
        public void Write(uint value) => writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));
        public void Write(long value) => writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));
        public void Write(ulong value) => writer.Write(BytesSwapper.SwapIf(value, IsBigEndian));
        public void Write(float value) => writer.Write(BitConverter.GetBytes(value).ReverseIf(IsBigEndian).ToArray());
        public void Write(double value) => writer.Write(BytesSwapper.SwapIf(BitConverter.DoubleToInt64Bits(value), IsBigEndian));
        public void Write(decimal value) => throw new NotImplementedException();
        public void Write(char value) => writer.Write(value);
        public void Write(char[] value) => writer.Write(value);
        public void Write(string value) => writer.Write(value);
        public void Write(byte[] value) => writer.Write(value);

        public void Write(object value)
        {
            Type t = value.GetType();
            if (t.IsValueType)
            {
                Type underlyingNullableType = Nullable.GetUnderlyingType(t);
                if (underlyingNullableType?.IsPrimitive == true)
                {
                    object underlyingValue = Convert.ChangeType(value, underlyingNullableType);
                    WritePrimitiveType(underlyingValue);
                    return;
                }
                else if (t.IsEnum)
                {
                    Type underlyingEnumType = Enum.GetUnderlyingType(t);
                    object underlyingValue = Convert.ChangeType(value, underlyingEnumType);
                    WritePrimitiveType(underlyingValue);
                    return;
                }
                else if (t.IsPrimitive)
                {
                    WritePrimitiveType(value);
                    return;
                }
            }
            throw new ArgumentException();
        }

        public void WritePrimitiveType(object value) =>
            funcs[value.GetType()].Invoke(value);

        public void AtPosition(long position, Action<EndianBinaryWriter> action)
        {
            long oldPosition = BaseStream.Position;
            BaseStream.Position = position;
            action.Invoke(this);
            BaseStream.Position = oldPosition;
        }

        #endregion
    }
}
