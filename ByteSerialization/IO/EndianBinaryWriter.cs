// SPDX-License-Identifier: MIT

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

        private readonly BinaryWriter _writer;
        private readonly Dictionary<Type, WriteFunc> _writeFuncs = 
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
            InitWriteFuncs();
        }

        #endregion

        #region Methods (: IDisposable)

        public void Dispose() => 
            _writer.Dispose();

        #endregion

        #region Methods

        #region Methods (initialization)

        private void InitWriteFuncs()
        {
            _writeFuncs.Add(typeof(bool), x => Write((bool)x));
            _writeFuncs.Add(typeof(byte), x => Write((byte)x));
            _writeFuncs.Add(typeof(sbyte), x => Write((sbyte)x));
            _writeFuncs.Add(typeof(short), x => Write((short)x));
            _writeFuncs.Add(typeof(ushort), x => Write((ushort)x));
            _writeFuncs.Add(typeof(int), x => Write((int)x));
            _writeFuncs.Add(typeof(uint), x => Write((uint)x));
            _writeFuncs.Add(typeof(long), x => Write((long)x));
            _writeFuncs.Add(typeof(ulong), x => Write((ulong)x));
            _writeFuncs.Add(typeof(float), x => Write((float)x));
            _writeFuncs.Add(typeof(double), x => Write((double)x));
            _writeFuncs.Add(typeof(decimal), x => Write((decimal)x));
            _writeFuncs.Add(typeof(char), x => Write((char)x));
            _writeFuncs.Add(typeof(string), x => Write((string)x));
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

        public void WritePrimitiveType(object value) =>
            _writeFuncs[value.GetType()].Invoke(value);

        #endregion

        #region Methods (Write(...); other value types)

        public void Write(decimal value) =>
            throw new NotImplementedException();

        public void Write(string value) =>
            _writer.Write(value);

        #endregion

        #region Methods (Write(...); array types)

        public void Write(char[] value) =>
            _writer.Write(value);

        public void Write(byte[] value) =>
            _writer.Write(value);

        #endregion

        #region Write (Write(...); object values)

        public void Write(object value)
        {
            Type t = value.GetType();
            if (t.IsValueType)
            {
                Type underlyingNullableType = Nullable.GetUnderlyingType(t);
                if (underlyingNullableType?.IsPrimitiveOrEnum() ?? false)
                {
                    object underlyingValue = Convert.ChangeType(value, underlyingNullableType);
                    Write(underlyingValue);
                    return;
                }
                else if (t.IsEnum)
                {
                    Type underlyingEnumType = Enum.GetUnderlyingType(t);
                    object underlyingValue = Convert.ChangeType(value, underlyingEnumType);
                    Write(underlyingValue);
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
