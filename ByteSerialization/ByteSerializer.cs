﻿// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using ByteSerialization.Nodes;
using System.IO;

namespace ByteSerialization
{
    public class ByteSerializer
    {
        public void Serialize(Stream stream, object value, Endianness endianness) =>
            Serialize(stream, value, endianness, out ByteSerializerContext _);

        public void Serialize(Stream stream, object value, Endianness endianness, out ByteSerializerContext context)
        {
            using var w = new EndianBinaryWriter(stream, endianness);
            var n = Node.CreateRoot(this, w, value);
            n.Serialize();
            context = n.Context;
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
    }
}
