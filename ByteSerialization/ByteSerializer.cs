﻿// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using ByteSerialization.Nodes;
using System;
using System.Collections.Generic;
using System.IO;

namespace ByteSerialization
{
    public class ByteSerializer
    {
        #region Fields

        private readonly Dictionary<Type, ICustomSerializer> _customSerializers = 
            new Dictionary<Type, ICustomSerializer>();

        #endregion

        #region Methods (ICustomSerializer)

        internal ICustomSerializer GetCustomSerializer(Type type)
        {
            if (_customSerializers.TryGetValue(type, out var customSerializer))
                return customSerializer;
            else
                return null;
        }

        public void RegisterCustomSerializer<T>(ICustomSerializer customSerializer) =>
            _customSerializers.Add(typeof(T), customSerializer);

        #endregion

        #region Methods (deserialization)

        public T Deserialize<T>(byte[] bytes, Endianness endianness) =>
            Deserialize<T>(bytes, endianness, out ByteSerializerContext _);

        public T Deserialize<T>(byte[] bytes, Endianness endianness, out ByteSerializerContext context)
        {
            using var ms = new MemoryStream(bytes);
            return Deserialize<T>(ms, endianness, out context);
        }

        public T Deserialize<T>(Stream stream, Endianness endianness) =>
            Deserialize<T>(stream, endianness, out ByteSerializerContext _);

        public T Deserialize<T>(Stream stream, Endianness endianness, out ByteSerializerContext context)
        {
            using var r = new EndianBinaryReader(stream, endianness);
            var n = Node.CreateRoot(this, r, typeof(T));
            n.Deserialize();
            context = n.Context;
            return (T)n.Value;
        }

        #endregion

        #region Methods (serialization)

        public byte[] Serialize<T>(T value, Endianness endianness) =>
            Serialize<T>(value, endianness, out ByteSerializerContext _);

        public byte[] Serialize<T>(T value, Endianness endianness, out ByteSerializerContext context)
        {
            using var ms = new MemoryStream();
            Serialize(ms, value, endianness, out context);
            return ms.ToArray();
        }

        public void Serialize(Stream stream, object value, Endianness endianness) =>
            Serialize(stream, value, endianness, out ByteSerializerContext _);

        public void Serialize(Stream stream, object value, Endianness endianness, out ByteSerializerContext context)
        {
            using var w = new EndianBinaryWriter(stream, endianness);
            var n = Node.CreateRoot(this, w, value);
            n.Serialize();
            context = n.Context;
        }

        #endregion
    }
}
