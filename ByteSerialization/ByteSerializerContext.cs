// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Values;
using ByteSerialization.IO;
using ByteSerialization.Nodes;
using ByteSerialization.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ByteSerialization
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class ByteSerializerContext
    {
        #region Properties

        private string DebuggerDisplay =>
            $"0x{HexStringConverter.ToCompactHexString(Position)} | {Mode}";

        public ByteSerializer Serializer { get; }
        public Stream Stream { get; }
        public EndianBinaryReader Reader { get; }
        public EndianBinaryWriter Writer { get; }
        public ByteSerializerMode Mode { get; }
        public long Position
        {
            get => Stream.Position;
            set => Stream.Position = value;
        }
        public ByteSerializerGraph Graph { get; }
        public StringBuilder Log { get; }
        internal ValueComponentFactory ValueComponentFactory { get; }

        #endregion

        #region Constructor

        public ByteSerializerContext(ByteSerializer serializer, EndianBinaryReader reader) : 
            this(serializer, reader.BaseStream, reader, null, ByteSerializerMode.Deserializing)
        { }

        public ByteSerializerContext(ByteSerializer serializer, EndianBinaryWriter writer) : 
            this(serializer, writer.BaseStream, null, writer, ByteSerializerMode.Serializing)
        { }

        private ByteSerializerContext(
            ByteSerializer serializer, 
            Stream stream, 
            EndianBinaryReader reader, 
            EndianBinaryWriter writer,
            ByteSerializerMode mode)
        {
            Stream = stream;
            Reader = reader;
            Writer = writer;
            Mode = mode;
            Graph = new ByteSerializerGraph();
            Log = new StringBuilder();
            ValueComponentFactory = new ValueComponentFactory(this);
        }

        #endregion

        #region Methods

        public void ConsumeBytes(int n)
        {
            switch (Mode)
            {
                case ByteSerializerMode.Serializing:
                    Writer.Write(new byte[n]); break;
                case ByteSerializerMode.Deserializing:
                    Reader.ReadBytes(n); break;
            }
        }

        public void EnsureOffsetFrom(int offset, Node node)
        {
            long actual = Position;
            long target = node.Position.Value + offset;

            if (actual < target)
            {
                ConsumeBytes(Convert.ToInt32(target - actual));
            }
            else if (actual > target)
            {
                if (Mode == ByteSerializerMode.Deserializing)
                    Position = target; // jump back to target
                else
                    throw new InvalidOperationException(); // not supported
            }
        }

        public void EnsureAlignment(int alignment)
        {
            if (alignment != 0)
            {
                long actual = Position;
                long target = CeilingHelper.Ceiling(Position, alignment);
                ConsumeBytes(Convert.ToInt32(target - actual));
            }
        }

        #endregion
    }
}
